using PQM00500COMMON;
using PQM00500COMMON.DTO_s;
using PQM00500COMMON.DTO_s.Base;
using PQM00500COMMON.DTO_s.Helper;
using PQM00500COMMON.Interfaces;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PQM00500MODEL
{
    public class PQM00500Model : R_BusinessObjectServiceClientBase<CompanyInfoDTO>, IPQM00500
    {
        private const string DEFAULT_CHECKPOINT_NAME = "api/PQM00500";

        public PQM00500Model(string pcHttpClientName = PQM00500ContextConstant.DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                PQM00500ContextConstant.DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        { }

        public IAsyncEnumerable<CompanyInfoDTO> GetList_CompanyInfo()
        {
            throw new NotImplementedException();
        }

        public async Task<List<CompanyInfoDTO>> GetList_CompanyInfoAsync()
        {
            var loEx = new R_Exception();
            List<CompanyInfoDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = PQM00500ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CompanyInfoDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPQM00500.GetList_CompanyInfo),
                    PQM00500ContextConstant.DEFAULT_MODULE,
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

        public IAsyncEnumerable<UserDTO> GetList_User()
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserDTO>> GetList_UserAsync()
        {
            var loEx = new R_Exception();
            List<UserDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = PQM00500ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UserDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPQM00500.GetList_User),
                    PQM00500ContextConstant.DEFAULT_MODULE,
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

        public GeneralRecordAPIResultDTO<UserDTO> GetRecord_User(UserDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetRecord_UserAsync(UserDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            UserDTO loRtn = new UserDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loRtnTemp = await R_HTTPClientWrapper.R_APIRequestObject<GeneralRecordAPIResultDTO<UserDTO>, UserDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPQM00500.GetRecord_User),
                    poParam,
                    PQM00500ContextConstant.DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loRtn = loRtnTemp.data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            return loRtn;
        }

    }
}
