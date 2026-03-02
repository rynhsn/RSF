using FAT00700Common;
using FAT00700Common.DTOs;
using R_APIClient;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace FAT00700Model
{
    /// <summary>
    /// Model class for FAT00700 - FA Transaction operations
    /// Handles communication with backend API service
    /// </summary>
    public class FAT00700Model : R_BusinessObjectServiceClientBase<FAT00700DTO>, IFAT00700
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlFA";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/FAT00700";
        private const string DEFAULT_MODULE = "FA";

        public FAT00700Model()
            : base(DEFAULT_HTTP_NAME, DEFAULT_SERVICEPOINT_NAME, DEFAULT_MODULE, true, true)
        {
        }

        #region Non-Streaming Methods

        /// <summary>
        /// Get period information
        /// </summary>
        public async Task<FAT00700ResultDTO<GetPeriodResultDTO>> GetPeriod(GetPeriodParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00700ResultDTO<GetPeriodResultDTO> loRtn = new FAT00700ResultDTO<GetPeriodResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<GetPeriodResultDTO>, GetPeriodParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.GetPeriod),
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
            return loRtn;
        }

        /// <summary>
        /// Get currency information
        /// </summary>
        public async Task<FAT00700ResultDTO<GetCurrencyResultDTO>> GetCurrency(GetCurrencyParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00700ResultDTO<GetCurrencyResultDTO> loRtn = new FAT00700ResultDTO<GetCurrencyResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<GetCurrencyResultDTO>, GetCurrencyParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.GetCurrency),
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
            return loRtn;
        }

        /// <summary>
        /// Get FA transaction data
        /// </summary>
        public async Task<FAT00700ResultDTO<GetFATransactionDataResultDTO>> GetFATransactionData(GetFATransactionDataParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00700ResultDTO<GetFATransactionDataResultDTO> loRtn = new FAT00700ResultDTO<GetFATransactionDataResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<GetFATransactionDataResultDTO>, GetFATransactionDataParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.GetFATransactionData),
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
            return loRtn;
        }

        /// <summary>
        /// Get asset info data
        /// </summary>
        public async Task<FAT00700ResultDTO<GetAssetInfoDataResultDTO>> GetAssetInfoData(GetAssetInfoDataParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00700ResultDTO<GetAssetInfoDataResultDTO> loRtn = new FAT00700ResultDTO<GetAssetInfoDataResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<GetAssetInfoDataResultDTO>, GetAssetInfoDataParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.GetAssetInfoData),
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
            return loRtn;
        }

        /// <summary>
        /// Get date status
        /// </summary>
        public async Task<FAT00700ResultDTO<GetDateStatusResultDTO>> GetDateStatus(GetDateStatusParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00700ResultDTO<GetDateStatusResultDTO> loRtn = new FAT00700ResultDTO<GetDateStatusResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<GetDateStatusResultDTO>, GetDateStatusParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.GetDateStatus),
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
            return loRtn;
        }

        /// <summary>
        /// Get asset information
        /// </summary>
        public async Task<FAT00700ResultDTO<GetAssetInformationResultDTO>> GetAssetInformation(GetAssetInformationParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00700ResultDTO<GetAssetInformationResultDTO> loRtn = new FAT00700ResultDTO<GetAssetInformationResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<GetAssetInformationResultDTO>, GetAssetInformationParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.GetAssetInformation),
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
            return loRtn;
        }

        /// <summary>
        /// Get user right approval
        /// </summary>
        public async Task<FAT00700ResultDTO<GetUserRightApprovalResultDTO>> GetUserRightApproval(GetUserRightApprovalParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00700ResultDTO<GetUserRightApprovalResultDTO> loRtn = new FAT00700ResultDTO<GetUserRightApprovalResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<GetUserRightApprovalResultDTO>, GetUserRightApprovalParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.GetUserRightApproval),
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
            return loRtn;
        }

        /// <summary>
        /// Get user activity rights
        /// </summary>
        public async Task<FAT00700ResultDTO<GetUserActivityRightsResultDTO>> GetUserActivityRights(GetUserActivityRightsParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00700ResultDTO<GetUserActivityRightsResultDTO> loRtn = new FAT00700ResultDTO<GetUserActivityRightsResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<GetUserActivityRightsResultDTO>, GetUserActivityRightsParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.GetUserActivityRights),
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
            return loRtn;
        }

        /// <summary>
        /// Check outstanding transaction
        /// </summary>
        public async Task<FAT00700ResultDTO<CheckOutstandingTransResultDTO>> CheckOutstandingTrans(CheckOutstandingTransParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00700ResultDTO<CheckOutstandingTransResultDTO> loRtn = new FAT00700ResultDTO<CheckOutstandingTransResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<CheckOutstandingTransResultDTO>, CheckOutstandingTransParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.CheckOutstandingTrans),
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
            return loRtn;
        }

        /// <summary>
        /// Validate void operation
        /// </summary>
        public async Task<FAT00700ResultDTO<ValidateVoidResultDTO>> ValidateVoid(ValidateVoidParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00700ResultDTO<ValidateVoidResultDTO> loRtn = new FAT00700ResultDTO<ValidateVoidResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<ValidateVoidResultDTO>, ValidateVoidParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.ValidateVoid),
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
            return loRtn;
        }

        /// <summary>
        /// Get approval precheck
        /// </summary>
        public async Task<FAT00700ResultDTO<GetApprovalPrecheckResultDTO>> GetApprovalPrecheck(GetApprovalPrecheckParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00700ResultDTO<GetApprovalPrecheckResultDTO> loRtn = new FAT00700ResultDTO<GetApprovalPrecheckResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<GetApprovalPrecheckResultDTO>, GetApprovalPrecheckParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.GetApprovalPrecheck),
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
            return loRtn;
        }

        /// <summary>
        /// Validate found department
        /// </summary>
        public async Task<FAT00700ResultDTO<ValidateFoundDeptResultDTO>> ValidateFoundDept(ValidateFoundDeptParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00700ResultDTO<ValidateFoundDeptResultDTO> loRtn = new FAT00700ResultDTO<ValidateFoundDeptResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<ValidateFoundDeptResultDTO>, ValidateFoundDeptParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.ValidateFoundDept),
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
            return loRtn;
        }

        /// <summary>
        /// Get transaction date validation
        /// </summary>
        public async Task<FAT00700ResultDTO<GetTransDateValidationResultDTO>> GetTransDateValidation(GetTransDateValidationParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00700ResultDTO<GetTransDateValidationResultDTO> loRtn = new FAT00700ResultDTO<GetTransDateValidationResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<GetTransDateValidationResultDTO>, GetTransDateValidationParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.GetTransDateValidation),
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
            return loRtn;
        }

        /// <summary>
        /// Get grid allocation data
        /// </summary>
        public async Task<FAT00700ResultDTO<GetGridAllocDataResultDTO>> GetGridAllocData(GetGridAllocDataParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00700ResultDTO<GetGridAllocDataResultDTO> loRtn = new FAT00700ResultDTO<GetGridAllocDataResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<GetGridAllocDataResultDTO>, GetGridAllocDataParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.GetGridAllocData),
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
            return loRtn;
        }

        #endregion

        #region Void Methods

        /// <summary>
        /// Validate GL journal
        /// </summary>
        public async Task ValidateGLJournal(ValidateGLJournalParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                await R_HTTPClientWrapper.R_APIRequestObject<R_APIResultBaseDTO, ValidateGLJournalParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.ValidateGLJournal),
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
        }

        /// <summary>
        /// Submit button action
        /// </summary>
        public async Task<FAT00700ResultDTO<FAT00700SubmitProcessParameterDTO>> SubmitButton(FAT00700SubmitProcessParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<FAT00700SubmitProcessParameterDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<FAT00700SubmitProcessParameterDTO>, FAT00700SubmitProcessParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.SubmitButton),
                    poParameter,
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

        public async Task<FAT00700ResultDTO<FAT00700DTO>> DeleteTransaction(FAT00700DTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<FAT00700DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<FAT00700DTO>, FAT00700DTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.DeleteTransaction),
                    poParameter,
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
        /// Close button action
        /// </summary>
        public async Task CloseButton(CloseButtonParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                await R_HTTPClientWrapper.R_APIRequestObject<R_APIResultBaseDTO, CloseButtonParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.CloseButton),
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
        }

        /// <summary>
        /// Approve button action
        /// </summary>
        public async Task ApproveButton(ApproveButtonParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                await R_HTTPClientWrapper.R_APIRequestObject<R_APIResultBaseDTO, ApproveButtonParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.ApproveButton),
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
        }

        /// <summary>
        /// Void button action
        /// </summary>
        public async Task VoidButton(VoidButtonParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                await R_HTTPClientWrapper.R_APIRequestObject<R_APIResultBaseDTO, VoidButtonParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.VoidButton),
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
        }

        #endregion

        #region Streaming Methods

        public async Task<List<GetTransactionListResultDTO>> GetTransactionListAsync(GetTransactionListParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<GetTransactionListResultDTO> loResult = new List<GetTransactionListResultDTO>();

            try
            {
                //R_FrontContext.R_SetStreamingContext(ContextConstantDTO.CCOMPANY_ID, "HGRBH");
                //R_FrontContext.R_SetStreamingContext(ContextConstantDTO.CUSER_ID, "RTM");
                R_FrontContext.R_SetStreamingContext(ContextConstantDTO.CTRANS_CODE, poParameter.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantDTO.CASSET_CODE, poParameter.CASSET_CODE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetTransactionListResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.GetTransactionList),
                    DEFAULT_MODULE,
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

        public IAsyncEnumerable<GetTransactionListResultDTO> GetTransactionList()
        {
            throw new NotImplementedException();
        }

        public async Task<FAT00700ResultDTO<FAT00700CompanyInfoResultDTO>> GetCompanyInfo(FAT00700CompanyInfoParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<FAT00700CompanyInfoResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<FAT00700CompanyInfoResultDTO>, FAT00700CompanyInfoParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.GetCompanyInfo),
                    poParameter,
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

        public async Task<FAT00700ResultDTO<FAT00700SystemParamResultDTO>> GetSystemParam(FAT00700SystemParamParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<FAT00700SystemParamResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<FAT00700SystemParamResultDTO>, FAT00700SystemParamParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.GetSystemParam),
                    poParameter,
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

        public async Task<FAT00700ResultDTO<FAT00700PeriodInfoResultDTO>> GetPeriodInfo(FAT00700PeriodInfoParamDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<FAT00700PeriodInfoResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<FAT00700PeriodInfoResultDTO>, FAT00700PeriodInfoParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.GetPeriodInfo),
                    poParameter,
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

        public async Task<FAT00700ResultDTO<FAT00700TransCodeInfoResultDTO>> GetTransCodeInfo(FAT00700TransCodeInfoParamDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<FAT00700TransCodeInfoResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<FAT00700TransCodeInfoResultDTO>, FAT00700TransCodeInfoParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.GetTransCodeInfo),
                    poParameter,
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

        public async Task<FAT00700ResultDTO<FAT00700PeriodRangeResultDTO>> GetPeriodRange(FAT00700PeriodRangeParamDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<FAT00700PeriodRangeResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00700ResultDTO<FAT00700PeriodRangeResultDTO>, FAT00700PeriodRangeParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.GetPeriodRange),
                    poParameter,
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

        public async Task<FAT00700ResultDTO<List<FAT00700GetDeptListResultDTO>>> GetAllDeptListAsync()
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00700ResultDTO<List<FAT00700GetDeptListResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT00700GetDeptListResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00700.GetAllDeptList),
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
        public IAsyncEnumerable<FAT00700GetDeptListResultDTO> GetAllDeptList()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

