using PMT01700COMMON.DTO._2._LOO._1._LOO___Offer_List;
using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Unit___Charges;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.Interface;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMT01700MODEL
{
    public class PMT01700LOO_UnitCharges_UnitUtilitiesModel : R_BusinessObjectServiceClientBase<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO>, IPMT01700LOO_UnitUtilities_UnitUtilities
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01700LOO_UnitUtilities_UnitUtilies";
        private const string DEFAULT_MODULE = "PM";

        public PMT01700LOO_UnitCharges_UnitUtilitiesModel(
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


        #region Just Implements
        public PMT01700LOO_UnitCharges_UnitCharges_AgreementUnitHeaderDTO GetUnitChargesHeader(PMT01700LOO_UnitUtilities_ParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO> GetUnitInfoList()
        {
            throw new NotImplementedException();
        }

        #endregion

        public async Task<PMT01700LOO_UnitCharges_UnitCharges_AgreementUnitHeaderDTO> GetUnitChargesHeaderAsync(PMT01700LOO_UnitUtilities_ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            PMT01700LOO_UnitCharges_UnitCharges_AgreementUnitHeaderDTO loResult = new PMT01700LOO_UnitCharges_UnitCharges_AgreementUnitHeaderDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01700LOO_UnitCharges_UnitCharges_AgreementUnitHeaderDTO, PMT01700LOO_UnitUtilities_ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700LOO_UnitUtilities_UnitUtilities.GetUnitChargesHeader),
                    poParameter,
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
        public async Task<PMT01700GenericList<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO>> GetUnitInfoListAsync()
        {
            var loEx = new R_Exception();
            PMT01700GenericList<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO> loResult = new PMT01700GenericList<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01700LOO_UnitUtilities_UnitUtilities_AgreementUnitInfoListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01700LOO_UnitUtilities_UnitUtilities.GetUnitInfoList),
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
   
    }
}
