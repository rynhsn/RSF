using System.Diagnostics;
using HDR00200Back;
using HDR00200Common;
using HDR00200Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace HDR00200Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class HDR00200Controller : ControllerBase, IHDR00200
{
    private LoggerHDR00200 _logger; // Logger object
    private readonly ActivitySource _activitySource; // ActivitySource object

    /*
     * Constructor
     * Digunakan untuk inisialisasi logger dan activity source
     * kemudian mendapatkan instance logger
     * dan instance activity source
     */
    public HDR00200Controller(ILogger<HDR00200Controller> logger)
    {
        //Initial and Get Logger
        LoggerHDR00200.R_InitializeLogger(logger);
        _logger = LoggerHDR00200.R_GetInstanceLogger();
        //Initial and Get ActivitySource
        _activitySource = HDR00200Activity.R_InitializeAndGetActivitySource(nameof(HDR00200Controller));
    }

    /*
     * Get Property List
     * Digunakan untuk mendapatkan daftar property
     * kemudian dikirim sebagai response ke client dalam bentuk HDR00200ListDTO
     */
    [HttpPost] // Attribute to specify that the method will be called when the HTTP POST request is received
    public HDR00200ListDTO<HDR00200PropertyDTO> HDR00200GetPropertyList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(HDR00200GetPropertyList)); // Start activity

        _logger.LogInfo(
            "Start - Get Property"); // Log information that the process of getting property list has started
        var loEx = new R_Exception(); // Create new exception object
        var loCls = new HDR00200Cls(); // Create new instance of HDR00200Cls
        var loDbParams = new HDR00200ParameterDb(); // Create new instance of HDR00200ParameterDb
        var loReturn = new HDR00200ListDTO<HDR00200PropertyDTO>(); // Create new instance of HDR00200ListDTO

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

    [HttpPost] // Attribute to specify that the method will be called when the HTTP POST request is received
    public HDR00200SingleDTO<HDR00200DefaultParamDTO> HDR00200GetDefaultParam(HDR00200PropertyDTO poProperty)
    {
        using var loActivity = _activitySource.StartActivity(nameof(HDR00200GetDefaultParam)); // Start activity

        _logger.LogInfo(
            "Start - Get Default Param"); // Log information that the process of getting property list has started
        var loEx = new R_Exception(); // Create new exception object
        var loCls = new HDR00200Cls(); // Create new instance of HDR00200Cls
        var loDbParams = new HDR00200ParameterDb(); // Create new instance of HDR00200ParameterDb
        var loReturn = new HDR00200SingleDTO<HDR00200DefaultParamDTO>(); // Create new instance of return object

        try
        {
            _logger.LogInfo("Set Parameter"); // Log information that the parameter is being set
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID; // Set company ID from global variable
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID; // Set user ID from global variable
            loDbParams.CPROPERTY_ID = poProperty.CPROPERTY_ID; // Set property ID from parameter

            _logger.LogInfo("Get Default Param"); // Log information that the property is being retrieved
            var loResult = loCls.GetDefaultParam(loDbParams); // Get property list
            loReturn.Data = loResult;
        }
        catch (Exception ex) // If an exception occurs
        {
            loEx.Add(ex); // Add the exception to the exception object
            _logger.LogError(loEx); // Log the exception
        }

        loEx.ThrowExceptionIfErrors(); // Throw exception if there are errors
        _logger.LogInfo(
            "End - Get Default Param"); // Log information that the process of getting property list has ended
        return loReturn; // Return the property list
    }

    [HttpPost] // Attribute to specify that the method will be called when the HTTP POST request is received
    public HDR00200ListDTO<HDR00200CodeDTO> HDR00200GetCodeList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(HDR00200GetCodeList)); // Start activity

        _logger.LogInfo(
            "Start - Get Code List"); // Log information that the process of getting property list has started
        var loEx = new R_Exception(); // Create new exception object
        var loCls = new HDR00200Cls(); // Create new instance of HDR00200Cls
        var loDbParams = new HDR00200ParameterDb(); // Create new instance of HDR00200ParameterDb
        var loReturn = new HDR00200ListDTO<HDR00200CodeDTO>(); // Create new instance of HDR00200ListDTO

        try
        {
            _logger.LogInfo("Set Parameter"); // Log information that the parameter is being set
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID; // Set company ID from global variable
            loDbParams.CLANG_ID = R_BackGlobalVar.CULTURE; // Set user ID from global variable

            _logger.LogInfo("Get Code List"); // Log information that the property is being retrieved
            loReturn.Data = loCls.GetCodeList(loDbParams); // Get property list
        }
        catch (Exception ex) // If an exception occurs
        {
            loEx.Add(ex); // Add the exception to the exception object
            _logger.LogError(loEx); // Log the exception
        }

        loEx.ThrowExceptionIfErrors(); // Throw exception if there are errors
        _logger.LogInfo("End - Get Code List"); // Log information that the process of getting property list has ended
        return loReturn; // Return the property list
    }
}