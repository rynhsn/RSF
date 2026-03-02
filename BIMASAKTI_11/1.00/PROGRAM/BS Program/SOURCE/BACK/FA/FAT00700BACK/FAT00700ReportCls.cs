using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using FAT00700BackResources;
using FAT00700Common.DTOs;
using FAT00700Common.Print;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace FAT00700Back
{
    /// <summary>
    /// Report class for FAT00700 - FA Transaction Report
    /// Handles report data generation for FA transactions
    /// </summary>
    public class FAT00700ReportCls
    {
        private readonly Resources_Dummy_Class loRsp = new();
        private readonly LoggerFAT00700 _logger;
        private readonly ActivitySource _activitySource;

        public FAT00700ReportCls()
        {
            _logger = LoggerFAT00700.R_GetInstanceLogger();
            _activitySource = FAT00700Activity.R_GetInstanceActivitySource();
        }

        /// <summary>
        /// Get company logo for report header
        /// </summary>
        /// <param name="poEntity">Parameter containing company ID</param>
        /// <returns>Parameter DTO with logo byte array</returns>
        /// TODO: Create FAT00700ParameterPrintLogoResultDTO in Common/Print folder
        public object GetBaseHeaderLogoCompany(object poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetBaseHeaderLogoCompany");
            var loEx = new R_Exception();
            object loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();

                var lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                
                // TODO: Update parameter name when DTO is created
                // loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 15, poEntity.CCOMPANY_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _logger.LogDebug("SELECT dbo.RFN_GET_COMPANY_LOGO({@CCOMPANY_ID}) as CLOGO", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                // TODO: Update return type when DTO is created
                // loResult = R_Utility.R_ConvertTo<FAT00700ParameterPrintLogoResultDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        /// <summary>
        /// Get print data from stored procedure
        /// </summary>
        /// <param name="poEntity">Parameter containing report filters</param>
        /// <returns>List of print data records</returns>
        /// TODO: Find the correct stored procedure name (e.g., RSP_FA_PRINT_TRANSACTION_HD or similar)
        /// TODO: Create FAT00700ResultSPPrintDTO if different from FAT00700PrintDataDTO
        public List<FAT00700PrintDataDTO> GetPrintResultSP(object poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetPrintResultSP");
            var loEx = new R_Exception();
            List<FAT00700PrintDataDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();

                // TODO: Replace with actual stored procedure name
                var lcQuery = "RSP_FA_PRINT_TRANSACTION_HD"; // TODO: Verify stored procedure name
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                // TODO: Update parameter names when DTO is created
                // loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                // loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                // loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poEntity.CTRANSACTION_CODE);
                // loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poEntity.CREFERENCE_NO);
                // loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poEntity.CASSET_CODE);
                // loDb.R_AddCommandParameter(loCmd, "@CLANGID", DbType.String, 50, poEntity.CLANGID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _logger.LogDebug("EXEC {StoredProcedure} {@poParameter}", lcQuery, loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<FAT00700PrintDataDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        /// <summary>
        /// Get report data for FA Transaction Report
        /// Main method called by report controller
        /// </summary>
        /// <param name="poParameter">Report parameters</param>
        /// <returns>List of report data records</returns>
        public List<FAT00700PrintDataDTO> GetReportData(GetReportDataParameterDTO poParameter)
        {
            string lcMethod = nameof(GetReportData);
            using var activity = _activitySource.StartActivity(lcMethod);
            _logger.LogInfo("START method {MethodName}", lcMethod);

            var loEx = new R_Exception();
            var loRtn = new List<FAT00700PrintDataDTO>();
            var loDb = new R_Db();

            try
            {
                using DbConnection loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                using DbCommand loCmd = loDb.GetCommand();

                loCmd.Parameters.Clear();
                
                // TODO: Replace with actual stored procedure name
                // Check net4 version or database for correct stored procedure name
                var lcQuery = "RSP_FA_PRINT_TRANSACTION_HD"; // TODO: Verify stored procedure name
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGID", DbType.String, 50, poParameter.CLANGID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREFERENCE_NO", DbType.String, 50, poParameter.CREFERENCE_NO);
                loDb.R_AddCommandParameter(loCmd, "@CASSET_CODE", DbType.String, 50, poParameter.CASSET_CODE);
                
                // TODO: Verify if these parameters are needed in the stored procedure
                // loDb.R_AddCommandParameter(loCmd, "@CFROM_DATE", DbType.String, 50, poParameter.CFROM_DATE);
                // loDb.R_AddCommandParameter(loCmd, "@CTO_DATE", DbType.String, 50, poParameter.CTO_DATE);
                // loDb.R_AddCommandParameter(loCmd, "@LPRINT_DETAIL", DbType.Boolean, 0, poParameter.LPRINT_DETAIL);
                // loDb.R_AddCommandParameter(loCmd, "@LPRINT_SUMMARY", DbType.Boolean, 0, poParameter.LPRINT_SUMMARY);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                loRtn = R_Utility.R_ConvertTo<FAT00700PrintDataDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loDb != null)
                    loDb = null;
            }

            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo("END method {MethodName}", lcMethod);
            return loRtn;
        }

        /// <summary>
        /// Get report template list
        /// </summary>
        /// <param name="poParameter">Template parameters</param>
        /// <returns>List of report templates</returns>
        /// TODO: Create FAT00700ReportTemplateParamDTO and FAT00700ReportTemplateDTO if report templates are used
        public List<object> GetReportTemplate(object poParameter)
        {
            string? lcMethod = nameof(GetReportTemplate);
            _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<object>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;

            try
            {
                _logger.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
                loDb = new();

                _logger.LogInfo(
                    string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
                loCommand = loDb.GetCommand();

                _logger.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));
                lcQuery = "RSP_GET_REPORT_TEMPLATE_LIST";

                _logger.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}",
                    lcMethod));
                DbConnection? loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);

                _logger.LogInfo(string.Format(
                    "Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                _logger.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
                
                // TODO: Update parameter names when DTOs are created
                // loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                // loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                // loDb.R_AddCommandParameter(loCommand, "@CPROGRAM_ID", DbType.String, 30, poParameter.CPROGRAM_ID);
                // loDb.R_AddCommandParameter(loCommand, "@CTEMPLATE_ID ", DbType.String, 30, poParameter.CTEMPLATE_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);

                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                _logger.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}",
                    lcMethod));

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);

                _logger.LogInfo(string.Format(
                    "Convert the data in loDataTable to a list objects and assign it to loRtn in Method {0}",
                    lcMethod));

                // TODO: Update return type when DTO is created
                // loReturn = R_Utility.R_ConvertTo<FAT00700ReportTemplateDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            if (loException.Haserror)
                _logger.LogError("{@ErrorObject}", loException.Message);
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("End Method {0}", lcMethod));

            return loReturn!;
        }
    }
}

