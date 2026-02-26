using FAM00100Common;
using FAM00100Common.DTOs;
using FAM00100Common.DTOs.FAM00100;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Threading.Tasks;

namespace FAM00100Model
{
    public class FAM00100Model : R_BusinessObjectServiceClientBase<FAM00100DTO>, IFAM00100
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlFA";
        private const string DEFAULT_ENDPOINT = "api/FAM00100";
        private const string DEFAULT_MODULE = "FA";

        public FAM00100Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<FAM00100ValidateInitDTO> GetInitValidateAsync()
        {
            var loEx = new R_Exception();
            FAM00100ValidateInitDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<FAM00100SingleResult<FAM00100ValidateInitDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAM00100.GetInitValidate),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<FAM00100DTO> GetSystemParamCBAsync()
        {
            var loEx = new R_Exception();
            FAM00100DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<FAM00100SingleResult<FAM00100DTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAM00100.GetSystemParamCB),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<FAM00100GSPeriodYearRangeDTO> GetGSPeriodYearRangeAsync()
        {
            var loEx = new R_Exception();
            FAM00100GSPeriodYearRangeDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<FAM00100SingleResult<FAM00100GSPeriodYearRangeDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IFAM00100.GetGSPeriodYearRange),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<FAM00100DTO> SaveSystemParamCBAsync(FAM00100SaveParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            FAM00100DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<FAM00100SingleResult<FAM00100DTO>, FAM00100SaveParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IFAM00100.SaveSystemParamCB),
                    poEntity,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #region not implemented
        public FAM00100SingleResult<FAM00100GSPeriodYearRangeDTO> GetGSPeriodYearRange()
        {
            throw new System.NotImplementedException();
        }

        public FAM00100SingleResult<FAM00100ValidateInitDTO> GetInitValidate()
        {
            throw new System.NotImplementedException();
        }

        public FAM00100SingleResult<FAM00100DTO> GetSystemParamCB()
        {
            throw new System.NotImplementedException();
        }

        public FAM00100SingleResult<FAM00100DTO> SaveSystemParamCB(FAM00100SaveParameterDTO poEntity)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
