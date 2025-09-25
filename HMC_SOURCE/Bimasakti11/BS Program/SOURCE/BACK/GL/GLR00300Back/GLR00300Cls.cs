using GLR00300Common;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data.Common;
using System.Data;
using System.IO.MemoryMappedFiles;
using GLR00300Common.GLR00300Print;
using System.Reflection.Metadata;
using AutoMapper;
using GLR00300Common.Logs;
using System.Diagnostics;

namespace GLR00300Back
{
    public class GLR00300Cls
    {
        private LoggerGLR00300 _loggerGLR00300;
        private readonly ActivitySource _activitySource;
        public GLR00300Cls()
        {
            //Initial and Get Logger
            _loggerGLR00300 = LoggerGLR00300.R_GetInstanceLogger();
            _activitySource = GLR00300Activity.R_GetInstanceActivitySource();
        }
        //public GLR00300Cls(LoggerGLR00300 poLogger)
        //{
        //    //Initial and Get Logger
        //    _loggerGLR00300 = LoggerGLR00300.R_GetInstanceLogger();
        //}
        public GLR00300InitialProcess InitialProcess(GLR00300DBParameter poParameter)
        {
            string lcMethodName = nameof(InitialProcess);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGLR00300.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            GLR00300InitialProcess loResult = null;
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
                _loggerGLR00300.LogInfo("Execute query initial process to get year range");
                _loggerGLR00300.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, false);
                loResult = R_Utility.R_ConvertTo<GLR00300InitialProcess>(loReturnTemp).FirstOrDefault();

                //Initial Process GET DEFAULT YEAR
                var lcQueryDefaultYear = $"EXEC RSP_GL_GET_SYSTEM_PARAM '{poParameter.CCOMPANY_ID}', '{poParameter.CLANGUAGE_ID}' ";
                loCommand.CommandText = lcQueryDefaultYear;
                loCommand.CommandType = CommandType.Text;
                _loggerGLR00300.LogInfo("Execute query initial process to get default year");
                _loggerGLR00300.LogDebug("{@ObjectQuery (2)} ", lcQueryDefaultYear);

                var loReturnDefaultYear = loDb.SqlExecQuery(loConn, loCommand, false);
                var LcDefaultYear = R_Utility.R_ConvertTo<GLR00300InitialProcess>(loReturnDefaultYear).FirstOrDefault();
                loResult.CSOFT_PERIOD_YY = LcDefaultYear.CSOFT_PERIOD_YY;

                //GET DEFAULT ACCOUNT FROM AND TO
                var lcQueryDefaultAccount = $"RSP_GS_GET_MIN_MAX_GLACCOUNT_NO '{poParameter.CCOMPANY_ID}', 'N' ";
                loCommand.CommandText = lcQueryDefaultAccount;
                loCommand.CommandType = CommandType.Text;
                _loggerGLR00300.LogInfo("Execute query initial process to get default account from and account to");
                _loggerGLR00300.LogDebug("{@ObjectQuery (2)} ", lcQueryDefaultAccount);

                var loReturnAccount = loDb.SqlExecQuery(loConn, loCommand, true);
                var loResultDefaultAccount = R_Utility.R_ConvertTo<GLR00300InitialProcess>(loReturnAccount).FirstOrDefault();
                loResult.CMIN_GLACCOUNT_NO = loResultDefaultAccount.CMIN_GLACCOUNT_NO;
                loResult.CMIN_GLACCOUNT_NAME = loResultDefaultAccount.CMIN_GLACCOUNT_NAME;
                loResult.CMAX_GLACCOUNT_NO = loResultDefaultAccount.CMAX_GLACCOUNT_NO;
                loResult.CMAX_GLACCOUNT_NAME = loResultDefaultAccount.CMAX_GLACCOUNT_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLR00300.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _loggerGLR00300.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult;
        }
        public List<GLR00300DTO> GetTrialBalanceTypeList(GLR00300DBParameter poParameter)
        {
            string lcMethodName = nameof(GetTrialBalanceTypeList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGLR00300.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            List<GLR00300DTO> loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_GS_GET_GSB_CODE_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CAPPLICATION", DbType.String, 20, "BIMASAKTI"); 
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCLASS_ID", DbType.String, 40, "_GL_TRIAL_BAL_TYPE");
                loDb.R_AddCommandParameter(loCommand, "@CLANGUAGE_ID", DbType.String, 2, poParameter.CLANGUAGE_ID);
                
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGLR00300.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<GLR00300DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLR00300.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _loggerGLR00300.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult;
        }
        public List<GLR00300DTO> GetPrintMethodTypeList(GLR00300DBParameter poParameter)
        {
            string lcMethodName = nameof(GetPrintMethodTypeList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGLR00300.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            List<GLR00300DTO> loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = "RSP_GS_GET_GSB_CODE_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CAPPLICATION", DbType.String, 20, "BIMASAKTI");
                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CCLASS_ID", DbType.String, 40, "_GL_TRIAL_BAL_PRINT_METHOD");
                loDb.R_AddCommandParameter(loCommand, "@CLANGUAGE_ID", DbType.String, 2, poParameter.CLANGUAGE_ID);
                
                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGLR00300.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);
                
                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<GLR00300DTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLR00300.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _loggerGLR00300.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult;
        }
        public List<GLR00300BudgetNoDTO> GetBudgetNoList(GLR00300DBParameter poParameter)
        {
            string lcMethodName = nameof(GetBudgetNoList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGLR00300.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

            R_Exception loException = new R_Exception();
            List<GLR00300BudgetNoDTO> loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_GL_GET_BUDGET_FOR_TB_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANGUAGE_ID", DbType.String, 10, poParameter.CLANGUAGE_ID);
                loDb.R_AddCommandParameter(loCommand, "@CYEAR", DbType.String, 10, poParameter.CYEAR);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_TYPE", DbType.String, 10, poParameter.CCURRENCY_TYPE);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGLR00300.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<GLR00300BudgetNoDTO>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLR00300.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _loggerGLR00300.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult;
        }
        public List<GLR00300_DataDetail_AccountTrialBalance> GetAllTrialBalanceReportData(GLR00300ParamDBToGetReportDTO poParameter)
        {
            string lcMethodName = nameof(GetAllTrialBalanceReportData);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGLR00300.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            R_Exception loException = new R_Exception();
            List<GLR00300_DataDetail_AccountTrialBalance> loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString); //loDb.GetConnection("R_ReportConnectionString");
                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_GL_REP_TRIAL_BALANCE"; //RSP_GL_REP_TRIAL_BALANCE
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);
                loDb.R_AddCommandParameter(loCommand, "@CLANGUAGE_ID", DbType.String, 6, poParameter.CLANGUAGE_ID);

                loDb.R_AddCommandParameter(loCommand, "@CTB_TYPE", DbType.String, 2, poParameter.CTB_TYPE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CJOURNAL_ADJ_MODE", DbType.String, 2, poParameter.CJOURNAL_ADJ_MODE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_TYPE", DbType.String, 2, poParameter.CCURRENCY_TYPE_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CFROM_ACCOUNT_NO", DbType.String, 20, poParameter.CFROM_ACCOUNT_NO);
                loDb.R_AddCommandParameter(loCommand, "@CTO_ACCOUNT_NO", DbType.String, 20, poParameter.CTO_ACCOUNT_NO);
                loDb.R_AddCommandParameter(loCommand, "@CFROM_CENTER_CODE", DbType.String, 20, poParameter.CFROM_CENTER_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CTO_CENTER_CODE", DbType.String, 20, poParameter.CTO_CENTER_CODE);

                loDb.R_AddCommandParameter(loCommand, "@CYEAR", DbType.String, 4, poParameter.CYEAR);
                loDb.R_AddCommandParameter(loCommand, "@CFROM_PERIOD_NO", DbType.String, 2, poParameter.CFROM_PERIOD_NO);
                loDb.R_AddCommandParameter(loCommand, "@CTO_PERIOD_NO", DbType.String, 2, poParameter.CTO_PERIOD_NO);
                loDb.R_AddCommandParameter(loCommand, "@CPRINT_METHOD", DbType.String, 10, poParameter.CPRINT_METHOD_CODE);
                loDb.R_AddCommandParameter(loCommand, "@CBUDGET_NO", DbType.String, 20, poParameter.CBUDGET_NO);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGLR00300.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<GLR00300_DataDetail_AccountTrialBalance>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLR00300.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _loggerGLR00300.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

            return loResult;
        }
        public List<GLR00300GetPeriod> GetPeriodList(GLR00300DBParameterDTO poParameter)
        {
            string lcMethodName = nameof(GetPeriodList);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerGLR00300.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));
            R_Exception loException = new R_Exception();
            List<GLR00300GetPeriod> loResult = null;
            R_Db loDb;
            DbCommand loCommand;
            try
            {
                loDb = new R_Db();
                var loConn = loDb.GetConnection();
                loCommand = loDb.GetCommand();
                var lcQuery = @"RSP_GS_GET_PERIOD_DT_LIST";
                loCommand.CommandText = lcQuery;
                loCommand.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 10, poParameter.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCommand, "@CYEAR", DbType.String, 10, poParameter.PERIOD_YEAR);

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                    .Where(x => x != null && x.ParameterName.StartsWith("@"))
                    .ToDictionary(x => x.ParameterName, x => x.Value);
                _loggerGLR00300.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

                var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, true);
                loResult = R_Utility.R_ConvertTo<GLR00300GetPeriod>(loReturnTemp).ToList();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerGLR00300.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _loggerGLR00300.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            return loResult;
        }

        public GLR00300HeaderAccountTrialBalanceDTO GetBaseHeaderLogoCompany(GLR00300ParamDBToGetReportDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("GetBaseHeaderLogoCompany");
            var loEx = new R_Exception();
            GLR00300HeaderAccountTrialBalanceDTO loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
                var loCmd = loDb.GetCommand();


                var lcQuery = "SELECT dbo.RFN_GET_COMPANY_LOGO(@CCOMPANY_ID) as CLOGO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.Text;
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 15, poParameter.CCOMPANY_ID);

                //Debug Logs
                var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@")).Select(x => x.Value);
                _loggerGLR00300.LogDebug("SELECT dbo.RFN_GET_COMPANY_LOGO({@CCOMPANY_ID}) as CLOGO", loDbParam);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
                loResult = R_Utility.R_ConvertTo<GLR00300HeaderAccountTrialBalanceDTO>(loDataTable).FirstOrDefault();

                lcQuery = "RSP_GS_GET_COMPANY_INFO";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;
                //Debug Logs
                _loggerGLR00300.LogDebug(string.Format("RSP_GS_GET_COMPANY_INFO", loDbParam));
                loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                var loCompanyNameResult = R_Utility.R_ConvertTo<GLR00300HeaderAccountTrialBalanceDTO>(loDataTable).FirstOrDefault();
                loResult.CDATETIME_NOW = loCompanyNameResult.CDATETIME_NOW;
                loResult.CCOMPANY_NAME = loCompanyNameResult.CCOMPANY_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerGLR00300.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }




    }
}