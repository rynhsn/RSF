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
        R_Exception loEx = new R_Exception();
        R_ServiceGetRecordResultDTO<GSM05000ApprovalUserDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM05000ApprovalUserDTO>();

        try
        {
            var loCls = new GSM05000ApprovalUserCls();
            loRtn = new R_ServiceGetRecordResultDTO<GSM05000ApprovalUserDTO>
            {
                data = loCls.R_GetRecord(poParameter.Entity)
            };
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
        
        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<GSM05000ApprovalUserDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM05000ApprovalUserDTO> poParameter)
    {
        R_Exception loEx = new R_Exception();
        R_ServiceSaveResultDTO<GSM05000ApprovalUserDTO> loRtn = null;
        GSM05000ApprovalUserCls loCls;

        try
        {
            loCls = new GSM05000ApprovalUserCls();
            loRtn = new R_ServiceSaveResultDTO<GSM05000ApprovalUserDTO>();
            
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;

            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05000ApprovalUserDTO> poParameter)
    {
        R_Exception loEx = new R_Exception();
        R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
        GSM05000ApprovalUserCls loCls;

        try
        {
            loCls = new GSM05000ApprovalUserCls();
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
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
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CUSER_ID = R_BackGlobalVar.USER_ID,
                CTRANSACTION_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CTRANSACTION_CODE),
                
            };

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
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CTRANSACTION_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CTRANSACTION_CODE)
            };

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
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID
            };

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

    [HttpPost]
    public string GSM05000ValidationForAction()
    {
        
        R_Exception loEx = new R_Exception();
        string loRtn = null;
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalUserCls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CUSER_ID = R_BackGlobalVar.USER_ID,
                CTRANSACTION_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CTRANSACTION_CODE),
                CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CDEPT_CODE),
            };

            loCls = new GSM05000ApprovalUserCls();
            loRtn = loCls.GSM05000ValidationForAction(loDbPar);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
}