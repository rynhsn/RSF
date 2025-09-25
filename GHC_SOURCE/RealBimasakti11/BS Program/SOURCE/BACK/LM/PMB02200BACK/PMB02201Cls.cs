using PMB02200COMMON;
using PMB02200COMMON.DTO_s;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;

namespace PMB02200BACK
{
    public class PMB02201Cls : R_IBatchProcess
    {
        RSP_PM_PROCESS_UPDATE_UTILITY_CHARGESResources.Resources_Dummy_Class _loRSPUpload = new();

        private LoggerPMB02200 _logger;

        private readonly ActivitySource _activitySource;

        public PMB02201Cls()
        {
            _logger = LoggerPMB02200.R_GetInstanceLogger();
            _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
        }

        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            R_Db loDb = new();
            _logger.LogInfo($"Start process method {MethodBase.GetCurrentMethod().Name} On {GetType().Name}");
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
            _logger.LogInfo($"End process method {MethodBase.GetCurrentMethod().Name} On {GetType().Name}");

        }

        public async Task _BatchProcessAsync(R_BatchProcessPar poBatchProcessPar)
        {
            string lcMethodName = nameof(_BatchProcessAsync);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _logger.LogInfo($"Start process method {MethodBase.GetCurrentMethod().Name} On {GetType().Name}");
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

                var loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<UtilityChargesDbDTO>>(poBatchProcessPar.BigObject);
                var loVar = poBatchProcessPar.UserParameters.FirstOrDefault((x) => x.Key.Equals(PMB02200ContextConstant.CPROPERTY_ID)).Value;
                var lcPropertyId = ((System.Text.Json.JsonElement)loVar).GetString();
                lcQuery = $"CREATE TABLE #UTILITY_CHARGES_LIST(" +
                    $"CCOMPANY_ID varchar(8)" +
                    $",CPROPERTY_ID varchar(20)" +
                    $",CDEPT_CODE varchar(20)" +
                    $",CTRANS_CODE varchar(10)" +
                    $",CREF_NO varchar(30)" +
                    $",CSEQ_NO varchar(3)" +
                    $",CUNIT_ID varchar(20)" +
                    $",CCHARGES_ID varchar(20)" +
                    $",CNEW_CHARGES_ID varchar(20)" +
                    $",CTAX_ID varchar(20)" +
                    $",CNEW_TAX_ID varchar(20)" +
                    $")";
                _logger.LogDebug("{@ObjectQuery} ", lcQuery);
                loDb.SqlExecNonQuery(lcQuery, loConn, false);
                loDb.R_BulkInsert((SqlConnection)loConn, "#UTILITY_CHARGES_LIST", loObject);

                lcQuery = "RSP_PM_PROCESS_UPDATE_UTILITY_CHARGES";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, int.MaxValue, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, int.MaxValue, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCommand, "@GUID_KEY", DbType.String, int.MaxValue, poBatchProcessPar.Key.KEY_GUID);
                _logger.LogDebug("Exec " + lcQuery + string.Join(", ", loCommand.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'")));
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
            _logger.LogInfo($"End process method {MethodBase.GetCurrentMethod().Name} On {GetType().Name}");
            loException.ThrowExceptionIfErrors();
        }
    }
}
