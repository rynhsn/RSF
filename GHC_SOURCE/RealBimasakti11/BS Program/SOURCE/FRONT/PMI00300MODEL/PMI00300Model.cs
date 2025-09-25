using PMI00300COMMON;
using PMI00300COMMON.DTO;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMI00300MODEL
{
    public class PMI00300Model : R_BusinessObjectServiceClientBase<PMI00300ParameterDTO>, IPMI00300
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMI00300";
        private const string DEFAULT_MODULE = "PM";

        public PMI00300Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region GET LEASE STATUS LIST
        public IAsyncEnumerable<PMI00300GetGSBCodeListDTO> GetLeaseStatusList()
        {
            throw new NotImplementedException();
        }

        public async Task<PMI00300GetGSBCodeListResultDTO> GetLeaseStatusListAsync()
        {
            R_Exception loEx = new R_Exception();
            List<PMI00300GetGSBCodeListDTO> loResult = null;
            PMI00300GetGSBCodeListResultDTO loRtn = new PMI00300GetGSBCodeListResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMI00300GetGSBCodeListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMI00300.GetLeaseStatusList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                    );
                loRtn.Data = loResult;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        #endregion

        #region GET PROPERTY LIST
        public IAsyncEnumerable<PMI00300GetPropertyListDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public async Task<PMI00300GetPropertyListResultDTO> GetPropertyListAsync()
        {
            R_Exception loEx = new R_Exception();
            List<PMI00300GetPropertyListDTO> loResult = null;
            PMI00300GetPropertyListResultDTO loRtn = new PMI00300GetPropertyListResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMI00300GetPropertyListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMI00300.GetPropertyList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                    );
                loRtn.Data = loResult;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        #endregion

        #region GET STRATA STATUS LIST
        public IAsyncEnumerable<PMI00300GetGSBCodeListDTO> GetStrataStatusList()
        {
            throw new NotImplementedException();
        }

        public async Task<PMI00300GetGSBCodeListResultDTO> GetStrataStatusListAsync()
        {
            R_Exception loEx = new R_Exception();
            List<PMI00300GetGSBCodeListDTO> loResult = null;
            PMI00300GetGSBCodeListResultDTO loRtn = new PMI00300GetGSBCodeListResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMI00300GetGSBCodeListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMI00300.GetStrataStatusList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                    );
                loRtn.Data = loResult;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        #endregion

        #region GET UNIT INQUIRY DETAIL LEFT LIST
        public IAsyncEnumerable<PMI00300DetailLeftDTO> GetUnitInquiryDetailLeftList()
        {
            throw new NotImplementedException();
        }

        public async Task<PMI00300DetailLeftResultDTO> GetUnitInquiryDetailLeftListAsync()
        {
            R_Exception loEx = new R_Exception();
            List<PMI00300DetailLeftDTO> loResult = null;
            PMI00300DetailLeftResultDTO loRtn = new PMI00300DetailLeftResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMI00300DetailLeftDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMI00300.GetUnitInquiryDetailLeftList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                    );
                loRtn.Data = loResult;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        #endregion

        #region GET UNIT INQUIRY DETAIL RIGHT LIST
        public IAsyncEnumerable<PMI00300DetailRightDTO> GetUnitInquiryDetailRightList()
        {
            throw new NotImplementedException();
        }
        public async Task<PMI00300DetailRightResultDTO> GetUnitInquiryDetailRightListAsync()
        {
            R_Exception loEx = new R_Exception();
            List<PMI00300DetailRightDTO> loResult = null;
            PMI00300DetailRightResultDTO loRtn = new PMI00300DetailRightResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMI00300DetailRightDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMI00300.GetUnitInquiryDetailRightList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                    );
                loRtn.Data = loResult;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        #endregion

        #region GET UNIT INQUIRY HEADER LIST
        public IAsyncEnumerable<PMI00300DTO> GetUnitInquiryHeaderList()
        {
            throw new NotImplementedException();
        }

        public async Task<PMI00300ResultDTO> GetUnitInquiryHeaderListAsync()
        {
            R_Exception loEx = new R_Exception();
            List<PMI00300DTO> loResult = null;
            PMI00300ResultDTO loRtn = new PMI00300ResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMI00300DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMI00300.GetUnitInquiryHeaderList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                    );
                loRtn.Data = loResult;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        #endregion
    }
}
