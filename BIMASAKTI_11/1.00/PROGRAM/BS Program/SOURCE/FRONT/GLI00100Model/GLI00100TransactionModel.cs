using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GLI00100Common;
using GLI00100Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GLI00100Model;

public class GLI00100TransactionModel : R_BusinessObjectServiceClientBase<GLI00100JournalDTO>, IGLI00100Transaction
{
    private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
    private const string DEFAULT_SERVICEPOINT_NAME = "api/GLI00100Transaction";
    private const string DEFAULT_MODULE = "gl";

    public GLI00100TransactionModel(
        string pcHttpClientName = DEFAULT_HTTP_NAME,
        string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
        string pcModuleName = DEFAULT_MODULE,
        bool plSendWithContext = true,
        bool plSendWithToken = true) : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName,
        plSendWithContext,
        plSendWithToken)
    {
    }

    public GLI00100JournalDTO GLI00100GetJournalDetail(GLI00100JournalParamDTO poParams)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<GLI00100JournalGridDTO> GLI00100GetJournalGridStream()
    {
        throw new NotImplementedException();
    }
    
    public async Task<GLI00100JournalDTO> GLI00100GetJournalDetailModel(
        GLI00100JournalParamDTO poParams)
    {
        var loEx = new R_Exception();
        GLI00100JournalDTO loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLI00100JournalDTO, GLI00100JournalParamDTO>(
                _RequestServiceEndPoint,
                nameof(IGLI00100Transaction.GLI00100GetJournalDetail),
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

    public async Task<List<GLI00100JournalGridDTO>> GLI00100GetJournalGridStreamModel()
    {
        var loEx = new R_Exception();
        List<GLI00100JournalGridDTO> loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLI00100JournalGridDTO>(
                _RequestServiceEndPoint,
                nameof(IGLI00100Transaction.GLI00100GetJournalGridStream),
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