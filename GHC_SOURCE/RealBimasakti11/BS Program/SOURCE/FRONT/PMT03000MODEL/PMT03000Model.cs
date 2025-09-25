using PMT03000COMMON;
using PMT03000COMMON.DTO_s;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMT03000MODEL
{
    public class PMT03000Model : R_BusinessObjectServiceClientBase<BuildingDTO>, IPMT03000
    {
        private const string DEFAULT_CHECKPOINT_NAME = "api/PMT03000";
        public PMT03000Model(
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

        public async Task<List<BuildingDTO>> GetList_BuildingAsync()
        {
            var loEx = new R_Exception();
            List<BuildingDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = PMT03000ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<BuildingDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT03000.GetList_Building),
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
        public async Task<List<BuildingUnitDTO>> GetList_BuildingUnitAsync()
        {
            var loEx = new R_Exception();
            List<BuildingUnitDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = PMT03000ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<BuildingUnitDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT03000.GetList_BuildingUnit),
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
        public async Task<List<TransByUnitDTO>> GetList_TransByUnitAsync()
        {
            var loEx = new R_Exception();
            List<TransByUnitDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = PMT03000ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TransByUnitDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT03000.GetList_TransByUnit),
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
        public async Task<List<PropertyDTO>> GetList_PropertyAsync()
        {
            var loEx = new R_Exception();
            List<PropertyDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = PMT03000ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT03000.GetPropertyList),
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



        public IAsyncEnumerable<BuildingDTO> GetList_Building()
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<BuildingUnitDTO> GetList_BuildingUnit()
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<TransByUnitDTO> GetList_TransByUnit()
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            throw new System.NotImplementedException();
        }
    }
}
