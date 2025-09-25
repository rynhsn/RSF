using PMM09000COMMON.Amortization_Entry_DTO;
using PMM09000COMMON.Amortization_List_DTO;
using PMM09000COMMON.Interface;
using PMM09000COMMON.UtiliyDTO;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMM09000MODEL
{
    public class PMM09000ListModel : R_BusinessObjectServiceClientBase<PMM09000AmortizationDTO>, IPMM09000List
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMM09000List";
        private const string DEFAULT_MODULE = "PM";
        public PMM09000ListModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<PMM09000GenericList<PMM09000AmortizationDTO>> GetAmortizationListAsyncModel()
        {
            var loEx = new R_Exception();
            PMM09000GenericList<PMM09000AmortizationDTO> loResult = new PMM09000GenericList<PMM09000AmortizationDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM09000AmortizationDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM09000List.GetAmortizationList),
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

        public async Task<PMM09000GenericList<PMM09000AmortizationSheduleDetailDTO>> GetAmortizationScheduleListAsyncModel()
        {
            var loEx = new R_Exception();
            PMM09000GenericList<PMM09000AmortizationSheduleDetailDTO> loResult = new PMM09000GenericList<PMM09000AmortizationSheduleDetailDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM09000AmortizationSheduleDetailDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM09000List.GetAmortizationScheduleList),
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
        public async Task<PMM09000GenericList<PMM09000AmortizationChargesDTO>> GetAmortizationChargesListAsyncModel()
        {
            var loEx = new R_Exception();
            PMM09000GenericList<PMM09000AmortizationChargesDTO> loResult = new PMM09000GenericList<PMM09000AmortizationChargesDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM09000AmortizationChargesDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM09000List.GetAmortizationChargesList),
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

        public async Task<PMM09000GenericList<PMM09000BuildingDTO>> GetBuldingListAsyncModel()
        {
            var loEx = new R_Exception();
            PMM09000GenericList<PMM09000BuildingDTO> loResult = new PMM09000GenericList<PMM09000BuildingDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM09000BuildingDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM09000List.GetBuldingList),
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

        public async Task<PMM09000GenericList<ChargesDTO>> GetChargesTypeListAsyncModel()
        {
            var loEx = new R_Exception();
            PMM09000GenericList<ChargesDTO> loResult = new PMM09000GenericList<ChargesDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<ChargesDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM09000List.GetChargesTypeList),
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



        #region ImplementLibrary
        public IAsyncEnumerable<PMM09000AmortizationDTO> GetAmortizationList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMM09000AmortizationSheduleDetailDTO> GetAmortizationScheduleList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMM09000BuildingDTO> GetBuldingList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<ChargesDTO> GetChargesTypeList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMM09000AmortizationChargesDTO> GetAmortizationChargesList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
