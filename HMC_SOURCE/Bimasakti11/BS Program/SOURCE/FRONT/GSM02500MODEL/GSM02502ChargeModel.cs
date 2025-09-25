using GSM02500COMMON;
using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs.GSM02502Charge;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500MODEL
{
    public class GSM02502ChargeModel : R_BusinessObjectServiceClientBase<GSM02502ChargeParameterDTO>, IGSM02502Charge
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM02502Charge";
        private const string DEFAULT_MODULE = "gs";

        public GSM02502ChargeModel(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GSM02502ChargeComboboxDTO> GetChargeComboBoxList()
        {
            throw new NotImplementedException();
        }

        public async Task<GSM02502ChargeComboboxResultDTO> GetChargeComboBoxListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GSM02502ChargeComboboxDTO> loResult = null;
            GSM02502ChargeComboboxResultDTO loRtn = new GSM02502ChargeComboboxResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02502ChargeComboboxDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02502Charge.GetChargeComboBoxList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public IAsyncEnumerable<GSM02502ChargeDTO> GetChargeList()
        {
            throw new NotImplementedException();
        }

        public async Task<GSM02502ChargeResultDTO> GetChargeListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GSM02502ChargeDTO> loResult = null;
            GSM02502ChargeResultDTO loRtn = new GSM02502ChargeResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02502ChargeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02502Charge.GetChargeList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn.Data = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}
