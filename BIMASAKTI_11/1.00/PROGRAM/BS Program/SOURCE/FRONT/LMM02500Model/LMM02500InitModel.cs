using System;
using System.Threading.Tasks;
using LMM02500Common;
using LMM02500Common.DTOs;
using R_APIClient;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace LMM02500Model
{
    public class LMM02500InitModel : R_BusinessObjectServiceClientBase<LMM02500PropertyDTO>, ILMM02500Init
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT_NAME = "api/LMM02500Init";
        private const string DEFAULT_MODULE = "LM";

        public LMM02500InitModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region not implemented

        public LMM02500ListDTO<LMM02500PropertyDTO> LMM02500GetPropertyList()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        public async Task<LMM02500ListDTO<LMM02500PropertyDTO>> GetAllPropertyAsync()
        {
            var loEx = new R_Exception();
            LMM02500ListDTO<LMM02500PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM02500ListDTO<LMM02500PropertyDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02500Init.LMM02500GetPropertyList),
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
        
        
        
        public async Task<T> GetAsync<T>(string pcNameOf) where T : R_APIResultBaseDTO
        {
            var loEx = new R_Exception();
            var loResult = default(T);

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<T>(
                    _RequestServiceEndPoint,
                    pcNameOf,
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
    }
}