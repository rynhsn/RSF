using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using FAT00300Common;
using FAT00300Common.DTOs;
using FAT00300Common.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FAT00300Model
{
    /// <summary>
    /// Model class for FAT00300 - FA Transaction operations
    /// Handles communication with backend service
    /// </summary>
    public class FAT00300Model : R_BusinessObjectServiceClientBase<FAT00300DTO>, IFAT00300
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlFA";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/FAT00300";
        private const string DEFAULT_MODULE = "FA";

        public FAT00300Model()
            : base(DEFAULT_HTTP_NAME, DEFAULT_SERVICEPOINT_NAME, DEFAULT_MODULE, true, true)
        {
        }

        #region Non-Streaming Methods

        /// <summary>
        /// Get validation data
        /// </summary>
        /// <param name="poParameter">Parameter DTO containing request data</param>
        /// <returns>Result DTO containing validation data</returns>
        public async Task<FAT00300ResultDTO<FAT00300GetValidationDataResultDTO>> GetValidationData(FAT00300GetValidationDataParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetValidationDataResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300GetValidationDataResultDTO>, FAT00300GetValidationDataParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.GetValidationData),
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
        /// Get initial process
        /// </summary>
        /// <param name="poParameter">Parameter DTO containing request data</param>
        /// <returns>Result DTO containing initial process data</returns>
        public async Task<FAT00300ResultDTO<FAT00300GetInitialProcessResultDTO>> GetInitialProcess(FAT00300GetInitialProcessParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetInitialProcessResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300GetInitialProcessResultDTO>, FAT00300GetInitialProcessParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.GetInitialProcess),
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
        /// Get asset information TAB
        /// </summary>
        /// <param name="poParameter">Parameter DTO containing request data</param>
        /// <returns>Result DTO containing asset information TAB data</returns>
        public async Task<FAT00300ResultDTO<FAT00300GetAssetInformationTABResultDTO>> GetAssetInformationTAB(FAT00300GetAssetInformationTABParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetAssetInformationTABResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300GetAssetInformationTABResultDTO>, FAT00300GetAssetInformationTABParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.GetAssetInformationTAB),
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
        /// Validate department code
        /// </summary>
        /// <param name="poParameter">Parameter DTO containing request data</param>
        /// <returns>Result DTO containing validation result</returns>
        public async Task<FAT00300ResultDTO<FAT00300ValidateDeptCodeResultDTO>> ValidateDeptCode(FAT00300ValidateDeptCodeParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300ValidateDeptCodeResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300ValidateDeptCodeResultDTO>, FAT00300ValidateDeptCodeParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.ValidateDeptCode),
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
        /// Validate GL journal account
        /// </summary>
        /// <param name="poParameter">Parameter DTO containing request data</param>
        /// <returns>Result DTO containing validation result</returns>
        public async Task<FAT00300ResultDTO<FAT00300ValidateGLJournalAccountResultDTO>> ValidateGLJournalAccount(FAT00300ValidateGLJournalAccountParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300ValidateGLJournalAccountResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300ValidateGLJournalAccountResultDTO>, FAT00300ValidateGLJournalAccountParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.ValidateGLJournalAccount),
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
        /// Get user can approve
        /// </summary>
        /// <param name="poParameter">Parameter DTO containing request data</param>
        /// <returns>Result DTO containing user can approve data</returns>
        public async Task<FAT00300ResultDTO<FAT00300GetUserCanApproveResultDTO>> GetUserCanApprove(FAT00300GetUserCanApproveParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetUserCanApproveResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300GetUserCanApproveResultDTO>, FAT00300GetUserCanApproveParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.GetUserCanApprove),
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
        /// Get user can close
        /// </summary>
        /// <param name="poParameter">Parameter DTO containing request data</param>
        /// <returns>Result DTO containing user can close data</returns>
        public async Task<FAT00300ResultDTO<FAT00300GetUserCanCloseResultDTO>> GetUserCanClose(FAT00300GetUserCanCloseParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetUserCanCloseResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300GetUserCanCloseResultDTO>, FAT00300GetUserCanCloseParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.GetUserCanClose),
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
        /// Get approval precheck
        /// </summary>
        /// <param name="poParameter">Parameter DTO containing request data</param>
        /// <returns>Result DTO containing approval precheck data</returns>
        public async Task<FAT00300ResultDTO<FAT00300GetApprovalPrecheckResultDTO>> GetApprovalPrecheck(FAT00300GetApprovalPrecheckParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetApprovalPrecheckResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300GetApprovalPrecheckResultDTO>, FAT00300GetApprovalPrecheckParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.GetApprovalPrecheck),
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
        /// Get validate void
        /// </summary>
        /// <param name="poParameter">Parameter DTO containing request data</param>
        /// <returns>Result DTO containing validate void data</returns>
        public async Task<FAT00300ResultDTO<FAT00300GetValidateVoidResultDTO>> GetValidateVoid(FAT00300GetValidateVoidParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetValidateVoidResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300GetValidateVoidResultDTO>, FAT00300GetValidateVoidParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.GetValidateVoid),
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
        /// Get validate trans date
        /// </summary>
        /// <param name="poParameter">Parameter DTO containing request data</param>
        /// <returns>Result DTO containing validate trans date data</returns>
        public async Task<FAT00300ResultDTO<FAT00300GetValidateTransDateResultDTO>> GetValidateTransDate(FAT00300GetValidateTransDateParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetValidateTransDateResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300GetValidateTransDateResultDTO>, FAT00300GetValidateTransDateParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.GetValidateTransDate),
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
        /// Get validate outstand trans
        /// </summary>
        /// <param name="poParameter">Parameter DTO containing request data</param>
        /// <returns>Result DTO containing validate outstand trans data</returns>
        public async Task<FAT00300ResultDTO<FAT00300GetValidateOutstandTransResultDTO>> GetValidateOutstandTrans(FAT00300GetValidateOutstandTransParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetValidateOutstandTransResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300GetValidateOutstandTransResultDTO>, FAT00300GetValidateOutstandTransParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.GetValidateOutstandTrans),
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
        /// Submit process
        /// </summary>
        /// <param name="poParameter">Parameter DTO containing request data</param>
        /// <returns>Result DTO containing submit process result</returns>
        public async Task<FAT00300ResultDTO<FAT00300SubmitProcessResultDTO>> SubmitProcess(FAT00300SubmitProcessParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300SubmitProcessResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300SubmitProcessResultDTO>, FAT00300SubmitProcessParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.SubmitProcess),
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
        /// Approve process
        /// </summary>
        /// <param name="poParameter">Parameter DTO containing request data</param>
        /// <returns>Result DTO containing approve process result</returns>
        public async Task<FAT00300ResultDTO<FAT00300ApproveProcessResultDTO>> ApproveProcess(FAT00300ApproveProcessParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300ApproveProcessResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300ApproveProcessResultDTO>, FAT00300ApproveProcessParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.ApproveProcess),
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
        /// Void process
        /// </summary>
        /// <param name="poParameter">Parameter DTO containing request data</param>
        /// <returns>Result DTO containing void process result</returns>
        public async Task<FAT00300ResultDTO<FAT00300VoidProcessResultDTO>> VoidProcess(FAT00300VoidProcessParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300VoidProcessResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300VoidProcessResultDTO>, FAT00300VoidProcessParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.VoidProcess),
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
        /// Close process
        /// </summary>
        /// <param name="poParameter">Parameter DTO containing request data</param>
        /// <returns>Result DTO containing close process result</returns>
        public async Task<FAT00300ResultDTO<FAT00300CloseProcessResultDTO>> CloseProcess(FAT00300CloseProcessParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300CloseProcessResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300CloseProcessResultDTO>, FAT00300CloseProcessParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.CloseProcess),
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

        public async Task<FAT00300ResultDTO<FAT00300GetAssetResultDTO>> GetAsset(FAT00300GetAssetParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetAssetResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300GetAssetResultDTO>, FAT00300GetAssetParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.GetAsset),
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

        public async Task<FAT00300ResultDTO<FAT00300GetCompanyInfoResultDTO>> GetCompanyInfo(FAT00300GetCompanyInfoParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetCompanyInfoResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300GetCompanyInfoResultDTO>, FAT00300GetCompanyInfoParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.GetCompanyInfo),
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

        public async Task<FAT00300ResultDTO<FAT00300GetSystemParamResultDTO>> GetSystemParam(FAT00300GetSystemParamParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetSystemParamResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300GetSystemParamResultDTO>, FAT00300GetSystemParamParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.GetSystemParam),
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

        public async Task<FAT00300ResultDTO<FAT00300GetPeriodInfoResultDTO>> GetPeriodInfo(FAT00300GetPeriodInfoParamDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetPeriodInfoResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300GetPeriodInfoResultDTO>, FAT00300GetPeriodInfoParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.GetPeriodInfo),
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

        public async Task<FAT00300ResultDTO<FAT00300GetTransCodeInfoResultDTO>> GetTransCodeInfo(FAT00300GetTransCodeInfoParamDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetTransCodeInfoResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300GetTransCodeInfoResultDTO>, FAT00300GetTransCodeInfoParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.GetTransCodeInfo),
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

        public async Task<FAT00300ResultDTO<FAT00300GetPeriodRangeResultDTO>> GetPeriodRange(FAT00300GetPeriodRangeParamDTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300GetPeriodRangeResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300GetPeriodRangeResultDTO>, FAT00300GetPeriodRangeParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.GetPeriodRange),
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

        #endregion

        #region Streaming Methods

        /// <summary>
        /// Get allocation expense list - Interface compliance only (not used)
        /// </summary>
        public IAsyncEnumerable<FAT00300GetAllocationExpenseListResultDTO> GetAllocationExpenseList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get allocation expense list - Actual implementation
        /// Retrieves allocation expense list using streaming API
        /// </summary>
        /// <returns>Result DTO containing list of allocation expense records</returns>
        public async Task<FAT00300ResultDTO<List<FAT00300GetAllocationExpenseListResultDTO>>> GetAllocationExpenseListAsync()
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<List<FAT00300GetAllocationExpenseListResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT00300GetAllocationExpenseListResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.GetAllocationExpenseList),
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

        public IAsyncEnumerable<FAT00300GetTransListResultDTO> GetAllTransList()
        {
            throw new NotImplementedException();
        }

        public async Task<FAT00300ResultDTO<List<FAT00300GetTransListResultDTO>>> GetAllTransListAsync()
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<List<FAT00300GetTransListResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT00300GetTransListResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.GetAllTransList),
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

        public async Task<FAT00300ResultDTO<List<FAT00300GetDeptListResultDTO>>> GetAllDeptListAsync()
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<List<FAT00300GetDeptListResultDTO>>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT00300GetDeptListResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.GetAllDeptList),
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
        public async Task<FAT00300ResultDTO<FAT00300DTO>> DeleteTransaction(FAT00300DTO poParameter)
        {
            var loEx = new R_Exception();
            var loRtn = new FAT00300ResultDTO<FAT00300DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<FAT00300ResultDTO<FAT00300DTO>, FAT00300DTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00300.DeleteTransaction),
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

        public IAsyncEnumerable<FAT00300GetDeptListResultDTO> GetAllDeptList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

