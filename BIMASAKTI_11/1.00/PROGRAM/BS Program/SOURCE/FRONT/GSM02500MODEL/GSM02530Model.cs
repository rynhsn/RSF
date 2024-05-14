using GSM02500COMMON;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02520;
using GSM02500COMMON.DTOs.GSM02530;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500MODEL
{
    public class GSM02530Model : R_BusinessObjectServiceClientBase<GSM02530ParameterDTO>, IGSM02530
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM02530";
        private const string DEFAULT_MODULE = "gs";

        public GSM02530Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public TemplateUnitDTO DownloadTemplateUnit()
        {
            throw new NotImplementedException();
        }

        public async Task<TemplateUnitDTO> DownloadTemplateUnitAsync()
        {
            R_Exception loEx = new R_Exception();
            TemplateUnitDTO loResult = new TemplateUnitDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<TemplateUnitDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02530.DownloadTemplateUnit),
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

        public IAsyncEnumerable<GSM02530DTO> GetUnitInfoList()
        {
            throw new NotImplementedException();
        }
        public async Task<GSM02530ResultDTO> GetUnitInfoListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GSM02530DTO> loResult = null;
            GSM02530ResultDTO loRtn = new GSM02530ResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02530DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02530.GetUnitInfoList),
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
        public GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_BUILDING_UNITMethod(GSM02500ActiveInactiveParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task RSP_GS_ACTIVE_INACTIVE_BUILDING_UNITMethodAsync(GSM02500ActiveInactiveParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            GSM02500ActiveInactiveResultDTO loRtn = new GSM02500ActiveInactiveResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GSM02500ActiveInactiveResultDTO, GSM02500ActiveInactiveParameterDTO> (
                    _RequestServiceEndPoint,
                    nameof(IGSM02530.RSP_GS_ACTIVE_INACTIVE_BUILDING_UNITMethod),
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
