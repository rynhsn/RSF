using GSM02000Back;
using GSM02000Common;
using GSM02000Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM02000Service;

[ApiController] 
[Route("api/[controller]/[action]")]
public class GSM02000Controller : ControllerBase, IGSM02000
{
    //Belum selesai, error spnya
    [HttpPost]
    public R_ServiceGetRecordResultDTO<GSM02000DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM02000DTO> poParameter)
    {
        R_Exception loEx = new R_Exception();
        R_ServiceGetRecordResultDTO<GSM02000DTO> loRtn = new R_ServiceGetRecordResultDTO<GSM02000DTO>();
        
        try
        {
            var loCls = new GSM02000Cls();
            loRtn = new R_ServiceGetRecordResultDTO<GSM02000DTO>();
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
    public R_ServiceSaveResultDTO<GSM02000DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02000DTO> poParameter)
    {
        R_Exception loEx = new R_Exception();
        R_ServiceSaveResultDTO<GSM02000DTO> loRtn = null;
        GSM02000Cls loCls;

        try
        {
            loCls = new GSM02000Cls();
            loRtn = new R_ServiceSaveResultDTO<GSM02000DTO>();
            
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
            
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
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02000DTO> poParameter)
    {
        R_Exception loEx = new R_Exception();
        R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
        GSM02000Cls loCls;

        try
        {
            loCls = new GSM02000Cls();
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
    public GSM02000ListDTO<GSM02000GridDTO> GetAllSalesTax()
    {
        R_Exception loEx = new R_Exception();
        GSM02000ListDTO<GSM02000GridDTO> loRtn = null;
        List<GSM02000GridDTO> loResult;
        GSM02000ParameterDb loDbPar;
        GSM02000Cls loCls;

        try
        {
            loDbPar = new GSM02000ParameterDb();
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

            loCls = new GSM02000Cls();
            loResult = loCls.SalesTaxListDb(loDbPar);
            loRtn = new GSM02000ListDTO<GSM02000GridDTO> { Data = loResult };
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<GSM02000GridDTO> GetAllSalesTaxStream()
    {
        R_Exception loEx = new R_Exception();
        GSM02000ParameterDb loDbPar;
        List<GSM02000GridDTO> loRtnTmp;
        GSM02000Cls loCls;
        IAsyncEnumerable<GSM02000GridDTO> loRtn = null;

        try
        {
            loDbPar = new GSM02000ParameterDb();
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

            loCls = new GSM02000Cls();
            loRtnTmp = loCls.SalesTaxListDb(loDbPar);

            loRtn = GetSalesTaxStream(loRtnTmp);
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
    public GSM02000ListDTO<GSM02000RoundingDTO> GetAllRounding()
    {
        R_Exception loEx = new R_Exception();
        GSM02000ListDTO<GSM02000RoundingDTO> loRtn = null;
        List<GSM02000RoundingDTO> loResult;
        GSM02000ParameterDb loDbPar;
        GSM02000Cls loCls;

        try
        {
            loDbPar = new GSM02000ParameterDb();
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE_MENU;

            loCls = new GSM02000Cls();
            loResult = loCls.RoundingListDb(loDbPar);
            loRtn = new GSM02000ListDTO<GSM02000RoundingDTO> { Data = loResult };
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;    
    }


    [HttpPost]
    public GSM02000ActiveInactiveDTO SetActiveInactive()
    {
        R_Exception loEx = new R_Exception();
        GSM02000ActiveInactiveDb loDbPar = new GSM02000ActiveInactiveDb();
        GSM02000ActiveInactiveDTO loRtn = new GSM02000ActiveInactiveDTO();
        GSM02000Cls loCls = new GSM02000Cls();

        try
        {
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CTAX_ID = R_Utility.R_GetContext<string>(ContextConstant.CTAX_ID);
            loDbPar.LACTIVE = R_Utility.R_GetContext<bool>(ContextConstant.LACTIVE);
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loCls.SetActiveInactiveDb(loDbPar);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    #region "Helper GetSalesTaxStream Functions"
    private async IAsyncEnumerable<GSM02000GridDTO> GetSalesTaxStream(List<GSM02000GridDTO> poParameter)
    {
        foreach (GSM02000GridDTO item in poParameter)
        {
            yield return item;
        }
    }
    #endregion
}