using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using FAT00100Common;
using FAT00100Common.DTOs;
using FAT00100Back;
using FAT00100Back.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FAT00100Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FAT00100AssetListController : ControllerBase, IFAT00100AssetList
    {
        private readonly LoggerFAT00100 _logger;
        private readonly ActivitySource _activitySource;

        public FAT00100AssetListController(ILogger<FAT00100AssetListController> logger)
        {
            LoggerFAT00100.R_InitializeLogger(logger);
            _logger = LoggerFAT00100.R_GetInstanceLogger();
            _activitySource = FAT00100Activity.R_InitializeAndGetActivitySource(nameof(FAT00100AssetListController));
        }

        [HttpPost]
        public async IAsyncEnumerable<FAT00100GetAssetListResultDTO> GetAssetList()
        {
            var lcMethod = nameof(GetAssetList);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT00100GetAssetListResultDTO> loResult = new List<FAT00100GetAssetListResultDTO>();

            try
            {
                var loCls = new FAT00100Cls();

                // Create parameter DTO internally with global variables and streaming context
                var loParam = new FAT00100DTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLANG_ID = R_BackGlobalVar.CULTURE,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CFOREIGN_LANGUAGE = R_BackGlobalVar.CULTURE,
                    CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstants.CDEPT_CODE) ?? string.Empty,
                    CTRANSACTION_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstants.CTRANSACTION_CODE) ?? string.Empty,
                    CREFERENCE_NO = R_Utility.R_GetStreamingContext<string>(ContextConstants.CREFERENCE_NO) ?? string.Empty,
                    CSTATUS = R_Utility.R_GetStreamingContext<string>(ContextConstants.CSTATUS) ?? string.Empty,
                    DUPDATE_DATE =  DateTime.Now
                };

                _logger.LogInfo("Start method GetAssetList in {0}", lcMethod);
                loResult = await loCls.GetAssetListAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            foreach (FAT00100GetAssetListResultDTO loItem in loResult)
            {
                yield return loItem;
            }
        }
    }
}

