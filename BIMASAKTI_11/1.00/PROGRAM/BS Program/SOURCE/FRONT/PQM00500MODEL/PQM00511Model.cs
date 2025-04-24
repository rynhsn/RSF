using PQM00500COMMON.DTO_s;
using PQM00500COMMON.Interfaces;
using PQM00500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using R_BusinessObjectFront;

namespace PQM00500MODEL
{
    public class PQM00511Model : R_BusinessObjectServiceClientBase<MenuUserDTO>, IPQM00511
    {
        private const string DEFAULT_CHECKPOINT_NAME = "api/PQM00511";

        public PQM00511Model(string pcHttpClientName = PQM00500ContextConstant.DEFAULT_HTTP_NAME,
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


        public async Task<List<MenuUserDTO>> GetList_UserMenuAsync()
        {
            var loEx = new R_Exception();
            List<MenuUserDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = PQM00500ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<MenuUserDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPQM00511.GetList_UserMenu),
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

        public IAsyncEnumerable<MenuUserDTO> GetList_UserMenu()
        {
            throw new NotImplementedException();
        }
    }
}
