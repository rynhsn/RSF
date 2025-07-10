using System.Data;
using System.Data.Common;
using System.Diagnostics;
using ICR00600Common;
using ICR00600Common.DTOs;
using ICR00600Common.DTOs.Print;
using R_BackEnd;
using R_Common;

namespace ICR00600Back;

public class ICR00600Cls
{
    private LoggerICR00600 _logger; // Logger
    private readonly ActivitySource _activitySource; // ActivitySource

    /*
     * Constructor
     * Digunakan untuk inisialisasi logger dan activity source
     * kemudian mendapatkan instance logger
     * dan instance activity source
     */
    public ICR00600Cls()
    {
        _logger = LoggerICR00600.R_GetInstanceLogger(); // Get instance of logger
        _activitySource = ICR00600Activity.R_GetInstanceActivitySource(); // Get instance of activity source
    }

    /*
     * Get Property List
     * Digunakan untuk mendapatkan daftar property
     * kemudian dikirim sebagai response ke controller dalam bentuk List<ICR00600PropertyDTO>
     */
    public List<ICR00600PropertyDTO> GetPropertyList(ICR00600ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetPropertyList)); // Start activity
        R_Exception loEx = new(); // Create new exception object
        List<ICR00600PropertyDTO> loRtn = null; // Create new list of ICR00600PropertyDTO
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

            loRtn = R_Utility.R_ConvertTo<ICR00600PropertyDTO>(loDataTable).ToList(); // Convert the data table to list of ICR00600PropertyDTO
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
     * kemudian dikirim sebagai response ke controller dalam bentuk ICR00600PeriodYearRangeDTO
     */
    public ICR00600PeriodYearRangeDTO GetYearRange(ICR00600ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetYearRange)); // Start activity
        R_Exception loEx = new(); // Create new exception object
        ICR00600PeriodYearRangeDTO loReturn = new(); // Create new instance of ICR00600PeriodYearRangeDTO
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

            loReturn = R_Utility.R_ConvertTo<ICR00600PeriodYearRangeDTO>(DataTable)
                .FirstOrDefault(); // Convert the data table to ICR00600PeriodYearRangeDTO
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
     * Get TransCode
     * Digunakan untuk mendapatkan Trans Code
     * kemudian dikirim sebagai response ke controller dalam bentuk ICR00600TransCodeDTO
     */
    public ICR00600TransCodeDTO GetTransCode(ICR00600ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetTransCode)); // Start activity
        R_Exception loEx = new(); // Create new exception object
        ICR00600TransCodeDTO loReturn = new(); // Create new instance of ICR00600TransCodeDTO
        R_Db loDb; // Database object
        DbConnection loConn; // Database connection object
        DbCommand loCmd; // Database command object
        string lcQuery; // Query

        try
        {
            loDb = new R_Db(); // Create new instance of R_Db
            loConn = loDb.GetConnection(); // Get database connection
            loCmd = loDb.GetCommand(); // Get database command

            lcQuery = "RSP_GS_GET_TRANS_CODE_INFO"; // Query to get year range
            loCmd.CommandType = CommandType.StoredProcedure; // Set command type to text
            loCmd.CommandText = lcQuery; // Set command text to query
            
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParams.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParams.CTRANS_CODE);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CTRANS_CODE"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true); // Execute the query

            loReturn = R_Utility.R_ConvertTo<ICR00600TransCodeDTO>(DataTable).FirstOrDefault(); // Convert the data table to ICR00600TransCodeDTO
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
     * Get Period List
     */
    public List<ICR00600PeriodDTO> GetPeriodList(ICR00600ParameterDb poParam)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetPeriodList));
        R_Exception loEx = new();
        List<ICR00600PeriodDTO> loRtn = null;
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

            loRtn = R_Utility.R_ConvertTo<ICR00600PeriodDTO>(loDataTable).ToList();
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
    
    
    public ICR00600PrintBaseHeaderLogoDTO GetBaseHeaderLogoCompany(string pcCompanyId)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetBaseHeaderLogoCompany));
        var loEx = new R_Exception();
        ICR00600PrintBaseHeaderLogoDTO loResult = null;
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
            loResult = R_Utility.R_ConvertTo<ICR00600PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();
            
            //ambil company name
            lcQuery = $"EXEC RSP_GS_GET_COMPANY_INFO '{pcCompanyId}'"; // Query to get company name
            loCmd.CommandText = lcQuery;
            loCmd.CommandType = CommandType.Text;

            //Debug Logs
            _logger.LogDebug(lcQuery);
            loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
            var loCompanyNameResult = R_Utility.R_ConvertTo<ICR00600PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();
            
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
    
    
    public List<ICR00600DataResultDTO> GetReportData(ICR00600ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetReportData));
        R_Exception loEx = new();
        List<ICR00600DataResultDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_IC_GET_STOCK_TAKE_ACTIVITY_REPORT";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String,8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String,20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String,20, poParam.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String,6, poParam.CPERIOD);
            loDb.R_AddCommandParameter(loCmd, "@COPTION_PRINT", DbType.String,20, poParam.COPTION_PRINT);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_REF_NO", DbType.String,30, poParam.CFROM_REF_NO);
            loDb.R_AddCommandParameter(loCmd, "@CTO_REF_NO", DbType.String,30, poParam.CTO_REF_NO);
            loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String,3, poParam.CLANG_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CDEPT_CODE" or
                        "@CPERIOD" or
                        "@COPTION_PRINT" or
                        "@CFROM_REF_NO" or
                        "@CTO_REF_NO" or
                        "@CLANG_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<ICR00600DataResultDTO>(loDataTable).ToList();
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