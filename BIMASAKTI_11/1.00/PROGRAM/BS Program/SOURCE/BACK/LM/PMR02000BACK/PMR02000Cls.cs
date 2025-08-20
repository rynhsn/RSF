using PMR02000COMMON;
using PMR02000COMMON.DTO_s;
using PMR02000COMMON.DTO_s.Print;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PMR02000BACK
{
    public class PMR02000Cls
    {
        private PMR02000Logger _logger;

        private readonly ActivitySource _activitySource;

        public PMR02000Cls()
        {
            _logger = PMR02000Logger.R_GetInstanceLogger();
            _activitySource = PMR02000Activity.R_GetInstanceActivitySource();
        }

        public List<PMR02000SpResultDTO> GetSummaryData(PMR02000SpParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<PMR02000SpResultDTO> loRtn = null;
            R_Db loDB = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery = "";
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PMR02000_GET_REPORT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poParam.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poParam.CUSER_ID);
                loDB.R_AddCommandParameter(loCmd, "@CREPORT_TYPE", DbType.String, int.MaxValue, poParam.CREPORT_TYPE);
                loDB.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, int.MaxValue, poParam.CCURRENCY_TYPE_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CBASED_ON", DbType.String, int.MaxValue, poParam.CBASED_ON);
                loDB.R_AddCommandParameter(loCmd, "CFR_BASED_ON", DbType.String, int.MaxValue, poParam.CBASED_ON=="C"? poParam.CFROM_CUSTOMER_ID: poParam.CFROM_JRNGRP_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTO_BASED_ON", DbType.String, int.MaxValue, poParam.CBASED_ON == "C" ? poParam.CTO_CUSTOMER_ID: poParam.CTO_JRNGRP_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, int.MaxValue, poParam.CPERIOD);
                loDB.R_AddCommandParameter(loCmd, "@CREMAINING_BASED_ON", DbType.String, int.MaxValue, poParam.CREMAINING_BASED_ON);
                loDB.R_AddCommandParameter(loCmd, "@CCUT_OFF_DATE", DbType.String, int.MaxValue, poParam.CCUT_OFF_DATE);
                loDB.R_AddCommandParameter(loCmd, "@CFR_DEPT_CODE", DbType.String, int.MaxValue, poParam.CFR_DEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTO_DEPT_CODE", DbType.String, int.MaxValue, poParam.CTO_DEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, int.MaxValue, poParam.CTRANS_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTENANT_CATEGORY_ID", DbType.String, int.MaxValue, poParam.CTENANT_CATEGORY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CSORT_BY", DbType.String, int.MaxValue, poParam.CSORT_BY);
                loDB.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, int.MaxValue, poParam.CLANGUAGE_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<PMR02000SpResultDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public List<PMR02001SpResultDTO> GetDetailData(PMR02000SpParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<PMR02001SpResultDTO> loRtn = null;
            R_Db loDB = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery = "";
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_PMR02000_GET_REPORT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poParam.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poParam.CUSER_ID);
                loDB.R_AddCommandParameter(loCmd, "@CREPORT_TYPE", DbType.String, int.MaxValue, poParam.CREPORT_TYPE);
                loDB.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, int.MaxValue, poParam.CCURRENCY_TYPE_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CBASED_ON", DbType.String, int.MaxValue, poParam.CBASED_ON);
                loDB.R_AddCommandParameter(loCmd, "CFR_BASED_ON", DbType.String, int.MaxValue, poParam.CBASED_ON == "C" ? poParam.CFROM_CUSTOMER_ID : poParam.CFROM_JRNGRP_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTO_BASED_ON", DbType.String, int.MaxValue, poParam.CBASED_ON == "C" ? poParam.CTO_CUSTOMER_ID : poParam.CTO_JRNGRP_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, int.MaxValue, poParam.CPERIOD);
                loDB.R_AddCommandParameter(loCmd, "@CREMAINING_BASED_ON", DbType.String, int.MaxValue, poParam.CREMAINING_BASED_ON);
                loDB.R_AddCommandParameter(loCmd, "@CCUT_OFF_DATE", DbType.String, int.MaxValue, poParam.CCUT_OFF_DATE);
                loDB.R_AddCommandParameter(loCmd, "@CFR_DEPT_CODE", DbType.String, int.MaxValue, poParam.CFR_DEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTO_DEPT_CODE", DbType.String, int.MaxValue, poParam.CTO_DEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, int.MaxValue, poParam.CTRANS_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTENANT_CATEGORY_ID", DbType.String, int.MaxValue, poParam.CTENANT_CATEGORY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CSORT_BY", DbType.String, int.MaxValue, poParam.CSORT_BY);
                loDB.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, int.MaxValue, poParam.CLANGUAGE_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<PMR02001SpResultDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public PrintLogoResultDTO GetCompanyLogo(string pcCompanyId)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            PrintLogoResultDTO loResult = null;
            try
            {
                R_Db loDb = new();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();
                var lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, pcCompanyId);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PrintLogoResultDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public PrintLogoResultDTO GetCompanyName(string pcCompanyId)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);

            var loEx = new R_Exception();
            PrintLogoResultDTO loResult = null;
            try
            {
                R_Db loDb = new();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();
                var lcQuery = "SELECT CCOMPANY_NAME FROM SAM_COMPANIES WHERE CCOMPANY_ID = @CCOMPANY_ID";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, pcCompanyId);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PrintLogoResultDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        #region log method helper

        private void ShowLogDebug(string pcQuery, DbParameterCollection poParam)
        {
            var paramValues = string.Join(", ", poParam.Cast<DbParameter>().Select(p => $"{p.ParameterName} : '{p.Value}'"));
            _logger.LogDebug($"EXEC {pcQuery} {paramValues}");
        }

        private void ShowLogError(Exception poException)
        {
            _logger.LogError(poException);
        }

        #endregion
    }
}
