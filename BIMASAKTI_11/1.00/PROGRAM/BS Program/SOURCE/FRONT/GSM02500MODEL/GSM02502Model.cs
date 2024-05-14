using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using GSM02500COMMON.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs.GSM02520;

namespace GSM02500MODEL
{
    public class GSM02502Model : R_BusinessObjectServiceClientBase<GSM02502ParameterDTO>, IGSM02502
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM02502";
        private const string DEFAULT_MODULE = "gs";

        public GSM02502Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public TemplateUnitTypeCategoryDTO DownloadTemplateUnitTypeCategory()
        {
            throw new NotImplementedException();
        }

        public async Task<TemplateUnitTypeCategoryDTO> DownloadTemplateUnitTypeCategoryAsync()
        {
            R_Exception loEx = new R_Exception();
            TemplateUnitTypeCategoryDTO loResult = new TemplateUnitTypeCategoryDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<TemplateUnitTypeCategoryDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02502.DownloadTemplateUnitTypeCategory),
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

        public IAsyncEnumerable<PropertyTypeDTO> GetPropertyTypeList()
        {
            throw new NotImplementedException();
        }

        public async Task<PropertyTypeResultDTO> GetPropertyTypeListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<PropertyTypeDTO> loResult = null;
            PropertyTypeResultDTO loRtn = new PropertyTypeResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02502.GetPropertyTypeList),
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

        public IAsyncEnumerable<GSM02502DTO> GetUnitTypeCategoryList()
        {
            throw new NotImplementedException();
        }

        public async Task<GSM02502ListDTO> GetUnitTypeCategoryListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GSM02502DTO> loResult = null;
            GSM02502ListDTO loRtn = new GSM02502ListDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02502DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02502.GetUnitTypeCategoryList),
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

        public GSM02500ActiveInactiveResultDTO RSP_GS_ACTIVE_INACTIVE_UNIT_TYPE_CATEGORYMethod(GSM02500ActiveInactiveParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task RSP_GS_ACTIVE_INACTIVE_UNIT_TYPE_CATEGORYMethodAsync(GSM02500ActiveInactiveParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            GSM02500ActiveInactiveResultDTO loRtn = new GSM02500ActiveInactiveResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GSM02500ActiveInactiveResultDTO, GSM02500ActiveInactiveParameterDTO> (
                    _RequestServiceEndPoint,
                    nameof(IGSM02502.RSP_GS_ACTIVE_INACTIVE_UNIT_TYPE_CATEGORYMethod),
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
