using PMT00100COMMON.Interface;
using PMT00100COMMON.UnitList;
using PMT00100COMMON.UtilityDTO;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMT00100MODEL
{
    public class PMT00100ListModel : R_BusinessObjectServiceClientBase<PMT00100DTO>, IPMT00100List
    {

        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT00100List";
        private const string DEFAULT_MODULE = "PM";
        public PMT00100ListModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<PMT00100GenericList<PMT00100BuildingDTO>> GetBuildingAsyncModel()
        {
            var loEx = new R_Exception();
            PMT00100GenericList<PMT00100BuildingDTO> loResult = new PMT00100GenericList<PMT00100BuildingDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT00100BuildingDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00100List.GetBuildingList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<PMT00100GenericList<PMT00100DTO>> GetBuildingUnitAsyncModel()
        {
            var loEx = new R_Exception();
            PMT00100GenericList<PMT00100DTO> loResult = new PMT00100GenericList<PMT00100DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT00100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00100List.GetBuildingUnitList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<PMT00100GenericList<PMT00100AgreementByUnitDTO>> GetAgreementByUnitAsyncModel()
        {
            var loEx = new R_Exception();
            PMT00100GenericList<PMT00100AgreementByUnitDTO> loResult = new PMT00100GenericList<PMT00100AgreementByUnitDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT00100AgreementByUnitDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT00100List.GetAgreementByUnitList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public IAsyncEnumerable<PMT00100AgreementByUnitDTO> GetAgreementByUnitList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT00100BuildingDTO> GetBuildingList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT00100DTO> GetBuildingUnitList()
        {
            throw new NotImplementedException();
        }
    }
}
