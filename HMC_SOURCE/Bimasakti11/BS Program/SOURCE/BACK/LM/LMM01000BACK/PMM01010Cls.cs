using PMM01000COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Transactions;
using System.Diagnostics;
using System.Data.SqlClient;
using PMM01000COMMON.Print;
using R_OpenTelemetry;

namespace PMM01000BACK
{
    public class PMM01010Cls : R_BusinessObject<PMM01010DTO>, R_IBatchProcess
    {
        private LoggerPMM01010 _PMM01010logger;
        private LoggerPMM01010Print _PMM01010Printlogger;
        private readonly ActivitySource _activitySource;

        public PMM01010Cls()
        {
            _PMM01010logger = LoggerPMM01010.R_GetInstanceLogger();
            var loActivity =  PMM01010ActivitySourceBase.R_GetInstanceActivitySource();
            if (loActivity is null)
            {
                _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
            }
            else
            {
                _activitySource = loActivity;
            }
        }
        public PMM01010Cls(LoggerPMM01010Print poParam)
        {
            _PMM01010Printlogger = LoggerPMM01010Print.R_GetInstanceLogger();
            _activitySource = PMM01010PrintActivitySourceBase.R_GetInstanceActivitySource();
            
        }
        public List<PMM01011DTO> GetAllRateECList(PMM01011DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllRateECList");
            var loEx = new R_Exception();
            List<PMM01011DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_RATE_EC_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_DATE", DbType.String, 50, poEntity.CCHARGES_DATE);

                //Logs Debug
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                 .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _PMM01010logger.LogDebug("EXEC RSP_PM_GET_RATE_EC_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMM01011DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _PMM01010logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<PMM01010DTO> GetAllRateECDateList(PMM01010DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllRateECDateList");
            var loEx = new R_Exception();
            List<PMM01010DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_CHARGES_UTILITY_RATE_EC_DATE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Logs Debug
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                 .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _PMM01010logger.LogDebug("EXEC RSP_PM_GET_CHARGES_UTILITY_RATE_EC_DATE {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMM01010DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _PMM01010logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            using Activity activity = _activitySource.StartActivity("R_BatchProcess");
            R_Exception loException = new R_Exception();
            var loDb = new R_Db();

            try
            {
                _PMM01010logger.LogInfo("Test Connection");
                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("01", "Database Connection Failed");
                    goto EndBlock;
                }

                _PMM01010logger.LogInfo("Start Batch");
                var loTask = Task.Run(() =>
                {
                    _BatchProcess(poBatchProcessPar);
                });
                _PMM01010logger.LogInfo("End Batch");

                //while (!loTask.IsCompleted)
                //{
                //    Thread.Sleep(100);
                //}

                //if (loTask.IsFaulted)
                //{
                //    loException.Add(loTask.Exception.InnerException != null ?
                //        loTask.Exception.InnerException :
                //        loTask.Exception);

                //    goto EndBlock;
                //}
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
        private async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            using Activity activity = _activitySource.StartActivity("_BatchProcess");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbCommand loCmd = null;
            DbConnection loConn = null;
            var lcQuery = "";

            try
            {
                // must delay for wait this method is completed in syncronous
                await Task.Delay(100);

                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<PMM01010DTO>(poBatchProcessPar.BigObject);
                var loListTempTable = R_Utility.R_ConvertCollectionToCollection<PMM01011DTO, PMM01011SaveBatchDTO>(loTempObject.CRATE_EC_LIST);

                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                _PMM01010logger.LogInfo("Start Bulk Insert");
                lcQuery += "CREATE TABLE #CRATE_EC_LIST (	NO INT " +
                        ", CCOMPANY_ID VARCHAR(20) " +
                        ", CPROPERTY_ID NVARCHAR(20) " +
                        ", CCHARGES_TYPE NVARCHAR(2) " +
                        ", CCHARGES_ID NVARCHAR(20) " +
                        ", IUP_TO_USAGE INT " +
                        ", CUSAGE_DESC NVARCHAR(255) " +
                        ", NBUY_LWBP_CHARGE NUMERIC(18,2) " +
                        ", NBUY_WBP_CHARGE NUMERIC(18,2) " +
                        ", NLWBP_CHARGE NUMERIC(18,2) " +
                        ", NWBP_CHARGE  NUMERIC(18,2) ) ";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);

                loDb.R_BulkInsert<PMM01011SaveBatchDTO>((SqlConnection)loConn, "#CRATE_EC_LIST", loListTempTable);
                _PMM01010logger.LogInfo("End Bulk Insert");

                lcQuery = "Select * From #CRATE_EC_LIST ";

                var loTesResult = loDb.SqlExecObjectQuery<PMM01011SaveBatchDTO>(lcQuery, loConn, false);

                lcQuery = "EXECUTE RSP_PM_MAINTAIN_RATE_EC " +
                     "@CCOMPANY_ID = @CCOMPANY_ID  " +
                     ",@CPROPERTY_ID = @CPROPERTY_ID " +

                     ",@CCHARGES_TYPE = @CCHARGES_TYPE " +
                     ",@CCHARGES_ID = @CCHARGES_ID " +
                     ",@CCHARGES_DATE = @CCHARGES_DATE " +
                     ",@CUSAGE_RATE_MODE = @CUSAGE_RATE_MODE " +
                     ",@CRATE_TYPE = @CRATE_TYPE " +
                     ",@NSTANDING_CHARGE = @NSTANDING_CHARGE " +
                     ",@NBUY_STANDING_CHARGE = @NBUY_STANDING_CHARGE " +
                     ",@NLWBP_CHARGE = @NLWBP_CHARGE " +
                     ",@NBUY_LWBP_CHARGE = @NBUY_LWBP_CHARGE " +
                     ",@NWBP_CHARGE = @NWBP_CHARGE " +
                     ",@NBUY_WBP_CHARGE = @NBUY_WBP_CHARGE " +
                     ",@NTRANSFORMATOR_FEE = @NTRANSFORMATOR_FEE " +
                     ",@NBUY_TRANSFORMATOR_FEE = @NBUY_TRANSFORMATOR_FEE " +
                     ",@LUSAGE_MIN_CHARGE = @LUSAGE_MIN_CHARGE " +
                     ",@NUSAGE_MIN_CHARGE = @NUSAGE_MIN_CHARGE " +
                     ",@NKWH_USED_MAX = @NKWH_USED_MAX " +
                     ",@NKWH_USED_RATE = @NKWH_USED_RATE " +
                     ",@NBUY_KWH_USED_RATE = @NBUY_KWH_USED_RATE " +
                     ",@NRETRIBUTION_PCT = @NRETRIBUTION_PCT " +
                     ",@LRETRIBUTION_EXCLUDED_VAT = @LRETRIBUTION_EXCLUDED_VAT " +

                     ",@CADMIN_FEE = @CADMIN_FEE " +
                     ",@NADMIN_FEE_PCT = @NADMIN_FEE_PCT " +
                     ",@NADMIN_FEE_AMT = @NADMIN_FEE_AMT " +
                     ",@LADMIN_FEE_TAX = @LADMIN_FEE_TAX " +
                     ",@NOTHER_DISINCENTIVE_FACTOR = @NOTHER_DISINCENTIVE_FACTOR " +
                     ",@NKVA_RANGE = @NKVA_RANGE " +
                     ",@NBUY_KVA_RANGE = @NBUY_KVA_RANGE " +

                     ",@LSPLIT_ADMIN = @LSPLIT_ADMIN " +
                     ",@CADMIN_CHARGE_ID = @CADMIN_CHARGE_ID " +
                     ",@CACTION = @CACTION " +
                     ",@CUSER_ID = @CUSER_ID " +
                     ",@CKEY_GUID = @CKEY_GUID " +
                     ",@CCURRENCY_CODE = @CCURRENCY_CODE ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 15, loTempObject.CPROPERTY_ID);

                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 15, loTempObject.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 20, loTempObject.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_DATE", DbType.String, 8, loTempObject.CCHARGES_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CUSAGE_RATE_MODE", DbType.String, 50, loTempObject.CUSAGE_RATE_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CRATE_TYPE", DbType.String, 20, loTempObject.CRATE_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@NSTANDING_CHARGE", DbType.Decimal, 50, loTempObject.NSTANDING_CHARGE);
                loDb.R_AddCommandParameter(loCmd, "@NBUY_STANDING_CHARGE", DbType.Decimal, 50, loTempObject.NBUY_STANDING_CHARGE);
                loDb.R_AddCommandParameter(loCmd, "@NLWBP_CHARGE", DbType.Decimal, 50, loTempObject.NLWBP_CHARGE);
                loDb.R_AddCommandParameter(loCmd, "@NBUY_LWBP_CHARGE", DbType.Decimal, 50, loTempObject.NBUY_LWBP_CHARGE);
                loDb.R_AddCommandParameter(loCmd, "@NWBP_CHARGE", DbType.Decimal, 50, loTempObject.NWBP_CHARGE);
                loDb.R_AddCommandParameter(loCmd, "@NBUY_WBP_CHARGE", DbType.Decimal, 50, loTempObject.NBUY_WBP_CHARGE);
                loDb.R_AddCommandParameter(loCmd, "@NTRANSFORMATOR_FEE", DbType.Decimal, 50, loTempObject.NTRANSFORMATOR_FEE);
                loDb.R_AddCommandParameter(loCmd, "@NBUY_TRANSFORMATOR_FEE", DbType.Decimal, 50, loTempObject.NBUY_TRANSFORMATOR_FEE);
                loDb.R_AddCommandParameter(loCmd, "@LUSAGE_MIN_CHARGE", DbType.Boolean, 50, loTempObject.LUSAGE_MIN_CHARGE);
                loDb.R_AddCommandParameter(loCmd, "@NUSAGE_MIN_CHARGE", DbType.Decimal, 50, loTempObject.NUSAGE_MIN_CHARGE);
                loDb.R_AddCommandParameter(loCmd, "@NKWH_USED_MAX", DbType.Decimal, 50, loTempObject.NKWH_USED_MAX);
                loDb.R_AddCommandParameter(loCmd, "@NKWH_USED_RATE", DbType.Decimal, 50, loTempObject.NKWH_USED_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NBUY_KWH_USED_RATE", DbType.Decimal, 50, loTempObject.NBUY_KWH_USED_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NRETRIBUTION_PCT", DbType.Decimal, 50, loTempObject.NRETRIBUTION_PCT);
                loDb.R_AddCommandParameter(loCmd, "@LRETRIBUTION_EXCLUDED_VAT", DbType.Boolean, 50, loTempObject.LRETRIBUTION_EXCLUDED_VAT);

                loDb.R_AddCommandParameter(loCmd, "@CADMIN_FEE", DbType.String, 20, loTempObject.CADMIN_FEE);
                loDb.R_AddCommandParameter(loCmd, "@NADMIN_FEE_PCT", DbType.Decimal, 50, loTempObject.NADMIN_FEE_PCT);
                loDb.R_AddCommandParameter(loCmd, "@NADMIN_FEE_AMT", DbType.Decimal, 50, loTempObject.NADMIN_FEE_AMT);
                loDb.R_AddCommandParameter(loCmd, "@LADMIN_FEE_TAX", DbType.Boolean, 50, loTempObject.LADMIN_FEE_TAX);
                loDb.R_AddCommandParameter(loCmd, "@NOTHER_DISINCENTIVE_FACTOR", DbType.Int32, 50, loTempObject.NOTHER_DISINCENTIVE_FACTOR);
                loDb.R_AddCommandParameter(loCmd, "@NKVA_RANGE", DbType.Decimal, 50, loTempObject.NKVA_RANGE);
                loDb.R_AddCommandParameter(loCmd, "@NBUY_KVA_RANGE", DbType.Decimal, 50, loTempObject.NBUY_KVA_RANGE);

                loDb.R_AddCommandParameter(loCmd, "@LSPLIT_ADMIN", DbType.Boolean, 20, loTempObject.LSPLIT_ADMIN);
                loDb.R_AddCommandParameter(loCmd, "@CADMIN_CHARGE_ID", DbType.String, 20, loTempObject.CADMIN_CHARGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 20, loTempObject.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 100, poBatchProcessPar.Key.KEY_GUID);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 3, loTempObject.CCURRENCY_CODE);

                //Logs Debug
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                 .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _PMM01010logger.LogDebug("EXEC RSP_PM_MAINTAIN_RATE_EC {@poParameter}", loDbParam);

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
                    if (!(loConn.State == ConnectionState.Closed))
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
                loDb.SqlExecNonQuery(lcQuery);

                lcQuery = string.Format("EXEC RSP_WriteUploadProcessStatus '{0}', ", poBatchProcessPar.Key.COMPANY_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.USER_ID) +
                   string.Format("'{0}', ", poBatchProcessPar.Key.KEY_GUID) +
                   string.Format("100, '{0}', 9", loException.ErrorList[0].ErrDescp);

                loDb.SqlExecNonQuery(lcQuery);
            }
        }
        protected override void R_Deleting(PMM01010DTO poEntity)
        {
            throw new NotImplementedException();
        }
        protected override PMM01010DTO R_Display(PMM01010DTO poEntity)  
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            var loEx = new R_Exception();
            PMM01010DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_CHARGES_UTILITY_RATE_EC";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_DATE", DbType.String, 50, poEntity.CCHARGES_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Logs Debug
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                 .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _PMM01010logger.LogDebug("EXEC RSP_PM_GET_CHARGES_UTILITY_RATE_EC {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMM01010DTO>(loDataTable).FirstOrDefault();

                loResult.CRATE_TYPE = string.IsNullOrWhiteSpace(loResult.CRATE_TYPE) ? "SR" : loResult.CRATE_TYPE;
                loResult.CUSAGE_RATE_MODE = string.IsNullOrWhiteSpace(loResult.CUSAGE_RATE_MODE) ? "HM" : loResult.CUSAGE_RATE_MODE;
                loResult.CADMIN_FEE = string.IsNullOrWhiteSpace(loResult.CADMIN_FEE) ? "00" : loResult.CADMIN_FEE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _PMM01010logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        protected override void R_Saving(PMM01010DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            var loCmd = loDb.GetCommand();

            try
            {
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "ADD";
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }

                using (var TransScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    loConn = loDb.GetConnection();

                    lcQuery = "DECLARE @CRATE_EC_LIST AS RDT_COMMON_OBJECT ";

                    if (poNewEntity.CRATE_EC_LIST != null && poNewEntity.CRATE_EC_LIST.Count > 0)
                    {
                        lcQuery += "INSERT INTO @CRATE_EC_LIST " +
                            "(COBJECT_ID, COBJECT_DESC, CATTRIBUTE01, CATTRIBUTE02, CATTRIBUTE03, CATTRIBUTE04, CATTRIBUTE05, CATTRIBUTE06, CATTRIBUTE07, CATTRIBUTE08) " +
                            "VALUES ";
                        foreach (var loRate in poNewEntity.CRATE_EC_LIST)
                        {
                            lcQuery += $"('{loRate.CCOMPANY_ID}', '{loRate.CPROPERTY_ID}', '{loRate.CCHARGES_TYPE}', '{loRate.CCHARGES_ID}', '{loRate.IUP_TO_USAGE}', " +
                                $"'{loRate.CUSAGE_DESC}', '{loRate.NBUY_LWBP_CHARGE}', '{loRate.NBUY_WBP_CHARGE}', '{loRate.NLWBP_CHARGE}', '{loRate.NWBP_CHARGE}'),";
                        }
                        lcQuery = lcQuery.Substring(0, lcQuery.Length - 1) + " ";

                    }

                    lcQuery += "EXECUTE RSP_PM_MAINTAIN_RATE_EC " +
                        $"@CCOMPANY_ID = '{poNewEntity.CCOMPANY_ID}' " +
                        $",@CPROPERTY_ID = '{poNewEntity.CPROPERTY_ID}' " +
                        $",@CCHARGES_TYPE = '{poNewEntity.CCHARGES_TYPE}' " +
                        $",@CCHARGES_ID = '{poNewEntity.CCHARGES_ID}' " +
                        $",@CUSAGE_RATE_MODE = '{poNewEntity.CUSAGE_RATE_MODE}' " +
                        $",@CRATE_TYPE = '{poNewEntity.CRATE_TYPE}' " +
                        $",@NSTANDING_CHARGE = {poNewEntity.NSTANDING_CHARGE} " +
                        $",@NBUY_STANDING_CHARGE = {poNewEntity.NBUY_STANDING_CHARGE}" +
                        $",@NLWBP_CHARGE = {poNewEntity.NLWBP_CHARGE}" +
                        $",@NBUY_LWBP_CHARGE = {poNewEntity.NBUY_LWBP_CHARGE} " +
                        $",@NWBP_CHARGE = {poNewEntity.NWBP_CHARGE} " +
                        $",@NBUY_WBP_CHARGE = {poNewEntity.NBUY_WBP_CHARGE} " +
                        $",@NTRANSFORMATOR_FEE = {poNewEntity.NTRANSFORMATOR_FEE} " +
                        $",@NBUY_TRANSFORMATOR_FEE = {poNewEntity.NBUY_TRANSFORMATOR_FEE} " +
                        $",@LUSAGE_MIN_CHARGE = {poNewEntity.LUSAGE_MIN_CHARGE} " +
                        $",@NUSAGE_MIN_CHARGE = {poNewEntity.NUSAGE_MIN_CHARGE} " +
                        $",@NKWH_USED_MAX = {poNewEntity.NKWH_USED_MAX} " +
                        $",@NKWH_USED_RATE = {poNewEntity.NKWH_USED_RATE} " +
                        $",@NBUY_KWH_USED_RATE = {poNewEntity.NBUY_KWH_USED_RATE} " +
                        $",@NRETRIBUTION_PCT = {poNewEntity.NRETRIBUTION_PCT} " +
                        $",@LRETRIBUTION_EXCLUDED_VAT = {poNewEntity.LRETRIBUTION_EXCLUDED_VAT} " +
                        $",@CADMIN_FEE = '{poNewEntity.CADMIN_FEE}' " +
                        $",@NADMIN_FEE_PCT = {poNewEntity.NADMIN_FEE_PCT} " +
                        $",@NADMIN_FEE_AMT = {poNewEntity.NADMIN_FEE_AMT} " +
                        $",@LADMIN_FEE_TAX = {poNewEntity.LADMIN_FEE_TAX} " +
                        $",@NOTHER_DISINCENTIVE_FACTOR = {poNewEntity.NOTHER_DISINCENTIVE_FACTOR} " +
                        $",@NKVA_RANGE = {poNewEntity.NKVA_RANGE} " +
                        $",@NBUY_KVA_RANGE = {poNewEntity.NBUY_KVA_RANGE}" +
                        $",@CACTION = '{poNewEntity.CACTION}' " +
                        $",@CUSER_ID = '{poNewEntity.CUSER_ID}' " +
                        ",@CRATE_EC_LIST = @CRATE_EC_LIST ";

                        R_ExternalException.R_SP_Init_Exception(loConn);

                        try
                        {
                            loDb.SqlExecQuery(lcQuery, loConn, false);
                        }
                        catch (Exception ex)
                        {
                            loEx.Add(ex);
                        }

                        loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

                    TransScope.Complete();
                };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
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
                if (loDb != null)
                {
                    loDb = null;
                }
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Report SP
        public PMM01010DTO GetBaseHeaderLogoCompany(PMM01010PrintParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetBaseHeaderLogoCompany");
            var loEx = new R_Exception();
            PMM01010DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();


                var lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 15, poEntity.CCOMPANY_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _PMM01010Printlogger.LogDebug("SELECT dbo.RFN_GET_COMPANY_LOGO({@CCOMPANY_ID}) as CLOGO", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMM01010DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _PMM01010Printlogger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public PMM01010DTO GetHDReportRateEC(PMM01010DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetHDReportRateEC");
            var loEx = new R_Exception();
            PMM01010DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_CHARGES_UTILITY_RATE_EC";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_DATE", DbType.String, 50, poEntity.CCHARGES_DATE);

                //Logs Debug
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                 .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _PMM01010Printlogger.LogDebug("EXEC RSP_PM_GET_CHARGES_UTILITY_RATE_EC {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMM01010DTO>(loDataTable).FirstOrDefault();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _PMM01010Printlogger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<PMM01011DTO> GetDetailReportRateEC(PMM01011DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetDetailReportRateEC");
            var loEx = new R_Exception();
            List<PMM01011DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_RATE_EC_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_DATE", DbType.String, 50, poEntity.CCHARGES_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                //Logs Debug
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                 .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _PMM01010Printlogger.LogDebug("EXEC RSP_PM_GET_RATE_EC_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMM01011DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _PMM01010Printlogger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion
    }
}
