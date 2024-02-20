using System.Data;
using System.Data.Common;
using System.Diagnostics;
using GLI00100Common;
using GLI00100Common.DTOs;
using R_BackEnd;
using R_Common;

namespace GLI00100Back;

public class GLI00100InitCls
{
    private LoggerGLI00100 _logger;
    private readonly ActivitySource _activitySource;

    public GLI00100InitCls()
    {
        _logger = LoggerGLI00100.R_GetInstanceLogger();
        _activitySource = GLI00100Activity.R_GetInstanceActivitySource();
    }
    
    public GLI00100GSMCompanyDTO GLI00100GetCompanyDb(GLI00100InitParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GLI00100GetCompanyDb));
        R_Exception loEx = new();
        GLI00100GSMCompanyDTO loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = $"EXEC RSP_GS_GET_COMPANY_INFO '{poParams.CCOMPANY_ID}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;
            
            _logger.LogDebug("{pcQuery}", lcQuery);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLI00100GSMCompanyDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
    
    public GLI00100GLSystemParamDTO GLI00100GetSystemParamDb(GLI00100InitParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GLI00100GetSystemParamDb));
        R_Exception loEx = new();
        GLI00100GLSystemParamDTO loReturn = null;
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

            loReturn = R_Utility.R_ConvertTo<GLI00100GLSystemParamDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
    
    public GLI00100GSMPeriodDTO GLI00100GetPeriodDb(GLI00100InitParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GLI00100GetPeriodDb));
        R_Exception loEx = new();
        GLI00100GSMPeriodDTO loReturn = null;
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

            loReturn = R_Utility.R_ConvertTo<GLI00100GSMPeriodDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public GLI00100PeriodInfoDTO GLI00100GetPeriodInfoDb(GLI00100InitParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GLI00100GetPeriodInfoDb));
        R_Exception loEx = new();
        GLI00100PeriodInfoDTO loReturn = new();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();  
            loCmd = loDb.GetCommand();

            lcQuery = $"EXEC RSP_GS_GET_PERIOD_YEAR_INFO '{poParams.CCOMPANY_ID}', '{poParams.CYEAR}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;
            
            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loReturn = R_Utility.R_ConvertTo<GLI00100PeriodInfoDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
    
    public List<GLI00100AccountGridDTO> GLI00100GetAccountListDb(GLI00100InitParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GLI00100GetAccountListDb));
        R_Exception loEx = new();
        List<GLI00100AccountGridDTO> loReturn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = $"EXEC RSP_GL_GET_ACCOUNT_LIST '{poParams.CCOMPANY_ID}', '{poParams.CLANGUAGE_ID}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;
            
            _logger.LogDebug("{pcQuery}", lcQuery);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<GLI00100AccountGridDTO>(DataTable).ToList();
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
