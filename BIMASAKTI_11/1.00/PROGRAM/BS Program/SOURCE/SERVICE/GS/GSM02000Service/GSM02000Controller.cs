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
        // GSM02000Cls loCls;
        
        try
        {
            var loCls = new GSM02000Cls();
            loRtn = new R_ServiceGetRecordResultDTO<GSM02000DTO>();
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
            // poParameter.Entity.CTAX_ID = R_Utility.R_GetContext<GSM02000ContextDTO>(ContextConstant.CTAX_ID);
            
            poParameter.Entity.CCOMPANY_ID = "RCD";
            poParameter.Entity.CUSER_ID = "Admin";
            
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
    public GSM02000ListDTO GetAllSalesTax()
    {
        R_Exception loEx = new R_Exception();
        GSM02000ListDTO loRtn = null;
        List<GSM02000GridDTO> loResult;
        GSM02000ParameterDb loDbPar;
        GSM02000Cls loCls;

        try
        {
            loDbPar = new GSM02000ParameterDb();
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

            // loDbPar.CCOMPANY_ID = "RCD";
            // loDbPar.CUSER_ID = "Admin";

            loCls = new GSM02000Cls();
            loResult = loCls.SalesTaxListDb(loDbPar);
            loRtn = new GSM02000ListDTO { Data = loResult };
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

            loDbPar.CCOMPANY_ID = "RCD";
            loDbPar.CUSER_ID = "Admin";

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
    public RoundingListDTO GetAllRounding()
    {
        R_Exception loEx = new R_Exception();
        RoundingListDTO loRtn = null;
        List<RoundingDTO> loResult;
        RoundingParameterDb loDbPar;
        GSM02000Cls loCls;

        try
        {
            loDbPar = new RoundingParameterDb();
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CCULTURE = R_BackGlobalVar.CULTURE;

            loCls = new GSM02000Cls();
            loResult = loCls.RoundingListDb(loDbPar);
            loRtn = new RoundingListDTO { Data = loResult };
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
            // await Task.Delay(5000);
        }
    }
    #endregion
}