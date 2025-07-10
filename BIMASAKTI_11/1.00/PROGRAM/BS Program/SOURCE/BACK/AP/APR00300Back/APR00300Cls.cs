using System.Data;
using System.Data.Common;
using System.Diagnostics;
using APR00300Common;
using APR00300Common.DTOs;
using APR00300Common.DTOs.Print;
using R_BackEnd;
using R_Common;

namespace APR00300Back;

public class APR00300Cls
{
    private LoggerAPR00300 _logger; // Logger
    private readonly ActivitySource _activitySource; // ActivitySource

    /*
     * Constructor
     * Digunakan untuk inisialisasi logger dan activity source
     * kemudian mendapatkan instance logger
     * dan instance activity source
     */
    public APR00300Cls()
    {
        _logger = LoggerAPR00300.R_GetInstanceLogger(); // Get instance of logger
        _activitySource = APR00300Activity.R_GetInstanceActivitySource(); // Get instance of activity source
    }

    /*
     * Get Property List
     * Digunakan untuk mendapatkan daftar property
     * kemudian dikirim sebagai response ke controller dalam bentuk List<APR00300PropertyDTO>
     */
    public List<APR00300PropertyDTO> GetPropertyList(APR00300ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetPropertyList)); // Start activity
        R_Exception loEx = new(); // Create new exception object
        List<APR00300PropertyDTO> loRtn = null; // Create new list of APR00300PropertyDTO
        R_Db loDb; // Database object
        DbConnection loConn; // Database connection object
        DbCommand loCmd; // Database command object
        string lcQuery; // Query
        try
        {
            loDb = new R_Db(); // Create new instance of R_Db
            loConn = loDb.GetConnection(); // Get database connection
            loCmd = loDb.GetCommand(); // Get database command
 
            lcQuery = @$"EXEC RSP_GS_GET_PROPERTY_LIST '{poParam.CCOMPANY_ID}', '{poParam.CUSER_ID}'"; // Query to get property list
            loCmd.CommandType = CommandType.Text; // Set command type to text
            loCmd.CommandText = lcQuery; // Set command text to query

            _logger.LogDebug("{pcQuery}", lcQuery); // Log the query

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true); // Execute the query

            loRtn = R_Utility.R_ConvertTo<APR00300PropertyDTO>(loDataTable).ToList(); // Convert the data table to list of APR00300PropertyDTO
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

    /*
     * Get Year Range
     * Digunakan untuk mendapatkan range tahun
     * kemudian dikirim sebagai response ke controller dalam bentuk APR00300PeriodYearRangeDTO
     */
    public APR00300PeriodYearRangeDTO GetYearRange(APR00300ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetYearRange)); // Start activity
        R_Exception loEx = new(); // Create new exception object
        APR00300PeriodYearRangeDTO loReturn = new(); // Create new instance of APR00300PeriodYearRangeDTO
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

            loReturn = R_Utility.R_ConvertTo<APR00300PeriodYearRangeDTO>(DataTable).FirstOrDefault(); // Convert the data table to APR00300PeriodYearRangeDTO
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
     * Get System Param
     * Digunakan untuk mendapatkan Tanggal Hari Ini
     * kemudian dikirim sebagai response ke controller dalam bentuk APR00300TodayDTO
     */
    public APR00300TodayDTO GetToday(APR00300ParameterDb poParams) 
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetToday)); // Start activity
        R_Exception loEx = new(); // Create new exception object
        APR00300TodayDTO loReturn = new(); // Create new instance of APR00300TodayDTO
        R_Db loDb; // Database object
        DbConnection loConn; // Database connection object
        DbCommand loCmd; // Database command object
        string lcQuery; // Query

        try
        {
            loDb = new R_Db(); // Create new instance of R_Db
            loConn = loDb.GetConnection(); // Get database connection
            loCmd = loDb.GetCommand(); // Get database command

            lcQuery = $"SELECT dbo.RFN_GET_DB_TODAY ('{poParams.CCOMPANY_ID}') as 'DTODAY'"; // Query to get today date
            loCmd.CommandType = CommandType.Text; // Set command type to text
            loCmd.CommandText = lcQuery; // Set command text to query

            _logger.LogDebug("{pcQuery}", lcQuery); // Log the query

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true); // Execute the query

            loReturn = R_Utility.R_ConvertTo<APR00300TodayDTO>(DataTable).FirstOrDefault(); // Convert the data table to APR00300TodayDTO
        }
        catch (Exception ex)
        {
            loEx.Add(ex); // Add the exception to the exception object
            _logger.LogError(loEx); // Log the exception
        }

        loEx.ThrowExceptionIfErrors(); // Throw exception if there are errors
        return loReturn; // Return the today date
    }
    
    /*
     * Get Report Data
     * Digunakan untuk mendapatkan data laporan
     * kemudian dikirim sebagai response ke controller dalam bentuk List<APR00300DataResultDTO>
     */
    public List<APR00300DataResultDTO> GetReportData(APR00300ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetReportData)); // Start activity
        R_Exception loEx = new(); // Create new exception object
        List<APR00300DataResultDTO> loRtn = null; // Create new list of APR00300DataResultDTO
        R_Db loDb; // Database object
        DbConnection loConn; // Database connection object
        DbCommand loCmd; // Database command object
        string lcQuery; // Query
        try
        { 
            loDb = new R_Db(); // Create new instance of R_Db
            loConn = loDb.GetConnection(); // Get database connection
            loCmd = loDb.GetCommand(); // Get database command

            lcQuery = "RSP_APR00300_GET_REPORT"; // Query to get report data
            loCmd.CommandType = CommandType.StoredProcedure; // Set command type to stored procedure
            loCmd.CommandText = lcQuery; // Set command text to query

            /*
             * Add Parameter
             * Digunakan untuk menambahkan parameter ke dalam command
             * yang akan digunakan dalam query
             */
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_SUPPLIER_ID", DbType.String, 20, poParam.CFROM_SUPPLIER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTO_SUPPLIER_ID", DbType.String, 20, poParam.CTO_SUPPLIER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CCUT_OFF_DATE", DbType.String, 8, poParam.CCUT_OFF_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_PERIOD", DbType.String, 6, poParam.CFROM_PERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CTO_PERIOD", DbType.String, 6, poParam.CTO_PERIOD);
            loDb.R_AddCommandParameter(loCmd, "@LINCLUDE_ZERO_BALANCE", DbType.Boolean, 1, poParam.LINCLUDE_ZERO_BALANCE);
            loDb.R_AddCommandParameter(loCmd, "@LSHOW_AGE_TOTAL", DbType.Boolean, 1, poParam.LSHOW_AGE_TOTAL);
            loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 2, poParam.CLANG_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CFROM_SUPPLIER_ID" or
                        "@CTO_SUPPLIER_ID" or
                        "@CCUT_OFF_DATE" or
                        "@CFROM_PERIOD" or
                        "@CTO_PERIOD" or
                        "@LINCLUDE_ZERO_BALANCE" or
                        "@LSHOW_AGE_TOTAL" or
                        "@CLANG_ID"
                )
                .Select(x => x.Value);

            /*
             * Log Debug
             * Digunakan untuk melakukan log debug
             * yang berisi query dan parameter
             */
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true); // Execute the query

            loRtn = R_Utility.R_ConvertTo<APR00300DataResultDTO>(loDataTable).ToList(); // Convert the data table to list of APR00300DataResultDTO
        }
        catch (Exception ex)
        {
            loEx.Add(ex); // Add the exception to the exception object
            _logger.LogError(loEx); // Log the exception
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors(); // Throw exception if there are errors

        return loRtn; // Return the report data
    }
    
    /*
     * Get Base Header Logo Company
     * Digunakan untuk mendapatkan logo perusahaan
     * kemudian dikirim sebagai response ke controller dalam bentuk APR00300PrintBaseHeaderLogoDTO
     */
    public APR00300PrintBaseHeaderLogoDTO GetBaseHeaderLogoCompany(string pcCompanyId)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetBaseHeaderLogoCompany)); // Start activity
        var loEx = new R_Exception(); // Create new exception object
        APR00300PrintBaseHeaderLogoDTO loResult = null; // Create new instance of APR00300PrintBaseHeaderLogoDTO
        R_Db loDb = null; // Database object    
        DbConnection loConn = null;
        DbCommand loCmd = null;

        try
        {
            loDb = new R_Db(); // Create new instance of R_Db
            loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString); // Get database connection
            loCmd = loDb.GetCommand(); // Get database command

            var lcQuery = $"SELECT dbo.RFN_GET_COMPANY_LOGO('{pcCompanyId}') as BLOGO"; // Query to get company logo
            loCmd.CommandText = lcQuery; // Set command text to query
            loCmd.CommandType = CommandType.Text; // Set command type to text

            _logger.LogDebug("{pcQuery}", lcQuery); // Log the query

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false); // Execute the query
            loResult = R_Utility.R_ConvertTo<APR00300PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault(); // Convert the data table to APR00300PrintBaseHeaderLogoDTO
            
            //ambil company name
            lcQuery = $"EXEC RSP_GS_GET_COMPANY_INFO '{pcCompanyId}'"; // Query to get company name
            loCmd.CommandText = lcQuery;
            loCmd.CommandType = CommandType.Text;

            //Debug Logs
            _logger.LogDebug(lcQuery);
            loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
            var loCompanyNameResult = R_Utility.R_ConvertTo<APR00300PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();

            loResult!.CCOMPANY_NAME = loCompanyNameResult?.CCOMPANY_NAME;
            loResult.CDATETIME_NOW = loCompanyNameResult.CDATETIME_NOW;
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


        loEx.ThrowExceptionIfErrors(); // Throw exception if there are errors
        return loResult; // Return the company logo
    }
}