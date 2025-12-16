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
    public class FAT00100Controller : ControllerBase, IFAT00100
    {
        private readonly LoggerFAT00100 _logger;
        private readonly ActivitySource _activitySource;

        public FAT00100Controller(ILogger<FAT00100Controller> logger)
        {
            LoggerFAT00100.R_InitializeLogger(logger);
            _logger = LoggerFAT00100.R_GetInstanceLogger();
            _activitySource = FAT00100Activity.R_InitializeAndGetActivitySource(nameof(FAT00100Controller));
        }

        [HttpPost]
        public async Task<R_ServiceGetRecordResultDTO<FAT00100DTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<FAT00100DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceGetRecord);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceGetRecordResultDTO<FAT00100DTO>();

            try
            {
                var loCls = new FAT00100Cls();

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
        public async Task<R_ServiceSaveResultDTO<FAT00100DTO>> R_ServiceSave(R_ServiceSaveParameterDTO<FAT00100DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceSave);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceSaveResultDTO<FAT00100DTO>();

            try
            {
                var loCls = new FAT00100Cls();

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
        public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<FAT00100DTO> poParameter)
        {
            var lcMethod = nameof(R_ServiceDelete);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                var loCls = new FAT00100Cls();

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
        public async Task<FAT00100ResultDTO<FAT00100GetDeptLookUpValidationResultDTO>> GetDeptLookUpValidation(FAT00100GetDeptLookUpValidationParameterDTO poParameter)
        {
            var lcMethod = nameof(GetDeptLookUpValidation);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00100ResultDTO<FAT00100GetDeptLookUpValidationResultDTO>();

            try
            {
                var loCls = new FAT00100Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetDeptLookUpValidation in {0}", lcMethod);
                loRtn = await loCls.GetDeptLookUpValidationAsync(poParameter);
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
        public async Task<FAT00100ResultDTO<FAT00100GetInitialProcessResultDTO>> GetInitialProcess(FAT00100GetInitialProcessParameterDTO poParameter)
        {
            var lcMethod = nameof(GetInitialProcess);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00100ResultDTO<FAT00100GetInitialProcessResultDTO>();

            try
            {
                var loCls = new FAT00100Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetInitialProcess in {0}", lcMethod);
                loRtn = await loCls.GetInitialProcessAsync(poParameter);
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
        public async Task<FAT00100ResultDTO<FAT00100GetPeriodYearResultDTO>> GetPeriodYear(FAT00100GetPeriodYearParameterDTO poParameter)
        {
            var lcMethod = nameof(GetPeriodYear);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00100ResultDTO<FAT00100GetPeriodYearResultDTO>();

            try
            {
                var loCls = new FAT00100Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetPeriodYear in {0}", lcMethod);
                loRtn = await loCls.GetPeriodYearAsync(poParameter);
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
        public async Task<FAT00100ResultDTO<FAT00100ValidateDeptCodeResultDTO>> ValidateDeptCode(FAT00100ValidateDeptCodeParameterDTO poParameter)
        {
            var lcMethod = nameof(ValidateDeptCode);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00100ResultDTO<FAT00100ValidateDeptCodeResultDTO>();

            try
            {
                var loCls = new FAT00100Cls();

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
        public async Task<FAT00100ResultDTO<FAT00100GetPeriodDTResultDTO>> GetPeriodDT(FAT00100GetPeriodDTParameterDTO poParameter)
        {
            var lcMethod = nameof(GetPeriodDT);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00100ResultDTO<FAT00100GetPeriodDTResultDTO>();

            try
            {
                var loCls = new FAT00100Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method GetPeriodDT in {0}", lcMethod);
                loRtn = await loCls.GetPeriodDTAsync(poParameter);
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
        public async Task<FAT00100ResultDTO<FAT00100RSP_GET_CURRENCY_RATEResultDTO>> RSP_GET_CURRENCY_RATE(FAT00100RSP_GET_CURRENCY_RATEParameterDTO poParameter)
        {
            var lcMethod = nameof(RSP_GET_CURRENCY_RATE);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00100ResultDTO<FAT00100RSP_GET_CURRENCY_RATEResultDTO>();

            try
            {
                var loCls = new FAT00100Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method RSP_GET_CURRENCY_RATE in {0}", lcMethod);
                loRtn = await loCls.RSP_GET_CURRENCY_RATEAsync(poParameter);
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
        public async Task<FAT00100ResultDTO<FAT00100SubmitProcessResultDTO>> SubmitProcess(FAT00100SubmitProcessParameterDTO poParameter)
        {
            var lcMethod = nameof(SubmitProcess);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00100ResultDTO<FAT00100SubmitProcessResultDTO>();

            try
            {
                var loCls = new FAT00100Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method SubmitProcess in {0}", lcMethod);
                loRtn = await loCls.SubmitProcessAsync(poParameter);
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
        public async Task<FAT00100ResultDTO<FAT00100CloseProcessResultDTO>> CloseProcess(FAT00100CloseProcessParameterDTO poParameter)
        {
            var lcMethod = nameof(CloseProcess);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00100ResultDTO<FAT00100CloseProcessResultDTO>();

            try
            {
                var loCls = new FAT00100Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method CloseProcess in {0}", lcMethod);
                loRtn = await loCls.CloseProcessAsync(poParameter);
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
        public async Task<FAT00100ResultDTO<FAT00100ApproveProcessResultDTO>> ApproveProcess(FAT00100ApproveProcessParameterDTO poParameter)
        {
            var lcMethod = nameof(ApproveProcess);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00100ResultDTO<FAT00100ApproveProcessResultDTO>();

            try
            {
                var loCls = new FAT00100Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method ApproveProcess in {0}", lcMethod);
                loRtn = await loCls.ApproveProcessAsync(poParameter);
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
        public async Task<FAT00100ResultDTO<FAT00100ValidationAssetCodeResultDTO>> ValidationAssetCode(FAT00100ValidationAssetCodeParameterDTO poParameter)
        {
            var lcMethod = nameof(ValidationAssetCode);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00100ResultDTO<FAT00100ValidationAssetCodeResultDTO>();

            try
            {
                var loCls = new FAT00100Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method ValidationAssetCode in {0}", lcMethod);
                loRtn = await loCls.ValidationAssetCodeAsync(poParameter);
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
        public async Task<FAT00100ResultDTO<FAT00100RunApprovalPrecheckResultDTO>> RunApprovalPrecheck(FAT00100RunApprovalPrecheckParameterDTO poParameter)
        {
            var lcMethod = nameof(RunApprovalPrecheck);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00100ResultDTO<FAT00100RunApprovalPrecheckResultDTO>();

            try
            {
                var loCls = new FAT00100Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method RunApprovalPrecheck in {0}", lcMethod);
                loRtn = await loCls.RunApprovalPrecheckAsync(poParameter);
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
        public async Task<FAT00100ResultDTO<FAT00100VoidProcessResultDTO>> VoidProcess(FAT00100VoidProcessParameterDTO poParameter)
        {
            var lcMethod = nameof(VoidProcess);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00100ResultDTO<FAT00100VoidProcessResultDTO>();

            try
            {
                var loCls = new FAT00100Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method VoidProcess in {0}", lcMethod);
                loRtn = await loCls.VoidProcessAsync(poParameter);
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
        public async Task<FAT00100ResultDTO<FAT00100ValidationBeforeSubmitResultDTO>> ValidationBeforeSubmit(FAT00100ValidationBeforeSubmitParameterDTO poParameter)
        {
            var lcMethod = nameof(ValidationBeforeSubmit);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00100ResultDTO<FAT00100ValidationBeforeSubmitResultDTO>();

            try
            {
                var loCls = new FAT00100Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method ValidationBeforeSubmit in {0}", lcMethod);
                loRtn = await loCls.ValidationBeforeSubmitAsync(poParameter);
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
        public async Task<FAT00100ResultDTO<FAT00100ValidationBeforeCloseResultDTO>> ValidationBeforeClose(FAT00100ValidationBeforeCloseParameterDTO poParameter)
        {
            var lcMethod = nameof(ValidationBeforeClose);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00100ResultDTO<FAT00100ValidationBeforeCloseResultDTO>();

            try
            {
                var loCls = new FAT00100Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method ValidationBeforeClose in {0}", lcMethod);
                loRtn = await loCls.ValidationBeforeCloseAsync(poParameter);
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
        public async Task<FAT00100ResultDTO<FAT00100ValidatePJTransResultDTO>> ValidatePJTrans(FAT00100ValidatePJTransParameterDTO poParameter)
        {
            var lcMethod = nameof(ValidatePJTrans);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            var loRtn = new FAT00100ResultDTO<FAT00100ValidatePJTransResultDTO>();

            try
            {
                var loCls = new FAT00100Cls();

                // Set global variables from R_BackGlobalVar
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLANG_ID = R_BackGlobalVar.CULTURE;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Start method ValidatePJTrans in {0}", lcMethod);
                loRtn = await loCls.ValidatePJTransAsync(poParameter);
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
        public async IAsyncEnumerable<FAT00100GetComboPeriodMonthResultDTO> GetComboPeriodMonth()
        {
            var lcMethod = nameof(GetComboPeriodMonth);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT00100GetComboPeriodMonthResultDTO> loResult = new List<FAT00100GetComboPeriodMonthResultDTO>();

            try
            {
                var loCls = new FAT00100Cls();

                // Create parameter DTO internally with global variables and streaming context
                var loParam = new FAT00100DTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLANG_ID = R_BackGlobalVar.CULTURE,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CSOFT_PERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstants.CSOFT_PERIOD) ?? string.Empty
                };

                _logger.LogInfo("Start method GetComboPeriodMonth in {0}", lcMethod);
                loResult = await loCls.GetComboPeriodMonthAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            foreach (FAT00100GetComboPeriodMonthResultDTO loItem in loResult)
            {
                yield return loItem;
            }
        }

        [HttpPost]
        public async IAsyncEnumerable<FAT00100GetDataGridResultDTO> GetDataGrid()
        {
            var lcMethod = nameof(GetDataGrid);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT00100GetDataGridResultDTO> loResult = new List<FAT00100GetDataGridResultDTO>();

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
                    CSUPPLIER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstants.CSUPPLIER_ID) ?? string.Empty,
                    CPERIODFROM = R_Utility.R_GetStreamingContext<string>(ContextConstants.CPERIODFROM) ?? string.Empty,
                    CPERIODTO = R_Utility.R_GetStreamingContext<string>(ContextConstants.CPERIODTO) ?? string.Empty,
                    CSTATUSDRAFT = R_Utility.R_GetStreamingContext<string>(ContextConstants.CSTATUSDRAFT) ?? string.Empty,
                    CSTATUSOPEN = R_Utility.R_GetStreamingContext<string>(ContextConstants.CSTATUSOPEN) ?? string.Empty,
                    CSTATUSAPPROVED = R_Utility.R_GetStreamingContext<string>(ContextConstants.CSTATUSAPPROVED) ?? string.Empty,
                    CSTATUSCLOSED = R_Utility.R_GetStreamingContext<string>(ContextConstants.CSTATUSCLOSED) ?? string.Empty
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

            foreach (FAT00100GetDataGridResultDTO loItem in loResult)
            {
                yield return loItem;
            }
        }

        [HttpPost]
        public async IAsyncEnumerable<FAT00100GetGSM_SUPPLIER_INFOResultDTO> GetGSM_SUPPLIER_INFO()
        {
            var lcMethod = nameof(GetGSM_SUPPLIER_INFO);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT00100GetGSM_SUPPLIER_INFOResultDTO> loResult = new List<FAT00100GetGSM_SUPPLIER_INFOResultDTO>();

            try
            {
                var loCls = new FAT00100Cls();

                // Create parameter DTO internally with global variables and streaming context
                var loParam = new FAT00100DTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLANG_ID = R_BackGlobalVar.CULTURE,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CSUPPLIER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstants.CSUPPLIER_ID) ?? string.Empty,
                    CINFO_SEQNO = R_Utility.R_GetStreamingContext<string>(ContextConstants.CINFO_SEQNO) ?? string.Empty
                };

                _logger.LogInfo("Start method GetGSM_SUPPLIER_INFO in {0}", lcMethod);
                loResult = await loCls.GetGSM_SUPPLIER_INFOAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            foreach (FAT00100GetGSM_SUPPLIER_INFOResultDTO loItem in loResult)
            {
                yield return loItem;
            }
        }

        [HttpPost]
        public async IAsyncEnumerable<FAT00100GetGSM_SUPPLIER_CONTACTResultDTO> GetGSM_SUPPLIER_CONTACT()
        {
            var lcMethod = nameof(GetGSM_SUPPLIER_CONTACT);
            using var activity = _activitySource.StartActivity(lcMethod);
            var loEx = new R_Exception();
            List<FAT00100GetGSM_SUPPLIER_CONTACTResultDTO> loResult = new List<FAT00100GetGSM_SUPPLIER_CONTACTResultDTO>();

            try
            {
                var loCls = new FAT00100Cls();

                // Create parameter DTO internally with global variables and streaming context
                var loParam = new FAT00100DTO
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLANG_ID = R_BackGlobalVar.CULTURE,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CSUPPLIER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstants.CSUPPLIER_ID) ?? string.Empty,
                    CINFO_SEQNO = R_Utility.R_GetStreamingContext<string>(ContextConstants.CINFO_SEQNO) ?? string.Empty
                };

                _logger.LogInfo("Start method GetGSM_SUPPLIER_CONTACT in {0}", lcMethod);
                loResult = await loCls.GetGSM_SUPPLIER_CONTACTAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            foreach (FAT00100GetGSM_SUPPLIER_CONTACTResultDTO loItem in loResult)
            {
                yield return loItem;
            }
        }
    }
}

