using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using PMT50600COMMON.DTOs.PMT50610;
using PMT50600COMMON.DTOs;
using System.ComponentModel.Design.Serialization;
using PMT50600COMMON.DTOs.PMT50600;
using R_BlazorFrontEnd.Helpers;
using PMT50600FrontResources;
using PMT50600COMMON.DTOs.PMT50630;
using PMT50600COMMON.DTOs.PMT50620;

namespace PMT50600MODEL.ViewModel
{
    public class PMT50610ViewModel : R_ViewModel<PMT50610DTO>
    {
        private PMT50610Model loModel = new PMT50610Model();

        private PMT50600Model loCreditNoteListModel = new PMT50600Model();

        public PMT50620Model loDetailModel = new PMT50620Model();

        public PMT50610DTO loCreditNoteHeader = new PMT50610DTO();

        public List<GetPaymentTermListDTO> loPaymentTermList = null;

        public GetPaymentTermListResultDTO loPaymentTermRtn = null;

        public GetPaymentTermListDTO loPaymentTerm = new GetPaymentTermListDTO();

        public List<GetCurrencyListDTO> loCurrencyList = null;

        public GetCurrencyListResultDTO loCurrencyRtn = null;

        public GetCurrencyListDTO loCurrency = new GetCurrencyListDTO();

        public GetPropertyListDTO loProperty = new GetPropertyListDTO();

        public List<GetPropertyListDTO> loPropertyList = new List<GetPropertyListDTO>();

        public GetPropertyListResultDTO loPropertyRtn = null;

        public PMT50610ResultDTO loRtn = null;

        public GetCurrencyOrTaxRateDTO loCurrencyRate = null;

        public GetCurrencyOrTaxRateResultDTO loCurrencyRateRtn = null;

        public GetCurrencyOrTaxRateDTO loTaxRate = null;

        public GetCurrencyOrTaxRateResultDTO loTaxRateRtn = null;

        public GetCompanyInfoDTO loCompanyInfo = null;

        public GetCurrencyOrTaxRateParameterDTO loCurrencyOrTaxRateParameter = new GetCurrencyOrTaxRateParameterDTO();

        public GetPMSystemParamDTO loAPSystemParam = null;

        public GetGLSystemParamDTO loGLSystemParam = null;

        public GetPeriodYearRangeDTO loPeriodYearRange = null;

        public GetTransCodeInfoDTO loTransCode = null;

        public InvoiceEntryPredifineParameterDTO loTabParameter = null;


        public async Task CloseFormProcessAsync(OnCloseProcessParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await loDetailModel.CloseFormProcessAsync(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPaymentTermListStreamAsync()
        {
            R_Exception loException = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.PMT50610_GET_PROPERTY_ID_STREAMING_CONTEXT, loProperty.CPROPERTY_ID);
                loPaymentTermRtn = await loModel.GetPaymentTermListStreamAsync();
                loPaymentTermList = loPaymentTermRtn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetPMSystemParamAsync()
        {
            R_Exception loEx = new R_Exception();
            GetPMSystemParamResultDTO loResult = null;
            try
            {
                loResult = await loCreditNoteListModel.GetPMSystemParamAsync(new GetPMSystemParamParameterDTO()
                {
                    CPROPERTY_ID = loTabParameter.PARAM_PROPERTY_ID
                });
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
                loResult = await loCreditNoteListModel.GetPeriodYearRangeAsync();
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
                loResult = await loCreditNoteListModel.GetGLSystemParamAsync();
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
                loResult = await loCreditNoteListModel.GetTransCodeInfoAsync();
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
                    CREC_ID = loCreditNoteHeader.CREC_ID,
                    CPROPERTY_ID = loCreditNoteHeader.CPROPERTY_ID
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
                    CREC_ID = loCreditNoteHeader.CREC_ID,
                    CPROPERTY_ID = loCreditNoteHeader.CPROPERTY_ID
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
                loResult = await loCreditNoteListModel.GetCompanyInfoAsync();
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
                loPropertyRtn = await loCreditNoteListModel.GetPropertyListStreamAsync();
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
                    CCURRENCY_CODE = loCreditNoteHeader.CCURRENCY_CODE,
                    CRATETYPE_CODE = "",
                    CREF_DATE = loCreditNoteHeader.CREF_DATE
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

        public async Task GetInvoiceHeaderAsync(PMT50610DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            PMT50610ParameterDTO loParam = null;
            PMT50610ParameterDTO loResult = null;
            try
            {
                loParam = new PMT50610ParameterDTO()
                {
                    Data = poEntity
                };
                loResult = await loModel.R_ServiceGetRecordAsync(loParam);

                loCreditNoteHeader = loResult.Data;

                try
                {
                    loCreditNoteHeader.DDUE_DATE = DateTime.ParseExact(loCreditNoteHeader.CDUE_DATE, "yyyyMMdd", null);
                }
                catch (Exception)
                {

                }
                try
                {
                    loCreditNoteHeader.DREF_DATE = DateTime.ParseExact(loCreditNoteHeader.CREF_DATE, "yyyyMMdd", null);
                }
                catch (Exception)
                {

                }
                try
                {
                    loCreditNoteHeader.DDOC_DATE = DateTime.ParseExact(loCreditNoteHeader.CDOC_DATE, "yyyyMMdd", null);
                }
                catch (Exception)
                {

                };

                loCreditNoteHeader.CLOCAL_CURRENCY_CODE = loCompanyInfo.CLOCAL_CURRENCY_CODE;
                loCreditNoteHeader.CBASE_CURRENCY_CODE = loCompanyInfo.CBASE_CURRENCY_CODE;
                loProperty.CPROPERTY_ID = loCreditNoteHeader.CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void InvoiceHeaderValidation(PMT50610DTO poParam)
        {
            bool llCancel = false;

            var loEx = new R_Exception();

            try
            {
                try
                {
                    poParam.CDUE_DATE = poParam.DDUE_DATE.Value.ToString("yyyyMMdd");
                }
                catch (Exception)
                {

                }
                try
                {
                    poParam.CREF_DATE = poParam.DREF_DATE.Value.ToString("yyyyMMdd");
                }
                catch (Exception)
                {

                }
                try
                {
                    poParam.CDOC_DATE = poParam.DDOC_DATE.Value.ToString("yyyyMMdd");
                }
                catch (Exception)
                {

                }

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
                llCancel = string.IsNullOrWhiteSpace(poParam.CTENANT_ID);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V005"));
                }

                llCancel = string.IsNullOrWhiteSpace(poParam.CDUE_DATE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V006"));
                }

                //llCancel = string.IsNullOrWhiteSpace(poParam.CTENANT_SEQ_NO);
                //if (llCancel)
                //{
                //    loEx.Add(R_FrontUtility.R_GetError(
                //        typeof(Resources_Dummy_Class),
                //        "V007"));
                //}

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

        public async Task SaveInvoiceHeaderAsync(PMT50610DTO poEntity, eCRUDMode peCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            PMT50610ParameterDTO loParam = null;
            PMT50610ParameterDTO loResult = null;
            try
            {
                poEntity.CDUE_DATE = poEntity.DDUE_DATE.Value.ToString("yyyyMMdd");
                poEntity.CREF_DATE = poEntity.DREF_DATE.Value.ToString("yyyyMMdd");
                poEntity.CDOC_DATE = poEntity.DDOC_DATE.Value.ToString("yyyyMMdd");
                loParam = new PMT50610ParameterDTO()
                {
                    Data = poEntity,
                    //CPROPERTY_ID = loProperty.CPROPERTY_ID
                };
                loResult = await loModel.R_ServiceSaveAsync(loParam, peCRUDMode);

                loCreditNoteHeader = loResult.Data;

                try
                {
                    loCreditNoteHeader.DDUE_DATE = DateTime.ParseExact(loCreditNoteHeader.CDUE_DATE, "yyyyMMdd", null);
                }
                catch (Exception)
                {

                }
                try
                {
                    loCreditNoteHeader.DREF_DATE = DateTime.ParseExact(loCreditNoteHeader.CREF_DATE, "yyyyMMdd", null);
                }
                catch (Exception)
                {

                }
                try
                {
                    loCreditNoteHeader.DDOC_DATE = DateTime.ParseExact(loCreditNoteHeader.CDOC_DATE, "yyyyMMdd", null);
                }
                catch (Exception)
                {

                };

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteInvoiceHeaderAsync(PMT50610DTO poEntity)
        {
            R_Exception loEx = new R_Exception();
            PMT50610ParameterDTO loParam = null;
            try
            {
                loParam = new PMT50610ParameterDTO()
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
