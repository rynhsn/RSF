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
    public class GSM04500AccountSettingDeptModel : R_BusinessObjectServiceClientBase<GSM04500AccountSettingDeptDTO>,
        IGSM04500AccountSettingDept
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM04500AccountSettingDept";
        private const string DEFAULT_MODULE = "gs";

        public GSM04500AccountSettingDeptModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region not implemented
        public IAsyncEnumerable<GSM04500AccountSettingDeptDTO> GSM04500GetAllAccountSettingDeptListStream()
        {
            throw new NotImplementedException();
        }
        #endregion
        
        
        public async Task<List<GSM04500AccountSettingDeptDTO>> GetAllStreamAsync()
        {
            var loEx = new R_Exception();
            List<GSM04500AccountSettingDeptDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM04500AccountSettingDeptDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04500AccountSettingDept.GSM04500GetAllAccountSettingDeptListStream),
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