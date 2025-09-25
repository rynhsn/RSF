using GSM02500COMMON.DTOs.GSM02502Charge;
using GSM02500COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using GSM02500COMMON.DTOs.GSM02502Facility;
using R_CommonFrontBackAPI;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs.GSM02530;

namespace GSM02500MODEL
{
    public class GSM02502FacilityModel : R_BusinessObjectServiceClientBase<GSM02502FacilityParameterDTO>, IGSM02502Facility
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM02502Facility";
        private const string DEFAULT_MODULE = "gs";

        public GSM02502FacilityModel(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GSM02502FacilityDTO> GetFacilityList()
        {
            throw new NotImplementedException();
        }

        public async Task<GSM02502FacilityResultDTO> GetFacilityListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GSM02502FacilityDTO> loResult = null;
            GSM02502FacilityResultDTO loRtn = new GSM02502FacilityResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02502FacilityDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02502Facility.GetFacilityList),
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

        public IAsyncEnumerable<GSM02502FacilityTypeDTO> GetFacilityTypeList()
        {
            throw new NotImplementedException();
        }
        public async Task<GSM02502FacilityTypeResultDTO> GetFacilityTypeListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GSM02502FacilityTypeDTO> loResult = null;
            GSM02502FacilityTypeResultDTO loRtn = new GSM02502FacilityTypeResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02502FacilityTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02502Facility.GetFacilityTypeList),
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
