using System.Data;
using System.Data.Common;
using System.Diagnostics;
using HDR00200Common;
using HDR00200Common.DTOs;
using HDR00200Common.DTOs.Print;
using R_BackEnd;
using R_Common;

namespace HDR00200Back;

public class HDR00200Cls
{
    
    private LoggerHDR00200 _logger; // Logger
    private readonly ActivitySource _activitySource; // ActivitySource

    /*
     * Constructor
     * Digunakan untuk inisialisasi logger dan activity source
     * kemudian mendapatkan instance logger
     * dan instance activity source
     */
    public HDR00200Cls()
    {
        _logger = LoggerHDR00200.R_GetInstanceLogger(); // Get instance of logger
        _activitySource = HDR00200Activity.R_GetInstanceActivitySource(); // Get instance of activity source
    }

    /*
     * Get Property List
     * Digunakan untuk mendapatkan daftar property
     * kemudian dikirim sebagai response ke controller dalam bentuk List<HDR00200PropertyDTO>
     */
    public List<HDR00200PropertyDTO> GetPropertyList(HDR00200ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetPropertyList)); // Start activity
        R_Exception loEx = new(); // Create new exception object
        List<HDR00200PropertyDTO> loRtn = new(); // Create new list of return object
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

            loRtn = R_Utility.R_ConvertTo<HDR00200PropertyDTO>(loDataTable).ToList(); // Convert the data table to list of HDR00200PropertyDTO
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
     * Digunakan untuk mendapatkan Parameter Default
     * kemudian dikirim sebagai response ke controller dalam bentuk HDR00200DefaultParamDTO
     */
    public HDR00200DefaultParamDTO GetDefaultParam(HDR00200ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetDefaultParam)); // Start activity
        R_Exception loEx = new(); // Create new exception object
        HDR00200DefaultParamDTO loReturn = new(); // Create new instance of return object
        R_Db loDb; // Database object
        DbConnection loConn; // Database connection object
        DbCommand loCmd; // Database command object
        string lcQuery; // Query
 
        try
        {
            loDb = new R_Db(); // Create new instance of R_Db
            loConn = loDb.GetConnection(); // Get database connection
            loCmd = loDb.GetCommand(); // Get database command

            lcQuery = $"RSP_PM_GET_REPORT_DEFAULT_PARAM"; // Query to get year range
            loCmd.CommandType = CommandType.StoredProcedure; // Set command type to text
            loCmd.CommandText = lcQuery; // Set command text to query
            /*
             * Add Parameter
             * Digunakan untuk menambahkan parameter ke dalam command
             * yang akan digunakan dalam query
             */
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParams.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParams.CUSER_ID);

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

            loReturn = R_Utility.R_ConvertTo<HDR00200DefaultParamDTO>(loDataTable).FirstOrDefault(); // Convert the data table to HDR00200DefaultParamDTO
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
     * Get Code List
     * Digunakan untuk mendapatkan daftar code
     * kemudian dikirim sebagai response ke controller dalam bentuk List<HDR00200CodeDTO>
     */
    public List<HDR00200CodeDTO> GetCodeList(HDR00200ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetCodeList)); // Start activity
        R_Exception loEx = new(); // Create new exception object
        List<HDR00200CodeDTO> loRtn = new(); // Create new list of return object
        R_Db loDb; // Database object
        DbConnection loConn; // Database connection object
        DbCommand loCmd; // Database command object
        string lcQuery; // Query
        try
        {
            loDb = new R_Db(); // Create new instance of R_Db
            loConn = loDb.GetConnection(); // Get database connection
            loCmd = loDb.GetCommand(); // Get database command
 
            var CAPPLICATION = "BIMASAKTI";
            var CCOMPANY_ID = poParam.CCOMPANY_ID;
            var CCLASS_ID = "_BS_AREA";
            var CLANGUAGE_ID = poParam.CLANG_ID;
            
            
            lcQuery = @$"EXEC RSP_GS_GET_GSB_CODE_LIST '{CAPPLICATION}', '{CCOMPANY_ID}', '{CCLASS_ID}', '{CLANGUAGE_ID}'"; // Query to get property list
            loCmd.CommandType = CommandType.Text; // Set command type to text
            loCmd.CommandText = lcQuery; // Set command text to query

            _logger.LogDebug("{pcQuery}", lcQuery); // Log the query

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true); // Execute the query

            loRtn = R_Utility.R_ConvertTo<HDR00200CodeDTO>(loDataTable).ToList(); // Convert the data table to list of HDR00200PropertyDTO
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
     * Get Base Header Logo Company
     * Digunakan untuk mendapatkan logo perusahaan
     * kemudian dikirim sebagai response ke controller dalam bentuk HDR00200PrintBaseHeaderLogoDTO
     */
    public HDR00200PrintBaseHeaderLogoDTO GetBaseHeaderLogoCompany(string pcCompanyId)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetBaseHeaderLogoCompany)); // Start activity
        var loEx = new R_Exception(); // Create new exception object
        HDR00200PrintBaseHeaderLogoDTO? loResult = new(); // Create new instance of return object
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
            loResult = R_Utility.R_ConvertTo<HDR00200PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault(); // Convert the data table to HDR00200PrintBaseHeaderLogoDTO
        
            lcQuery = $"SELECT CCOMPANY_NAME FROM SAM_COMPANIES WHERE CCOMPANY_ID = '{pcCompanyId}'"; // Query to get company name
            loCmd.CommandText = lcQuery;
            loCmd.CommandType = CommandType.Text;

            //Debug Logs
            _logger.LogDebug(string.Format("SELECT CCOMPANY_NAME FROM SAM_COMPANIES WHERE CCOMPANY_ID = '@CCOMPANY_ID'", pcCompanyId));
            loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
            var loCompanyNameResult = R_Utility.R_ConvertTo<HDR00200PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();

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
    
    
    
    public List<HDR00200DataResultDTO> GetReportData(HDR00200ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetReportData)); // Start activity
        R_Exception loEx = new(); // Create new exception object
        List<HDR00200DataResultDTO> loRtn = null; // Create new list of HDR00200DataResultDTO
        R_Db loDb; // Database object
        DbConnection loConn; // Database connection object
        DbCommand loCmd; // Database command object
        string lcQuery; // Query
        try
        { 
            loDb = new R_Db(); // Create new instance of R_Db
            loConn = loDb.GetConnection(); // Get database connection
            loCmd = loDb.GetCommand(); // Get database command
    
            lcQuery = "RSP_HDR00200_GET_REPORT"; // Query to get report data
            loCmd.CommandType = CommandType.StoredProcedure; // Set command type to stored procedure
            loCmd.CommandText = lcQuery; // Set command text to query
    
            /*
             * Add Parameter
             * Digunakan untuk menambahkan parameter ke dalam command
             * yang akan digunakan dalam query
             */
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CREPORT_TYPE", DbType.String, 1, poParam.CREPORT_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CAREA", DbType.String, 2, poParam.CAREA);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_BUILDING_ID", DbType.String, 20, poParam.CFROM_BUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTO_BUILDING_ID", DbType.String, 20, poParam.CTO_BUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_DEPT_CODE", DbType.String, 20, poParam.CFROM_DEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CTO_DEPT_CODE", DbType.String, 20, poParam.CTO_DEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_PERIOD", DbType.String, 8, poParam.CFROM_PERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CTO_PERIOD", DbType.String, 8, poParam.CTO_PERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CCATEGORY", DbType.String, 255, poParam.CCATEGORY);
            loDb.R_AddCommandParameter(loCmd, "@CSTATUS", DbType.String, 255, poParam.CSTATUS);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParam.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 2, poParam.CLANG_ID);
    
            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CREPORT_TYPE" or
                        "@CAREA" or
                        "@CFROM_BUILDING_ID" or
                        "@CTO_BUILDING_ID" or
                        "@CFROM_DEPT_CODE" or
                        "@CTO_DEPT_CODE" or
                        "@CFROM_PERIOD" or
                        "@CTO_PERIOD" or
                        "@CCATEGORY" or
                        "@CSTATUS" or 
                        "@CUSER_ID" or 
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
    
            loRtn = R_Utility.R_ConvertTo<HDR00200DataResultDTO>(loDataTable).ToList(); // Convert the data table to list of HDR00200DataResultDTO
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
}