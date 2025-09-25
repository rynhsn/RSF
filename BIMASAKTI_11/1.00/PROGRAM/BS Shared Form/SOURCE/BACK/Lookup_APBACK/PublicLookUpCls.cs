using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lookup_APCOMMON.DTOs.APL00100;
using Lookup_APCOMMON.DTOs.APL00110;
using Lookup_APCOMMON.DTOs.APL00200;
using Lookup_APCOMMON.DTOs.APL00300;
using Lookup_APCOMMON.DTOs.APL00400;
using Lookup_APCOMMON.Loggers;
using R_BackEnd;
using R_Common;
using APL00100ParameterDTO = Lookup_APCOMMON.DTOs.APL00100.APL00100ParameterDTO;

namespace Lookup_APBACK
{
    public class PublicLookUpCls
    {
        private LoggerPublicLookup _Logger;

        public PublicLookUpCls()
        {
            _Logger = LoggerPublicLookup.R_GetInstanceLogger();
        }

        public List<APL00100DTO> SupplierLookup(APL00100ParameterDTO poParameter)
        {
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
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 40, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 40, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSEARCH_TEXT", DbType.String, 40, poParameter.CSEARCH_TEXT);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 40, poParameter.CLANGUAGE_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x.ParameterName == "@" + poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_AP_SEARCH_SUPPLIER_LOOKUP_LIST {@poParameter}", loDbParam);

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
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 40, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 40, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_ID", DbType.String, 40, poParameter.CSUPPLIER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 40, poParameter.CLANGUAGE_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x.ParameterName == "@" + poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_AP_GET_SUPPLIER_INFO_LIST {@poParameter}", loDbParam);

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
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 40, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 40, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_ID", DbType.String, 40, poParameter.CCATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAXABLE_TYPE", DbType.String, 40, poParameter.CTAXABLE_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CACTIVE_TYPE", DbType.String, 40, poParameter.CACTIVE_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 40, poParameter.CLANGUAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_DATE", DbType.String, 40, poParameter.CTAX_DATE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x.ParameterName == "@" + poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_AP_LOOKUP_EXPENDITURE {@poParameter}", loDbParam);

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
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 40, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 40, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_ID", DbType.String, 40, poParameter.CCATEGORY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAXABLE_TYPE", DbType.String, 40, poParameter.CTAXABLE_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CACTIVE_TYPE", DbType.String, 40, poParameter.CACTIVE_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 40, poParameter.CLANGUAGE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_DATE", DbType.String, 40, poParameter.CTAX_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CBUYSELL", DbType.String, 40, poParameter.CBUYSELL);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x.ParameterName == "@" + poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_AP_LOOKUP_PRODUCT {@poParameter}", loDbParam);

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
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 40, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 40, poParameter.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTIVE_TYPE", DbType.String, 40, poParameter.CACTIVE_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 40, poParameter.CLANGUAGE_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x.ParameterName == "@" + poParameter.GetType().GetProperty(x.ParameterName.Replace("@", "")).Name).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_AP_LOOKUP_PRODUCT_ALLOCATION {@poParameter}", loDbParam);

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
    }
}
