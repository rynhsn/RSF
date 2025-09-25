using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMT01500Common.Context;
using PMT01500Common.DTO._5._Invoice_Plan;
using PMT01500Common.Interface;
using PMT01500Common.Utilities;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMT01500Model
{
    public class PMT01500InvoicePlanModel : R_BusinessObjectServiceClientBase<PMT01500BlankDTO>, IPMT01500InvoicePlan
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMT01500InvoicePlan";
        private const string DEFAULT_MODULE = "PM";

        public PMT01500InvoicePlanModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            string pcModule = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, pcModule, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<PMT01500InvoicePlanHeaderDTO> GetInvoicePlanHeaderAsync(PMT01500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            var loResult = new PMT01500InvoicePlanHeaderDTO();
            PMT01500GetHeaderParameterDTO? loParam;


            try
            {
                if (!string.IsNullOrEmpty(poParameter.CPROPERTY_ID))
                {
                    loParam = new PMT01500GetHeaderParameterDTO()
                    {
                        CPROPERTY_ID = poParameter.CPROPERTY_ID,
                        CDEPT_CODE = poParameter.CDEPT_CODE,
                        CREF_NO = poParameter.CREF_NO,
                        CTRANS_CODE = poParameter.CTRANS_CODE
                    };

                    R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                    loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT01500InvoicePlanHeaderDTO, PMT01500GetHeaderParameterDTO>(
                        _RequestServiceEndPoint,
                        nameof(IPMT01500InvoicePlan.GetInvoicePlanHeader),
                        loParam,
                        DEFAULT_MODULE,
                        _SendWithContext,
                        _SendWithToken
                    );
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task<List<PMT01500InvoicePlanChargesListDTO>> GetInvoicePlanChargeListAsync(PMT01500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<PMT01500InvoicePlanChargesListDTO>? loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(PMT01500GetHeaderParameterContextConstantDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT01500GetHeaderParameterContextConstantDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMT01500GetHeaderParameterContextConstantDTO.CREF_NO, poParameter.CREF_NO);
                R_FrontContext.R_SetStreamingContext(PMT01500GetHeaderParameterContextConstantDTO.CTRANS_CODE, poParameter.CTRANS_CODE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01500InvoicePlanChargesListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01500InvoicePlan.GetInvoicePlanChargeList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

#pragma warning disable CS8603 // Possible null reference return.
            return loResult;
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<List<PMT01500InvoicePlanListDTO>> GetInvoicePlanListAsync(PMT01500GetHeaderParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            List<PMT01500InvoicePlanListDTO>? loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(PMT01500GetHeaderParameterContextConstantDTO.CPROPERTY_ID, poParameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMT01500GetHeaderParameterContextConstantDTO.CDEPT_CODE, poParameter.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMT01500GetHeaderParameterContextConstantDTO.CREF_NO, poParameter.CREF_NO);
                R_FrontContext.R_SetStreamingContext(PMT01500GetHeaderParameterContextConstantDTO.CTRANS_CODE, poParameter.CTRANS_CODE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT01500InvoicePlanListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT01500InvoicePlan.GetInvoicePlanList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

#pragma warning disable CS8603 // Possible null reference return.
            return loResult;
#pragma warning restore CS8603 // Possible null reference return.
        }

        #region Not Used!
        public IAsyncEnumerable<PMT01500InvoicePlanChargesListDTO> GetInvoicePlanChargeList()
        {
            throw new NotImplementedException();
        }

        public PMT01500InvoicePlanHeaderDTO GetInvoicePlanHeader(PMT01500GetHeaderParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT01500InvoicePlanListDTO> GetInvoicePlanList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}