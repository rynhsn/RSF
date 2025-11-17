using APR00700COMMON;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using APR00700COMMON.DTO_s;
using APR00700COMMON.DTOs;

namespace APR00700BACK
{
    public class APR00700GeneralCls
    {
        private APR00700Logger _logger;

        private readonly ActivitySource _activitySource;

        public APR00700GeneralCls()
        {
            _logger = APR00700Logger.R_GetInstanceLogger();
            _activitySource = APR00700Activity.R_GetInstanceActivitySource();
        }

        public async Task<List<PropertyDTO>> GetPropertyList(PropertyDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<PropertyDTO> loRtn = new List<PropertyDTO>();
            try
            {
                R_Db loDB = new R_Db();
                DbConnection loConn = await loDB.GetConnectionAsync();
                DbCommand loCmd = loDB.GetCommand();

                string lcQuery = "RSP_GS_GET_PROPERTY_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poEntity.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = await loDB.SqlExecQueryAsync(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<PropertyDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public async Task <List<PeriodDtDTO>> GetPeriodDtList(string pcCompanyId, string pcYear)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<PeriodDtDTO> loRtn = new List<PeriodDtDTO>();

            try
            {
                R_Db loDB = new R_Db();
                DbConnection loConn = await loDB.GetConnectionAsync();
                DbCommand loCmd = loDB.GetCommand();

                string lcQuery = "RSP_GS_GET_PERIOD_DT_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, pcCompanyId);
                loDB.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, pcYear);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = await loDB.SqlExecQueryAsync(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<PeriodDtDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public async Task<PeriodYearDTO> GetPeriodYearRecord(PeriodYearDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            PeriodYearDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = await loDb.GetConnectionAsync();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_PERIOD_YEAR_RANGE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, poParam.CYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CMODE", DbType.String, 10, poParam.CMODE);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PeriodYearDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<APR00700SystemParamDTO> GetSystemParam(APR00700SystemParamDTO poParams)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            APR00700SystemParamDTO loRtn = new APR00700SystemParamDTO();
            try
            {
                R_Db loDB = new R_Db();
                DbConnection loConn = loDB.GetConnection();
                DbCommand loCmd = loDB.GetCommand();

                string lcQuery = "RSP_AP_GET_SYSTEM_PARAM";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, R_BackGlobalVar.COMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 20, R_BackGlobalVar.CULTURE);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParams.CPROPERTY_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<APR00700SystemParamDTO>(loRtnTemp).FirstOrDefault();
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

        private void ShowLogDebug(string pcQuery, DbParameterCollection poParam)
        {
            var paramValues = string.Join(", ", poParam.Cast<DbParameter>().Select(p => $"{p.ParameterName} = '{p.Value}'"));
            _logger.LogDebug($"EXEC {pcQuery} {paramValues}");
        }

        private void ShowLogError(Exception poException)
        {
            _logger.LogError(poException);
        }

        #endregion
    }
}
