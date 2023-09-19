using GLM00500Back;
using GLM00500Common;
using GLM00500Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;

namespace GLM00500Services;

[ApiController]
[Route("api/[controller]/[action]")]
public class GLM00500UploadController : ControllerBase, IGLM00500Upload
{

    [HttpPost]
    public GLM00500UploadErrorReturnDTO GLM00500UploadGetBudgetList()
    {
        var loEx = new R_Exception();
        var loCls = new GLM00500UploadCls();
        var loRtn = new GLM00500UploadErrorReturnDTO();

        try
        {
            var lcCompanyID = R_BackGlobalVar.COMPANY_ID;
            var lcUserID = R_BackGlobalVar.USER_ID;
            var keyGuid = R_Utility.R_GetStreamingContext<string>(GLM00500ContextContant.CKEY_GUID);
            loRtn = loCls.GetUploadList(keyGuid, lcCompanyID, lcUserID);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }
}