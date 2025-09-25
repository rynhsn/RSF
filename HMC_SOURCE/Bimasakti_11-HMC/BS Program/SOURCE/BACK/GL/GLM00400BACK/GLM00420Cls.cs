using GLM00400COMMON;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data;
using System.Data.Common;

namespace GLM00400BACK
{
    public class GLM00420Cls
    {
        private LoggerGLM00420 _Logger;
        public GLM00420Cls()
        {
            _Logger = LoggerGLM00420.R_GetInstanceLogger();
        }

        public List<GLM00420DTO> GetAllSourceAllocationCenter(GLM00420DTO poEntity)
        {
            var loEx = new R_Exception();
            List<GLM00420DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var loCmd = loDb.GetCommand();

                var lcQuery = "RSP_GL_GET_SOURCE_ALLOCATION_CENTER_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CALLOC_ID", DbType.String, 50, poEntity.CREC_ID_ALLOCATION_ID);
                loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, poEntity.CUSER_LANGUAGE);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
             .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GL_GET_SOURCE_ALLOCATION_CENTER_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLM00420DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GLM00421DTO> GetAllAllocationCenter(GLM00421DTO poEntity)
        {
            var loEx = new R_Exception();
            List<GLM00421DTO> loResult = null;

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
                .Where(x => x.ParameterName == "@CALLOC_ID" ||
                x.ParameterName == "@CLANGUAGE_ID").Select(x => x.Value);
                _Logger.LogDebug("EXEC RSP_GL_GET_ALLOCATION_CENTER_LIST {@poParameter}", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GLM00421DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public void SaveAllocationCenter(GLM00421DTO poNewEntity)
        {
            var loEx = new R_Exception();
            string lcQuery = "";
            var loDb = new R_Db();
            var loConn = loDb.GetConnection("R_DefaultConnectionString");
            var loCmd = loDb.GetCommand();

            try
            {
                lcQuery = "EXECUTE RSP_GL_ADD_ALLOCATION_CENTER_LIST @CUSER_ID, @CCOMPANY_ID, @CALLOC_ID, @CCENTER_LIST";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CALLOC_ID", DbType.String, 255, poNewEntity.CREC_ID_ALLOCATION_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCENTER_LIST", DbType.String, int.MaxValue, poNewEntity.CCENTER_LIST);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    //Debug Logs
                    var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                    .Where(x => x.ParameterName == "@CUSER_ID" ||
                    x.ParameterName == "@CCOMPANY_ID" ||
                    x.ParameterName == "@CALLOC_ID" ||
                    x.ParameterName == "@CCENTER_LIST").Select(x => x.Value);
                    _Logger.LogDebug("EXEC RSP_GL_ADD_ALLOCATION_CENTER_LIST {@poParameter}", loDbParam);

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

    }
}
