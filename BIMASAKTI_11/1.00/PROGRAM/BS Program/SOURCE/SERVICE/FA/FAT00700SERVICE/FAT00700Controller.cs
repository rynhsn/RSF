using FAT00700Back;
using FAT00700Common;
using FAT00700Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FAT00700Service
{
    /// <summary>
    /// Controller for FAT00700 - FA Transaction operations
    /// Provides API endpoints for transaction CRUD operations, validations, and process workflows
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FAT00700Controller : ControllerBase, IFAT00700
    {
        private readonly LoggerFAT00700 _logger;
        private readonly ActivitySource _activitySource;

        public FAT00700Controller(ILogger<FAT00700Controller> logger)
        {
            LoggerFAT00700.R_InitializeLogger(logger);
            _logger = LoggerFAT00700.R_GetInstanceLogger();
            _activitySource = FAT00700Activity.R_InitializeAndGetActivitySource(nameof(FAT00700Controller));
        }

        #region CRUD Methods

        [HttpPost]
        public async Task<R_ServiceGetRecordResultDTO<FAT00700DTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<FAT00700DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceGetRecord);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<FAT00700DTO>();

            try
            {
                var loCls = new FAT00700Cls();

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
        public async Task<R_ServiceSaveResultDTO<FAT00700DTO>> R_ServiceSave(R_ServiceSaveParameterDTO<FAT00700DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceSave);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceSaveResultDTO<FAT00700DTO>();

            try
            {
                var loCls = new FAT00700Cls();

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
        public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<FAT00700DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceDelete);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                var loCls = new FAT00700Cls();

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

        #region Non-Streaming Methods with Parameters

        [HttpPost]
        public async Task<FAT00700ResultDTO<GetPeriodResultDTO>> GetPeriod(GetPeriodParameterDTO poParameter)
        {
            var lcMethod = nameof(GetPeriod);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<GetPeriodResultDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetPeriodAsync in {0}", lcMethod);
                loRtn = await loCls.GetPeriodAsync(poParameter);
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
        public async Task<FAT00700ResultDTO<GetCurrencyResultDTO>> GetCurrency(GetCurrencyParameterDTO poParameter)
        {
            var lcMethod = nameof(GetCurrency);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<GetCurrencyResultDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetCurrencyAsync in {0}", lcMethod);
                loRtn = await loCls.GetCurrencyAsync(poParameter);
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
        public async Task<FAT00700ResultDTO<GetFATransactionDataResultDTO>> GetFATransactionData(GetFATransactionDataParameterDTO poParameter)
        {
            var lcMethod = nameof(GetFATransactionData);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<GetFATransactionDataResultDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetFATransactionDataAsync in {0}", lcMethod);
                loRtn = await loCls.GetFATransactionDataAsync(poParameter);
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
        public async Task<FAT00700ResultDTO<GetAssetInfoDataResultDTO>> GetAssetInfoData(GetAssetInfoDataParameterDTO poParameter)
        {
            var lcMethod = nameof(GetAssetInfoData);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<GetAssetInfoDataResultDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetAssetInfoDataAsync in {0}", lcMethod);
                loRtn = await loCls.GetAssetInfoDataAsync(poParameter);
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
        public async Task<FAT00700ResultDTO<GetDateStatusResultDTO>> GetDateStatus(GetDateStatusParameterDTO poParameter)
        {
            var lcMethod = nameof(GetDateStatus);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<GetDateStatusResultDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetDateStatusAsync in {0}", lcMethod);
                loRtn = await loCls.GetDateStatusAsync(poParameter);
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
        public async Task<FAT00700ResultDTO<GetAssetInformationResultDTO>> GetAssetInformation(GetAssetInformationParameterDTO poParameter)
        {
            var lcMethod = nameof(GetAssetInformation);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<GetAssetInformationResultDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetAssetInformationAsync in {0}", lcMethod);
                loRtn = await loCls.GetAssetInformationAsync(poParameter);
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
        public async Task<FAT00700ResultDTO<GetUserRightApprovalResultDTO>> GetUserRightApproval(GetUserRightApprovalParameterDTO poParameter)
        {
            var lcMethod = nameof(GetUserRightApproval);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<GetUserRightApprovalResultDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetUserRightApprovalAsync in {0}", lcMethod);
                loRtn = await loCls.GetUserRightApprovalAsync(poParameter);
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
        public async Task<FAT00700ResultDTO<GetUserActivityRightsResultDTO>> GetUserActivityRights(GetUserActivityRightsParameterDTO poParameter)
        {
            var lcMethod = nameof(GetUserActivityRights);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<GetUserActivityRightsResultDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetUserActivityRightsAsync in {0}", lcMethod);
                loRtn = await loCls.GetUserActivityRightsAsync(poParameter);
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
        public async Task<FAT00700ResultDTO<CheckOutstandingTransResultDTO>> CheckOutstandingTrans(CheckOutstandingTransParameterDTO poParameter)
        {
            var lcMethod = nameof(CheckOutstandingTrans);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<CheckOutstandingTransResultDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method CheckOutstandingTransAsync in {0}", lcMethod);
                loRtn = await loCls.CheckOutstandingTransAsync(poParameter);
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
        public async Task<FAT00700ResultDTO<ValidateVoidResultDTO>> ValidateVoid(ValidateVoidParameterDTO poParameter)
        {
            var lcMethod = nameof(ValidateVoid);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<ValidateVoidResultDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method ValidateVoidAsync in {0}", lcMethod);
                loRtn = await loCls.ValidateVoidAsync(poParameter);
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
        public async Task<FAT00700ResultDTO<GetApprovalPrecheckResultDTO>> GetApprovalPrecheck(GetApprovalPrecheckParameterDTO poParameter)
        {
            var lcMethod = nameof(GetApprovalPrecheck);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<GetApprovalPrecheckResultDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetApprovalPrecheckAsync in {0}", lcMethod);
                loRtn = await loCls.GetApprovalPrecheckAsync(poParameter);
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
        public async Task<FAT00700ResultDTO<ValidateFoundDeptResultDTO>> ValidateFoundDept(ValidateFoundDeptParameterDTO poParameter)
        {
            var lcMethod = nameof(ValidateFoundDept);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<ValidateFoundDeptResultDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method ValidateFoundDeptAsync in {0}", lcMethod);
                loRtn = await loCls.ValidateFoundDeptAsync(poParameter);
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
        public async Task<FAT00700ResultDTO<GetTransDateValidationResultDTO>> GetTransDateValidation(GetTransDateValidationParameterDTO poParameter)
        {
            var lcMethod = nameof(GetTransDateValidation);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<GetTransDateValidationResultDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetTransDateValidationAsync in {0}", lcMethod);
                loRtn = await loCls.GetTransDateValidationAsync(poParameter);
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
        public async Task<FAT00700ResultDTO<GetGridAllocDataResultDTO>> GetGridAllocData(GetGridAllocDataParameterDTO poParameter)
        {
            var lcMethod = nameof(GetGridAllocData);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<GetGridAllocDataResultDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetGridAllocDataAsync in {0}", lcMethod);
                var loBackResult = await loCls.GetGridAllocDataAsync(poParameter);

                // Back returns List<GetGridAllocDataResultDTO>, but interface expects single GetGridAllocDataResultDTO
                // Take first item from list if available
                if (loBackResult?.Data != null && loBackResult.Data.Count > 0)
                {
                    loRtn.Data = loBackResult.Data.FirstOrDefault();
                }
                else
                {
                    loRtn.Data = new GetGridAllocDataResultDTO();
                }
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

        #region Void Methods

        [HttpPost]
        public async Task ValidateGLJournal(ValidateGLJournalParameterDTO poParameter)
        {
            var lcMethod = nameof(ValidateGLJournal);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();

            try
            {
                var loCls = new FAT00700Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method ValidateGLJournalAsync in {0}", lcMethod);
                await loCls.ValidateGLJournalAsync(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }

        [HttpPost]
        public async Task<FAT00700ResultDTO<FAT00700SubmitProcessParameterDTO>> SubmitButton(FAT00700SubmitProcessParameterDTO poParameter)
        {
            var lcMethod = nameof(SubmitButton);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<FAT00700SubmitProcessParameterDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                _logger.LogInfo("Start method SubmitProcess in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.Data = await loCls.SubmitButtonAsync(poParameter);
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
        public async Task<FAT00700ResultDTO<FAT00700DTO>> DeleteTransaction(FAT00700DTO poParameter)
        {
            var lcMethod = nameof(DeleteTransaction);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<FAT00700DTO>();

            try
            {
                var loCls = new FAT00700Cls();

                _logger.LogInfo("Start method CloseProcess in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                await loCls.DeleteTransaction(poParameter);
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
        public async Task CloseButton(CloseButtonParameterDTO poParameter)
        {
            var lcMethod = nameof(CloseButton);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();

            try
            {
                var loCls = new FAT00700Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method CloseButtonAsync in {0}", lcMethod);
                await loCls.CloseButtonAsync(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }

        [HttpPost]
        public async Task ApproveButton(ApproveButtonParameterDTO poParameter)
        {
            var lcMethod = nameof(ApproveButton);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();

            try
            {
                var loCls = new FAT00700Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method ApproveButtonAsync in {0}", lcMethod);
                await loCls.ApproveButtonAsync(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }

        [HttpPost]
        public async Task VoidButton(VoidButtonParameterDTO poParameter)
        {
            var lcMethod = nameof(VoidButton);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();

            try
            {
                var loCls = new FAT00700Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method VoidButtonAsync in {0}", lcMethod);
                await loCls.VoidButtonAsync(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
        }

        [HttpPost]
        public async IAsyncEnumerable<GetTransactionListResultDTO> GetTransactionList()
        {
            var lcMethod = nameof(GetTransactionList);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<GetTransactionListResultDTO> loTempRtn = new List<GetTransactionListResultDTO>();
            FAT00700Cls? loCls = null;
            var poParameter = new GetTransactionListParameterDTO();

            try
            {
                loCls = new FAT00700Cls();

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                poParameter.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantDTO.CTRANS_CODE);
                poParameter.CPERIOD_TO = R_Utility.R_GetStreamingContext<string>(ContextConstantDTO.CTO_PERIOD);
                poParameter.CPERIOD_FROM = R_Utility.R_GetStreamingContext<string>(ContextConstantDTO.CFROM_PERIOD);
                poParameter.CASSET_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantDTO.CASSET_CODE);
                poParameter.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantDTO.CDEPT_CODE);

                _logger.LogInfo("Start method GetTransactionListAsync in {0}", lcMethod);

                loCls = new();
                loTempRtn = await loCls.GetTransactionListAsync(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }
            finally
            {
                if (loCls != null)
                {
                    loCls = null;
                }
            }

            loEx.ThrowExceptionIfErrors();
            foreach (GetTransactionListResultDTO item in loTempRtn)
            {
                yield return item;
            }
        }
        [HttpPost]
        public async Task<FAT00700ResultDTO<FAT00700CompanyInfoResultDTO>> GetCompanyInfo(FAT00700CompanyInfoParameterDTO poParameter)
        {
            var lcMethod = nameof(GetCompanyInfo);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<FAT00700CompanyInfoResultDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                _logger.LogInfo("Start method CloseProcess in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.Data = await loCls.GetCompanyInfoAsync(poParameter);
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
        public async Task<FAT00700ResultDTO<FAT00700SystemParamResultDTO>> GetSystemParam(FAT00700SystemParamParameterDTO poParameter)
        {
            var lcMethod = nameof(GetSystemParam);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<FAT00700SystemParamResultDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                _logger.LogInfo("Start method CloseProcess in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                //poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.Data = await loCls.GetSystemParamAsync(poParameter);
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
        public async Task<FAT00700ResultDTO<FAT00700PeriodInfoResultDTO>> GetPeriodInfo(FAT00700PeriodInfoParamDTO poParameter)
        {
            var lcMethod = nameof(GetPeriodInfo);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<FAT00700PeriodInfoResultDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                _logger.LogInfo("Start method CloseProcess in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                loRtn.Data = await loCls.GetPeriodInfoAsync(poParameter);
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
        public async Task<FAT00700ResultDTO<FAT00700TransCodeInfoResultDTO>> GetTransCodeInfo(FAT00700TransCodeInfoParamDTO poParameter)
        {
            var lcMethod = nameof(GetTransCodeInfo);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<FAT00700TransCodeInfoResultDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                _logger.LogInfo("Start method CloseProcess in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                loRtn.Data = await loCls.GetTransCodeInfoAsync(poParameter);
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
        public async Task<FAT00700ResultDTO<FAT00700PeriodRangeResultDTO>> GetPeriodRange(FAT00700PeriodRangeParamDTO poParameter)
        {
            var lcMethod = nameof(GetPeriodRange);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<FAT00700PeriodRangeResultDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                _logger.LogInfo("Start method CloseProcess in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                loRtn.Data = await loCls.GetPeriodRangeAsync(poParameter);
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
        public async IAsyncEnumerable<FAT00700GetDeptListResultDTO> GetAllDeptList()
        {
            var lcMethod = nameof(GetAllDeptList);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT00700GetDeptListResultDTO> loResult = new List<FAT00700GetDeptListResultDTO>();

            try
            {
                var loCls = new FAT00700Cls();

                _logger.LogInfo("Start method FAT00400GetDeptListResultDTO in {0}", lcMethod);

                var loParam = new FAT00700GetDeptListParameterDTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                };

                loResult = await loCls.GetDeptList(loParam);
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

        #endregion
    }
}

