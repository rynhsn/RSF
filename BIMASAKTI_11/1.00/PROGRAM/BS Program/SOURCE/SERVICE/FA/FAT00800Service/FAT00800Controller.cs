using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using FAT00800Common;
using FAT00800Common.DTOs;
using FAT00800Back;
using FAT00800Back.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FAT00800Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FAT00800Controller : ControllerBase, IFAT00800
    {
        private readonly LoggerFAT00800 _logger;
        private readonly ActivitySource _activitySource;

        public FAT00800Controller(ILogger<FAT00800Controller> logger)
        {
            LoggerFAT00800.R_InitializeLogger(logger);
            _logger = LoggerFAT00800.R_GetInstanceLogger();
            _activitySource = FAT00800Activity.R_InitializeAndGetActivitySource(nameof(FAT00800Controller));
        }

        #region Streaming Methods

        [HttpPost]
        public async IAsyncEnumerable<FAT00800GetTransListResultDTO> FAT00800GetTransList()
        {
            var lcMethod = nameof(FAT00800GetTransList);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT00800GetTransListResultDTO> loResult = new();

            try
            {
                var loCls = new FAT00800Cls();

                var loParam = new FAT00800GetTransListParameterDTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstants.CDEPT_CODE) ?? string.Empty,
                    CFROM_PERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstants.CFROM_PERIOD) ?? string.Empty,
                    CTO_PERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstants.CTO_PERIOD) ?? string.Empty,
                    CASSET_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstants.CASSET_CODE) ?? string.Empty,
                    CLANGUAGE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstants.CLANGUAGE_ID) ?? string.Empty
                };

                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loResult = await loCls.FAT00800GetTransListAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            foreach (FAT00800GetTransListResultDTO loItem in loResult)
            {
                yield return loItem;
            }
        }

        #endregion

        #region FAT00800EntryCls Delegation (GetSystemParam, GetYearRange)

        [HttpPost]
        public async Task<FAT00800ResultDTO<FAT00800GetGetSystemParamResultDTO>> FAT00800GetGetSystemParam(FAT00800GetGetSystemParamParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT00800GetGetSystemParam);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetGetSystemParamResultDTO>();

            try
            {
                var loCls = new FAT00800EntryCls();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method FAT00800GetGetSystemParam in {0}", lcMethod);
                loRtn = await loCls.FAT00800GetGetSystemParamAsync(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpPost]
        public async Task<FAT00800ResultDTO<FAT00800GetYearRangeResultDTO>> FAT00800GetYearRange(FAT00800GetYearRangeParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT00800GetYearRange);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetYearRangeResultDTO>();

            try
            {
                var loCls = new FAT00800EntryCls();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method FAT00800GetYearRange in {0}", lcMethod);
                loRtn = await loCls.FAT00800GetYearRangeAsync(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        #endregion
    }
}
