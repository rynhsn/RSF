using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GLI00100Common;
using GLI00100Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GLI00100Model;

public class GLI00100AccountJournalModel : R_BusinessObjectServiceClientBase<GLI00100AccountAnalysisDetailDTO>, IGLI00100AccountJournal
{
    private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
    private const string DEFAULT_SERVICEPOINT_NAME = "api/GLI00100AccountJournal";
    private const string DEFAULT_MODULE = "gl";

    public GLI00100AccountJournalModel(
        string pcHttpClientName = DEFAULT_HTTP_NAME,
        string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
        string pcModuleName = DEFAULT_MODULE,
        bool plSendWithContext = true,
        bool plSendWithToken = true) : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName,
        plSendWithContext,
        plSendWithToken)
    {
    }

    public GLI00100AccountAnalysisDetailDTO GLI00100GetAccountAnalysisDetail(
        GLI00100AccountAnalysisDetailParamDTO poParams)
    {
        throw new System.NotImplementedException();
    }

    public IAsyncEnumerable<GLI00100TransactionGridDTO> GLI00100GetTransactionGridStream()
    {
        throw new System.NotImplementedException();
    }
    
    
    public async Task<GLI00100AccountAnalysisDetailDTO> GLI00100GetAccountAnalysisDetailModel(
        GLI00100AccountAnalysisDetailParamDTO poParams)
    {
        var loEx = new R_Exception();
        GLI00100AccountAnalysisDetailDTO loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLI00100AccountAnalysisDetailDTO, GLI00100AccountAnalysisDetailParamDTO>(
                _RequestServiceEndPoint,
                nameof(IGLI00100AccountJournal.GLI00100GetAccountAnalysisDetail),
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

    public async Task<List<GLI00100TransactionGridDTO>> GLI00100GetTransactionGridStreamModel()
    {
        var loEx = new R_Exception();
        List<GLI00100TransactionGridDTO> loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLI00100TransactionGridDTO>(
                _RequestServiceEndPoint,
                nameof(IGLI00100AccountJournal.GLI00100GetTransactionGridStream),
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