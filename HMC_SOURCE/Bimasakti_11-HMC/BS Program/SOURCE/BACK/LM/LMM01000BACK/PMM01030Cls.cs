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
using PMM01000COMMON.Print;

namespace PMM01000BACK
{
    public class PMM01030Cls : R_BusinessObject<PMM01030DTO>
    {
        private LoggerPMM01030 _Logger;
        private LoggerPMM01030Print _Printlogger;
        private readonly ActivitySource _activitySource;
        public PMM01030Cls()
        {
            _Logger = LoggerPMM01030.R_GetInstanceLogger();
            _activitySource = PMM01030ActivitySourceBase.R_GetInstanceActivitySource();
        }
        public PMM01030Cls(LoggerPMM01030Print poParam)
        {
            _Printlogger = LoggerPMM01030Print.R_GetInstanceLogger();
            _activitySource = PMM01030PrintActivitySourceBase.R_GetInstanceActivitySource();
        }
        public List<PMM01030DTO> GetAllRatePGDateList(PMM01030DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllRatePGDateList");
            var loEx = new R_Exception();
            List<PMM01030DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_CHARGES_UTILITY_RATE_PG_DATE";
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
                _Logger.LogDebug("EXEC RSP_PM_GET_CHARGES_UTILITY_RATE_PG_DATE {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMM01030DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        protected override void R_Deleting(PMM01030DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            var loEx = new R_Exception();
            PMM01020DTO loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            var loDb = new R_Db();

            try
            {
                poEntity.CACTION = "DELETE";

                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_MAINTAIN_RATE_PG";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_DATE", DbType.String, 8, poEntity.CCHARGES_DATE);
                loDb.R_AddCommandParameter(loCmd, "@NBUY_STANDING_CHARGE", DbType.Decimal, 50, poEntity.NBUY_STANDING_CHARGE);
                loDb.R_AddCommandParameter(loCmd, "@NSTANDING_CHARGE", DbType.Decimal, 50, poEntity.NSTANDING_CHARGE);
                loDb.R_AddCommandParameter(loCmd, "@NBUY_USAGE_CHARGE_RATE", DbType.Decimal, 50, poEntity.NBUY_USAGE_CHARGE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NUSAGE_CHARGE_RATE", DbType.Decimal, 50, poEntity.NUSAGE_CHARGE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NMAINTENANCE_FEE", DbType.Decimal, 50, poEntity.NMAINTENANCE_FEE);
                loDb.R_AddCommandParameter(loCmd, "@CADMIN_FEE", DbType.String, 50, poEntity.CADMIN_FEE);
                loDb.R_AddCommandParameter(loCmd, "@NADMIN_FEE_PCT", DbType.Decimal, 50, poEntity.NADMIN_FEE_PCT);
                loDb.R_AddCommandParameter(loCmd, "@NADMIN_FEE_AMT", DbType.Decimal, 50, poEntity.NADMIN_FEE_AMT);
                loDb.R_AddCommandParameter(loCmd, "@LADMIN_FEE_TAX", DbType.Boolean, 50, poEntity.LADMIN_FEE_TAX);
                loDb.R_AddCommandParameter(loCmd, "@LADMIN_FEE_SC", DbType.Boolean, 50, poEntity.LADMIN_FEE_SC);
                loDb.R_AddCommandParameter(loCmd, "@LADMIN_FEE_USAGE", DbType.Boolean, 50, poEntity.LADMIN_FEE_USAGE);
                loDb.R_AddCommandParameter(loCmd, "@LADMIN_FEE_MAINTENANCE", DbType.Boolean, 50, poEntity.LADMIN_FEE_MAINTENANCE);

                loDb.R_AddCommandParameter(loCmd, "@LSPLIT_ADMIN", DbType.Boolean, 20, poEntity.LSPLIT_ADMIN);
                loDb.R_AddCommandParameter(loCmd, "@CADMIN_CHARGE_ID", DbType.String, 20, poEntity.CADMIN_CHARGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 3, poEntity.CCURRENCY_CODE);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Logs Debug
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                        .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_RATE_PG {@poParameter}", loDbParam);

                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
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
        protected override PMM01030DTO R_Display(PMM01030DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            var loEx = new R_Exception();
            PMM01030DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_CHARGES_UTILITY_RATE_PG";
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
                _Logger.LogDebug("EXEC RSP_PM_GET_CHARGES_UTILITY_RATE_PG {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMM01030DTO>(loDataTable).FirstOrDefault();

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
        protected override void R_Saving(PMM01030DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            var loEx = new R_Exception();
            PMM01020DTO loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            var loDb = new R_Db();

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

                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_MAINTAIN_RATE_PG";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poNewEntity.CCHARGES_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_ID", DbType.String, 50, poNewEntity.CCHARGES_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_DATE", DbType.String, 8, poNewEntity.CCHARGES_DATE);
                loDb.R_AddCommandParameter(loCmd, "@NBUY_STANDING_CHARGE", DbType.Decimal, 50, poNewEntity.NBUY_STANDING_CHARGE);
                loDb.R_AddCommandParameter(loCmd, "@NSTANDING_CHARGE", DbType.Decimal, 50, poNewEntity.NSTANDING_CHARGE);
                loDb.R_AddCommandParameter(loCmd, "@NBUY_USAGE_CHARGE_RATE", DbType.Decimal, 50, poNewEntity.NBUY_USAGE_CHARGE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NUSAGE_CHARGE_RATE", DbType.Decimal, 50, poNewEntity.NUSAGE_CHARGE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NMAINTENANCE_FEE", DbType.Decimal, 50, poNewEntity.NMAINTENANCE_FEE);
                loDb.R_AddCommandParameter(loCmd, "@CADMIN_FEE", DbType.String, 50, poNewEntity.CADMIN_FEE);
                loDb.R_AddCommandParameter(loCmd, "@NADMIN_FEE_PCT", DbType.Decimal, 50, poNewEntity.NADMIN_FEE_PCT);
                loDb.R_AddCommandParameter(loCmd, "@NADMIN_FEE_AMT", DbType.Decimal, 50, poNewEntity.NADMIN_FEE_AMT);
                loDb.R_AddCommandParameter(loCmd, "@LADMIN_FEE_TAX", DbType.Boolean, 50, poNewEntity.LADMIN_FEE_TAX);
                loDb.R_AddCommandParameter(loCmd, "@LADMIN_FEE_SC", DbType.Boolean, 50, poNewEntity.LADMIN_FEE_SC);
                loDb.R_AddCommandParameter(loCmd, "@LADMIN_FEE_USAGE", DbType.Boolean, 50, poNewEntity.LADMIN_FEE_USAGE);
                loDb.R_AddCommandParameter(loCmd, "@LADMIN_FEE_MAINTENANCE", DbType.Boolean, 50, poNewEntity.LADMIN_FEE_MAINTENANCE);

                loDb.R_AddCommandParameter(loCmd, "@LSPLIT_ADMIN", DbType.Boolean, 20, poNewEntity.LSPLIT_ADMIN);
                loDb.R_AddCommandParameter(loCmd, "@CADMIN_CHARGE_ID", DbType.String, 20, poNewEntity.CADMIN_CHARGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CCURRENCY_CODE);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Logs Debug
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                        .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_PM_MAINTAIN_RATE_PG {@poParameter}", loDbParam);

                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
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
        public PMM01030DTO GetBaseHeaderLogoCompany(PMM01030PrintParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetBaseHeaderLogoCompany");
            var loEx = new R_Exception();
            PMM01030DTO loResult = null;

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
                loResult = R_Utility.R_ConvertTo<PMM01030DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Printlogger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public PMM01030DTO GetReportRatePG(PMM01030DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetReportRatePG");
            var loEx = new R_Exception();
            PMM01030DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_GET_CHARGES_UTILITY_RATE_PG";
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
                _Printlogger.LogDebug("EXEC RSP_PM_GET_CHARGES_UTILITY_RATE_PG {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMM01030DTO>(loDataTable).FirstOrDefault();

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
