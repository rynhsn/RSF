using PMT02100COMMON.DTOs.PMT02120;
using PMT02100COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using PMT02100COMMON.DTOs.Utility;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace PMT02100MODEL
{
    public class PMT02120Model : R_BusinessObjectServiceClientBase<PMT02120EmployeeListParameterDTO>, IPMT02120
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMT02120";
        private const string DEFAULT_MODULE = "PM";

        public PMT02120Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<PMT02120EmployeeListResultDTO> GetEmployeeListStreamAsync()
        {
            var loEx = new R_Exception();
            PMT02120EmployeeListResultDTO loResult = new PMT02120EmployeeListResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02120EmployeeListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02120.GetEmployeeList),
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

        public async Task<PMT02120RescheduleListResultDTO> GetRescheduleListStreamAsync()
        {
            var loEx = new R_Exception();
            PMT02120RescheduleListResultDTO loResult = new PMT02120RescheduleListResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02120RescheduleListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02120.GetRescheduleList),
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

        public async Task<PMT02120HandoverUtilityResultDTO> GetHandoverUtilityListStreamAsync()
        {
            var loEx = new R_Exception();
            PMT02120HandoverUtilityResultDTO loResult = new PMT02120HandoverUtilityResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMT02120HandoverUtilityDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02120.GetHandoverUtilityList),
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

        public async Task HandoverProcessAsync(PMT02120HandoverProcessParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            PMT02100VoidResultHelperDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT02100VoidResultHelperDTO, PMT02120HandoverProcessParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02120.HandoverProcess),
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
        }

        public async Task ReinviteProcessAsync(PMT02120ReinviteProcessParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            PMT02100VoidResultHelperDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT02100VoidResultHelperDTO, PMT02120ReinviteProcessParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02120.ReinviteProcess),
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
        }

        public async Task RescheduleProcessAsync(PMT02120RescheduleProcessParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            PMT02100VoidResultHelperDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT02100VoidResultHelperDTO, PMT02120RescheduleProcessParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02120.RescheduleProcess),
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
        }

        public async Task AssignEmployeeAsync(PMT02120AssignEmployeeParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            PMT02100VoidResultHelperDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<PMT02100VoidResultHelperDTO, PMT02120AssignEmployeeParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMT02120.AssignEmployee),
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
        }

        #region NotImplemented

        public IAsyncEnumerable<PMT02120EmployeeListDTO> GetEmployeeList()
        {
            throw new NotImplementedException();
        }

        public PMT02100VoidResultHelperDTO HandoverProcess(PMT02120HandoverProcessParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public PMT02100VoidResultHelperDTO ReinviteProcess(PMT02120ReinviteProcessParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public PMT02100VoidResultHelperDTO RescheduleProcess(PMT02120RescheduleProcessParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02120RescheduleListDTO> GetRescheduleList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMT02120HandoverUtilityDTO> GetHandoverUtilityList()
        {
            throw new NotImplementedException();
        }

        public PMT02100VoidResultHelperDTO AssignEmployee(PMT02120AssignEmployeeParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
