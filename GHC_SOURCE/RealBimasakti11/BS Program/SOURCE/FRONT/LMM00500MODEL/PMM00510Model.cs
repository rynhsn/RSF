using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PMM00500Common;
using PMM00500Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace PMM00500Model
{
    public class PMM00510Model : R_BusinessObjectServiceClientBase<PMM00510DTO>, IPMM00510
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/PMM00510";
        private const string DEFAULT_MODULE = "PM";

        public PMM00510Model() :
            base(DEFAULT_HTTP_NAME, DEFAULT_SERVICEPOINT_NAME, DEFAULT_MODULE, true, true)
        {
        }
        #region GetList
        public async Task<PropertyListDataChargeDTO> GetPropertyListChargesAsync()
        {
            var loEx = new R_Exception();
            PropertyListDataChargeDTO loResult = new PropertyListDataChargeDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var LoTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyListStreamChargeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM00510.GetPropertyListCharges),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = LoTempResult;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<PMM00500ListDTO> GetAllChargesListAsync(string pcProperty, string pcChargesType)
        {

            var loEx = new R_Exception();
            PMM00500ListDTO loResult = new PMM00500ListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, pcProperty);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGE_TYPE_ID, pcChargesType);

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM00500GridDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM00510.GetAllChargesList),
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
        public async Task<ChargesTaxTypeListDTO> GetChargesTaxTypeAsync()
        {
            var loEx = new R_Exception();
            ChargesTaxTypeListDTO loResult = new ChargesTaxTypeListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<ChargesTaxTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM00510.GetChargesTaxType),
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
        public async Task<ChargesTaxCodeListDTO> GetChargesTaxCodeAsync()
        {
            var loEx = new R_Exception();
            ChargesTaxCodeListDTO loResult = new ChargesTaxCodeListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<ChargesTaxCodeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM00510.GetChargesTaxCode),
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

        public async Task<AccrualListDTO> GetAccrualListAsync()
        {
            var loEx = new R_Exception();
            AccrualListDTO loResult = new AccrualListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<AccurualDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM00510.GetAccrualList),
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
        public async Task<PMM00510ListDTO<PMM00510DTO>> UnitChargesPrintAsync()
        {
            var loEx = new R_Exception();
            PMM00510ListDTO< PMM00510DTO> loResult = new PMM00510ListDTO<PMM00510DTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTemp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM00510DTO>(
                      _RequestServiceEndPoint,
                      nameof(IPMM00510.UnitChargesPrint),
                      DEFAULT_MODULE,
                      _SendWithContext,
                      _SendWithToken);
                loResult.Data = loTemp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        #endregion

        public async Task RSP_LM_ACTIVE_INACTIVE_MethodAsync(PMM00510DTO poData)
        {
            var loEx = new R_Exception();
            ActiveInactiveDTO loRtn = new ActiveInactiveDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<ActiveInactiveDTO, PMM00510DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM00510.RSP_LM_ACTIVE_INACTIVE_Method),
                    poData,
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
        public async Task CopyNewProcessAsync(PMM00510DTO poData)
        {
            var loEx = new R_Exception();
            CopyNewProcessListDTO loRtn = new CopyNewProcessListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<CopyNewProcessListDTO, PMM00510DTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM00510.CopyNewProcess),
                    poData,
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
        public async Task<UnitChargesTypeListDTO> GetUnitChargesTypeAsync()
        {
            var loEx = new R_Exception();
            UnitChargesTypeListDTO loResult = new UnitChargesTypeListDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UnitChargesTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM00510.GetUnitChargeType),
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
        #region not Impelemet
        public IAsyncEnumerable<PropertyListStreamChargeDTO> GetPropertyListCharges()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMM00500GridDTO> GetAllChargesList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<ChargesTaxTypeDTO> GetChargesTaxType()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<ChargesTaxCodeDTO> GetChargesTaxCode()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<AccurualDTO> GetAccrualList()
        {
            throw new NotImplementedException();
        }

        public ActiveInactiveDTO RSP_LM_ACTIVE_INACTIVE_Method(PMM00510DTO poActiveInactive)
        {
            throw new NotImplementedException();
        }

        public CopyNewProcessListDTO CopyNewProcess(PMM00510DTO poData)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMM00510DTO> UnitChargesPrint()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<UnitChargesTypeDTO> GetUnitChargeType()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
