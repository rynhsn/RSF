using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GLI00100Common;
using GLI00100Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GLI00100Model;

public class GLI00100Model : R_BusinessObjectServiceClientBase<GLI00100AccountGridDTO>, IGLI00100
{
    private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
    private const string DEFAULT_SERVICEPOINT_NAME = "api/GLI00100";
    private const string DEFAULT_MODULE = "gl";

    public GLI00100Model(
        string pcHttpClientName = DEFAULT_HTTP_NAME,
        string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
        string pcModuleName = DEFAULT_MODULE,
        bool plSendWithContext = true,
        bool plSendWithToken = true) : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName,
        plSendWithContext,
        plSendWithToken)
    {
    }

    public GLI00100AccountDTO GLI00100GetAccountDetail(GLI00100AccountParameterDTO poParams)
    {
        throw new NotImplementedException();
    }

    public GLI00100AccountAnalysisDTO GLI00100GetAccountAnalysisDetail(GLI00100AccountAnalysisParameterDTO poParams)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<GLI00100BudgetDTO> GLI00100GetBudgetStream(GLI00100BudgetParamsDTO poParams)
    {
        throw new NotImplementedException();
    }


    public async Task<GLI00100AccountDTO> GLI00100GetAccountDetailModel(GLI00100AccountParameterDTO poParams)
    {
        var loEx = new R_Exception();
        GLI00100AccountDTO loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLI00100AccountDTO, GLI00100AccountParameterDTO>(
                _RequestServiceEndPoint,
                nameof(IGLI00100.GLI00100GetAccountDetail),
                poParams,
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
    
    public async Task<GLI00100AccountAnalysisDTO> GLI00100GetAccountAnalysisDetailModel(GLI00100AccountAnalysisParameterDTO poParams)
    {
        var loEx = new R_Exception();
        GLI00100AccountAnalysisDTO loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLI00100AccountAnalysisDTO, GLI00100AccountAnalysisParameterDTO>(
                _RequestServiceEndPoint,
                nameof(IGLI00100.GLI00100GetAccountAnalysisDetail),
                poParams,
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
    public async Task<List<GLI00100BudgetDTO>> GLI00100GetBudgetStreamModel(GLI00100BudgetParamsDTO poParams)
    {
        var loEx = new R_Exception();
        List<GLI00100BudgetDTO> loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLI00100BudgetDTO, GLI00100BudgetParamsDTO>(
                _RequestServiceEndPoint,
                nameof(IGLI00100.GLI00100GetBudgetStream),
                poParams,
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