using CBT02300COMMON;
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
using System.Collections;

namespace CBT02300BACK
{
    public class CBT02300ProcessCls : R_IBatchProcess
    {
        //Resources_Dummy_Class _loRSP = new();
        private LoggerCBT02300 _loggerCBT02300;
        private readonly ActivitySource _activitySource;
        public CBT02300ProcessCls()
        {
            _loggerCBT02300 = LoggerCBT02300.R_GetInstanceLogger();
            _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
        }

        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            string lcMethodName = nameof(R_BatchProcess);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerCBT02300.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            try
            {
                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("", "Error where Connection to database");
                    _loggerCBT02300.LogError(loException);
                    goto EndBlock;
                }
                loException.Add("", "End Test Connection");
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

                //    goto EndBlock;
                //}
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerCBT02300.LogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            _loggerCBT02300.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
        }
        private async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            string lcMethodName = nameof(_BatchProcess);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerCBT02300.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

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

            List<string> loTempListForProcess =
                R_NetCoreUtility.R_DeserializeObjectFromByte<List<string>>(poBatchProcessPar.BigObject);

            string lcDataToProcess = string.Join(",", loTempListForProcess);

            try
            {
                await Task.Delay(100);

                lcCompany = poBatchProcessPar.Key.COMPANY_ID;
                lcUserId = poBatchProcessPar.Key.USER_ID;
                lcGuidId = poBatchProcessPar.Key.KEY_GUID;

                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.CPROCESS_DATE)).FirstOrDefault().Value;
                string lcDate = ((System.Text.Json.JsonElement)loVar).GetString();

                var loVar2 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.CACTION)).FirstOrDefault().Value;
                string lcAction = ((System.Text.Json.JsonElement)loVar2).GetString();
                var loVar3 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.CREASON)).FirstOrDefault().Value;
                string lcReason = ((System.Text.Json.JsonElement)loVar3).GetString();

                Var_Total = loTempListForProcess.Count;

                loCommand = loDb.GetCommand();
                loConnection = loDb.GetConnection();

                lcQuery = "RSP_CB_PROCESS_BANK_IN_CHEQUE";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, lcCompany);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, lcUserId);

                loDb.R_AddCommandParameter(loCommand, "@CACTION ", DbType.String, 20, lcAction);
                loDb.R_AddCommandParameter(loCommand, "@CPROCESS_DATE", DbType.String, 8, lcDate);
                loDb.R_AddCommandParameter(loCommand, "@CREASON", DbType.String, 100, lcReason);
                loDb.R_AddCommandParameter(loCommand, "@CREC_ID_LIST", DbType.String, int.MaxValue, lcDataToProcess);

                loDb.R_AddCommandParameter(loCommand, "@CKEY_GUID", DbType.String, 50, lcGuidId);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerCBT02300.LogInfo("Execute query : ");
                _loggerCBT02300.LogDebug("{@ObjectQuery(1)} {@Parameter}", loCommand.CommandText, loDbParam);

                loDb.SqlExecNonQuery(loConnection, loCommand, false);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerCBT02300.LogError(loException);
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

                _loggerCBT02300.LogInfo(string.Format("Exec query to inform framework from outer exception on cls"));
                _loggerCBT02300.LogDebug("{@ObjectQuery}", lcQueryMessage);

                loDb.SqlExecNonQuery(loConnection, loCommand, false);

                lcQuery = $"EXEC RSP_WriteUploadProcessStatus '{poBatchProcessPar.Key.COMPANY_ID}', " +
                   $"'{poBatchProcessPar.Key.USER_ID}', " +
                   $"'{poBatchProcessPar.Key.KEY_GUID}', " +
                   $"100, '{loException.ErrorList[0].ErrDescp}', 9";

                _loggerCBT02300.LogDebug("{@ObjectQuery}", lcQuery);
                loDb.SqlExecNonQuery(lcQuery);
            }
        }
    }
}
