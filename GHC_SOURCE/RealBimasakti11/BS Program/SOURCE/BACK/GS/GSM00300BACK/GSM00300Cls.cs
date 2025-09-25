using GSM00300COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Reflection;
using System.Data.Common;
using System.Diagnostics;
using GSM00300COMMON.DTO_s;
using GSM00300COMMON.DTO_s.Helper;

namespace GSM00300BACK
{
    public class GSM00300Cls : R_BusinessObject<CompanyParamRecordDTO>
    {
        private RSP_GS_MAINTAIN_COMP_PARAMResources.Resources_Dummy_Class _rsp = new();

        private readonly ActivitySource _activitySource;

        private LoggerGSM00300 _logger;

        public GSM00300Cls()
        {
            _logger = LoggerGSM00300.R_GetInstanceLogger();
            _activitySource = GSM00300Activity.R_GetInstanceActivitySource();
        }

        protected override void R_Deleting(CompanyParamRecordDTO poEntity)
        {
            throw new NotImplementedException();
        }

        protected override CompanyParamRecordDTO R_Display(CompanyParamRecordDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            CompanyParamRecordDTO loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GS_GET_COMPANY_PARAM";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<CompanyParamRecordDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        protected override void R_Saving(CompanyParamRecordDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            string lcQuery;
            R_Db loDb;
            DbCommand loCmd;
            DbConnection loConn = null;
            string lcAction = "";
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);

                lcQuery = "RSP_GS_MAINTAIN_COMP_PARAM";

                switch (poCRUDMode)
                {
                    case eCRUDMode.AddMode:
                        lcAction = "ADD";
                        break;

                    case eCRUDMode.EditMode:
                        lcAction = "EDIT";
                        break;
                }

                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@LPRIMARY_ACCOUNT", DbType.Boolean, int.MaxValue, poNewEntity.LPRIMARY_ACCOUNT);
                loDb.R_AddCommandParameter(loCmd, "@CBASE_CURRENCY_CODE", DbType.String, int.MaxValue, poNewEntity.CBASE_CURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CLOCAL_CURRENCY_CODE", DbType.String, int.MaxValue, poNewEntity.CLOCAL_CURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCENTER_BY", DbType.String, int.MaxValue, poNewEntity.CCENTER_BY);
                loDb.R_AddCommandParameter(loCmd, "@LCASH_FLOW", DbType.Boolean, int.MaxValue, poNewEntity.LCASH_FLOW);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, int.MaxValue, lcAction);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poNewEntity.CUSER_ID);
                try
                {
                    ShowLogDebug(lcQuery, loCmd.Parameters);
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

        public CheckPrimaryAccountDTO CheckIsPrimaryAccount(string pcCompanyId)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loException = new R_Exception();
            CheckPrimaryAccountDTO loRtn = null;
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("");
                string lcQuery = $"SELECT TOP 1 1 as LIS_PRIMARY FROM GSM_COMPANY (NOLOCK) WHERE CCOMPANY_ID <> @CCOMPANY_ID AND LPRIMARY_ACCOUNT = 1";
                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, pcCompanyId);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<CheckPrimaryAccountDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        public ValidateCompanyDTO ValidateCompanyParamEditable(string pcCompanyId, string pcUserId)
        {
            R_Exception loEx = new();
            ValidateCompanyDTO loRtn = new();
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("");
                string lcQuery = $"RSP_GS_VALIDATE_COMP_PARAM";
                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;
                loCmd.CommandType=CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, pcCompanyId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, pcUserId);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<ValidateCompanyDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
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
