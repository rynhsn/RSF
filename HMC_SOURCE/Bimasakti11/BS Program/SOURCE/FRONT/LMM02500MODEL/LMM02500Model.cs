using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LMM02500Common;
using LMM02500Common.DTO;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace LMM02500Model
{
    public class LMM02500Model : R_BusinessObjectServiceClientBase<LMM02500ProfileDTO>, ILMM02500
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMM02500";
        private const string DEFAULT_MODULE = "LM";

        public LMM02500Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<LMM02500ProfileDTO>> GetAllTenantGroupStreamAsync(string pcCPROPERTY_ID)
        {
            var loEx = new R_Exception();
            var loResult = new List<LMM02500ProfileDTO>();


            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantParameterLMM02500.CPROPERTY_ID, pcCPROPERTY_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM02500ProfileDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02500.GetAllTenantGroupStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<LMM02500ListDTO<LMM02500ParameterDTO>> GetParameterTenantGroupAsync()
        {
            var loEx = new R_Exception();
            LMM02500ListDTO<LMM02500ParameterDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM02500ListDTO<LMM02500ParameterDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02500.GetParameterTenantGroup),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<LMM02500ListDTO<LMM02500ComboBoxType>> GetTaxTypeComboBoxAsync()
        {
            var loEx = new R_Exception();
            LMM02500ListDTO<LMM02500ComboBoxType> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM02500ListDTO<LMM02500ComboBoxType>>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02500.GetTaxCodeComboBox),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<LMM02500ListDTO<LMM02500ComboBoxType>> GetIdTypeComboBoxAsync()
        {
            var loEx = new R_Exception();
            LMM02500ListDTO<LMM02500ComboBoxType> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM02500ListDTO<LMM02500ComboBoxType>>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02500.GetIdTypeComboBox),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<LMM02500ListDTO<LMM02500ComboBoxType>> GetTaxCodeComboBoxAsync()
        {
            var loEx = new R_Exception();
            LMM02500ListDTO<LMM02500ComboBoxType> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM02500ListDTO<LMM02500ComboBoxType>>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02500.GetTaxCodeComboBox),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        /*
        public async Task<LMM02500TaxInfoDTO> GetTaxInfoTenantGroupAsync(string pcCPROPERTY_ID,
            string pcTENANT_GROUP_ID)
        {
            var loEx = new R_Exception();
            LMM02500TaxInfoDTO loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantDetailLMM02500.CPROPERTY_ID, pcCPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantDetailLMM02500.CTENANT_GROUP_ID, pcTENANT_GROUP_ID);
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM02500TaxInfoDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02500.GetTaxInfoTenantGroup),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        */

        #region NotUsed

        public IAsyncEnumerable<LMM02500ProfileDTO> GetAllTenantGroupStream()
        {
            throw new NotImplementedException();
        }

        public LMM02500ListDTO<LMM02500ParameterDTO> GetParameterTenantGroup()
        {
            throw new NotImplementedException();
        }

        public LMM02500ProfileDTO GetProfileTenantGroup()
        {
            throw new NotImplementedException();
        }

        public LMM02500TaxInfoDTO GetTaxInfoTenantGroup()
        {
            throw new NotImplementedException();
        }

        public LMM02500ListDTO<LMM02500ComboBoxType> GetTaxTypeComboBox()
        {
            throw new NotImplementedException();
        }

        public LMM02500ListDTO<LMM02500ComboBoxType> GetIdTypeComboBox()
        {
            throw new NotImplementedException();
        }

        public LMM02500ListDTO<LMM02500ComboBoxType> GetTaxCodeComboBox()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}