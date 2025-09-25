using HDM00500COMMON.DTO_s;
using HDM00500COMMON.Interfaces;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HDM00500MODEL
{
    public class HDM00501Model : R_BusinessObjectServiceClientBase<ChecklistDTO>, IHDM00501
    {
        //var & const
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlHD";
        private const string DEFAULT_CHECKPOINT_NAME = "api/HDM00501";
        private const string DEFAULT_MODULE = "HD";
        public HDM00501Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        //real method
        public async Task<List<ChecklistDTO>> GetList_ChecklistAsync()
        {
            var loEx = new R_Exception();
            List<ChecklistDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<ChecklistDTO>(
                    _RequestServiceEndPoint,
                    nameof(IHDM00501.GetList_Checklist),
                    DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        //implement only
        public IAsyncEnumerable<ChecklistDTO> GetList_Checklist()
        {
            throw new NotImplementedException();
        }
    }
}
