using PMM10000COMMON.Call_Type_Pricelist;
using PMM10000COMMON.Interface;
using PMM10000COMMON.SLA_Call_Type;
using PMM10000COMMON.SLA_Category;
using PMM10000COMMON.UtilityDTO;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMM10000MODEL
{
    public class PMM10000ListModel : R_BusinessObjectServiceClientBase<PMM10000SLACallTypeDTO>, IPMM10000List
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlPM";
        private const string DEFAULT_ENDPOINT = "api/PMM10000List";
        private const string DEFAULT_MODULE = "PM";
        public PMM10000ListModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            string pcModuleName = DEFAULT_MODULE,
            bool plSendWithContext = true,
            bool plSendWithToken = true)
            : base(pcHttpClientName, pcRequestServiceEndPoint, pcModuleName, plSendWithContext, plSendWithToken)
        {
        }
        public async Task<PMM10000GenericList<PMM10000SLACallTypeDTO>> GetCallTypeListAsyncModel()
        {
            var loEx = new R_Exception();
            PMM10000GenericList<PMM10000SLACallTypeDTO> loResult = new PMM10000GenericList<PMM10000SLACallTypeDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM10000SLACallTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM10000List.GetCallTypeList),
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
        public async Task<PMM10000GenericList<PMM10000CategoryDTO>> GetCategoryListModel()
        {
            var loEx = new R_Exception();
            PMM10000GenericList<PMM10000CategoryDTO> loResult = new PMM10000GenericList<PMM10000CategoryDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM10000CategoryDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM10000List.GetCategoryList),
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
        public async Task<PMM10000GenericList<PMM10000PricelistDTO>> GetPricelistModel()
        {
            var loEx = new R_Exception();
            PMM10000GenericList<PMM10000PricelistDTO> loResult = new PMM10000GenericList<PMM10000PricelistDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM10000PricelistDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM10000List.GetPricelist),
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

        public async Task<PMM10000GenericList<PMM10000PricelistDTO>> GetAssignPricelistModel()
        {
            var loEx = new R_Exception();
            PMM10000GenericList<PMM10000PricelistDTO> loResult = new PMM10000GenericList<PMM10000PricelistDTO>();
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var temp = await R_HTTPClientWrapper.R_APIRequestStreamingObject<PMM10000PricelistDTO>(
                    _RequestServiceEndPoint,
                    nameof(IPMM10000List.GetAssignPricelist),
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

        //IMPLEMENT LIBRARY
        public IAsyncEnumerable<PMM10000SLACallTypeDTO> GetCallTypeList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMM10000CategoryDTO> GetCategoryList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMM10000PricelistDTO> GetAssignPricelist()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<PMM10000PricelistDTO> GetPricelist()
        {
            throw new NotImplementedException();
        }
    }
}
