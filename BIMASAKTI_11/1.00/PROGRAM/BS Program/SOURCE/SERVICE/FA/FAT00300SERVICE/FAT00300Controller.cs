using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using FAT00300Common;
using FAT00300Common.DTOs;
using FAT00300Common.Requests;
using FAT00300Back;
using FAT00300Back.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FAT00300Service
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FAT00300Controller : ControllerBase, IFAT00300
    {
        private readonly LoggerFAT00300 _logger;
        private readonly ActivitySource _activitySource;

        public FAT00300Controller(ILogger<FAT00300Controller> logger)
        {
            LoggerFAT00300.R_InitializeLogger(logger);
            _logger = LoggerFAT00300.R_GetInstanceLogger();
            _activitySource = FAT00300Activity.R_InitializeAndGetActivitySource(nameof(FAT00300Controller));
        }

        #region CRUD Methods

        [HttpPost]
        public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<FAT00300DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceDelete);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                var loCls = new FAT00300Cls();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CLANG_ID = R_BackGlobalVar.CULTURE;
                _logger.LogInfo("Start method R_DeleteAsync in {0}", lcMethod);
                await loCls.R_DeleteAsync(poParameter.Entity);
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
        public async Task<R_ServiceGetRecordResultDTO<FAT00300DTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<FAT00300DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceGetRecord);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<FAT00300DTO>();

            try
            {
                var loCls = new FAT00300Cls();
                _logger.LogInfo("Start method R_GetRecordAsync in {0}", lcMethod);
                poParameter.Entity.CLANG_ID = R_BackGlobalVar.CULTURE;
                loRtn.data = await loCls.R_GetRecordAsync(poParameter.Entity);
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
        public async Task<R_ServiceSaveResultDTO<FAT00300DTO>> R_ServiceSave(R_ServiceSaveParameterDTO<FAT00300DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceSave);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceSaveResultDTO<FAT00300DTO>();

            try
            {
                var loCls = new FAT00300Cls();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CLANG_ID = R_BackGlobalVar.CULTURE;
                _logger.LogInfo("Start method R_ServiceSave in {0}", lcMethod);
                loRtn.data = await loCls.R_SaveAsync(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception loExCaught)
            {
                loEx.Add(loExCaught);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        #endregion

        #region Non-Streaming Methods

        [HttpPost]
        public async Task<FAT00300ResultDTO<FAT00300GetValidationDataResultDTO>> GetValidationData(FAT00300GetValidationDataParameterDTO poParameter)
        {
            var lcMethod = nameof(GetValidationData);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetValidationDataResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method GetValidationData in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.Data = await loCls.GetValidationDataAsync(poParameter);
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
        public async Task<FAT00300ResultDTO<FAT00300GetInitialProcessResultDTO>> GetInitialProcess(FAT00300GetInitialProcessParameterDTO poParameter)
        {
            var lcMethod = nameof(GetInitialProcess);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetInitialProcessResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method GetInitialProcess in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.Data = await loCls.GetInitialProcessAsync(poParameter);
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
        public async Task<FAT00300ResultDTO<FAT00300GetAssetInformationTABResultDTO>> GetAssetInformationTAB(FAT00300GetAssetInformationTABParameterDTO poParameter)
        {
            var lcMethod = nameof(GetAssetInformationTAB);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetAssetInformationTABResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method GetAssetInformationTAB in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.Data = await loCls.GetAssetInformationTABAsync(poParameter);
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
        public async Task<FAT00300ResultDTO<FAT00300ValidateDeptCodeResultDTO>> ValidateDeptCode(FAT00300ValidateDeptCodeParameterDTO poParameter)
        {
            var lcMethod = nameof(ValidateDeptCode);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300ValidateDeptCodeResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method ValidateDeptCode in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.Data = await loCls.ValidateDeptCodeAsync(poParameter);
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
        public async Task<FAT00300ResultDTO<FAT00300ValidateGLJournalAccountResultDTO>> ValidateGLJournalAccount(FAT00300ValidateGLJournalAccountParameterDTO poParameter)
        {
            var lcMethod = nameof(ValidateGLJournalAccount);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300ValidateGLJournalAccountResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method ValidateGLJournalAccount in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.Data = await loCls.ValidateGLJournalAccountAsync(poParameter);
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
        public async Task<FAT00300ResultDTO<FAT00300GetUserCanApproveResultDTO>> GetUserCanApprove(FAT00300GetUserCanApproveParameterDTO poParameter)
        {
            var lcMethod = nameof(GetUserCanApprove);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetUserCanApproveResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method GetUserCanApprove in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.Data = await loCls.GetUserCanApproveAsync(poParameter);
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
        public async Task<FAT00300ResultDTO<FAT00300GetUserCanCloseResultDTO>> GetUserCanClose(FAT00300GetUserCanCloseParameterDTO poParameter)
        {
            var lcMethod = nameof(GetUserCanClose);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetUserCanCloseResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method GetUserCanClose in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.Data = await loCls.GetUserCanCloseAsync(poParameter);
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
        public async Task<FAT00300ResultDTO<FAT00300GetApprovalPrecheckResultDTO>> GetApprovalPrecheck(FAT00300GetApprovalPrecheckParameterDTO poParameter)
        {
            var lcMethod = nameof(GetApprovalPrecheck);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetApprovalPrecheckResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method GetApprovalPrecheck in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.Data = await loCls.GetApprovalPrecheckAsync(poParameter);
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
        public async Task<FAT00300ResultDTO<FAT00300GetValidateVoidResultDTO>> GetValidateVoid(FAT00300GetValidateVoidParameterDTO poParameter)
        {
            var lcMethod = nameof(GetValidateVoid);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetValidateVoidResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method GetValidateVoid in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.Data = await loCls.GetValidateVoidAsync(poParameter);
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
        public async Task<FAT00300ResultDTO<FAT00300GetValidateTransDateResultDTO>> GetValidateTransDate(FAT00300GetValidateTransDateParameterDTO poParameter)
        {
            var lcMethod = nameof(GetValidateTransDate);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetValidateTransDateResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method GetValidateTransDate in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.Data = await loCls.GetValidateTransDateAsync(poParameter);
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
        public async Task<FAT00300ResultDTO<FAT00300GetValidateOutstandTransResultDTO>> GetValidateOutstandTrans(FAT00300GetValidateOutstandTransParameterDTO poParameter)
        {
            var lcMethod = nameof(GetValidateOutstandTrans);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetValidateOutstandTransResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method GetValidateOutstandTrans in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.Data = await loCls.GetValidateOutstandTransAsync(poParameter);
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
        public async Task<FAT00300ResultDTO<FAT00300SubmitProcessResultDTO>> SubmitProcess(FAT00300SubmitProcessParameterDTO poParameter)
        {
            var lcMethod = nameof(SubmitProcess);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300SubmitProcessResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method SubmitProcess in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.Data = await loCls.SubmitProcessAsync(poParameter);
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
        public async Task<FAT00300ResultDTO<FAT00300ApproveProcessResultDTO>> ApproveProcess(FAT00300ApproveProcessParameterDTO poParameter)
        {
            var lcMethod = nameof(ApproveProcess);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300ApproveProcessResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method ApproveProcess in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.Data = await loCls.ApproveProcessAsync(poParameter);
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
        public async Task<FAT00300ResultDTO<FAT00300VoidProcessResultDTO>> VoidProcess(FAT00300VoidProcessParameterDTO poParameter)
        {
            var lcMethod = nameof(VoidProcess);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300VoidProcessResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method VoidProcess in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.Data = await loCls.VoidProcessAsync(poParameter);
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
        public async Task<FAT00300ResultDTO<FAT00300CloseProcessResultDTO>> CloseProcess(FAT00300CloseProcessParameterDTO poParameter)
        {
            var lcMethod = nameof(CloseProcess);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300CloseProcessResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method CloseProcess in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.Data = await loCls.CloseProcessAsync(poParameter);
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
        public async Task<FAT00300ResultDTO<FAT00300GetAssetResultDTO>> GetAsset(FAT00300GetAssetParameterDTO poParameter)
        {
            var lcMethod = nameof(GetAsset);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetAssetResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method CloseProcess in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                //poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.Data = await loCls.GetAssetAsync(poParameter);
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
        public async Task<FAT00300ResultDTO<FAT00300GetCompanyInfoResultDTO>> GetCompanyInfo(FAT00300GetCompanyInfoParameterDTO poParameter)
        {
            var lcMethod = nameof(GetCompanyInfo);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetCompanyInfoResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

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
        public async Task<FAT00300ResultDTO<FAT00300GetSystemParamResultDTO>> GetSystemParam(FAT00300GetSystemParamParameterDTO poParameter)
        {
            var lcMethod = nameof(GetSystemParam);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetSystemParamResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method CloseProcess in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
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
        public async Task<FAT00300ResultDTO<FAT00300GetPeriodInfoResultDTO>> GetPeriodInfo(FAT00300GetPeriodInfoParamDTO poParameter)
        {
            var lcMethod = nameof(GetPeriodInfo);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetPeriodInfoResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

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
        public async Task<FAT00300ResultDTO<FAT00300GetTransCodeInfoResultDTO>> GetTransCodeInfo(FAT00300GetTransCodeInfoParamDTO poParameter)
        {
            var lcMethod = nameof(GetTransCodeInfo);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetTransCodeInfoResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

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
        public async Task<FAT00300ResultDTO<FAT00300GetPeriodRangeResultDTO>> GetPeriodRange(FAT00300GetPeriodRangeParamDTO poParameter)
        {
            var lcMethod = nameof(GetPeriodRange);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetPeriodRangeResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

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
        #endregion

        #region Streaming Methods

        [HttpPost]
        public async IAsyncEnumerable<FAT00300GetAllocationExpenseListResultDTO> GetAllocationExpenseList()
        {
            var lcMethod = nameof(GetAllocationExpenseList);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT00300GetAllocationExpenseListResultDTO> loResult = new List<FAT00300GetAllocationExpenseListResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method GetAllocationExpenseList in {0}", lcMethod);

                var loParam = new FAT00300GetAllocationExpenseListParameterDTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLANG_ID = R_BackGlobalVar.CULTURE,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CASSET_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstants.ASSET_CODE) ?? string.Empty
                };

                loResult = await loCls.GetAllocationExpenseListAsync(loParam);
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
        [HttpPost]
        public async IAsyncEnumerable<FAT00300GetTransListResultDTO> GetAllTransList()
        {
            var lcMethod = nameof(GetAllTransList);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT00300GetTransListResultDTO> loResult = new List<FAT00300GetTransListResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method GetAllocationExpenseList in {0}", lcMethod);

                var loParam = new FAT00300GetTransListParameterDTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLANGUAGE_ID = R_BackGlobalVar.CULTURE,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstants.TRANS_CODE) ?? string.Empty,
                    CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstants.DEPT_CODE) ?? string.Empty,
                    CFROM_PERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstants.FROM_PERIOD) ?? string.Empty,
                    CTO_PERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstants.TO_PERIOD) ?? string.Empty,
                    CASSET_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstants.ASSET_CODE) ?? string.Empty
                };

                loResult = await loCls.GetTransListAsync(loParam);
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
        [HttpPost]
        public async Task<FAT00300ResultDTO<FAT00300DTO>> DeleteTransaction(FAT00300DTO poParameter)
        {
            var lcMethod = nameof(DeleteTransaction);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300DTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method Delete Transaction in {0}", lcMethod);

                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                loRtn.Data = await loCls.DeleteTransactionAsync(poParameter);
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
        public async IAsyncEnumerable<FAT00300GetDeptListResultDTO> GetAllDeptList()
        {
            var lcMethod = nameof(GetAllDeptList);
            using var loActivity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT00300GetDeptListResultDTO> loResult = new List<FAT00300GetDeptListResultDTO>();

            try
            {
                var loCls = new FAT00300Cls();

                _logger.LogInfo("Start method FAT00300GetDeptListResultDTO in {0}", lcMethod);

                var loParam = new FAT00300GetDeptListParameterDTO
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






