using PMB02200COMMON;
using PMB02200COMMON.DTO_s;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMB02200MODEL
{
    public class PMB02200Model : R_BusinessObjectServiceClientBase<UtilityChargesDTO>, IPMB02200
    {

        private const string DEFAULT_CHECKPOINT_NAME = "api/PMB02200";

        public PMB02200Model(
            string pcHttpClientName = PMB02200ContextConstant.DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                PMB02200ContextConstant.DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }
        public IAsyncEnumerable<UtilityChargesDTO> GetUtilityCharges()
        {
            throw new NotImplementedException();
        }

        public async Task<List<UtilityChargesDTO>> GetUtilityChargesAsync()

        {
            var loEx = new R_Exception();
            List<UtilityChargesDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = PMB02200ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UtilityChargesDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMB02200.GetUtilityCharges),
                    PMB02200ContextConstant.DEFAULT_MODULE, _SendWithContext,
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
