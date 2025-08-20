using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using GSM02500COMMON.DTOs.GSM02520;
using System.Data.SqlClient;
using GSM02500COMMON.DTOs;
using System.Diagnostics;
using GSM02500BACK.OpenTelemetry;
using R_OpenTelemetry;
using GSM02500COMMON.Loggers;

namespace GSM02500BACK
{
    public class UploadFloorCls : R_IBatchProcessAsync
    {
        RSP_GS_UPLOAD_PROPERTY_FLOORResources.Resources_Dummy_Class _loRsp = new RSP_GS_UPLOAD_PROPERTY_FLOORResources.Resources_Dummy_Class();
        private readonly ActivitySource _activitySource;
        private LoggerGSM02502 _logger;

        public UploadFloorCls()
        {
            _logger = LoggerGSM02502.R_GetInstanceLogger();
            var loActivity = UploadFloorActivitySourceBase.R_GetInstanceActivitySource();
            if (loActivity == null)
            {
                _activitySource = R_LibraryActivity.R_GetInstanceActivitySource();
            }
            else
            {
                _activitySource = loActivity;
            }
        }

        public async Task R_BatchProcessAsync(R_BatchProcessPar poBatchProcessPar)
        {
            using Activity activity = _activitySource.StartActivity("R_BatchProcess");
            R_Exception loException = new R_Exception();
            var loDb = new R_Db();

            try
            {
                _logger.LogInfo("Test Connection");
                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("01", "Database Connection Failed");
                    goto EndBlock;
                }
                _logger.LogInfo("Start Batch");
                _R_BatchProcessAsync(poBatchProcessPar);
                _logger.LogInfo("End Batch");
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            finally
            {
                if (loDb != null)
                {
                    loDb = null;
                }
            }
        EndBlock:

            loException.ThrowExceptionIfErrors();
        }
        private async Task _R_BatchProcessAsync(R_BatchProcessPar poBatchProcessPar)
        {
            using Activity activity = _activitySource.StartActivity("_BatchProcess");
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            R_Exception loException = new R_Exception();
            string lcQuery = "";

            try
            {
                //await Task.Delay(100);
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();

                var liFinishFlag = 1; //0=Process, 1=Success, 9=Fail
                var loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<UploadFloorDTO>>(poBatchProcessPar.BigObject);

                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.UPLOAD_FLOOR_PROPERTY_ID_CONTEXT)).FirstOrDefault().Value;
                string PropertyId = ((System.Text.Json.JsonElement)loVar).GetString();

                var loVar2 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.UPLOAD_FLOOR_BUILDING_ID_CONTEXT)).FirstOrDefault().Value;
                string BuildingId = ((System.Text.Json.JsonElement)loVar2).GetString();

                List<UploadFloorSaveDTO> loParam = new List<UploadFloorSaveDTO>();

                loParam = loObject.Select(item => new UploadFloorSaveDTO()
                {
                    NO = item.No,
                    FloorCode = item.FloorCode,
                    FloorName = item.FloorName,
                    Description = item.Description,
                    Active = item.Active,
                    NonActiveDate = item.NonActiveDate,
                    UnitType = item.UnitType,
                    UnitCategory = item.UnitCategory
                }).ToList();

                _logger.LogInfo("Start Inser Bulk");
                lcQuery = $"CREATE TABLE #FLOOR " +
                    $"(NO INT, " +
                    $"FloorCode VARCHAR(20), " +
                    $"FloorName VARCHAR(100), " +
                    $"Description NVARCHAR(MAX), " +
                    $"Active BIT, " +
                    $"NonActiveDate	VARCHAR(8), " +
                    $"UnitType VARCHAR(20), " +
                    $"UnitCategory VARCHAR(2))";

                await loDb.SqlExecNonQueryAsync(lcQuery, loConn, false);

                await loDb.R_BulkInsertAsync<UploadFloorSaveDTO>((SqlConnection)loConn, "#FLOOR", loParam);

                _logger.LogInfo("End Inser Bulk");

                lcQuery = $"EXEC RSP_GS_UPLOAD_PROPERTY_FLOOR " +
                    $"@CCOMPANY_ID, " +
                    $"@CPROPERTY_ID, " +
                    $"@CBUILDING_ID, " +
                    $"@CUSER_ID, " +
                    $"@CKEY_GUID";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, PropertyId);
                loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 50, BuildingId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

                loCmd.CommandText = lcQuery;

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _logger.LogDebug("EXEC RSP_GS_UPLOAD_PROPERTY_FLOOR {@poParameter}", loDbParam);

                await loDb.SqlExecNonQueryAsync(loConn, loCmd, false);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
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

                await loDb.SqlExecNonQueryAsync(lcQuery);

                lcQuery = $"EXEC RSP_WriteUploadProcessStatus '{poBatchProcessPar.Key.COMPANY_ID}', " +
                    $"'{poBatchProcessPar.Key.USER_ID}', " +
                    $"'{poBatchProcessPar.Key.KEY_GUID}', " +
                    $"100, '{loException.ErrorList[0].ErrDescp}', 9";

                await loDb.SqlExecNonQueryAsync(lcQuery);
            }
        }
    }
}
