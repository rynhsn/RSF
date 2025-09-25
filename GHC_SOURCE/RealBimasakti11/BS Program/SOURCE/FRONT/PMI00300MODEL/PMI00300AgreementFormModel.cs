using PMI00300COMMON.DTO;
using PMI00300COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace PMI00300MODEL
{
    public class PMI00300AgreementFormModel: R_BusinessObjectServiceClientBase<PMI00300GetAgreementFormListParameterDTO>, IPMI00300AgreementForm
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMI00300AgreementForm";
        private const string DEFAULT_MODULE = "PM";

        public PMI00300AgreementFormModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region GET AGREEMENT FORM LIST
        public IAsyncEnumerable<PMI00300GetAgreementFormListDTO> GetAgreementFormList()
        {
            throw new NotImplementedException();
        }

        public async Task<PMI00300GetAgreementFormListResultDTO> GetAgreementFormListAsync()
        {
            R_Exception loEx = new R_Exception();
            List<PMI00300GetAgreementFormListDTO> loResult = null;
            PMI00300GetAgreementFormListResultDTO loRtn = new PMI00300GetAgreementFormListResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMI00300GetAgreementFormListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMI00300AgreementForm.GetAgreementFormList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                    );
                loRtn.Data = loResult;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        #endregion

        #region GET PERIOD YEAR RANGE
        public PMI00300GetPeriodYearRangeResultDTO GetPeriodYearRange()
        {
            throw new NotImplementedException();
        }
        public async Task<PMI00300GetPeriodYearRangeResultDTO> GetPeriodYearRangeAsync()
        {
            R_Exception loEx = new R_Exception();
            PMI00300GetPeriodYearRangeResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<PMI00300GetPeriodYearRangeResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMI00300AgreementForm.GetPeriodYearRange),
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
            return loRtn;
        }

        #endregion

        #region GET HAND OVER CHECKLIST
        public IAsyncEnumerable<PMI00300HandOverChecklistDTO> GetList_HandOverChecklist(PMI00300HandOverChecklistParamDTO poParam)
        {
            throw new NotImplementedException();
        }
        
        public async Task<List<PMI00300HandOverChecklistDTO>> GetList_HandOverChecklistAsync(PMI00300HandOverChecklistParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            List<PMI00300HandOverChecklistDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMI00300HandOverChecklistDTO, PMI00300HandOverChecklistParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMI00300AgreementForm.GetList_HandOverChecklist),
                    poParam,
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
            return loResult;
        }

        #endregion
    }
}
