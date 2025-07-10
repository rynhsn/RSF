using System.Diagnostics;
using ICB00300Back;
using ICB00300Back.DTOs;
using ICB00300Common;
using ICB00300Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace ICB00300Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class ICB00300Controller : ControllerBase, IICB00300
{
    private LoggerICB00300 _logger;
    private readonly ActivitySource _activitySource;

    public ICB00300Controller(ILogger<ICB00300Controller> logger)
    {
        //Initial and Get Logger
        LoggerICB00300.R_InitializeLogger(logger);
        _logger = LoggerICB00300.R_GetInstanceLogger();
        _activitySource = ICB00300Activity.R_InitializeAndGetActivitySource(nameof(ICB00300Controller));
    }

    [HttpPost]
    public ICB00300ListDTO<ICB00300PropertyDTO> ICB00300GetPropertyList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(ICB00300GetPropertyList));

        _logger.LogInfo("Start - Get Property");
        var loEx = new R_Exception();
        var loCls = new ICB00300Cls();
        var loDbParams = new ICB00300ParameterDb();
        var loReturn = new ICB00300ListDTO<ICB00300PropertyDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Get Property");
            loReturn.Data = loCls.GetPropertyList(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Property");
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<ICB00300ProductDTO> ICB00300GetProductListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(ICB00300GetProductListStream));
        _logger.LogInfo("Start - Get Product List");
        R_Exception loEx = new();
        ICB00300ParameterDb loDbPar;
        List<ICB00300ProductDTO> loRtnTmp;
        ICB00300Cls loCls;
        IAsyncEnumerable<ICB00300ProductDTO> loRtn = null;

        try
        {
            loDbPar = new ICB00300ParameterDb();

            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ICB00300ContextConstant.CPROPERTY_ID);
            loDbPar.CRECALCULATE_TYPE = R_Utility.R_GetStreamingContext<string>(ICB00300ContextConstant.CRECALCULATE_TYPE);
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

            loCls = new ICB00300Cls();
            _logger.LogInfo("Get Product List");
            loRtnTmp = loCls.GetProductList(loDbPar);

            loRtn = GetStream(loRtnTmp);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Product List");
        return loRtn;
    }
    
    

    #region "Helper ListStream Functions"

    private async IAsyncEnumerable<T> GetStream<T>(List<T> poParameter)
    {
        foreach (var item in poParameter)
        {
            yield return item;
        }
    }

    #endregion
}