using GLM00400COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;

namespace GLM00400BACK
{
    public class GLM00410Cls : R_BusinessObject<GLM00410DTO>
    {
        private LoggerGLM00410 _Logger;
        public GLM00410Cls()
        {
            _Logger = LoggerGLM00410.R_GetInstanceLogger();
        }

        public List<GLM00411DTO> GetAllAllocationAccount(GLM00411DTO poEntity)
        {
            var loEx = new R_Exception();
            List<GLM00411DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_GET_ALLOCATION_ACCOUNT_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CALLOC_ID", DbType.String, 50, poEntity.CREC_ID_ALLOCATION_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CUSER_LANGUAGE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GL_GET_ALLOCATION_ACCOUNT_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLM00411DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GLM00412DTO> GetAllAllocationTargetCenter(GLM00412DTO poEntity)
        {
            var loEx = new R_Exception();
            List<GLM00412DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_GET_ALLOCATION_CENTER_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CALLOC_ID", DbType.String, 50, poEntity.CREC_ID_ALLOCATION_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CUSER_LANGUAGE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GL_GET_ALLOCATION_CENTER_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLM00412DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GLM00413DTO> GetAllAllocationTargetCenterByPeriod(GLM00413DTO poEntity)
        {
            var loEx = new R_Exception();
            List<GLM00413DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_GET_ALLOCATION_CENTER_VALUE_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCENTER_ID", DbType.String, 50, poEntity.CREC_ID_CENTER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 50, poEntity.CYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CUSER_LANGUAGE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GL_GET_ALLOCATION_CENTER_VALUE_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLM00413DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GLM00414DTO> GetAllAllocationPeriod(GLM00414DTO poEntity)
        {
            var loEx = new R_Exception();
            List<GLM00414DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GS_GET_PERIOD_DT_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 5, poEntity.CCYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, 50, poEntity.CPERIOD_NO);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GS_GET_PERIOD_DT_INFO {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLM00414DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GLM00415DTO> GetAllAllocationPeriodByTargetCenter(GLM00415DTO poEntity)
        {
            var loEx = new R_Exception();
            List<GLM00415DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_GET_ALLOCATION_CENTER_VALUE_BY_PERIOD_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CALLOC_ID", DbType.String, 50, poEntity.CREC_ID_ALLOCATION_ID);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 50, poEntity.CYEAR);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, 50, poEntity.CPERIOD_NO);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CUSER_LANGUAGE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GL_GET_ALLOCATION_CENTER_VALUE_BY_PERIOD_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLM00415DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Deleting(GLM00410DTO poEntity)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("R_DefaultConnectionString");
            var loCmd = loDb.GetCommand();

            try
            {
                lcQuery = "RSP_GL_DELETE_ALLOCATION_HD";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 255, poEntity.CREC_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                 .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_GL_DELETE_ALLOCATION_HD {@poParameter}", loDbParam);

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
                loEx.Add(ex);
                _Logger.LogError(loEx);
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

        protected override GLM00410DTO R_Display(GLM00410DTO poEntity)
        {
            var loEx = new R_Exception();
            GLM00410DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_GET_ALLOCATION_HD";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GL_GET_ALLOCATION_HD {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loDb.GetConnection(), loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLM00410DTO>(loDataTable).FirstOrDefault();
                loResult.CREC_ID_ALLOCATION_ID = loResult.CREC_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        protected override void R_Saving(GLM00410DTO poNewEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("R_DefaultConnectionString");
            var loCmd = loDb.GetCommand();

            try
            {
                // set action 
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CACTION = "NEW";

                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poNewEntity.CACTION = "EDIT";
                }
                if (string.IsNullOrWhiteSpace(poNewEntity.CREC_ID))
                {
                    poNewEntity.CREC_ID = "";
                }

                lcQuery = "RSP_GL_SAVE_ALLOCATION_HD";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 255, poNewEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CALLOC_NO", DbType.String, 255, poNewEntity.CALLOC_NO);
                loDb.R_AddCommandParameter(loCmd, "@CALLOC_NAME", DbType.String, 255, poNewEntity.CALLOC_NAME);
                loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 7, poNewEntity.LACTIVE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poNewEntity.CACTION);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, R_BackGlobalVar.USER_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_GL_SAVE_ALLOCATION_HD {@poParameter}", loDbParam);

                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);

                    var loResult = R_Utility.R_ConvertTo<GLM00410DTO>(loDataTable).FirstOrDefault();

                    _Logger.LogInfo("Set CREC_ID IF ADD Data");
                    if (poCRUDMode == eCRUDMode.AddMode)
                    {
                        poNewEntity.CREC_ID = loResult.CREC_ID;
                    }
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
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
            loEx.ThrowExceptionIfErrors();
        }

        public GLM00413DTO GetAllocationTargetCenterByPeriod(GLM00413DTO poEntity)
        {
            var loEx = new R_Exception();
            GLM00413DTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_GET_ALLOCATION_CENTER_VALUE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CVALUE_ID", DbType.String, 50, poEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, R_BackGlobalVar.CULTURE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GL_GET_ALLOCATION_CENTER_VALUE {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLM00413DTO>(loDataTable).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GLM00413DTO SavingAllocationTargetCenterByPeriod(GLM00413DTO poNewEntity)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            GLM00413DTO loResult = null;

            try
            {
                loConn = loDb.GetConnection("R_DefaultConnectionString");
                loCmd = loDb.GetCommand();

                lcQuery = "EXEC RSP_GL_UPDATE_ALLOCATION_CENTER_VALUE @CUSER_ID, @CCOMPANY_ID, @CVALUE_ID, @NVALUE";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;

                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, R_BackGlobalVar.USER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CVALUE_ID", DbType.String, 255, poNewEntity.CREC_ID);
                loDb.R_AddCommandParameter(loCmd, "@NVALUE", DbType.Decimal, 100, poNewEntity.NVALUE);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 8, R_BackGlobalVar.CULTURE);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_GL_UPDATE_ALLOCATION_CENTER_VALUE  {@poParameter}", loDbParam);

                    loDb.SqlExecNonQuery(loConn, loCmd, false);

                    lcQuery = "EXEC RSP_GL_GET_ALLOCATION_CENTER_VALUE @CVALUE_ID, @CLANGUAGE_ID";
                    loCmd.CommandText = lcQuery;
                    loCmd.CommandType = CommandType.Text;

                    //Debug Logs
                    var loDbParam1 = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x.ParameterName == "@CVALUE_ID" ||
                    x.ParameterName == "@CLANGUAGE_ID").Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_GL_GET_ALLOCATION_CENTER_VALUE {@poParameter}", loDbParam1);

                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);

                    loResult = R_Utility.R_ConvertTo<GLM00413DTO>(loDataTable).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    loEx.Add(ex);
                }

                loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
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
            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
