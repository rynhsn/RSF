using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using R_OpenTelemetry;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT02100COMMON.DTOs.PMT02100;
using System.Data.SqlClient;
using PMT02100COMMON;

namespace PMT02100BACK
{
    public class PMT02101Cls : R_IBatchProcessAsync
    {
        RSP_PM_HANDOVER_SCHEDULE_PROCESSResources.Resources_Dummy_Class _loRsp = new RSP_PM_HANDOVER_SCHEDULE_PROCESSResources.Resources_Dummy_Class();
        private readonly ActivitySource _activitySource;

        public PMT02101Cls()
        {
            _activitySource = R_LibraryActivity.R_GetInstanceActivitySource();
        }

        public async Task R_BatchProcessAsync(R_BatchProcessPar poBatchProcessPar)
        {
            using Activity activity = _activitySource.StartActivity("_BatchProcess");
            R_Db loDb = new R_Db();
            string lcQuery;
            R_Exception loException = new R_Exception();
            int liFinishFlag;
            string PropertyId;
            string ScheduledHODate;
            string ScheduledHOTime;

            DbConnection loConn = null;
            DbCommand loCmd = null;
            try
            {
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();
                liFinishFlag = 1;
                List<PMT02100ScheduleDTO> loParam = R_NetCoreUtility.R_DeserializeObjectFromByte<List<PMT02100ScheduleDTO>>(poBatchProcessPar.BigObject);

                var loVar1 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.PMT02100_TAB_OPEN_SCHEDULE_PROCESS_PROPERTY_ID)).FirstOrDefault().Value;
                var loVar2 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.PMT02100_TAB_OPEN_SCHEDULE_PROCESS_SCHEDULED_HO_DATE)).FirstOrDefault().Value;
                var loVar3 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.PMT02100_TAB_OPEN_SCHEDULE_PROCESS_SCHEDULED_HO_TIME)).FirstOrDefault().Value;

                PropertyId = ((System.Text.Json.JsonElement)loVar1).GetString();
                ScheduledHODate = ((System.Text.Json.JsonElement)loVar2).GetString();
                ScheduledHOTime = ((System.Text.Json.JsonElement)loVar3).GetString();

                lcQuery = "CREATE TABLE #SELECTED_HANDOVER " +
                    "(NO INT NOT NULL, " +
                    "CCOMPANY_ID VARCHAR(8) NOT NULL, " +
                    "CPROPERTY_ID VARCHAR(20) NOT NULL, " +
                    "CDEPT_CODE VARCHAR(20) NOT NULL, " +
                    "CTRANS_CODE VARCHAR(10) NOT NULL, " +
                    "CREF_NO VARCHAR(30) NOT NULL)";


                await loDb.SqlExecNonQueryAsync(lcQuery, loConn, false);

                await loDb.R_BulkInsertAsync<PMT02100ScheduleDTO>((SqlConnection)loConn, "#SELECTED_HANDOVER", loParam);

                lcQuery = "EXEC RSP_PM_HANDOVER_SCHEDULE_PROCESS " +
                    "@CCOMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "@CSCHEDULED_HO_DATE, " +
                    "@CSCHEDULED_HO_TIME, " +
                    "@CUSER_ID, " +
                    "@KEY_GUID";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, PropertyId);
                loDb.R_AddCommandParameter(loCmd, "@CSCHEDULED_HO_DATE", DbType.String, 50, ScheduledHODate);
                loDb.R_AddCommandParameter(loCmd, "@CSCHEDULED_HO_TIME", DbType.String, 50, ScheduledHOTime);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@KEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

                loCmd.CommandText = lcQuery;
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
                lcQuery = "INSERT INTO GST_UPLOAD_ERROR_STATUS(CCOMPANY_ID,CUSER_ID,CKEY_GUID,ISEQ_NO,CERROR_MESSAGE) VALUES" +
                    string.Format("('{0}', '{1}', ", poBatchProcessPar.Key.COMPANY_ID, poBatchProcessPar.Key.USER_ID) +
                    string.Format("'{0}', -1, '{1}')", poBatchProcessPar.Key.KEY_GUID, loException.ErrorList[0].ErrDescp);
                await loDb.SqlExecNonQueryAsync(lcQuery);

                lcQuery = string.Format("EXEC RSP_WriteUploadProcessStatus '{0}', ", poBatchProcessPar.Key.COMPANY_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.USER_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.KEY_GUID) +
                   string.Format("100, '{0}', 9", loException.ErrorList[0].ErrDescp);

                await loDb.SqlExecNonQueryAsync(lcQuery);
            }
        }
    }
}
