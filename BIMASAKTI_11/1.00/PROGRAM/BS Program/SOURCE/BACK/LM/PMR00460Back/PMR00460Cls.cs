using System.Data;
using System.Data.Common;
using System.Diagnostics;
using PMR00460Common;
using PMR00460Common.DTOs;
using PMR00460Common.DTOs.Print;
using R_BackEnd;
using R_Common;

namespace PMR00460Back;

public class PMR00460Cls
{
    private LoggerPMR00460 _logger; // Logger
    private readonly ActivitySource _activitySource; // ActivitySource

    /*
     * Constructor
     * Digunakan untuk inisialisasi logger dan activity source
     * kemudian mendapatkan instance logger
     * dan instance activity source
     */
    public PMR00460Cls()
    {
        _logger = LoggerPMR00460.R_GetInstanceLogger(); // Get instance of logger
        _activitySource = PMR00460Activity.R_GetInstanceActivitySource(); // Get instance of activity source
    }

    /*
     * Get Property List
     * Digunakan untuk mendapatkan daftar property
     * kemudian dikirim sebagai response ke controller dalam bentuk List<PMR00460PropertyDTO>
     */
    public List<PMR00460PropertyDTO> GetPropertyList(PMR00460ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetPropertyList)); // Start activity
        R_Exception loEx = new(); // Create new exception object
        List<PMR00460PropertyDTO> loRtn = null; // Create new list of PMR00460PropertyDTO
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

            loRtn = R_Utility.R_ConvertTo<PMR00460PropertyDTO>(loDataTable).ToList(); // Convert the data table to list of PMR00460PropertyDTO
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
     * kemudian dikirim sebagai response ke controller dalam bentuk PMR00460PeriodYearRangeDTO
     */
    public PMR00460PeriodYearRangeDTO GetYearRange(PMR00460ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetYearRange)); // Start activity
        R_Exception loEx = new(); // Create new exception object
        PMR00460PeriodYearRangeDTO loReturn = new(); // Create new instance of PMR00460PeriodYearRangeDTO
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

            loReturn = R_Utility.R_ConvertTo<PMR00460PeriodYearRangeDTO>(DataTable).FirstOrDefault(); // Convert the data table to PMR00460PeriodYearRangeDTO
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
     * Get Default Param
     * Digunakan untuk mendapatkan parameter default
     * kemudian dikirim sebagai response ke controller dalam bentuk PMR00460ReportParam
     */
    public PMR00460DefaultParamDTO GetDefaultParam(PMR00460ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetYearRange)); // Start activity
        R_Exception loEx = new(); // Create new exception object
        PMR00460DefaultParamDTO? loReturn = new(); // Create new instance of PMR00460DefaultParamDTO
        R_Db loDb; // Database object
        DbConnection loConn; // Database connection object
        DbCommand loCmd; // Database command object
        string lcQuery; // Query
 
        try
        {
            loDb = new R_Db(); // Create new instance of R_Db
            loConn = loDb.GetConnection(); // Get database connection
            loCmd = loDb.GetCommand(); // Get database command

            lcQuery = "RSP_PM_GET_REPORT_DEFAULT_PARAM"; // Query to get report data
            loCmd.CommandType = CommandType.StoredProcedure; // Set command type to stored procedure
            loCmd.CommandText = lcQuery; // Set command text to query

            /*
             * Add Parameter
             * Digunakan untuk menambahkan parameter ke dalam command
             * yang akan digunakan dalam query
             */
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);
            
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            /*
             * Log Debug
             * Digunakan untuk melakukan log debug
             * yang berisi query dan parameter
             */
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true); // Execute the query

            loReturn = R_Utility.R_ConvertTo<PMR00460DefaultParamDTO>(loDataTable).FirstOrDefault(); // Convert the data table to PMR00460PeriodYearRangeDTO
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
     * Get Report Data
     * Digunakan untuk mendapatkan data report
     * kemudian dikirim sebagai response ke controller dalam bentuk List<PMR00460DataResultDTO>
     */
    public List<PMR00460DataResultDTO> GetReportData(PMR00460ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetReportData)); // Start activity
        R_Exception loEx = new(); // Create new exception object
        List<PMR00460DataResultDTO> loRtn = null; // Create new list of PMR00460DataResultDTO
        R_Db loDb; // Database object
        DbConnection loConn; // Database connection object
        DbCommand loCmd; // Database command object
        string lcQuery; // Query
        try
        { 
            loDb = new R_Db(); // Create new instance of R_Db
            loConn = loDb.GetConnection(); // Get database connection
            loCmd = loDb.GetCommand(); // Get database command

            lcQuery = "RSP_PMR00460_GET_REPORT"; // Query to get report data
            loCmd.CommandType = CommandType.StoredProcedure; // Set command type to stored procedure
            loCmd.CommandText = lcQuery; // Set command text to query

            /*
             * Add Parameter
             * Digunakan untuk menambahkan parameter ke dalam command
             * yang akan digunakan dalam query
             */
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_BUILDING_ID", DbType.String, 20, poParam.CFROM_BUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTO_BUILDING_ID", DbType.String, 20, poParam.CTO_BUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_PERIOD", DbType.String, 20, poParam.CFROM_PERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CTO_PERIOD", DbType.String, 20, poParam.CTO_PERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CREPORT_TYPE", DbType.String, 20, poParam.CREPORT_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@LOPEN", DbType.Boolean, 1, poParam.LOPEN);
            loDb.R_AddCommandParameter(loCmd, "@LSCHEDULED", DbType.Boolean, 1, poParam.LSCHEDULED);
            loDb.R_AddCommandParameter(loCmd, "@LCONFIRMED", DbType.Boolean, 1, poParam.LCONFIRMED);
            loDb.R_AddCommandParameter(loCmd, "@LCLOSED", DbType.Boolean, 1, poParam.LCLOSED);
            loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 20, poParam.CLANG_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTYPE", DbType.String, 1, poParam.CTYPE);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CFROM_BUILDING_ID" or
                        "@CTO_BUILDING_ID" or
                        "@CFROM_PERIOD" or
                        "@CTO_PERIOD" or
                        "@CREPORT_TYPE" or
                        "@LOPEN" or
                        "@LSCHEDULED" or
                        "@LCONFIRMED" or
                        "@LCLOSED" or
                        "@CLANG_ID" or 
                        "@CTYPE"
                )
                .Select(x => x.Value);

            /*
             * Log Debug
             * Digunakan untuk melakukan log debug
             * yang berisi query dan parameter
             */
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true); // Execute the query

            loRtn = R_Utility.R_ConvertTo<PMR00460DataResultDTO>(loDataTable).ToList(); // Convert the data table to list of PMR00460DataResultDTO
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
     * kemudian dikirim sebagai response ke controller dalam bentuk PMR00460PrintBaseHeaderLogoDTO
     */
    public PMR00460PrintBaseHeaderLogoDTO GetBaseHeaderLogoCompany(string pcCompanyId)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetBaseHeaderLogoCompany)); // Start activity
        var loEx = new R_Exception(); // Create new exception object
        PMR00460PrintBaseHeaderLogoDTO loResult = null; // Create new instance of PMR00460PrintBaseHeaderLogoDTO
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
            loResult = R_Utility.R_ConvertTo<PMR00460PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault(); // Convert the data table to PMR00460PrintBaseHeaderLogoDTO
            
            
            lcQuery = $"SELECT CCOMPANY_NAME FROM SAM_COMPANIES WHERE CCOMPANY_ID = '{pcCompanyId}'"; // Query to get company name
            loCmd.CommandText = lcQuery;
            loCmd.CommandType = CommandType.Text;

            //Debug Logs
            _logger.LogDebug(string.Format("SELECT CCOMPANY_NAME FROM SAM_COMPANIES WHERE CCOMPANY_ID = '@CCOMPANY_ID'", pcCompanyId));
            loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
            var loCompanyNameResult = R_Utility.R_ConvertTo<PMR00460PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();

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

        loEx.ThrowExceptionIfErrors(); // Throw exception if there are errors
        return loResult; // Return the company logo
    }

}