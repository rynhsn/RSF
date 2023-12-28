using System;
using System.Threading.Tasks;
using LMM01500Common;
using LMM01500Common.DTOs;
using R_BusinessObjectFront;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace LMM01500Model
{
    public class LMM01500InitModel : R_BusinessObjectServiceClientBase<LMM01500PropertyDTO>, ILMM01500Init
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMM01500Init";
        private const string DEFAULT_MODULE = "lm";

        public LMM01500InitModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
        
        #region not implemented
        public LMM01500ListDTO<LMM01500PropertyDTO> LMM01500GetPropertyList()
        {
            throw new NotImplementedException();
        }
        #endregion
        
        public async Task<LMM01500ListDTO<LMM01500PropertyDTO>> GetAllPropertyAsync()
        {
            var loEx = new R_Exception();
            LMM01500ListDTO<LMM01500PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM01500ListDTO<LMM01500PropertyDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01500Init.LMM01500GetPropertyList),
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