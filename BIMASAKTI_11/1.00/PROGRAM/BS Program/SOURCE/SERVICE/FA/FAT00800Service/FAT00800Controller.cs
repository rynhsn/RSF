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
    public class FAT00800Controller : ControllerBase, IFAT00800
    {
        private readonly LoggerFAT00800 _logger;
        private readonly ActivitySource _activitySource;

        public FAT00800Controller(ILogger<FAT00800Controller> logger)
        {
            LoggerFAT00800.R_InitializeLogger(logger);
            _logger = LoggerFAT00800.R_GetInstanceLogger();
            _activitySource = FAT00800Activity.R_InitializeAndGetActivitySource(nameof(FAT00800Controller));
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
                var loCls = new FAT00800Cls();

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
                var loCls = new FAT00800Cls();

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
                var loCls = new FAT00800Cls();

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

        #region Initial Process Methods

        [HttpPost]
        public async Task<FAT00800ResultDTO<FAT00800GetPeriodResultDTO>> GetPeriod(FAT00800GetPeriodParameterDTO poParameter)
        {
            var lcMethod = nameof(GetPeriod);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetPeriodResultDTO>();

            try
            {
                var loCls = new FAT00800Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetPeriod in {0}", lcMethod);
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
        public async Task<FAT00800ResultDTO<FAT00800GetLocalBaseCurrResultDTO>> GetLocalBaseCurr(FAT00800GetLocalBaseCurrParameterDTO poParameter)
        {
            var lcMethod = nameof(GetLocalBaseCurr);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetLocalBaseCurrResultDTO>();

            try
            {
                var loCls = new FAT00800Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetLocalBaseCurr in {0}", lcMethod);
                loRtn = await loCls.GetLocalBaseCurrAsync(poParameter);
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
        public async Task<FAT00800ResultDTO<FAT00800GetTransTypeDescResultDTO>> GetTransTypeDesc(FAT00800GetTransTypeDescParameterDTO poParameter)
        {
            var lcMethod = nameof(GetTransTypeDesc);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetTransTypeDescResultDTO>();

            try
            {
                var loCls = new FAT00800Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetTransTypeDesc in {0}", lcMethod);
                loRtn = await loCls.GetTransTypeDescAsync(poParameter);
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
        public async Task<FAT00800ResultDTO<FAT00800GetUserRightApprovalResultDTO>> GetUserRightApproval(FAT00800GetUserRightApprovalParameterDTO poParameter)
        {
            var lcMethod = nameof(GetUserRightApproval);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetUserRightApprovalResultDTO>();

            try
            {
                var loCls = new FAT00800Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetUserRightApproval in {0}", lcMethod);
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
        public async Task<FAT00800ResultDTO<FAT00800GetUserActivityRightsResultDTO>> GetUserActivityRights(FAT00800GetUserActivityRightsParameterDTO poParameter)
        {
            var lcMethod = nameof(GetUserActivityRights);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetUserActivityRightsResultDTO>();

            try
            {
                var loCls = new FAT00800Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetUserActivityRights in {0}", lcMethod);
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
        public async Task<FAT00800ResultDTO<FAT00800GetValidateDepartmentResultDTO>> GetValidateDepartment(FAT00800GetValidateDepartmentParameterDTO poParameter)
        {
            var lcMethod = nameof(GetValidateDepartment);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetValidateDepartmentResultDTO>();

            try
            {
                var loCls = new FAT00800Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetValidateDepartment in {0}", lcMethod);
                loRtn = await loCls.GetValidateDepartmentAsync(poParameter);
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

        #region Page 1 Validation Methods

        [HttpPost]
        public async Task<FAT00800ResultDTO<FAT00800GetValidateTransDateResultDTO>> GetValidateTransDate(FAT00800GetValidateTransDateParameterDTO poParameter)
        {
            var lcMethod = nameof(GetValidateTransDate);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetValidateTransDateResultDTO>();

            try
            {
                var loCls = new FAT00800Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetValidateTransDate in {0}", lcMethod);
                loRtn = await loCls.GetValidateTransDateAsync(poParameter);
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
        public async Task<FAT00800ResultDTO<FAT00800GetValidateOutstandTransResultDTO>> GetValidateOutstandTrans(FAT00800GetValidateOutstandTransParameterDTO poParameter)
        {
            var lcMethod = nameof(GetValidateOutstandTrans);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetValidateOutstandTransResultDTO>();

            try
            {
                var loCls = new FAT00800Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetValidateOutstandTrans in {0}", lcMethod);
                loRtn = await loCls.GetValidateOutstandTransAsync(poParameter);
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
        public async Task<FAT00800ResultDTO<FAT00800GetValidateVoidResultDTO>> GetValidateVoid(FAT00800GetValidateVoidParameterDTO poParameter)
        {
            var lcMethod = nameof(GetValidateVoid);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetValidateVoidResultDTO>();

            try
            {
                var loCls = new FAT00800Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetValidateVoid in {0}", lcMethod);
                loRtn = await loCls.GetValidateVoidAsync(poParameter);
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

        #region Page 1 Button Methods

        [HttpPost]
        public async Task<FAT00800ResultDTO<FAT00800DoSubmitResultDTO>> DoSubmit(FAT00800DoSubmitParameterDTO poParameter)
        {
            var lcMethod = nameof(DoSubmit);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800DoSubmitResultDTO>
            {
                Data = new FAT00800DoSubmitResultDTO()
            };

            try
            {
                var loCls = new FAT00800Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method DoSubmit in {0}", lcMethod);
                await loCls.DoSubmitAsync(poParameter);
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
        public async Task<FAT00800ResultDTO<FAT00800DoCloseResultDTO>> DoClose(FAT00800DoCloseParameterDTO poParameter)
        {
            var lcMethod = nameof(DoClose);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800DoCloseResultDTO>
            {
                Data = new FAT00800DoCloseResultDTO()
            };

            try
            {
                var loCls = new FAT00800Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method DoClose in {0}", lcMethod);
                await loCls.DoCloseAsync(poParameter);
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
        public async Task<FAT00800ResultDTO<FAT00800GetValidateGLResultDTO>> GetValidateGL(FAT00800GetValidateGLParameterDTO poParameter)
        {
            var lcMethod = nameof(GetValidateGL);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetValidateGLResultDTO>
            {
                Data = new FAT00800GetValidateGLResultDTO()
            };

            try
            {
                var loCls = new FAT00800Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetValidateGL in {0}", lcMethod);
                await loCls.GetValidateGLAsync(poParameter);
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
        public async Task<FAT00800ResultDTO<FAT00800DoApproveResultDTO>> DoApprove(FAT00800DoApproveParameterDTO poParameter)
        {
            var lcMethod = nameof(DoApprove);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800DoApproveResultDTO>
            {
                Data = new FAT00800DoApproveResultDTO()
            };

            try
            {
                var loCls = new FAT00800Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method DoApprove in {0}", lcMethod);
                await loCls.DoApproveAsync(poParameter);
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
        public async Task<FAT00800ResultDTO<FAT00800DoVoidResultDTO>> DoVoid(FAT00800DoVoidParameterDTO poParameter)
        {
            var lcMethod = nameof(DoVoid);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800DoVoidResultDTO>
            {
                Data = new FAT00800DoVoidResultDTO()
            };

            try
            {
                var loCls = new FAT00800Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method DoVoid in {0}", lcMethod);
                await loCls.DoVoidAsync(poParameter);
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
        public async Task<FAT00800ResultDTO<FAT00800GetApprovalPrecheckResultDTO>> GetApprovalPrecheck(FAT00800GetApprovalPrecheckParameterDTO poParameter)
        {
            var lcMethod = nameof(GetApprovalPrecheck);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetApprovalPrecheckResultDTO>();

            try
            {
                var loCls = new FAT00800Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetApprovalPrecheck in {0}", lcMethod);
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

        #endregion

        #region Page 1 Display Methods

        [HttpPost]
        public async Task<FAT00800ResultDTO<FAT00800GetBookValueResultDTO>> GetBookValue(FAT00800GetBookValueParameterDTO poParameter)
        {
            var lcMethod = nameof(GetBookValue);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetBookValueResultDTO>();

            try
            {
                var loCls = new FAT00800Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetBookValue in {0}", lcMethod);
                loRtn = await loCls.GetBookValueAsync(poParameter);
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
        public async Task<FAT00800ResultDTO<FAT00800GetCurrencyResultDTO>> GetCurrency(FAT00800GetCurrencyParameterDTO poParameter)
        {
            var lcMethod = nameof(GetCurrency);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetCurrencyResultDTO>();

            try
            {
                var loCls = new FAT00800Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetCurrency in {0}", lcMethod);
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

        #endregion

        #region Page 2 Methods

        [HttpPost]
        public async Task<FAT00800ResultDTO<FAT00800GetAssetInfoResultDTO>> GetAssetInfo(FAT00800GetAssetInfoParameterDTO poParameter)
        {
            var lcMethod = nameof(GetAssetInfo);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00800ResultDTO<FAT00800GetAssetInfoResultDTO>();

            try
            {
                var loCls = new FAT00800Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetAssetInfo in {0}", lcMethod);
                loRtn = await loCls.GetAssetInfoAsync(poParameter);
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

        #region Streaming Methods

        [HttpPost]
        public async IAsyncEnumerable<FAT00800GetGridAllocResultDTO> GetGridAlloc()
        {
            var lcMethod = nameof(GetGridAlloc);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT00800GetGridAllocResultDTO> loResult = new List<FAT00800GetGridAllocResultDTO>();

            try
            {
                var loCls = new FAT00800Cls();

                // Create parameter DTO internally with global variables and streaming context
                var loParam = new FAT00800GetGridAllocParameterDTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLANG_ID = R_BackGlobalVar.CULTURE,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CASSET_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstants.CASSET_CODE) ?? string.Empty
                };

                _logger.LogInfo("Start method GetGridAlloc in {0}", lcMethod);
                loResult = await loCls.GetGridAllocAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            foreach (FAT00800GetGridAllocResultDTO loItem in loResult)
            {
                yield return loItem;
            }
        }


        #endregion
    }
}

