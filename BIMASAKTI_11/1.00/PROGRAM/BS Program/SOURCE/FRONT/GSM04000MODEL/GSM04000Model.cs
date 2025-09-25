using GSM04000Common;
using GSM04000Common.DTO_s;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSM04000Model
{
    public class GSM04000Model : R_BusinessObjectServiceClientBase<DepartmentDTO>, IGSM04000
    {
        private const string DEFAULT_CHECKPOINT_NAME = "api/GSM04000";

        public GSM04000Model(
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

        public async Task<List<DepartmentDTO>> GetGSM04000ListAsync()
        {
            var loEx = new R_Exception();
            List<DepartmentDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = ContextConstant.DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<DepartmentDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04000.GetAllDeptList),
                    ContextConstant.DEFAULT_MODULE,
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

        public async Task ActiveInactiveDepartmentAsync(ActiveInactiveParam poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GeneralAPIResultDTO<ActiveInactiveParam>, ActiveInactiveParam>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04000.ActiveInactiveDepartmentAsync),
                    poParam,
                    ContextConstant.DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<DeptUserIsExistDTO> CheckIsUserDeptExistAsync(DepartmentDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            DeptUserIsExistDTO loRtn = new DeptUserIsExistDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loRtnTemp = await R_HTTPClientWrapper.R_APIRequestObject<GeneralAPIResultDTO<DeptUserIsExistDTO>, DepartmentDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04000.CheckIsUserDeptExist),
                    poEntity,
                    ContextConstant.DEFAULT_MODULE,
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

        public async Task DeleteDeptUserWhenChangingEveryoneAsync(DepartmentDTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            DeleteAssignedUser loRtn = new DeleteAssignedUser();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loRtnTemp = await R_HTTPClientWrapper.R_APIRequestObject<GeneralAPIResultDTO<DeleteAssignedUser>, DepartmentDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04000.DeleteDeptUserWhenChangingEveryone),
                    poEntity,
                    ContextConstant.DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }

        public async Task<UploadFileDTO> DownloadUploadDeptTemplateAsync()
        {
            var loEx = new R_Exception();
            UploadFileDTO loResult = new UploadFileDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loRtnTemp = await R_HTTPClientWrapper.R_APIRequestObject<GeneralAPIResultDTO<UploadFileDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04000.DownloadUploadDeptTemplate),
                    ContextConstant.DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult = loRtnTemp.data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }


        //implement only

        public IAsyncEnumerable<DepartmentDTO> GetAllDeptList()
        {
            throw new NotImplementedException();
        }

        GeneralAPIResultDTO<DepartmentDTO> IGSM04000.ActiveInactiveDepartmentAsync(ActiveInactiveParam poParam)
        {
            throw new NotImplementedException();
        }

        GeneralAPIResultDTO<DeptUserIsExistDTO> IGSM04000.CheckIsUserDeptExist(DepartmentDTO poEntity)
        {
            throw new NotImplementedException();
        }

        GeneralAPIResultDTO<DeleteAssignedUser> IGSM04000.DeleteDeptUserWhenChangingEveryone(DepartmentDTO poEntity)
        {
            throw new NotImplementedException();
        }

        GeneralAPIResultDTO<UploadFileDTO> IGSM04000.DownloadUploadDeptTemplate()
        {
            throw new NotImplementedException();
        }
    }
}
