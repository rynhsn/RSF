using LMM02500Common;
using LMM02500Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_CommonFrontBackAPI;

namespace LMM02500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class LMM02500ProfileController : ILMM02500Profile
{
    private LoggerLMM02500 _logger;

    public LMM02500ProfileController(ILogger<LMM02500ProfileController> logger)
    {
        LoggerLMM02500.R_InitializeLogger(logger);
        _logger = LoggerLMM02500.R_GetInstanceLogger();
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<LMM02500TenantGroupDetailDTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<LMM02500TenantGroupDetailDTO> poParameter)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<LMM02500TenantGroupDetailDTO> R_ServiceSave(
        R_ServiceSaveParameterDTO<LMM02500TenantGroupDetailDTO> poParameter)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(
        R_ServiceDeleteParameterDTO<LMM02500TenantGroupDetailDTO> poParameter)
    {
        throw new NotImplementedException();
    }
}