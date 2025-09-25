using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GLB00700COMMON;

namespace GLB00700BACK
{
    public class GLB00701Cls : R_IBatchProcess
    {
        //variables & constructors
        private GLB00700Logger _logger;
        private readonly ActivitySource _activitySource;
        public GLB00701Cls()
        {
            _logger = GLB00700Logger.R_GetInstanceLogger();
            _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
        }

        //method
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            R_Db loDb = new();
            _logger.LogInfo($"Start process method {MethodBase.GetCurrentMethod().Name} On {GetType().Name}");
            try
            {
                _logger.LogInfo("start test connection");
                if (loDb.R_TestConnection() == false)
                {
                    loEx.Add("", "Database Connection Failed");
                    _logger.LogError(loEx);
                    goto EndBlock;
                }
                _logger.LogInfo("end test connection");

                _logger.LogInfo("start run _BatchProcess");
                var loTask = Task.Run(() =>
                {
                    _BatchProcessAsync(poBatchProcessPar);
                });
                _logger.LogInfo("end run _BatchProcess");

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
            _logger.LogInfo($"End process method {MethodBase.GetCurrentMethod().Name} On {GetType().Name}");

        }
        public async Task _BatchProcessAsync(R_BatchProcessPar poBatchProcessPar)
        {
            string lcMethodName = nameof(_BatchProcessAsync);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _logger.LogInfo($"Start process method {MethodBase.GetCurrentMethod().Name} On {GetType().Name}");
            R_Exception loException = new R_Exception();
            string lcQuery = "";
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCommand = null;
            try
            {
                await Task.Delay(100);
                
                //get parameter
                var loDeptParam = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(GLB00700ContextConstant.CDEPT_CODE)).FirstOrDefault().Value;
                var lcDeptCode = ((System.Text.Json.JsonElement)loDeptParam).GetString();
                var loDateParam = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(GLB00700ContextConstant.CDATE)).FirstOrDefault().Value;
                var lcDate = ((System.Text.Json.JsonElement)loDateParam).GetString();
                var loProcessFor = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(GLB00700ContextConstant.CPROCESS_FOR)).FirstOrDefault().Value;
                var lcProcessFor = ((System.Text.Json.JsonElement)loProcessFor).GetString();

                loCommand = loDb.GetCommand();
                loConn = loDb.GetConnection();
                lcQuery = "RSP_GL_RATE_REVALUATION_PROCESS";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, int.MaxValue, poBatchProcessPar.Key.COMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, int.MaxValue, lcDeptCode);
                loDb.R_AddCommandParameter(loCommand, "@CDATE", DbType.String, int.MaxValue, lcDate);
                loDb.R_AddCommandParameter(loCommand, "@CPROCESS_FOR", DbType.String, int.MaxValue,lcProcessFor);
                loDb.R_AddCommandParameter(loCommand, "@CKEY_GUID", DbType.String, int.MaxValue, poBatchProcessPar.Key.KEY_GUID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, int.MaxValue, poBatchProcessPar.Key.USER_ID);
                _logger.LogDebug("Exec " + lcQuery + string.Join(", ", loCommand.Parameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'")));
                var loRtn = loDb.SqlExecNonQuery(loConn, loCommand, false);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            finally
            {
                if (loConn != null)
                {
                    if (!(loConn.State == ConnectionState.Closed))
                        loConn.Close();
                    loConn.Dispose();
                    loConn = null;
                }

                if (loCommand != null)
                {
                    loCommand.Dispose();
                    loCommand = null;
                }
            }
            //HANDLE EXCEPTION IF THERE ANY ERROR ON TRY CATCH paling luar
            if (loException.Haserror)
            {
                lcQuery = string.Format("EXEC RSP_WRITEUPLOADPROCESSSTATUS '{0}', '{1}', '{2}', 100, '{3}', {4}", poBatchProcessPar.Key.COMPANY_ID, poBatchProcessPar.Key.USER_ID, poBatchProcessPar.Key.KEY_GUID, loException.ErrorList[0].ErrDescp, 9);
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.Text;
                loDb.SqlExecNonQuery(lcQuery);
            }
            _logger.LogInfo($"End process method {MethodBase.GetCurrentMethod().Name} On {GetType().Name}");
            loException.ThrowExceptionIfErrors();
        }
    }
}
