using PMB04000COMMON.Logs;
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
using PMB04000COMMON.DTO.DTOs;
using PMB04000COMMON.DTO.Utilities;

namespace PMB04000BACK
{
    public class PMB04000Cls
    {
        private readonly LoggerPMB04000? _logger;
        private readonly ActivitySource _activitySource;

        public PMB04000Cls()
        {
            _logger = LoggerPMB04000.R_GetInstanceLogger();
            _activitySource = PMB04000Activity.R_GetInstanceActivitySource();
        }
        public List<PMB04000DTO> GetInvoiceReceiptList(PMB04000ParamDTO poParameter)
        {
            string? lcMethodName = nameof(GetInvoiceReceiptList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));


            R_Exception loException = new();
            List<PMB04000DTO>? loReturn = null;
            string lcQuery;
            DbCommand loCommand;
            R_Db loDb;
            try
            {
                loDb = new();
                DbConnection? loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);
                lcQuery = "RSP_PM_GET_INV_RECEIPT_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTENANT_ID", DbType.String, 20, poParameter.CTENANT_ID);
                loDb.R_AddCommandParameter(loCommand, "@LALL_TENANT", DbType.Boolean, 2, poParameter.LALL_TENANT);
                loDb.R_AddCommandParameter(loCommand, "@CINVOICE_TYPE", DbType.String, 2, poParameter.CINVOICE_TYPE);
                loDb.R_AddCommandParameter(loCommand, "@CPERIOD", DbType.String, 6, poParameter.CPERIOD);
                loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 6, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CLANG_ID", DbType.String, 3, poParameter.CLANG_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PMB04000DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            if (loException.Haserror)
            {
                loException.ThrowExceptionIfErrors();
            }
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }
        public PeriodYearDTO GetPeriodYearRange(PMB04000ParamDTO poParameter)
        {
            string lcMethodName = nameof(GetPeriodYearRange);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            PeriodYearDTO? loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_GS_GET_PERIOD_YEAR_RANGE";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CYEAR", DbType.String, 4, "");
                loDb.R_AddCommandParameter(loCommand, "@CMODE", DbType.String, 10, "");
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                 .Where(x => x != null && x.ParameterName.StartsWith("@"))
                 .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogInfo("Execute query initial process to get year range");
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<PeriodYearDTO>(loReturnTemp).ToList().Any() ?
                           R_Utility.R_ConvertTo<PeriodYearDTO>(loReturnTemp).FirstOrDefault()! : new PeriodYearDTO();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult!;
        }
        public List<TemplateDTO> GetTemplateList(TemplateParamDTO poParameter)
        {
            string lcMethodName = nameof(GetTemplateList);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger!.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new();
            List<TemplateDTO>? loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                R_ExternalException.R_SP_Init_Exception(loConn);
                var lcQuery = "RSP_GET_REPORT_TEMPLATE_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPROGRAM_ID", DbType.String, 30, poParameter.CPROGRAM_ID);
                loDb.R_AddCommandParameter(loCommand, "@CTEMPLATE_ID ", DbType.String, 30, poParameter.CTEMPLATE_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                 .Where(x => x != null && x.ParameterName.StartsWith("@"))
                 .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogInfo("Execute query");
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<List<TemplateDTO>>(loReturnTemp).ToList().Any() ?
                           R_Utility.R_ConvertTo<List<TemplateDTO>>(loReturnTemp).FirstOrDefault()! : new List<TemplateDTO>();
            }

            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult!;
        }
    }
}
