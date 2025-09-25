using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GSM05000Common;
using GSM05000Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GSM05000Model;

public class GSM05000ApprovalReplacementModel : R_BusinessObjectServiceClientBase<GSM05000ApprovalReplacementDTO>,
    IGSM05000ApprovalReplacement
{
    private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
    private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM05000ApprovalReplacement";
    private const string DEFAULT_MODULE = "gs";

    public GSM05000ApprovalReplacementModel(
        string pcHttpClientName = DEFAULT_HTTP_NAME,
        string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
        bool plSendWithContext = true,
        bool plSendWithToken = true) :
        base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
    {
    }

    public IAsyncEnumerable<GSM05000ApprovalReplacementDTO> GSM05000GetApprovalReplacementListStream()
    {
        throw new NotImplementedException();
    }

    public async Task<List<GSM05000ApprovalReplacementDTO>> GetReplacementListStreamAsync()
    {
        var loEx = new R_Exception();
        List<GSM05000ApprovalReplacementDTO> loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM05000ApprovalReplacementDTO>(
                _RequestServiceEndPoint,
                nameof(IGSM05000ApprovalReplacement.GSM05000GetApprovalReplacementListStream),
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