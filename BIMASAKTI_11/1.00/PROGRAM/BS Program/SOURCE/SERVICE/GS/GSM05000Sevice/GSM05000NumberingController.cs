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
public class GSM05000NumberingController : ControllerBase, IGSM05000Numbering
{
    [HttpPost]
    public R_ServiceGetRecordResultDTO<GSM05000NumberingGridDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM05000NumberingGridDTO> poParameter)
    {
        R_Exception loEx = new R_Exception();
        R_ServiceGetRecordResultDTO<GSM05000NumberingGridDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM05000NumberingGridDTO>();

        try
        {
            var loCls = new GSM05000NumberingCls();
            loRtn = new R_ServiceGetRecordResultDTO<GSM05000NumberingGridDTO>();
            
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;


            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        EndBlock:
        loEx.ThrowExceptionIfErrors();
        
        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<GSM05000NumberingGridDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM05000NumberingGridDTO> poParameter)
    {
        R_Exception loEx = new R_Exception();
        R_ServiceSaveResultDTO<GSM05000NumberingGridDTO> loRtn = null;
        GSM05000NumberingCls loCls;

        try
        {
            loCls = new GSM05000NumberingCls();
            loRtn = new R_ServiceSaveResultDTO<GSM05000NumberingGridDTO>();
            
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
            
            // poParameter.Entity.CCOMPANY_ID = "rcd";
            // poParameter.Entity.CUSER_ID= "rhc";
            // poParameter.Entity.CDEPT_CODE = "";
            // poParameter.Entity.CCYEAR= "2023";
            // poParameter.Entity.CPERIOD_NO = "05";
            // poParameter.Entity.CTRANSACTION_CODE = "000000";
            // poParameter.Entity.ISTART_NUMBER = 2;
            // poParameter.CRUDMode = eCRUDMode.AddMode;
            
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        EndBlock:
        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05000NumberingGridDTO> poParameter)
    {
        R_Exception loEx = new R_Exception();
        R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
        GSM05000NumberingCls loCls;

        try
        {
            loCls = new GSM05000NumberingCls();
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
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
    public GSM05000ListDTO<GSM05000NumberingGridDTO> GetNumberingList()
    {
        R_Exception loEx = new R_Exception();
        GSM05000ListDTO<GSM05000NumberingGridDTO> loRtn = null;
        List<GSM05000NumberingGridDTO> loResult;
        GSM05000ParameterDb loDbPar;
        GSM05000NumberingCls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb();
            
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CTRANSACTION_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CTRANSACTION_CODE);

            // loDbPar.CCOMPANY_ID = "rcd";
            // loDbPar.CUSER_ID = "rhc";
            // loDbPar.CTRANSACTION_CODE = "000000";

            loCls = new GSM05000NumberingCls();
            loResult = loCls.GetNumberingListDb(loDbPar);
            loRtn = new GSM05000ListDTO<GSM05000NumberingGridDTO> { Data = loResult };
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    [HttpPost]
    public GSM05000NumberingHeaderDTO GetNumberingHeader()
    {
        R_Exception loEx = new R_Exception();
        GSM05000NumberingHeaderDTO loRtn = null;
        GSM05000ParameterDb loDbPar;
        GSM05000NumberingCls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb();
            
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CTRANSACTION_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CTRANSACTION_CODE);

            // loDbPar.CCOMPANY_ID = "rcd";
            // loDbPar.CTRANSACTION_CODE = "000000";
            
            loCls = new GSM05000NumberingCls();
            loRtn = loCls.GetNumberingHeaderDb(loDbPar);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
}