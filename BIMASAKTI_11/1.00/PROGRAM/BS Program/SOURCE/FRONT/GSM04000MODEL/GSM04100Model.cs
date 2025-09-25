using GSM04000Common;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSM04000Model
{
    public class GSM04100Model : R_BusinessObjectServiceClientBase<UserDepartmentDTO>, IGSM04100
    {
        private const string DEFAULT_CHECKPOINT_NAME = "api/GSM04100";

        public GSM04100Model(
            string pcHttpClientName = ContextConstant.DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                ContextConstant.DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        public async Task<List<UserDepartmentDTO>> GetUserDeptListAsync()
        {
            var loEx = new R_Exception();
            List<UserDepartmentDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UserDepartmentDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04100.GetUserDeptList),
                    ContextConstant.DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task<List<UserDepartmentDTO>> GetUserListAsync()
        {
            var loEx = new R_Exception();
            List<UserDepartmentDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UserDepartmentDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04100.GetUserList),
                    ContextConstant.DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        //impement only

        public IAsyncEnumerable<UserDepartmentDTO> GetUserDeptList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<UserDepartmentDTO> GetUserList()
        {
            throw new NotImplementedException();
        }


    }
}
