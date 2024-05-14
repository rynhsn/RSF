using GSM02500COMMON.DTOs.GSM02503;
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
using GSM02500COMMON.DTOs.GSM02540;
using GSM02500COMMON.DTOs;
using System.Diagnostics;
using GSM02500BACK.OpenTelemetry;
using R_OpenTelemetry;

namespace GSM02500BACK
{
    public class UploadUnitPromotionTypeCls : R_IBatchProcess
    {
        RSP_GS_UPLOAD_UNIT_PROMOTION_TYPEResources.Resources_Dummy_Class _loRsp = new RSP_GS_UPLOAD_UNIT_PROMOTION_TYPEResources.Resources_Dummy_Class();

        private readonly ActivitySource _activitySource;
        public UploadUnitPromotionTypeCls()
        {
            var loActivity = UploadUnitPromotionTypeActivitySourceBase.R_GetInstanceActivitySource();
            if (loActivity == null)
            {
                _activitySource = R_LibraryActivity.R_GetInstanceActivitySource();
            }
            else
            {
                _activitySource = loActivity;
            }
        }

        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            using Activity activity = _activitySource.StartActivity("R_BatchProcess");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();

            try
            {
                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("01", "Database Connection Failed");
                    goto EndBlock;
                }

                var loTask = Task.Run(() =>
                {
                    _BatchProcess(poBatchProcessPar);
                });

                while (!loTask.IsCompleted)
                {
                    Thread.Sleep(100);
                }

                if (loTask.IsFaulted)
                {
                    loException.Add(loTask.Exception.InnerException != null ?
                        loTask.Exception.InnerException :
                        loTask.Exception);

                    goto EndBlock;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

        EndBlock:

            loException.ThrowExceptionIfErrors();
        }


        public async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            using Activity activity = _activitySource.StartActivity("_BatchProcess");
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            R_Exception loException = new R_Exception();
            string lcQuery;

            try
            {
                await Task.Delay(100);
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var loObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<UploadUnitPromotionTypeDTO>>(poBatchProcessPar.BigObject);

                var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.UPLOAD_UNIT_PROMOTION_TYPE_PROPERTY_ID_CONTEXT)).FirstOrDefault().Value;
                string PropertyId = ((System.Text.Json.JsonElement)loVar).GetString();
                
                List<UploadUnitPromotionTypeSaveDTO> loParam = new List<UploadUnitPromotionTypeSaveDTO>();

                loParam = loObject.Select(item => new UploadUnitPromotionTypeSaveDTO()
                {
                    NO = item.No,
                    UnitPromotionTypeCode = item.UnitPromotionTypeCode,
                    UnitPromotionTypeName = item.UnitPromotionTypeName,
                    GrossAreaSize = item.GrossAreaSize,
                    NetAreaSize = item.NetAreaSize,
                    CommonArea = item.CommonArea,
                    Active = item.Active,
                    NonActiveDate = item.NonActiveDate
                }).ToList();

                lcQuery = $"CREATE TABLE #UNIT_PROMOTION_TYPE " +
                    $"(NO INT, " +
                    $"UnitPromotionTypeCode VARCHAR(20), " +
                    $"UnitPromotionTypeName NVARCHAR(100), " +
                    $"GrossAreaSize NUMERIC(5,2), " +
                    $"NetAreaSize NUMERIC(5,2), " +
                    $"CommonArea NUMERIC(5,2), " +
                    $"Active BIT, " +
                    $"NonActiveDate VARCHAR(8))";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert<UploadUnitPromotionTypeSaveDTO>((SqlConnection)loConn, "#UNIT_PROMOTION_TYPE", loParam);

                lcQuery = $"EXEC RSP_GS_UPLOAD_UNIT_PROMOTION_TYPE " +
                    $"@CCOMPANY_ID, " +
                    $"@CPROPERTY_ID, " +
                    $"@CUSER_ID, " +
                    $"@KEY_GUID";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, PropertyId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@KEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

                loCmd.CommandText = lcQuery;
                loDb.SqlExecNonQuery(loConn, loCmd, false);
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
