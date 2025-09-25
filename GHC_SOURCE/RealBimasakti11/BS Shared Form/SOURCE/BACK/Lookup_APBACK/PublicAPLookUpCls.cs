using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APCOMMON.DTOs.APL00110;
using Lookup_APCOMMON.DTOs.APL00200;
using Lookup_APCOMMON.DTOs.APL00300;
using Lookup_APCOMMON.DTOs.APL00400;
using Lookup_APCOMMON.DTOs.APL00500;
using Lookup_APCOMMON.DTOs.APL00600;
using Lookup_APCOMMON.DTOs.APL00700;
using Lookup_APCOMMON.Loggers;
using R_BackEnd;
using R_Common;
using APL00100ParameterDTO = Lookup_APCOMMON.DTOs.APL00100.APL00100ParameterDTO;

namespace Lookup_APBACK
{
    public class PublicAPLookUpCls
    {
        private LoggerAPPublicLookup _loggerAp;
        private readonly ActivitySource _activitySource;

        public PublicAPLookUpCls()
        {
            _loggerAp = LoggerAPPublicLookup.R_GetInstanceLogger();
            _activitySource = Lookup_APBACKActivity.R_GetInstanceActivitySource();
        }

        public List<APL00100DTO> SupplierLookup(APL00100ParameterDTO poParameter)
        {
            using var activity = _activitySource.StartActivity(nameof(SupplierLookup));
            R_Exception loException = new R_Exception();
            List<APL00100DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_AP_SEARCH_SUPPLIER_LOOKUP_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 10, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSEARCH_TEXT", DbType.String, 10, poParameter.CSEARCH_TEXT);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParameter.CLANGUAGE_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x.ParameterName ==
                                "@" + poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name)
                    .Select(x => x.Value);
                _loggerAp.LogDebug("EXEC RSP_AP_SEARCH_SUPPLIER_LOOKUP_LIST {@poParameter}", loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<APL00100DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)

            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }


        public List<APL00110DTO> SupplierInfoLookup(APL00110ParameterDTO poParameter)
        {
            using var activity = _activitySource.StartActivity(nameof(SupplierInfoLookup));
            R_Exception loException = new R_Exception();
            List<APL00110DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_AP_GET_SUPPLIER_INFO_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 10, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_ID", DbType.String, 10, poParameter.CSUPPLIER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParameter.CLANGUAGE_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x.ParameterName ==
                                "@" + poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name)
                    .Select(x => x.Value);
                _loggerAp.LogDebug("EXEC RSP_AP_GET_SUPPLIER_INFO_LIST {@poParameter}", loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<APL00110DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)

            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        public List<APL00200DTO> ExpenditureLookup(APL00200ParameterDTO poParameter)
        {
            using var activity = _activitySource.StartActivity(nameof(ExpenditureLookup));
            R_Exception loException = new R_Exception();
            List<APL00200DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_AP_LOOKUP_EXPENDITURE";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 10, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_ID", DbType.String, 10, poParameter.CCATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAXABLE_TYPE", DbType.String, 10, poParameter.CTAXABLE_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CACTIVE_TYPE", DbType.String, 10, poParameter.CACTIVE_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParameter.CLANGUAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_DATE", DbType.String, 10, poParameter.CTAX_DATE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x.ParameterName ==
                                "@" + poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name)
                    .Select(x => x.Value);
                _loggerAp.LogDebug("EXEC RSP_AP_LOOKUP_EXPENDITURE {@poParameter}", loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<APL00200DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)

            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        public List<APL00300DTO> ProductLookup(APL00300ParameterDTO poParameter)
        {
            using var activity = _activitySource.StartActivity(nameof(ProductLookup));
            R_Exception loException = new R_Exception();
            List<APL00300DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_AP_LOOKUP_PRODUCT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 10, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_ID", DbType.String, 10, poParameter.CCATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAXABLE_TYPE", DbType.String, 10, poParameter.CTAXABLE_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CACTIVE_TYPE", DbType.String, 10, poParameter.CACTIVE_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParameter.CLANGUAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_DATE", DbType.String, 10, poParameter.CTAX_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CBUYSELL", DbType.String, 40, poParameter.CBUYSELL);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x.ParameterName ==
                                "@" + poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name)
                    .Select(x => x.Value);
                _loggerAp.LogDebug("EXEC RSP_AP_LOOKUP_PRODUCT {@poParameter}", loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<APL00300DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)

            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        public List<APL00400DTO> ProductAllocationLookup(APL00400ParameterDTO poParameter)
        {
            using var activity = _activitySource.StartActivity(nameof(ProductAllocationLookup));
            R_Exception loException = new R_Exception();
            List<APL00400DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_AP_LOOKUP_PRODUCT_ALLOCATION";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 10, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTIVE_TYPE", DbType.String, 10, poParameter.CACTIVE_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParameter.CLANGUAGE_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _loggerAp.LogDebug("EXEC RSP_AP_LOOKUP_PRODUCT_ALLOCATION {@poParameter}", loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<APL00400DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)

            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        public List<APL00500DTO> TransactionLookup(APL00500ParameterDTO poParameter)
        {
            using var activity = _activitySource.StartActivity(nameof(TransactionLookup));
            R_Exception loException = new R_Exception();
            List<APL00500DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_AP_LOOKUP_TRX_REF_NO";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 10, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 10, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParameter.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_ID", DbType.String, 10, poParameter.CSUPPLIER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 10, poParameter.CPERIOD);
                loDb.R_AddCommandParameter(loCmd, "@LHAS_REMAINING", DbType.Boolean, 10, poParameter.LHAS_REMAINING);
                loDb.R_AddCommandParameter(loCmd, "@LNO_REMAINING", DbType.Boolean, 10, poParameter.LNO_REMAINING);
                loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_CODE", DbType.String, 10, poParameter.CCURRENCY_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 10, poParameter.CLANGUAGE_ID);


                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _loggerAp.LogDebug("EXEC RSP_AP_LOOKUP_TRX_REF_NO {@poParameter}", loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<APL00500DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)

            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        public APL00500PeriodDTO InitialProcessApl00500(APL00500ParameterDTO poParameter)
        {
            using var activity = _activitySource.StartActivity(nameof(InitialProcessApl00500));
            R_Exception loException = new R_Exception();
            APL00500PeriodDTO loReturn = null;
            R_Db loDb;
            DbCommand loCmd;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_PERIOD_YEAR_RANGE";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 10, poParameter.CYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CMODE", DbType.String, 10, poParameter.CMODE);


                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _loggerAp.LogDebug("EXEC RSP_GS_GET_PERIOD_YEAR_RANGE {@poParameter}", loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<APL00500PeriodDTO>(loReturnTemp).FirstOrDefault();
            }
            catch (Exception ex)

            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        public List<APL00600DTO> SchedulePaymentLookup(APL00600ParameterDTO poParameter)
        {
            using var activity = _activitySource.StartActivity(nameof(SchedulePaymentLookup));
            R_Exception loException = new R_Exception();
            List<APL00600DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_AP_LOOKUP_SCHEDULE_PAYMENT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 10, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 10, poParameter.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CPAYMENT_TYPE", DbType.String, 10, poParameter.CPAYMENT_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_ID", DbType.String, 10, poParameter.CSUPPLIER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 10, poParameter.CLANG_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSCH_PAYMENT_ID", DbType.String, 10, poParameter.CSCH_PAYMENT_ID);


                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _loggerAp.LogDebug("EXEC RSP_AP_LOOKUP_SCHEDULE_PAYMENT {@poParameter}", loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<APL00600DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)

            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }

        public List<APL00600DTO> InvoiceListLookup(APL00600ParameterDTO poParameter)
        {
            using var activity = _activitySource.StartActivity(nameof(InvoiceListLookup));
            R_Exception loException = new R_Exception();
            List<APL00600DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_AP_GET_SCH_PAYMENT_INVOICE";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 40, poParameter.CREC_ID);


                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _loggerAp.LogDebug("EXEC RSP_AP_GET_SCH_PAYMENT_INVOICE {@poParameter}", loDbParam);
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<APL00600DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)

            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }
        public APL00700DTO InitialPaymentToSupplierLookup(APL00700ParameterDTO poParameter)
        {
            using var activity = _activitySource.StartActivity(nameof(InitialPaymentToSupplierLookup));
            R_Exception loException = new R_Exception();
            APL00700DTO loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_PERIOD_YEAR_RANGE";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 40, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 40,"");
                loDb.R_AddCommandParameter(loCmd, "@CMODE", DbType.String, 40,"");


                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _loggerAp.LogDebug("EXEC RSP_GS_GET_PERIOD_YEAR_RANGE {@poParameter}", loDbParam);
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<APL00700DTO>(loReturnTemp).FirstOrDefault();
            }
            catch (Exception ex)

            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }
        public List<APL00700DTO>CancelPaymentToSupplierLookup(APL00700ParameterDTO poParameter)
        {
            using var activity = _activitySource.StartActivity(nameof(CancelPaymentToSupplierLookup));
            R_Exception loException = new R_Exception();
            List<APL00700DTO> loReturn = null;
            R_Db loDb;
            DbCommand loCmd;

            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_AP_LOOKUP_CANCEL_SUPP_PAYMENT";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 40, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 40, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 40, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 40, poParameter.CPERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_ID", DbType.String, 40, poParameter.CSUPPLIER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSCH_PAYMENT_ID", DbType.String, 40, poParameter.CSCH_PAYMENT_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 40, poParameter.CLANGUAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 40, poParameter.CDEPT_CODE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _loggerAp.LogDebug("EXEC RSP_AP_LOOKUP_CANCEL_SUPP_PAYMENT {@poParameter}", loDbParam);


                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);

                loReturn = R_Utility.R_ConvertTo<APL00700DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)

            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loReturn;
        }
    }
    
    
}