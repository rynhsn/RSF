using ICT00900COMMON;
using ICT00900COMMON.DTO;
using ICT00900COMMON.Interface;
using ICT00900COMMON.Param;
using ICT00900COMMON.Utility_DTO;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ICT00900MODEL
{
    public class ICT00900Model : R_BusinessObjectServiceClientBase<ICT00900AjustmentDetailDTO>, IICT00900
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlIC";
        private const string DEFAULT_ENDPOINT = "api/ICT00100Adjustment";
        private const string DEFAULT_MODULE = "IC";
        public ICT00900Model(
         string pcHttpClientName = DEFAULT_HTTP,
         string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
         string pcModuleName = DEFAULT_MODULE,
         bool plSendWithContext = true,
         bool plSendWithToken = true)
         : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }
        public async Task<ICT00900GenericList<PropertyDTO>> PropertyListAsync()
        {
            var loEx = new R_Exception();
            ICT00900GenericList<PropertyDTO> loResult = new ICT00900GenericList<PropertyDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IICT00900.PropertyList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<ICT00900GenericList<CurrencyDTO>> CurrencyListAsync()
        {
            var loEx = new R_Exception();
            ICT00900GenericList<CurrencyDTO> loResult = new ICT00900GenericList<CurrencyDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<CurrencyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IICT00900.CurrencyList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
                loResult.Data = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<VarGsmTransactionCodeDTO> GetVAR_GSM_TRANSACTION_CODEAsync()
        {
            var loEx = new R_Exception();
            var loResult = new VarGsmTransactionCodeDTO();
            try
            {

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<VarGsmTransactionCodeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IICT00900.GetVAR_GSM_TRANSACTION_CODE),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<VarGsmCompanyInfoDTO> GetVAR_GSM_COMPANY_INFOAsync()
        {
            var loEx = new R_Exception();
            var loResult = new VarGsmCompanyInfoDTO();
            try
            {

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<VarGsmCompanyInfoDTO>(
                    _RequestServiceEndPoint,
                    nameof(IICT00900.GetVAR_GSM_COMPANY_INFO),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<ICT00900AdjustmentDTO> ChangeStatusAdjAsyncModel(ICT00900ParameterChangeStatusDTO poEntity)
        {
            var loEx = new R_Exception();
            ICT00900AdjustmentDTO loResult = new ICT00900AdjustmentDTO();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<ICT00900AdjustmentDTO, ICT00900ParameterChangeStatusDTO>(
                    _RequestServiceEndPoint,
                    nameof(IICT00900.ChangeStatusAdjustment),
                    poEntity,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken
                );
                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult!;
        }
        public async Task<ICT00900GenericList<ICT00900AdjustmentDTO>> GetAdjustmentListAsyncModel()
        {
            var loEx = new R_Exception();
            ICT00900GenericList<ICT00900AdjustmentDTO> loResult = new ICT00900GenericList<ICT00900AdjustmentDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<ICT00900AdjustmentDTO>(
                    _RequestServiceEndPoint,
                    nameof(IICT00900.GetAdjustmentList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = temp;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        #region JustImpelement
        public ICT00900AdjustmentDTO ChangeStatusAdjustment(ICT00900ParameterChangeStatusDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<ICT00900AdjustmentDTO> GetAdjustmentList()
        {
            throw new NotImplementedException();
        }

        public VarGsmTransactionCodeDTO GetVAR_GSM_TRANSACTION_CODE()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PropertyDTO> PropertyList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<CurrencyDTO> CurrencyList()
        {
            throw new NotImplementedException();
        }

        public VarGsmCompanyInfoDTO GetVAR_GSM_COMPANY_INFO()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
