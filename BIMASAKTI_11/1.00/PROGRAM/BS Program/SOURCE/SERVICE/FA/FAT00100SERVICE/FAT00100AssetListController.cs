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
        public async IAsyncEnumerable<FAT00100GetTransAssetListResultDTO> FAT00100GetTransAssetList()
        {
            var lcMethod = nameof(FAT00100GetTransAssetList);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT00100GetTransAssetListResultDTO> loResult = new List<FAT00100GetTransAssetListResultDTO>();

            try
            {
                var loCls = new FAT00100AssetListCls();

                // Create parameter DTO internally with global variables and streaming context
                var loParam = new FAT00100GetTransAssetListParameterDTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CREC_ID = R_Utility.R_GetStreamingContext<string>(ContextConstants.CREC_ID) ?? string.Empty,
                    CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstants.CDEPT_CODE) ?? string.Empty,
                    CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstants.CREF_NO) ?? string.Empty,
                    CLANGUAGE_ID = R_BackGlobalVar.CULTURE
                };

                _logger.LogInfo("Start method FAT00100GetTransAssetList in {0}", lcMethod);
                loResult = await loCls.FAT00100GetTransAssetList(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            foreach (FAT00100GetTransAssetListResultDTO loItem in loResult)
            {
                yield return loItem;
            }
        }

        [HttpPost]
        public async Task<FAT00100ResultDTO<FAT00100GetTransAssetResultDTO>> FAT00100GetTransAsset(FAT00100GetTransAssetParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT00100GetTransAsset);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00100ResultDTO<FAT00100GetTransAssetResultDTO>();

            try
            {
                var loCls = new FAT00100AssetListCls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Start method FAT00100GetTransAsset in {0}", lcMethod);
                loRtn = await loCls.FAT00100GetTransAssetAsync(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}

