using PMM00100COMMON;
using PMM00100COMMON.DTO_s;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMM00100MODEL
{
    public class PMM00102Model : R_BusinessObjectServiceClientBase<HoUtilBuildingMappingDTO>, IPMM00102
    {
        private const string DEFAULT_CHECKPOINT_NAME = "api/PMM00101";
        public PMM00102Model(
            string pcHttpClientName = PMM00100ContextConstant.DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                PMM00100ContextConstant.DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public async Task<List<HoUtilBuildingMappingDTO>> GetBuildingListAsync()
        {
            var loEx = new R_Exception();
            List<HoUtilBuildingMappingDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = PMM00100ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<HoUtilBuildingMappingDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM00102.GetBuildingList),
                    PMM00100ContextConstant.DEFAULT_MODULE, _SendWithContext,
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
        public IAsyncEnumerable<HoUtilBuildingMappingDTO> GetBuildingList()
        {
            throw new NotImplementedException();
        }
    }
}
