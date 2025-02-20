using PMM01500COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMM01500MODEL
{
    public class PMM01500Model : R_BusinessObjectServiceClientBase<PMM01500DTO>, IPMM01500
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMM01500";
        private const string DEFAULT_MODULE = "PM";

        public PMM01500Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public PMM01500SingleResult<bool> CheckDataTabTemplateBank(PMM01500DTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> CheckDataTabTemplateBankAsync(PMM01500DTO poParam)
        {
            var loEx = new R_Exception();
            bool llRtn = false;
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempRtn = await R_HTTPClientWrapper.R_APIRequestObject<PMM01500SingleResult<bool>, PMM01500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01500.CheckDataTabTemplateBank),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                llRtn = loTempRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
            return llRtn;
        }

        public IAsyncEnumerable<PMM01500UniversalDTO> GetAllUniversalList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PMM01500UniversalDTO>> GetAllUniversalListAsync(string pcParameter)
        {
            var loEx = new R_Exception();
            List<PMM01500UniversalDTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPARAMETER, pcParameter);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01500UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01500.GetAllUniversalList),
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

        public IAsyncEnumerable<PMM01501DTO> GetInvoiceGrpList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PMM01501DTO>> GetInvoiceGrpListAsync(string poPropertyIdParam)
        {
            var loEx = new R_Exception();
            List<PMM01501DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poPropertyIdParam);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01501DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01500.GetInvoiceGrpList),
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

        public IAsyncEnumerable<PMM01500DTOPropety> GetProperty()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PMM01500DTOPropety>> GetPropertyAsync()
        {
            var loEx = new R_Exception();
            List<PMM01500DTOPropety> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01500DTOPropety>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01500.GetProperty),
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

        public IAsyncEnumerable<PMM01500StampRateDTO> GetStampRateList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<PMM01500StampRateDTO>> GetStampRateListAsync(string poPropertyIdParam)
        {
            var loEx = new R_Exception();
            List<PMM01500StampRateDTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poPropertyIdParam);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01500StampRateDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01500.GetStampRateList),
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

        
        
        public IAsyncEnumerable<PMM01500DTOInvTemplate> GetInvoiceTemplate()
        {
            throw new NotImplementedException();
        }
        
        public async Task<List<PMM01500DTOInvTemplate>> GetInvoiceTemplateAsync(string poPropertyIdParam)
        {
            var loEx = new R_Exception();
            List<PMM01500DTOInvTemplate> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poPropertyIdParam);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01500DTOInvTemplate>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01500.GetInvoiceTemplate),
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
        
        public PMM01500SingleResult<PMM01500DTO> PMM01500ActiveInactive(PMM01500DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task PMM01500ActiveInactiveAsync(PMM01500DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loRtn = await R_HTTPClientWrapper.R_APIRequestObject<PMM01500SingleResult<PMM01500DTO>, PMM01500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01500.PMM01500ActiveInactive),
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

        public IAsyncEnumerable<PMM01502DTO> PMM01500LookupBank()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PMM01502DTO>> PMM01500LookupBankAsync()
        {
            var loEx = new R_Exception();
            List<PMM01502DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM01502DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM01500.PMM01500LookupBank),
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
        
    }
}
