using System;
using System.Threading.Tasks;
using GSM05000Common;
using GSM05000Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GSM05000Model;

public class GSM05000ApprovalUserModel : R_BusinessObjectServiceClientBase<GSM05000ApprovalUserDTO>, IGSM05000ApprovalUser
{
    private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
    private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM05000ApprovalUser";
    private const string DEFAULT_MODULE = "gs";

    public GSM05000ApprovalUserModel(
        string pcHttpClientName = DEFAULT_HTTP_NAME,
        string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
        bool plSendWithContext = true,
        bool plSendWithToken = true) :
        base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
    {
    }

    public GSM05000ApprovalHeaderDTO GSM05000GetApprovalHeader()
    {
        throw new NotImplementedException();
    }

    public GSM05000ListDTO<GSM05000ApprovalUserDTO> GSM05000GetApprovalList()
    {
        throw new NotImplementedException();
    }

    public GSM05000ListDTO<GSM05000ApprovalDepartmentDTO> GSM05000GetApprovalDepartment()
    {
        throw new NotImplementedException();
    }

    public string GSM05000ValidationForAction()
    {
        throw new NotImplementedException();
    }

    public async Task<GSM05000ListDTO<GSM05000ApprovalDepartmentDTO>> GetApprovalDepartmentAsync()
    {
        var loEx = new R_Exception();
        GSM05000ListDTO<GSM05000ApprovalDepartmentDTO> loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05000ListDTO<GSM05000ApprovalDepartmentDTO>>(
                _RequestServiceEndPoint,
                nameof(IGSM05000ApprovalUser.GSM05000GetApprovalDepartment),
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
    
    public async Task<GSM05000ListDTO<GSM05000ApprovalUserDTO>> GetApprovalListAsync()
    {
        var loEx = new R_Exception();
        GSM05000ListDTO<GSM05000ApprovalUserDTO> loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05000ListDTO<GSM05000ApprovalUserDTO>>(
                _RequestServiceEndPoint,
                nameof(IGSM05000ApprovalUser.GSM05000GetApprovalList),
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
    
    public async Task<GSM05000ApprovalHeaderDTO> GetApprovalHeaderAsync()
    {
        var loEx = new R_Exception();
        GSM05000ApprovalHeaderDTO loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05000ApprovalHeaderDTO>(
                _RequestServiceEndPoint,
                nameof(IGSM05000ApprovalUser.GSM05000GetApprovalHeader),
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