using PMB01800COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PMB01800COMMON.DTOs;
using System.Data.SqlClient;

namespace PMB01800BACK
{
    public class PMB01801Cls : R_IBatchProcess
    {
        private RSP_PM_GENERATE_DEPOSIT_ADJResources.Resources_Dummy_Class _rsp = new();
        private LoggerPMB01800 _logger;
        private readonly ActivitySource _activitySource;
        public PMB01801Cls()
        {
            _logger = LoggerPMB01800.R_GetInstanceLogger();
            _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
        }
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            using var activity = _activitySource.StartActivity(nameof(R_BatchProcess));
            R_Exception loEx = new R_Exception();
            R_Db loDb = new();
            _logger.LogInfo(string.Format("START process method {0} on Cls", nameof(R_BatchProcess)));
            try
            {
                _logger.LogInfo("start test connection");
                if (loDb.R_TestConnection() == false)
                {
                    loEx.Add("", "Database Connection Failed");
                    _logger.LogError(loEx);
                    goto EndBlock;
                }
                _logger.LogInfo("end test connection");

                _logger.LogInfo("start run _BatchProcess");
                var loTask = Task.Run(() =>
                {
                    _BatchProcessAsync(poBatchProcessPar);
                });
                _logger.LogInfo("end run _BatchProcess");

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("End process method on Cls", nameof(R_BatchProcess)));

        }

        public async Task _BatchProcessAsync(R_BatchProcessPar poBatchProcessPar)
        {
            using var Activity = _activitySource.StartActivity(nameof(_BatchProcessAsync));
            _logger.LogInfo(string.Format("START process method {0} on Cls", nameof(_BatchProcessAsync)));
            R_Exception loException = new R_Exception();
            string lcQuery = "";
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCommand = null;
            try
            {
                await Task.Delay(100);
                loCommand = loDb.GetCommand();
                loConn = loDb.GetConnection();
                //Get data from poBatchPRocessParam
                var loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<PMB01800BatchDTO>>(poBatchProcessPar.BigObject);

                //get parameter
                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(Batch_ContextConstant.CPROPERTY_ID)).FirstOrDefault().Value;
                var lcPropertyId = ((System.Text.Json.JsonElement)loVar).GetString();

                lcQuery = "CREATE TABLE #SELECTED_DEPOSIT(" +
                    "INO INT" +
                    ",CDEPT_CODE VARCHAR(20)" +
                    ",CTRANS_CODE VARCHAR(10)" +
                    ",CREF_NO VARCHAR(30)" +
                    ",CSEQ_NO VARCHAR(3)" +
                    ")";


                _logger.LogDebug("{@ObjectQuery} ", lcQuery);

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                for (var i = 0; i < loObject.Count; i++)
                {
                    _logger.LogDebug($"INSERT INTO #SELECTED_DEPOSIT " +
                                     $"VALUES (" +
                                     $"{loObject[i].INO}, " +
                                     $"'{loObject[i].CDEPT_CODE}', " +
                                     $"'{loObject[i].CTRANS_CODE}', " +
                                     $"'{loObject[i].CREF_NO}', " +
                                     $"'{loObject[i].CSEQ_NO}', " +
                                     $")");
                }

                loDb.R_BulkInsert((SqlConnection)loConn, "#SELECTED_DEPOSIT", loObject);

                lcQuery = "RSP_PM_GENERATE_DEPOSIT_ADJ ";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, int.MaxValue, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, int.MaxValue, lcPropertyId);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, int.MaxValue, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CKEY_GUID", DbType.String, int.MaxValue, poBatchProcessPar.Key.KEY_GUID);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCommand.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'")));
                var loRtn = loDb.SqlExecNonQuery(loConn, loCommand, false);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
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
                lcQuery = string.Format("EXEC RSP_WRITEUPLOADPROCESSSTATUS '{0}', '{1}', '{2}', 100, '{3}', {4}", poBatchProcessPar.Key.COMPANY_ID, poBatchProcessPar.Key.USER_ID, poBatchProcessPar.Key.KEY_GUID, loException.ErrorList[0].ErrDescp, 9);
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;
                loDb.SqlExecNonQuery(lcQuery);
            }
            _logger.LogInfo(string.Format("End process method on Cls", nameof(_BatchProcessAsync)));
            loException.ThrowExceptionIfErrors();
        }

    }
}
