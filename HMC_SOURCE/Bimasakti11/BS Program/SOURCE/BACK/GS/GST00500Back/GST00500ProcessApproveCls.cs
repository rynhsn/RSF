using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using GST00500Common;
using GST00500Common.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using RSP_GS_MAINTAIN_APPROVALResources;

namespace GST00500Back
{
    public class GST00500ProcessApproveCls : R_IBatchProcess
    {
        Resources_Dummy_Class _loRSP = new();

        private LoggerGST00500 _loggerGST00500;
        private readonly ActivitySource _activitySource;
        public GST00500ProcessApproveCls()
        {
            //Initial and Get Logger
            _loggerGST00500 = LoggerGST00500.R_GetInstanceLogger();
            _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
        }
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            string lcMethodName = nameof(R_BatchProcess);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGST00500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            try
            {
                if (loDb.R_TestConnection() == false)
                {
                    //cara 1
                    // throw new Exception("Connection to database failed");

                    //cara2
                    loException.Add("", "Approve Error When Connection to database");
                    _loggerGST00500.LogError(loException);
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
                //    loException.Add(loTask.Exception.InnerException != null ?
                //        loTask.Exception.InnerException :
                //        loTask.Exception);
                //    _loggerGST00500.LogError(loException);
                //    goto EndBlock;
                //}
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGST00500.LogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _loggerGST00500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
        }

        private async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            string lcMethodName = nameof(_BatchProcess);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGST00500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            R_Exception loExceptionDt;
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

            var loTempListForProcess =
                R_NetCoreUtility.R_DeserializeObjectFromByte<List<GST00500DTO>>(poBatchProcessPar.BigObject);

            try
            {
                await Task.Delay(100);

                lcCompany = poBatchProcessPar.Key.COMPANY_ID;
                lcUserId = poBatchProcessPar.Key.USER_ID;
                lcGuidId = poBatchProcessPar.Key.KEY_GUID;
                Var_Total = loTempListForProcess.Count;

                loCommand = loDb.GetCommand();
                loConnection = loDb.GetConnection();

                lcQuery = "RSP_WRITEUPLOADPROCESSSTATUS";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CoId", DbType.String, 50, lcCompany);
                loDb.R_AddCommandParameter(loCommand, "@UserId", DbType.String, 50, lcUserId);
                loDb.R_AddCommandParameter(loCommand, "@KeyGUID", DbType.String, 50, lcGuidId);
                loDb.R_AddCommandParameter(loCommand, "@Step", DbType.Int32, 256, Var_Step);
                loDb.R_AddCommandParameter(loCommand, "@Status", DbType.String, 500,
                    "Start Processing Approve");
                loDb.R_AddCommandParameter(loCommand, "@Finish", DbType.Int32, 20, 0);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGST00500.LogInfo("Execute query : ");
                _loggerGST00500.LogDebug("{@ObjectQuery(1)} {@Parameter}", loCommand.CommandText, loDbParam);

                loDb.SqlExecNonQuery(loConnection, loCommand, false);

                Var_Step = 1;
                foreach (var item in loTempListForProcess)
                {
                    //try catch ini digunakan untuk handle error perdata yang unhandled
                    loExceptionDt = new R_Exception();
                    try
                    {
                        lcStatusProcess = string.Format("Process data {0} of  {1} ..", Var_Step, Var_Total);
                        lcQueryMessage = string.Format(
                            "EXEC RSP_WRITEUPLOADPROCESSSTATUS @CoId, @UserId, @KeyGUID, {0}, '{1}', 0",
                            Var_Step, lcStatusProcess);

                        loCommand.CommandText = lcQueryMessage;
                        loCommand.CommandType = CommandType.Text;
                        loDb.SqlExecNonQuery(loConnection, loCommand, false);
                        
                        //CALL METHOD TO EACH PROCESS JOURNAL
                        loStatusProcess = UpdateEachApprovalStatus(lcCompany, lcUserId, item, loConnection, Var_Step, lcGuidId);
                        //loStatusProcess = false;
                        if (loStatusProcess == false)
                        {
                            lnErrorCount += 1;

                            lsError = string.Format("Failed to process Master Ref. No. CREF_NO {0} !!", item.CREF_NO);
                        }
                        else
                        {
                            lsError = string.Format("Processing Master Ref. No. CREF_NO {0}: SUCCESSFUL!", item.CREF_NO);
                        }

                        lcQueryMessage = string.Format(
                            "EXEC RSP_WRITEUPLOADPROCESSSTATUS @CoId, @UserId, @KeyGUID, {0}, '{1}', 0",
                            Var_Step, lsError);

                        loCommand.CommandText = lcQueryMessage;
                        loCommand.CommandType = CommandType.Text;
                        loDb.SqlExecNonQuery(loConnection, loCommand, false);

                    }
                    catch (Exception ex)
                    {
                        loExceptionDt.Add(ex);
                        _loggerGST00500.LogError(loExceptionDt);
                    }
                    //UNHANDLED Error
                    if (loExceptionDt.Haserror)
                    {
                        lnErrorCount += 1;

                        lcQueryMessage = $"INSERT INTO GST_UPLOAD_ERROR_STATUS (CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE)" +
                                         $"VALUES " +
                                         $"( '{lcCompany}', '{lcUserId}','{lcGuidId}', {Var_Step}, '{loExceptionDt.ErrorList.FirstOrDefault().ErrDescp}') ;";

                        loCommand.CommandText = lcQueryMessage;
                        loCommand.CommandType = CommandType.Text;
                        _loggerGST00500.LogDebug("{@ObjectQuery}", lcQueryMessage);
                        loDb.SqlExecNonQuery(loConnection, loCommand, false);
                        
                        lcQueryMessage = string.Format(
                            "EXEC RSP_WRITEUPLOADPROCESSSTATUS @CoId, @UserId, @KeyGUID, {0}, '{1}', 0",
                            Var_Step, loExceptionDt.ErrorList.FirstOrDefault().ErrDescp);

                        loCommand.CommandText = lcQueryMessage;
                        loCommand.CommandType = CommandType.Text;
                        _loggerGST00500.LogDebug("{@ObjectQuery}", lcQueryMessage);
                        loDb.SqlExecNonQuery(loConnection, loCommand, false);
                    }

                    Var_Step += 1;
                }
                //Check if there is error on the process
                var flag = (lnErrorCount == 0) ? 1 : 9;

                if (flag == 1)
                {
                    loStatusFinish = "Finish Processing Approval!!";
                }
                else
                {
                    loStatusFinish = "Finish Processing Approval but fail !!";
                }

                //Close The Process to inform framework
                var lcQueryFinish =
                    $@"EXEC RSP_WRITEUPLOADPROCESSSTATUS @CoId, @UserId, @KeyGUID, '{Var_Step}', '{loStatusFinish}', '{flag}'";
                loCommand.CommandText = lcQueryFinish;
                loCommand.CommandType = CommandType.Text;
                _loggerGST00500.LogInfo(string.Format("Exec query to inform framework Cls"));
                _loggerGST00500.LogDebug("{@ObjectQuery}", lcQueryFinish);
                loDb.SqlExecNonQuery(loConnection, loCommand, false);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGST00500.LogError(loException);
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
                    loCommand = null;
                }
            }
            //HANDLE EXCEPTION IF THERE ANY ERROR ON TRY CATCH paling luar
            if (loException.Haserror)
            {
                //Lakukan penambahan pada GST_UPLOAD_ERROR_STATUS untuk handle Try catch paling luar

                lcQueryMessage = $"INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE)" +
                                 $"VALUES " +
                                 $"( '{poBatchProcessPar.Key.COMPANY_ID}', '{poBatchProcessPar.Key.USER_ID}','{poBatchProcessPar.Key.KEY_GUID}', {100}, '{loException.ErrorList[0].ErrDescp}' );";

                loCommand.CommandText = lcQueryMessage;
                loCommand.CommandType = CommandType.Text;
                _loggerGST00500.LogInfo(string.Format("Exec query to inform framework from outer exception on cls"));
                _loggerGST00500.LogDebug("{@ObjectQuery}", lcQueryMessage);
                loDb.SqlExecNonQuery(loConnection, loCommand, true);

                lcQuery = $"EXEC RSP_WriteUploadProcessStatus '{poBatchProcessPar.Key.COMPANY_ID}', " +
                          $"'{poBatchProcessPar.Key.USER_ID}', " +
                          $"'{poBatchProcessPar.Key.KEY_GUID}', " +
                          $"100, '{loException.ErrorList[0].ErrDescp}', 9";
                _loggerGST00500.LogDebug("{@ObjectQuery}", lcQuery);
                _loggerGST00500.LogInfo("Exec query to inform framework that process upload is finished");
                loDb.SqlExecNonQuery(loConnection, loCommand, true);
            }

            _loggerGST00500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
        }

        private bool UpdateEachApprovalStatus(string Company, string UserId, GST00500DTO item, DbConnection poConnection, int step, string guid)
        {
            string lcMethodName = nameof(UpdateEachApprovalStatus);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGST00500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
           
            var loEx = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCommand = null;
            bool lbRtn = false;
            string lcQuery;

            try
            {
                DataTable loReturnTemp = new DataTable();
                loConn = poConnection;
                loCommand = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);

                lcQuery = "RSP_GS_MAINTAIN_APPROVAL";
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, Company);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, UserId);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 8, item.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 8, item.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, item.CREF_NO);
                loDb.R_AddCommandParameter(loCommand, "@CREJECT_REASON", DbType.String, 255, "");
                loDb.R_AddCommandParameter(loCommand, "@CREJECT_NOTES", DbType.String, 512, "");
                loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 2, "01");

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGST00500.LogInfo("Execute query");
                _loggerGST00500.LogDebug("{@ObjectQuery(1)} {@Parameter}", loCommand.CommandText, loDbParam);

                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                try
                {
                    loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                    _loggerGST00500.LogError(loEx);
                }
                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

                if (loReturnTemp.HasErrors == false)
                {
                    lbRtn = true;
                }
                else
                {
                    lbRtn = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerGST00500.LogError(loEx);
            }
            finally
            {
                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
            _loggerGST00500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return lbRtn;

        }

    }
}
