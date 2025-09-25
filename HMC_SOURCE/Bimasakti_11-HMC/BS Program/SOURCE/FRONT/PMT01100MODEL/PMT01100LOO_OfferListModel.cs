using PMT01100Common.Context._1._Unit_List;
using PMT01100Common.DTO._1._Unit_List;
using PMT01100Common.DTO._2._LOO._1._LOO___Offer_List;
using PMT01100Common.Interface;
using PMT01100Common.Utilities;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using PMT01100Common.Utilities.Db;
using PMT01100Common.Context._2._LOO._1._Offer_List;

namespace PMT01100Model
{
    public class PMT01100LOO_OfferListModel : R_BusinessObjectServiceClientBase<PMT01100BlankDTO>, IPMT01100LOO_OfferList
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01100LOO_OfferList";
        private const string DEFAULT_MODULE = "PM";

        public PMT01100LOO_OfferListModel(
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

        public async Task<PMT01100UtilitiesListDTO<PMT01100LOO_OfferList_OfferListDTO>> GetOfferListAsync(PMT01100UtilitiesParameterOfferListDTO poParameter)
        {

            var loEx = new R_Exception();
            PMT01100UtilitiesListDTO<PMT01100LOO_OfferList_OfferListDTO> loResult = new PMT01100UtilitiesListDTO<PMT01100LOO_OfferList_OfferListDTO>();
            try
            {
                if (poParameter != null)
                {

                    R_FrontContext.R_SetStreamingContext(PMT01100UtilitiesParameterOfferListContextDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                    R_FrontContext.R_SetStreamingContext(PMT01100UtilitiesParameterOfferListContextDTO.CTRANS_CODE, poParameter.CTRANS_CODE);

                    R_HTTPClientWrapper.httpClientName = _HttpClientName;
                    var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01100LOO_OfferList_OfferListDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT01100LOO_OfferList.GetOfferList),
                        DEFAULT_MODULE,
                        _SendWithContext,
                        _SendWithToken
                    );
                    loResult.Data = loTempResult;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }

        public async Task<PMT01100UtilitiesListDTO<PMT01100LOO_OfferList_UnitListDTO>> GetUnitListAsync(PMT01100UtilitiesParameterGetUnitListDTO poParameter)
        {

            var loEx = new R_Exception();
            PMT01100UtilitiesListDTO<PMT01100LOO_OfferList_UnitListDTO> loResult = new PMT01100UtilitiesListDTO<PMT01100LOO_OfferList_UnitListDTO>();
            try
            {
                if (poParameter != null)
                {

                    R_FrontContext.R_SetStreamingContext(PMT01100UtilitiesParameterGetUnitListContextDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                    R_FrontContext.R_SetStreamingContext(PMT01100UtilitiesParameterGetUnitListContextDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                    R_FrontContext.R_SetStreamingContext(PMT01100UtilitiesParameterGetUnitListContextDTO.CTRANS_CODE, poParameter.CTRANS_CODE);
                    R_FrontContext.R_SetStreamingContext(PMT01100UtilitiesParameterGetUnitListContextDTO.CREF_NO, poParameter.CREF_NO);

                    R_HTTPClientWrapper.httpClientName = _HttpClientName;
                    var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01100LOO_OfferList_UnitListDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT01100LOO_OfferList.GetUnitList),
                        DEFAULT_MODULE,
                        _SendWithContext,
                        _SendWithToken
                    );

                    loResult.Data = loTempResult;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }


        #region Not Used

        public IAsyncEnumerable<PMT01100LOO_OfferList_OfferListDTO> GetOfferList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01100LOO_OfferList_UnitListDTO> GetUnitList()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
