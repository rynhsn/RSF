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
    public class FAT00800ListController : ControllerBase, IFAT00800List
    {
        private readonly LoggerFAT00800 _logger;
        private readonly ActivitySource _activitySource;

        public FAT00800ListController(ILogger<FAT00800ListController> logger)
        {
            LoggerFAT00800.R_InitializeLogger(logger);
            _logger = LoggerFAT00800.R_GetInstanceLogger();
            _activitySource = FAT00800Activity.R_InitializeAndGetActivitySource(nameof(FAT00800ListController));
        }

        #region Streaming Methods

        [HttpPost]
        public async IAsyncEnumerable<FAT00800TransListResultDTO> FAT00800TransList()
        {
            var lcMethod = nameof(FAT00800TransList);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT00800TransListResultDTO> loResult = new List<FAT00800TransListResultDTO>();

            try
            {
                var loCls = new FAT00800Cls();

                // Create parameter DTO internally with global variables and streaming context
                var loParam = new FAT00800TransListParameterDTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstants.CTRANS_CODE) ?? string.Empty,
                    CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstants.CDEPT_CODE) ?? string.Empty,
                    CFROM_PERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstants.CFROM_PERIOD) ?? string.Empty,
                    CTO_PERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstants.CTO_PERIOD) ?? string.Empty,
                    CASSET_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstants.CASSET_CODE) ?? string.Empty,
                    CLANGUAGE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstants.CLANGUAGE_ID) ?? string.Empty
                };

                _logger.LogInfo("Start method FAT00800TransList in {0}", lcMethod);
                loResult = await loCls.FAT00800TransListAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            foreach (FAT00800TransListResultDTO loItem in loResult)
            {
                yield return loItem;
            }
        }

        #endregion
    }
}
