using PMT01700COMMON.DTO._1._Other_Unit_List;
using PMT01700COMMON.DTO._2._LOO._1._LOO___Offer_List;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.DTO.Utilities.Print;
using PMT01700COMMON.Interface;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMT01700MODEL
{
    public class PMT01700LOO_OfferListModel : R_BusinessObjectServiceClientBase<PMT01700BlankDTO>, IPMT01700LOO_OfferList
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01700LOO_OfferList";
        private const string DEFAULT_MODULE = "PM";

        public PMT01700LOO_OfferListModel(
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
        { }

        public async Task<PMT01700GenericList<PMT01700LOO_OfferList_OfferListDTO>> GetOfferListAsync()
        {
            var loEx = new R_Exception();
            PMT01700GenericList<PMT01700LOO_OfferList_OfferListDTO> loResult = new PMT01700GenericList<PMT01700LOO_OfferList_OfferListDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01700LOO_OfferList_OfferListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700LOO_OfferList.GetOfferList),
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

        public async Task<PMT01700GenericList<PMT01700LOO_OfferList_UnitListDTO>> GetUnitListAsync()
        {

            var loEx = new R_Exception();
            PMT01700GenericList<PMT01700LOO_OfferList_UnitListDTO> loResult = new PMT01700GenericList<PMT01700LOO_OfferList_UnitListDTO>();
            try
            {
                    R_HTTPClientWrapper.httpClientName = _HttpClientName;
                    var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01700LOO_OfferList_UnitListDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT01700LOO_OfferList.GetUnitList),
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
        public async Task<PMT01700LOO_OfferList_OfferListDTO> UpdateAgreementAsync(PMT01700LOO_ProcessOffer_DTO poEntity)
        {
            var loEx = new R_Exception();
            PMT01700LOO_OfferList_OfferListDTO loResult = new PMT01700LOO_OfferList_OfferListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01700LOO_OfferList_OfferListDTO, PMT01700LOO_ProcessOffer_DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700LOO_OfferList.UpdateAgreement),
                    poEntity,
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

            return loResult!;
        }
        public async Task<PMT01700GenericList<ReportTemplateListDTO>> GetDataGetReportTemplateListAsync(ParamaterGetReportTemplateListDTO poParameter)
        {
            var loEx = new R_Exception();
            PMT01700GenericList<ReportTemplateListDTO> loResult = new PMT01700GenericList<ReportTemplateListDTO>();
            ParamaterGetReportTemplateListDTO loParameter;

            try
            {
                if (!string.IsNullOrEmpty(poParameter.CPROPERTY_ID))
                {
                    loParameter = poParameter;

                    R_HTTPClientWrapper.httpClientName = _HttpClientName;
                    var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<ReportTemplateListDTO, ParamaterGetReportTemplateListDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT01700LOO_OfferList.GetDataGetReportTemplateList),
                        loParameter,
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
        #region Library Not Implement
        public IAsyncEnumerable<PMT01700LOO_OfferList_OfferListDTO> GetOfferList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01700LOO_OfferList_UnitListDTO> GetUnitList()
        {
            throw new NotImplementedException();
        }

        public PMT01700LOO_OfferList_OfferListDTO UpdateAgreement(PMT01700LOO_ProcessOffer_DTO poEntity)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<ReportTemplateListDTO> GetDataGetReportTemplateList(ParamaterGetReportTemplateListDTO poParameter)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
