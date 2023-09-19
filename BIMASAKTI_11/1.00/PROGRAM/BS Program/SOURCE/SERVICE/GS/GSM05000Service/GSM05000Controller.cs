using GSM05000Back;
using GSM05000Common;
using GSM05000Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM05000Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class GSM05000Controller : ControllerBase, IGSM05000
{
    [HttpPost]
    public R_ServiceGetRecordResultDTO<GSM05000DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM05000DTO> poParameter)
    {
        R_Exception loEx = new R_Exception();
        R_ServiceGetRecordResultDTO<GSM05000DTO> loRtn = new R_ServiceGetRecordResultDTO<GSM05000DTO>();

        try
        {
            var loCls = new GSM05000Cls();
            loRtn = new R_ServiceGetRecordResultDTO<GSM05000DTO>();
            
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            
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
    public R_ServiceSaveResultDTO<GSM05000DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM05000DTO> poParameter)
    {
        R_Exception loEx = new R_Exception();
        R_ServiceSaveResultDTO<GSM05000DTO> loRtn = null;
        GSM05000Cls loCls;

        try
        {
            loCls = new GSM05000Cls();
            loRtn = new R_ServiceSaveResultDTO<GSM05000DTO>();
            
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            
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
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05000DTO> poParameter)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public IAsyncEnumerable<GSM05000GridDTO> GetTransactionCodeListStream()
    {
        R_Exception loEx = new R_Exception();
        IAsyncEnumerable<GSM05000GridDTO> loRtn = null;
        List<GSM05000GridDTO> loResult;
        GSM05000ParameterDb loDbPar;
        GSM05000Cls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb();
            
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CLANGUAGE_ID = R_BackGlobalVar.CULTURE_MENU;

            loCls = new GSM05000Cls();
            loResult = loCls.GetTransactionCodeListDb(loDbPar);
            loRtn = GetTransactionCodeStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    [HttpPost]
    public GSM05000ListDTO<GSM05000DelimiterDTO> GetDelimiterList()
    {
        R_Exception loEx = new R_Exception();
        GSM05000ListDTO<GSM05000DelimiterDTO> loRtn = null;
        List<GSM05000DelimiterDTO> loResult;
        GSM05000ParameterDb loDbPar;
        GSM05000Cls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb();
            
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CLANGUAGE_ID = R_BackGlobalVar.CULTURE_MENU;
            
            loCls = new GSM05000Cls();
            loResult = loCls.GetDelimiterListDb(loDbPar);
            loRtn = new GSM05000ListDTO<GSM05000DelimiterDTO> { Data = loResult };
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    [HttpPost]
    public GSM05000ExistDTO CheckExistData(GSM05000TrxCodeParamsDTO poParams)
    {
        R_Exception loEx = new R_Exception();
        GSM05000ExistDTO loRtn = null;
        GSM05000ExistDTO loResult;
        GSM05000ParameterDb loDbPar;
        GSM05000Cls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb();
            
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CTRANSACTION_CODE = poParams.CTRANSACTION_CODE;
            
            // loDbPar.CCOMPANY_ID = "rcd";
            // loDbPar.CTRANSACTION_CODE = "000000";
            
            loCls = new GSM05000Cls();
            loResult = loCls.GetValidateUpdateDb(loDbPar);
            loRtn = loResult;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
    
    
    #region "Helper ListStream Functions"
    private async IAsyncEnumerable<GSM05000GridDTO> GetTransactionCodeStream(List<GSM05000GridDTO> poParameter)
    {
        foreach (GSM05000GridDTO item in poParameter)
        {
            yield return item;
        }
    }
    #endregion
}