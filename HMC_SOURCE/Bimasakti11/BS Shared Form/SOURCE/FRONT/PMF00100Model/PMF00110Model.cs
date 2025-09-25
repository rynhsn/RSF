using PMF00100COMMON.DTOs.PMF00100;
using PMF00100COMMON;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using PMF00100COMMON.DTOs.PMF00110;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Threading.Tasks;

namespace PMF00100Model
{
    public class PMF00110Model : R_BusinessObjectServiceClientBase<PMF00110ParameterDTO>, IPMF00110
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMF00110";
        private const string DEFAULT_MODULE = "PM";

        public PMF00110Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public PMF00110ResultDTO GetAllocationDetail(GetAllocationDetailParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<PMF00110ResultDTO> GetAllocationDetailAsync(GetAllocationDetailParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            PMF00110ResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<PMF00110ResultDTO, GetAllocationDetailParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMF00110.GetAllocationDetail),
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
                    nameof(IPMF00110.GetTransactionTypeList),
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
                    nameof(IPMF00110.RedraftAllocationProcess),
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
                    nameof(IPMF00110.SubmitAllocationProcess),
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
