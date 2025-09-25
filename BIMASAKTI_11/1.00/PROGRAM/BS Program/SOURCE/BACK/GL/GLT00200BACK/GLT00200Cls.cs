using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLT00200COMMON.DTOs.GLT00200;
using System.Xml.Linq;
using System.Data.SqlClient;
using R_CommonFrontBackAPI;
using System.Globalization;
using System.Transactions;
using GLT00200COMMON;

namespace GLT00200BACK
{
    public class GLT00200Cls : R_IBatchProcess
    {
        public GetCompanyDTO GetCompany(InitialProcessParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GetCompanyDTO loResult = new GetCompanyDTO();

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = $"SELECT CCOGS_METHOD, " +
                    $"LENABLE_CENTER_IS, " +
                    $"LENABLE_CENTER_BS, " +
                    $"LPRIMARY_ACCOUNT, " +
                    $"CBASE_CURRENCY_CODE, " +
                    $"CLOCAL_CURRENCY_CODE " +
                    $"FROM GSM_COMPANY (NOLOCK) " +
                    $"WHERE CCOMPANY_ID = @CCOMPANY_ID";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetCompanyDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public GetSystemParamDTO GetSystemParam(InitialProcessParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GetSystemParamDTO loResult = new GetSystemParamDTO();

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = $"EXEC RSP_GL_GET_SYSTEM_PARAM " +
                    $"@CCOMPANY_ID, " +
                    $"@CLANGUAGE_ID";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetSystemParamDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }


        public GetCurrentPeriodStartDateDTO GetCurrentPeriodStartDate(InitialProcessParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GetCurrentPeriodStartDateDTO loResult = new GetCurrentPeriodStartDateDTO();

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = $"SELECT CSTART_DATE " +
                    $"FROM GSM_PERIOD_DT (NOLOCK) " +
                    $"WHERE CCOMPANY_ID= @CCOMPANY_ID " +
                    $"AND CCYEAR= @CCURRENT_PERIOD_YY " +
                    $"AND CPERIOD_NO= @CCURRENT_PERIOD_MM";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENT_PERIOD_YY", DbType.String, 50, poEntity.CCURRENT_PERIOD_YY);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENT_PERIOD_MM", DbType.String, 50, poEntity.CCURRENT_PERIOD_MM);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetCurrentPeriodStartDateDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GetDeptLookUpListDTO> GetDeptLookUpList(InitialProcessParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<GetDeptLookUpListDTO> loResult = new List<GetDeptLookUpListDTO>();

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = $"EXEC RSP_GS_GET_DEPT_LOOKUP_LIST " +
                    $"@CCOMPANY_ID, " +
                    $"@CUSER_ID";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetDeptLookUpListDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public GetSoftPeriodStartDateDTO GetSoftPeriodStartDate(InitialProcessParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GetSoftPeriodStartDateDTO loResult = new GetSoftPeriodStartDateDTO();

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = $"SELECT CSTART_DATE " +
                    $"FROM GSM_PERIOD_DT (NOLOCK) " +
                    $"WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                    $"AND CCYEAR = @CCYEAR " +
                    $"AND CPERIOD_NO = @CPERIOD_NO";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCYEAR", DbType.String, 50, poEntity.CCYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, 50, poEntity.CPERIOD_NO);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetSoftPeriodStartDateDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public GetUndoCommitJrnDTO GetUndoCommitJrn(InitialProcessParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GetUndoCommitJrnDTO loResult = new GetUndoCommitJrnDTO();

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = $"SELECT IOPTION " +
                    $"FROM GLM_SYSTEM_ENABLE_OPTION (NOLOCK) " +
                    $"WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                    $"AND COPTION_CODE='GL014001'";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetUndoCommitJrnDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public GetTransactionCodeDTO GetTransactionCode(InitialProcessParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GetTransactionCodeDTO loResult = new GetTransactionCodeDTO();

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = $"SELECT LINCREMENT_FLAG, " +
                    $"LAPPROVAL_FLAG " +
                    $"FROM GSM_TRANSACTION_CODE (NOLOCK) " +
                    $"WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                    $"AND CTRANSACTION_CODE='000000'";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetTransactionCodeDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }
        
        public GetPeriodDTO GetPeriod(InitialProcessParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GetPeriodDTO loResult = new GetPeriodDTO();

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = $"SELECT IMIN_YEAR=CAST(MIN(CYEAR) AS INT), " +
                    $"IMAX_YEAR=CAST(MAX(CYEAR) AS INT) " +
                    $"FROM GSM_PERIOD (NOLOCK) " +
                    $"WHERE CCOMPANY_ID = @CCOMPANY_ID";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetPeriodDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }


        public ImportJournalSaveResultDTO GetErrorCount(string pcCompanyId, string pcUserId, string pcKeyGuid)
        {
            var loEx = new R_Exception();
            var lcQuery = "";
            var loDb = new R_Db();
            ImportJournalSaveResultDTO loResult = null;

            try
            {
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                lcQuery = "EXECUTE RSP_ConvertXMLToTable @CCOMPANY_ID, @CUSER_ID, @CKEY_GUID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, pcCompanyId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, pcUserId);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, pcKeyGuid);

                var loDataTableResult = loDb.SqlExecQuery(loConn, loCmd, false);

                loResult = R_Utility.R_ConvertTo<ImportJournalSaveResultDTO>(loDataTableResult).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GLT00200DetailDTO> ImportJournal(ImportJournalParameterDTO poEntity)
        {
            var loDb = new R_Db();
            var loConn = loDb.GetConnection();
            var loCmd = loDb.GetCommand();
            var loEx = new R_Exception();
            var lcQuery = "";
            List<GLT00200DetailDTO> loResult = new List<GLT00200DetailDTO>();

            try
            {
                using (var transScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    List<BulkInsertParameterDTO> loParam = poEntity.DetailData.Select(x => new BulkInsertParameterDTO
                    {
                        CPROCESS_ID = poEntity.CPROCESS_ID,
                        CCOMPANY_ID = poEntity.CCOMPANY_ID,
                        CDEPT_CODE = poEntity.HeaderData.CDEPT_CODE,
                        CTRANS_CODE = "000000",
                        CREF_NO = poEntity.HeaderData.CREF_NO,
                        CREF_DATE = (Convert.ToDateTime(poEntity.HeaderData.CREF_DATE)).ToString("yyyyMMdd"),
                        CDOC_NO = poEntity.HeaderData.CDOC_NO,
                        CDOC_DATE = (Convert.ToDateTime(poEntity.HeaderData.CDOC_DATE)).ToString("yyyyMMdd"),
                        CTRANS_DESC = poEntity.HeaderData.CTRANS_DESC,
                        CCURRENCY_CODE = poEntity.HeaderData.CCURRENCY_CODE,
                        NLBASE_RATE = poEntity.HeaderData.NLBASE_RATE,
                        NLCURRENCY_RATE = poEntity.HeaderData.NLCURRENCY_RATE,
                        NBBASE_RATE = poEntity.HeaderData.NBBASE_RATE,
                        NBCURRENCY_RATE = poEntity.HeaderData.NBCURRENCY_RATE,
                        CGLACCOUNT_NO = x.CGLACCOUNT_NO,
                        CGLACCOUNT_NAME = x.CGLACCOUNT_NAME,
                        CCENTER_CODE = x.CCENTER_CODE,
                        CDBCR = x.CDBCR,
                        NTRANS_AMOUNT = x.NTRANS_AMOUNT,
                        NLTRANS_AMOUNT = 0,
                        NBTRANS_AMOUNT = 0,
                        CDETAIL_DESC = x.CDETAIL_DESC,
                        CDOCUMENT_NO = x.CDOCUMENT_NO,
                        CDOCUMENT_DATE = x.CDOCUMENT_DATE.ToString("yyyyMMdd"),
                    }).ToList();

                    lcQuery = $"CREATE TABLE #GLT00200_JOURNAL " +
                        $"(CPROCESS_ID VARCHAR(50) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CPROCESS_ID DEFAULT(''), " +
                        $"CCOMPANY_ID VARCHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CCOMPANY_ID DEFAULT(''), " +
                        $"CDEPT_CODE VARCHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDEPT_CODE DEFAULT(''), " +
                        $"CTRANS_CODE VARCHAR(10) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CTRANS_CODE DEFAULT(''), " +
                        $"CREF_NO VARCHAR(30) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CREF_NO DEFAULT(''), " +
                        $"CREF_DATE CHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CREF_DATE DEFAULT(''), " +
                        $"CDOC_NO VARCHAR(30) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDOC_NO DEFAULT(''), " +
                        $"CDOC_DATE CHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDOC_DATE DEFAULT(''), " +
                        $"CTRANS_DESC NVARCHAR(200) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CTRANS_DESC DEFAULT(''), " +
                        $"CCURRENCY_CODE CHAR(3) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CCURRENCY_CODE DEFAULT(''), " +
                        $"NLBASE_RATE NUMERIC(20, 6) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NLBASE_RATE DEFAULT(1), " +
                        $"NLCURRENCY_RATE NUMERIC(20, 6) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NLCURRENCY_RATE DEFAULT(1), " +
                        $"NBBASE_RATE NUMERIC(20, 6) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NBBASE_RATE DEFAULT(1), " +
                        $"NBCURRENCY_RATE NUMERIC(20, 6) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NBCURRENCY_RATE DEFAULT(1), " +
                        $"CGLACCOUNT_NO VARCHAR(20) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CGLACCOUNT_NO DEFAULT(''), " +
                        $"CGLACCOUNT_NAME NVARCHAR(60) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CGLACCOUNT_NAME DEFAULT(''), " +
                        $"CCENTER_CODE VARCHAR(10) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CCENTER_CODE DEFAULT(''), " +
                        $"CDBCR CHAR(1) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDBCR DEFAULT(''), " +
                        $"NTRANS_AMOUNT NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NTRANS_AMOUNT DEFAULT(0), " +
                        $"NLTRANS_AMOUNT NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NLTRANS_AMOUNT DEFAULT(0), " +
                        $"NBTRANS_AMOUNT NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NBTRANS_AMOUNT DEFAULT(0), " +
                        $"CDETAIL_DESC NVARCHAR(200) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDETAIL_DESC DEFAULT(''), " +
                        $"CDOCUMENT_NO VARCHAR(20) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDOCUMENT_NO DEFAULT(''), " +
                        $"CDOCUMENT_DATE CHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDOCUMENT_DATE DEFAULT('')) ";

                    loDb.SqlExecNonQuery(lcQuery, loConn, false);

                    loDb.R_BulkInsert<BulkInsertParameterDTO>((SqlConnection)loConn, "#GLT00200_JOURNAL", loParam);

                    //Get Calculated Journal Detail List
                    lcQuery = $"EXEC RSP_GL_GET_IMPORT_JOURNAL_DETAIL_LIST " +
                        $"@CPROCESS_ID, " +
                        $"@CLANGUAGE_ID";

                    loDb.R_AddCommandParameter(loCmd, "@CPROCESS_ID", DbType.String, 50, poEntity.CPROCESS_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

                    loCmd.CommandText = lcQuery;

                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                    loResult = R_Utility.R_ConvertTo<GLT00200DetailDTO>(loDataTable).ToList();

                    transScope.Complete();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
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
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public ImportJournalSaveResultDTO SaveImportJournal(ImportJournalSaveParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            ImportJournalSaveResultDTO loResult = new ImportJournalSaveResultDTO();
            DbConnection loConn = loDb.GetConnection();
            string lcQuery = "";

            try
            {
                using (var transScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    List<BulkInsertParameterDTO> loParam = poEntity.DetailData.Select(x => new BulkInsertParameterDTO
                    {
                        CPROCESS_ID = poEntity.CPROCESS_ID,
                        CCOMPANY_ID = poEntity.CCOMPANY_ID,
                        CDEPT_CODE = poEntity.HeaderData.CDEPT_CODE,
                        CTRANS_CODE = "000000",
                        CREF_NO = poEntity.HeaderData.CREF_NO,
                        CREF_DATE = (Convert.ToDateTime(poEntity.HeaderData.CREF_DATE)).ToString("yyyyMMdd"),
                        CDOC_NO = poEntity.HeaderData.CDOC_NO,
                        CDOC_DATE = (Convert.ToDateTime(poEntity.HeaderData.CDOC_DATE)).ToString("yyyyMMdd"),
                        CTRANS_DESC = poEntity.HeaderData.CTRANS_DESC,
                        CCURRENCY_CODE = poEntity.HeaderData.CCURRENCY_CODE,
                        NLBASE_RATE = poEntity.HeaderData.NLBASE_RATE,
                        NLCURRENCY_RATE = poEntity.HeaderData.NLCURRENCY_RATE,
                        NBBASE_RATE = poEntity.HeaderData.NBBASE_RATE,
                        NBCURRENCY_RATE = poEntity.HeaderData.NBCURRENCY_RATE,
                        CGLACCOUNT_NO = x.CGLACCOUNT_NO,
                        CGLACCOUNT_NAME = x.CGLACCOUNT_NAME,
                        CCENTER_CODE = x.CCENTER_CODE,
                        CDBCR = x.CDBCR,
                        NTRANS_AMOUNT = x.NTRANS_AMOUNT,
                        NLTRANS_AMOUNT = 0,
                        NBTRANS_AMOUNT = 0,
                        CDETAIL_DESC = x.CDETAIL_DESC,
                        CDOCUMENT_NO = x.CDOCUMENT_NO,
                        CDOCUMENT_DATE = x.CDOCUMENT_DATE.ToString("yyyyMMdd"),
                    }).ToList();

                    lcQuery = $"CREATE TABLE #GLT00200_JOURNAL " +
                        $"(CPROCESS_ID VARCHAR(50) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CPROCESS_ID DEFAULT(''), " +
                        $"CCOMPANY_ID VARCHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CCOMPANY_ID DEFAULT(''), " +
                        $"CDEPT_CODE VARCHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDEPT_CODE DEFAULT(''), " +
                        $"CTRANS_CODE VARCHAR(10) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CTRANS_CODE DEFAULT(''), " +
                        $"CREF_NO VARCHAR(30) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CREF_NO DEFAULT(''), " +
                        $"CREF_DATE CHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CREF_DATE DEFAULT(''), " +
                        $"CDOC_NO VARCHAR(30) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDOC_NO DEFAULT(''), " +
                        $"CDOC_DATE CHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDOC_DATE DEFAULT(''), " +
                        $"CTRANS_DESC NVARCHAR(200) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CTRANS_DESC DEFAULT(''), " +
                        $"CCURRENCY_CODE CHAR(3) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CCURRENCY_CODE DEFAULT(''), " +
                        $"NLBASE_RATE NUMERIC(20, 6) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NLBASE_RATE DEFAULT(1), " +
                        $"NLCURRENCY_RATE NUMERIC(20, 6) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NLCURRENCY_RATE DEFAULT(1), " +
                        $"NBBASE_RATE NUMERIC(20, 6) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NBBASE_RATE DEFAULT(1), " +
                        $"NBCURRENCY_RATE NUMERIC(20, 6) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NBCURRENCY_RATE DEFAULT(1), " +
                        $"CGLACCOUNT_NO VARCHAR(20) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CGLACCOUNT_NO DEFAULT(''), " +
                        $"CGLACCOUNT_NAME NVARCHAR(60) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CGLACCOUNT_NAME DEFAULT(''), " +
                        $"CCENTER_CODE VARCHAR(10) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CCENTER_CODE DEFAULT(''), " +
                        $"CDBCR CHAR(1) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDBCR DEFAULT(''), " +
                        $"NTRANS_AMOUNT NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NTRANS_AMOUNT DEFAULT(0), " +
                        $"NLTRANS_AMOUNT NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NLTRANS_AMOUNT DEFAULT(0), " +
                        $"NBTRANS_AMOUNT NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NBTRANS_AMOUNT DEFAULT(0), " +
                        $"CDETAIL_DESC NVARCHAR(200) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDETAIL_DESC DEFAULT(''), " +
                        $"CDOCUMENT_NO VARCHAR(20) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDOCUMENT_NO DEFAULT(''), " +
                        $"CDOCUMENT_DATE CHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDOCUMENT_DATE DEFAULT('')) ";

                    loDb.SqlExecNonQuery(lcQuery, loConn, false);

                    loDb.R_BulkInsert<BulkInsertParameterDTO>((SqlConnection)loConn, "#GLT00200_JOURNAL", loParam);

                    lcQuery = $"EXEC RSP_GL_IMPORT_JOURNAL " +
                        $"@CCOMPANY_ID, " +
                        $"@CUSER_ID, " +
                        $"@CPROCESS_ID";

                    DbCommand loCmd = loDb.GetCommand();
                    loCmd.CommandText = lcQuery;

                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CPROCESS_ID", DbType.String, 50, poEntity.CPROCESS_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                    loResult = R_Utility.R_ConvertTo<ImportJournalSaveResultDTO>(loDataTable).FirstOrDefault();

                    transScope.Complete();
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
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
            }

            loException.ThrowExceptionIfErrors();
            return loResult;
        }   

        public List<ImportJournalErrorDTO> GetImportJournalErrorList(ImportJournalErrorParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = loDb.GetConnection();
            List<ImportJournalErrorDTO> loResult = new List<ImportJournalErrorDTO>();

            try
            {
                string lcQuery = $"EXEC RSP_GL_GET_IMPORT_JOURNAL_ERROR_LIST @CPROCESS_ID, @CLANGUAGE_ID";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CPROCESS_ID", DbType.String, 50, poEntity.CPROCESS_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);
                
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<ImportJournalErrorDTO>(loDataTable).ToList();

                loResult = loResult.Select(i => new ImportJournalErrorDTO()
                {
                    INO = i.INO,
                    CNO = i.INO.ToString(),
                    CERROR_MSG = i.CERROR_MSG
                }).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
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
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            string lcCmd;
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = loDb.GetConnection();
            var loCmd = loDb.GetCommand();
            Dictionary<string, string> loMapping = new Dictionary<string, string>();
            ImportJournalSaveResultDTO loResult = new ImportJournalSaveResultDTO();
            int count = 1;

            try
            {
                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<ImportJournalSaveParameterDTO>(poBatchProcessPar.BigObject);

                List<BulkInsertParameterDTO> loParam = loTempObject.DetailData.Select(x => new BulkInsertParameterDTO
                {
                    CPROCESS_ID = poBatchProcessPar.Key.KEY_GUID,
                    CCOMPANY_ID = poBatchProcessPar.Key.COMPANY_ID,
                    CDEPT_CODE = loTempObject.HeaderData.CDEPT_CODE,
                    CTRANS_CODE = "000000",
                    CREF_NO = loTempObject.HeaderData.CREF_NO,
                    CREF_DATE = (Convert.ToDateTime(loTempObject.HeaderData.CREF_DATE)).ToString("yyyyMMdd"),
                    CDOC_NO = loTempObject.HeaderData.CDOC_NO,
                    CDOC_DATE = (Convert.ToDateTime(loTempObject.HeaderData.CDOC_DATE)).ToString("yyyyMMdd"),
                    CTRANS_DESC = loTempObject.HeaderData.CTRANS_DESC,
                    CCURRENCY_CODE = loTempObject.HeaderData.CCURRENCY_CODE,
                    NLBASE_RATE = loTempObject.HeaderData.NLBASE_RATE,
                    NLCURRENCY_RATE = loTempObject.HeaderData.NLCURRENCY_RATE,
                    NBBASE_RATE = loTempObject.HeaderData.NBBASE_RATE,
                    NBCURRENCY_RATE = loTempObject.HeaderData.NBCURRENCY_RATE,
                    CGLACCOUNT_NO = x.CGLACCOUNT_NO,
                    CGLACCOUNT_NAME = x.CGLACCOUNT_NAME,
                    CCENTER_CODE = x.CCENTER_CODE,
                    CDBCR = x.CDBCR,
                    NTRANS_AMOUNT = x.NTRANS_AMOUNT,
                    NLTRANS_AMOUNT = 0,
                    NBTRANS_AMOUNT = 0,
                    CDETAIL_DESC = x.CDETAIL_DESC,
                    CDOCUMENT_NO = x.CDOCUMENT_NO,
                    CDOCUMENT_DATE = x.CDOCUMENT_DATE.ToString("yyyyMMdd"),
                }).ToList();

                using (var TransScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    lcQuery = $"CREATE TABLE #GLT00200_JOURNAL " +
                        $"(CPROCESS_ID VARCHAR(50) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CPROCESS_ID DEFAULT(''), " +
                        $"CCOMPANY_ID VARCHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CCOMPANY_ID DEFAULT(''), " +
                        $"CDEPT_CODE VARCHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDEPT_CODE DEFAULT(''), " +
                        $"CTRANS_CODE VARCHAR(10) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CTRANS_CODE DEFAULT(''), " +
                        $"CREF_NO VARCHAR(30) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CREF_NO DEFAULT(''), " +
                        $"CREF_DATE CHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CREF_DATE DEFAULT(''), " +
                        $"CDOC_NO VARCHAR(30) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDOC_NO DEFAULT(''), " +
                        $"CDOC_DATE CHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDOC_DATE DEFAULT(''), " +
                        $"CTRANS_DESC NVARCHAR(200) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CTRANS_DESC DEFAULT(''), " +
                        $"CCURRENCY_CODE CHAR(3) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CCURRENCY_CODE DEFAULT(''), " +
                        $"NLBASE_RATE NUMERIC(20, 6) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NLBASE_RATE DEFAULT(1), " +
                        $"NLCURRENCY_RATE NUMERIC(20, 6) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NLCURRENCY_RATE DEFAULT(1), " +
                        $"NBBASE_RATE NUMERIC(20, 6) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NBBASE_RATE DEFAULT(1), " +
                        $"NBCURRENCY_RATE NUMERIC(20, 6) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NBCURRENCY_RATE DEFAULT(1), " +
                        $"CGLACCOUNT_NO VARCHAR(20) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CGLACCOUNT_NO DEFAULT(''), " +
                        $"CGLACCOUNT_NAME NVARCHAR(60) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CGLACCOUNT_NAME DEFAULT(''), " +
                        $"CCENTER_CODE VARCHAR(10) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CCENTER_CODE DEFAULT(''), " +
                        $"CDBCR CHAR(1) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDBCR DEFAULT(''), " +
                        $"NTRANS_AMOUNT NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NTRANS_AMOUNT DEFAULT(0), " +
                        $"NLTRANS_AMOUNT NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NLTRANS_AMOUNT DEFAULT(0), " +
                        $"NBTRANS_AMOUNT NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NBTRANS_AMOUNT DEFAULT(0), " +
                        $"CDETAIL_DESC NVARCHAR(200) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDETAIL_DESC DEFAULT(''), " +
                        $"CDOCUMENT_NO VARCHAR(20) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDOCUMENT_NO DEFAULT(''), " +
                        $"CDOCUMENT_DATE CHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDOCUMENT_DATE DEFAULT('')) ";

                    loDb.SqlExecNonQuery(lcQuery, loConn, false);

                    loDb.R_BulkInsert<BulkInsertParameterDTO>((SqlConnection)loConn, "#GLT00200_JOURNAL", loParam);

                    lcQuery = $"EXEC RSP_GL_IMPORT_JOURNAL " +
                        $"@CCOMPANY_ID, " +
                        $"@CUSER_ID, " +
                        $"@CPROCESS_ID";

                    loCmd.CommandText = lcQuery;

                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CPROCESS_ID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);
                    loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);

                    R_ExternalException.R_SP_Init_Exception(loConn);

                    try
                    {
                        var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                        loResult = R_Utility.R_ConvertTo<ImportJournalSaveResultDTO>(loDataTable).FirstOrDefault();
                    }
                    catch (Exception ex)
                    {
                        loException.Add(ex);
                    }

                    loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));

                    
                    lcQuery = $"CREATE TABLE #SAVE_JOURNAL " +
                        $"(IERROR_COUNT INT NOT NULL);";

                    loDb.SqlExecNonQuery(lcQuery, loConn, false);

                    lcQuery = $"INSERT INTO #SAVE_JOURNAL VALUES (@IERROR_COUNT);";
                    loDb.R_AddCommandParameter(loCmd, "@IERROR_COUNT", DbType.Int64, 50, loResult.IERROR_COUNT);

                    loCmd.CommandText = lcQuery;

                    loDb.SqlExecNonQuery(loConn, loCmd, false);

                    lcQuery = $"EXEC RSP_ConvertTableToXML " +
                        $"@CCOMPANY_ID, " +
                        $"@CUSER_ID, " +
                        $"@KEY_GUID, " +
                        $"'#SAVE_JOURNAL', " +
                        $"1";

                    loCmd.CommandText = lcQuery;
                    loDb.R_AddCommandParameter(loCmd, "@KEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);/*
                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CPROCESS_ID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);
                    loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);*/

                    loDb.SqlExecNonQuery(loConn, loCmd, false);

                    TransScope.Complete();
                }
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
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
            }

            loException.ThrowExceptionIfErrors();
        }
    }
}
