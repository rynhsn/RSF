using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMT05500COMMON.DTO;
using PMT05500COMMON.Interface;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMT05500Model
{
    public class LMT05500AgreementModel : R_BusinessObjectServiceClientBase<LMT05500AgreementDTO>, ILMT05500Agreement
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/LMT05500Agreement";
        private const string DEFAULT_MODULE = "PM";
        public LMT05500AgreementModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) 
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        #region implementLibrary
        public IAsyncEnumerable<LMT05500AgreementDTO> AgreementListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMT05500PropertyDTO> GetPropertyListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMT05500UnitDTO> GetDepositUnitStream()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region implements

        public async Task<LMT05500GenericList<LMT05500PropertyDTO>> GetPropertyListStreamAsyncModel()
        {
            var loEx = new R_Exception();
            LMT05500GenericList<LMT05500PropertyDTO> loResult = new LMT05500GenericList<LMT05500PropertyDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT05500PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT05500Agreement.GetPropertyListStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<LMT05500GenericList<LMT05500AgreementDTO>> GetAgreementListStreamAsyncModel()
        {
            var loEx = new R_Exception();
            LMT05500GenericList<LMT05500AgreementDTO> loResult = new LMT05500GenericList<LMT05500AgreementDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT05500AgreementDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT05500Agreement.AgreementListStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<LMT05500GenericList<LMT05500UnitDTO>> GetDepositUnitStreamAsyncModel()
        {
            var loEx = new R_Exception();
            LMT05500GenericList<LMT05500UnitDTO> loResult = new LMT05500GenericList<LMT05500UnitDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMT05500UnitDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMT05500Agreement.GetDepositUnitStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        #endregion
    }
}
