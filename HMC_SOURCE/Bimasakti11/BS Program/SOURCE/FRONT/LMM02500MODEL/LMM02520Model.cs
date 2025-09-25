using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LMM02500Common;
using LMM02500Common.DTO;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace LMM02500Model
{
    public class LMM02520Model : R_BusinessObjectServiceClientBase<LMM02520DTO>, ILMM02520
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMM02520";
        private const string DEFAULT_MODULE = "LM";

        public LMM02520Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<LMM02520GridDTO>> GetAllTenantGroupListStreamAsync(string pcCPROPERTY_ID,
            string pcCTENANT_GROUP_ID)
        {
            var loEx = new R_Exception();
            var loResult = new List<LMM02520GridDTO>();


            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantDetailLMM02500.CPROPERTY_ID, pcCPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantDetailLMM02500.CTENANT_GROUP_ID,
                    pcCTENANT_GROUP_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM02520GridDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02520.GetAllTenantGroupListStream),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task SaveMoveTenantGroupCategoryAsync(ObjectParameterLMM02500MoveTenantGroup poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                await R_HTTPClientWrapper.R_APIRequestObject<SaveMoveTenantGroupResultDTO, ObjectParameterLMM02500MoveTenantGroup>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02520.SaveMoveTenantGroupCategory),
                    poParameter,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region NotUsed

        public IAsyncEnumerable<LMM02520GridDTO> GetAllTenantGroupListStream()
        {
            throw new NotImplementedException();
        }

        public SaveMoveTenantGroupResultDTO SaveMoveTenantGroupCategory(ObjectParameterLMM02500MoveTenantGroup poParameter)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}