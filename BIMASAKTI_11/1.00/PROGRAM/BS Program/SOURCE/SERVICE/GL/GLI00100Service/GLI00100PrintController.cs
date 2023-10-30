using System.Collections;
using BaseHeaderReportCOMMON;
using GLI00100Back;
using GLI00100Common;
using GLI00100Common.DTOs;
using GLI00100Common.DTOs.Print;
using GLI00100Service.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;

namespace GLI00100Service;

public class GLI00100PrintController : R_ReportControllerBase
{
    private LoggerGLI00100 _logger;
    private R_ReportFastReportBackClass _ReportCls;
    private GLI00100PopupParamsDTO _Parameter;

    public GLI00100PrintController(ILogger<GLI00100PrintController> logger)
    {
        LoggerGLI00100.R_InitializeLogger(logger);
        _logger = LoggerGLI00100.R_GetInstanceLogger();

        _ReportCls = new R_ReportFastReportBackClass();
        _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
        _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
    }

    private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcfiletemplate)
    {
        pcfiletemplate = System.IO.Path.Combine("Reports", "GLI00100AccountStatusReport.frx");
    }

    private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
    {
        poData.Add(GeneratePrint(_Parameter));
        pcDataSourceName = "ResponseDataModel";
    }

    [HttpPost]
    public R_DownloadFileResultDTO AccountStatusPost(GLI00100PopupParamsDTO poParam)
    {
        _logger.LogInfo("Start - Post Account Status");
        R_Exception loException = new();
        GLI00100PrintLogKeyDTO loCache = null;
        R_DownloadFileResultDTO loRtn = null;
        try
        {
            loRtn = new R_DownloadFileResultDTO();
            loCache = new GLI00100PrintLogKeyDTO
            {
                poParam = poParam,
                poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
            };


            _logger.LogInfo("Set GUID Param - Post Account Status");
            R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        loException.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Post Account Status");
        return loRtn;
    }

    [HttpGet, AllowAnonymous]
    public FileStreamResult AccountStatusGet(string pcGuid)
    {
        _logger.LogInfo("Start - Get Account Status");
        R_Exception loException = new();
        FileStreamResult loRtn = null;
        GLI00100PrintLogKeyDTO loResultGUID = null;
        try
        {
            //Get Parameter
            loResultGUID =
                R_NetCoreUtility.R_DeserializeObjectFromByte<GLI00100PrintLogKeyDTO>(
                    R_DistributedCache.Cache.Get(pcGuid));

            //Get Data and Set Log Key
            R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);

            _Parameter = loResultGUID.poParam;
            loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        loException.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Account Status");
        return loRtn;
    }

    private GLI00100PrintWithBaseHeaderDTO GeneratePrint(GLI00100PopupParamsDTO poParam)
    {
        var loEx = new R_Exception();
        var loRtn = new GLI00100PrintWithBaseHeaderDTO();
        var loParam = new BaseHeaderDTO();
        try
        {
            _logger.LogInfo("Set Base Header Data");
            
            loParam.CCOMPANY_NAME = "PT Realta Chakradarma";
            loParam.CPRINT_CODE = "GLI00100";
            loParam.CPRINT_NAME = "Account Status Report";
            loParam.CUSER_ID = poParam.CUSER_ID;

            GLI00100PrintResultDTO loData = new()
            {
                Title = "Account Status",
                HeaderTitle = new GLI00100PrintHeaderTitleDTO(),
                Column = new GLI00100PrintColumnDTO(),
                Row = new GLI00100PrintRowDTO(),
                Data = new GLI00100AccountAnalysisDTO()
            };
            var loCls = new GLI00100Cls();
            var loDbParam = new GLI00100ParameterDb();
            var loDbOptParam = new GLI00100AccountAnalysisParamDb();

            _logger.LogInfo("Set Parameter");
            loDbParam.CCOMPANY_ID = poParam.CCOMPANY_ID;
            loDbParam.CLANGUAGE_ID = poParam.CLANGUAGE_ID;
            loDbOptParam.CGLACCOUNT_NO = poParam.CGLACCOUNT_NO;
            loDbOptParam.CYEAR = poParam.CYEAR;
            loDbOptParam.CCURRENCY_TYPE = poParam.CCURRENCY_TYPE;
            loDbOptParam.CCENTER_CODE = poParam.CCENTER_CODE;
            loDbOptParam.CBUDGET_NO = poParam.CBUDGET_NO;

            _logger.LogInfo("Get Detail Account Analysis Report");
            var loResult = loCls.GLI00100GetDetailAccountAnalysisReportDb(loDbParam, loDbOptParam);

            loData.Data = loResult;
            loRtn.Data = loData;
            
            loRtn.BaseHeaderData = loParam;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }
}