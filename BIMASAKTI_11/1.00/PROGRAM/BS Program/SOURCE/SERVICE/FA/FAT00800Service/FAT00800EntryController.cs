using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
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
    public class FAT00800EntryController : ControllerBase, IFAT00800Entry
    {
        private readonly LoggerFAT00800 _logger;
        private readonly ActivitySource _activitySource;

        public FAT00800EntryController(ILogger<FAT00800EntryController> logger)
        {
            LoggerFAT00800.R_InitializeLogger(logger);
            _logger = LoggerFAT00800.R_GetInstanceLogger();
            _activitySource = FAT00800Activity.R_InitializeAndGetActivitySource(nameof(FAT00800EntryController));
        }

        #region CRUD Methods

        [HttpPost]
        public async Task<R_ServiceGetRecordResultDTO<FAT00800DTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<FAT00800DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceGetRecord);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<FAT00800DTO>();

            try
            {
                var loCls = new FAT00800EntryCls();

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
        public async Task<R_ServiceSaveResultDTO<FAT00800DTO>> R_ServiceSave(R_ServiceSaveParameterDTO<FAT00800DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceSave);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceSaveResultDTO<FAT00800DTO>();

            try
            {
                var loCls = new FAT00800EntryCls();

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
        public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<FAT00800DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceDelete);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                var loCls = new FAT00800EntryCls();

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

        #endregion

        #region Init Process

        [HttpPost]
        public async Task<FAT00800ResultDTO<FAT00800GetCompanyInfoResultDTO>> FAT00800GetCompanyInfo(FAT00800GetCompanyInfoParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT00800GetCompanyInfo);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetCompanyInfoResultDTO>();

            try
            {
                var loCls = new FAT00800EntryCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loRtn = await loCls.FAT00800GetCompanyInfoAsync(poParameter);
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
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Start method {MethodName}", lcMethod);
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
        public async Task<FAT00800ResultDTO<FAT00800GetPeriodeDtInfoResultDTO>> FAT00800GetPeriodeDtInfo(FAT00800GetPeriodeDtInfoParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT00800GetPeriodeDtInfo);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetPeriodeDtInfoResultDTO>();

            try
            {
                var loCls = new FAT00800EntryCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loRtn = await loCls.FAT00800GetPeriodeDtInfoAsync(poParameter);
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
        public async Task<FAT00800ResultDTO<FAT00800GetTransCodeInfoResultDTO>> FAT00800GetTransCodeInfo(FAT00800GetTransCodeInfoParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT00800GetTransCodeInfo);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetTransCodeInfoResultDTO>();

            try
            {
                var loCls = new FAT00800EntryCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loRtn = await loCls.FAT00800GetTransCodeInfoAsync(poParameter);
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
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Start method {MethodName}", lcMethod);
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

        #region Streaming (Init)

        /// <summary>
        /// Get currency list (streaming). Uses RSP_GS_GET_CURRENCY_LIST. Parameters from R_BackGlobalVar.
        /// </summary>
        [HttpPost]
        public async IAsyncEnumerable<FAT00800GetCurrencyListResultDTO> GetCurrencyList()
        {
            var lcMethod = nameof(GetCurrencyList);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT00800GetCurrencyListResultDTO> loResult = new();

            try
            {
                var loCls = new FAT00800EntryCls();
                var loParam = new FAT00800GetCurrencyListParameterDTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CLANG_ID = R_BackGlobalVar.CULTURE ?? string.Empty
                };

                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loResult = await loCls.GetCurrencyListAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            foreach (var loItem in loResult)
            {
                yield return loItem;
            }
        }

        /// <summary>
        /// Get department lookup list (streaming). Parameters from R_BackGlobalVar and streaming context.
        /// </summary>
        [HttpPost]
        public async IAsyncEnumerable<FAT00800GetDeptLookupListResultDTO> FAT00800GetDeptLookupList()
        {
            var lcMethod = nameof(FAT00800GetDeptLookupList);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT00800GetDeptLookupListResultDTO> loResult = new();

            try
            {
                var loCls = new FAT00800EntryCls();
                var loParam = new FAT00800GetDeptLookupListParameterDTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CPROGRAM_ID = R_Utility.R_GetStreamingContext<string>(ContextConstants.CPROGRAM_ID) ?? string.Empty
                };

                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loResult = await loCls.FAT00800GetDeptLookupListAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            foreach (var loItem in loResult)
            {
                yield return loItem;
            }
        }

        #endregion
    }
}

