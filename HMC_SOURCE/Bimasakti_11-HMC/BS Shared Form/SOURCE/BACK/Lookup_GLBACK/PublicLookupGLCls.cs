using Lookup_GLCOMMON.DTOs;
using Lookup_GLCOMMON.DTOs.GLL00100;
using Lookup_GLCOMMON.DTOs.GLL00110;
using Lookup_GLCOMMON.Loggers;
using R_BackEnd;
using R_Common;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace Lookup_GLBACK
{
    public class PublicLookupGLCls
    {
        private LoggerLookupGL _loggerLookup;
        private readonly ActivitySource _activitySource;
        public PublicLookupGLCls()
        {
            _loggerLookup = LoggerLookupGL.R_GetInstanceLogger();
            _activitySource = LookupGLActivity.R_GetInstanceActivitySource();
        }

        public List<GLL00100DTO> GLL00100ReferenceNoLookUpDb(GLL00100ParameterDTO poEntity)
        {
            string lcMethodName = nameof(GLL00100ReferenceNoLookUpDb);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            var loEx = new R_Exception();
            List<GLL00100DTO> loResult = null;
            R_Db loDb;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GL_LOOKUP_REFERENCE_NO_BY_PERIOD";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 6, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_DEPT_CODE", DbType.String, 20, poEntity.CFROM_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTO_DEPT_CODE", DbType.String, 20, poEntity.CTO_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_DATE", DbType.String, 8, poEntity.CFROM_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CTO_DATE", DbType.String, 8, poEntity.CTO_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 8, poEntity.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLookup.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLL00100DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult!;
        }


        public List<GLL00110DTO> GLL00110ReferenceNoLookUpByPeriodDb(GLL00110ParameterDTO poEntity)
        {
            string lcMethodName = nameof(GLL00110ReferenceNoLookUpByPeriodDb);
            using Activity activity = _activitySource.StartActivity(lcMethodName)!;
            _loggerLookup.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            var loEx = new R_Exception();
            List<GLL00110DTO> loResult = null;
            R_Db loDb;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GL_LOOKUP_REFERENCE_NO_BY_PERIOD";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 6, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_DEPT_CODE", DbType.String, 20, poEntity.CFROM_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTO_DEPT_CODE", DbType.String, 20, poEntity.CTO_DEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CFROM_DATE", DbType.String, 8, poEntity.CFROM_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CTO_DATE", DbType.String, 8, poEntity.CTO_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 8, poEntity.CLANGUAGE_ID);

                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerLookup.LogDebug("{@ObjectQuery} {@Parameter}", loCmd.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLL00110DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult!;
        }

    }
}
