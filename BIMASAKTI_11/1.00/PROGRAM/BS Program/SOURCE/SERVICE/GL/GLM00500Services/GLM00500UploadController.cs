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
    public GLM00500UploadCheckErrorDTO GLM00500UploadCheckBudget(List<GLM00500UploadToSystemDTO> poUploadBudgetDTO)
    {
        var loEx = new R_Exception();
        var loCls = new GLM00500UploadCls();
        var loDbParams = new GLM00500ParameterUploadDb();
        var loReturn = new GLM00500UploadCheckErrorDTO();
        
        try
        {
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            
            loReturn = loCls.Validate(loDbParams, poUploadBudgetDTO);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
    
    [HttpPost]
    public void GLM00500UploadBudget(List<GLM00500UploadToSystemDTO> poUploadBudgetDTO)
    {
        var loEx = new R_Exception();
        var loCls = new GLM00500UploadCls();
        var loDbParams = new GLM00500ParameterUploadDb();
        
        try
        {
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROCESS_ID = R_Utility.R_GetStreamingContext<string>(GLM00500ContextContant.CPROCESS_ID);
            loCls.Upload(loDbParams, poUploadBudgetDTO);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }

    [HttpPost]
    public GLM00500ListDTO<GLM00500UploadFromSystemDTO> GLM00500UploadGetBudgetList()
    {
        var loEx = new R_Exception();
        var loCls = new GLM00500UploadCls();
        var loDbParams = new GLM00500ParameterUploadDb();
        var loResult = new List<GLM00500UploadFromSystemDTO>();
        var loReturn = new GLM00500ListDTO<GLM00500UploadFromSystemDTO>();
        
        try
        {
            loDbParams.CPROCESS_ID = R_Utility.R_GetStreamingContext<string>(GLM00500ContextContant.CPROCESS_ID);

            loResult = loCls.GetUploadList(loDbParams);
            loReturn.Data = loResult;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
    
    [HttpPost]
    public GLM00500UploadErrorDTO GLM00500UploadGetErrorMsg()
    {
        var loEx = new R_Exception();
        var loCls = new GLM00500UploadCls();
        var loDbParams = new GLM00500ParameterUploadDb();
        var loReturn = new GLM00500UploadErrorDTO();
        
        try
        {
            loDbParams.CREC_ID = R_Utility.R_GetStreamingContext<string>(GLM00500ContextContant.CREC_ID);
            loReturn = loCls.GetErrorMsg(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
}