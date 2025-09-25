using APR00100COMMON;
using APR00100COMMON.DTO_s;
using APR00100COMMON.DTO_s.Print;
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

namespace APR00100BACK
{
    public class APR00100Cls
    {
        private APR00100Logger _logger;

        private readonly ActivitySource _activitySource;

        public APR00100Cls()
        {
            _logger = APR00100Logger.R_GetInstanceLogger();
            _activitySource = APR00100Activity.R_GetInstanceActivitySource();
        }

        public List<APR00100SpResultDTO> GetSummaryData(APR00100SpParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<APR00100SpResultDTO> loRtn = null;
            R_Db loDB = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery = "";
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_APR00100_GET_REPORT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poParam.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CFROM_SUPPLIER_ID", DbType.String, int.MaxValue, poParam.CFROM_SUPPLIER_ID);
                loDB.R_AddCommandParameter(loCmd, "@CTO_SUPPLIER_ID", DbType.String, int.MaxValue, poParam.CTO_SUPPLIER_ID);
                loDB.R_AddCommandParameter(loCmd, "@CFROM_JRNGRP_CODE", DbType.String, int.MaxValue, poParam.CFROM_JRNGRP_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTO_JRNGRP_CODE", DbType.String, int.MaxValue, poParam.CTO_JRNGRP_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CREMAINING_BASED_ON", DbType.String, int.MaxValue, poParam.CREMAINING_BASED_ON);
                loDB.R_AddCommandParameter(loCmd, "@CCUT_OFF", DbType.String, int.MaxValue, poParam.CCUT_OFF);
                loDB.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, int.MaxValue, poParam.CPERIOD);
                loDB.R_AddCommandParameter(loCmd, "@CREPORT_TYPE", DbType.String, int.MaxValue, poParam.CREPORT_TYPE);
                loDB.R_AddCommandParameter(loCmd, "@CSORT_BY", DbType.String, int.MaxValue, poParam.CSORT_BY);
                loDB.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE_CODE", DbType.String, int.MaxValue, poParam.CCURRENCY_TYPE_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CFROM_DEPT_CODE", DbType.String, int.MaxValue, poParam.CFROM_DEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTO_DEPT_CODE", DbType.String, int.MaxValue, poParam.CTO_DEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@LALLOCATION", DbType.Boolean, int.MaxValue, poParam.LALLOCATION);
                loDB.R_AddCommandParameter(loCmd, "@CTRANSACTION_TYPE_CODE", DbType.String, int.MaxValue, poParam.CTRANSACTION_TYPE_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CSUPPLIER_CATEGORY_CODE", DbType.String, int.MaxValue, poParam.CSUPPLIER_CATEGORY_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, int.MaxValue, poParam.CLANG_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<APR00100SpResultDTO>(loRtnTemp).ToList();
             }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public List<APR00101SpResultDTO> GetDetailData(APR00100SpParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<APR00101SpResultDTO> loRtn = null;
            R_Db loDB = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery = "";
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_APR00100_GET_REPORT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poParam.CPROPERTY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CFROM_CUSTOMER_ID", DbType.String, int.MaxValue, poParam.CFROM_SUPPLIER_ID);
                loDB.R_AddCommandParameter(loCmd, "@CTO_CUSTOMER_ID", DbType.String, int.MaxValue, poParam.CTO_SUPPLIER_ID);
                loDB.R_AddCommandParameter(loCmd, "@FROM_JRNGRP_CODE", DbType.String, int.MaxValue, poParam.CFROM_JRNGRP_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTO_JRNGRP_CODE", DbType.String, int.MaxValue, poParam.CTO_JRNGRP_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CREMAINING_BASED_ON", DbType.String, int.MaxValue, poParam.CREMAINING_BASED_ON);
                loDB.R_AddCommandParameter(loCmd, "@CCUT_OFF", DbType.String, int.MaxValue, poParam.CCUT_OFF);
                loDB.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, int.MaxValue, poParam.CPERIOD);
                loDB.R_AddCommandParameter(loCmd, "@CREPORT_TYPE", DbType.String, int.MaxValue, poParam.CREPORT_TYPE);
                loDB.R_AddCommandParameter(loCmd, "@CSORT_BY", DbType.String, int.MaxValue, poParam.CSORT_BY);
                loDB.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE_CODE", DbType.String, int.MaxValue, poParam.CCURRENCY_TYPE_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CFROM_DEPT_CODE", DbType.String, int.MaxValue, poParam.CFROM_DEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTO_DEPT_CODE", DbType.String, int.MaxValue, poParam.CTO_DEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@LALLOCATION", DbType.Boolean, int.MaxValue, poParam.LALLOCATION);
                loDB.R_AddCommandParameter(loCmd, "@CTRANSACTION_TYPE_CODE", DbType.String, int.MaxValue, poParam.CTRANSACTION_TYPE_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CSUPPLIER_CATEGORY_CODE", DbType.String, int.MaxValue, poParam.CSUPPLIER_CATEGORY_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, int.MaxValue, poParam.CLANG_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<APR00101SpResultDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        // public PrintLogoResultDTO GetBaseHeaderLogoCompany(string pcCompanyId)
        // {
        //     using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
        //     var loEx = new R_Exception();
        //     PrintLogoResultDTO loResult = null;
        //
        //     try
        //     {
        //         var loDb = new R_Db();
        //         var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
        //         var loCmd = loDb.GetCommand();
        //
        //
        //         var lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
        //         loCmd.CommandText = lcQuery;
        //         loCmd.CommandType = CommandType.Text;
        //         loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, pcCompanyId);
        //
        //         //Debug Logs
        //         var loDbParam = loCmd.Parameters.Cast<DbParameter>()
        //         .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
        //         _logger.LogDebug("SELECT dbo.RFN_GET_COMPANY_LOGO({@CCOMPANY_ID}) as CLOGO", loDbParam);
        //
        //         var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
        //         loResult = R_Utility.R_ConvertTo<PrintLogoResultDTO>(loDataTable).FirstOrDefault();
        //     }
        //     catch (Exception ex)
        //     {
        //         loEx.Add(ex);
        //         _logger.LogError(loEx);
        //     }
        //
        //     loEx.ThrowExceptionIfErrors();
        //
        //     return loResult;
        // }

        public HeaderPrintResult GetBaseHeaderLogoCompany()
        {
            using Activity activity = _activitySource.StartActivity("GetBaseHeaderLogoCompany");
            var loEx = new R_Exception();
            HeaderPrintResult loResult = null;
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;

            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDb.GetCommand();

                lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 15, R_BackGlobalVar.COMPANY_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _logger.LogDebug(string.Format("SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO", loDbParam));

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<HeaderPrintResult>(loDataTable).FirstOrDefault();

                lcQuery = "EXEC RSP_GS_GET_COMPANY_INFO @CCOMPANY_ID";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                //Debug Logs
                _logger.LogDebug(lcQuery);
                loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                var loCompanyNameResult = R_Utility.R_ConvertTo<HeaderPrintResult>(loDataTable).FirstOrDefault();

                loResult.CCOMPANY_NAME = loCompanyNameResult.CCOMPANY_NAME;
                loResult.CDATETIME_NOW = loCompanyNameResult.CDATETIME_NOW;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
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
