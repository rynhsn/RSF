using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection.Metadata;
using PMR03000Common;
using PMR03000Common.DTOs;
using PMR03000Common.DTOs.Print;
using R_BackEnd;
using R_Common;

namespace PMR03000Back;

public class PMR03000Cls
{
    private LoggerPMR03000 _logger;
    private readonly ActivitySource _activitySource;

    public PMR03000Cls()
    {
        _logger = LoggerPMR03000.R_GetInstanceLogger();
        _activitySource = PMR03000Activity.R_GetInstanceActivitySource();
    }

    public async Task<List<PMR03000PropertyDTO>> GetPropertyList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetPropertyList));
        R_Exception loEx = new();
        List<PMR03000PropertyDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            lcQuery = @$"EXEC RSP_GS_GET_PROPERTY_LIST '{R_BackGlobalVar.COMPANY_ID}', '{R_BackGlobalVar.USER_ID}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMR03000PropertyDTO>(loDataTable).ToList();
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

    public async Task<List<PMR03000PeriodDTO>> GetPeriodList(PMR03000ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetPeriodList));
        R_Exception loEx = new();
        List<PMR03000PeriodDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = await loDb.GetConnectionAsync();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_PERIOD_DT_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, poParam.CYEAR);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CYEAR"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMR03000PeriodDTO>(loDataTable).ToList();
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

    public async Task<List<PMR03000ReportTemplateDTO>> GetReportTemplate(PMR03000ParameterDb poParam)
    {
        string? lcMethod = nameof(GetReportTemplate);
        _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
        R_Exception loException = new R_Exception();
        List<PMR03000ReportTemplateDTO>? loReturn = null;
        string lcQuery;
        DbCommand loCommand;
        R_Db loDb;
        try
        {
            _logger.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
            loDb = new();
            _logger.LogDebug("{@ObjectDb}", loDb);

            _logger.LogInfo(
                string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
            loCommand = loDb.GetCommand();
            _logger.LogDebug("{@ObjectDb}", loCommand);

            _logger.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));

            lcQuery = "RSP_GET_REPORT_TEMPLATE_LIST";
            _logger.LogDebug("{@ObjectQuery} ", lcQuery);

            _logger.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}",
                lcMethod));
            var loConn = await loDb.GetConnectionAsync();
            _logger.LogDebug("{@ObjectDbConnection}", loConn);

            _logger.LogInfo(string.Format(
                "Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
            loCommand.CommandText = lcQuery;
            loCommand.CommandType = CommandType.StoredProcedure;
            _logger.LogDebug("{@ObjectDbCommand}", loCommand);

            _logger.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, R_BackGlobalVar.COMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROGRAM_ID", DbType.String, 30, poParam.CPROGRAM_ID);
            loDb.R_AddCommandParameter(loCommand, "@CTEMPLATE_ID ", DbType.String, 30, poParam.CTEMPLATE_ID);

            var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@"))
                .ToDictionary(x => x.ParameterName, x => x.Value);
            _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

            _logger.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}",
                lcMethod));
            var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCommand, true);

            _logger.LogInfo(string.Format(
                "Convert the data in loDataTable to a list objects and assign it to loRtn in Method {0}",
                lcMethod));
            loReturn = R_Utility.R_ConvertTo<PMR03000ReportTemplateDTO>(loDataTable).ToList();
            _logger.LogDebug("{@ObjectReturn}", loReturn);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        if (loException.Haserror)
            _logger.LogError("{@ErrorObject}", loException.Message);

        loException.ThrowExceptionIfErrors();

        _logger.LogInfo(string.Format("End Method {0}", lcMethod));

        return loReturn!;
    }
    
    public async Task<List<PMR03000MessageInfoDTO>> GetMessageInfo(PMR03000ParameterDb poParam)
    {
        string? lcMethod = nameof(GetMessageInfo);
        _logger.LogInfo(string.Format("Start Method {0}", lcMethod));
        R_Exception loException = new R_Exception();
        List<PMR03000MessageInfoDTO>? loReturn = null;
        string lcQuery;
        DbCommand loCommand;
        R_Db loDb;
        try
        {
            _logger.LogInfo(string.Format("initialization R_Db in Method {0}", lcMethod));
            loDb = new();
            _logger.LogDebug("{@ObjectDb}", loDb);

            _logger.LogInfo(
                string.Format("Create a new command and assign it to loCommand in Method {0}", lcMethod));
            loCommand = loDb.GetCommand();
            _logger.LogDebug("{@ObjectDb}", loCommand);

            _logger.LogInfo(string.Format("Set the query string for lcQuery in Method {0}", lcMethod));

            lcQuery = "RSP_GS_GET_MESSAGE_LIST";
            _logger.LogDebug("{@ObjectQuery} ", lcQuery);

            _logger.LogInfo(string.Format("Get a database connection and assign it to loConn in Method {0}",
                lcMethod));
            var loConn = await loDb.GetConnectionAsync();
            _logger.LogDebug("{@ObjectDbConnection}", loConn);

            _logger.LogInfo(string.Format(
                "Set the command's text to lcQuery and type to StoredProcedure in Method {0}", lcMethod));
            loCommand.CommandText = lcQuery;
            loCommand.CommandType = CommandType.StoredProcedure;
            _logger.LogDebug("{@ObjectDbCommand}", loCommand);

            _logger.LogInfo(string.Format("Add command parameters in Method {0}", lcMethod));
            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, R_BackGlobalVar.COMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CMESSAGE_TYPE", DbType.String, 20, poParam.CMESSAGE_TYPE);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, R_BackGlobalVar.USER_ID);

            var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@"))
                .ToDictionary(x => x.ParameterName, x => x.Value);
            _logger.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

            _logger.LogInfo(string.Format("Execute the SQL query and store the result in loDataTable in Method {0}",
                lcMethod));
            var loDataTable = await loDb.SqlExecQueryAsync(loConn, loCommand, true);

            _logger.LogInfo(string.Format(
                "Convert the data in loDataTable to a list objects and assign it to loRtn in Method {0}",
                lcMethod));
            loReturn = R_Utility.R_ConvertTo<PMR03000MessageInfoDTO>(loDataTable).ToList();
            _logger.LogDebug("{@ObjectReturn}", loReturn);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        if (loException.Haserror)
            _logger.LogError("{@ErrorObject}", loException.Message);

        loException.ThrowExceptionIfErrors();

        _logger.LogInfo(string.Format("End Method {0}", lcMethod));

        return loReturn!;
    }
}