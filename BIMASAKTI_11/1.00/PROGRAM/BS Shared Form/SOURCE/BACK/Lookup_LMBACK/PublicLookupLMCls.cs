using System.Data;
using System.Data.Common;
using System.Runtime.InteropServices;
using Lookup_PMCOMMON.DTOs;
using R_BackEnd;
using R_Common;
using Lookup_PMCOMMON.Logs;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace Lookup_PMBACK
{
    public class PublicLookupLMCls
    {
        private LoggerLookupLM _loggerLookup;
        private readonly ActivitySource _activitySource;
        public PublicLookupLMCls()
        {
            _loggerLookup = LoggerLookupLM.R_GetInstanceLogger();
            _activitySource = LookupLMActivity.R_GetInstanceActivitySource();
        }


        public List<LML00200DTO> GetAllUnitCharges(LML00200ParameterDTO poEntity)
        {
            string lcMethodName = nameof(GetAllUnitCharges);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            var loEx = new R_Exception();
            List<LML00200DTO> loResult = null;
            R_Db loDb;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_PM_GET_CHARGES_TYPE_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 25, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 25, poEntity.CCHARGE_TYPE_ID);
                
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLookup.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);
                
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<LML00200DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult;
        }
        public List<LML00300DTO> GetAllSupervisor(LML00300ParameterDTO poEntity)
        {
            string lcMethodName = nameof(GetAllSupervisor);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            var loEx = new R_Exception();
            List<LML00300DTO> loResult = null;
            R_Db loDb;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_PM_GET_SUPERVISOR_LOOKUP_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 25, poEntity.CPROPERTY_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLookup.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<LML00300DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult;
        }
        public List<LML00400DTO> GetAllUtilityCharges(LML00400ParameterDTO poEntity)
        {
            string lcMethodName = nameof(GetAllUtilityCharges);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            var loEx = new R_Exception();
            List<LML00400DTO> loResult = null;
            R_Db loDb;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_PM_GET_CHARGES_UTILITY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 25, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGE_TYPE_ID", DbType.String, 99, poEntity.CCHARGE_TYPE_ID);
               
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLookup.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<LML00400DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult;
        }

        public List<LML00500DTO> GetAllSalesman(LML00500ParameterDTO poEntity)
        {
            string lcMethodName = nameof(GetAllSalesman);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            
            var loEx = new R_Exception();
            List<LML00500DTO> loResult = null;
            R_Db loDb;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_PM_GET_SALESMAN_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLookup.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<LML00500DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult;
        }
        public List<LML00600DTO> GetTenant(LML00600ParameterDTO poEntity)
        {
            string lcMethodName = nameof(GetTenant);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            var loEx = new R_Exception();
            List<LML00600DTO> loResult = null;
            R_Db loDb;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_PM_GET_TENANT_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCUSTOMER_TYPE", DbType.String, 2, poEntity.CCUSTOMER_TYPE);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLookup.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<LML00600DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult;
        }
        public List<LML00700DTO> GetDiscount(LML00700ParameterDTO poEntity)
        {
            string lcMethodName = nameof(GetDiscount);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            var loEx = new R_Exception();
            List<LML00700DTO> loResult = null;
            R_Db loDb;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_PM_GET_DISCOUNT_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 2, poEntity.CCHARGES_TYPE);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLookup.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<LML00700DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult;
        }

    }
}