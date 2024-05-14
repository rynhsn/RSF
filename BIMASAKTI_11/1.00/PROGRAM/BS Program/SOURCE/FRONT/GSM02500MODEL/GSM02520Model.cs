using GSM02500COMMON;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02510;
using GSM02500COMMON.DTOs.GSM02520;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500MODEL
{
    public class GSM02520Model : R_BusinessObjectServiceClientBase<GSM02520ParameterDTO>, IGSM02520
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM02520";
        private const string DEFAULT_MODULE = "gs";

        public GSM02520Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public TemplateFloorDTO DownloadTemplateFloor()
        {
            throw new NotImplementedException();
        }

        public async Task<TemplateFloorDTO> DownloadTemplateFloorAsync()
        {
            R_Exception loEx = new R_Exception();
            TemplateFloorDTO loResult = new TemplateFloorDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<TemplateFloorDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02520.DownloadTemplateFloor),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public IAsyncEnumerable<GSM02520DTO> GetFloorList()
        {
            throw new NotImplementedException();
        }
        public async Task<GSM02520ListDTO> GetFloorListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GSM02520DTO> loResult = null;
            GSM02520ListDTO loRtn = new GSM02520ListDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02520DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02520.GetFloorList),
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

        public GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_FLOORMethod(GSM02500ActiveInactiveParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task RSP_GS_ACTIVE_INACTIVE_FLOORMethodAsync(GSM02500ActiveInactiveParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            GSM02500ActiveInactiveResultDTO loRtn = new GSM02500ActiveInactiveResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GSM02500ActiveInactiveResultDTO, GSM02500ActiveInactiveParameterDTO> (
                    _RequestServiceEndPoint,
                    nameof(IGSM02520.RSP_GS_ACTIVE_INACTIVE_FLOORMethod),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
    }
}
