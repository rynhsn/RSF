using System;
using System.Threading.Tasks;
using APR00500Common;
using APR00500Common.DTOs;
using APR00500Common.DTOs.Print;
using APR00500Common.Params;
using R_APIClient;
using R_APICommonDTO;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace APR00500Model
{
    public class APR00500Model : R_BusinessObjectServiceClientBase<APR00500DataResultDTO>, IAPR00500
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlAP";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/APR00500";
        private const string DEFAULT_MODULE = "AP";

        public APR00500Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public APR00500ListDTO<APR00500PropertyDTO> APR00500GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public APR00500SingleDTO<APR00500PeriodYearRangeDTO> APR00500GetYearRange()
        {
            throw new NotImplementedException();
        }

        public APR00500SingleDTO<APR00500SystemParamDTO> APR00500GetSystemParam()
        {
            throw new NotImplementedException();
        }

        public APR00500SingleDTO<APR00500TransCodeInfoDTO> APR00500GetTransCodeInfo()
        {
            throw new NotImplementedException();
        }

        public APR00500ListDTO<APR00500PeriodDTO> APR00500GetPeriodList(APR00500PeriodParam poParam)
        {
            throw new NotImplementedException();
        }

        public APR00500ListDTO<APR00500FunctDTO> APR00500GetCodeInfoList()
        {
            throw new NotImplementedException();
        }
        
        
        //Untuk fetch data object dari controller
        public async Task<T> GetAsync<T>(string pcNameOf) where T : R_APIResultBaseDTO
        {
            var loEx = new R_Exception();
            var loResult = default(T);

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<T>(
                    _RequestServiceEndPoint,
                    pcNameOf,
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
        
        //Untuk fetch data object dari controller
        public async Task<T> GetAsync<T, T1>(string pcNameOf, T1 poParameter) where T : R_APIResultBaseDTO
        {
            var loEx = new R_Exception();
            var loResult = default(T);

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<T, T1>(
                    _RequestServiceEndPoint,
                    pcNameOf,
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

            return loResult;
        }
    }
}