using GSM05000Back;
using GSM05000Common;
using GSM05000Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM05000Sevice;

[ApiController]
[Route("api/[controller]/[action]")]
public class GSM05000ApprovalUserController : ControllerBase, IGSM05000ApprovalUser
{
    [HttpPost]
    public R_ServiceGetRecordResultDTO<GSM05000ApprovalUserDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM05000ApprovalUserDTO> poParameter)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<GSM05000ApprovalUserDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM05000ApprovalUserDTO> poParameter)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05000ApprovalUserDTO> poParameter)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public GSM05000ListDTO<GSM05000ApprovalUserDTO> GSM05000GetApprovalList()
    {
        R_Exception loEx = new R_Exception();
        GSM05000ListDTO<GSM05000ApprovalUserDTO> loRtn = null;
        List<GSM05000ApprovalUserDTO> loResult;
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb();
            
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CTRANSACTION_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CTRANSACTION_CODE);

            loCls = new GSM05000ApprovalUserCls();
            loResult = loCls.GSM05000GetApprovalUser(loDbPar);
            loRtn = new GSM05000ListDTO<GSM05000ApprovalUserDTO> { Data = loResult };
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    [HttpPost]
    public GSM05000ApprovalHeaderDTO GSM05000GetApprovalHeader()
    {
        R_Exception loEx = new R_Exception();
        GSM05000ApprovalHeaderDTO loRtn = null;
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb();
            
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CTRANSACTION_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CTRANSACTION_CODE);

            loCls = new GSM05000ApprovalUserCls();
            loRtn = loCls.GSM05000GetApprovalHeader(loDbPar);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    [HttpPost]
    public GSM05000ListDTO<GSM05000ApprovalDepartmentDTO> GSM05000GetApprovalDepartment()
    {
        R_Exception loEx = new R_Exception();
        List<GSM05000ApprovalDepartmentDTO> loResult;
        GSM05000ListDTO<GSM05000ApprovalDepartmentDTO> loRtn = null;
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb();
            
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

            loCls = new GSM05000ApprovalUserCls();
            loResult = loCls.GSM05000GetApprovalDepartment(loDbPar);
            loRtn = new GSM05000ListDTO<GSM05000ApprovalDepartmentDTO> { Data = loResult };
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
}