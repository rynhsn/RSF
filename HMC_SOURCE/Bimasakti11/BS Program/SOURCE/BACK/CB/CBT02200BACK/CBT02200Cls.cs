using CBT02200BACK.OpenTelemetry;
using CBT02200COMMON.Logger;
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
using CBT02200COMMON.DTO.CBT02200;
using System.Reflection.Metadata;

namespace CBT02200BACK
{
    public class CBT02200Cls
    {
        //RSP_CB_UPDATE_TRANS_HD_STATUSResources.Resources_Dummy_Class loUpdateStatus = new RSP_CB_UPDATE_TRANS_HD_STATUSResources.Resources_Dummy_Class();

        private LoggerCBT02210 _logger;
        private readonly ActivitySource _activitySource;
        public CBT02200Cls()
        {
            _logger = LoggerCBT02210.R_GetInstanceLogger();
            _activitySource = CBT02200ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public GetCompanyInfoDTO GetCompanyInfo(GetCompanyInfoParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetCompanyInfo");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            string lcQuery;
            GetCompanyInfoDTO loResult = null;
            DbCommand loCmd = null;

            try
            {
                loCmd = loDb.GetCommand();
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_GS_GET_COMPANY_INFO " +
                    $"@CLOGIN_COMPANY_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);

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

        public GetGLSystemParamDTO GetGLSystemParam(GetGLSystemParamParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetGLSystemParam");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            string lcQuery;
            GetGLSystemParamDTO loResult = null;
            DbCommand loCmd = null;

            try
            {
                loCmd = loDb.GetCommand();
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_GL_GET_SYSTEM_PARAM " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CLANGUAGE_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParameter.CLANGUAGE_ID);

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

        public GetCBSystemParamDTO GetCBSystemParam(GetCBSystemParamParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetCBSystemParam");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            string lcQuery;
            GetCBSystemParamDTO loResult = null;
            DbCommand loCmd = null;

            try
            {
                loCmd = loDb.GetCommand();
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_CB_GET_SYSTEM_PARAM " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CLANGUAGE_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParameter.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_CB_GET_SYSTEM_PARAM {@Parameters} || GetCBSystemParam(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetCBSystemParamDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public GetSoftPeriodStartDateDTO GetSoftPeriodStartDate(GetSoftPeriodStartDateParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetSoftPeriodStartDate");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            string lcQuery;
            GetSoftPeriodStartDateDTO loResult = null;
            DbCommand loCmd = null;

            try
            {
                loCmd = loDb.GetCommand();
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_GS_GET_PERIOD_DT_INFO " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CSOFT_PERIOD_YY, " +
                    $"@CSOFT_PERIOD_MM";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSOFT_PERIOD_YY", DbType.String, 50, poParameter.CSOFT_PERIOD_YY);
                loDb.R_AddCommandParameter(loCmd, "@CSOFT_PERIOD_MM", DbType.String, 50, poParameter.CSOFT_PERIOD_MM);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_PERIOD_DT_INFO {@Parameters} || GetSoftPeriodStartDate(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetSoftPeriodStartDateDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public GetTransCodeInfoDTO GetTransCodeInfo(GetTransCodeInfoParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetTransCodeInfo");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            string lcQuery;
            GetTransCodeInfoDTO loResult = null;
            DbCommand loCmd = null;

            try
            {
                loCmd = loDb.GetCommand();
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_GS_GET_TRANS_CODE_INFO " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CTRANS_CODE";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParameter.CTRANS_CODE);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_TRANS_CODE_INFO {@Parameters} || GetTransCodeInfo(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetTransCodeInfoDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public GetPeriodYearRangeDTO GetPeriodYearRange(GetPeriodYearRangeParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetPeriodYearRange");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            string lcQuery;
            GetPeriodYearRangeDTO loResult = null;
            DbCommand loCmd = null;

            try
            {
                loCmd = loDb.GetCommand();
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_GS_GET_PERIOD_YEAR_RANGE " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CYEAR, " +
                    $"@CMODE";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CMODE", DbType.String, 50, "");

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_PERIOD_YEAR_RANGE {@Parameters} || GetPeriodYearRange(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetPeriodYearRangeDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GetDeptLookupListDTO> GetDeptLookupList(GetDeptLookupListParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetDeptLookupList");
            R_Exception loException = new R_Exception();
            List<GetDeptLookupListDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_GS_GET_DEPT_LOOKUP_LIST " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CLOGIN_USER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poParameter.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_DEPT_LOOKUP_LIST {@Parameters} || GetDeptLookupList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetDeptLookupListDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GetStatusDTO> GetStatusList(GetStatusParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetStatusList");
            R_Exception loException = new R_Exception();
            List<GetStatusDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_GS_GET_GSB_CODE_LIST " +
                    $"@CAPPLICATION, " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CCLASS_ID, " +
                    $"@CLANGUAGE_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CAPPLICATION", DbType.String, 50, "BIMASAKTI");
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCLASS_ID", DbType.String, 50, "_CB_Cheque_STATUS");
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParameter.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_GSB_CODE_LIST {@Parameters} || GetStatusList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetStatusDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }


        public List<CBT02200GridDTO> GetChequeHeaderList(CBT02200GridParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetChequeHeaderList");
            R_Exception loException = new R_Exception();
            List<CBT02200GridDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_CB_SEARCH_CHEQUE_HD_LIST " +
                    $"@CLOGIN_COMPANY_ID, " +
                    $"@CLOGIN_USER_ID, " +
                    $"@CTRANS_CODE, " +
                    $"@CDEPT_CODE, " +
                    $"@CCB_CODE, " +
                    $"@CCB_ACCOUNT_NO, " +
                    $"@CPERIOD, " +
                    $"@CSTATUS, " +
                    $"@CSEARCH_TEXT, " +
                    $"@CLANGUAGE_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poParameter.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCB_CODE", DbType.String, 50, poParameter.CCB_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCB_ACCOUNT_NO", DbType.String, 50, poParameter.CCB_ACCOUNT_NO);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 50, poParameter.CPERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 50, poParameter.CSTATUS);
                loDb.R_AddCommandParameter(loCmd, "@CSEARCH_TEXT", DbType.String, 50, poParameter.CSEARCH_TEXT);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParameter.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_CB_SEARCH_CHEQUE_HD_LIST {@Parameters} || GetChequeHeaderList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<CBT02200GridDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<CBT02200GridDetailDTO> GetChequeDetailList(CBT02200GridDetailParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetChequeDetailList");
            R_Exception loException = new R_Exception();
            List<CBT02200GridDetailDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_CB_GET_CHEQUE_JRN_LIST " +
                    $"@CREC_ID, " +
                    $"@CLANGUAGE_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParameter.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParameter.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_CB_GET_CHEQUE_JRN_LIST {@Parameters} || GetChequeDetailList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<CBT02200GridDetailDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public void UpdateStatus(UpdateStatusParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("UpdateStatus");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = $"EXEC RSP_CB_UPDATE_CHEQUE_HD_STATUS " +
                                 $"@CLOGIN_COMPANY_ID, " +
                                 $"@CLOGIN_USER_ID, " +
                                 $"@CREC_ID_LIST, " +
                                 $"@CNEW_STATUS";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poParameter.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID_LIST", DbType.String, 50, poParameter.CREC_ID_LIST);
                loDb.R_AddCommandParameter(loCmd, "@CNEW_STATUS", DbType.String, 50, poParameter.CNEW_STATUS);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_CB_UPDATE_CHEQUE_HD_STATUS {@Parameters} || UpdateStatus(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }

                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
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
            loException.ThrowExceptionIfErrors();
        }
    }
}
