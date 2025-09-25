using CBR00600COMMON;
using R_BackEnd;
using R_Common;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace CBR00600BACK
{
    public class CBR00600Cls
    {
        private LoggerCBR00600 _Logger;
        private readonly ActivitySource _activitySource;
        public CBR00600Cls()
        {
            _Logger = LoggerCBR00600.R_GetInstanceLogger();
            _activitySource = CBR00600ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public async Task<List<CBR00600PropetyDTO>> GetPropertyList()
        {
            using Activity activity = _activitySource.StartActivity("GetPropertyList");
            var loEx = new R_Exception();
            List<CBR00600PropetyDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = await loDb.GetConnectionAsync();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_PROPERTY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_PROPERTY_LIST {@poParameter}", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<CBR00600PropetyDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<List<CBR00600TrxTypeDTO>> GetTrxTypeList()
        {
            using Activity activity = _activitySource.StartActivity("GetTrxTypeList");
            var loEx = new R_Exception();
            List<CBR00600TrxTypeDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = await loDb.GetConnectionAsync();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_CB_GET_PRINT_TRX_TYPE_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_CB_GET_PRINT_TRX_TYPE_LIST {@poParameter}", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<CBR00600TrxTypeDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<List<CBR00600PeriodDTO>> GetPeriodList(CBR00600PeriodDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetPeriodList");
            var loEx = new R_Exception();
            List<CBR00600PeriodDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = await loDb.GetConnectionAsync();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_PERIOD_DT_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 50, poEntity.CCYEAR);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_PERIOD_DT_LIST {@poParameter}", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<CBR00600PeriodDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #region Report SP
        public CBR00600GSCompanyInfoDTO GetCompanyInfo()
        {
            using Activity activity = _activitySource.StartActivity("GetCompanyInfo");
            var loEx = new R_Exception();
            CBR00600GSCompanyInfoDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);

                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_COMPANY_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_COMPANY_INFO {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<CBR00600GSCompanyInfoDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public CBR00600PrintResultDTO GetBaseHeaderLogoCompany()
        {
            using Activity activity = _activitySource.StartActivity("GetBaseHeaderLogoCompany");
            var loEx = new R_Exception();
            CBR00600PrintResultDTO loResult = null;
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
                _Logger.LogDebug(string.Format("SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO", loDbParam));

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<CBR00600PrintResultDTO>(loDataTable).FirstOrDefault();

                lcQuery = "EXEC RSP_GS_GET_COMPANY_INFO @CCOMPANY_ID";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                //Debug Logs
                _Logger.LogDebug(string.Format("EXEC RSP_GS_GET_COMPANY_INFO '@CCOMPANY_ID'", loDbParam));
                loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                var loCompanyNameResult = R_Utility.R_ConvertTo<CBR00600PrintResultDTO>(loDataTable).FirstOrDefault();

                loResult.CCOMPANY_NAME = loCompanyNameResult.CCOMPANY_NAME;
                loResult.CDATETIME_NOW = loCompanyNameResult.CDATETIME_NOW;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                //_Logger.LogError(loEx);
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
        public List<CBR00600SPResultDTO> GetDataReport(CBR00600DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetDataReport");
            var loEx = new R_Exception();
            List<CBR00600SPResultDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);

                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_CBR00600_GET_DATA";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CFILTER_BY", DbType.String, 50, poEntity.CFILTER_BY);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_VALUE", DbType.String, 50, poEntity.CFROM_VALUE);
                loDb.R_AddCommandParameter(loCmd, "@CTO_VALUE", DbType.String, 50, poEntity.CTO_VALUE);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 50, poEntity.CPERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CMESSAGE_TYPE", DbType.String, 50, poEntity.CMESSAGE_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CMESSAGE_NO", DbType.String, 50, poEntity.CMESSAGE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CREPORT_TYPE", DbType.String, 50, poEntity.CREPORT_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_CBR00600_GET_DATA {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<CBR00600SPResultDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion
    }
}
