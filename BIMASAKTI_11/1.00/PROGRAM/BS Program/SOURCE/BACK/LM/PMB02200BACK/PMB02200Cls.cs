using PMB02200COMMON;
using PMB02200COMMON.DTO_s;
using R_BackEnd;
using R_Common;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace PMB02200BACK
{
    public class PMB02200Cls
    {
        private LoggerPMB02200 _logger;

        private readonly ActivitySource _activitySource;
        
        public PMB02200Cls()
        {
            _logger = LoggerPMB02200.R_GetInstanceLogger();
            _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
        }

        public List<UtilityChargesDTO> GetUpdatetUtilityChargesList(UtilityChargesParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new();
            List<UtilityChargesDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PM_GET_UPDATE_UTILITY_CHARGES_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poParam.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, int.MaxValue, poParam.CDEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, int.MaxValue, poParam.CBUILDING_ID);
                loDB.R_AddCommandParameter(loCmd, "@LALL_BUILDING", DbType.Boolean, int.MaxValue, poParam.LALL_BUILDING);
                loDB.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, int.MaxValue, poParam.CTENANT_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUTILITY_TYPE", DbType.String, int.MaxValue, poParam.CUTILITY_TYPE);
                loDB.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, int.MaxValue, poParam.CLANG_ID);
                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<UtilityChargesDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        #region log method helper

        private void ShowLogDebug(string query, DbParameterCollection parameters)
        {
            var paramValues = string.Join(", ", parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'"));
            _logger.LogDebug($"EXEC {query} {paramValues}");
        }

        private void ShowLogError(Exception ex)
        {
            _logger.LogError(ex);
        }

        #endregion
    }
}
