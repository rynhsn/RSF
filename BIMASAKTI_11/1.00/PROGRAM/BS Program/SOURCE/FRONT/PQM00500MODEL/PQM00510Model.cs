using PQM00500COMMON.DTO_s.Base;
using PQM00500COMMON.Interfaces;
using PQM00500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using R_BusinessObjectFront;
using PQM00500COMMON.DTO_s;

namespace PQM00500MODEL
{
    public class PQM00510Model : R_BusinessObjectServiceClientBase<MenuDTO>, IPQM00510
    {
        private const string DEFAULT_CHECKPOINT_NAME = "api/PQM00510";

        public PQM00510Model(string pcHttpClientName = PQM00500ContextConstant.DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                PQM00500ContextConstant.DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        { }


        public async Task<List<MenuDTO>> GetList_MenuAsync()
        {
            var loEx = new R_Exception();
            List<MenuDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = PQM00500ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<MenuDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPQM00510.GetList_Menu),
                    PQM00500ContextConstant.DEFAULT_MODULE,
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

        public IAsyncEnumerable<MenuDTO> GetList_Menu()
        {
            throw new NotImplementedException();
        }
    }
}
