using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using FAT01100Common;
using FAT01100Common.DTOs;
using FAT01100Back;
using FAT01100Back.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FAT01100Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FAT01100ExpenseAllocationController 
    {
        private readonly LoggerFAT01100 _logger;
        private readonly ActivitySource _activitySource;

        public FAT01100ExpenseAllocationController(ILogger<FAT01100ExpenseAllocationController> logger)
        {
            LoggerFAT01100.R_InitializeLogger(logger);
            _logger = LoggerFAT01100.R_GetInstanceLogger();
            _activitySource = FAT01100Activity.R_InitializeAndGetActivitySource(nameof(FAT01100ExpenseAllocationController));
        }

        

        [HttpPost]
        public async IAsyncEnumerable<FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO> RSP_FA_GET_ASSET_EXP_ALLOC_LIST()
        {
            var lcMethod = nameof(RSP_FA_GET_ASSET_EXP_ALLOC_LIST);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO> loResult = new List<FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO>();

            try
            {
                var loCls = new FAT01100ExpenseAllocationCls();

                // Create parameter DTO internally with global variables and streaming context
                var loParam = new FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTParameterDTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CASSET_CODE = R_Utility.R_GetStreamingContext<string>(FAT01100ContextConstants.CASSET_CODE) ?? string.Empty,
                    CLANGUAGE_ID = R_BackGlobalVar.CULTURE
                };

                _logger.LogInfo("Start method GetComboDepreciationMethod in {0}", lcMethod);
                loResult = await loCls.RSP_FA_GET_ASSET_EXP_ALLOC_LIST(loParam);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            foreach (FAT01100ExpenseAllocationRSP_FA_GET_ASSET_EXP_ALLOC_LISTResultDTO loItem in loResult)
            {
                yield return loItem;
            }
        }

        [HttpPost]
        public async IAsyncEnumerable<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO> RSP_FA_GET_TRANS_EXP_ALLOC_LIST()
        {
            var lcMethod = nameof(RSP_FA_GET_TRANS_EXP_ALLOC_LIST);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO> loResult = new List<FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO>();

            try
            {
                var loCls = new FAT01100ExpenseAllocationCls();
                var loParam = new FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTParameterDTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPARENT_ID = R_Utility.R_GetStreamingContext<string>(FAT01100ContextConstants.CPARENT_ID) ?? string.Empty,
                    CDEPT_CODE= R_Utility.R_GetStreamingContext<string>(FAT01100ContextConstants.CDEPT_CODE) ?? string.Empty,
                    CTRANS_CODE= R_Utility.R_GetStreamingContext<string>(FAT01100ContextConstants.CTRANS_CODE) ?? string.Empty,
                    CREF_NO= R_Utility.R_GetStreamingContext<string>(FAT01100ContextConstants.CREF_NO) ?? string.Empty,
                    CASSET_CODE= R_Utility.R_GetStreamingContext<string>(FAT01100ContextConstants.CASSET_CODE) ?? string.Empty,
                    CASSET_TRANS_SEQ_NO= R_Utility.R_GetStreamingContext<string>(FAT01100ContextConstants.CASSET_TRANS_SEQ_NO) ?? string.Empty,
                    CLANGUAGE_ID= R_BackGlobalVar.CULTURE,
                };

                _logger.LogInfo("Start method GetComboDepreciationMethod in {0}", lcMethod);
                loResult = await loCls.RSP_FA_GET_TRANS_EXP_ALLOC_LIST(loParam);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            foreach (FAT01100ExpenseAllocationRSP_FA_GET_TRANS_EXP_ALLOC_LISTResultDTO loItem in loResult)
            {
                yield return loItem;
            }
        }
    }
}
