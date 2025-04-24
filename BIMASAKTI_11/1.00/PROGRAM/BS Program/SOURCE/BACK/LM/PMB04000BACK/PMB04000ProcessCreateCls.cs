using PMB04000COMMON.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMB04000COMMON.DTO.DTOs;
using PMB04000COMMON.Context;
using System.Data.SqlClient;

namespace PMB04000BACK
{
    public class PMB04000ProcessCreateCls : R_IBatchProcess
    {
        private LoggerPMB04000 _logger;
        private readonly ActivitySource _activitySource;
        RSP_PM_CANCEL_OFFICIAL_RECEIPTResources.Resources_Dummy_Class _RSPCancel = new();
        RSP_PM_CREATE_OFFICIAL_RECEIPTResources.Resources_Dummy_Class _RSPCreate = new();
        public PMB04000ProcessCreateCls()
        {
            _logger = LoggerPMB04000.R_GetInstanceLogger();
            _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
        }

        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            string lcMethodName = nameof(R_BatchProcess);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            try
            {
                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("", "Error where Connection to database");
                    _logger.LogError(loException);
                    goto EndBlock;
                }
                var loTask = Task.Run(() =>
                {
                    _BatchProcess(poBatchProcessPar);
                });

                //while (!loTask.IsCompleted)
                //{
                //    Thread.Sleep(100);
                //}

                //if (loTask.IsFaulted)
                //{
                //    loException.Add(loTask.Exception!.InnerException != null ?
                //        loTask.Exception.InnerException :
                //        loTask.Exception);
                //    goto EndBlock;
                //}
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

        }

        private async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            string lcMethodName = nameof(_BatchProcess);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            R_Exception? loExceptionDt;
            string lcQuery = "";
            var loDb = new R_Db();
            DbCommand loCommand = null;
            bool loStatusProcess;
            string lcStatusProcess = null;
            string loStatusFinish = null;
            DbConnection loConnection = null;
            string lcCompany;
            string lcUserId;
            string lcGuidId;
            int Var_Step = 0;
            int Var_Total;
            int lnErrorCount = 0;
            string lsError;
            string lcQueryMessage;

            List<PMB04000ProcessDTO> loTempListForProcess =
                R_NetCoreUtility.R_DeserializeObjectFromByte<List<PMB04000ProcessDTO>>(poBatchProcessPar.BigObject);
            try
            {
                await Task.Delay(100);

                lcCompany = poBatchProcessPar.Key.COMPANY_ID;
                lcUserId = poBatchProcessPar.Key.USER_ID;
                lcGuidId = poBatchProcessPar.Key.KEY_GUID;

                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(PMB04000ContextDTO.CCREATE_RECEIPT_DATE)).FirstOrDefault()!.Value;
                string lcReceiptDate = ((System.Text.Json.JsonElement)loVar).GetString()!;
                loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(PMB04000ContextDTO.CTYPE_PROCESS)).FirstOrDefault()!.Value;
                string lcTypeReceipt = ((System.Text.Json.JsonElement)loVar).GetString()!;

                Var_Total = loTempListForProcess.Count;

                loCommand = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConnection);
                loConnection = loDb.GetConnection();

                switch (lcTypeReceipt)
                {
                    case "CREATE_RECEIPT":
                        lcQuery = "CREATE TABLE #RECEIPT_LIST  ( " +
                               "INO INT, " +
                               "CCOMPANY_ID VARCHAR(8), " +
                               "CPROPERTY_ID VARCHAR(20), " +
                               "CDEPT_CODE VARCHAR(20), " +
                               "CTRANS_CODE VARCHAR(10), " +
                                "CREF_NO VARCHAR(200) " +
                               ")";
                        _logger.LogDebug("CREATE TABLE #RECEIPT_LIST");
                        loDb.SqlExecNonQuery(lcQuery, loConnection, false);

                        loDb.R_BulkInsert((SqlConnection)loConnection, "#RECEIPT_LIST", loTempListForProcess);
                        _logger.LogDebug("R_BulkInsert To TABLE #RECEIPT_LIST");

                        lcQuery = "RSP_PM_CREATE_OFFICIAL_RECEIPT";
                        loCommand.CommandText = lcQuery;
                        loCommand.CommandType = CommandType.StoredProcedure;
                        loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, lcCompany);
                        loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 80, lcUserId);
                        loDb.R_AddCommandParameter(loCommand, "@CRECEIPT_DATE ", DbType.String, 8, lcReceiptDate);
                        loDb.R_AddCommandParameter(loCommand, "@GUID_KEY", DbType.String, 50, lcGuidId);
                        break;

                    case "CANCEL_RECEIPT":
                        lcQuery = "CREATE TABLE #CANCEL_RECEIPT_LIST   ( " +
                                   "INO INT, " +
                                   "CCOMPANY_ID VARCHAR(8), " +
                                   "CPROPERTY_ID VARCHAR(20), " +
                                   "CDEPT_CODE VARCHAR(20), " +
                                   "CTRANS_CODE VARCHAR(10), " +
                                    "CREF_NO VARCHAR(200) " +
                                   ")";

                        _logger.LogDebug("CREATE TABLE #CANCEL_RECEIPT_LIST ");
                        loDb.SqlExecNonQuery(lcQuery, loConnection, false);

                        loDb.R_BulkInsert((SqlConnection)loConnection, "#CANCEL_RECEIPT_LIST ", loTempListForProcess);
                        _logger.LogDebug("R_BulkInsert To TABLE #CANCEL_RECEIPT_LIST ");

                        lcQuery = "RSP_PM_CANCEL_OFFICIAL_RECEIPT";
                        loCommand.CommandText = lcQuery;
                        loCommand.CommandType = CommandType.StoredProcedure;
                        loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, lcCompany);
                        loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, lcUserId);
                        loDb.R_AddCommandParameter(loCommand, "@GUID_KEY", DbType.String, 50, lcGuidId);
                        break;

                    default:
                        break;
                }
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogInfo("Execute query : ");
                _logger.LogDebug("{@ObjectQuery(1)} {@Parameter}", loCommand.CommandText, loDbParam);
                try
                {
                    loDb.SqlExecNonQuery(loConnection, loCommand, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                    _logger!.LogError(loException);
                }
                loException.Add(R_ExternalException.R_SP_Get_Exception(loConnection));

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            finally
            {
                if (loConnection != null)
                {
                    if (!(loConnection.State == ConnectionState.Closed))
                        loConnection.Close();
                    loConnection.Dispose();
                    loConnection = null;
                }

                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null!;
                }
            }
            //HANDLE EXCEPTION IF THERE ANY ERROR ON TRY CATCH paling luar
            if (loException.Haserror)
            {
                //ST_UPLOAD_ERROR_STATUS untuk handle outer Try
                lcQueryMessage = $"INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE)" +
                                 $"VALUES " +
                                 $"( '{poBatchProcessPar.Key.COMPANY_ID}', '{poBatchProcessPar.Key.USER_ID}','{poBatchProcessPar.Key.KEY_GUID}', {100}, '{loException.ErrorList[0].ErrDescp}' );";

                loCommand!.CommandText = lcQueryMessage;
                loCommand.CommandType = CommandType.Text;

                _logger.LogInfo(string.Format("Exec query to inform framework from outer exception on cls"));
                _logger.LogDebug("{@ObjectQuery}", lcQueryMessage);

                loDb.SqlExecNonQuery(loConnection, loCommand, false);

                lcQuery = $"EXEC RSP_WriteUploadProcessStatus '{poBatchProcessPar.Key.COMPANY_ID}', " +
                   $"'{poBatchProcessPar.Key.USER_ID}', " +
                   $"'{poBatchProcessPar.Key.KEY_GUID}', " +
                   $"100, '{loException.ErrorList[0].ErrDescp}', 9";

                _logger.LogDebug("{@ObjectQuery}", lcQuery);
                loDb.SqlExecNonQuery(lcQuery);
            }
        }
    }
}
