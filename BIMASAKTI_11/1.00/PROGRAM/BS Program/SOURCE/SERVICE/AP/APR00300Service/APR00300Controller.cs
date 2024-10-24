using System.Diagnostics;
using APR00300Back;
using APR00300Common;
using APR00300Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace APR00300Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class APR00300Controller : ControllerBase, IAPR00300
{
    private LoggerAPR00300 _logger; // Logger object
    private readonly ActivitySource _activitySource; // ActivitySource object

    /*
     * Constructor
     * Digunakan untuk inisialisasi logger dan activity source
     * kemudian mendapatkan instance logger
     * dan instance activity source
     */
    public APR00300Controller(ILogger<APR00300Controller> logger)
    {
        //Initial and Get Logger
        LoggerAPR00300.R_InitializeLogger(logger);
        _logger = LoggerAPR00300.R_GetInstanceLogger();
        //Initial and Get ActivitySource
        _activitySource = APR00300Activity.R_InitializeAndGetActivitySource(nameof(APR00300Controller));
    }
    
    /*
     * Get Property List
     * Digunakan untuk mendapatkan daftar property
     * kemudian dikirim sebagai response ke client dalam bentuk APR00300ListDTO
     */
    [HttpPost] // Attribute to specify that the method will be called when the HTTP POST request is received
    public APR00300ListDTO<APR00300PropertyDTO> APR00300GetPropertyList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(APR00300GetPropertyList)); // Start activity
        
        _logger.LogInfo("Start - Get Property"); // Log information that the process of getting property list has started
        var loEx = new R_Exception(); // Create new exception object
        var loCls = new APR00300Cls(); // Create new instance of APR00300Cls
        var loDbParams = new APR00300ParameterDb(); // Create new instance of APR00300ParameterDb
        var loReturn = new APR00300ListDTO<APR00300PropertyDTO>(); // Create new instance of APR00300ListDTO

        try
        {
            _logger.LogInfo("Set Parameter"); // Log information that the parameter is being set
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID; // Set company ID from global variable
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID; // Set user ID from global variable
            
            _logger.LogInfo("Get Property"); // Log information that the property is being retrieved
            loReturn.Data = loCls.GetPropertyList(loDbParams); // Get property list
        }
        catch (Exception ex) // If an exception occurs
        {
            loEx.Add(ex); // Add the exception to the exception object
            _logger.LogError(loEx); // Log the exception
        }

        loEx.ThrowExceptionIfErrors(); // Throw exception if there are errors
        _logger.LogInfo("End - Get Property"); // Log information that the process of getting property list has ended
        return loReturn; // Return the property list
    }
    
    /*
     * Get Year Range
     * Digunakan untuk mendapatkan range tahun
     * kemudian dikirim sebagai response ke client dalam bentuk APR00300SingleDTO
     */
    [HttpPost] // Attribute to specify that the method will be called when the HTTP POST request is received
    public APR00300SingleDTO<APR00300PeriodYearRangeDTO> APR00300GetYearRange()
    {
        using var loActivity = _activitySource.StartActivity(nameof(APR00300GetYearRange)); // Start activity
        
        _logger.LogInfo("Start - Get Year Range Param"); // Log information that the process of getting year range has started
        
        var loEx = new R_Exception(); // Create new exception object
        var loDbParams = new APR00300ParameterDb(); // Create new instance of APR00300ParameterDb
        var loCls = new APR00300Cls(); // Create new instance of APR00300Cls
        var loReturn = new APR00300SingleDTO<APR00300PeriodYearRangeDTO>(); // Create new instance of APR00300SingleDTO

        try // Try to execute the following code
        {
            _logger.LogInfo("Set Parameter"); // Log information that the parameter is being set
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID; // Set company ID from global variable

            _logger.LogInfo("Get Year Range Param"); // Log information that the year range is being retrieved
            loReturn.Data = loCls.GetYearRange(loDbParams); // Get year range
        }
        catch (Exception ex) // If an exception occurs
        {
            loEx.Add(ex); // Add the exception to the exception object
            _logger.LogError(loEx); // Log the exception
        }

        loEx.ThrowExceptionIfErrors(); // Throw exception if there are errors
        _logger.LogInfo("End - Get Year Range Param"); // Log information that the process of getting year range has ended
        return loReturn; // Return the year range
    }
    
    /*
     * Get Today
     * Digunakan untuk mendapatkan tanggal hari ini
     * kemudian dikirim sebagai response ke client dalam bentuk APR00300SingleDTO
     */
    [HttpPost] // Attribute to specify that the method will be called when the HTTP POST request is received
    public APR00300SingleDTO<APR00300TodayDTO> APR00300GetToday()
    {
        using var loActivity = _activitySource.StartActivity(nameof(APR00300GetYearRange)); // Start activity
        
        _logger.LogInfo("Start - Get Today Date"); // Log information that the process of getting Today Date has started
        
        var loEx = new R_Exception(); // Create new exception object
        var loDbParams = new APR00300ParameterDb(); // Create new instance of APR00300ParameterDb
        var loCls = new APR00300Cls(); // Create new instance of APR00300Cls
        var loReturn = new APR00300SingleDTO<APR00300TodayDTO>(); // Create new instance of APR00300SingleDTO

        try // Try to execute the following code
        {
            _logger.LogInfo("Set Parameter"); // Log information that the parameter is being set
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID; // Set company ID from global variable

            _logger.LogInfo("Get Today Date"); // Log information that the Today Date is being retrieved
            loReturn.Data = loCls.GetToday(loDbParams); // Get Today Date
            loReturn.Data.IYEAR = loReturn.Data.DTODAY?.Year ?? 0; // Set the year of the Today Date
            loReturn.Data.CMONTH = loReturn.Data.DTODAY?.Month.ToString().PadLeft(2, '0') ?? string.Empty;// Set the month of the Today Date in string format (2 string period)
        }
        catch (Exception ex) // If an exception occurs
        {
            loEx.Add(ex); // Add the exception to the exception object
            _logger.LogError(loEx); // Log the exception
        }

        loEx.ThrowExceptionIfErrors(); // Throw exception if there are errors
        _logger.LogInfo("End - Get Today Date"); // Log information that the process of getting Today Date has ended
        return loReturn; // Return the year range
    }
}