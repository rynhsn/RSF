using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using FAT01100Common;
using FAT01100Common.DTOs;
using FAT01100Back;
using FAT01100Back.DTOs;

namespace FAT01100Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FAT01100EntryController : ControllerBase, IFAT01100Entry
    {
        private readonly LoggerFAT01100 _logger;
        private readonly ActivitySource _activitySource;

        public FAT01100EntryController(ILogger<FAT01100EntryController> logger)
        {
            LoggerFAT01100.R_InitializeLogger(logger);
            _logger = LoggerFAT01100.R_GetInstanceLogger();
            _activitySource = FAT01100Activity.R_InitializeAndGetActivitySource(nameof(FAT01100EntryController));
        }

        #region CRUD Methods

        [HttpPost]
        public async Task<R_ServiceGetRecordResultDTO<FAT01100DTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<FAT01100DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceGetRecord);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<FAT01100DTO>();

            try
            {
                var loCls = new FAT01100EntryCls();
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
        public async Task<R_ServiceSaveResultDTO<FAT01100DTO>> R_ServiceSave(R_ServiceSaveParameterDTO<FAT01100DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceSave);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceSaveResultDTO<FAT01100DTO>();

            try
            {
                var loCls = new FAT01100EntryCls();
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
        public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<FAT01100DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceDelete);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                var loCls = new FAT01100EntryCls();
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

        #region Init / Lookup Methods

        [HttpPost]
        public async Task<FAT01100ResultDTO<FAT01100GetCompanyInfoResultDTO>> FAT01100GetCompanyInfo(FAT01100GetCompanyInfoParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT01100GetCompanyInfo);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT01100ResultDTO<FAT01100GetCompanyInfoResultDTO>();

            try
            {
                var loCls = new FAT01100EntryCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loRtn = await loCls.FAT01100GetCompanyInfo(poParameter);
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
        public async Task<FAT01100ResultDTO<FAT01100GetGetSystemParamResultDTO>> FAT01100GetGetSystemParam(FAT01100GetGetSystemParamParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT01100GetGetSystemParam);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT01100ResultDTO<FAT01100GetGetSystemParamResultDTO>();

            try
            {
                var loCls = new FAT01100EntryCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loRtn = await loCls.FAT01100GetGetSystemParam(poParameter);
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
        public async Task<FAT01100ResultDTO<FAT01100GetPeriodeDtInfoResultDTO>> FAT01100GetPeriodeDtInfo(FAT01100GetPeriodeDtInfoParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT01100GetPeriodeDtInfo);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT01100ResultDTO<FAT01100GetPeriodeDtInfoResultDTO>();

            try
            {
                var loCls = new FAT01100EntryCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loRtn = await loCls.FAT01100GetPeriodeDtInfo(poParameter);
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
        public async Task<FAT01100ResultDTO<List<FAT01100GetCurrencyListResultDTO>>> GetCurrencyList(FAT01100GetCurrencyListParameterDTO poParameter)
        {
            var lcMethod = nameof(GetCurrencyList);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT01100ResultDTO<List<FAT01100GetCurrencyListResultDTO>>();

            try
            {
                var loCls = new FAT01100EntryCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loRtn.Data = await loCls.GetCurrencyList(poParameter);
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
        public async Task<FAT01100ResultDTO<List<FAT01100GetDeptLookupListResultDTO>>> FAT01100GetDeptLookupList(FAT01100GetDeptLookupListParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT01100GetDeptLookupList);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT01100ResultDTO<List<FAT01100GetDeptLookupListResultDTO>>();

            try
            {
                var loCls = new FAT01100EntryCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loRtn.Data = await loCls.FAT01100GetDeptLookupList(poParameter);
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
        public async Task<FAT01100ResultDTO<FAT01100GetTransCodeInfoResultDTO>> FAT01100GetTransCodeInfo(FAT01100GetTransCodeInfoParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT01100GetTransCodeInfo);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT01100ResultDTO<FAT01100GetTransCodeInfoResultDTO>();

            try
            {
                var loCls = new FAT01100EntryCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loRtn = await loCls.FAT01100GetTransCodeInfo(poParameter);
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
        public async Task<FAT01100ResultDTO<FAT01100GetYearRangeResultDTO>> FAT01100GetYearRange(FAT01100GetYearRangeParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT01100GetYearRange);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT01100ResultDTO<FAT01100GetYearRangeResultDTO>();

            try
            {
                var loCls = new FAT01100EntryCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loRtn = await loCls.FAT01100GetYearRange(poParameter);
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
        public async Task<FAT01100ResultDTO<FAT01100GetLastCurrencyRateResultDTO>> FAT01100GetLastCurrencyRate(FAT01100GetLastCurrencyRateParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT01100GetLastCurrencyRate);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT01100ResultDTO<FAT01100GetLastCurrencyRateResultDTO>();

            try
            {
                var loCls = new FAT01100EntryCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loRtn = await loCls.FAT01100GetLastCurrencyRate(poParameter);
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
        public async Task<FAT01100ResultDTO<object>> FAT01100UpdateTransHdStatus(FAT01100UpdateTransHdStatusParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT01100UpdateTransHdStatus);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT01100ResultDTO<object>();

            try
            {
                var loCls = new FAT01100EntryCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loRtn = await loCls.FAT01100UpdateTransHdStatus(poParameter);
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
        public async Task<FAT01100ResultDTO<object>> FAT01100SubmitTrans(FAT01100SubmitTransParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT01100SubmitTrans);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT01100ResultDTO<object>();

            try
            {
                var loCls = new FAT01100EntryCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loRtn = await loCls.FAT01100SubmitTrans(poParameter);
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
        public async Task<FAT01100ResultDTO<FAT01100GetAssetResultDTO>> FAT01100GetAsset(FAT01100GetAssetParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT01100GetAsset);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT01100ResultDTO<FAT01100GetAssetResultDTO>();

            try
            {
                var loCls = new FAT01100EntryCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loRtn = await loCls.FAT01100GetAsset(poParameter);
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
        public async IAsyncEnumerable<FAT01100GetGsbCodeListResultDTO> FAT01100GetGsbCodeList()
        {
            var lcMethod = nameof(FAT01100GetGsbCodeList);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT01100GetGsbCodeListResultDTO> loResult = new();

            try
            {
                var loCls = new FAT01100EntryCls();
                var loParam = new FAT01100GetGsbCodeListParameterDTO
                {
                    CAPPLICATION = "BIMASAKTI",
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CCLASS_ID = "_FA_DEPR_METHOD",
                    CLANGUAGE_ID = R_BackGlobalVar.CULTURE,
                    CREC_ID_LIST = ""
                };

                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loResult = await loCls.FAT01100GetGsbCodeList(loParam);
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

        [HttpPost]
        public async Task<FAT01100ResultDTO<FAT01100GetOutstandingTransResultDTO>> FAT01100GetOutstandingTrans(FAT01100GetOutstandingTransParameterDTO poParameter)
        {
            var lcMethod = nameof(FAT01100GetOutstandingTrans);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT01100ResultDTO<FAT01100GetOutstandingTransResultDTO>();

            try
            {
                var loCls = new FAT01100EntryCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                _logger.LogInfo("Start method {MethodName}", lcMethod);
                loRtn = await loCls.FAT01100GetOutstandingTrans(poParameter);
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
