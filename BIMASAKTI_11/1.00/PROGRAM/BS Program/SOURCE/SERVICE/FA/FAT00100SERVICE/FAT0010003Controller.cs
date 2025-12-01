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
    public class FAT0010003Controller : ControllerBase, IFAT0010003
    {
        private readonly LoggerFAT00100 _logger;
        private readonly ActivitySource _activitySource;

        public FAT0010003Controller(ILogger<FAT0010003Controller> logger)
        {
            LoggerFAT00100.R_InitializeLogger(logger);
            _logger = LoggerFAT00100.R_GetInstanceLogger();
            _activitySource = FAT00100Activity.R_InitializeAndGetActivitySource(nameof(FAT0010003Controller));
        }

        [HttpPost]
        public async Task<R_ServiceGetRecordResultDTO<FAT0010003DTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<FAT0010003DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceGetRecord);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<FAT0010003DTO>();

            try
            {
                var loCls = new FAT0010003Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CLANG_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Start method R_ServiceGetRecord in {0}", lcMethod);
                loRtn.data = await loCls.R_GetRecordAsync(poParameter.Entity);
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
        public async Task<R_ServiceSaveResultDTO<FAT0010003DTO>> R_ServiceSave(R_ServiceSaveParameterDTO<FAT0010003DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceSave);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceSaveResultDTO<FAT0010003DTO>();

            try
            {
                var loCls = new FAT0010003Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CLANG_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Start method R_ServiceSave in {0}", lcMethod);
                loRtn.data = await loCls.R_SaveAsync(poParameter.Entity, poParameter.CRUDMode);
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
        public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<FAT0010003DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceDelete);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                var loCls = new FAT0010003Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CLANG_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Start method R_ServiceDelete in {0}", lcMethod);
                await loCls.R_DeleteAsync(poParameter.Entity);
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
        public async Task<FAT0010003ResultDTO<FAT0010003GetDataHeaderResultDTO>> GetDataHeader(FAT0010003GetDataHeaderParameterDTO poParameter)
        {
            var lcMethod = nameof(GetDataHeader);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT0010003ResultDTO<FAT0010003GetDataHeaderResultDTO>();

            try
            {
                var loCls = new FAT0010003Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetDataHeader in {0}", lcMethod);
                loRtn = await loCls.GetDataHeaderAsync(poParameter);
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
        public async IAsyncEnumerable<FAT0010003GetDataGridResultDTO> GetDataGrid()
        {
            var lcMethod = nameof(GetDataGrid);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT0010003GetDataGridResultDTO> loResult = new List<FAT0010003GetDataGridResultDTO>();

            try
            {
                var loCls = new FAT0010003Cls();

                // Create parameter DTO internally with global variables and streaming context
                var loParam = new FAT0010003GetDataGridParameterDTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLANG_ID = R_BackGlobalVar.CULTURE,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    PCFR_DEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstants.PCFR_DEPT_CODE) ?? string.Empty,
                    PCFR_TRANSACTION_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstants.PCFR_TRANSACTION_CODE) ?? string.Empty,
                    PCFR_REFERENCE_NO = R_Utility.R_GetStreamingContext<string>(ContextConstants.PCFR_REFERENCE_NO) ?? string.Empty
                };

                _logger.LogInfo("Start method GetDataGrid in {0}", lcMethod);
                loResult = await loCls.GetDataGridAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            foreach (FAT0010003GetDataGridResultDTO loItem in loResult)
            {
                yield return loItem;
            }
        }
    }
}

