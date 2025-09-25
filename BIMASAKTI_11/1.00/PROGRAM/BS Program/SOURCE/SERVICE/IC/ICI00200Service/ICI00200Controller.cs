using System.Diagnostics;
using ICI00200Back;
using ICI00200Common;
using ICI00200Common.DTOs;
using ICI00200Common.Params;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace ICI00200Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class ICI00200Controller : ControllerBase, IICI00200
{
    private LoggerICI00200 _logger;
    private readonly ActivitySource _activitySource;

    public ICI00200Controller(ILogger<ICI00200Controller> logger)
    {
        //Initial and Get Logger
        LoggerICI00200.R_InitializeLogger(logger);
        _logger = LoggerICI00200.R_GetInstanceLogger();
        _activitySource = ICI00200Activity.R_InitializeAndGetActivitySource(nameof(ICI00200Controller));
    }

    [HttpPost]
    public IAsyncEnumerable<ICI00200ProductDTO> ICI00200GetProductListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(ICI00200GetLastInfoDetail));
        _logger.LogInfo("Start - Get Product List");
        R_Exception loEx = new();
        ICI00200ParameterDb loDbPar;
        List<ICI00200ProductDTO> loRtnTmp;
        ICI00200Cls loCls;
        IAsyncEnumerable<ICI00200ProductDTO> loRtn = null;

        try
        {
            loDbPar = new ICI00200ParameterDb();

            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CCATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ICI00200ContextConstant.CCATEGORY_ID);
            loDbPar.CPRODUCT_ID = R_Utility.R_GetStreamingContext<string>(ICI00200ContextConstant.CPRODUCT_ID);
            loDbPar.CLIST_TYPE = R_Utility.R_GetStreamingContext<string>(ICI00200ContextConstant.CLIST_TYPE);

            loCls = new ICI00200Cls();
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

    [HttpPost]
    public IAsyncEnumerable<ICI00200DeptDTO> ICI00200GetDeptListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(ICI00200GetDeptListStream));
        _logger.LogInfo("Start - Get Dept List");
        R_Exception loEx = new();
        ICI00200ParameterDb loDbPar;
        List<ICI00200DeptDTO> loRtnTmp;
        ICI00200Cls loCls;
        IAsyncEnumerable<ICI00200DeptDTO> loRtn = null;

        try
        {
            loDbPar = new ICI00200ParameterDb();

            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CCATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ICI00200ContextConstant.CCATEGORY_ID);
            loDbPar.CPRODUCT_ID = R_Utility.R_GetStreamingContext<string>(ICI00200ContextConstant.CPRODUCT_ID);
            loDbPar.CLIST_TYPE = R_Utility.R_GetStreamingContext<string>(ICI00200ContextConstant.CLIST_TYPE);

            loCls = new ICI00200Cls();
            _logger.LogInfo("Get Dept List");
            loRtnTmp = loCls.GetDeptList(loDbPar);

            loRtn = GetStream(loRtnTmp);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Dept List");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<ICI00200WarehouseDTO> ICI00200GetWarehouseListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(ICI00200GetWarehouseListStream));
        _logger.LogInfo("Start - Get Warehouse List");
        R_Exception loEx = new();
        ICI00200ParameterDb loDbPar;
        List<ICI00200WarehouseDTO> loRtnTmp;
        ICI00200Cls loCls;
        IAsyncEnumerable<ICI00200WarehouseDTO> loRtn = null;

        try
        {
            loDbPar = new ICI00200ParameterDb();

            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CCATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ICI00200ContextConstant.CCATEGORY_ID);
            loDbPar.CPRODUCT_ID = R_Utility.R_GetStreamingContext<string>(ICI00200ContextConstant.CPRODUCT_ID);
            loDbPar.CLIST_TYPE = R_Utility.R_GetStreamingContext<string>(ICI00200ContextConstant.CLIST_TYPE);

            loCls = new ICI00200Cls();
            _logger.LogInfo("Get Warehouse List");
            loRtnTmp = loCls.GetWarehouseList(loDbPar);

            loRtn = GetStream(loRtnTmp);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Warehouse List");
        return loRtn;
    }

    [HttpPost]
    public ICI00200SingleDTO<ICI00200ProductDetailDTO> ICI00200GetProductDetail(ICI00200DetailParam productDetailParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ICI00200GetProductDetail));
        _logger.LogInfo("Start - Get Product Detail");
        var loEx = new R_Exception();
        var loCls = new ICI00200Cls();
        var loReturn = new ICI00200SingleDTO<ICI00200ProductDetailDTO>();
        ICI00200ParameterDb loDbParams;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams = R_Utility.R_ConvertObjectToObject<ICI00200DetailParam, ICI00200ParameterDb>(productDetailParam);
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

            _logger.LogInfo("Get Product Detail");
            loReturn.Data = loCls.GetUtilityUsageDetailWithImage(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Product Detail");
        return loReturn;
    }

    [HttpPost]
    public ICI00200SingleDTO<ICI00200LastInfoDetailDTO> ICI00200GetLastInfoDetail(ICI00200DetailParam productDetailParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ICI00200GetLastInfoDetail));
        _logger.LogInfo("Start - Get Last Info Detail");
        var loEx = new R_Exception();
        var loCls = new ICI00200Cls();
        var loReturn = new ICI00200SingleDTO<ICI00200LastInfoDetailDTO>();
        ICI00200ParameterDb loDbParams;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams = R_Utility.R_ConvertObjectToObject<ICI00200DetailParam, ICI00200ParameterDb>(productDetailParam);
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

            _logger.LogInfo("Get Last Info Detail");
            loReturn.Data = loCls.GetLastInfoDetail(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Last Info Detail");
        return loReturn;
    }

    [HttpPost]
    public ICI00200SingleDTO<ICI00200DeptWareDetailDTO> ICI00200GetDeptWareDetail(ICI00200DetailParam productDetailParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ICI00200GetDeptWareDetail));
        _logger.LogInfo("Start - Get Dept Ware Detail");
        var loEx = new R_Exception();
        var loCls = new ICI00200Cls();
        var loReturn = new ICI00200SingleDTO<ICI00200DeptWareDetailDTO>();
        ICI00200ParameterDb loDbParams;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams = R_Utility.R_ConvertObjectToObject<ICI00200DetailParam, ICI00200ParameterDb>(productDetailParam);
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

            _logger.LogInfo("Get Dept Ware Detail");
            loReturn.Data = loCls.GetDeptWareDetail(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Dept Ware Detail");
        return loReturn;
    }


    #region "Helper GetSalesTaxStream Functions"

    private async IAsyncEnumerable<T> GetStream<T>(List<T> poParameter)
    {
        foreach (var item in poParameter)
        {
            yield return item;
        }
        
        await Task.CompletedTask;
    }

    #endregion
}