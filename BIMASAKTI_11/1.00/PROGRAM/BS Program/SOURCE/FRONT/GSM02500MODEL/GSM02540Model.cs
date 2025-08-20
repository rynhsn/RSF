using GSM02500COMMON;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02503;
using GSM02500COMMON.DTOs.GSM02530;
using GSM02500COMMON.DTOs.GSM02540;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500MODEL
{
    public class GSM02540Model : R_BusinessObjectServiceClientBase<GSM02540ParameterDTO>, IGSM02540
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM02540";
        private const string DEFAULT_MODULE = "gs";

        public GSM02540Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public TemplateOtherUnitTypeDTO DownloadTemplateOtherUnitType()
        {
            throw new NotImplementedException();
        }

        public async Task<TemplateOtherUnitTypeDTO> DownloadTemplateOtherUnitTypeAsync()
        {
            R_Exception loEx = new R_Exception();
            TemplateOtherUnitTypeDTO loResult = new TemplateOtherUnitTypeDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<TemplateOtherUnitTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02540.DownloadTemplateOtherUnitType),
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

        public IAsyncEnumerable<GSM02540DTO> GetOtherUnitTypeList()
        {
            throw new NotImplementedException();
        }

        public async Task<GSM02540ResultDTO> GetOtherUnitTypeListStreamAsync()
        {
            {
                R_Exception loEx = new R_Exception();
                List<GSM02540DTO> loResult = null;
                GSM02540ResultDTO loRtn = new GSM02540ResultDTO();

                try
                {
                    R_HTTPClientWrapper.httpClientName = _HttpClientName;

                    loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02540DTO>(
                        _RequestServiceEndPoint,
                        nameof(IGSM02540.GetOtherUnitTypeList),
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

        public IAsyncEnumerable<GetPropertyTypeDTO> GetPropertyTypeList()
        {
            throw new NotImplementedException();
        }

        public async Task<GetPropertyTypeResultDTO> GetPropertyTypeListStreamAsync()
        {
            {
                R_Exception loEx = new R_Exception();
                List<GetPropertyTypeDTO> loResult = null;
                GetPropertyTypeResultDTO loRtn = new GetPropertyTypeResultDTO();

                try
                {
                    R_HTTPClientWrapper.httpClientName = _HttpClientName;

                    loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetPropertyTypeDTO>(
                        _RequestServiceEndPoint,
                        nameof(IGSM02540.GetPropertyTypeList),
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

        public GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_OTHER_UNIT_TYPEMethod(GSM02500ActiveInactiveParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task RSP_GS_ACTIVE_INACTIVE_OTHER_UNIT_TYPEMethodAsync(GSM02500ActiveInactiveParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            GSM02500ActiveInactiveResultDTO loRtn = new GSM02500ActiveInactiveResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GSM02500ActiveInactiveResultDTO, GSM02500ActiveInactiveParameterDTO> (
                    _RequestServiceEndPoint,
                    nameof(IGSM02540.RSP_GS_ACTIVE_INACTIVE_OTHER_UNIT_TYPEMethod),
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
