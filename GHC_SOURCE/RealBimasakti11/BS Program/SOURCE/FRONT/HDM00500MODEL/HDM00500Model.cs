using HDM00500COMMON.DTO.General;
using HDM00500COMMON.DTO_s;
using HDM00500COMMON.DTO_s.Helper;
using HDM00500COMMON.Interfaces;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HDM00500MODEL
{
    public class HDM00500Model : R_BusinessObjectServiceClientBase<TaskchecklistDTO>, IHDM00500
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlHD";
        private const string DEFAULT_CHECKPOINT_NAME = "api/HDM00500";
        private const string DEFAULT_MODULE = "HD";
        public HDM00500Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        //real method
        public async Task<List<PropertyDTO>> GetList_PropertyAsync()
        {
            var loEx = new R_Exception();
            List<PropertyDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IHDM00500.GetList_Property),
                    DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<List<TaskchecklistDTO>> GetList_TaskchecklistAsync()
        {
            var loEx = new R_Exception();
            List<TaskchecklistDTO> loResult = null;
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TaskchecklistDTO>(
                    _RequestServiceEndPoint,
                    nameof(IHDM00500.GetList_Taskchecklist),
                    DEFAULT_MODULE, _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task ActiveInactive_TaskchecklistAsync(TaskchecklistDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                await R_HTTPClientWrapper.R_APIRequestObject<GeneralAPIResultBaseDTO<object>, TaskchecklistDTO>(
                    _RequestServiceEndPoint,
                    nameof(IHDM00500.ActiveInactive_Taskchecklist),
                    poParam,
                    DEFAULT_MODULE
                    , _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //implement only
        public IAsyncEnumerable<PropertyDTO> GetList_Property()
        {
            throw new System.NotImplementedException();
        }
        public IAsyncEnumerable<TaskchecklistDTO> GetList_Taskchecklist()
        {
            throw new System.NotImplementedException();
        }

        public GeneralAPIResultBaseDTO<object> ActiveInactive_Taskchecklist(TaskchecklistDTO poParam)
        {
            throw new NotImplementedException();
        }
    }
}
