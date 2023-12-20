using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GSM05000Common;
using GSM05000Common.DTOs;
using R_APIClient;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GSM05000Model;

public class GSM05000ApprovalUserModel : R_BusinessObjectServiceClientBase<GSM05000ApprovalUserDTO>,
    IGSM05000ApprovalUser
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

    public async Task<GSM05000ApprovalHeaderDTO> GetApprovalHeaderAsync(GSM05000TrxCodeParamsDTO poParams)
    {
        var loEx = new R_Exception();
        GSM05000ApprovalHeaderDTO loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult =
                await R_HTTPClientWrapper.R_APIRequestObject<GSM05000ApprovalHeaderDTO, GSM05000TrxCodeParamsDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05000ApprovalUser.GSM05000GetApprovalHeader),
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

    public async Task<List<GSM05000ApprovalUserDTO>> GetApprovalListStreamAsync()
    {
        var loEx = new R_Exception();
        List<GSM05000ApprovalUserDTO> loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM05000ApprovalUserDTO>(
                _RequestServiceEndPoint,
                nameof(IGSM05000ApprovalUser.GSM05000GetApprovalListStream),
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

    public async Task<List<GSM05000ApprovalDepartmentDTO>> GetApprovalDepartmentStreamAsync()
    {
        var loEx = new R_Exception();
        List<GSM05000ApprovalDepartmentDTO> loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM05000ApprovalDepartmentDTO>(
                _RequestServiceEndPoint,
                nameof(IGSM05000ApprovalUser.GSM05000GetApprovalDepartmentStream),
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

    public async Task<List<GSM05000ApprovalDepartmentDTO>> DepartmentChangeSequenceModelStream(
        GSM05000TrxCodeParamsDTO poParams)
    {
        var loEx = new R_Exception();
        List<GSM05000ApprovalDepartmentDTO> loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper
                .R_APIRequestStreamingObject<GSM05000ApprovalDepartmentDTO, GSM05000TrxCodeParamsDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05000ApprovalUser.GSM05000DepartmentChangeSequenceStream),
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

    public async Task<List<GSM05000ApprovalUserDTO>> GSM05000GetUserSequenceDataModelStream()
    {
        var loEx = new R_Exception();
        List<GSM05000ApprovalUserDTO> loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM05000ApprovalUserDTO>(
                _RequestServiceEndPoint,
                nameof(IGSM05000ApprovalUser.GSM05000GetUserSequenceDataStream),
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

    public async Task GSM05000UpdateSequenceModel(List<GSM05000ApprovalUserDTO> poEntity)
    {
        var loEx = new R_Exception();

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            await R_HTTPClientWrapper
                .R_APIRequestObject<GSM05000ListDTO<GSM05000ApprovalUserDTO>, List<GSM05000ApprovalUserDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05000ApprovalUser.GSM05000UpdateSequence),
                    poEntity,
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

    // public async Task<List<GSM05000ApprovalDepartmentDTO>> LookupApprovalDepartmentStreamAsync(GSM05000DeptCodeParamsDTO poParams)
    public async Task<List<GSM05000ApprovalDepartmentDTO>> LookupApprovalDepartmentStreamAsync()
    {
        var loEx = new R_Exception();
        List<GSM05000ApprovalDepartmentDTO> loResult = null;

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM05000ApprovalDepartmentDTO>(
                _RequestServiceEndPoint,
                nameof(IGSM05000ApprovalUser.GSM05000LookupApprovalDepartmentStream),
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

    public async Task CopyToApprovalAsync(GSM05000CopyToParamsDTO poParams)
    {
        var loEx = new R_Exception();

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            var loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05000SingleDTO<string>, GSM05000CopyToParamsDTO>(
                _RequestServiceEndPoint,
                nameof(IGSM05000ApprovalUser.GSM05000CopyToApproval),
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
    }

    public async Task CopyFromApprovalAsync(GSM05000CopyFromParamsDTO poParams)
    {
        var loEx = new R_Exception();

        try
        {
            R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
            var loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05000SingleDTO<string>, GSM05000CopyFromParamsDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05000ApprovalUser.GSM05000CopyFromApproval),
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
    }


    public GSM05000ApprovalHeaderDTO GSM05000GetApprovalHeader(GSM05000TrxCodeParamsDTO poParams)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<GSM05000ApprovalUserDTO> GSM05000GetApprovalListStream()
    {
        throw new NotImplementedException();
    }

    public string GSM05000ValidationForAction(GSM05000TrxDeptParamsDTO poParams)
    {
        throw new NotImplementedException();
    }


    public IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> GSM05000GetApprovalDepartmentStream()
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> GSM05000DepartmentChangeSequenceStream(
        GSM05000TrxCodeParamsDTO poParams)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<GSM05000ApprovalUserDTO> GSM05000GetUserSequenceDataStream()
    {
        throw new NotImplementedException();
    }

    public void GSM05000UpdateSequence(List<GSM05000ApprovalUserDTO> poEntity)
    {
        throw new NotImplementedException();
    }

    // public IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> GSM05000LookupApprovalDepartmentStream(GSM05000DeptCodeParamsDTO poParams)
    public IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> GSM05000LookupApprovalDepartmentStream()
    {
        throw new NotImplementedException();
    }

    public GSM05000SingleDTO<string> GSM05000CopyToApproval(GSM05000CopyToParamsDTO poParams)
    {
        throw new NotImplementedException();
    }

    public GSM05000SingleDTO<string> GSM05000CopyFromApproval(GSM05000CopyFromParamsDTO poParams)
    {
        throw new NotImplementedException();
    }
}