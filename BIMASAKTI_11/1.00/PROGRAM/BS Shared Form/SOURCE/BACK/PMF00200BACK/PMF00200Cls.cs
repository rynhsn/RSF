using System.Data.Common;
using System.Data;
using System.Diagnostics;
using PMF00200COMMON;
using R_BackEnd;
using R_Common;
using PMF00200ReportCommon;
using R_CommonFrontBackAPI;
using System.Transactions;
using System.Globalization;

namespace PMF00200BACK
{
    public class PMF00200Cls 
    {
        private LoggerPMF00200 _Logger;
        private readonly ActivitySource _activitySource;
        public PMF00200Cls()
        {
            _Logger = LoggerPMF00200.R_GetInstanceLogger();
            _activitySource = PMF00200ActivitySourceBase.R_GetInstanceActivitySource();
        }

        #region Back SP
        public async Task<PMF00200GSCompanyInfoDTO> GetCompanyInfoRecord()
        {
            using Activity activity = _activitySource.StartActivity("GetCompanyInfoRecord");
            var loEx = new R_Exception();
            PMF00200GSCompanyInfoDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = await loDb.GetConnectionAsync();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_COMPANY_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_COMPANY_INFO {@poParameter}", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMF00200GSCompanyInfoDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<List<PMF00200ReportTemplateDTO>> GetReportTempateList(PMF00200InputParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetReportTempateList");
            var loEx = new R_Exception();
            List<PMF00200ReportTemplateDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = await loDb.GetConnectionAsync();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GET_REPORT_TEMPLATE_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, string.IsNullOrWhiteSpace(poEntity.PARAM_PROPERTY_ID) ? "" : poEntity.PARAM_PROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROGRAM_ID", DbType.String, 50, string.IsNullOrWhiteSpace(poEntity.PARAM_CALLER_ID) ? "" : poEntity.PARAM_CALLER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTEMPLATE_ID", DbType.String, 50, "");

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GET_REPORT_TEMPLATE_LIST {@poParameter}", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMF00200ReportTemplateDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<PMF00200DTO> GetJournalDisplay(PMF00200InputParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetJournalDisplay");
            var loEx = new R_Exception();
            PMF00200DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = await loDb.GetConnectionAsync();
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_PM_GET_CA_WT_CUST_RECEIPT";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, int.MaxValue, string.IsNullOrWhiteSpace(poEntity.PARAM_RECEIPT_ID) ? "" : poEntity.PARAM_RECEIPT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, "");

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_CA_WT_CUST_RECEIPT {@poParameter}", loDbParam);

                var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMF00200DTO>(loDataTable).FirstOrDefault();
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

        #region Report SP
        public PMF00200GSCompanyInfoDTO GetCompanyInfoPrint(string pcCompanyId)
        {
            using Activity activity = _activitySource.StartActivity("GetCompanyInfoPrint");
            var loEx = new R_Exception();
            PMF00200GSCompanyInfoDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_COMPANY_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, pcCompanyId);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_COMPANY_INFO {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMF00200GSCompanyInfoDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public PMF00200PrintResult GetBaseHeaderLogoCompany()
        {
            using Activity activity = _activitySource.StartActivity("GetBaseHeaderLogoCompany");
            var loEx = new R_Exception();
            PMF00200PrintResult loResult = null;
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
                loResult = R_Utility.R_ConvertTo<PMF00200PrintResult>(loDataTable).FirstOrDefault();

                lcQuery = "EXEC RSP_GS_GET_COMPANY_INFO @CCOMPANY_ID";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                //Debug Logs
                _Logger.LogDebug(string.Format("EXEC RSP_GS_GET_COMPANY_INFO '@CCOMPANY_ID'", loDbParam));
                loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                var loCompanyNameResult = R_Utility.R_ConvertTo<PMF00200PrintResult>(loDataTable).FirstOrDefault();

                loResult.CCOMPANY_NAME = loCompanyNameResult.CCOMPANY_NAME;
                loResult.CDATETIME_NOW = loCompanyNameResult.CDATETIME_NOW;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
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
        public PMF00200HeaderPrintDTO GetJournalReport(PMF00200InputParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetJournalReport");
            var loEx = new R_Exception();
            PMF00200HeaderPrintDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_PM_GET_CA_WT_CUST_RECEIPT";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, int.MaxValue, string.IsNullOrWhiteSpace(poEntity.PARAM_RECEIPT_ID) ? "" : poEntity.PARAM_RECEIPT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, "");

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_GET_CA_WT_CUST_RECEIPT {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMF00200HeaderPrintDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public PMF00200EmailTemplateDTO GetEmailTemplateBody()
        {
            using Activity activity = _activitySource.StartActivity("GetJournalReport");
            var loEx = new R_Exception();
            PMF00200EmailTemplateDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_EMAIL_TEMPLATE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTEMPLATE_ID", DbType.String, 50, "CUSTOMER_RECEIPT");

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_EMAIL_TEMPLATE {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMF00200EmailTemplateDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<PMF00201DTO> GetAllocReceiptList(string pcCompanyId, string pcLanId, string pcRecId)
        {
            using Activity activity = _activitySource.StartActivity("GetAllocReceiptList");
            var loEx = new R_Exception();
            List<PMF00201DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_REP_GET_RECEIPT_ALLOC_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, pcCompanyId);
                loDb.R_AddCommandParameter(loCmd, "@CRECEIPT_ID", DbType.String, int.MaxValue, pcRecId);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, pcLanId);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_REP_GET_RECEIPT_ALLOC_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMF00201DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<PMF00202DTO> GetJournalReceiptList(string pcCompanyId, string pcLanId, string pcRecId)
        {
            using Activity activity = _activitySource.StartActivity("GetJournalReceiptList");
            var loEx = new R_Exception();
            List<PMF00202DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_PM_REP_GET_RECEIPT_JOURNAL_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, pcCompanyId);
                loDb.R_AddCommandParameter(loCmd, "@CRECEIPT_ID", DbType.String, int.MaxValue, pcRecId);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, pcLanId);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_PM_REP_GET_RECEIPT_JOURNAL_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PMF00202DTO>(loDataTable).ToList();
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

        #region Send Email
        public string SendEmailProcess(byte[] EmailMessageData, PMF00200InputParameterDTO poEntity)
        {
            _Logger.LogInfo("start SendEmailProcess ");
            string lcRtn = "";

            byte[] loDataInByte;
            R_Exception loException = new R_Exception();

            try
            {
                var loHeaderData = GetJournalReport(poEntity);
                var loTemplateBody = GetEmailTemplateBody();

                using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    lcRtn = SaveEmailProcess(EmailMessageData, loHeaderData, loTemplateBody, poEntity.PARAM_CALLER_ID);
                    transactionScope.Complete();
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            
            _Logger.LogInfo("end SendEmailProcess ");
            loException.ThrowExceptionIfErrors();

            return lcRtn;
        }
        private string SaveEmailProcess(byte[] EmailMessageData, PMF00200HeaderPrintDTO poReceiptData, PMF00200EmailTemplateDTO poBodyData, string pcProgramId)
        {
            _Logger.LogInfo("start SaveEmailProcess ");

            string lcMethodName = nameof(SaveEmailProcess);
            byte[] loDataInByte;
            R_Exception loException = new R_Exception();
            R_EmailEngineBackCommandPar loEmailPar;
            List<R_EmailEngineBackObject> loEmailFiles;
            R_Db loDb = null;
            DbConnection loConn = null;
            string lcRtn = "";
            CultureInfo loCultureInfo = new CultureInfo(R_BackGlobalVar.REPORT_CULTURE);

            try
            {

                loEmailFiles = new List<R_EmailEngineBackObject>
                {
                    new R_EmailEngineBackObject()
                    {
                        FileName = string.Format("{0}_{1}", poReceiptData.CCUST_SUPP_ID, poReceiptData.CREF_NO),
                        FileExtension = Path.GetExtension(".pdf"),
                        FileId = Guid.NewGuid().ToString(),
                        FileData = EmailMessageData
                    }
                };

                _Logger.LogInfo("After assign loEmailFiles", lcMethodName);

                loEmailPar = new R_EmailEngineBackCommandPar()
                {
                    COMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    USER_ID = R_BackGlobalVar.USER_ID,
                    PROGRAM_ID = pcProgramId,
                    Message = new R_EmailEngineMessage()
                    {
                        EMAIL_FROM = poBodyData.CGENERAL_EMAIL_ADDRESS,
                        EMAIL_BODY = string.Format(poBodyData.CTEMPLATE_BODY, poReceiptData.CCUST_SUPP_NAME, poReceiptData.CCURRENCY_CODE, poReceiptData.NTRANS_AMOUNT, poReceiptData.CCUST_SUPP_NAME, poReceiptData.CCURRENCY_CODE, poReceiptData.NTRANS_AMOUNT),
                        EMAIL_SUBJECT = string.Format(R_Utility.R_GetMessage(typeof(PMF00200BackResources.Resources_Dummy_Class), "SubjectEmail", loCultureInfo), poReceiptData.CREF_NO),
                        EMAIL_TO = poReceiptData.CEMAIL,
                        EMAIL_CC = "",
                        EMAIL_BCC = "",
                        FLAG_HTML = true,
                    },
                    Attachments = loEmailFiles,
                };

                _Logger.LogInfo("After assign loEmailPar", lcMethodName);

                loDb = new R_Db();
                var loConnAttr = loDb.GetConnectionAttribute(R_Db.eDbConnectionStringType.ReportConnectionString);
                loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString, loConnAttr);
                lcRtn = R_EmailEngineBack.R_EmailEngineSaveFromBack(loEmailPar, loConn);
                _Logger.LogInfo("After method R_EmailEngineSaveFromBack", lcMethodName);
                _Logger.LogDebug("Email ID: ", lcRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                    {
                        loConn.Close();
                        loConn = null;
                    };
                }
                if (loDb != null)
                {
                    loDb = null;
                }
            }

            _Logger.LogInfo("end SaveEmailProcess ");
            loException.ThrowExceptionIfErrors();

            return lcRtn;
        }
        #endregion
    }
}
