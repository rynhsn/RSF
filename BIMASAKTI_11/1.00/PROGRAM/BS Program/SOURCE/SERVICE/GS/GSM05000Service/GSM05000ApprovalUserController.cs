using GSM05000Back;
using GSM05000Common;
using GSM05000Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM05000Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class GSM05000ApprovalUserController : ControllerBase, IGSM05000ApprovalUser
{
    private LoggerGSM05000 _logger;
    
    public GSM05000ApprovalUserController(ILogger<GSM05000ApprovalUserController> logger)
    {
        //Initial and Get Logger
        LoggerGSM05000.R_InitializeLogger(logger);
        _logger = LoggerGSM05000.R_GetInstanceLogger();
    }
    
    [HttpPost]
    public R_ServiceGetRecordResultDTO<GSM05000ApprovalUserDTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<GSM05000ApprovalUserDTO> poParameter)
    {
        _logger.LogInfo("Start - Get Approval User Record");
        R_Exception loEx = new();
        R_ServiceGetRecordResultDTO<GSM05000ApprovalUserDTO> loRtn = new();

        try
        {
            var loCls = new GSM05000ApprovalUserCls();
            
            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
            
            _logger.LogInfo("Get Approval User Record");
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Approval User Record");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<GSM05000ApprovalUserDTO> R_ServiceSave(
        R_ServiceSaveParameterDTO<GSM05000ApprovalUserDTO> poParameter)
    {
        _logger.LogInfo("Start - Save Approval User Entity");
        R_Exception loEx = new();
        R_ServiceSaveResultDTO<GSM05000ApprovalUserDTO> loRtn = null;
        GSM05000ApprovalUserCls loCls;

        try
        {
            loCls = new GSM05000ApprovalUserCls();
            loRtn = new R_ServiceSaveResultDTO<GSM05000ApprovalUserDTO>();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Save Approval User Entity");
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save Approval User Entity");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05000ApprovalUserDTO> poParameter)
    {
        _logger.LogInfo("Start - Delete Approval User Entity");
        R_Exception loEx = new();
        R_ServiceDeleteResultDTO loRtn = new();
        GSM05000ApprovalUserCls loCls;

        try
        {
            loCls = new GSM05000ApprovalUserCls();
            
            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
            
            _logger.LogInfo("Delete Approval User Entity");
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Delete Approval User Entity");
        return loRtn;
    }


    [HttpPost]
    public IAsyncEnumerable<GSM05000ApprovalUserDTO> GSM05000GetApprovalListStream()
    {
        _logger.LogInfo("Start - Get Approval User List Stream");
        R_Exception loEx = new();
        IAsyncEnumerable<GSM05000ApprovalUserDTO> loRtn = null;
        List<GSM05000ApprovalUserDTO> loResult;
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID,
                CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CTRANSACTION_CODE),
            };
            
            _logger.LogInfo("Get Approval User List");
            loCls = new GSM05000ApprovalUserCls();
            loResult = loCls.GSM05000GetApprovalUser(loDbPar);
            // loRtn = GetApprovalStream(loResult);
            loRtn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Approval User List Stream");
        return loRtn;
    }

    [HttpPost]
    public GSM05000ApprovalHeaderDTO GSM05000GetApprovalHeader(GSM05000TrxCodeParamsDTO poParams)
    {
        _logger.LogInfo("Start - Get Approval Header");
        R_Exception loEx = new();
        GSM05000ApprovalHeaderDTO loRtn = null;
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CTRANS_CODE = poParams.CTRANS_CODE
            };

            loCls = new GSM05000ApprovalUserCls();
            
            _logger.LogInfo("Get Approval Header");
            loRtn = loCls.GSM05000GetApprovalHeader(loDbPar);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Approval Header");
        return loRtn;
    }

    [HttpPost]
    public string GSM05000ValidationForAction(GSM05000TrxDeptParamsDTO poParams)
    {
        _logger.LogInfo("Start - Validate For Action");
        R_Exception loEx = new();
        string loRtn = null;
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CUSER_ID = R_BackGlobalVar.USER_ID,
                CTRANS_CODE = poParams.CTRANSACTION_CODE,
                CDEPT_CODE = poParams.CDEPT_CODE,
            };

            loCls = new GSM05000ApprovalUserCls();
            
            _logger.LogInfo("Validate For Action");
            loRtn = loCls.GSM05000ValidationForAction(loDbPar);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Validate For Action");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> GSM05000GetApprovalDepartmentStream()
    {
        _logger.LogInfo("Start - Get Approval Department Stream");
        R_Exception loEx = new();
        List<GSM05000ApprovalDepartmentDTO> loResult;
        IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> loRtn = null;
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CUSER_ID = R_BackGlobalVar.USER_ID
            };

            loCls = new GSM05000ApprovalUserCls();
            
            _logger.LogInfo("Get Approval Department");
            loResult = loCls.GSM05000GetApprovalDepartment(loDbPar);
            // loRtn = GetApprovalDepartmentStream(loResult);
            loRtn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Approval Department Stream");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> GSM05000DepartmentChangeSequenceStream(
        GSM05000TrxCodeParamsDTO poParams)
    {
        _logger.LogInfo("Start - Department Change Sequence Stream");
        R_Exception loEx = new();
        List<GSM05000ApprovalDepartmentDTO> loResult;
        IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> loRtn = null;
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CUSER_ID = R_BackGlobalVar.USER_ID,
                CTRANS_CODE = poParams.CTRANS_CODE
            };

            loCls = new GSM05000ApprovalUserCls();
            
            _logger.LogInfo("Department Change Sequence");
            loResult = loCls.GSM05000DepartmentChangeSequence(loDbPar);
            // loRtn = DepartmentChangeSequenceStream(loResult);
            loRtn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Department Change Sequence Stream");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<GSM05000ApprovalUserDTO> GSM05000GetUserSequenceDataStream()
    {
        _logger.LogInfo("Start - Get User Sequence Data Stream");
        R_Exception loEx = new();
        List<GSM05000ApprovalUserDTO> loResult;
        IAsyncEnumerable<GSM05000ApprovalUserDTO> loRtn = null;
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID,
                CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CTRANSACTION_CODE),
                CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CDEPT_CODE)
            };

            loCls = new GSM05000ApprovalUserCls();
            
            _logger.LogInfo("Get User Sequence Data");
            loResult = loCls.GSM05000GetUserSequenceData(loDbPar);
            // loRtn = GetUserSequenceDataStream(loResult);
            loRtn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get User Sequence Data Stream");
        return loRtn;
    }

    [HttpPost]
    public void GSM05000UpdateSequence(List<GSM05000ApprovalUserDTO> poEntity)
    {
        _logger.LogInfo("Start - Update Sequence");
        var loEx = new R_Exception();

        try
        {
            var loCls = new GSM05000ApprovalUserCls();
            
            _logger.LogInfo("Update Sequence");
            foreach (var loItem in poEntity)
            {
                loItem.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loItem.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
                loCls.GSM05000UpdateSequence(loItem);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Update Sequence");
    }

    [HttpPost]
    // public IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> GSM05000LookupApprovalDepartmentStream(GSM05000DeptCodeParamsDTO poParams)
    public IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> GSM05000LookupApprovalDepartmentStream()
    {
        _logger.LogInfo("Start - Lookup Approval Department Stream");
        R_Exception loEx = new();
        List<GSM05000ApprovalDepartmentDTO> loResult;
        IAsyncEnumerable<GSM05000ApprovalDepartmentDTO> loRtn = null;
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CUSER_ID = R_BackGlobalVar.USER_ID
                // CDEPT_CODE = poParams.CDEPT_CODE,
            };

            loCls = new GSM05000ApprovalUserCls();
            
            _logger.LogInfo("Lookup Approval Department");
            loResult = loCls.GSM05000LookupApprovalDepartment(loDbPar);
            // loRtn = LookupApprovalDepartmentStream(loResult);
            loRtn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Lookup Approval Department Stream");
        return loRtn;
    }

    [HttpPost]
    public void GSM05000CopyToApproval(GSM05000CopyToParamsDTO poParams)
    {
        _logger.LogInfo("Start - Copy To Approval");
        R_Exception loEx = new();
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CTRANS_CODE = poParams.CTRANSACTION_CODE,
                CDEPT_CODE = poParams.CDEPT_CODE,
                CDEPT_CODE_TO = poParams.CDEPT_CODE_TO,
                CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID
            };

            loCls = new GSM05000ApprovalUserCls();
            
            _logger.LogInfo("Copy To Approval");
            loCls.GSM05000ApprovalCopyTo(loDbPar);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Copy To Approval");
    }

    [HttpPost]
    public void GSM05000CopyFromApproval(GSM05000CopyFromParamsDTO poParams)
    {
        _logger.LogInfo("Start - Copy From Approval");
        R_Exception loEx = new();
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CTRANS_CODE = poParams.CTRANSACTION_CODE,
                CDEPT_CODE = poParams.CDEPT_CODE,
                CDEPT_CODE_FROM = poParams.CDEPT_CODE_FROM,
                CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID
            };

            loCls = new GSM05000ApprovalUserCls();
            
            _logger.LogInfo("Copy From Approval");
            loCls.GSM05000ApprovalCopyFrom(loDbPar);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Copy From Approval");
    }


    #region "Helper ListStream Functions"

    private async IAsyncEnumerable<T> GetStream<T>(List<T> poParameter)
    {
        foreach (T item in poParameter)
        {
            yield return item;
        }
    }

    #endregion
}