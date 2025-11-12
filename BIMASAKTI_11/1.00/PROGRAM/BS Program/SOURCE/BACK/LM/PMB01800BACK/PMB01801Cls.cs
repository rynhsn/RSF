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
                
                loCommand = loDb.GetCommand();
                loConn = await loDb.GetConnectionAsync();
                //Get data from poBatchPRocessParam
                var loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<PMB01800BatchDTO>>(poBatchProcessPar.BigObject);

                //get parameter
                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(Batch_ContextConstant.CPROPERTY_ID)).FirstOrDefault().Value;
                var lcPropertyId = ((System.Text.Json.JsonElement)loVar).GetString();

                var loVarRefDate = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(Batch_ContextConstant.CREF_DATE)).FirstOrDefault().Value;
                var lcRefDate = ((System.Text.Json.JsonElement)loVarRefDate).GetString();

                var loVarDeptCode = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(Batch_ContextConstant.CDEPT_CODE)).FirstOrDefault().Value;
                var lcDeptCode = ((System.Text.Json.JsonElement)loVarDeptCode).GetString();

                var loVarChargesId = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(Batch_ContextConstant.CCHARGES_ID)).FirstOrDefault().Value;
                var lcChargesId = ((System.Text.Json.JsonElement)loVarChargesId).GetString();

                lcQuery = "CREATE TABLE #SELECTED_DEPOSIT(" +
                    "INO INT" +
                    ",CDEPT_CODE VARCHAR(20)" +
                    ",CTRANS_CODE VARCHAR(10)" +
                    ",CREF_NO VARCHAR(30)" +
                    ",CSEQ_NO VARCHAR(3)" +
                    ")";


                _logger.LogDebug("{@ObjectQuery} ", lcQuery);

                await loDb.SqlExecNonQueryAsync(lcQuery, loConn, false);

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

               await loDb.R_BulkInsertAsync((SqlConnection)loConn, "#SELECTED_DEPOSIT", loObject);

                lcQuery = "RSP_PM_GENERATE_DEPOSIT_ADJ ";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, lcPropertyId);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, lcDeptCode);
                loDb.R_AddCommandParameter(loCommand, "@CCHARGES_ID", DbType.String, 20, lcChargesId);
                loDb.R_AddCommandParameter(loCommand, "@CREF_DATE", DbType.String, 8, lcRefDate);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CKEY_GUID", DbType.String, 100, poBatchProcessPar.Key.KEY_GUID);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCommand.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} ='{p.Value}'")));
                var loRtn = await loDb.SqlExecNonQueryAsync(loConn, loCommand, false);
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
                string lcMessageError = loException.ErrorList[0].ErrDescp.Replace("'", "`");
                lcQuery = "INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE) VALUES" +
                    string.Format("('{0}', '{1}', ", poBatchProcessPar.Key.COMPANY_ID, poBatchProcessPar.Key.USER_ID) +
                    string.Format("'{0}', -1, '{1}')", poBatchProcessPar.Key.KEY_GUID, lcMessageError);
                await loDb.SqlExecNonQueryAsync(lcQuery);

                lcQuery = string.Format("EXEC RSP_WriteUploadProcessStatus '{0}', ", poBatchProcessPar.Key.COMPANY_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.USER_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.KEY_GUID) +
                   string.Format("100, '{0}', 9", lcMessageError);

                await loDb.SqlExecNonQueryAsync(lcQuery);
            }
            _logger.LogInfo(string.Format("End process method on Cls", nameof(_BatchProcessAsync)));
            loException.ThrowExceptionIfErrors();
        }

    }
}
