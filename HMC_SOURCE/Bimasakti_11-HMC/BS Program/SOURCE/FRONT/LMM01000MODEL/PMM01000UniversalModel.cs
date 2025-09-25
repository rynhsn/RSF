using PMM01000COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMM01000MODEL
{
    public class PMM01000UniversalModel : R_BusinessObjectServiceClientBase<PMM01000DTO>, IPMM01000Universal
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMM01000Universal";
        private const string DEFAULT_MODULE = "PM";

        public PMM01000UniversalModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region Implemet
        public async Task<List<PMM01000DTOPropety>> GetPropertyAsync()
        {
            var loEx = new R_Exception();
            List<PMM01000DTOPropety> loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01000DTOPropety>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01000Universal.GetProperty),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public async Task<PMM01000List<PMM01000UniversalDTO>> GetChargesTypeListAsync()
        {
            var loEx = new R_Exception();
            PMM01000List<PMM01000UniversalDTO> loRtn = new PMM01000List<PMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data= await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01000Universal.GetChargesTypeList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public async Task<PMM01000List<PMM01000UniversalDTO>> GetStatusListAsync()
        {
            var loEx = new R_Exception();
            PMM01000List<PMM01000UniversalDTO> loRtn = new PMM01000List<PMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01000Universal.GetStatusList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public async Task<PMM01000List<PMM01000UniversalDTO>> GetTaxExemptionCodeListAsync()
        {
            var loEx = new R_Exception();
            PMM01000List<PMM01000UniversalDTO> loRtn = new PMM01000List<PMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01000Universal.GetTaxExemptionCodeList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public async Task<PMM01000List<PMM01000UniversalDTO>> GetWithholdingTaxTypeListAsync()
        {
            var loEx = new R_Exception();
            PMM01000List<PMM01000UniversalDTO> loRtn = new PMM01000List<PMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01000Universal.GetWithholdingTaxTypeList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public async Task<PMM01000List<PMM01000UniversalDTO>> GetRateTypeListAsync()
        {
            var loEx = new R_Exception();
            PMM01000List<PMM01000UniversalDTO> loRtn = new PMM01000List<PMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01000Universal.GetRateTypeList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public async Task<PMM01000List<PMM01000UniversalDTO>> GetUsageRateModeListAsync()
        {
            var loEx = new R_Exception();
            PMM01000List<PMM01000UniversalDTO> loRtn = new PMM01000List<PMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01000Universal.GetUsageRateModeList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public async Task<PMM01000List<PMM01000UniversalDTO>> GetAdminFeeTypeListAsync()
        {
            var loEx = new R_Exception();
            PMM01000List<PMM01000UniversalDTO> loRtn = new PMM01000List<PMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01000Universal.GetAdminFeeTypeList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public async Task<PMM01000List<PMM01000UniversalDTO>> GetAccrualMethodListAsync()
        {
            var loEx = new R_Exception();
            PMM01000List<PMM01000UniversalDTO> loRtn = new PMM01000List<PMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01000Universal.GetAccrualMethodList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public async Task<PMM01000AllResultInit> GetAllInitPMM01000ListAsync()
        {
            var loEx = new R_Exception();
            PMM01000AllResultInit loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<PMM01000AllResultInit>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01000Universal.GetAllInitLMM01000List),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        #endregion

        #region NotImplement
        public IAsyncEnumerable<PMM01000UniversalDTO> GetChargesTypeList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMM01000UniversalDTO> GetStatusList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMM01000UniversalDTO> GetTaxExemptionCodeList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMM01000UniversalDTO> GetWithholdingTaxTypeList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMM01000UniversalDTO> GetUsageRateModeList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMM01000UniversalDTO> GetRateTypeList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMM01000UniversalDTO> GetAdminFeeTypeList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMM01000UniversalDTO> GetAccrualMethodList()
        {
            throw new NotImplementedException();
        }
        public PMM01000AllResultInit GetAllInitLMM01000List()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PMM01000DTOPropety> GetProperty()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
