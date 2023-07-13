﻿using System;
using System.Threading.Tasks;
using GLM00500Common;
using GLM00500Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GLM00500Model;

public class GLM00500DetailModel : R_BusinessObjectServiceClientBase<GLM00500BudgetDTDTO>, IGLM00500Detail
{
    private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
    private const string DEFAULT_SERVICEPOINT_NAME = "api/GLM00500Detail";
    private const string DEFAULT_MODULE = "gl";
    public GLM00500DetailModel(
        string pcHttpClientName = DEFAULT_HTTP_NAME, 
        string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME, 
        string pcModuleName = DEFAULT_HTTP_NAME, 
        bool plSendWithContext = true, 
        bool plSendWithToken = true) : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
    {
    }

    #region IGLM00500Detail Members not implemented

    public GLM00500ListDTO<GLM00500BudgetDTGridDTO> GLM00500GetBudgetDTList()
    {
        throw new NotImplementedException();
    }

    public GLM00500ListDTO<GLM00500FunctionDTO> GLM00500GetRoundingMethodList()
    {
        throw new NotImplementedException();
    }

    public GLM00500PeriodCount GLM00500GetPeriodCount()
    {
        throw new NotImplementedException();
    }
        
    #endregion
        
    public async Task<GLM00500ListDTO<GLM00500BudgetDTGridDTO>> GLM00500GetBudgetDTListModel()
    {
        var loEx = new R_Exception();
        GLM00500ListDTO<GLM00500BudgetDTGridDTO> loResult = null;
            
        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00500ListDTO<GLM00500BudgetDTGridDTO>>(
                _RequestServiceEndPoint,
                nameof(IGLM00500Detail.GLM00500GetBudgetDTList),
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
        
    public async Task<GLM00500ListDTO<GLM00500FunctionDTO>> GLM00500GetRoundingMethodListModel()
    {
        var loEx = new R_Exception();
        GLM00500ListDTO<GLM00500FunctionDTO> loResult = null;
            
        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00500ListDTO<GLM00500FunctionDTO>>(
                _RequestServiceEndPoint,
                nameof(IGLM00500Detail.GLM00500GetRoundingMethodList),
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

    public async Task<GLM00500PeriodCount> GLM00500GetPeriodCountModel()
    {
        var loEx = new R_Exception();
        GLM00500PeriodCount loResult = null;
            
        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00500PeriodCount>(
                _RequestServiceEndPoint,
                nameof(IGLM00500Detail.GLM00500GetPeriodCount),
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