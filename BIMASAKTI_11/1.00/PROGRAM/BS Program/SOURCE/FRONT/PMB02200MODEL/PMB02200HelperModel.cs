using PMB02200COMMON.DTO_s;
using PMB02200COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using R_APIClient;

namespace PMB02200MODEL
{
    public class PMB02200HelperModel : R_BusinessObjectServiceClientBase<UtilityChargesDTO>, IPMB02200General
    {

        private const string DEFAULT_CHECKPOINT_NAME = "api/PMB02200Helper";

        public PMB02200HelperModel(
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

        public GeneralAPIResultBaseDTO<PMSystemParamDTO> GetPMSysParam()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GeneralTypeDTO> GetUtilityTypeList()
        {
            throw new NotImplementedException();
        }

        public async Task<PMSystemParamDTO> GetPMSysParamAsync() {

            var loEx = new R_Exception();
            PMSystemParamDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = PMB02200ContextConstant.DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GeneralAPIResultBaseDTO<PMSystemParamDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IPMB02200General.GetPMSysParam),
                    PMB02200ContextConstant.DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<List<PropertyDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = PMB02200ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMB02200General.GetPropertyList),
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

        public async Task<List<GeneralTypeDTO>> GetUtilityTypeListAsync()
        {
            var loEx = new R_Exception();
            List<GeneralTypeDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = PMB02200ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GeneralTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMB02200General.GetUtilityTypeList),
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
