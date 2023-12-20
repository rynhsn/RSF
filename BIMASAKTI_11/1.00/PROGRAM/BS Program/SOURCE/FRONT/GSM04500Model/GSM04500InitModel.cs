using System;
using System.Threading.Tasks;
using GSM04500Common;
using GSM04500Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GSM04500Model
{
    public class GSM04500InitModel : R_BusinessObjectServiceClientBase<GSM04500PropertyDTO>,
        IGSM04500Init
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM04500Init";
        private const string DEFAULT_MODULE = "gs";

        public GSM04500InitModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region not implemented
        public GSM04500ListDTO<GSM04500PropertyDTO> GSM04500GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public GSM04500ListDTO<GSM04500FunctionDTO> GSM04500GetTypeList()
        {
            throw new NotImplementedException();
        }

        #endregion
        
        
        public async Task<GSM04500ListDTO<GSM04500PropertyDTO>> GetAllPropertyAsync()
        {
            var loEx = new R_Exception();
            GSM04500ListDTO<GSM04500PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM04500ListDTO<GSM04500PropertyDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04500Init.GSM04500GetPropertyList),
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
        public async Task<GSM04500ListDTO<GSM04500FunctionDTO>> GetAllTypeAsync()
        {
            var loEx = new R_Exception();
            GSM04500ListDTO<GSM04500FunctionDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM04500ListDTO<GSM04500FunctionDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04500Init.GSM04500GetTypeList),
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