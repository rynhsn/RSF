using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GLM00500Common;
using GLM00500Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GLM00500Model;

public class GLM00500UploadModel : R_BusinessObjectServiceClientBase<GLM00500UploadToSystemDTO>, IGLM00500Upload
{
    private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
    private const string DEFAULT_SERVICEPOINT_NAME = "api/GLM00500Upload";
    private const string DEFAULT_MODULE = "gl";

    public GLM00500UploadModel(
        string pcHttpClientName = DEFAULT_HTTP_NAME,
        string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
        string pcModuleName = DEFAULT_MODULE,
        bool plSendWithContext = true,
        bool plSendWithToken = true) : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext,
        plSendWithToken)
    {
    }


    public GLM00500UploadCheckErrorDTO GLM00500UploadCheckBudget(List<GLM00500UploadToSystemDTO> poUploadBudgetDTO)
    {
        throw new NotImplementedException();
    }

    public void GLM00500UploadBudget(List<GLM00500UploadToSystemDTO> poUploadBudgetDTO)
    {
        throw new NotImplementedException();
    }

    public GLM00500ListDTO<GLM00500UploadFromSystemDTO> GLM00500UploadGetBudgetList()
    {
        throw new NotImplementedException();
    }

    public GLM00500UploadErrorDTO GLM00500UploadGetErrorMsg()
    {
        throw new NotImplementedException();
    }
    
    
    public async Task<GLM00500UploadCheckErrorDTO> GLM00500UploadCheckBudgetModel(List<GLM00500UploadToSystemDTO> poUploadBudgetDTO)
    {
        var loEx = new R_Exception();
        GLM00500UploadCheckErrorDTO loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00500UploadCheckErrorDTO, List<GLM00500UploadToSystemDTO>>(
                _RequestServiceEndPoint,
                nameof(IGLM00500Upload.GLM00500UploadCheckBudget),
                poUploadBudgetDTO,
                DEFAULT_MODULE,
                _SendWithContext,
                _SendWithToken);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loResult;
    }
    
    public async Task GLM00500UploadBudgetModel(List<GLM00500UploadToSystemDTO> poUploadBudgetDTO)
    {
        var loEx = new R_Exception();

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            await R_HTTPClientWrapper.R_APIRequestObject<GLM00500UploadCheckErrorDTO, List<GLM00500UploadToSystemDTO>>(
                _RequestServiceEndPoint,
                nameof(IGLM00500Upload.GLM00500UploadBudget),
                poUploadBudgetDTO,
                DEFAULT_MODULE,
                _SendWithContext,
                _SendWithToken);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task<GLM00500ListDTO<GLM00500UploadErrorDTO>> GLM00500UploadGetErrorMsgModel(List<GLM00500UploadParameterGetErrorDTO> poParam)
    {
        var loEx = new R_Exception();
        GLM00500ListDTO<GLM00500UploadErrorDTO> loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00500ListDTO<GLM00500UploadErrorDTO>, List<GLM00500UploadParameterGetErrorDTO>>(
                _RequestServiceEndPoint,
                nameof(IGLM00500Upload.GLM00500UploadGetErrorMsg),
                poParam,
                DEFAULT_MODULE,
                _SendWithContext,
                _SendWithToken);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loResult;
    }
    
    public async Task<GLM00500ListDTO<GLM00500UploadFromSystemDTO>> GLM00500UploadGetBudgetListModel()
    {
        var loEx = new R_Exception();
        GLM00500ListDTO<GLM00500UploadFromSystemDTO> loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00500ListDTO<GLM00500UploadFromSystemDTO>>(
                _RequestServiceEndPoint,
                nameof(IGLM00500Upload.GLM00500UploadGetBudgetList),
                DEFAULT_MODULE,
                _SendWithContext,
                _SendWithToken);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loResult;
    }
}