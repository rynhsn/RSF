using Global_PMCOMMON.Logs;
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
using Global_PMCOMMON.DTOs.User_Param_Detail;
using Global_PMCOMMON.DTOs.Response.Property;
using System.Reflection.Metadata;
using Global_PMCOMMON.DTOs.Response.Invoice_Type;

namespace Global_PMBACK
{
    public class GlobalFunctionPMCls
    {
        private LoggerGlobalFunctionPM _logger;
        private readonly ActivitySource _activitySource;

        public GlobalFunctionPMCls()
        {
            _logger = LoggerGlobalFunctionPM.R_GetInstanceLogger();
            _activitySource = GlobalFunctionPMActivity.R_GetInstanceActivitySource();
        }

        public GetUserParamDetailDTO GetUserParamDetailDb(GetUserParamDetailParameterDTO poParameter)
        {
            string lcMethodName = nameof(GetUserParamDetailDb);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            var loEx = new R_Exception();
            GetUserParamDetailDTO? loResult = null;
            R_Db loDb;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_PM_GET_USER_PARAM_DETAIL";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCODE", DbType.String, 8, poParameter.CCODE);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GetUserParamDetailDTO>(loReturnTemp).ToList().FirstOrDefault() != null ?
                     R_Utility.R_ConvertTo<GetUserParamDetailDTO>(loReturnTemp).ToList().FirstOrDefault() :
                     new GetUserParamDetailDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult!;
        }
        public List<PropertyDTO> GetPropertyListDb(PropertyParameterDTO poParameter)
        {
            string lcMethodName = nameof(GetPropertyListDb);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            R_Exception loException = new R_Exception();
            List<PropertyDTO>? loReturn = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_PROPERTY_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<PropertyDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }
        public List<InvoiceTypeDTO> GetInvoiceTypeDb(InvoiceTypeParameterDTO poParameter)
        {
            string lcMethodName = nameof(GetInvoiceTypeDb);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _logger.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            R_Exception loException = new R_Exception();
            List<InvoiceTypeDTO>? loReturn = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_GSB_CODE_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CAPPLICATION", DbType.String, 20, poParameter.CCLASS_APPLICATION);
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCLASS_ID", DbType.String, 40, poParameter.CCLASS_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANGUAGE_ID", DbType.String, 2, poParameter.CLANG_ID);
                loDb.R_AddCommandParameter(loCommand, "@CREC_ID_LIST", DbType.String, 50, poParameter.CCLASS_RECID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loReturn = R_Utility.R_ConvertTo<InvoiceTypeDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
            return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
        }

    }
}
