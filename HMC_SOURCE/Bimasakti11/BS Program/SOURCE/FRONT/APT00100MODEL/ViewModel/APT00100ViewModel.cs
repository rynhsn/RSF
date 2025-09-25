using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using APT00100COMMON.DTOs.APT00100;
using APT00100COMMON.DTOs;
using R_BlazorFrontEnd.Helpers;
using APT00100FrontResources;

namespace APT00100MODEL.ViewModel
{
    public class APT00100ViewModel : R_ViewModel<APT00100DetailDTO>
    {
        private APT00100Model loModel = new APT00100Model();

        public APT00100DTO loInvoice = new APT00100DTO();

        public APT00100DetailDTO loSelectedInvoice = new APT00100DetailDTO();   

        public ObservableCollection<APT00100DetailDTO> loInvoiceList = new ObservableCollection<APT00100DetailDTO>();

        public APT00100ResultDTO loRtn = null;

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public List<GetPropertyListDTO> loPropertyList = new List<GetPropertyListDTO>();

        public GetPropertyListResultDTO loPropertyRtn = null;

        //public GetAPSystemParamResultDTO loAPSystemParamRtn = null;

        //public GetPeriodYearRangeResultDTO loPeriodYearRangeRtn = null;

        //public GetCompanyInfoResultDTO loCompanyInfoRtn = null;

        //public GetGLSystemParamResultDTO loGLSystemParamRtn = null;

        //public GetTransCodeInfoResultDTO loTransCodeInfoRtn = null;

        public GetAPSystemParamDTO loAPSystemParam = null;

        public GetPeriodYearRangeDTO loPeriodYearRange = new GetPeriodYearRangeDTO();

        public GetCompanyInfoDTO loCompanyInfo = null;

        public GetGLSystemParamDTO loGLSystemParam = null;

        public GetTransCodeInfoDTO loTransCodeInfo = null;

        public async Task GetInvoiceListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            APT00100ParameterDTO loParam = null;
            List<APT00100DetailDTO> loTemp = null;
            try
            {
                loParam = new APT00100ParameterDTO()
                {
                    CPROPERTY_ID = loProperty.CPROPERTY_ID,
                    CDEPT_CODE = loInvoice.CDEPARTMENT_CODE,
                    CSUPPLIER_ID = loInvoice.CSUPPLIER_ID,
                    CPERIOD_FROM = Convert.ToString(loInvoice.IPERIOD_FROM_YEAR) + loInvoice.CPERIOD_FROM_MONTH,
                    CPERIOD_TO = Convert.ToString(loInvoice.IPERIOD_TO_YEAR) + loInvoice.CPERIOD_TO_MONTH
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.APT00100_GET_INVOICE_LIST_STREAMING_CONTEXT, loParam);
                loRtn = await loModel.GetInvoiceListStreamAsync();
                loRtn.Data.ForEach(x =>
                {
                    try
                    {
                        x.DDUE_DATE = DateTime.ParseExact(x.CDUE_DATE, "yyyyMMdd", null);
                    }
                    catch (Exception)
                    {

                    }
                    try
                    {
                        x.DREF_DATE = DateTime.ParseExact(x.CREF_DATE, "yyyyMMdd", null);
                    }
                    catch (Exception)
                    {

                    }
                });
                loInvoiceList = new ObservableCollection<APT00100DetailDTO>(loRtn.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loPropertyRtn = await loModel.GetPropertyListStreamAsync();
                loPropertyList = loPropertyRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }


        public async Task InitialProcess()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await GetAPSystemParamAsync();
                await GetPeriodYearRangeAsync();
                await GetCompanyInfoAsync();
                await GetGLSystemParamAsync();
                await GetTransCodeInfoAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetAPSystemParamAsync()
        {
            R_Exception loEx = new R_Exception();
            GetAPSystemParamResultDTO loResult = null;
            try
            {
                loResult = await loModel.GetAPSystemParamAsync();
                loAPSystemParam = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetGLSystemParamAsync()
        {
            R_Exception loEx = new R_Exception();
            GetGLSystemParamResultDTO loResult = null;
            try
            {
                loResult = await loModel.GetGLSystemParamAsync();
                loGLSystemParam = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPeriodYearRangeAsync()
        {
            R_Exception loEx = new R_Exception();
            GetPeriodYearRangeResultDTO loResult = null;
            try
            {
                loResult = await loModel.GetPeriodYearRangeAsync();
                loPeriodYearRange = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetCompanyInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            GetCompanyInfoResultDTO loResult = null;
            try
            {
                loResult = await loModel.GetCompanyInfoAsync();
                loCompanyInfo = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTransCodeInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            GetTransCodeInfoResultDTO loResult = null;
            try
            {
                loResult = await loModel.GetTransCodeInfoAsync();
                loTransCodeInfo = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void RefreshInvoiceListValidation()
        {
            bool llCancel = false;
            R_Exception loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(loInvoice.CDEPARTMENT_CODE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V001"));
                }

                llCancel = string.IsNullOrWhiteSpace(loInvoice.CSUPPLIER_ID) && loInvoice.CSUPPLIER_OPTIONS == "S";
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V002"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}