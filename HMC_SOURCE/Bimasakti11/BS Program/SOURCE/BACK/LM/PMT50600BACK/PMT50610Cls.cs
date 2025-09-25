using PMT50600COMMON.DTOs.PMT50600;
using PMT50600COMMON.DTOs.PMT50610;
using PMT50600COMMON.Loggers;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_CommonFrontBackAPI;
using PMT50600COMMON.DTOs.PMT50610Print;
using System.Diagnostics;
using PMT50600BACK.OpenTelemetry;

namespace PMT50600BACK
{
    public class PMT50610Cls : R_BusinessObject<PMT50610ParameterDTO>
    {
        RSP_PM_SUBMIT_TRANS_HDResources.Resources_Dummy_Class _loRspSubmitTransHd = new RSP_PM_SUBMIT_TRANS_HDResources.Resources_Dummy_Class();

        private LoggerPMT50610 _logger;
        private readonly ActivitySource _activitySource;
        private LoggerPMT50610Print _loggerPMT50610Print;
        public PMT50610Cls()
        {
            _logger = LoggerPMT50610.R_GetInstanceLogger();
            _activitySource = PMT50610ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public PMT50610Cls(LoggerPMT50610Print logger)
        {
            _loggerPMT50610Print = LoggerPMT50610Print.R_GetInstanceLogger();
            _activitySource = PMT50610PrintActivitySourceBase.R_GetInstanceActivitySource();
        }

        public List<PMT50610PrintReportSubDetailDTO> GetPrintSubDetailReportList(PMT50610PrintReportSubDetailParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetPrintSubDetailReportList");
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            List<PMT50610PrintReportSubDetailDTO> loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);

                lcQuery = "EXEC RSP_GL_GET_JOURNAL_DETAIL_LIST " +
                          "@CJRN_ID, " +
                          "@CLANGUAGE_ID";
                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CJRN_ID", DbType.String, 50, poParam.CJRN_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParam.CLANGUAGE_ID);


                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _loggerPMT50610Print.LogDebug("EXEC RSP_GL_GET_JOURNAL_DETAIL_LIST {@Parameters} || GetPrintSubDetailReportList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMT50610PrintReportSubDetailDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerPMT50610Print.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<PMT50610PrintReportDTO> GetPrintReportList(PMT50610PrintReportParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetPrintReportList");
            R_Db loDb = new R_Db();
            R_Exception loException = new R_Exception();
            List<PMT50610PrintReportDTO> loResult = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);

                lcQuery = "EXEC RSP_PM_REP_TRANS_HD " +
                          "@CLOGIN_COMPANY_ID, " +
                          "@CPROPERTY_ID, " +
                          "@CDEPT_CODE, " +
                          "@CTRANS_CODE, " +
                          "@CREF_NO, " +
                          "@CREC_ID, " +
                          "@CLANGUAGE_ID";
                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParam.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParam.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParam.CLANGUAGE_ID);


                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _loggerPMT50610Print.LogDebug("EXEC RSP_PM_REP_TRANS_HD {@Parameters} || GetPrintReportList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMT50610PrintReportDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerPMT50610Print.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public PMT50610PrintReportDTO GetBaseHeaderLogoCompany(PMT50610PrintReportParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetBaseHeaderLogoCompany");
            R_Exception loEx = new R_Exception();
            PMT50610PrintReportDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();


                var lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CLOGIN_COMPANY_ID) as OLOGO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 15, poEntity.CLOGIN_COMPANY_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _loggerPMT50610Print.LogDebug(string.Format("SELECT dbo.RFN_GET_COMPANY_LOGO(@CLOGIN_COMPANY_ID) as OLOGO", loDbParam));

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMT50610PrintReportDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerPMT50610Print.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }


        public void SubmitJournalProcess(SubmitJournalParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("SubmitJournalProcess");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_PM_SUBMIT_TRANS_HD " +
                          "@CLOGIN_COMPANY_ID, " +
                          "@CPROPERTY_ID, " +
                          "@CLOGIN_USER_ID, " +
                          "@CREC_ID";

                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poParameter.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParameter.CREC_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_SUBMIT_TRANS_HD {@Parameters} || SubmitJournalProcess(Cls) ", loDbParam);

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

        public void RedraftJournalProcess(RedraftJournalParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("RedraftJournalProcess");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_PM_UPDATE_TRANS_HD_STATUS " +
                          "@CLOGIN_COMPANY_ID, " +
                          "@CPROPERTY_ID, " +
                          "@CLOGIN_USER_ID, " +
                          "@CREC_ID, " +
                          "@CNEW_STATUS";

                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poParameter.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParameter.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CNEW_STATUS", DbType.String, 50, "00");

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_UPDATE_TRANS_HD_STATUS {@Parameters} || RedraftJournalProcess(Cls) ", loDbParam);

                loDb.SqlExecNonQuery(loConn, loCmd, true);
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

        public List<GetPaymentTermListDTO> GetPaymentTermList(GetPaymentTermListParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetPaymentTermList");
            R_Exception loException = new R_Exception();
            List<GetPaymentTermListDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_GS_GET_PAYMENT_TERM_LIST " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "@CLOGIN_USER_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poParameter.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_PAYMENT_TERM_LIST {@Parameters} || GetPaymentTermList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetPaymentTermListDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public List<GetCurrencyListDTO> GetCurrencyList(GetCurrencyListParameterDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetCurrencyList");
            R_Exception loException = new R_Exception();
            List<GetCurrencyListDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_GS_GET_CURRENCY_LIST " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CLOGIN_USER_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParameter.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poParameter.CLOGIN_USER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_CURRENCY_LIST {@Parameters} || GetCurrencyList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetCurrencyListDTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public GetCurrencyOrTaxRateDTO GetCurrencyOrTaxRate (GetCurrencyOrTaxRateParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetCurrencyOrTaxRate");
            R_Exception loException = new R_Exception();
            GetCurrencyOrTaxRateDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_GS_GET_LAST_CURRENCY_RATE " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CCURRENCY_CODE, " +
                    "@CRATETYPE_CODE, " +
                    "@CREF_DATE";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParam.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 50, poParam.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CRATETYPE_CODE", DbType.String, 50, poParam.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 50, poParam.CREF_DATE);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_LAST_CURRENCY_RATE {@Parameters} || GetCurrencyRate(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetCurrencyOrTaxRateDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public PMT50610DTO GetInvoiceHeader(GetInvoiceHeaderParameterDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetInvoiceHeader");
            R_Exception loException = new R_Exception();
            PMT50610DTO loResult = null;
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
                    "@CDEPT_CODE, " +
                    "@CTRANS_CODE, " +
                    "@CREF_NO, " +
                    "@CREC_ID, " +
                    "@CLANGUAGE_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poParam.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParam.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poParam.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poParam.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParam.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poParam.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_GET_TRANS_HD {@Parameters} || GetInvoiceHeader(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<PMT50610DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        protected override void R_Deleting(PMT50610ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Deleting");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_PM_DELETE_TRANS_HD " +
                          "@CLOGIN_COMPANY_ID, " +
                          "@CLOGIN_USER_ID, " +
                          "@CREC_ID";

                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poEntity.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.Data.CREC_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_DELETE_TRANS_HD {@Parameters} || R_Deleting(Cls) ", loDbParam);

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

        protected override PMT50610ParameterDTO R_Display(PMT50610ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("R_Display");
            R_Exception loException = new R_Exception();
            PMT50610ParameterDTO loResult = new PMT50610ParameterDTO();
            GetInvoiceHeaderParameterDTO loParam = null;

            try
            {
                loParam = new GetInvoiceHeaderParameterDTO()
                {
                    CLOGIN_COMPANY_ID = poEntity.CLOGIN_COMPANY_ID,
                    CLANGUAGE_ID = poEntity.CLANGUAGE_ID,
                    CREC_ID = poEntity.Data.CREC_ID,
                    CPROPERTY_ID = poEntity.Data.CPROPERTY_ID,
                    CDEPT_CODE = poEntity.Data.CDEPT_CODE,
                    CREFERENCE_NO = poEntity.Data.CREF_NO,
                    CTRANSACTION_CODE = poEntity.Data.CTRANS_CODE
                };
                loResult.Data = GetInvoiceHeader(loParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        protected override void R_Saving(PMT50610ParameterDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity("R_Saving");
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();

                lcQuery = "EXEC RSP_PM_SAVE_TRANS_HD " +
                    "@CLOGIN_COMPANY_ID, " +
                    "@CPROPERTY_ID, " +
                    "@CLOGIN_USER_ID, " +
                    "@CACTION, " +
                    "@CREC_ID, " +
                    "@CREF_NO, " +
                    "@CDEPT_CODE, " +
                    "@CTRANS_CODE, " +
                    "@CREF_DATE, " +
                    "@CTENANT_ID, " +
                    "@CDOC_NO, " +
                    "@CDOC_DATE, " +
                    "@CTERM_CODE, " +
                    "@CDUE_DATE, " +
                    "@CTRANS_DESC, " +
                    "@CCURRENCY_CODE, " +
                    "@NLCURRENCY_BASE_RATE, " +
                    "@NLCURRENCY_RATE, " +
                    "@NBCURRENCY_BASE_RATE, " +
                    "@NBCURRENCY_RATE, " +
                    "@LTAXABLE, " +
                    "@CTAX_ID, " +
                    "@NTAX_PERCENTAGE, " +
                    "@NTAX_BASE_RATE, " +
                    "@NTAX_RATE, " +
                    "@CSOURCE_MODULE, " +
                    "@CTRANS_STATUS";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_COMPANY_ID", DbType.String, 50, poNewEntity.CLOGIN_COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poNewEntity.Data.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLOGIN_USER_ID", DbType.String, 50, poNewEntity.CLOGIN_USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.Data.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poNewEntity.Data.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.Data.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, "920040");
                loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 50, poNewEntity.Data.CREF_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 50, poNewEntity.Data.CTENANT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_NO", DbType.String, 50, poNewEntity.Data.CDOC_NO);
                loDb.R_AddCommandParameter(loCmd, "@CDOC_DATE", DbType.String, 50, poNewEntity.Data.CDOC_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CTERM_CODE", DbType.String, 50, poNewEntity.Data.CPAY_TERM_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDUE_DATE", DbType.String, 50, poNewEntity.Data.CDUE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_DESC", DbType.String, 200, poNewEntity.Data.CTRANS_DESC);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 50, poNewEntity.Data.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@NLCURRENCY_BASE_RATE", DbType.Int32, 50, poNewEntity.Data.NLBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NLCURRENCY_RATE", DbType.Int32, 50, poNewEntity.Data.NLCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NBCURRENCY_BASE_RATE", DbType.Int32, 50, poNewEntity.Data.NBBASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NBCURRENCY_RATE", DbType.Int32, 50, poNewEntity.Data.NBCURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@LTAXABLE", DbType.Boolean, 50, poNewEntity.Data.LTAXABLE);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_ID", DbType.String, 50, poNewEntity.Data.CTAX_ID);
                loDb.R_AddCommandParameter(loCmd, "@NTAX_PERCENTAGE", DbType.String, 50, poNewEntity.Data.NTAX_PCT);
                loDb.R_AddCommandParameter(loCmd, "@NTAX_BASE_RATE", DbType.String, 50, poNewEntity.Data.NTAX_BASE_RATE);
                loDb.R_AddCommandParameter(loCmd, "@NTAX_RATE", DbType.String, 50, poNewEntity.Data.NTAX_CURRENCY_RATE);
                loDb.R_AddCommandParameter(loCmd, "@CSOURCE_MODULE", DbType.String, 50, "PM");
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_STATUS", DbType.String, 50, "00");

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_PM_SAVE_TRANS_HD {@Parameters} || R_Saving(Cls) ", loDbParam);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);

                    PMT50610SaveResultDTO loResult = R_Utility.R_ConvertTo<PMT50610SaveResultDTO>(loDataTable).FirstOrDefault();
                    poNewEntity.Data.CREC_ID = loResult.CREC_ID;
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
