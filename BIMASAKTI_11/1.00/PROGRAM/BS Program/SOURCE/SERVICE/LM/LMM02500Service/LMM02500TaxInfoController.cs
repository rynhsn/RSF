using LMM02500Common;
using LMM02500Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_CommonFrontBackAPI;

namespace LMM02500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class LMM02500TaxInfoController : ILMM02500TaxInfo
{
    private LoggerLMM02500 _logger;

    public LMM02500TaxInfoController(ILogger<LMM02500TaxInfoController> logger)
    {
        LoggerLMM02500.R_InitializeLogger(logger);
        _logger = LoggerLMM02500.R_GetInstanceLogger();
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<LMM02500TaxInfoDTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<LMM02500TaxInfoDTO> poParameter)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<LMM02500TaxInfoDTO> R_ServiceSave(
        R_ServiceSaveParameterDTO<LMM02500TaxInfoDTO> poParameter)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM02500TaxInfoDTO> poParameter)
    {
        throw new NotImplementedException();
    }
}