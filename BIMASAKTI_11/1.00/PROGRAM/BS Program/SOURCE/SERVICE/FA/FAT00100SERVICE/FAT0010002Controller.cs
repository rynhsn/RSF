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
    public class FAT0010002Controller : ControllerBase, IFAT0010002
    {
        private readonly LoggerFAT00100 _logger;
        private readonly ActivitySource _activitySource;

        public FAT0010002Controller(ILogger<FAT0010002Controller> logger)
        {
            LoggerFAT00100.R_InitializeLogger(logger);
            _logger = LoggerFAT00100.R_GetInstanceLogger();
            _activitySource = FAT00100Activity.R_InitializeAndGetActivitySource(nameof(FAT0010002Controller));
        }

        [HttpPost]
        public async Task<R_ServiceGetRecordResultDTO<FAT0010002DTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<FAT0010002DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceGetRecord);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<FAT0010002DTO>();

            try
            {
                var loCls = new FAT0010002Cls();

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
        public async Task<R_ServiceSaveResultDTO<FAT0010002DTO>> R_ServiceSave(R_ServiceSaveParameterDTO<FAT0010002DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceSave);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceSaveResultDTO<FAT0010002DTO>();

            try
            {
                var loCls = new FAT0010002Cls();

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
        public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<FAT0010002DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceDelete);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                var loCls = new FAT0010002Cls();

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
        public async Task<FAT0010002ResultDTO<FAT0010002GetFAAcquisitionDetailHeaderResultDTO>> GetFAAcquisitionDetailHeader(FAT0010002GetFAAcquisitionDetailHeaderParameterDTO poParameter)
        {
            var lcMethod = nameof(GetFAAcquisitionDetailHeader);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT0010002ResultDTO<FAT0010002GetFAAcquisitionDetailHeaderResultDTO>();

            try
            {
                var loCls = new FAT0010002Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetFAAcquisitionDetailHeader in {0}", lcMethod);
                loRtn = await loCls.GetFAAcquisitionDetailHeaderAsync(poParameter);
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
        public async Task<FAT0010002ResultDTO<FAT0010002ValidateDeptCodeResultDTO>> ValidateDeptCode(FAT0010002ValidateDeptCodeParameterDTO poParameter)
        {
            var lcMethod = nameof(ValidateDeptCode);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT0010002ResultDTO<FAT0010002ValidateDeptCodeResultDTO>();

            try
            {
                var loCls = new FAT0010002Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method ValidateDeptCode in {0}", lcMethod);
                loRtn = await loCls.ValidateDeptCodeAsync(poParameter);
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
        public async Task<FAT0010002ResultDTO<FAT0010002GetDecliningDeprAmtResultDTO>> GetDecliningDeprAmt(FAT0010002GetDecliningDeprAmtParameterDTO poParameter)
        {
            var lcMethod = nameof(GetDecliningDeprAmt);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT0010002ResultDTO<FAT0010002GetDecliningDeprAmtResultDTO>();

            try
            {
                var loCls = new FAT0010002Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetDecliningDeprAmt in {0}", lcMethod);
                loRtn = await loCls.GetDecliningDeprAmtAsync(poParameter);
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
        public async IAsyncEnumerable<FAT0010002GetComboDepreciationMethodResultDTO> GetComboDepreciationMethod()
        {
            var lcMethod = nameof(GetComboDepreciationMethod);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT0010002GetComboDepreciationMethodResultDTO> loResult = new List<FAT0010002GetComboDepreciationMethodResultDTO>();

            try
            {
                var loCls = new FAT0010002Cls();

                // Create parameter DTO internally with global variables and streaming context
                var loParam = new FAT0010002GetFAAcquisitionDetailHeaderParameterDTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLANG_ID = R_BackGlobalVar.CULTURE,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CFOREIGN_LANGUAGE = R_Utility.R_GetStreamingContext<string>(ContextConstants.CFOREIGN_LANGUAGE) ?? string.Empty
                };

                _logger.LogInfo("Start method GetComboDepreciationMethod in {0}", lcMethod);
                loResult = await loCls.GetComboDepreciationMethodAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            foreach (FAT0010002GetComboDepreciationMethodResultDTO loItem in loResult)
            {
                yield return loItem;
            }
        }

        [HttpPost]
        public async IAsyncEnumerable<FAT0010002GetFAAcquisitionDetailAssetListResultDTO> GetFAAcquisitionDetailAssetList()
        {
            var lcMethod = nameof(GetFAAcquisitionDetailAssetList);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT0010002GetFAAcquisitionDetailAssetListResultDTO> loResult = new List<FAT0010002GetFAAcquisitionDetailAssetListResultDTO>();

            try
            {
                var loCls = new FAT0010002Cls();

                // Create parameter DTO internally with global variables and streaming context
                var loParam = new FAT0010002GetFAAcquisitionDetailHeaderParameterDTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLANG_ID = R_BackGlobalVar.CULTURE,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CFOREIGN_LANGUAGE = R_Utility.R_GetStreamingContext<string>(ContextConstants.CFOREIGN_LANGUAGE) ?? string.Empty,
                    CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstants.CDEPT_CODE) ?? string.Empty,
                    CTRANSACTION_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstants.CTRANSACTION_CODE) ?? string.Empty,
                    CREFERENCE_NO = R_Utility.R_GetStreamingContext<string>(ContextConstants.CREFERENCE_NO) ?? string.Empty,
                    CSTATUS = R_Utility.R_GetStreamingContext<string>(ContextConstants.CSTATUS) ?? string.Empty,
                    DUPDATE_DATE = R_Utility.R_GetStreamingContext<DateTime?>(ContextConstants.DUPDATE_DATE)
                };

                _logger.LogInfo("Start method GetFAAcquisitionDetailAssetList in {0}", lcMethod);
                loResult = await loCls.GetFAAcquisitionDetailAssetListAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            foreach (FAT0010002GetFAAcquisitionDetailAssetListResultDTO loItem in loResult)
            {
                yield return loItem;
            }
        }

        [HttpPost]
        public async IAsyncEnumerable<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO> GetFAAcquisitionDetailAllocExpenPageList()
        {
            var lcMethod = nameof(GetFAAcquisitionDetailAllocExpenPageList);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO> loResult = new List<FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO>();

            try
            {
                var loCls = new FAT0010002Cls();

                // Create parameter DTO internally with global variables and streaming context
                var loParam = new FAT0010002GetFAAcquisitionDetailHeaderParameterDTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLANG_ID = R_BackGlobalVar.CULTURE,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CFOREIGN_LANGUAGE = R_Utility.R_GetStreamingContext<string>(ContextConstants.CFOREIGN_LANGUAGE) ?? string.Empty,
                    CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstants.CDEPT_CODE) ?? string.Empty,
                    CTRANSACTION_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstants.CTRANSACTION_CODE) ?? string.Empty,
                    CREFERENCE_NO = R_Utility.R_GetStreamingContext<string>(ContextConstants.CREFERENCE_NO) ?? string.Empty,
                    CASSET_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstants.CASSET_CODE) ?? string.Empty,
                    CASSET_TRANS_SEQNO = R_Utility.R_GetStreamingContext<string>(ContextConstants.CASSET_TRANS_SEQNO) ?? string.Empty
                };

                _logger.LogInfo("Start method GetFAAcquisitionDetailAllocExpenPageList in {0}", lcMethod);
                loResult = await loCls.GetFAAcquisitionDetailAllocExpenPageListAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            foreach (FAT0010002GetFAAcquisitionDetailAllocExpenPageListResultDTO loItem in loResult)
            {
                yield return loItem;
            }
        }
    }
}

