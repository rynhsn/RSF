using PMT01100Common.Context._1._Unit_List;
using PMT01100Common.DTO._1._Unit_List;
using PMT01100Common.Interface;
using PMT01100Common.Utilities;
using PMT01100Common.Utilities.Db;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace PMT01100Model
{
    public class PMT01100UnitListModel : R_BusinessObjectServiceClientBase<PMT01100BlankDTO>, IPMT01100UnitList
    {

        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01100UnitList";
        private const string DEFAULT_MODULE = "PM";

        public PMT01100UnitListModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                pcModule,
                plSendWithContext,
                plSendWithToken)
        {
        }


        public async Task<PMT01100UtilitiesListDTO<PMT01100UnitList_BuildingDTO>> GetBuildingListAsync(PMT01100UtilitiesParameterPropertyDTO poParameter)
        {
            var loEx = new R_Exception();
            PMT01100UtilitiesListDTO<PMT01100UnitList_BuildingDTO> loResult = new PMT01100UtilitiesListDTO<PMT01100UnitList_BuildingDTO>();

            try
            {
                R_FrontContext.R_SetStreamingContext(PMT01100UnitList_BuildingContextDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01100UnitList_BuildingDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01100UnitList.GetBuildingList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();


            return loResult!;
        }

        public async Task<PMT01100UtilitiesListDTO<PMT01100UnitList_UnitListDTO>> GetUnitListAsync(PMT01100UtilitiesParameterPropertyandBuildingDTO poParameter)
        {
            var loEx = new R_Exception();
            PMT01100UtilitiesListDTO<PMT01100UnitList_UnitListDTO> loResult = new PMT01100UtilitiesListDTO<PMT01100UnitList_UnitListDTO>(); ;
            try
            {

                R_FrontContext.R_SetStreamingContext(PMT01100UnitList_UnitListContextDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT01100UnitList_UnitListContextDTO.CBUILDING_ID, poParameter.CBUILDING_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01100UnitList_UnitListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01100UnitList.GetUnitList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }

        public async Task<PMT01100UtilitiesListDTO<PMT01100PropertyListDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            PMT01100UtilitiesListDTO<PMT01100PropertyListDTO> loResult = new PMT01100UtilitiesListDTO<PMT01100PropertyListDTO>(); ;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01100PropertyListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01100UnitList.GetPropertyList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }

        #region Not Used!

        public IAsyncEnumerable<PMT01100UnitList_BuildingDTO> GetBuildingList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01100PropertyListDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01100UnitList_UnitListDTO> GetUnitList()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
