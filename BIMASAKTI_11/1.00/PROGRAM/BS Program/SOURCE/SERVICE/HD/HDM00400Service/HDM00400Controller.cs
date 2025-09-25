using System.Diagnostics;
using System.Reflection;
using HDM00400Back;
using HDM00400Common;
using HDM00400Common.DTOs;
using HDM00400Common.Param;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace HDM00400Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class HDM00400Controller : ControllerBase, IHDM00400
{
    private LoggerHDM00400 _logger;
    private readonly ActivitySource _activitySource;

    public HDM00400Controller(ILogger<HDM00400Controller> logger)
    {
        //Initial and Get Logger
        LoggerHDM00400.R_InitializeLogger(logger);
        _logger = LoggerHDM00400.R_GetInstanceLogger();
        _activitySource = HDM00400Activity.R_InitializeAndGetActivitySource(nameof(HDM00400Controller));
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<HDM00400PublicLocationDTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<HDM00400PublicLocationDTO> poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(R_ServiceGetRecord));
        _logger.LogInfo("Start - Get Public Location Record");

        R_Exception loEx = new();
        R_ServiceGetRecordResultDTO<HDM00400PublicLocationDTO> loRtn =
            new R_ServiceGetRecordResultDTO<HDM00400PublicLocationDTO>();

        try
        {
            var loCls = new HDM00400Cls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Get Public Location Record");
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Public Location Record");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<HDM00400PublicLocationDTO> R_ServiceSave(
        R_ServiceSaveParameterDTO<HDM00400PublicLocationDTO> poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_ServiceSave));
        _logger.LogInfo("Start - Save Public Location Entity");
        R_Exception loEx = new();
        R_ServiceSaveResultDTO<HDM00400PublicLocationDTO> loRtn = null;
        HDM00400Cls loCls;

        try
        {
            loCls = new HDM00400Cls();
            loRtn = new R_ServiceSaveResultDTO<HDM00400PublicLocationDTO>();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Save Public Location Entity");
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save Public Location Entity");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<HDM00400PublicLocationDTO> poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_ServiceDelete));
        _logger.LogInfo("Start - Delete Public Location Entity");
        R_Exception loEx = new();
        R_ServiceDeleteResultDTO loRtn = new();
        HDM00400Cls loCls;

        try
        {
            loCls = new HDM00400Cls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Delete Public Location Entity");
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Delete Public Location Entity");
        return loRtn;
    }

    [HttpPost]
    public HDM00400ListDTO<HDM00400PropertyDTO> HDM00400GetPropertyList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(HDM00400GetPropertyList));
        _logger.LogInfo("Start - Get Property List");
        R_Exception loEx = new();
        HDM00400ListDTO<HDM00400PropertyDTO> loRtn = null;
        List<HDM00400PropertyDTO> loResult;
        HDM00400ParameterDb loDbPar;
        HDM00400Cls loCls;

        try
        {
            loDbPar = new HDM00400ParameterDb();

            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

            loCls = new HDM00400Cls();

            _logger.LogInfo("Get Property List");
            loResult = loCls.GetPropertyList(loDbPar);
            loRtn = new HDM00400ListDTO<HDM00400PropertyDTO> { Data = loResult };
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Property List");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<HDM00400PublicLocationDTO> HDM00400GetPublicLocationListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(HDM00400GetPublicLocationListStream));
        _logger.LogInfo("Start - Get Tax List");
        R_Exception loEx = new();
        HDM00400ParameterDb loDbPar;
        List<HDM00400PublicLocationDTO> loRtnTmp;
        HDM00400Cls loCls;
        IAsyncEnumerable<HDM00400PublicLocationDTO> loRtn = null;

        try
        {
            loDbPar = new HDM00400ParameterDb();

            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(HDM00400ContextConstant.CPROPERTY_ID);
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

            loCls = new HDM00400Cls();
            _logger.LogInfo("Get Tax List");
            loRtnTmp = loCls.GetPublicLocationList(loDbPar);

            loRtn = GetStream(loRtnTmp);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Tax List");
        return loRtn;
    }

    [HttpPost]
    public HDM00400SingleDTO<HDM00400ActiveInactiveDTO> HDM00400ActivateInactivate(
        HDM00400ActiveInactiveParamsDTO poParams)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(HDM00400ActivateInactivate));
        _logger.LogInfo("Start - Set Active/Inactive");
        R_Exception loEx = new();
        HDM00400ParameterDb loDbPar = new HDM00400ParameterDb();
        HDM00400SingleDTO<HDM00400ActiveInactiveDTO> loRtn = new HDM00400SingleDTO<HDM00400ActiveInactiveDTO>();
        HDM00400Cls loCls = new HDM00400Cls();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CPROPERTY_ID = poParams.CPROPERTY_ID;
            loDbPar.CPUBLIC_LOC_ID = poParams.CPUBLIC_LOC_ID;
            loDbPar.LACTIVE = poParams.LACTIVE;

            _logger.LogInfo("Set Active/Inactive");
            loRtn.Data = loCls.SetActiveInactive(loDbPar);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Set Active/Inactive");
        return loRtn;
    }

    [HttpPost]
    public HDM00400PublicLocationExcelDTO HDM00400DownloadTemplateFileModel()
    {
        using var loActivity = _activitySource.StartActivity(nameof(HDM00400DownloadTemplateFileModel));
        _logger.LogInfo("Start - Download Template File");
        var loEx = new R_Exception();
        var loRtn = new HDM00400PublicLocationExcelDTO();

        try
        {
            _logger.LogInfo("Get Template File");
            var loAsm = Assembly.Load("BIMASAKTI_HD_API");

            _logger.LogInfo("Set Resource File");
            var lcResourceFile = "BIMASAKTI_HD_API.Template.PublicLocation.xlsx";

            _logger.LogInfo("Get Resource File");
            using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
            {
                var ms = new MemoryStream();
                resFilestream.CopyTo(ms);
                var bytes = ms.ToArray();

                loRtn.FileBytes = bytes;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Download Template File");
        return loRtn;
    }

    #region "Helper GetSalesTaxStream Functions"

    private async IAsyncEnumerable<T> GetStream<T>(List<T> poParameter)
    {
        foreach (var item in poParameter)
        {
            yield return item;
        }
    }

    #endregion
}