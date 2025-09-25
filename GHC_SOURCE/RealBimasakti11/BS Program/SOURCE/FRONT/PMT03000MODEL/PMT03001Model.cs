using PMT03000COMMON;
using PMT03000COMMON.DTO_s;
using PMT03000COMMON.DTO_s.Helper;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMT03000MODEL
{
    public class PMT03001Model : R_BusinessObjectServiceClientBase<TenantUnitFacilityDTO>, IPMT03001
    {
        private const string DEFAULT_CHECKPOINT_NAME = "api/PMT03001";
        public PMT03001Model(
            string pcHttpClientName = PMT03000ContextConstant.DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                PMT03000ContextConstant.DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        //real method
        public async Task<List<UnitTypeCtgFacilityDTO>> GetList_UnitTypeCtgFacilityAsync()
        {
            var loEx = new R_Exception();
            List<UnitTypeCtgFacilityDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = PMT03000ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UnitTypeCtgFacilityDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT03001.GetList_UnitTypeCtgFacility),
                    PMT03000ContextConstant.DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<List<TenantUnitFacilityDTO>> GetList_TenantUnitFacilityAsync()
        {
            var loEx = new R_Exception();
            List<TenantUnitFacilityDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = PMT03000ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TenantUnitFacilityDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT03001.GetList_TenantUnitFacility),
                    PMT03000ContextConstant.DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task ActiveInactive_TenantUnitFacilityAsync(TenantUnitFacilityDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                R_HTTPClientWrapper.httpClientName = PMT03000ContextConstant.DEFAULT_HTTP_NAME;
                await R_HTTPClientWrapper.R_APIRequestObject<GeneralAPIResultDTO<TenantUnitFacilityDTO>, TenantUnitFacilityDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT03001.ActiveInactive_TenantUnitFacility),
                    poParam,
                    PMT03000ContextConstant.DEFAULT_MODULE
                    , _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task<FacilityQtyDTO>GetRecord_FacilityQtyAsync(TenantUnitFacilityDTO poParam)
        {
            var loEx = new R_Exception();
            var loRtn = new FacilityQtyDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = PMT03000ContextConstant.DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GeneralAPIResultDTO<FacilityQtyDTO>, TenantUnitFacilityDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT03001.GetRecord_FacilityQty),
                    poParam,
                    PMT03000ContextConstant.DEFAULT_MODULE
                    , _SendWithContext,
                    _SendWithToken);
                loRtn = loTempResult.data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }


        //implement only
        public IAsyncEnumerable<UnitTypeCtgFacilityDTO> GetList_UnitTypeCtgFacility()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<TenantUnitFacilityDTO> GetList_TenantUnitFacility()
        {
            throw new NotImplementedException();
        }

        public GeneralAPIResultDTO<TenantUnitFacilityDTO> ActiveInactive_TenantUnitFacility(TenantUnitFacilityDTO poParam)
        {
            throw new NotImplementedException();
        }

        public GeneralAPIResultDTO<FacilityQtyDTO> GetRecord_FacilityQty(TenantUnitFacilityDTO poParam)
        {
            throw new NotImplementedException();
        }
    }
}
