using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using APT00200COMMON.DTOs.APT00210;
using APT00200COMMON.DTOs;
using System.ComponentModel.Design.Serialization;
using APT00200COMMON.DTOs.APT00200;
using R_BlazorFrontEnd.Helpers;
using APT00200FrontResources;

namespace APT00200MODEL.ViewModel
{
    public class APT00210ViewModel : R_ViewModel<APT00210DTO>
    {
        private APT00210Model loModel = new APT00210Model();

        private APT00200Model loPurchaseReturnListModel = new APT00200Model();

        public APT00210DTO loPurchaseReturnHeader = new APT00210DTO();

        public List<GetPaymentTermListDTO> loPaymentTermList = null;

        public GetPaymentTermListResultDTO loPaymentTermRtn = null;

        public GetPaymentTermListDTO loPaymentTerm = new GetPaymentTermListDTO();

        public List<GetCurrencyListDTO> loCurrencyList = null;

        public GetCurrencyListResultDTO loCurrencyRtn = null;

        public GetCurrencyListDTO loCurrency = new GetCurrencyListDTO();

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public List<GetPropertyListDTO> loPropertyList = new List<GetPropertyListDTO>();

        public GetPropertyListResultDTO loPropertyRtn = null;

        public APT00210ResultDTO loRtn = null;

        public GetCurrencyOrTaxRateDTO loCurrencyRate = null;

        public GetCurrencyOrTaxRateResultDTO loCurrencyRateRtn = null;

        public GetCurrencyOrTaxRateDTO loTaxRate = null;

        public GetCurrencyOrTaxRateResultDTO loTaxRateRtn = null;

        public GetCompanyInfoDTO loCompanyInfo = null;

        public GetCurrencyOrTaxRateParameterDTO loCurrencyOrTaxRateParameter = new GetCurrencyOrTaxRateParameterDTO();

        public GetAPSystemParamDTO loAPSystemParam = null;

        public GetGLSystemParamDTO loGLSystemParam = null;

        public GetPeriodYearRangeDTO loPeriodYearRange = null;

        public GetTransCodeInfoDTO loTransCode = null;

        public async Task GetPaymentTermListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.APT00210_GET_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                loPaymentTermRtn = await loModel.GetPaymentTermListStreamAsync();
                loPaymentTermList = loPaymentTermRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetAPSystemParamAsync()
        {
            R_Exception loEx = new R_Exception();
            GetAPSystemParamResultDTO loResult = null;
            try
            {
                loResult = await loPurchaseReturnListModel.GetAPSystemParamAsync();
                loAPSystemParam = loResult.Data;
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
                loResult = await loPurchaseReturnListModel.GetPeriodYearRangeAsync();
                loPeriodYearRange = loResult.Data;
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
                loResult = await loPurchaseReturnListModel.GetGLSystemParamAsync();
                loGLSystemParam = loResult.Data;
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
                loResult = await loPurchaseReturnListModel.GetTransCodeInfoAsync();
                loTransCode = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        public async Task SubmitJournalProcessAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await loModel.SubmitJournalProcessAsync(new SubmitJournalParameterDTO()
                {
                    CREC_ID = loPurchaseReturnHeader.CREC_ID,
                    CPROPERTY_ID = loPurchaseReturnHeader.CPROPERTY_ID
                });
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task RedraftJournalProcessAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await loModel.RedraftJournalProcessAsync(new RedraftJournalParameterDTO()
                {
                    CREC_ID = loPurchaseReturnHeader.CREC_ID,
                    CPROPERTY_ID = loPurchaseReturnHeader.CPROPERTY_ID
                });
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
                loResult = await loPurchaseReturnListModel.GetCompanyInfoAsync();
                loCompanyInfo = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetCurrencyListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loCurrencyRtn = await loModel.GetCurrencyListStreamAsync();
                loCurrencyList = loCurrencyRtn.Data;
                loCurrencyList.ForEach(x => x.CDISPLAY = x.CCURRENCY_CODE + " - " + x.CCURRENCY_NAME);
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
                loPropertyRtn = await loPurchaseReturnListModel.GetPropertyListStreamAsync();
                loPropertyList = loPropertyRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetCurrencyRateAsync()
        {
            R_Exception loEx = new R_Exception();
            GetCurrencyOrTaxRateParameterDTO loParam = null;
            try
            {/*
                loParam = new GetCurrencyOrTaxRateParameterDTO()
                {
                    CCURRENCY_CODE = loCurrencyOrTaxRateParameter.CCURRENCY_CODE,
                    CRATETYPE_CODE = "",
                    CREF_DATE = loCurrencyOrTaxRateParameter.CREF_DATE
                };*/
                loCurrencyRateRtn = await loModel.GetCurrencyOrTaxRateAsync(loCurrencyOrTaxRateParameter);
                loCurrencyRate = loCurrencyRateRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTaxRateAsync()
        {
            R_Exception loEx = new R_Exception();
            GetCurrencyOrTaxRateParameterDTO loParam = null;
            try
            {/*
                loParam = new GetCurrencyOrTaxRateParameterDTO()
                {
                    CCURRENCY_CODE = loPurchaseReturnHeader.CCURRENCY_CODE,
                    CRATETYPE_CODE = "",
                    CREF_DATE = loPurchaseReturnHeader.CREF_DATE
                };*/
                loTaxRateRtn = await loModel.GetCurrencyOrTaxRateAsync(loCurrencyOrTaxRateParameter);
                loTaxRate = loTaxRateRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPurchaseReturnHeaderAsync(APT00210DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            APT00210ParameterDTO loParam = null;
            APT00210ParameterDTO loResult = null;
            try
            {
                loParam = new APT00210ParameterDTO()
                {
                    Data = poEntity
                };
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loPurchaseReturnHeader = loResult.Data;

                try
                {
                    loPurchaseReturnHeader.DREF_DATE = DateTime.ParseExact(loPurchaseReturnHeader.CREF_DATE, "yyyyMMdd", null);
                }
                catch (Exception)
                {

                }
                try
                {
                    loPurchaseReturnHeader.DDOC_DATE = DateTime.ParseExact(loPurchaseReturnHeader.CDOC_DATE, "yyyyMMdd", null);
                }
                catch (Exception)
                {

                }

                loPurchaseReturnHeader.CLOCAL_CURRENCY_CODE = loCompanyInfo.CLOCAL_CURRENCY_CODE;
                loPurchaseReturnHeader.CBASE_CURRENCY_CODE = loCompanyInfo.CBASE_CURRENCY_CODE;
                loProperty.CPROPERTY_ID = loPurchaseReturnHeader.CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void PurchaseReturnHeaderValidation(APT00210DTO poParam)
        {
            bool llCancel = false;

            var loEx = new R_Exception();

            try
            {
                llCancel = string.IsNullOrWhiteSpace(poParam.CPROPERTY_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V003"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CDEPT_CODE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V004"));
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CSUPPLIER_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V005"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CSUPPLIER_NAME);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V006"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CSUPPLIER_SEQ_NO);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V007"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CDOC_NO);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V008"));
                }
                llCancel = string.IsNullOrWhiteSpace(poParam.CDOC_DATE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V009"));
                }

                //llCancel = string.IsNullOrWhiteSpace(poParam.CPAY_TERM_CODE);
                //if (llCancel)
                //{
                //    loEx.Add(R_FrontUtility.R_GetError(
                //        typeof(Resources_Dummy_Class),
                //        "V010"));
                //}

                llCancel = string.IsNullOrWhiteSpace(poParam.CCURRENCY_CODE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V011"));
                }

                llCancel = poParam.NLBASE_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V012"));
                }

                llCancel = poParam.NLCURRENCY_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V013"));
                }

                llCancel = poParam.NLBASE_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V014"));
                }

                llCancel = poParam.NLCURRENCY_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V015"));
                }

                llCancel = poParam.LTAXABLE && string.IsNullOrWhiteSpace(poParam.CTAX_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V016"));
                }

                llCancel = poParam.LTAXABLE && poParam.NTAX_BASE_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V017"));
                }

                llCancel = poParam.LTAXABLE && poParam.NTAX_CURRENCY_RATE <= 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V018"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CTRANS_DESC);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V019"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SavePurchaseReturnHeaderAsync(APT00210DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            APT00210ParameterDTO loParam = null;
            APT00210ParameterDTO loResult = null;
            try
            {
                poEntity.CREF_DATE = poEntity.DREF_DATE.ToString("yyyyMMdd");
                poEntity.CDOC_DATE = poEntity.DDOC_DATE.ToString("yyyyMMdd");
                loParam = new APT00210ParameterDTO()
                {
                    Data = poEntity,
                    //CPROPERTY_ID = loProperty.CPROPERTY_ID
                };
                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loPurchaseReturnHeader = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeletePurchaseReturnHeaderAsync(APT00210DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            APT00210ParameterDTO loParam = null;
            try
            {
                loParam = new APT00210ParameterDTO()
                {
                    Data = poEntity,
                    //CPROPERTY_ID = loProperty.CPROPERTY_ID
                };
                await loModel.R_ServiceDeleteAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
