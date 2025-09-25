using PMM00100COMMON;
using PMM00100COMMON.DTO_s;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using PMM00100COMMON.DTO_s.Helper;

namespace PMM00100BACK
{
    public class PMM00103Cls : R_BusinessObject<SystemParamBillingDTO>
    {
        //var & constructor
        RSP_PM_MAINTAIN_BILLING_PARAMETERResources.Resources_Dummy_Class _resBillingParam = new();
        private LoggerPMM00100 _logger;
        private readonly ActivitySource _activitySource;
        public PMM00103Cls()
        {
            _logger = LoggerPMM00100.R_GetInstanceLogger();
            _activitySource = PMM00100Activity.R_GetInstanceActivitySource();
        }

        //methods
        protected override SystemParamBillingDTO R_Display(SystemParamBillingDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            SystemParamBillingDTO loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PM_GET_SYSTEM_PARAMETER_DETAIL";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poEntity.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);
                loDB.R_AddCommandParameter(loCmd, "@CTAB_ID", DbType.String, int.MaxValue, poEntity.CTAB_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<SystemParamBillingDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        protected override void R_Saving(SystemParamBillingDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            string lcQuery = null;
            R_Db loDb;
            DbCommand loCmd;
            DbConnection loConn = null;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);

                lcQuery = "RSP_PM_MAINTAIN_BILLING_PARAMETER";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String,8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String,20, poNewEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@COL_PAY_START_DATE", DbType.String,8, poNewEntity.COL_PAY_START_DATE);
                loDb.R_AddCommandParameter(loCmd, "@COL_PAY_SUBMIT_BY", DbType.String,3, poNewEntity.COL_PAY_SUBMIT_BY);
                loDb.R_AddCommandParameter(loCmd, "@COL_PAY_CURRENCY", DbType.String,3, poNewEntity.COL_PAY_CURRENCY);
                loDb.R_AddCommandParameter(loCmd, "@LOL_PAY_INCL_PENALTY", DbType.Boolean,1, poNewEntity.LOL_PAY_INCL_PENALTY);
                loDb.R_AddCommandParameter(loCmd, "@CBILLING_STATEMENT_DATE", DbType.String,2, poNewEntity.CBILLING_STATEMENT_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CBILLING_STATEMENT_TOP_CODE", DbType.String,8, poNewEntity.CBILLING_STATEMENT_TOP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String,10, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
                try
                {
                    ShowLogDebug(lcQuery, loCmd.Parameters);
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                    ShowLogError(loEx);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                    {
                        loConn.Close();
                    }

                    loConn.Dispose();
                }
            }

            loEx.ThrowExceptionIfErrors();
        }
        protected override void R_Deleting(SystemParamBillingDTO poEntity)
        {
            throw new NotImplementedException();
        }

        //helper
        private void ShowLogDebug(string query, DbParameterCollection parameters)
        {
            var paramValues = string.Join(", ", parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'"));
            _logger.LogDebug($"EXEC {query} {paramValues}");
        }
        private void ShowLogError(Exception ex)
        {
            _logger.LogError(ex);
        }
    }

}
