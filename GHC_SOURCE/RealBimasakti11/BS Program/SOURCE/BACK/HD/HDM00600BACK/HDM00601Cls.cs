using HDM00600COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using HDM00600COMMON.DTO;
using System.Data.SqlClient;

namespace HDM00600BACK
{
    public class HDM00601Cls : R_IBatchProcess //add pricelist batch class
    {
        //varr & const
        private RSP_HD_ADD_PRICELIST_PROCESSResources.Resources_Dummy_Class _rsp;
        private PricelistMaster_Logger _logger;
        private readonly ActivitySource _activitySource;
        public HDM00601Cls()
        {
            _logger = PricelistMaster_Logger.R_GetInstanceLogger();
            _activitySource = PricelistMaster_Activity.R_GetInstanceActivitySource();
        }

        //method
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
                //Get data from poBatchPRocessParam
                var loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<PricelistBatchDTO>>(poBatchProcessPar.BigObject);

                //get parameter
                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(PricelistMaster_ContextConstant.CPROPERTY_ID)).FirstOrDefault().Value;
                var lcPropertyId = ((System.Text.Json.JsonElement)loVar).GetString();

                lcQuery = "CREATE TABLE #NEW_PRICELIST(" +
                    "NO INT" +
                    ",CPRICELIST_ID VARCHAR(20)" +
                    ",CPRICELIST_NAME NVARCHAR(100)" +
                    ",CDEPT_CODE VARCHAR(20)" +
                    ",CCHARGES_ID VARCHAR(20)" +
                    ",CUNIT VARCHAR(10)" +
                    ",CCURRENCY_CODE VARCHAR(3)" +
                    ",IPRICE INT" +
                    ",CDESCRIPTION NVARCHAR(500)" +
                    ",CSTART_DATE VARCHAR(8)" +
                    ")";

                _logger.LogDebug("{@ObjectQuery} ", lcQuery);

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert((SqlConnection)loConn, "#NEW_PRICELIST", loObject);

                lcQuery = "RSP_HD_ADD_PRICELIST_PROCESS";
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
            _logger.LogInfo($"End process method {MethodBase.GetCurrentMethod().Name} On {GetType().Name}");
            loException.ThrowExceptionIfErrors();
        }
    }
}
