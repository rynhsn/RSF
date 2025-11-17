using PMR00100Common;
using PMR00100Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMR00100Model
{
    public class PMR00100Model : R_BusinessObjectServiceClientBase<PMR00100DTO>, IPMR00100
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMR00100";
        private const string DEFAULT_MODULE = "PM";

        public PMR00100Model() :
            base(DEFAULT_HTTP_NAME,DEFAULT_SERVICEPOINT_NAME,DEFAULT_MODULE,true,true)
        {
        }

        public async Task<PeriodYearRangeDTO> GetPeriodYearAsync()
        {
            var loEx = new R_Exception();
            PeriodYearRangeDTO loResult = new PeriodYearRangeDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PeriodYearRangeDTO>(
                   _RequestServiceEndPoint,
                   nameof(IPMR00100.GetPeriodYear),
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
        public async Task<PeriodDT_DataDTO> GetPeriodDTListAsync(PMR00100ParamDTO poData)
        {
            var loEx = new R_Exception();
            PeriodDT_DataDTO loResult = new PeriodDT_DataDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PeriodDT_DataDTO,PMR00100ParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR00100.GetPeriodDTList),
                    poData,
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
        public async Task<PropertyDataDTO> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            PropertyDataDTO loResult = new PropertyDataDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var LoTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR00100.GetPropertyList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = LoTempResult;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<LOOStatusDataDTO> GetStatusListAsync()
        {
            var loEx = new R_Exception();
            LOOStatusDataDTO loResult = new LOOStatusDataDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<LOOStatusDataDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMR00100.GetLOOStatusList),
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

        public async Task<PMR00100DataDTO> GetLOOPrintListAsync(PrintParamDTO poData)
        {
            var loEx = new R_Exception();
            PMR00100DataDTO loResult = new PMR00100DataDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMR00100DTO,PrintParamDTO>(
                      _RequestServiceEndPoint,
                      nameof(IPMR00100.GetLOOPrintList),
                      poData,
                      DEFAULT_MODULE,
                      _SendWithContext,
                      _SendWithToken);
                loResult.Data = loTemp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        #region Not Implemented
        public PeriodDT_DataDTO GetPeriodDTList(PMR00100ParamDTO poData)
        {
            throw new NotImplementedException();
        }
        public PeriodYearRangeDTO GetPeriodYear()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public LOOStatusDataDTO GetLOOStatusList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMR00100DTO> GetLOOPrintList(PrintParamDTO poData)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
