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

namespace PMM01000BACK
{
    public class PMM01050Cls : R_BusinessObject<PMM01050DTO>, R_IBatchProcess
    {
        private LoggerPMM01050 _Logger;
        private LoggerPMM01050Print _Printlogger;
        private readonly ActivitySource _activitySource;

        public PMM01050Cls()
        {
            _Logger = LoggerPMM01050.R_GetInstanceLogger();
            var loActivity = PMM01050ActivitySourceBase.R_GetInstanceActivitySource();
            if (loActivity is null)
            {
                _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
            }
            else
            {
                _activitySource = loActivity;
            }
        }
        public PMM01050Cls(LoggerPMM01050Print poParam)
        {
            _Printlogger = LoggerPMM01050Print.R_GetInstanceLogger();
            _activitySource = PMM01050PrintActivitySourceBase.R_GetInstanceActivitySource();
        }
        public List<PMM01050DTO> GetAllRateOTDateList(PMM01050DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllRateOTDateList");
            var loEx = new R_Exception();
            List<PMM01050DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_CHARGES_UTILITY_RATE_OT_DATE";
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
                _Logger.LogDebug("EXEC RSP_PM_GET_CHARGES_UTILITY_RATE_OT_DATE {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMM01050DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<PMM01051DTO> GetAllRateOTList(PMM01051DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllRateOTList");
            var loEx = new R_Exception();
            List<PMM01051DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_CHARGES_UTILITY_RATE_OT_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CDAY_TYPE", DbType.String, 50, poEntity.CDAY_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_DATE", DbType.String, 50, poEntity.CCHARGES_DATE);

                //Logs Debug
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                 .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_CHARGES_UTILITY_RATE_OT_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMM01051DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            var loDb = new R_Db();

            try
            {
                _Logger.LogInfo("Test Connection");
                if (loDb.R_TestConnection() == false)
                {
                    loException.Add("01", "Database Connection Failed");
                    goto EndBlock;
                }

                _Logger.LogInfo("Start Batch");
                var loTask = Task.Run(() =>
                {
                    _BatchProcess(poBatchProcessPar);
                });
                _Logger.LogInfo("End Batch");

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
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbCommand loCmd = null;
            DbConnection loConn = null;
            var lcQuery = "";

            try
            {
                // must delay for wait this method is completed in syncronous
                await Task.Delay(100);

                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<PMM01050DTO>(poBatchProcessPar.BigObject);
                var loListTempTable = R_Utility.R_ConvertCollectionToCollection<PMM01051DTO, PMM01051SaveBatchDTO>(loTempObject.CRATE_OT_LIST);

                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                _Logger.LogInfo("Start Bulk Insert");
                lcQuery += "CREATE TABLE #CRATE_OT_LIST (	NO INT " +
                        ", CCOMPANY_ID VARCHAR(20) " +
                        ", CPROPERTY_ID NVARCHAR(20) " +
                        ", CCHARGES_TYPE NVARCHAR(2) " +
                        ", CCHARGES_ID NVARCHAR(20) " +
                        ", CDAY_TYPE NVARCHAR(2) " +
                        ", ILEVEL INT " +
                        ", CLEVEL_DESC NVARCHAR(255) " +
                        ", IHOURS_FROM INT " +
                        ", IHOURS_TO INT " +
                        ", NRATE_HOUR  NUMERIC(18,2) ) ";

                loDb.SqlExecNonQuery(lcQuery, loConn, false);
                _Logger.LogInfo("End Bulk Insert");

                loDb.R_BulkInsert<PMM01051SaveBatchDTO>((SqlConnection)loConn, "#CRATE_OT_LIST", loListTempTable);

                lcQuery = "RSP_PM_MAINTAIN_RATE_OT";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 15, loTempObject.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 20, loTempObject.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 20, loTempObject.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_DATE", DbType.String, 8, loTempObject.CCHARGES_DATE);

                loDb.R_AddCommandParameter(loCmd, "@CADMIN_FEE", DbType.String, 20, loTempObject.CADMIN_FEE);
                loDb.R_AddCommandParameter(loCmd, "@NADMIN_FEE_PCT", DbType.Decimal, 50, loTempObject.NADMIN_FEE_PCT);
                loDb.R_AddCommandParameter(loCmd, "@NADMIN_FEE_AMT", DbType.Decimal, 50, loTempObject.NADMIN_FEE_AMT);
                loDb.R_AddCommandParameter(loCmd, "@LADMIN_FEE_TAX", DbType.Boolean, 50, loTempObject.LADMIN_FEE_TAX);
                loDb.R_AddCommandParameter(loCmd, "@NUNIT_AREA_VALID_FROM", DbType.Decimal, 50, loTempObject.NUNIT_AREA_VALID_FROM);
                loDb.R_AddCommandParameter(loCmd, "@NUNIT_AREA_VALID_TO", DbType.Decimal, 50, loTempObject.NUNIT_AREA_VALID_TO);
                loDb.R_AddCommandParameter(loCmd, "@LCUT_OFF_WEEKDAY", DbType.Boolean, 50, loTempObject.LCUT_OFF_WEEKDAY);
                loDb.R_AddCommandParameter(loCmd, "@LHOLIDAY", DbType.Boolean, 50, loTempObject.LHOLIDAY);
                loDb.R_AddCommandParameter(loCmd, "@LSATURDAY", DbType.Boolean, 50, loTempObject.LSATURDAY);
                loDb.R_AddCommandParameter(loCmd, "@LSUNDAY", DbType.Boolean, 50, loTempObject.LSUNDAY);

                loDb.R_AddCommandParameter(loCmd, "@LSPLIT_ADMIN", DbType.Boolean, 20, loTempObject.LSPLIT_ADMIN);
                loDb.R_AddCommandParameter(loCmd, "@CADMIN_CHARGE_ID", DbType.String, 20, loTempObject.CADMIN_CHARGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, loTempObject.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poBatchProcessPar.Key.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 100, poBatchProcessPar.Key.KEY_GUID);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 3, loTempObject.CCURRENCY_CODE);

                //Logs Debug
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                 .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_RATE_OT {@poParameter}", loDbParam);

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
        protected override void R_Deleting(PMM01050DTO poEntity)
        {
            throw new NotImplementedException();
        }
        protected override PMM01050DTO R_Display(PMM01050DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            var loEx = new R_Exception();
            PMM01050DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_CHARGES_UTILITY_RATE_OT";
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
                _Logger.LogDebug("EXEC RSP_PM_GET_CHARGES_UTILITY_RATE_OT {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMM01050DTO>(loDataTable).FirstOrDefault();

                loResult.CADMIN_FEE = string.IsNullOrWhiteSpace(loResult.CADMIN_FEE) ? "00" : loResult.CADMIN_FEE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        protected override void R_Saving(PMM01050DTO poNewEntity, eCRUDMode poCRUDMode)
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

                    lcQuery = "DECLARE @CRATE_OT_LIST AS RDT_COMMON_OBJECT ";

                    if (poNewEntity.CRATE_OT_LIST != null && poNewEntity.CRATE_OT_LIST.Count > 0)
                    {
                        lcQuery += "INSERT INTO @CRATE_OT_LIST " +
                            "(COBJECT_ID, COBJECT_DESC, CATTRIBUTE01, CATTRIBUTE02, CATTRIBUTE03, CATTRIBUTE04, CATTRIBUTE05, CATTRIBUTE06, CATTRIBUTE07, CATTRIBUTE08) " +
                            "VALUES ";
                        foreach (var loRate in poNewEntity.CRATE_OT_LIST)
                        {
                            lcQuery += $"('{loRate.CCOMPANY_ID}', '{loRate.CPROPERTY_ID}', '{loRate.CCHARGES_TYPE}', '{loRate.CCHARGES_ID}', '{loRate.CDAY_TYPE}', " +
                                $"'{loRate.ILEVEL}', '{loRate.CLEVEL_DESC}', '{loRate.IHOURS_FROM}', '{loRate.IHOURS_TO}', '{loRate.NRATE_HOUR}'),";
                        }
                        lcQuery = lcQuery.Substring(0, lcQuery.Length - 1) + " ";

                    }

                    lcQuery += "EXECUTE RSP_PM_MAINTAIN_RATE_OT " +
                        $"@CCOMPANY_ID = '{poNewEntity.CCOMPANY_ID}' " +
                        $",@CPROPERTY_ID = '{poNewEntity.CPROPERTY_ID}' " +
                        $",@CCHARGES_TYPE = '{poNewEntity.CCHARGES_TYPE}' " +
                        $",@CCHARGES_ID = '{poNewEntity.CCHARGES_ID}' " +
                        $",@CADMIN_FEE = '{poNewEntity.CADMIN_FEE}' " +
                        $",@NADMIN_FEE_PCT = {poNewEntity.NADMIN_FEE_PCT} " +
                        $",@NADMIN_FEE_AMT = {poNewEntity.NADMIN_FEE_AMT} " +
                        $",@NUNIT_AREA_VALID_FROM = {poNewEntity.NUNIT_AREA_VALID_FROM} " +
                        $",@NUNIT_AREA_VALID_TO = {poNewEntity.NUNIT_AREA_VALID_TO} " +
                        $",@LCUT_OFF_WEEKDAY = {poNewEntity.LCUT_OFF_WEEKDAY} " +
                        $",@LHOLIDAY = {poNewEntity.LHOLIDAY} " +
                        $",@LSATURDAY = {poNewEntity.LSATURDAY} " +
                        $",@LSUNDAY = {poNewEntity.LSUNDAY} " +
                        $",@CACTION = '{poNewEntity.CACTION}' " +
                        $",@CUSER_ID = '{poNewEntity.CUSER_ID}' " +
                        ",@CRATE_OT_LIST = @CRATE_OT_LIST ";

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
        public PMM01050DTO GetBaseHeaderLogoCompany(PMM01050PrintParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetBaseHeaderLogoCompany");
            var loEx = new R_Exception();
            PMM01050DTO loResult = null;

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
                _Printlogger.LogDebug(string.Format("SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO", loDbParam));

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMM01050DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Printlogger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public PMM01050DTO GetHDReportRateOT(PMM01050DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetHDReportRateOT");
            var loEx = new R_Exception();
            PMM01050DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_CHARGES_UTILITY_RATE_OT";
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
                _Printlogger.LogDebug("EXEC RSP_PM_GET_CHARGES_UTILITY_RATE_OT {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMM01050DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Printlogger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<PMM01051DTO> GetDetailReportRateOT(PMM01051DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetDetailReportRateOT");
            var loEx = new R_Exception();
            List<PMM01051DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_CHARGES_UTILITY_RATE_OT_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CDAY_TYPE", DbType.String, 50, poEntity.CDAY_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_DATE", DbType.String, 50, poEntity.CCHARGES_DATE);

                //Logs Debug
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                 .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Printlogger.LogDebug("EXEC RSP_PM_GET_CHARGES_UTILITY_RATE_OT_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMM01051DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Printlogger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion
    }
}
