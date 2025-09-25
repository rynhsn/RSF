using GSM02500COMMON;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs.GSM02503;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500MODEL
{
    public class GSM02503UnitTypeModel : R_BusinessObjectServiceClientBase<GSM02503UnitTypeParameterDTO>, IGSM02503UnitType
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM02503UnitType";
        private const string DEFAULT_MODULE = "gs";

        public GSM02503UnitTypeModel(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public TemplateUnitTypeDTO DownloadTemplateUnitType()
        {
            throw new NotImplementedException();
        }

        public async Task<TemplateUnitTypeDTO> DownloadTemplateUnitTypeAsync()
        {
            R_Exception loEx = new R_Exception();
            TemplateUnitTypeDTO loResult = new TemplateUnitTypeDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<TemplateUnitTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02503UnitType.DownloadTemplateUnitType),
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


        public IAsyncEnumerable<GSM02503UnitTypeDTO> GetUnitTypeList()
        {
            throw new NotImplementedException();
        }

        public async Task<GSM02503UnitTypeListDTO> GetUnitTypeListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GSM02503UnitTypeDTO> loResult = null;
            GSM02503UnitTypeListDTO loRtn = new GSM02503UnitTypeListDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02503UnitTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02503UnitType.GetUnitTypeList),
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

        public GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_UNIT_TYPEMethod(GSM02500ActiveInactiveParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task RSP_GS_ACTIVE_INACTIVE_UNIT_TYPEMethodAsync(GSM02500ActiveInactiveParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            GSM02500ActiveInactiveResultDTO loRtn = new GSM02500ActiveInactiveResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GSM02500ActiveInactiveResultDTO, GSM02500ActiveInactiveParameterDTO> (
                    _RequestServiceEndPoint,
                    nameof(IGSM02503UnitType.RSP_GS_ACTIVE_INACTIVE_UNIT_TYPEMethod),
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
