using PMM09000COMMON.Amortization_Entry_DTO;
using PMM09000COMMON.Interface;
using PMM09000COMMON.UtiliyDTO;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMM09000MODEL
{
    public class PMM09000CRUDModel : R_BusinessObjectServiceClientBase<PMM09000EntryHeaderDetailDTO>, IPMM09000CRUDBase
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMM09000";
        private const string DEFAULT_MODULE = "PM";
        public PMM09000CRUDModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<UpdateStatusDTO> UpdateAmortizationStatusAsyncModel(PMM09000DbParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            UpdateStatusDTO loResult = new UpdateStatusDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<UpdateStatusDTO, PMM09000DbParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM09000CRUDBase.UpdateAmortizationStatus),
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

        #region ImplementLibrary
        public UpdateStatusDTO UpdateAmortizationStatus(PMM09000DbParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
