using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GSM05000Common;
using GSM05000Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GSM05000Model;

public class GSM05000NumberingModel : R_BusinessObjectServiceClientBase<GSM05000NumberingGridDTO>, IGSM05000Numbering
{
    private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
    private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM05000Numbering";
    private const string DEFAULT_MODULE = "gs";

    public GSM05000NumberingModel(
        string pcHttpClientName = DEFAULT_HTTP_NAME,
        string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
        bool plSendWithContext = true,
        bool plSendWithToken = true) :
        base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
    {
    }

    public IAsyncEnumerable<GSM05000NumberingGridDTO> GetNumberingListStream()
    {
        throw new NotImplementedException();
    }

    public GSM05000NumberingHeaderDTO GetNumberingHeader(GSM05000TrxCodeParamsDTO poParams)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<GSM05000NumberingGridDTO>> GetNumberingListStreamAsync()
    {
        var loEx = new R_Exception();
        List<GSM05000NumberingGridDTO> loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM05000NumberingGridDTO>(
                _RequestServiceEndPoint,
                nameof(IGSM05000Numbering.GetNumberingListStream),
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

    public async Task<GSM05000NumberingHeaderDTO> GetNumberingHeaderAsync(GSM05000TrxCodeParamsDTO poParams)
    {
        var loEx = new R_Exception();
        GSM05000NumberingHeaderDTO loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05000NumberingHeaderDTO, GSM05000TrxCodeParamsDTO>(
                _RequestServiceEndPoint,
                nameof(IGSM05000Numbering.GetNumberingHeader),
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