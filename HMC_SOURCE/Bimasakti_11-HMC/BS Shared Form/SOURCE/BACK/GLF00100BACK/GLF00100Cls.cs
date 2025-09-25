using GLF00100COMMON;
using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data;
using System.Diagnostics;

namespace GLF00100BACK
{
    public class GLF00100Cls
    {
        private LoggerGLF00100 _Logger;
        private readonly ActivitySource _activitySource;
        public GLF00100Cls()
        {
            _Logger = LoggerGLF00100.R_GetInstanceLogger();
            _activitySource = GLF00100ActivitySourceBase.R_GetInstanceActivitySource();
        }

        public GLF00100InitialDTO GetInfoCompany(string poCompanyID)
        {
            using Activity activity = _activitySource.StartActivity("GetInfoCompany");
            var loEx = new R_Exception();
            GLF00100InitialDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_COMPANY_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poCompanyID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                   .Where(x => x != null && x.ParameterName.StartsWith("@"))
                   .Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_COMPANY_INFO {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLF00100InitialDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GLF00100DTO GetJournalDetail(GLF00100ParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetJournalDetail");
            var loEx = new R_Exception();
            GLF00100DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_GET_JOURNAL_DETAIL";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, poEntity.CTRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 50, poEntity.CREF_NO);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                   .Where(x => x != null && x.ParameterName.StartsWith("@"))
                   .Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GL_GET_JOURNAL_DETAIL {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLF00100DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GLF00101DTO> GetAllJournalDetailList(GLF00101DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetAllJournalDetailList");
            var loEx = new R_Exception();
            List<GLF00101DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_GET_JOURNAL_DETAIL_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CJRN_ID", DbType.String, 50, poEntity.CJRN_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CLANGUAGE_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                   .Where(x => x != null && x.ParameterName.StartsWith("@"))
                   .Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GL_GET_JOURNAL_DETAIL_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLF00101DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}