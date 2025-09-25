using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using GSM04500Common;
using System.Transactions;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using GSM04500Common.Logs;
using System.Diagnostics;

namespace GSM04500Back
{
    public class GSM04500UploadJournalGroupCls : R_IBatchProcess
    {
        RSP_GS_MAINTAIN_JOURNAL_GROUP_ACCOUNT_DEPTResources.Resources_Dummy_Class _loRSPGroupAcc = new();
        RSP_GS_MAINTAIN_JOURNAL_GROUPResources.Resources_Dummy_Class _loRSPGroup = new();
        RSP_GS_UPLOAD_JOURNAL_GROUPResources.Resources_Dummy_Class _loRSPUpload = new();

        private LoggerGSM04500 _loggerGSM04500;
        private readonly ActivitySource _activitySource;
        public GSM04500UploadJournalGroupCls()
        {
            //Initial and Get Logger
            _loggerGSM04500 = LoggerGSM04500.R_GetInstanceLogger();
            _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
        }
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            string lcMethodName = nameof(R_BatchProcess);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerGSM04500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            var loDb = new R_Db();

            try
            {
                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("000", "Database Connection Failed");
                    _loggerGSM04500.LogError(loException);
                    goto EndBlock;
                }

                var loTask = Task.Run(() =>
                {
                    _BatchProcess(poBatchProcessPar);
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM04500.LogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _loggerGSM04500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
        }

        public async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            string lcMethodName = nameof(_BatchProcess);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGSM04500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            string lcQuery = "";
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand? loCommand = null;
            try
            {
                await Task.Delay(100);

                loCommand = loDb.GetCommand();
                loConn = loDb.GetConnection();
                //Get data from poBatchPRocessParam
                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<GSM04500UploadErrorValidateDTO>>(poBatchProcessPar.BigObject);
                //CONVERT DATA, SO TO BE READY INSERT TO TEMPORARY TABLE 
                var loObject = R_Utility.R_ConvertCollectionToCollection<GSM04500UploadErrorValidateDTO, GSM04500FieldTemporaryTableDTO>(loTempObject);

                #region GetParameterPropert
                //get parameter
                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.CPROPERTY_ID)).FirstOrDefault().Value;
                var lcPropertyId = ((System.Text.Json.JsonElement)loVar).GetString();

                var loVar2 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.CJRNGRP_TYPE)).FirstOrDefault().Value;
                var lcJournalGroupType = ((System.Text.Json.JsonElement)loVar2).GetString();
                #endregion

                lcQuery = $"CREATE TABLE #JRNLGROUP " +
                          $"(No INT, " +
                          $"JournalGroup VARCHAR(100), " +
                          $"JournalGroupName VARCHAR(200), " +
                          $"EnableAccrual BIT )";

                _loggerGSM04500.LogDebug("{@ObjectQuery} ", lcQuery);

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert((SqlConnection)loConn, "#JRNLGROUP", loObject);

                lcQuery = "RSP_GS_UPLOAD_JOURNAL_GROUP";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poBatchProcessPar.Key.USER_ID);
             //   loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, lcPropertyId);
                loDb.R_AddCommandParameter(loCommand, "@CJOURNAL_GROUP_TYPE", DbType.String, 20, lcJournalGroupType);
                loDb.R_AddCommandParameter(loCommand, "@CKEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGSM04500.LogInfo("Execute query : ");
                _loggerGSM04500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                
                var abc = loDb.SqlExecNonQuery(loConn, loCommand, false);

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGSM04500.LogError(loException);
            }

            finally
            {
                if (loConn != null)
                {
                    if (!(loConn.State == ConnectionState.Closed))
                        loConn.Close();
                    loConn.Dispose();
                    loConn = null;
                }

                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
            }
            //HANDLE EXCEPTION IF THERE ANY ERROR ON TRY CATCH paling luar
            if (loException.Haserror)
            {
                lcQuery = "INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE) VALUES" +
                    string.Format("('{0}', '{1}', ", poBatchProcessPar.Key.COMPANY_ID, poBatchProcessPar.Key.USER_ID) +
                    string.Format("'{0}', -1, '{1}')", poBatchProcessPar.Key.KEY_GUID, loException.ErrorList[0].ErrDescp);

                loDb.SqlExecNonQuery(lcQuery);
                _loggerGSM04500.LogInfo(string.Format("Exec query to inform framework from outer exception on cls"));
                _loggerGSM04500.LogDebug("{@ObjectQuery}", lcQuery);

                lcQuery = string.Format("EXEC RSP_WriteUploadProcessStatus '{0}', ", poBatchProcessPar.Key.COMPANY_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.USER_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.KEY_GUID) +
                   string.Format("100, '{0}', 9", loException.ErrorList[0].ErrDescp);

                _loggerGSM04500.LogDebug("{@ObjectQuery}", lcQuery);
                _loggerGSM04500.LogInfo("Exec query to inform framework that process upload is finished");
                loDb.SqlExecNonQuery(lcQuery);
            }
            _loggerGSM04500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

        }
    }
}
