using System.Diagnostics;
using ICR00100Back;
using ICR00100Common;
using ICR00100Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace ICR00100Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class ICR00100Controller : ControllerBase, IICR00100
{
    private LoggerICR00100 _logger; // Logger object
    private readonly ActivitySource _activitySource; // ActivitySource object

    /*
     * Constructor
     * Digunakan untuk inisialisasi logger dan activity source
     * kemudian mendapatkan instance logger
     * dan instance activity source
     */
    public ICR00100Controller(ILogger<ICR00100Controller> logger)
    {
        //Initial and Get Logger
        LoggerICR00100.R_InitializeLogger(logger);
        _logger = LoggerICR00100.R_GetInstanceLogger();
        //Initial and Get ActivitySource
        _activitySource = ICR00100Activity.R_InitializeAndGetActivitySource(nameof(ICR00100Controller));
    }

    /*
     * Get Year Range
     * Digunakan untuk mendapatkan range tahun
     * kemudian dikirim sebagai response ke client dalam bentuk ICR00100SingleDTO
     */
    [HttpPost] // Attribute to specify that the method will be called when the HTTP POST request is received
    public ICR00100SingleDTO<ICR00100PeriodYearRangeDTO> ICR00100GetPeriodYearRange()
    {
        using var loActivity = _activitySource.StartActivity(nameof(ICR00100GetPeriodYearRange)); // Start activity

        _logger.LogInfo(
            "Start - Get Year Range Param"); // Log information that the process of getting year range has started

        var loEx = new R_Exception(); // Create new exception object
        var loDbParams = new ICR00100ParameterDb(); // Create new instance of ICR00100ParameterDb
        var loCls = new ICR00100Cls(); // Create new instance of ICR00100Cls
        var loReturn = new ICR00100SingleDTO<ICR00100PeriodYearRangeDTO>(); // Create new instance of ICR00100SingleDTO

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
        _logger.LogInfo(
            "End - Get Year Range Param"); // Log information that the process of getting year range has ended
        return loReturn; // Return the year range
    }
    
    /*
     * Get Property List
     * Digunakan untuk mendapatkan daftar property
     * kemudian dikirim sebagai response ke client dalam bentuk ICR00100ListDTO
     */
    [HttpPost] // Attribute to specify that the method will be called when the HTTP POST request is received
    public ICR00100ListDTO<ICR00100PropertyDTO> ICR00100GetPropertyList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(ICR00100GetPropertyList)); // Start activity
        
        _logger.LogInfo("Start - Get Property"); // Log information that the process of getting property list has started
        var loEx = new R_Exception(); // Create new exception object
        var loCls = new ICR00100Cls(); // Create new instance of ICR00100Cls
        var loDbParams = new ICR00100ParameterDb(); // Create new instance of ICR00100ParameterDb
        var loReturn = new ICR00100ListDTO<ICR00100PropertyDTO>(); // Create new instance of ICR00100ListDTO

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
}