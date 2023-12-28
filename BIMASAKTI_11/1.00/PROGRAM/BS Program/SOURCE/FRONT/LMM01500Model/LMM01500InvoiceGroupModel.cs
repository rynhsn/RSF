using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LMM01500Common;
using LMM01500Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace LMM01500Model
{
    public class LMM01500InvoiceGroupModel : R_BusinessObjectServiceClientBase<LMM01500InvGrpDTO>, ILMM01500InvoiceGroup
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMM01500InvoiceGroup";
        private const string DEFAULT_MODULE = "lm";

        public LMM01500InvoiceGroupModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
        
        #region not implemented
        public IAsyncEnumerable<LMM01500InvGrpGridDTO> LMM01500GetInvoiceGroupListStream()
        {
            throw new System.NotImplementedException();
        }
        #endregion
        
        public async Task<List<LMM01500InvGrpGridDTO>> GetAllStreamAsync()
        {
            var loEx = new R_Exception();
            List<LMM01500InvGrpGridDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01500InvGrpGridDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01500InvoiceGroup.LMM01500GetInvoiceGroupListStream),
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