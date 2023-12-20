using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GSM04500Common;
using GSM04500Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GSM04500Model
{
    public class GSM04500AccountSettingModel : R_BusinessObjectServiceClientBase<GSM04500AccountSettingDTO>,
        IGSM04500AccountSetting
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM04500AccountSetting";
        private const string DEFAULT_MODULE = "gs";

        public GSM04500AccountSettingModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region not implemented
        public IAsyncEnumerable<GSM04500AccountSettingDTO> GSM04500GetAllAccountSettingListStream()
        {
            throw new NotImplementedException();
        }
        #endregion
        
        
        public async Task<List<GSM04500AccountSettingDTO>> GetAllStreamAsync()
        {
            var loEx = new R_Exception();
            List<GSM04500AccountSettingDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM04500AccountSettingDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04500AccountSetting.GSM04500GetAllAccountSettingListStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken,
                    null);
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