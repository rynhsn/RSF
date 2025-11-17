using APF00100COMMON.DTOs.APF00100;
using APF00100COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using APF00100COMMON.DTOs.APF00110;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace APF00100Model
{
    public class APF00110Model : R_BusinessObjectServiceClientBase<APF00110ParameterDTO>, IAPF00110
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlAP";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/APF00110";
        private const string DEFAULT_MODULE = "AP";

        public APF00110Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public APF00110ResultDTO GetAllocationDetail(GetAllocationDetailParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<APF00110ResultDTO> GetAllocationDetailAsync(GetAllocationDetailParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            APF00110ResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<APF00110ResultDTO, GetAllocationDetailParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPF00110.GetAllocationDetail),
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
            return loRtn;
        }

        public IAsyncEnumerable<GetTransactionTypeDTO> GetTransactionTypeList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetTransactionTypeResultDTO> GetTransactionTypeListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetTransactionTypeDTO> loResult = null;
            GetTransactionTypeResultDTO loRtn = new GetTransactionTypeResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetTransactionTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPF00110.GetTransactionTypeList),
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

        public RedraftAllocationResultDTO RedraftAllocationProcess(RedraftAllocationParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<RedraftAllocationResultDTO> RedraftAllocationProcessAsync(RedraftAllocationParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            RedraftAllocationResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<RedraftAllocationResultDTO, RedraftAllocationParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPF00110.RedraftAllocationProcess),
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
            return loRtn;
        }


        public SubmitAllocationResultDTO SubmitAllocationProcess(SubmitAllocationParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<SubmitAllocationResultDTO> SubmitAllocationProcessAsync(SubmitAllocationParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            SubmitAllocationResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<SubmitAllocationResultDTO, SubmitAllocationParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPF00110.SubmitAllocationProcess),
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
            return loRtn;
        }

    }
}
