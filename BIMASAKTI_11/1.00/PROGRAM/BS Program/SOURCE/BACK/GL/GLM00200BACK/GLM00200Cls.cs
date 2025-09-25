using GLM00200COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data.Common;
using System.Data;
using System.Transactions;
using System.Data.SqlClient;
using System.Diagnostics;
using GLM00200COMMON.Loggers;
using System.Reflection;

namespace GLM00200BACK
{
    public class GLM00200Cls
    {
        //var
        private RSP_GL_SAVE_RECURRING_JRNResources.Resources_Dummy_Class _saveRecurringRsc = new();
        private RSP_GL_PROCESS_RECURRINGResources.Resources_Dummy_Class _processRecurringRsc = new();
        private LoggerGLM00200 _logger;
        private readonly ActivitySource _activitySource;
        
        //methods
        public GLM00200Cls()
        {
            _logger = LoggerGLM00200.R_GetInstanceLogger();
            _activitySource = GLM00200Activity.R_GetInstanceActivitySource();
        }
        public JournalDTO GetRecurringJrnRecord(JournalDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            JournalDTO loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GL_GET_RECURRING_JRN";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CJRN_ID);
                loDB.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<JournalDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);

            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        public JournalDTO SaveRecurringJrn(JournalParamDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            JournalDTO loRtn = null;

            try
            {
                using (TransactionScope transactionScope = new(TransactionScopeOption.Required))
                {
                    SavingRecurringJrnDB(poNewEntity, poCRUDMode);

                    transactionScope.Complete();
                }

                loRtn = GetRecurringJrnRecord(poNewEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        private void SavingRecurringJrnDB(JournalParamDTO poNewEntity, eCRUDMode poCRUDMode)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            R_Db loDB = null;
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);

                //Bulk Insert Data
                lcQuery = @"CREATE TABLE #GLM0200_JOURNAL_DETAIL 
                            (
                                CGLACCOUNT_NO   VARCHAR(20),
                                CCENTER_CODE    VARCHAR(10),
                                CDBCR           CHAR(1),
                                NAMOUNT         NUMERIC(19, 2),
                                CDETAIL_DESC    NVARCHAR(200),
                                CDOCUMENT_NO    VARCHAR(20),
                                CDOCUMENT_DATE  VARCHAR(8)
                            )";
                _logger.LogDebug($"{lcQuery}");//showdebug
                loDB.SqlExecNonQuery(lcQuery, loConn, false);


                var loMappingDetail = poNewEntity.ListJournalDetail.Select(
                    item => new JournalDetailMappingDTO
                    {
                        CGLACCOUNT_NO = item.CGLACCOUNT_NO,
                        CCENTER_CODE = item.CCENTER_CODE,
                        CDBCR = item.CDBCR.FirstOrDefault(), // Assuming CDBCR is a string
                        NAMOUNT = item.NAMOUNT,
                        CDETAIL_DESC = item.CDETAIL_DESC,
                        CDOCUMENT_NO = item.CDOCUMENT_NO,
                        CDOCUMENT_DATE = item.CDOCUMENT_DATE
                    }).ToList();
                _logger.LogDebug($"INSERT INTO #GLT0100_JOURNAL_DETAIL VALUES {loMappingDetail}");//log insert
                loDB.R_BulkInsert<JournalDetailMappingDTO>((SqlConnection)loConn, "#GLM0200_JOURNAL_DETAIL", loMappingDetail);

                lcQuery = "RSP_GL_SAVE_RECURRING_JRN";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);
                loDB.R_AddCommandParameter(loCmd, "@CJRN_ID", DbType.String, 50, poNewEntity.CJRN_ID);
                loDB.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 50, poCRUDMode == eCRUDMode.AddMode ? "NEW" : "EDIT");
                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, RecurringJournalContext.TRANSACTION_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poNewEntity.CREF_NO);

                loDB.R_AddCommandParameter(loCmd, "@CDOC_NO", DbType.String, 50, poNewEntity.CDOC_NO);
                loDB.R_AddCommandParameter(loCmd, "@CDOC_DATE", DbType.String, 50, poNewEntity.CDOC_DATE);
                loDB.R_AddCommandParameter(loCmd, "@IFREQUENCY", DbType.Int32, 50, poNewEntity.IFREQUENCY);
                loDB.R_AddCommandParameter(loCmd, "@IPERIOD", DbType.Int32, 50, poNewEntity.IPERIOD);

                loDB.R_AddCommandParameter(loCmd, "@CSTART_DATE", DbType.String, 50, poNewEntity.CSTART_DATE);
                loDB.R_AddCommandParameter(loCmd, "@CTRANS_DESC", DbType.String, int.MaxValue, poNewEntity.CTRANS_DESC);
                loDB.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 50, poNewEntity.CCURRENCY_CODE);
                loDB.R_AddCommandParameter(loCmd, "@LFIX_RATE", DbType.Boolean, 50, poNewEntity.LFIX_RATE);
                loDB.R_AddCommandParameter(loCmd, "@NLBASE_RATE", DbType.Decimal, 50, poNewEntity.NLBASE_RATE);
                loDB.R_AddCommandParameter(loCmd, "@NLCURRENCY_RATE", DbType.Decimal, 50, poNewEntity.NLCURRENCY_RATE);
                loDB.R_AddCommandParameter(loCmd, "@NBBASE_RATE", DbType.Decimal, 50, poNewEntity.NBBASE_RATE);
                loDB.R_AddCommandParameter(loCmd, "@NBCURRENCY_RATE", DbType.Decimal, 50, poNewEntity.NBCURRENCY_RATE);
                loDB.R_AddCommandParameter(loCmd, "@NPRELIST_AMOUNT", DbType.Decimal, 50, poNewEntity.NPRELIST_AMOUNT);

                try
                {
                    ShowLogDebug(lcQuery, loCmd.Parameters);
                    var loDataTable = loDB.SqlExecQuery(loConn, loCmd, false);
                    var loTempResult = R_Utility.R_ConvertTo<ConvertRecID>(loDataTable).FirstOrDefault();
                    poNewEntity.CJRN_ID = loTempResult.CJRN_ID;
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
                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        public void UpdateRecurringJrnStatus(GLM00200UpdateStatusDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_GL_UPDATE_RECURRING_STATUS";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CAPPROVE_BY", DbType.String, int.MaxValue, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CJRN_ID_LIST", DbType.String, int.MaxValue, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CNEW_STATUS", DbType.String, int.MaxValue, poEntity.CNEW_STATUS);
                loDb.R_AddCommandParameter(loCmd, "@LAUTO_COMMIT", DbType.Boolean, int.MaxValue, poEntity.LAUTO_COMMIT);
                loDb.R_AddCommandParameter(loCmd, "@LUNDO_COMMIT", DbType.Boolean, int.MaxValue, poEntity.LUNDO_COMMIT);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    ShowLogDebug(lcQuery, loCmd.Parameters);
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
                ShowLogError(loException);
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
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loException.ThrowExceptionIfErrors();
        }
        public List<JournalDTO> GetRecurringJrnList(RecurringJournalListParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<JournalDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GL_SEARCH_RECURRING_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);
                loDB.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 20, RecurringJournalContext.TRANSACTION_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParam.CDEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 6, poParam.CPERIOD_YYYYMM);
                loDB.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 3, poParam.CSTATUS);
                loDB.R_AddCommandParameter(loCmd, "@CSEARCH_TEXT", DbType.String, 20, poParam.CSEARCH_TEXT);
                loDB.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 20, R_BackGlobalVar.CULTURE);

                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<JournalDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;

        }
        public List<JournalDetailGridDTO> GetRecurringJrnDtList(RecurringJournalListParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<JournalDetailGridDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GL_GET_RECURRING_DETAIL_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CJRN_ID", DbType.String, 50, poParam.CREC_ID);
                loDB.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<JournalDetailGridDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;

        }
        public CurrencyRateResultDTO GetLastCurrency(CurrencyRateResultDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            CurrencyRateResultDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_LAST_CURRENCY_RATE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 50, string.IsNullOrWhiteSpace(poEntity.CCURRENCY_CODE) ? "" : poEntity.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CRATETYPE_CODE", DbType.String, 50, poEntity.CRATETYPE_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CRATE_DATE", DbType.String, 50, string.IsNullOrWhiteSpace(poEntity.CRATE_DATE) ? "" : poEntity.CRATE_DATE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<CurrencyRateResultDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<JournalDetailActualGridDTO> GetActualJournalList(RecurringJournalListParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<JournalDetailActualGridDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GL_GET_RECURRING_ACTUAL_JRN_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParam.CDEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poParam.CREF_NO);
                loDB.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<JournalDetailActualGridDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;

        }

        //methods-report
        public JournalDTO GLM00200HeaderReport(JournalDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            JournalDTO loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GL_GET_RECURRING_JRN";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);
                loDB.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);

                
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                        x.ParameterName is
                            "@CCOMPANY_ID" or
                            "@CREC_ID" or
                            "@CLANGUAGE_ID" or
                            "@CUSER_ID"
                    )
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

                
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<JournalDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        public List<JournalDetailActualGridDTO> GLM00200DetailListReport(JournalDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<JournalDetailActualGridDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GL_GET_RECURRING_ACTUAL_JRN_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParam.CDEPT_CODE);
                loDB.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poParam.CREF_NO);
                loDB.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<JournalDetailActualGridDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;

        }
        public UploadByte GetBaseHeaderLogoCompany()
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new();
            UploadByte loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();


                var lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 15, R_BackGlobalVar.COMPANY_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                //_LoggerPrint.LogDebug(string.Format("SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO", loDbParam));

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<UploadByte>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                //_LoggerPrint.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public UploadByte GetRecordCompanyName(string pcCompanyId)
        {
            using Activity activity = _activitySource.StartActivity("GetBaseHeaderLogoCompany");
            R_Exception loEx = new();
            string lcQuery = "";
            UploadByte loRtn = new();
            R_Db loDb = new();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            try
            {
                loDb = new R_Db();
                loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                loCmd = loDb.GetCommand();
                lcQuery = $"EXEC RSP_GS_GET_COMPANY_INFO '{pcCompanyId}'";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 15, pcCompanyId);
                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                var loResult = R_Utility.R_ConvertTo<UploadByte>(loDataTable).FirstOrDefault();
                loRtn.CCOMPANY_NAME = loResult.CCOMPANY_NAME;
                loRtn.CDATETIME_NOW = loResult.CDATETIME_NOW;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        //methods-initial
        public CompanyDTO GetVAR_GSM_COMPANY()
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loException = new R_Exception();
            CompanyDTO loResult = null;

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection();
                DbCommand loCmd = loDb.GetCommand();

                string lcQuery = "RSP_GS_GET_COMPANY_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);

                var loRtnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<CompanyDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;

        }
        public GLSysParamDTO GetVAR_GL_SYSTEM_PARAM()
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loException = new R_Exception();
            GLSysParamDTO loResult = null;
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection();
                DbCommand loCmd = loDb.GetCommand();

                string lcQuery = "RSP_GL_GET_SYSTEM_PARAM";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                var loRtnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLSysParamDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }
        public PeriodDTInfoDTO GetPERIOD_DT_INFO(PeriodDTInfoDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loException = new R_Exception();
            PeriodDTInfoDTO loResult = null;
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection();
                DbCommand loCmd = loDb.GetCommand();

                string lcQuery = "RSP_GS_GET_PERIOD_DT_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 50, poParam.CCYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, 50, poParam.CPERIOD_NO);

                var loRtnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PeriodDTInfoDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }
        public IUndoCommitJrnDTO GetIUNDO_COMMIT_JRN()
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);

            R_Exception loException = new R_Exception();
            IUndoCommitJrnDTO loResult = null;
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection();
                DbCommand loCmd = loDb.GetCommand();

                string lcQuery = "RSP_GL_GET_SYSTEM_ENABLE_OPTION_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@COPTION_CODE", DbType.String, 50, "GL014001");

                var loRtnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<IUndoCommitJrnDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }
        public TransCodeDTO GetGSM_TRANSACTION_CODE()
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);

            R_Exception loException = new R_Exception();
            TransCodeDTO loResult = null;
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection();
                DbCommand loCmd = loDb.GetCommand();

                string lcQuery = "RSP_GS_GET_TRANS_CODE_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, RecurringJournalContext.TRANSACTION_CODE);

                var loRtnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<TransCodeDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }
        public PeriodDTO GetGSM_PERIOD()
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loException = new R_Exception();
            PeriodDTO loResult = null;
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection();
                DbCommand loCmd = loDb.GetCommand();

                string lcQuery = "RSP_GS_GET_PERIOD_YEAR_RANGE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 50, "");
                loDb.R_AddCommandParameter(loCmd, "@CMODE", DbType.String, 50, "");

                var loRtnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<PeriodDTO>(loRtnTemp).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        //helper
        private void ShowLogDebug(string pcQuery, DbParameterCollection poParam)
        {
            var paramValues = string.Join(", ", poParam.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'"));
            _logger.LogDebug($"EXEC {pcQuery} {paramValues}");
        }
        private void ShowLogError(Exception ex)
        {
            _logger.LogError(ex);
        }
    }
    internal class ConvertRecID //class helper
    {
        public string CJRN_ID { get; set; }
    }
}
