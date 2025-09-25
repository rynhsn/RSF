using System.Diagnostics;
using ICR00600Back;
using ICR00600Common;
using ICR00600Common.DTOs;
using ICR00600Common.Params;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace ICR00600Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class ICR00600Controller : ControllerBase, IICR00600
{
    private LoggerICR00600 _logger; // Logger object
    private readonly ActivitySource _activitySource; // ActivitySource object

    /*
     * Constructor
     * Digunakan untuk inisialisasi logger dan activity source
     * kemudian mendapatkan instance logger
     * dan instance activity source
     */
    public ICR00600Controller(ILogger<ICR00600Controller> logger)
    {
        //Initial and Get Logger
        LoggerICR00600.R_InitializeLogger(logger);
        _logger = LoggerICR00600.R_GetInstanceLogger();
        //Initial and Get ActivitySource
        _activitySource = ICR00600Activity.R_InitializeAndGetActivitySource(nameof(ICR00600Controller));
    }

    /*
     * Get Property List
     * Digunakan untuk mendapatkan daftar property
     * kemudian dikirim sebagai response ke client dalam bentuk ICR00600ListDTO
     */
    [HttpPost] // Attribute to specify that the method will be called when the HTTP POST request is received
    public ICR00600ListDTO<ICR00600PropertyDTO> ICR00600GetPropertyList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(ICR00600GetPropertyList)); // Start activity

        _logger.LogInfo(
            "Start - Get Property"); // Log information that the process of getting property list has started
        var loEx = new R_Exception(); // Create new exception object
        var loCls = new ICR00600Cls(); // Create new instance of ICR00600Cls
        var loDbParams = new ICR00600ParameterDb(); // Create new instance of ICR00600ParameterDb
        var loReturn = new ICR00600ListDTO<ICR00600PropertyDTO>(); // Create new instance of ICR00600ListDTO

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
     * kemudian dikirim sebagai response ke client dalam bentuk ICR00600SingleDTO
     */
    [HttpPost] // Attribute to specify that the method will be called when the HTTP POST request is received
    public ICR00600SingleDTO<ICR00600PeriodYearRangeDTO> ICR00600GetPeriodYearRange()
    {
        using var loActivity = _activitySource.StartActivity(nameof(ICR00600GetPeriodYearRange)); // Start activity

        _logger.LogInfo(
            "Start - Get Year Range Param"); // Log information that the process of getting year range has started

        var loEx = new R_Exception(); // Create new exception object
        var loDbParams = new ICR00600ParameterDb(); // Create new instance of ICR00600ParameterDb
        var loCls = new ICR00600Cls(); // Create new instance of ICR00600Cls
        var loReturn = new ICR00600SingleDTO<ICR00600PeriodYearRangeDTO>(); // Create new instance of ICR00600SingleDTO

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
     * Get Year Range
     * Digunakan untuk mendapatkan List period month
     * kemudian dikirim sebagai response ke client dalam bentuk ICR00600ListDTO
     */
    [HttpPost] // Attribute to specify that the method will be called when the HTTP POST request is received
    public ICR00600ListDTO<ICR00600PeriodDTO> ICR00600GetPeriodList(ICR00600PeriodParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ICR00600GetPeriodList)); // Start activity

        _logger.LogInfo(
            "Start - Get Period"); // Log information that the process of getting Period list has started
        var loEx = new R_Exception(); // Create new exception object
        var loCls = new ICR00600Cls(); // Create new instance of ICR00600Cls
        var loDbParams = new ICR00600ParameterDb(); // Create new instance of ICR00600ParameterDb
        var loReturn = new ICR00600ListDTO<ICR00600PeriodDTO>(); // Create new instance of ICR00600ListDTO

        try
        {
            _logger.LogInfo("Set Parameter"); // Log information that the parameter is being set
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID; // Set company ID from global variable
            loDbParams.CYEAR = poParam.CYEAR;

            _logger.LogInfo("Get Period"); // Log information that the Period is being retrieved
            loReturn.Data = loCls.GetPeriodList(loDbParams); // Get Period list
        }
        catch (Exception ex) // If an exception occurs
        {
            loEx.Add(ex); // Add the exception to the exception object
            _logger.LogError(loEx); // Log the exception
        }

        loEx.ThrowExceptionIfErrors(); // Throw exception if there are errors
        _logger.LogInfo("End - Get Period"); // Log information that the process of getting Period list has ended
        return loReturn; // Return the Period list
    }
    
    [HttpPost] // Attribute to specify that the method will be called when the HTTP POST request is received
    public ICR00600SingleDTO<ICR00600TransCodeDTO> ICR00600GetTransCode()
    {
        using var loActivity = _activitySource.StartActivity(nameof(ICR00600GetPeriodYearRange)); // Start activity

        _logger.LogInfo("Start - Get Trans Code Param");

        var loEx = new R_Exception();
        var loDbParams = new ICR00600ParameterDb();
        var loCls = new ICR00600Cls();
        var loReturn = new ICR00600SingleDTO<ICR00600TransCodeDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CTRANS_CODE = ICR00600TransCode.CTRANS_CODE;

            _logger.LogInfo("Get Trans Code Param");
            loReturn.Data = loCls.GetTransCode(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Trans Code Param");
        return loReturn;
    }
}