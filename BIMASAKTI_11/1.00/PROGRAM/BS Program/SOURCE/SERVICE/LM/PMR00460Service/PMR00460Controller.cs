using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR00460Back;
using PMR00460Common;
using PMR00460Common.DTOs;
using PMR00460Common.Params;
using R_BackEnd;
using R_Common;

namespace PMR00460Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class PMR00460Controller : ControllerBase, IPMR00460
{
    private LoggerPMR00460 _logger; // Logger object
    private readonly ActivitySource _activitySource; // ActivitySource object

    /*
     * Constructor
     * Digunakan untuk inisialisasi logger dan activity source
     * kemudian mendapatkan instance logger
     * dan instance activity source
     */
    public PMR00460Controller(ILogger<PMR00460Controller> logger)
    {
        //Initial and Get Logger
        LoggerPMR00460.R_InitializeLogger(logger);
        _logger = LoggerPMR00460.R_GetInstanceLogger();
        //Initial and Get ActivitySource
        _activitySource = PMR00460Activity.R_InitializeAndGetActivitySource(nameof(PMR00460Controller));
    }

    /*
     * Get Property List
     * Digunakan untuk mendapatkan daftar property
     * kemudian dikirim sebagai response ke client dalam bentuk PMR00460ListDTO
     */
    [HttpPost] // Attribute to specify that the method will be called when the HTTP POST request is received
    public PMR00460ListDTO<PMR00460PropertyDTO> PMR00460GetPropertyList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMR00460GetPropertyList)); // Start activity

        _logger.LogInfo(
            "Start - Get Property"); // Log information that the process of getting property list has started
        var loEx = new R_Exception(); // Create new exception object
        var loCls = new PMR00460Cls(); // Create new instance of PMR00460Cls
        var loDbParams = new PMR00460ParameterDb(); // Create new instance of PMR00460ParameterDb
        var loReturn = new PMR00460ListDTO<PMR00460PropertyDTO>(); // Create new instance of PMR00460ListDTO

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
     * kemudian dikirim sebagai response ke client dalam bentuk PMR00460SingleDTO
     */
    [HttpPost] // Attribute to specify that the method will be called when the HTTP POST request is received
    public PMR00460SingleDTO<PMR00460PeriodYearRangeDTO> PMR00460GetYearRange()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMR00460GetYearRange)); // Start activity

        _logger.LogInfo(
            "Start - Get Year Range Param"); // Log information that the process of getting year range has started

        var loEx = new R_Exception(); // Create new exception object
        var loDbParams = new PMR00460ParameterDb(); // Create new instance of PMR00460ParameterDb
        var loCls = new PMR00460Cls(); // Create new instance of PMR00460Cls
        var loReturn = new PMR00460SingleDTO<PMR00460PeriodYearRangeDTO>(); // Create new instance of PMR00460SingleDTO

        try // Try to execute the following code
        {
            _logger.LogInfo("Set Parameter"); // Log information that the parameter is being set
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID; // Set company ID from global variable

            _logger.LogInfo("Get Year Range"); // Log information that the year range is being retrieved
            loReturn.Data = loCls.GetYearRange(loDbParams); // Get year range
        }
        catch (Exception ex) // If an exception occurs
        {
            loEx.Add(ex); // Add the exception to the exception object
            _logger.LogError(loEx); // Log the exception
        }

        loEx.ThrowExceptionIfErrors(); // Throw exception if there are errors
        _logger.LogInfo("End - Get Year Range"); // Log information that the process of getting year range has ended
        return loReturn; // Return the year range
    }

    /*
     * Get Default Param
     * Digunakan untuk mendapatkan parameter default
     * kemudian dikirim sebagai response ke client dalam bentuk PMR00460SingleDTO
     */
    [HttpPost] // Attribute to specify that the method will be called when the HTTP POST request is received
    public PMR00460SingleDTO<PMR00460DefaultParamDTO> PMR00460GetDefaultParam(PMR00460DefaultParamParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMR00460GetDefaultParam)); // Start activity

        _logger.LogInfo("Start - Get Default Param"); // Log information that the process of getting default parameter has started

        var loEx = new R_Exception(); // Create new exception object
        var loDbParams = new PMR00460ParameterDb(); // Create new instance of PMR00460ParameterDb
        var loCls = new PMR00460Cls(); // Create new instance of PMR00460Cls
        var loReturn = new PMR00460SingleDTO<PMR00460DefaultParamDTO>(); // Create new instance of PMR00460SingleDTO

        try // Try to execute the following code
        {
            _logger.LogInfo("Set Parameter"); // Log information that the parameter is being set
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID; // Set company ID from global variable
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID; // Set user ID from global variable
            loDbParams.CPROPERTY_ID = poParam.CPROPERTY_ID; // Set property ID from parameter

            _logger.LogInfo("Get Default Param"); // Log information that the default parameter is being retrieved
            loReturn.Data = loCls.GetDefaultParam(loDbParams); // Get default parameter
        }
        catch (Exception ex) // If an exception occurs
        {
            loEx.Add(ex); // Add the exception to the exception object
            _logger.LogError(loEx); // Log the exception
        }

        loEx.ThrowExceptionIfErrors(); // Throw exception if there are errors
        _logger.LogInfo("End - Get Default Param"); // Log information that the process of getting default parameter has ended
        return loReturn; // Return the default parameter
    }
}