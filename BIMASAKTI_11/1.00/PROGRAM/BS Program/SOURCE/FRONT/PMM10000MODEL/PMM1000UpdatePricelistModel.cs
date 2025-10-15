using PMM10000COMMON.Call_Type_Pricelist;
using PMM10000COMMON.Interface;
using PMM10000COMMON.SLA_Call_Type;
using PMM10000COMMON.SLA_Category;
using PMM10000COMMON.UtilityDTO;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMM10000MODEL
{
    public class PMM1000UpdatePricelistModel : R_BusinessObjectServiceClientBase<AssignUnassignPricelistDTO>, IPMM10000CallTypePricelist
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMM10000CallTypePricelist";
        private const string DEFAULT_MODULE = "PM";
        public PMM1000UpdatePricelistModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<AssignUnassignPricelistDTO> AssignPricelistAsyncModel(PMM10000DbParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            AssignUnassignPricelistDTO loResult = new AssignUnassignPricelistDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<AssignUnassignPricelistDTO, PMM10000DbParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM10000CallTypePricelist.AssignPricelist),
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
            return loResult;
        }
        public async Task<AssignUnassignPricelistDTO> UnassignPricelistAsyncModel(PMM10000DbParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            AssignUnassignPricelistDTO loResult = new AssignUnassignPricelistDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<AssignUnassignPricelistDTO, PMM10000DbParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM10000CallTypePricelist.UnassignPricelist),
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
            return loResult;
        }

        public AssignUnassignPricelistDTO AssignPricelist(PMM10000DbParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public AssignUnassignPricelistDTO UnassignPricelist(PMM10000DbParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
    }
}
