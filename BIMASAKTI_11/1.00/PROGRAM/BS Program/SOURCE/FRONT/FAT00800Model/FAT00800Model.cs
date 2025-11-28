using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FAT00800Common;
using FAT00800Common.DTOs;
using R_APIClient;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;

namespace FAT00800Model
{
    /// <summary>
    /// Model class for FAT00800 - Fixed Asset Transaction operations
    /// Handles communication with backend service
    /// </summary>
    public class FAT00800Model : R_BusinessObjectServiceClientBase<FAT00800DTO>, IFAT00800
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlFA";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/FAT00800";
        private const string DEFAULT_MODULE = "FA";

        public FAT00800Model()
            : base(DEFAULT_HTTP_NAME, DEFAULT_SERVICEPOINT_NAME, DEFAULT_MODULE, true, true)
        {
        }

        #region CRUD Methods

        public new async Task<R_ServiceGetRecordResultDTO<FAT00800DTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<FAT00800DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<FAT00800DTO> loResult = new R_ServiceGetRecordResultDTO<FAT00800DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<R_ServiceGetRecordResultDTO<FAT00800DTO>, R_ServiceGetRecordParameterDTO<FAT00800DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.R_ServiceGetRecord),
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

        public new async Task<R_ServiceSaveResultDTO<FAT00800DTO>> R_ServiceSave(R_ServiceSaveParameterDTO<FAT00800DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<FAT00800DTO> loResult = new R_ServiceSaveResultDTO<FAT00800DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<R_ServiceSaveResultDTO<FAT00800DTO>, R_ServiceSaveParameterDTO<FAT00800DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.R_ServiceSave),
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

        public new async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<FAT00800DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loResult = new R_ServiceDeleteResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<R_ServiceDeleteResultDTO, R_ServiceDeleteParameterDTO<FAT00800DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.R_ServiceDelete),
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

        #region Initial Process Methods

        public async Task<FAT00800ResultDTO<FAT00800GetPeriodResultDTO>> GetPeriod(FAT00800GetPeriodParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800GetPeriodResultDTO> loResult = new FAT00800ResultDTO<FAT00800GetPeriodResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetPeriodResultDTO>, FAT00800GetPeriodParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.GetPeriod),
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

        public async Task<FAT00800ResultDTO<FAT00800GetLocalBaseCurrResultDTO>> GetLocalBaseCurr(FAT00800GetLocalBaseCurrParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800GetLocalBaseCurrResultDTO> loResult = new FAT00800ResultDTO<FAT00800GetLocalBaseCurrResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetLocalBaseCurrResultDTO>, FAT00800GetLocalBaseCurrParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.GetLocalBaseCurr),
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

        public async Task<FAT00800ResultDTO<FAT00800GetTransTypeDescResultDTO>> GetTransTypeDesc(FAT00800GetTransTypeDescParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800GetTransTypeDescResultDTO> loResult = new FAT00800ResultDTO<FAT00800GetTransTypeDescResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetTransTypeDescResultDTO>, FAT00800GetTransTypeDescParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.GetTransTypeDesc),
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

        public async Task<FAT00800ResultDTO<FAT00800GetUserRightApprovalResultDTO>> GetUserRightApproval(FAT00800GetUserRightApprovalParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800GetUserRightApprovalResultDTO> loResult = new FAT00800ResultDTO<FAT00800GetUserRightApprovalResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetUserRightApprovalResultDTO>, FAT00800GetUserRightApprovalParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.GetUserRightApproval),
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

        public async Task<FAT00800ResultDTO<FAT00800GetUserActivityRightsResultDTO>> GetUserActivityRights(FAT00800GetUserActivityRightsParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800GetUserActivityRightsResultDTO> loResult = new FAT00800ResultDTO<FAT00800GetUserActivityRightsResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetUserActivityRightsResultDTO>, FAT00800GetUserActivityRightsParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.GetUserActivityRights),
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

        public async Task<FAT00800ResultDTO<FAT00800GetValidateDepartmentResultDTO>> GetValidateDepartment(FAT00800GetValidateDepartmentParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800GetValidateDepartmentResultDTO> loResult = new FAT00800ResultDTO<FAT00800GetValidateDepartmentResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetValidateDepartmentResultDTO>, FAT00800GetValidateDepartmentParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.GetValidateDepartment),
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

        #region Page 1 Validation Methods

        public async Task<FAT00800ResultDTO<FAT00800GetValidateTransDateResultDTO>> GetValidateTransDate(FAT00800GetValidateTransDateParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800GetValidateTransDateResultDTO> loResult = new FAT00800ResultDTO<FAT00800GetValidateTransDateResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetValidateTransDateResultDTO>, FAT00800GetValidateTransDateParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.GetValidateTransDate),
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

        public async Task<FAT00800ResultDTO<FAT00800GetValidateOutstandTransResultDTO>> GetValidateOutstandTrans(FAT00800GetValidateOutstandTransParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800GetValidateOutstandTransResultDTO> loResult = new FAT00800ResultDTO<FAT00800GetValidateOutstandTransResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetValidateOutstandTransResultDTO>, FAT00800GetValidateOutstandTransParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.GetValidateOutstandTrans),
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

        public async Task<FAT00800ResultDTO<FAT00800GetValidateVoidResultDTO>> GetValidateVoid(FAT00800GetValidateVoidParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800GetValidateVoidResultDTO> loResult = new FAT00800ResultDTO<FAT00800GetValidateVoidResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetValidateVoidResultDTO>, FAT00800GetValidateVoidParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.GetValidateVoid),
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

        #region Page 1 Button Methods

        public async Task<FAT00800ResultDTO<FAT00800DoSubmitResultDTO>> DoSubmit(FAT00800DoSubmitParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800DoSubmitResultDTO> loResult = new FAT00800ResultDTO<FAT00800DoSubmitResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800DoSubmitResultDTO>, FAT00800DoSubmitParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.DoSubmit),
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

        public async Task<FAT00800ResultDTO<FAT00800DoCloseResultDTO>> DoClose(FAT00800DoCloseParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800DoCloseResultDTO> loResult = new FAT00800ResultDTO<FAT00800DoCloseResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800DoCloseResultDTO>, FAT00800DoCloseParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.DoClose),
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

        public async Task<FAT00800ResultDTO<FAT00800GetValidateGLResultDTO>> GetValidateGL(FAT00800GetValidateGLParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800GetValidateGLResultDTO> loResult = new FAT00800ResultDTO<FAT00800GetValidateGLResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetValidateGLResultDTO>, FAT00800GetValidateGLParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.GetValidateGL),
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

        public async Task<FAT00800ResultDTO<FAT00800DoApproveResultDTO>> DoApprove(FAT00800DoApproveParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800DoApproveResultDTO> loResult = new FAT00800ResultDTO<FAT00800DoApproveResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800DoApproveResultDTO>, FAT00800DoApproveParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.DoApprove),
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

        public async Task<FAT00800ResultDTO<FAT00800DoVoidResultDTO>> DoVoid(FAT00800DoVoidParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800DoVoidResultDTO> loResult = new FAT00800ResultDTO<FAT00800DoVoidResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800DoVoidResultDTO>, FAT00800DoVoidParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.DoVoid),
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

        public async Task<FAT00800ResultDTO<FAT00800GetApprovalPrecheckResultDTO>> GetApprovalPrecheck(FAT00800GetApprovalPrecheckParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800GetApprovalPrecheckResultDTO> loResult = new FAT00800ResultDTO<FAT00800GetApprovalPrecheckResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetApprovalPrecheckResultDTO>, FAT00800GetApprovalPrecheckParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.GetApprovalPrecheck),
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

        #region Page 1 Display Methods

        public async Task<FAT00800ResultDTO<FAT00800GetBookValueResultDTO>> GetBookValue(FAT00800GetBookValueParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800GetBookValueResultDTO> loResult = new FAT00800ResultDTO<FAT00800GetBookValueResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetBookValueResultDTO>, FAT00800GetBookValueParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.GetBookValue),
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

        public async Task<FAT00800ResultDTO<FAT00800GetCurrencyResultDTO>> GetCurrency(FAT00800GetCurrencyParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800GetCurrencyResultDTO> loResult = new FAT00800ResultDTO<FAT00800GetCurrencyResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetCurrencyResultDTO>, FAT00800GetCurrencyParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.GetCurrency),
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

        #region Page 2 Methods

        public async Task<FAT00800ResultDTO<FAT00800GetAssetInfoResultDTO>> GetAssetInfo(FAT00800GetAssetInfoParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<FAT00800GetAssetInfoResultDTO> loResult = new FAT00800ResultDTO<FAT00800GetAssetInfoResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<FAT00800ResultDTO<FAT00800GetAssetInfoResultDTO>, FAT00800GetAssetInfoParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.GetAssetInfo),
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

        public IAsyncEnumerable<FAT00800GetGridAllocResultDTO> GetGridAlloc()
        {
            throw new NotImplementedException();
        }

        public async Task<FAT00800ResultDTO<List<FAT00800GetGridAllocResultDTO>>> GetGridAllocAsync()
        {
            var loEx = new R_Exception();
            FAT00800ResultDTO<List<FAT00800GetGridAllocResultDTO>> loRtn = new FAT00800ResultDTO<List<FAT00800GetGridAllocResultDTO>>();
            List<FAT00800GetGridAllocResultDTO> loResult = new List<FAT00800GetGridAllocResultDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<FAT00800GetGridAllocResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAT00800.GetGridAlloc),
                    _ModuleName,
                    _SendWithContext,
                    _SendWithToken);
                
                loRtn.Data = loResult;
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

