using GLT00100COMMON;
using R_BackEnd;
using R_Common;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using RSP_GL_SAVE_JOURNALResources;
using RSP_GL_UPDATE_JOURNAL_STATUSResources;

namespace GLT00100BACK
{
    public class GLT00100Cls
    {
        private RSP_GL_SAVE_JOURNALResources.Resources_Dummy_Class saveJournal = new();

        private RSP_GL_UPDATE_JOURNAL_STATUSResources.Resources_Dummy_Class updateJournalnew = new();

        private LoggerGLT00100 _logger;

        private readonly ActivitySource _activitySource;

        public GLT00100Cls()
        {
            _logger = LoggerGLT00100.R_GetInstanceLogger();
            _activitySource = GLT00100Activity.R_GetInstanceActivitySource();
        }

        public List<GLT00100DTO> GetJournalList(GLT00100ParamDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            List<GLT00100DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_SEARCH_JOURNAL_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 50, ContextConstant.VAR_TRANS_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 50, poEntity.CPERIOD);
                loDb.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 50, poEntity.CSTATUS);
                loDb.R_AddCommandParameter(loCmd, "@CSEARCH_TEXT", DbType.String, 50, poEntity.CSEARCH_TEXT);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLT00100DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GLT00101DTO> GetJournalDetailList(string poRecId)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            List<GLT00101DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_GET_JOURNAL_DETAIL_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CJRN_ID", DbType.String, int.MaxValue, poRecId);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLT00101DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public void UpdateJournalStatus(GLT00100UpdateStatusDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                loCmd = loDb.GetCommand();

                lcQuery = "RSP_GL_UPDATE_JOURNAL_STATUS";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CAPPROVE_BY", DbType.String, 20, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CJRN_ID_LIST", DbType.String, int.MaxValue, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CNEW_STATUS", DbType.String, 20, poEntity.CNEW_STATUS);
                loDb.R_AddCommandParameter(loCmd, "@LAUTO_COMMIT", DbType.Boolean, 20, poEntity.LAUTO_COMMIT);
                loDb.R_AddCommandParameter(loCmd, "@LUNDO_COMMIT", DbType.Boolean, 20, poEntity.LUNDO_COMMIT);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    ShowLogDebug(lcQuery, loCmd.Parameters);
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

            }
            catch (Exception ex)
            {
                loEx.Add(ex); ShowLogError(loEx);
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
                if (loDb != null)
                {
                    loDb = null;
                }
            }
            loEx.ThrowExceptionIfErrors();
        }

        public GLT00100RapidApprovalValidationDTO ValidationRapidAppro(GLT00100RapidApprovalValidationDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            var loEx = new R_Exception();
            GLT00100RapidApprovalValidationDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                var lcQuery = @"RSP_GS_GET_ALLOW_APPROVAL_STATUS";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, int.MaxValue, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, int.MaxValue, poEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, int.MaxValue, poEntity.CTRANS_CODE);

                //Debug Logs
                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLT00100RapidApprovalValidationDTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);

                ShowLogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #region log activity

        private void ShowLogDebug(string query, DbParameterCollection parameters)
        {
            var paramValues = string.Join(", ", parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'"));
            _logger.LogDebug($"EXEC {query} {paramValues}");
        }

        private void ShowLogError(Exception ex)
        {
            _logger.LogError(ex);
        }                       

        #endregion
    }
}