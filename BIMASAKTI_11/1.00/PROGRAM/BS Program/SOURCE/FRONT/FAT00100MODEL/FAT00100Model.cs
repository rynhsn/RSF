using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;
using FAT00100Common;
using FAT00100Common.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FAT00100Model
{
    /// <summary>
    /// Model class for FAT00100 - Fixed Asset Transaction operations
    /// Handles communication with backend service
    /// </summary>
    public class FAT00100Model : R_BusinessObjectServiceClientBase<FAT00100DTO>, IFAT00100
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlFA";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/FAT00100";
        private const string DEFAULT_MODULE = "FA";

        public FAT00100Model()
            : base(DEFAULT_HTTP_NAME, DEFAULT_SERVICEPOINT_NAME, DEFAULT_MODULE, true, true)
        {
        }

        #region CRUD Methods

        public async Task<R_ServiceGetRecordResultDTO<FAT00100DTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<FAT00100DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<FAT00100DTO> loResult = new R_ServiceGetRecordResultDTO<FAT00100DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<R_ServiceGetRecordResultDTO<FAT00100DTO>, R_ServiceGetRecordParameterDTO<FAT00100DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.R_ServiceGetRecord),
                    poParameter,
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<R_ServiceSaveResultDTO<FAT00100DTO>> R_ServiceSave(R_ServiceSaveParameterDTO<FAT00100DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<FAT00100DTO> loResult = new R_ServiceSaveResultDTO<FAT00100DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<R_ServiceSaveResultDTO<FAT00100DTO>, R_ServiceSaveParameterDTO<FAT00100DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.R_ServiceSave),
                    poParameter,
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<FAT00100DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loResult = new R_ServiceDeleteResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<R_ServiceDeleteResultDTO, R_ServiceDeleteParameterDTO<FAT00100DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.R_ServiceDelete),
                    poParameter,
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        #endregion

        #region Non-Streaming Methods

        public async Task<FAT00100ResultDTO<FAT00100GetDeptLookUpValidationResultDTO>> GetDeptLookUpValidation(FAT00100GetDeptLookUpValidationParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<FAT00100GetDeptLookUpValidationResultDTO> loResult = new FAT00100ResultDTO<FAT00100GetDeptLookUpValidationResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00100ResultDTO<FAT00100GetDeptLookUpValidationResultDTO>, FAT00100GetDeptLookUpValidationParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.GetDeptLookUpValidation),
                    poParameter,
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<FAT00100ResultDTO<FAT00100GetInitialProcessResultDTO>> GetInitialProcess(FAT00100GetInitialProcessParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<FAT00100GetInitialProcessResultDTO> loResult = new FAT00100ResultDTO<FAT00100GetInitialProcessResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00100ResultDTO<FAT00100GetInitialProcessResultDTO>, FAT00100GetInitialProcessParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.GetInitialProcess),
                    poParameter,
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<FAT00100ResultDTO<FAT00100GetPeriodYearResultDTO>> GetPeriodYear(FAT00100GetPeriodYearParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<FAT00100GetPeriodYearResultDTO> loResult = new FAT00100ResultDTO<FAT00100GetPeriodYearResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00100ResultDTO<FAT00100GetPeriodYearResultDTO>, FAT00100GetPeriodYearParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.GetPeriodYear),
                    poParameter,
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<FAT00100ResultDTO<FAT00100ValidateDeptCodeResultDTO>> ValidateDeptCode(FAT00100ValidateDeptCodeParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<FAT00100ValidateDeptCodeResultDTO> loResult = new FAT00100ResultDTO<FAT00100ValidateDeptCodeResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00100ResultDTO<FAT00100ValidateDeptCodeResultDTO>, FAT00100ValidateDeptCodeParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.ValidateDeptCode),
                    poParameter,
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<FAT00100ResultDTO<FAT00100GetPeriodDTResultDTO>> GetPeriodDT(FAT00100GetPeriodDTParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<FAT00100GetPeriodDTResultDTO> loResult = new FAT00100ResultDTO<FAT00100GetPeriodDTResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00100ResultDTO<FAT00100GetPeriodDTResultDTO>, FAT00100GetPeriodDTParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.GetPeriodDT),
                    poParameter,
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<FAT00100ResultDTO<FAT00100RSP_GET_CURRENCY_RATEResultDTO>> RSP_GET_CURRENCY_RATE(FAT00100RSP_GET_CURRENCY_RATEParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<FAT00100RSP_GET_CURRENCY_RATEResultDTO> loResult = new FAT00100ResultDTO<FAT00100RSP_GET_CURRENCY_RATEResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00100ResultDTO<FAT00100RSP_GET_CURRENCY_RATEResultDTO>, FAT00100RSP_GET_CURRENCY_RATEParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.RSP_GET_CURRENCY_RATE),
                    poParameter,
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<FAT00100ResultDTO<FAT00100SubmitProcessResultDTO>> SubmitProcess(FAT00100SubmitProcessParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<FAT00100SubmitProcessResultDTO> loResult = new FAT00100ResultDTO<FAT00100SubmitProcessResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00100ResultDTO<FAT00100SubmitProcessResultDTO>, FAT00100SubmitProcessParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.SubmitProcess),
                    poParameter,
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<FAT00100ResultDTO<FAT00100CloseProcessResultDTO>> CloseProcess(FAT00100CloseProcessParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<FAT00100CloseProcessResultDTO> loResult = new FAT00100ResultDTO<FAT00100CloseProcessResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00100ResultDTO<FAT00100CloseProcessResultDTO>, FAT00100CloseProcessParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.CloseProcess),
                    poParameter,
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<FAT00100ResultDTO<FAT00100ApproveProcessResultDTO>> ApproveProcess(FAT00100ApproveProcessParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<FAT00100ApproveProcessResultDTO> loResult = new FAT00100ResultDTO<FAT00100ApproveProcessResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00100ResultDTO<FAT00100ApproveProcessResultDTO>, FAT00100ApproveProcessParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.ApproveProcess),
                    poParameter,
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<FAT00100ResultDTO<FAT00100ValidationAssetCodeResultDTO>> ValidationAssetCode(FAT00100ValidationAssetCodeParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<FAT00100ValidationAssetCodeResultDTO> loResult = new FAT00100ResultDTO<FAT00100ValidationAssetCodeResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00100ResultDTO<FAT00100ValidationAssetCodeResultDTO>, FAT00100ValidationAssetCodeParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.ValidationAssetCode),
                    poParameter,
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<FAT00100ResultDTO<FAT00100RunApprovalPrecheckResultDTO>> RunApprovalPrecheck(FAT00100RunApprovalPrecheckParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<FAT00100RunApprovalPrecheckResultDTO> loResult = new FAT00100ResultDTO<FAT00100RunApprovalPrecheckResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00100ResultDTO<FAT00100RunApprovalPrecheckResultDTO>, FAT00100RunApprovalPrecheckParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.RunApprovalPrecheck),
                    poParameter,
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<FAT00100ResultDTO<FAT00100VoidProcessResultDTO>> VoidProcess(FAT00100VoidProcessParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<FAT00100VoidProcessResultDTO> loResult = new FAT00100ResultDTO<FAT00100VoidProcessResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00100ResultDTO<FAT00100VoidProcessResultDTO>, FAT00100VoidProcessParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.VoidProcess),
                    poParameter,
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<FAT00100ResultDTO<FAT00100ValidationBeforeSubmitResultDTO>> ValidationBeforeSubmit(FAT00100ValidationBeforeSubmitParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<FAT00100ValidationBeforeSubmitResultDTO> loResult = new FAT00100ResultDTO<FAT00100ValidationBeforeSubmitResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00100ResultDTO<FAT00100ValidationBeforeSubmitResultDTO>, FAT00100ValidationBeforeSubmitParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.ValidationBeforeSubmit),
                    poParameter,
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<FAT00100ResultDTO<FAT00100ValidationBeforeCloseResultDTO>> ValidationBeforeClose(FAT00100ValidationBeforeCloseParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<FAT00100ValidationBeforeCloseResultDTO> loResult = new FAT00100ResultDTO<FAT00100ValidationBeforeCloseResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00100ResultDTO<FAT00100ValidationBeforeCloseResultDTO>, FAT00100ValidationBeforeCloseParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.ValidationBeforeClose),
                    poParameter,
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<FAT00100ResultDTO<FAT00100ValidatePJTransResultDTO>> ValidatePJTrans(FAT00100ValidatePJTransParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<FAT00100ValidatePJTransResultDTO> loResult = new FAT00100ResultDTO<FAT00100ValidatePJTransResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00100ResultDTO<FAT00100ValidatePJTransResultDTO>, FAT00100ValidatePJTransParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.ValidatePJTrans),
                    poParameter,
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        #endregion

        #region Streaming Methods

        /// <summary>
        /// Get combo period month - Interface method (throws NotImplementedException)
        /// </summary>
        public IAsyncEnumerable<FAT00100GetComboPeriodMonthResultDTO> GetComboPeriodMonth()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get combo period month - Actual implementation
        /// </summary>
        public async Task<FAT00100ResultDTO<List<FAT00100GetComboPeriodMonthResultDTO>>> GetComboPeriodMonthAsync()
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<List<FAT00100GetComboPeriodMonthResultDTO>> loRtn = new FAT00100ResultDTO<List<FAT00100GetComboPeriodMonthResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT00100GetComboPeriodMonthResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.GetComboPeriodMonth),
                    _ModuleName,
                    true,
                    true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        /// <summary>
        /// Get data grid - Interface method (throws NotImplementedException)
        /// </summary>
        public IAsyncEnumerable<FAT00100GetDataGridResultDTO> GetDataGrid()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get data grid - Actual implementation
        /// </summary>
        public async Task<FAT00100ResultDTO<List<FAT00100GetDataGridResultDTO>>> GetDataGridAsync()
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<List<FAT00100GetDataGridResultDTO>> loRtn = new FAT00100ResultDTO<List<FAT00100GetDataGridResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT00100GetDataGridResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.GetDataGrid),
                    _ModuleName,
                    true,
                    true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        /// <summary>
        /// Get GSM supplier info - Interface method (throws NotImplementedException)
        /// </summary>
        public IAsyncEnumerable<FAT00100GetGSM_SUPPLIER_INFOResultDTO> GetGSM_SUPPLIER_INFO()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get GSM supplier info - Actual implementation
        /// </summary>
        public async Task<FAT00100ResultDTO<List<FAT00100GetGSM_SUPPLIER_INFOResultDTO>>> GetGSM_SUPPLIER_INFOAsync()
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<List<FAT00100GetGSM_SUPPLIER_INFOResultDTO>> loRtn = new FAT00100ResultDTO<List<FAT00100GetGSM_SUPPLIER_INFOResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT00100GetGSM_SUPPLIER_INFOResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.GetGSM_SUPPLIER_INFO),
                    _ModuleName,
                    true,
                    true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        /// <summary>
        /// Get GSM supplier contact - Interface method (throws NotImplementedException)
        /// </summary>
        public IAsyncEnumerable<FAT00100GetGSM_SUPPLIER_CONTACTResultDTO> GetGSM_SUPPLIER_CONTACT()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get GSM supplier contact - Actual implementation
        /// </summary>
        public async Task<FAT00100ResultDTO<List<FAT00100GetGSM_SUPPLIER_CONTACTResultDTO>>> GetGSM_SUPPLIER_CONTACTAsync()
        {
            var loEx = new R_Exception();
            FAT00100ResultDTO<List<FAT00100GetGSM_SUPPLIER_CONTACTResultDTO>> loRtn = new FAT00100ResultDTO<List<FAT00100GetGSM_SUPPLIER_CONTACTResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT00100GetGSM_SUPPLIER_CONTACTResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00100.GetGSM_SUPPLIER_CONTACT),
                    _ModuleName,
                    true,
                    true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        #endregion
    }
}

