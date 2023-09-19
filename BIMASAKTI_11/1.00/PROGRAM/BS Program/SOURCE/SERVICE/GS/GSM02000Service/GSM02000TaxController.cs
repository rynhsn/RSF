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
public class GSM02000TaxController : ControllerBase, IGSM02000Tax
{
    [HttpPost]
    public R_ServiceGetRecordResultDTO<GSM02000TaxDTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<GSM02000TaxDTO> poParameter)
    {
        var loEx = new R_Exception();
        var loRtn = new R_ServiceGetRecordResultDTO<GSM02000TaxDTO>();

        try
        {
            var loCls = new GSM02000TaxCls();
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<GSM02000TaxDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02000TaxDTO> poParameter)
    {
        R_Exception loEx = new R_Exception();
        R_ServiceSaveResultDTO<GSM02000TaxDTO> loRtn = null;
        GSM02000TaxCls loCls;

        try
        {
            loCls = new GSM02000TaxCls();
            loRtn = new R_ServiceSaveResultDTO<GSM02000TaxDTO>();

            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

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
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02000TaxDTO> poParameter)
    {
        R_Exception loEx = new R_Exception();
        R_ServiceDeleteResultDTO loRtn = null;
        GSM02000TaxCls loCls;

        try
        {
            loCls = new GSM02000TaxCls();
            loRtn = new R_ServiceDeleteResultDTO();

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
    public IAsyncEnumerable<GSM02000TaxSalesDTO> GSM02000GetAllSalesTaxListStream()
    {
        R_Exception loEx = new R_Exception();
        IAsyncEnumerable<GSM02000TaxSalesDTO> loRtn = null;
        GSM02000TaxCls loCls;

        List<GSM02000TaxSalesDTO> loResult;
        GSM02000ParameterDb loDbPar;

        try
        {
            loDbPar = new GSM02000ParameterDb();
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

            loCls = new GSM02000TaxCls();
            loResult = loCls.SalesTaxListDb(loDbPar);

            loRtn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<GSM02000TaxDTO> GSM02000GetAllTaxListStream()
    {
        R_Exception loEx = new R_Exception();
        IAsyncEnumerable<GSM02000TaxDTO> loRtn = null;
        GSM02000TaxCls loCls;
        
        List<GSM02000TaxDTO> loResult;
        GSM02000ParameterDb loDbPar;

        try
        {
            loDbPar = new GSM02000ParameterDb();
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CTAX_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTAX_ID);

            loCls = new GSM02000TaxCls();
            loResult = loCls.TaxListDb(loDbPar);

            loRtn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
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