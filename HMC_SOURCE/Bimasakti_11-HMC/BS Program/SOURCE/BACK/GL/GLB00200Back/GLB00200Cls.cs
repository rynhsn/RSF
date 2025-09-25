using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Transactions;
using GLB00200Common;
using GLB00200Common.Logs;
using Microsoft.SqlServer.Server;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using RSP_GL_PROCESS_REVERSING_JRNResources;

namespace GLB00200Back
{
    public class GLB00200Cls
    {
        Resources_Dummy_Class _loRSP = new();

        private LoggerGLB00200 _loggerGLB00200;
        private readonly ActivitySource _activitySource;
        public GLB00200Cls()
        {
            _loggerGLB00200 = LoggerGLB00200.R_GetInstanceLogger();
            _activitySource = GLB00200Activity.R_GetInstanceActivitySource();
        }
        public GLB00200InitalProcessDTO InitialProcess(GLB00200DBParameter poParameter)
        {
            string lcMethodName = nameof(InitialProcess);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGLB00200.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            GLB00200InitalProcessDTO loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_GS_GET_PERIOD_YEAR_RANGE ";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CYEAR", DbType.String, 4, "");
                loDb.R_AddCommandParameter(loCommand, "@CMODE", DbType.String, 10, "");
                
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
               _loggerGLB00200.LogInfo("Execute query initial process to get year range");
                _loggerGLB00200.LogDebug("{@ObjectQuery(1)} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, false);
                loResult = R_Utility.R_ConvertTo<GLB00200InitalProcessDTO>(loReturnTemp).FirstOrDefault();

                //for LINCREMENT __ VALIDATION
                var lcQueryLincrement = (string.Format("RSP_GS_GET_TRANS_CODE_INFO '{0}', '000020' ", poParameter.CCOMPANY_ID));
                loCommand.CommandText = lcQueryLincrement;
                loCommand.CommandType = CommandType.Text;
                _loggerGLB00200.LogInfo("Execute query initial process to validation");
                _loggerGLB00200.LogDebug("{@ObjectQuery (2)} ", lcQueryLincrement);

                var loReturnTempVal = loDb.SqlExecQuery(loConn, loCommand, true);
                var loResultTemp = R_Utility.R_ConvertTo<GLB00200InitalProcessDTO>(loReturnTempVal).FirstOrDefault();

                loResult.LINCREMENT_FLAG = loResultTemp.LINCREMENT_FLAG;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLB00200.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _loggerGLB00200.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult;
        }
        public List<GLB00200DTO> ReversingJournalProcessList(GLB00200DBParameter poParameter)
        {
            string lcMethodName = nameof(ReversingJournalProcessList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGLB00200.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            List<GLB00200DTO> loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_GL_SEARCH_ACTIVE_REVERSING_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CPERIOD", DbType.String, 6, poParameter.CPERIOD);
                loDb.R_AddCommandParameter(loCommand, "@CSEARCH_TEXT", DbType.String, 30, poParameter.CSEARCH_TEXT);
                loDb.R_AddCommandParameter(loCommand, "@CLANGUAGE_ID", DbType.String, 2, poParameter.CLANGUAGE_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGLB00200.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<GLB00200DTO>(loReturnTemp).ToList();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLB00200.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _loggerGLB00200.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult;
        }
    
        public List<GLB00200JournalDetailDTO> GetDetail_ReversingJournalList(GLB00200DBParameter poParameter)
        {
            string lcMethodName = nameof(GetDetail_ReversingJournalList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGLB00200.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));


            R_Exception loException = new R_Exception();
            List<GLB00200JournalDetailDTO> loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_GL_GET_JOURNAL_DETAIL_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CJRN_ID", DbType.String, 50, poParameter.CREC_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANGUAGE_ID", DbType.String, 2, poParameter.CLANGUAGE_ID);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGLB00200.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);


                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<GLB00200JournalDetailDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLB00200.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _loggerGLB00200.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult;
        }

    }

}