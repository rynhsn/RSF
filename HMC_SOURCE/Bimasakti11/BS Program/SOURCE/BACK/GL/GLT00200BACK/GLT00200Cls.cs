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
using GLT00200COMMON.Loggers;

namespace GLT00200BACK
{
    public class GLT00200Cls : R_IBatchProcess
    {
        private LoggerGLT00200 _logger;
        public GLT00200Cls()
        {
            _logger = LoggerGLT00200.R_GetInstanceLogger();
        }

        public List<GetImportJournalResult> GetSuccessProcess(string pcCompanyId, string pcUserId, string pcKeyGuid)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            List<GetImportJournalResult> loResult = null;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXECUTE RSP_ConvertXMLToTable @CCOMPANY_ID, @CUSER_ID, @CKEY_GUID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, pcCompanyId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, pcUserId);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, pcKeyGuid);

                var loDataTableResult = loDb.SqlExecQuery(loConn, loCmd, false);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXECUTE RSP_ConvertXMLToTable {@Parameters} || GetSuccessProcess(Cls) ", loDbParam);

                loResult = R_Utility.R_ConvertTo<GetImportJournalResult>(loDataTableResult).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public GetCompanyDTO GetCompany(InitialProcessParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GetCompanyDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_COMPANY_INFO @CCOMPANY_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_COMPANY_INFO {@Parameters} || GetCompany(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetCompanyDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public GetSystemParamDTO GetSystemParam(InitialProcessParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GetSystemParamDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GL_GET_SYSTEM_PARAM " +
                    $"@CCOMPANY_ID, " +
                    $"@CLANGUAGE_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GL_GET_SYSTEM_PARAM {@Parameters} || GetSystemParam(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetSystemParamDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }


        public GetCurrentPeriodStartDateDTO GetCurrentPeriodStartDate(InitialProcessParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GetCurrentPeriodStartDateDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_PERIOD_DT_INFO " +
                    $"@CCOMPANY_ID, " +
                    $"@CCURRENT_PERIOD_YY, " +
                    $"@CCURRENT_PERIOD_MM ";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENT_PERIOD_YY", DbType.String, 50, poEntity.CCURRENT_PERIOD_YY);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENT_PERIOD_MM", DbType.String, 50, poEntity.CCURRENT_PERIOD_MM);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_PERIOD_DT_INFO {@Parameters} || GetCurrentPeriodStartDate(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetCurrentPeriodStartDateDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GetDeptLookUpListDTO> GetDeptLookUpList(InitialProcessParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<GetDeptLookUpListDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_DEPT_LOOKUP_LIST " +
                    $"@CCOMPANY_ID, " +
                    $"@CUSER_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_DEPT_LOOKUP_LIST {@Parameters} || GetDeptLookUpList(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetDeptLookUpListDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public GetSoftPeriodStartDateDTO GetSoftPeriodStartDate(InitialProcessParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GetSoftPeriodStartDateDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_PERIOD_DT_INFO " +
                    $"@CCOMPANY_ID, " +
                    $"@CCYEAR, " +
                    $"@CPERIOD_NO ";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCYEAR", DbType.String, 50, poEntity.CCYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, 50, poEntity.CPERIOD_NO);

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

        public GetUndoCommitJrnDTO GetUndoCommitJrn(InitialProcessParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GetUndoCommitJrnDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GL_GET_SYSTEM_ENABLE_OPTION_INFO " +
                    $"@CCOMPANY_ID, " +
                    $"'GL014001'";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;
                
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GL_GET_SYSTEM_ENABLE_OPTION_INFO {@Parameters} || GetUndoCommitJrn(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetUndoCommitJrnDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public GetTransactionCodeDTO GetTransactionCode(InitialProcessParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GetTransactionCodeDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_TRANS_CODE_INFO " +
                    $"@CCOMPANY_ID, " +
                    $"'000000'";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_TRANS_CODE_INFO {@Parameters} || GetTransactionCode(Cls) ", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GetTransactionCodeDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }
        
        public GetPeriodDTO GetPeriod(InitialProcessParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            GetPeriodDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");

                lcQuery = $"EXEC RSP_GS_GET_PERIOD_YEAR_RANGE @CCOMPANY_ID, '', ''";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GS_GET_PERIOD_YEAR_RANGE {@Parameters} || GetPeriod(Cls) ", loDbParam);

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


        public ImportJournalSaveResultDTO GetErrorCount(string pcCompanyId, string pcUserId, string pcKeyGuid)
        {
            R_Exception loException = new R_Exception();
            ImportJournalSaveResultDTO loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "EXECUTE RSP_ConvertXMLToTable @CCOMPANY_ID, @CUSER_ID, @CKEY_GUID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, pcCompanyId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, pcUserId);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, pcKeyGuid);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXECUTE RSP_ConvertXMLToTable {@Parameters} || GetErrorCount(Cls) ", loDbParam);

                var loDataTableResult = loDb.SqlExecQuery(loConn, loCmd, false);

                loResult = R_Utility.R_ConvertTo<ImportJournalSaveResultDTO>(loDataTableResult).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<ImportJournalErrorDTO> GetImportJournalErrorList(ImportJournalErrorParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            List<ImportJournalErrorDTO> loResult = null;
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();
                
                lcQuery = $"EXEC RSP_GL_GET_IMPORT_JOURNAL_ERROR_LIST @CPROCESS_ID, @CLANGUAGE_ID";

                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CPROCESS_ID", DbType.String, 50, poEntity.CPROCESS_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GL_GET_IMPORT_JOURNAL_ERROR_LIST {@Parameters} || GetImportJournalErrorList(Cls) ", loDbParam);

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
            return loResult;
        }

        private async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();/*
            R_Exception loSpException = new R_Exception();*/
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            Dictionary<string, string> loMapping = new Dictionary<string, string>();
            ImportJournalSaveResultDTO loResult = new ImportJournalSaveResultDTO();
            List<BulkInsertParameterDTO> loParam = null;
            int count = 1;

            try
            {
                await Task.Delay(100);
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<ImportJournalSaveParameterDTO>(poBatchProcessPar.BigObject);

                loParam = loTempObject.DetailData.Select(x => new BulkInsertParameterDTO
                {
                    INO = x.SEQ_NO,
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

                lcQuery = $"CREATE TABLE #GLT00200_JOURNAL " +
                    $"(INO INT NOT NULL DEFAULT(0), " +
                    $"CPROCESS_ID VARCHAR(50) NOT NULL DEFAULT(''), " +
                    $"CCOMPANY_ID VARCHAR(8) NOT NULL DEFAULT(''), " +
                    $"CDEPT_CODE VARCHAR(20) NOT NULL DEFAULT(''), " +
                    $"CTRANS_CODE VARCHAR(10) NOT NULL DEFAULT(''), " +
                    $"CREF_NO VARCHAR(30) NOT NULL DEFAULT(''), " +
                    $"CREF_DATE CHAR(8) NOT NULL DEFAULT(''), " +
                    $"CDOC_NO VARCHAR(30) NOT NULL DEFAULT(''), " +
                    $"CDOC_DATE CHAR(8) NOT NULL DEFAULT(''), " +
                    $"CTRANS_DESC NVARCHAR(200) NOT NULL DEFAULT(''), " +
                    $"CCURRENCY_CODE CHAR(3) NOT NULL DEFAULT(''), " +
                    $"NLBASE_RATE NUMERIC(20, 6) NOT NULL DEFAULT(1), " +
                    $"NLCURRENCY_RATE NUMERIC(20, 6) NOT NULL DEFAULT(1), " +
                    $"NBBASE_RATE NUMERIC(20, 6) NOT NULL DEFAULT(1), " +
                    $"NBCURRENCY_RATE NUMERIC(20, 6) NOT NULL DEFAULT(1), " +
                    $"CGLACCOUNT_NO VARCHAR(20) NOT NULL DEFAULT(''), " +
                    $"CGLACCOUNT_NAME NVARCHAR(60) NOT NULL DEFAULT(''), " +
                    $"CCENTER_CODE VARCHAR(10) NOT NULL DEFAULT(''), " +
                    $"CDBCR CHAR(1) NOT NULL DEFAULT(''), " +
                    $"NTRANS_AMOUNT NUMERIC(19, 2) NOT NULL DEFAULT(0), " +
                    $"NLTRANS_AMOUNT NUMERIC(19, 2) NOT NULL DEFAULT(0), " +
                    $"NBTRANS_AMOUNT NUMERIC(19, 2) NOT NULL DEFAULT(0), " +
                    $"CDETAIL_DESC NVARCHAR(200) NOT NULL DEFAULT(''), " +
                    $"CDOCUMENT_NO VARCHAR(20) NOT NULL DEFAULT(''), " +
                    $"CDOCUMENT_DATE CHAR(8) NOT NULL DEFAULT('')) ";

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

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                    .Select(x => x.Value);

                _logger.LogDebug("EXEC RSP_GL_IMPORT_JOURNAL {@Parameters} || _BatchProcess(Cls) ", loDbParam);

                loDb.SqlExecNonQuery(loConn, loCmd, false);

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
                    if (!(loConn.State == ConnectionState.Closed))
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

            if (loException.Haserror)
            {
                string lcCmd = $"EXEC RSP_WriteUploadProcessStatus '{poBatchProcessPar.Key.COMPANY_ID}', " +
                   $"'{poBatchProcessPar.Key.USER_ID}', " +
                   $"'{poBatchProcessPar.Key.KEY_GUID}', " +
                   $"100, '{loException.ErrorList[0].ErrDescp}', 9";

                loDb.SqlExecNonQuery(lcCmd);
            }
        }

        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            try
            {
                if (loDb.R_TestConnection() == false)
                {
                    throw new Exception("Connection Failed");
                }
                var loTask = Task.Run(() =>
                {
                    _BatchProcess(poBatchProcessPar);
                });

                while (!loTask.IsCompleted)
                {
                    Thread.Sleep(100);
                }

                if (loTask.IsFaulted)
                {
                    loException.Add(loTask.Exception.InnerException != null ?
                        loTask.Exception.InnerException :
                        loTask.Exception);

                    goto EndBlock;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }
/*
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();*//*
            R_Exception loSpException = new R_Exception();*//*
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;
            Dictionary<string, string> loMapping = new Dictionary<string, string>();
            ImportJournalSaveResultDTO loResult = new ImportJournalSaveResultDTO();
            List<BulkInsertParameterDTO> loParam = null;
            int count = 1;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<ImportJournalSaveParameterDTO>(poBatchProcessPar.BigObject);

                loParam = loTempObject.DetailData.Select(x => new BulkInsertParameterDTO
                {
                    INO = x.SEQ_NO,
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

                lcQuery = $"CREATE TABLE #GLT00200_JOURNAL " +
                    $"(INO INT NOT NULL DEFAULT(0), " +
                    $"CPROCESS_ID VARCHAR(50) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CPROCESS_ID DEFAULT(''), " +
                    $"CCOMPANY_ID VARCHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CCOMPANY_ID DEFAULT(''), " +
                    $"CDEPT_CODE VARCHAR(20) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDEPT_CODE DEFAULT(''), " +
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
                *//*
                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                    loResult = R_Utility.R_ConvertTo<ImportJournalSaveResultDTO>(loDataTable).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    loSpException.Add(ex);
                }
                *//*

                loDb.SqlExecNonQuery(loConn, loCmd, false);

                *//*
                                loResult = R_Utility.R_ConvertTo<ImportJournalSaveResultDTO>(loDataTable).FirstOrDefault();
                                loSpException.Add(R_ExternalException.R_SP_Get_Exception(loConn));

                                if (loResult.IERROR_COUNT > 0)
                                {
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
                                    loDb.R_AddCommandParameter(loCmd, "@KEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);
                            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                            loDb.R_AddCommandParameter(loCmd, "@CPROCESS_ID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);
                            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);

                                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                                *//*


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
                if (loCmd != null)
                {
                    loCmd.Dispose();
                    loCmd = null;
                }
            }

            loException.ThrowExceptionIfErrors();
        }*/
    }
}
