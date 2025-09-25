﻿    using GSM02500COMMON.DTOs.GSM02530;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02520;
using System.Diagnostics;
using GSM02500BACK.OpenTelemetry;
using R_OpenTelemetry;
using GSM02500COMMON.Loggers;

namespace GSM02500BACK
{
    public class UploadUnitTypeCategoryCls : R_IBatchProcessAsync
    {
        RSP_GS_UPLOAD_PROPERTY_UNIT_TYPE_CTGResources.Resources_Dummy_Class _loRsp = new RSP_GS_UPLOAD_PROPERTY_UNIT_TYPE_CTGResources.Resources_Dummy_Class();

        private readonly ActivitySource _activitySource;
        private LoggerGSM02502 _logger;
        public UploadUnitTypeCategoryCls()
        {
            _logger = LoggerGSM02502.R_GetInstanceLogger();
            var loActivity = UploadUnitTypeCategoryActivitySourceBase.R_GetInstanceActivitySource();
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
            string lcQuery;

            try
            {
                //await Task.Delay(100);
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();

                var loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<UploadUnitTypeCategoryDTO>>(poBatchProcessPar.BigObject);

                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.UPLOAD_UNIT_TYPE_CATEGORY_PROPERTY_ID_CONTEXT)).FirstOrDefault().Value;
                string PropertyId = ((System.Text.Json.JsonElement)loVar).GetString();


                List<UploadUnitTypeCategorySaveDTO> loParam = new List<UploadUnitTypeCategorySaveDTO>();

                loParam = loObject.Select(item => new UploadUnitTypeCategorySaveDTO()
                {
                    NO = item.No,
                    UnitTypeCategoryId = item.UnitTypeCategoryCode,
                    UnitTypeCategoryName = item.UnitTypeCategoryName,
                    Description = item.Description,
                    DepartmentCode = item.DepartmentCode,
                    PropertyType = item.PropertyType,
                    InvitationInvoicePeriod = item.InvitationInvoicePeriod,
                    Active = item.Active,
                    NonActiveDate = item.NonActiveDate
                }).ToList();

                _logger.LogInfo("Start Inser Bulk");
                lcQuery = $"CREATE TABLE #UNIT_TYPE_CATEGORY " +
                    $"(NO INT, " +
                    $"UnitTypeCategoryId VARCHAR(100), " +
                    $"UnitTypeCategoryName NVARCHAR(200), " +
                    $"Description NVARCHAR(MAX), " +
                    $"DepartmentCode NVARCHAR(20), " +
                    $"PropertyType VARCHAR(2), " +
                    $"InvitationInvoicePeriod INT, " +
                    $"Active BIT, " +
                    $"NonActiveDate VARCHAR(8))";

                await loDb.SqlExecNonQueryAsync(lcQuery, loConn, false);

                await loDb.R_BulkInsertAsync<UploadUnitTypeCategorySaveDTO>((SqlConnection)loConn, "#UNIT_TYPE_CATEGORY", loParam);
                _logger.LogInfo("End Inser Bulk");

                lcQuery = $"EXEC RSP_GS_UPLOAD_PROPERTY_UNIT_TYPE_CTG " +
                    $"@CCOMPANY_ID, " +
                    $"@CPROPERTY_ID, " +
                    $"@CUSER_ID, " +
                    $"@KEY_GUID";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, PropertyId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@KEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

                loCmd.CommandText = lcQuery;

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _logger.LogDebug("EXEC RSP_GS_UPLOAD_PROPERTY_UNIT_TYPE_CTG {@poParameter}", loDbParam);

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
