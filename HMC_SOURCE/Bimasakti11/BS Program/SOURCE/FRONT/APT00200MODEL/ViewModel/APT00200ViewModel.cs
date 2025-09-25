using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using APT00200COMMON.DTOs.APT00200;
using APT00200COMMON.DTOs;
using R_BlazorFrontEnd.Helpers;
using APT00200FrontResources;

namespace APT00200MODEL.ViewModel
{
    public class APT00200ViewModel : R_ViewModel<APT00200DetailDTO>
    {
        private APT00200Model loModel = new APT00200Model();

        public APT00200DTO loPurchaseReturn = new APT00200DTO();

        public APT00200DetailDTO loSelectedPurchaseReturn = new APT00200DetailDTO();   

        public ObservableCollection<APT00200DetailDTO> loPurchaseReturnList = new ObservableCollection<APT00200DetailDTO>();

        public APT00200ResultDTO loRtn = null;

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

        public async Task GetPurchaseReturnListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            APT00200ParameterDTO loParam = null;
            List<APT00200DetailDTO> loTemp = null;
            try
            {
                loParam = new APT00200ParameterDTO()
                {
                    CPROPERTY_ID = loProperty.CPROPERTY_ID,
                    CDEPT_CODE = loPurchaseReturn.CDEPARTMENT_CODE,
                    CSUPPLIER_ID = loPurchaseReturn.CSUPPLIER_ID,
                    CPERIOD_FROM = Convert.ToString(loPurchaseReturn.IPERIOD_FROM_YEAR) + loPurchaseReturn.CPERIOD_FROM_MONTH,
                    CPERIOD_TO = Convert.ToString(loPurchaseReturn.IPERIOD_TO_YEAR) + loPurchaseReturn.CPERIOD_TO_MONTH
                };
                R_FrontContext.R_SetStreamingContext(ContextConstant.APT00200_GET_PURCHASE_RETURN_LIST_STREAMING_CONTEXT, loParam);
                loRtn = await loModel.GetPurchaseReturnListStreamAsync();
                loRtn.Data.ForEach(x =>
                {
                    try
                    {
                        x.DREF_DATE = DateTime.ParseExact(x.CREF_DATE, "yyyyMMdd", null);
                        x.DDOC_DATE = DateTime.ParseExact(x.CDOC_DATE, "yyyyMMdd", null);
                    }
                    catch (Exception)
                    {

                    }
                });
                loPurchaseReturnList = new ObservableCollection<APT00200DetailDTO>(loRtn.Data);
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

        public void RefreshPurchaseReturnListValidation()
        {
            bool llCancel = false;
            R_Exception loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(loPurchaseReturn.CDEPARTMENT_CODE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V001"));
                }

                llCancel = string.IsNullOrWhiteSpace(loPurchaseReturn.CSUPPLIER_ID) && loPurchaseReturn.CSUPPLIER_OPTIONS == "S";
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