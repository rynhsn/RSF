using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PMM00500Common;
using PMM00500Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMM00500Model
{
    public class PMM00500Model : R_BusinessObjectServiceClientBase<PMM00500DTO>, IPMM00500
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMM00500";
        private const string DEFAULT_MODULE = "PM";

        public PMM00500Model() :
            base(DEFAULT_HTTP_NAME, DEFAULT_SERVICEPOINT_NAME, DEFAULT_MODULE, true, true)
        {
        }

        public async Task<ChargesTypeListDTO> GetAllChargesTypeAsync()
        {
            var loEx = new R_Exception();
            ChargesTypeListDTO loResult = new ChargesTypeListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<ChargesTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM00500.GetAllChargesType),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = loTempResult;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #region notImplemented
        public IAsyncEnumerable<ChargesTypeDTO> GetAllChargesType()
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
