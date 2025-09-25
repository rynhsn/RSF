using System.Collections;
using System.Globalization;
using BaseHeaderReportCOMMON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMR00400Back;
using PMR00400Common.DTOs;
using PMR00400Common.DTOs.Print;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;

namespace PMR00400Service;

public class PMR00400PrintController : R_ReportControllerBase
{
    private R_ReportFastReportBackClass _ReportCls;
    private PMR00400ParamDTO _Parameter;
    public PMR00400PrintController()
    {
        _ReportCls = new R_ReportFastReportBackClass();
        _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
        _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
    }
    
    
    private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcfiletemplate)
    {
        pcfiletemplate = Path.Combine("Reports", "PMR00400Unit.frx");
    }

    private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
    {
        poData.Add(GeneratePrint(_Parameter));
        pcDataSourceName = "ResponseDataModel";
    }
    
    
    [HttpPost]
    public R_DownloadFileResultDTO ActivityReportPost(PMR00400ParamDTO poParam)
    {
        R_Exception loException = new();
        PMR00400ReportLogKeyDTO loCache = null;
        R_DownloadFileResultDTO loRtn = null;
        try
        {
            loRtn = new R_DownloadFileResultDTO();
            loCache = new PMR00400ReportLogKeyDTO
            {
                poParam = poParam,
                poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
            };

            R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte(loCache));
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();
        return loRtn;
    }

    [HttpGet, AllowAnonymous]
    public FileStreamResult ActivityReportGet(string pcGuid)
    {
        R_Exception loException = new();
        FileStreamResult loRtn = null;
        PMR00400ReportLogKeyDTO loResultGUID = null;
        try
        {
            //Get Parameter
            loResultGUID =
                R_NetCoreUtility.R_DeserializeObjectFromByte<PMR00400ReportLogKeyDTO>(
                    R_DistributedCache.Cache.Get(pcGuid));

            //Get Data and Set Log Key
            R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);

            _Parameter = loResultGUID.poParam;
            loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();
        return loRtn;
    }

    private PMR00400ReportWithBaseHeaderDTO GeneratePrint(PMR00400ParamDTO poParam)
    {
        var loEx = new R_Exception();
        var loRtn = new PMR00400ReportWithBaseHeaderDTO();
        var loCultureInfo = new CultureInfo(R_BackGlobalVar.REPORT_CULTURE);

        try
        {
            loRtn.BaseHeaderColumn.Page = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
                "Page", loCultureInfo);
            loRtn.BaseHeaderColumn.Of =
                R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Of", loCultureInfo);
            loRtn.BaseHeaderColumn.Print_Date =
                R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class), "Print_Date", loCultureInfo);
            loRtn.BaseHeaderColumn.Print_By = R_Utility.R_GetMessage(typeof(BaseHeaderResources.Resources_Dummy_Class),
                "Print_By", loCultureInfo);

            var loLabelObject = new PMR00400LabelDTO();
            var loLabel = AssignValuesWithMessages(typeof(PMR00400BackResources.Resources_Dummy_Class),
                loCultureInfo, loLabelObject);
            
            var loCls = new PMR00400Cls();
            var loLogo = loCls.GetBaseHeaderLogoCompany(poParam.CCOMPANY_ID);
            
            loRtn.BaseHeaderData = new BaseHeaderDTO
            {
                BLOGO_COMPANY = loLogo.CLOGO,
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "PMR00400",
                CPRINT_NAME = "Unit List",
                CUSER_ID = poParam.CUSER_ID,
            };

            var loData = new PMR00400ReportResultDTO()
            {
                Title = "PMR00400 Unit List",
                Label = (PMR00400LabelDTO)loLabel,
                Header = null,
                Data = new List<PMR00400DataDTO>()
            };

            loData.Data = loCls.GetDataDb(poParam);

            loData.Header = poParam;
            
            loRtn.Data = loData;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    private object AssignValuesWithMessages(Type poResourceType, CultureInfo poCultureInfo, object poObject)
    {
        object loObj = Activator.CreateInstance(poObject.GetType());
        var loGetPropertyObject = poObject.GetType().GetProperties();

        foreach (var property in loGetPropertyObject)
        {
            string propertyName = property.Name;
            string message = R_Utility.R_GetMessage(poResourceType, propertyName, poCultureInfo);
            property.SetValue(loObj, message);
        }

        return loObj;
    }

}