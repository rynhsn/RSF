using System.Data;
using System.Data.Common;
using System.Diagnostics;
using GLR00100Common;
using GLR00100Common.DTOs;
using GLR00100Common.DTOs.Print;
using R_BackEnd;
using R_Common;

namespace GLR00100Back;

public class GLR00100Cls
{
    private LoggerGLR00100 _logger;
    private readonly ActivitySource _activitySource;

    public GLR00100Cls()
    {
        _logger = LoggerGLR00100.R_GetInstanceLogger();
        _activitySource = GLR00100Activity.R_GetInstanceActivitySource();
    }

    public GLR00100SystemParamDTO GetSystemParamDb(GLR00100ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetSystemParamDb));
        R_Exception loEx = new();
        GLR00100SystemParamDTO loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = $"EXEC RSP_GL_GET_SYSTEM_PARAM '{poParams.CCOMPANY_ID}', '{poParams.CLANGUAGE_ID}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLR00100SystemParamDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public GLR00100PeriodDTO GetPeriodDb(GLR00100ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetPeriodDb));
        R_Exception loEx = new();
        GLR00100PeriodDTO loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = $"EXEC RSP_GS_GET_PERIOD_YEAR_RANGE '{poParams.CCOMPANY_ID}', '', ''";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLR00100PeriodDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
    
    public List<GLR00100TransCodeDTO> GetTransCodeListDb(GLR00100ParameterDb poParameterDb)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetTransCodeListDb));
        R_Exception loEx = new();
        List<GLR00100TransCodeDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = $"EXEC RSP_GS_GET_TRANS_CODE_LIST '{poParameterDb.CCOMPANY_ID}', '{poParameterDb.CUSER_ID}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;
            
            _logger.LogDebug("{pcQuery}", lcQuery);
            
            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<GLR00100TransCodeDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
    
    public List<GLR00100PeriodDTDTO> GetPeriodListDb(GLR00100ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetPeriodListDb));
        R_Exception loEx = new();
        List<GLR00100PeriodDTDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_PERIOD_DT_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, poParam.CYEAR);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CYEAR"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<GLR00100PeriodDTDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
    
    public GLR00100PrintBaseHeaderLogoDTO GetBaseHeaderLogoCompany(string pcCompanyId)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetBaseHeaderLogoCompany));
        var loEx = new R_Exception();
        GLR00100PrintBaseHeaderLogoDTO loResult = null;
        R_Db loDb = null; // Database object    
        DbConnection loConn = null;
        DbCommand loCmd = null;
    
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
            loCmd = loDb.GetCommand();
    
    
            var lcQuery = $"SELECT dbo.RFN_GET_COMPANY_LOGO('{pcCompanyId}') as BLOGO";
            loCmd.CommandText = lcQuery;
            loCmd.CommandType = CommandType.Text;
            
            _logger.LogDebug("{pcQuery}", lcQuery);
    
            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
            loResult = R_Utility.R_ConvertTo<GLR00100PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();
            
            //ambil company name
            lcQuery = $"SELECT CCOMPANY_NAME FROM SAM_COMPANIES WHERE CCOMPANY_ID = '{pcCompanyId}'"; // Query to get company name
            loCmd.CommandText = lcQuery;
            loCmd.CommandType = CommandType.Text;

            //Debug Logs
            _logger.LogDebug(string.Format("SELECT CCOMPANY_NAME FROM SAM_COMPANIES WHERE CCOMPANY_ID = '@CCOMPANY_ID'", pcCompanyId));
            loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
            var loCompanyNameResult = R_Utility.R_ConvertTo<GLR00100PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();

            loResult!.CCOMPANY_NAME = loCompanyNameResult?.CCOMPANY_NAME;

        }
        catch (Exception ex)
        {
            loEx.Add(ex); // Add the exception to the exception object
            _logger.LogError(loEx); // Log the exception
        }
        finally
        {
            if (loConn != null)
            {
                if (loConn.State != ConnectionState.Closed)
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

    public List<GLR00100ResultActivityReportDTO> BasedOnDateReportDb(GLR00100ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(BasedOnDateReportDb));
        var loEx = new R_Exception();
        List<GLR00100ResultActivityReportDTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_REP_ACTIVITY_BY_DATE";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParams.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CREPORT_TYPE", DbType.String, 1, "D");
            loDb.R_AddCommandParameter(loCmd, "@CFROM_DEPT_CODE", DbType.String, 20, poParams.CFROM_DEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CTO_DEPT_CODE", DbType.String, 20, poParams.CTO_DEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD_TYPE", DbType.String, 1, poParams.CPERIOD_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_DATE", DbType.String, 8, poParams.CFROM_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CTO_DATE", DbType.String, 8, poParams.CTO_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, 1, poParams.CCURRENCY_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 2, poParams.CLANGUAGE_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CUSER_ID" or
                        "@CREPORT_TYPE" or
                        "@CFROM_DEPT_CODE" or
                        "@CTO_DEPT_CODE" or
                        "@CPERIOD_TYPE" or
                        "@CFROM_DATE" or
                        "@CTO_DATE" or
                        "@CCURRENCY_TYPE" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLR00100ResultActivityReportDTO>(DataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
    
    public List<GLR00100ResultActivitySubReportDTO> BasedOnDateSubReportDb(GLR00100ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(BasedOnDateReportDb));
        var loEx = new R_Exception();
        List<GLR00100ResultActivitySubReportDTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_REP_ACTIVITY_BY_DATE";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParams.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CREPORT_TYPE", DbType.String, 1, "S");
            loDb.R_AddCommandParameter(loCmd, "@CFROM_DEPT_CODE", DbType.String, 20, poParams.CFROM_DEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CTO_DEPT_CODE", DbType.String, 20, poParams.CTO_DEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD_TYPE", DbType.String, 1, poParams.CPERIOD_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_DATE", DbType.String, 8, poParams.CFROM_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CTO_DATE", DbType.String, 8, poParams.CTO_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, 1, poParams.CCURRENCY_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 2, poParams.CLANGUAGE_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CUSER_ID" or
                        "@CREPORT_TYPE" or
                        "@CFROM_DEPT_CODE" or
                        "@CTO_DEPT_CODE" or
                        "@CPERIOD_TYPE" or
                        "@CFROM_DATE" or
                        "@CTO_DATE" or
                        "@CCURRENCY_TYPE" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLR00100ResultActivitySubReportDTO>(DataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public List<GLR00100ResultActivityReportDTO> BasedOnTransCodeReportDb(GLR00100ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(BasedOnTransCodeReportDb));
        var loEx = new R_Exception();
        List<GLR00100ResultActivityReportDTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_REP_ACTIVITY_BY_TRANS_CODE";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParams.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CREPORT_TYPE", DbType.String, 1, "D");
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 20, poParams.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_DEPT_CODE", DbType.String, 20, poParams.CFROM_DEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CTO_DEPT_CODE", DbType.String, 20, poParams.CTO_DEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD_TYPE", DbType.String, 1, poParams.CPERIOD_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_DATE", DbType.String, 8, poParams.CFROM_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CTO_DATE", DbType.String, 8, poParams.CTO_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CSORT_BY", DbType.String, 1, poParams.CSORT_BY);
            loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, 1, poParams.CCURRENCY_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 2, poParams.CLANGUAGE_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CUSER_ID" or
                        "@CREPORT_TYPE" or
                        "@CTRANS_CODE" or
                        "@CFROM_DEPT_CODE" or
                        "@CTO_DEPT_CODE" or
                        "@CPERIOD_TYPE" or
                        "@CFROM_DATE" or
                        "@CTO_DATE" or
                        "@CSORT_BY" or
                        "@CCURRENCY_TYPE" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLR00100ResultActivityReportDTO>(DataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
    
    public List<GLR00100ResultActivitySubReportDTO> BasedOnTransCodeSubReportDb(GLR00100ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(BasedOnTransCodeSubReportDb));
        var loEx = new R_Exception();
        List<GLR00100ResultActivitySubReportDTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_REP_ACTIVITY_BY_TRANS_CODE";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParams.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CREPORT_TYPE", DbType.String, 1, "S");
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 20, poParams.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_DEPT_CODE", DbType.String, 20, poParams.CFROM_DEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CTO_DEPT_CODE", DbType.String, 20, poParams.CTO_DEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD_TYPE", DbType.String, 1, poParams.CPERIOD_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_DATE", DbType.String, 8, poParams.CFROM_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CTO_DATE", DbType.String, 8, poParams.CTO_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CSORT_BY", DbType.String, 1, poParams.CSORT_BY);
            loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, 1, poParams.CCURRENCY_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 2, poParams.CLANGUAGE_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CUSER_ID" or
                        "@CREPORT_TYPE" or
                        "@CTRANS_CODE" or
                        "@CFROM_DEPT_CODE" or
                        "@CTO_DEPT_CODE" or
                        "@CPERIOD_TYPE" or
                        "@CFROM_DATE" or
                        "@CTO_DATE" or
                        "@CSORT_BY" or
                        "@CCURRENCY_TYPE" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLR00100ResultActivitySubReportDTO>(DataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public List<GLR00100ResultActivityReportDTO> BasedOnRefNoReportDb(GLR00100ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(BasedOnRefNoReportDb));
        var loEx = new R_Exception();
        List<GLR00100ResultActivityReportDTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_REP_ACTIVITY_BY_REF_NO";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParams.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CREPORT_TYPE", DbType.String, 1, "D");
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 20, poParams.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_DEPT_CODE", DbType.String, 20, poParams.CFROM_DEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CTO_DEPT_CODE", DbType.String, 20, poParams.CTO_DEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_REF_NO", DbType.String, 30, poParams.CFROM_REF_NO);
            loDb.R_AddCommandParameter(loCmd, "@CTO_REF_NO", DbType.String, 30, poParams.CTO_REF_NO);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD_TYPE", DbType.String, 1, poParams.CPERIOD_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_DATE", DbType.String, 8, poParams.CFROM_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CTO_DATE", DbType.String, 8, poParams.CTO_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, 1, poParams.CCURRENCY_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 2, poParams.CLANGUAGE_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CUSER_ID" or
                        "@CREPORT_TYPE" or
                        "@CTRANS_CODE" or
                        "@CFROM_DEPT_CODE" or
                        "@CTO_DEPT_CODE" or
                        "@CFROM_REF_NO" or
                        "@CTO_REF_NO" or
                        "@CPERIOD_TYPE" or
                        "@CFROM_DATE" or
                        "@CTO_DATE" or
                        "@CCURRENCY_TYPE" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLR00100ResultActivityReportDTO>(DataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
    
    public List<GLR00100ResultActivitySubReportDTO> BasedOnRefNoSubReportDb(GLR00100ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(BasedOnRefNoSubReportDb));
        var loEx = new R_Exception();
        List<GLR00100ResultActivitySubReportDTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GL_REP_ACTIVITY_BY_REF_NO";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poParams.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CREPORT_TYPE", DbType.String, 1, "S");
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 20, poParams.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_DEPT_CODE", DbType.String, 20, poParams.CFROM_DEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CTO_DEPT_CODE", DbType.String, 20, poParams.CTO_DEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_REF_NO", DbType.String, 30, poParams.CFROM_REF_NO);
            loDb.R_AddCommandParameter(loCmd, "@CTO_REF_NO", DbType.String, 30, poParams.CTO_REF_NO);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD_TYPE", DbType.String, 1, poParams.CPERIOD_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_DATE", DbType.String, 8, poParams.CFROM_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CTO_DATE", DbType.String, 8, poParams.CTO_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CCURRENCY_TYPE", DbType.String, 1, poParams.CCURRENCY_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 2, poParams.CLANGUAGE_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CUSER_ID" or
                        "@CREPORT_TYPE" or
                        "@CTRANS_CODE" or
                        "@CFROM_DEPT_CODE" or
                        "@CTO_DEPT_CODE" or
                        "@CFROM_REF_NO" or
                        "@CTO_REF_NO" or
                        "@CPERIOD_TYPE" or
                        "@CFROM_DATE" or
                        "@CTO_DATE" or
                        "@CCURRENCY_TYPE" or
                        "@CLANGUAGE_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLR00100ResultActivitySubReportDTO>(DataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

}