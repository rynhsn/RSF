using PMF00100BACK.OpenTelemetry;
using PMF00100COMMON.Logger;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMF00100COMMON.DTOs.PMF00100;

namespace PMF00100BACK
{
    public class PMF00100Cls
    {
        private LoggerPMF00100 _logger;
        private readonly ActivitySource _activitySource;

        public PMF00100Cls()
        {
            _logger = LoggerPMF00100.R_GetInstanceLogger();
            _activitySource = PMF00100ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<PMF00100ListDTO> GetAllocationList(PMF00100ListParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetAllocationList");
            R_Exception loException = new R_Exception();
            List<PMF00100ListDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_PM_GET_ALLOCATION_LIST " +
                    "@CCOMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "@CDEPT_CODE, " +
                    "@CTRANS_CODE, " +
                    "@CREF_NO, " +
                    "@CLANGUAGE_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poParameter.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParameter.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_GET_ALLOCATION_LIST {@Parameters} || GetAllocationList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMF00100ListDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public PMF00100HeaderDTO GetHeader(PMF00100HeaderParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetHeader");
            R_Exception loException = new R_Exception();
            PMF00100HeaderDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_PM_GET_TRANS_HD " +
                    "@CLOGIN_COMPANY_ID, " +
                    //"@CPROPERTY_ID, " +
                    "@CDEPT_CODE, " +
                    "@CTRANS_CODE, " +
                    "@CREF_NO, " +
                    "@CREC_ID, " +
                    "@CLANGUAGE_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParam.CLOGIN_COMPANY_ID);
                //loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParam.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParam.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParam.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poParam.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParam.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParam.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_GET_TRANS_HD {@Parameters} || GetHeader(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMF00100HeaderDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public PMF00100HeaderDTO GetCAWTCustReceipt(PMF00100HeaderParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetCAWTCustReceipt");
            R_Exception loException = new R_Exception();
            PMF00100HeaderDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_PM_GET_CA_WT_CUST_RECEIPT " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CREC_ID, " +
                    "@CLANGUAGE_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParam.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParam.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParam.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_GET_CA_WT_CUST_RECEIPT {@Parameters} || GetCAWTCustReceipt(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMF00100HeaderDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public PMF00100HeaderDTO GetCQCustReceipt(PMF00100HeaderParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetCQCustReceipt");
            R_Exception loException = new R_Exception();
            PMF00100HeaderDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_PM_GET_CQ_CUST_RECEIPT " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CREC_ID, " +
                    "@CLANGUAGE_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParam.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParam.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParam.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_GET_CQ_CUST_RECEIPT {@Parameters} || GetCQCustReceipt(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMF00100HeaderDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        //public PMF00100HeaderDTO GetCallerTrxInfo(PMF00100HeaderParameterDTO poParam)
        //{
        //    using Activity activity = _activitySource.StartActivity("GetCallerTrxInfo");
        //    R_Exception loException = new R_Exception();
        //    PMF00100HeaderDTO loResult = null;
        //    R_Db loDb = new R_Db();
        //    DbConnection loConn = null;
        //    DbCommand loCmd = null;
        //    string lcQuery;

        //    try
        //    {
        //        loConn = loDb.GetConnection();
        //        loCmd = loDb.GetCommand();

        //        lcQuery = "EXEC RSP_PM_GET_TRANS_HD " +
        //            "@CLOGIN_COMPANY_ID, " +
        //            //"@CPROPERTY_ID, " +
        //            "@CDEPT_CODE, " +
        //            "@CTRANS_CODE, " +
        //            "@CREF_NO, " +
        //            "@CREC_ID, " +
        //            "@CLANGUAGE_ID";

        //        loCmd.CommandText = lcQuery;

        //        loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParam.CLOGIN_COMPANY_ID);
        //        //loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParam.CPROPERTY_ID);
        //        loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParam.CDEPT_CODE);
        //        loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParam.CTRANSACTION_CODE);
        //        loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poParam.CREFERENCE_NO);
        //        loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParam.CREC_ID);
        //        loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParam.CLANGUAGE_ID);

        //        var loDbParam = loCmd.Parameters.Cast<DbParameter>()
        //            .Where(x =>
        //            x != null && x.ParameterName.StartsWith("@"))
        //            .Select(x => x.Value);

        //        _logger.LogDebug("EXEC RSP_PM_GET_TRANS_HD {@Parameters} || GetCallerTrxInfo(Cls) ", loDbParam);

        //        var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

        //        loResult = R_Utility.R_ConvertTo<GetCallerTrxInfoDTO>(loDataTable).FirstOrDefault();
        //    }
        //    catch (Exception ex)
        //    {
        //        loException.Add(ex);
        //        _logger.LogError(loException);
        //    }

        //    loException.ThrowExceptionIfErrors();
        //    return loResult;
        //}

        public GetCompanyInfoDTO GetCompanyInfo(GetCompanyInfoParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetCompanyInfo");
            R_Exception loException = new R_Exception();
            GetCompanyInfoDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_GS_GET_COMPANY_INFO @CCOMPANY_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_COMPANY_INFO {@Parameters} || GetCompanyInfo(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetCompanyInfoDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public GetGLSystemParamDTO GetGLSystemParam(GetGLSystemParamParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetGLSystemParam");
            R_Exception loException = new R_Exception();
            GetGLSystemParamDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_GL_GET_SYSTEM_PARAM @CCOMPANY_ID, @CLANGUAGE_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParam.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GL_GET_SYSTEM_PARAM {@Parameters} || GetGLSystemParam(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetGLSystemParamDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public GetPeriodDTO GetPeriod(GetPeriodParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetPeriod");
            R_Exception loException = new R_Exception();
            GetPeriodDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_GS_GET_PERIOD_DT_INFO " +
                    $"@CCOMPANY_ID, " +
                    $"@CPERIOD_YY, " +
                    $"@CPERIOD_MM";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_YY", DbType.String, 50, poEntity.CPERIOD_YY);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_MM", DbType.String, 50, poEntity.CPERIOD_MM);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_PERIOD_DT_INFO {@Parameters} || GetPeriod(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetPeriodDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }
        public GetTransactionFlagDTO GetTransactionFlag(GetTransactionFlagParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetTransactionFlag");
            R_Exception loException = new R_Exception();
            GetTransactionFlagDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = "SELECT dbo.RFN_PM_IS_ALLOCATION_TO_TRX(@CTRANS_CODE) AS LVALUE";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParam.CTRANS_CODE);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug(String.Format("SELECT dbo.RFN_PM_IS_ALLOCATION_TO_TRX({0}) AS LVALUE || GetTransactionFlag(Cls) ", poParam), loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetTransactionFlagDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
