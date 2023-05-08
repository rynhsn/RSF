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
    [HttpPost]
    public R_ServiceGetRecordResultDTO<GSM02000DTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<GSM02000DTO> poParameter)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<GSM02000DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02000DTO> poParameter)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02000DTO> poParameter)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public GSM02000ListDTO GetAllSalesTax()
    {
        var loEx = new R_Exception();
        GSM02000ListDTO loRtn = null;

        try
        {
            var lcCompId = R_BackGlobalVar.COMPANY_ID;
            var lcUserId = R_BackGlobalVar.USER_ID;

            lcCompId = "RCD";
            lcUserId = "Admin";

            var loCls = new GSM02000Cls();
            var loResult = loCls.SalesTaxListDb(lcCompId, lcUserId);
            loRtn = new GSM02000ListDTO { Data = loResult };
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    public IAsyncEnumerable<GSM02000GridDTO> GetAllSalesTaxStream()
    {
        throw new NotImplementedException();
    }
}