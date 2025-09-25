using PMM03700COMMON;
using PMM03700COMMON.DTO_s;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMM03700MODEL
{
    public class PMM03710Model : R_BusinessObjectServiceClientBase<TenantClassificationDTO>, IPMM03710
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_CHECKPOINT_NAME = "api/PMM03710";
        private const string DEFAULT_MODULE = "PM";
        public PMM03710Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
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
        public IAsyncEnumerable<TenantDTO> GetAssignedTenantList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<TenantDTO>> GetAssignedTenantListAsync()
        {
            var loEx = new R_Exception();
            List<TenantDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TenantDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM03710.GetAssignedTenantList),
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
        public IAsyncEnumerable<TenantClassificationDTO> GetTenantClassificationList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<TenantClassificationDTO>> GetTenantClassificationListAsync()
        {
            var loEx = new R_Exception();
            List<TenantClassificationDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TenantClassificationDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM03710.GetTenantClassificationList),
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

        #region AssignTenant

        public IAsyncEnumerable<TenantDTO> GetAvailableTenantList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<TenantDTO>> GetTenanToAssigntListAsync()
        {
            var loEx = new R_Exception();
            List<TenantDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TenantDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM03710.GetAvailableTenantList),
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
        public TenantResultDumpDTO AssignTenant(TenantParamDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task AssignTenantAsync(TenantParamDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                await R_HTTPClientWrapper.R_APIRequestObject<TenantResultDumpDTO, TenantParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM03710.AssignTenant),
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

        #endregion

        #region MoveTenant

        public TenantResultDumpDTO MoveTenant(TenantMoveParamDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task MoveTenantAsync(TenantMoveParamDTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                await R_HTTPClientWrapper.R_APIRequestObject<TenantResultDumpDTO,TenantMoveParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM03710.MoveTenant),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public IAsyncEnumerable<TenantDTO> GetTenantToMoveList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<TenantDTO>> GetTenanToMoveListAsync()
        {
            var loEx = new R_Exception();
            List<TenantDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<TenantDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM03710.GetTenantToMoveList),
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

        #endregion
    }
}
