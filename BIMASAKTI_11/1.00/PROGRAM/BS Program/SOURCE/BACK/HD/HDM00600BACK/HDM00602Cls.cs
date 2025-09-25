using HDM00600COMMON;
using HDM00600COMMON.DTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;

namespace HDM00600BACK
{
    public class HDM00602Cls : R_IBatchProcess //upload pricelist
    {
        private RSP_HD_UPLOAD_PRICELIST_PROCESSResources.Resources_Dummy_Class _rsp = new();
        private PricelistMaster_Logger _logger;
        private readonly ActivitySource _activitySource;
        public HDM00602Cls()
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
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbCommand loCmd = null;
            DbConnection loConn = null;
            var lcQuery = "";
            _logger.LogInfo($"Start process method {MethodBase.GetCurrentMethod().Name} On {GetType().Name}");
            try
            {
                await Task.Delay(100);
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                // creation temptable
                lcQuery += "CREATE TABLE #UPLOAD_PRICELIST(" +
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

                //exec
                _logger.LogInfo(lcQuery);
                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                //get param header
                var loVar = poBatchProcessPar.UserParameters.FirstOrDefault((x) => x.Key.Equals(PricelistMaster_ContextConstant.CPROPERTY_ID)).Value;
                var lcPropertyId = ((System.Text.Json.JsonElement)loVar).GetString();

                //deserialize object list
                var loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<PricelistBatchDTO>>(poBatchProcessPar.BigObject).ToList();

                //logger data bulkinsert
                foreach (var item in loObject)
                {
                    var logMessage = $"INSERT INTO #UPLOAD_PRICELIST : {string.Join(", ", item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(p => p.GetValue(item)?.ToString() ?? ""))}";
                    _logger.LogInfo(logMessage);
                }

                //exec bulkinsert
                loDb.R_BulkInsert<PricelistBatchDTO>((SqlConnection)loConn, "#UPLOAD_PRICELIST", loObject);

                //query exec rsp upload
                lcQuery = "RSP_HD_UPLOAD_PRICELIST_PROCESS";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, lcPropertyId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, int.MaxValue, poBatchProcessPar.Key.KEY_GUID);

                _logger.LogDebug("EXEC " + lcQuery + string.Join(", ", loCmd.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'")));
                //exec rsp uploads
                loDb.SqlExecNonQuery(loConn, loCmd, false);
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
                    if (loConn.State != System.Data.ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
            }
            if (loException.Haserror)
            {
                lcQuery = $"INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE)" +
                            $"VALUES " +
                            $"('{poBatchProcessPar.Key.COMPANY_ID}', " +
                            $"'{poBatchProcessPar.Key.USER_ID}', " +
                            $"'{poBatchProcessPar.Key.KEY_GUID}', " +
                            $"-100, " +
                            $"'{loException.ErrorList[0].ErrDescp}');";

                loDb.SqlExecNonQuery(lcQuery);

                lcQuery = $"EXEC RSP_WriteUploadProcessStatus '{poBatchProcessPar.Key.COMPANY_ID}', " +
                    $"'{poBatchProcessPar.Key.USER_ID}', " +
                    $"'{poBatchProcessPar.Key.KEY_GUID}', " +
                    $"100, '{loException.ErrorList[0].ErrDescp}', 9";

                loDb.SqlExecNonQuery(lcQuery);
            }
        }
    }
}
