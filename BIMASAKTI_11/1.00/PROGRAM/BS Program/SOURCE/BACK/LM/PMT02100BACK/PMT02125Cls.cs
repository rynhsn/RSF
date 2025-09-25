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
using PMT02100COMMON.DTOs.PMT02120;

namespace PMT02100BACK
{
    public class PMT02125Cls : R_IBatchProcessAsync
    {
        //RSP_PM_UPLOAD_TENANTResources.Resources_Dummy_Class _loRsp = new RSP_PM_UPLOAD_TENANTResources.Resources_Dummy_Class();
        private readonly ActivitySource _activitySource;

        public PMT02125Cls()
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
            string DeptCode;
            string TransCode;
            string RefNo;
            string HOActualDate;

            DbConnection loConn = null;
            DbCommand loCmd = null;
            try
            {
                loConn = await loDb.GetConnectionAsync();
                loCmd = loDb.GetCommand();
                liFinishFlag = 1;
                PMT02125BigObjectDTO loParam = R_NetCoreUtility.R_DeserializeObjectFromByte<PMT02125BigObjectDTO>(poBatchProcessPar.BigObject);

                var loVar1 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.PMT02125_HANDOVER_PROCESS_PROPERTY_ID)).FirstOrDefault().Value;
                var loVar2 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.PMT02125_HANDOVER_PROCESS_CDEPT_CODE)).FirstOrDefault().Value;
                var loVar3 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.PMT02125_HANDOVER_PROCESS_CTRANS_CODE)).FirstOrDefault().Value;
                var loVar4 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.PMT02125_HANDOVER_PROCESS_CREF_NO)).FirstOrDefault().Value;
                var loVar5 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.PMT02125_HANDOVER_PROCESS_CHO_ACTUAL_DATE)).FirstOrDefault().Value;

                PropertyId = ((System.Text.Json.JsonElement)loVar1).GetString();
                DeptCode = ((System.Text.Json.JsonElement)loVar2).GetString();
                TransCode = ((System.Text.Json.JsonElement)loVar3).GetString();
                RefNo = ((System.Text.Json.JsonElement)loVar4).GetString();
                HOActualDate = ((System.Text.Json.JsonElement)loVar5).GetString();

                lcQuery = "CREATE TABLE #HANDOVER_UNIT " +
                    "(NO INT NOT NULL, " +
                    "CUNIT_ID VARCHAR(20) NOT NULL, " +
                    "CFLOOR_ID VARCHAR(20) NOT NULL, " +
                    "CBUILDING_ID VARCHAR(20) NOT NULL, " +
                    "NACTUAL_AREA_SIZE NUMERIC(8,2) NOT NULL)";

                await loDb.SqlExecNonQueryAsync(lcQuery, loConn, false);

                await loDb.R_BulkInsertAsync<PMT02120HandoverProcessUnitDTO>((SqlConnection)loConn, "#HANDOVER_UNIT", loParam.UnitList);

                lcQuery = "CREATE TABLE #HANDOVER_UTILITY " +
                          "(NO INT NOT NULL, " +
                          "CUNIT_ID VARCHAR(20) NOT NULL, " +
                          "CFLOOR_ID VARCHAR(20) NOT NULL, " +
                          "CBUILDING_ID VARCHAR(20) NOT NULL, " +
                          "CCHARGES_TYPE VARCHAR(20) NOT NULL, " +
                          "CCHARGES_ID VARCHAR(20) NOT NULL, " +
                          "CSEQ_NO VARCHAR(3) NOT NULL, " +
                          "CSTART_INV_PRD VARCHAR(6) NOT NULL, " +
                          "NMETER_START NUMERIC(16,2) NOT NULL, " +
                          "NBLOCK1_START NUMERIC(16,2) NOT NULL, " +
                          "NBLOCK2_START NUMERIC(16,2) NOT NULL)";


                await loDb.SqlExecNonQueryAsync(lcQuery, loConn, false);

                await loDb.R_BulkInsertAsync<PMT02120HandoverProcessUtilityDTO>((SqlConnection)loConn, "#HANDOVER_UTILITY", loParam.UtilityList);

                lcQuery = "EXEC RSP_PM_HANDOVER_PROCESS " +
                    "@CCOMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "@CDEPT_CODE, " +
                    "@CTRANS_CODE, " +
                    "@CREF_NO, " +
                    "@CHO_ACTUAL_DATE, " +
                    "@CUSER_ID, " +
                    "@KEY_GUID";

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, PropertyId);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, DeptCode);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, TransCode);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, RefNo);
                loDb.R_AddCommandParameter(loCmd, "@CHO_ACTUAL_DATE", DbType.String, 50, HOActualDate);
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
