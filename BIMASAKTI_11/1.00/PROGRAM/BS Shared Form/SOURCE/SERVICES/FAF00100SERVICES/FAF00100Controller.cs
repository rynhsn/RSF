using FAF00100BACK;
using FAF00100BACK.DTOs;
using FAF00100COMMON;
using FAF00100COMMON.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace FAF00100SERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FAF00100Controller : Controller, IFAF00100
    {
        private readonly ActivitySource _activitySource;
        private readonly LoggerFAF00100 _logger;

        public FAF00100Controller(ILogger<FAF00100Controller> logger)
        {
            LoggerFAF00100.R_InitializeLogger(logger);
            _logger = LoggerFAF00100.R_GetInstanceLogger();
            _activitySource = FAF00100Activity.R_InitializeAndGetActivitySource(nameof(FAF00100Controller));
        }
        [HttpPost]
        public Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<FAF00100GetAssetResultDTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<R_ServiceGetRecordResultDTO<FAF00100GetAssetResultDTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<FAF00100GetAssetResultDTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceGetRecord);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<FAF00100GetAssetResultDTO>();

            try
            {
                var loCls = new FAF00100Cls();
                _logger.LogInfo("Start method R_GetRecordAsync in {0}", lcMethod);
                poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loRtn.data = await loCls.R_GetRecordAsync(poParameter.Entity);
            }
            catch (Exception loExCaught)
            {
                loEx.Add(loExCaught);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        [HttpPost]
        public Task<R_ServiceSaveResultDTO<FAF00100GetAssetResultDTO>> R_ServiceSave(R_ServiceSaveParameterDTO<FAF00100GetAssetResultDTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public async IAsyncEnumerable<FAF00100GetAssetAllocResultDTO> GetListAssetAlloc()
        {
            var lcMethod = nameof(GetListAssetAlloc);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAF00100GetAssetAllocResultDTO> loResult = new List<FAF00100GetAssetAllocResultDTO>();

            try
            {
                var loCls = new FAF00100Cls();

                _logger.LogInfo("Start method GetAllocationExpenseList in {0}", lcMethod);

                var loParam = new FAF00100GetAssetAllocParameterDTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLANGUAGE_ID = R_BackGlobalVar.CULTURE,
                    CASSET_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.ASSET_CODE) ?? string.Empty
                };

                loResult = await loCls.GetListAssetAlloc(loParam);
            }
            catch (Exception loExCaught)
            {
                loEx.Add(loExCaught);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            foreach (var loItem in loResult)
            {
                yield return loItem;
            }
        }
    }
}
