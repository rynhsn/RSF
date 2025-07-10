using System.Data;
using System.Data.Common;
using System.Diagnostics;
using ICR00100Common;
using ICR00100Common.DTOs;
using ICR00100Common.DTOs.Print;
using R_BackEnd;
using R_Common;

namespace ICR00100Back;

public class ICR00100Cls
{
    private LoggerICR00100 _logger; // Logger
    private readonly ActivitySource _activitySource; // ActivitySource

    /*
     * Constructor
     * Digunakan untuk inisialisasi logger dan activity source
     * kemudian mendapatkan instance logger
     * dan instance activity source
     */
    public ICR00100Cls()
    {
        _logger = LoggerICR00100.R_GetInstanceLogger(); // Get instance of logger
        _activitySource = ICR00100Activity.R_GetInstanceActivitySource(); // Get instance of activity source
    }


    /*
     * Get Year Range
     * Digunakan untuk mendapatkan range tahun
     * kemudian dikirim sebagai response ke controller dalam bentuk ICR00100PeriodYearRangeDTO
     */
    public ICR00100PeriodYearRangeDTO GetYearRange(ICR00100ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetYearRange)); // Start activity
        R_Exception loEx = new(); // Create new exception object
        ICR00100PeriodYearRangeDTO loReturn = new(); // Create new instance of ICR00100PeriodYearRangeDTO
        R_Db loDb; // Database object
        DbConnection loConn; // Database connection object
        DbCommand loCmd; // Database command object
        string lcQuery; // Query

        try
        {
            loDb = new R_Db(); // Create new instance of R_Db
            loConn = loDb.GetConnection(); // Get database connection
            loCmd = loDb.GetCommand(); // Get database command

            lcQuery = $"EXEC RSP_GS_GET_PERIOD_YEAR_RANGE '{poParams.CCOMPANY_ID}', '', ''"; // Query to get year range
            loCmd.CommandType = CommandType.Text; // Set command type to text
            loCmd.CommandText = lcQuery; // Set command text to query

            _logger.LogDebug("{pcQuery}", lcQuery); // Log the query

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true); // Execute the query

            loReturn = R_Utility.R_ConvertTo<ICR00100PeriodYearRangeDTO>(DataTable)
                .FirstOrDefault(); // Convert the data table to ICR00100PeriodYearRangeDTO
        }
        catch (Exception ex)
        {
            loEx.Add(ex); // Add the exception to the exception object
            _logger.LogError(loEx); // Log the exception
        }

        loEx.ThrowExceptionIfErrors(); // Throw exception if there are errors
        return loReturn; // Return the year range
    }

    /*
     * Get Property List
     * Digunakan untuk mendapatkan daftar property
     * kemudian dikirim sebagai response ke controller dalam bentuk List<ICR00100PropertyDTO>
     */
    public List<ICR00100PropertyDTO> GetPropertyList(ICR00100ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetPropertyList)); // Start activity
        R_Exception loEx = new(); // Create new exception object
        List<ICR00100PropertyDTO> loRtn = null; // Create new list of ICR00100PropertyDTO
        R_Db loDb; // Database object
        DbConnection loConn; // Database connection object
        DbCommand loCmd; // Database command object
        string lcQuery; // Query
        try
        {
            loDb = new R_Db(); // Create new instance of R_Db
            loConn = loDb.GetConnection(); // Get database connection
            loCmd = loDb.GetCommand(); // Get database command

            lcQuery =
                @$"EXEC RSP_GS_GET_PROPERTY_LIST '{poParam.CCOMPANY_ID}', '{poParam.CUSER_ID}'"; // Query to get property list
            loCmd.CommandType = CommandType.Text; // Set command type to text
            loCmd.CommandText = lcQuery; // Set command text to query

            _logger.LogDebug("{pcQuery}", lcQuery); // Log the query

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true); // Execute the query

            loRtn = R_Utility.R_ConvertTo<ICR00100PropertyDTO>(loDataTable)
                .ToList(); // Convert the data table to list of ICR00100PropertyDTO
        }
        catch (Exception ex)
        {
            loEx.Add(ex); // Add the exception to the exception object
            _logger.LogError(loEx); // Log the exception
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors(); // Throw exception if there are errors

        return loRtn; // Return the property list
    }

    public ICR00100PrintBaseHeaderLogoDTO GetBaseHeaderLogoCompany(string pcCompanyId)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetBaseHeaderLogoCompany));
        var loEx = new R_Exception();
        ICR00100PrintBaseHeaderLogoDTO loResult = null;
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
            loResult = R_Utility.R_ConvertTo<ICR00100PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();

            //ambil company name
            lcQuery = $"EXEC RSP_GS_GET_COMPANY_INFO '{pcCompanyId}'"; // Query to get company name
            loCmd.CommandText = lcQuery;
            loCmd.CommandType = CommandType.Text;

            //Debug Logs
            _logger.LogDebug(lcQuery);
            loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
            var loCompanyNameResult =
                R_Utility.R_ConvertTo<ICR00100PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();

            loResult!.CDATETIME_NOW = loCompanyNameResult.CDATETIME_NOW;
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


    public List<ICR00100DataResultDTO> GetReportData(ICR00100ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetReportData));
        R_Exception loEx = new();
        List<ICR00100DataResultDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_ICR00100_GET_REPORT";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CDATE_FILTER", DbType.String, 10, poParam.CDATE_FILTER);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 6, poParam.CPERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_DATE", DbType.String, 8, poParam.CFROM_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CTO_DATE", DbType.String, 8, poParam.CTO_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CWAREHOUSE_CODE", DbType.String, 20, poParam.CWAREHOUSE_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParam.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@LINC_FUTURE_TRANSACTION", DbType.Boolean, 1,
                poParam.LINC_FUTURE_TRANSACTION);
            loDb.R_AddCommandParameter(loCmd, "@CFILTER_BY", DbType.String, 20, poParam.CFILTER_BY);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_PROD_ID", DbType.String, 20, poParam.CFROM_PROD_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTO_PROD_ID", DbType.String, 20, poParam.CTO_PROD_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFILTER_DATA", DbType.String, 20, poParam.CFILTER_DATA);
            loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 3, poParam.CLANG_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CDATE_FILTER" or
                        "@CPERIOD" or
                        "@CFROM_DATE" or
                        "@CTO_DATE" or
                        "@CWAREHOUSE_CODE" or
                        "@CDEPT_CODE" or
                        "@LINC_FUTURE_TRANSACTION" or
                        "@CFILTER_BY" or
                        "@CFROM_PROD_ID" or
                        "@CTO_PROD_ID" or
                        "@CFILTER_DATA" or
                        "@CLANG_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<ICR00100DataResultDTO>(loDataTable).ToList();
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
}