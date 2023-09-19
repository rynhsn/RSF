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

    public GLM00500UploadErrorReturnDTO GLM00500UploadGetBudgetList()
    {
        throw new NotImplementedException();
    }

    public async Task<GLM00500UploadErrorReturnDTO> GLM00500UploadGetBudgetListModel()
    {
        var loEx = new R_Exception();
        GLM00500UploadErrorReturnDTO loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00500UploadErrorReturnDTO>(
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