using CBT02200COMMON;
using CBT02200COMMON.DTO.CBT02200;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace CBT02200MODEL
{
    internal class CBT02200Model : R_BusinessObjectServiceClientBase<CBT02200GridParameterDTO>, ICBT02200
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlCB";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/CBT02200";
        private const string DEFAULT_MODULE = "CB";

        public CBT02200Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GetDeptLookupListDTO> GetDeptLookupList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetDeptLookupListResultDTO> GetDeptLookupListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetDeptLookupListDTO> loResult = null;
            GetDeptLookupListResultDTO loRtn = new GetDeptLookupListResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetDeptLookupListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT02200.GetDeptLookupList),
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


        public IAsyncEnumerable<CBT02200GridDetailDTO> GetChequeDetailList()
        {
            throw new NotImplementedException();
        }
        public async Task<CBT02200GridDetailResultDTO> GetChequeDetailListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<CBT02200GridDetailDTO> loResult = null;
            CBT02200GridDetailResultDTO loRtn = new CBT02200GridDetailResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CBT02200GridDetailDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT02200.GetChequeDetailList),
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


        public IAsyncEnumerable<CBT02200GridDTO> GetChequeHeaderList()
        {
            throw new NotImplementedException();
        }
        public async Task<CBT02200GridResultDTO> GetChequeHeaderListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<CBT02200GridDTO> loResult = null;
            CBT02200GridResultDTO loRtn = new CBT02200GridResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CBT02200GridDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT02200.GetChequeHeaderList),
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

        public IAsyncEnumerable<GetStatusDTO> GetStatusList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetStatusResultDTO> GetStatusListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetStatusDTO> loResult = null;
            GetStatusResultDTO loRtn = new GetStatusResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetStatusDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT02200.GetStatusList),
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


        public InitialProcessResultDTO InitialProcess()
        {
            throw new NotImplementedException();
        }
        public async Task<InitialProcessResultDTO> InitialProcessAsync()
        {
            R_Exception loEx = new R_Exception();
            InitialProcessResultDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestObject<InitialProcessResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT02200.InitialProcess),
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

            return loResult;
        }

        public UpdateStatusResultDTO UpdateStatus(UpdateStatusParameterDTO poParameter)
        {
            throw new NotImplementedException();
        }
        public async Task UpdateStatusAsync(UpdateStatusParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            UpdateStatusResultDTO loRtn = new UpdateStatusResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<UpdateStatusResultDTO, UpdateStatusParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBT02200.UpdateStatus),
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
