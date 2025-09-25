using PMF00100COMMON.DTOs.PMF00100;
using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMF00100Model.ViewModel;
using PMF00100COMMON.DTOs.PMF00110;
using GLF00100FRONT;
using GLF00100COMMON;
using Lookup_PMFRONT;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using PMF00100Model.Constant;
using Lookup_PMCOMMON.DTOs;
using R_BlazorFrontEnd.Helpers;

namespace PMF00100FRONT
{
    public partial class PMF00110 : R_Page
    {
        private PMF00110ViewModel loViewModel = new PMF00110ViewModel();

        private R_Conductor _conductorRef;

        [Inject] private R_PopupService PopupService { get; set; }
        [Inject] private R_ILocalizer<PMF00100FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        private bool IsCallerDiscountEnable = false;

        private bool IsTargetDiscountEnable = false;

        private bool IsCallerAmountEnable = false;

        private bool IsTargetAmountEnable = false;

        private bool IsCallerTaxAmountEnable = false;

        private bool IsTargetTaxAmountEnable = false;


        private bool IsTransStatus00 = false;

        private bool IsTransStatus10 = false;

        private bool IsJournalButtonEnabled = false;

        private bool IsCRUDModeButtonHidden = false;

        private string PageWidth = "width: auto;";

        //private string CALLER_LABEL = "";

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                loViewModel.loAllocationEntryParameter = (OpenAllocationEntryParameterDTO)poParameter;
                IsCRUDModeButtonHidden = loViewModel.loAllocationEntryParameter.LDISPLAY_ONLY;
                if (loViewModel.loAllocationEntryParameter.LDISPLAY_ONLY)
                {
                    //PageWidth = "width: 1250px;";
                }
                else
                {
                    loViewModel.loAllocationDetail.CREC_ID = loViewModel.loAllocationEntryParameter.CALLOCATION_ID;
                }
                if (string.IsNullOrWhiteSpace(loViewModel.loAllocationEntryParameter.CALLOCATION_ID))
                {
                    return;
                }
                await loViewModel.InitialProcess();
                await loViewModel.GetTransactionTypeListStreamAsync();
                //if ((!string.IsNullOrWhiteSpace(loViewModel.loAllocationEntryParameter.CALLOCATION_ID) && !loViewModel.loAllocationEntryParameter.LDISPLAY_ONLY) || (!string.IsNullOrWhiteSpace(loViewModel.loAllocationEntryParameter.CREF_NO) && loViewModel.loAllocationEntryParameter.LDISPLAY_ONLY))
                if (!string.IsNullOrWhiteSpace(loViewModel.loAllocationEntryParameter.CALLOCATION_ID))
                {
                    await _conductorRef.R_GetEntity(new PMF00110DTO());
                    
                    int lnCompareResult = String.Compare(loViewModel.loAllocationDetail.CTRANS_STATUS, "00");
                    if (lnCompareResult > 0 && loViewModel.loAllocationDetail.CGL_REF_NO != "")
                    {
                        IsJournalButtonEnabled = true;
                    }
                }
                
                //RefreshFromProcess();
                //CALLER_LABEL = loViewModel.l
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Allocation_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
            int lnCompareResult = String.Compare(loViewModel.loAllocationDetail.CTRANS_STATUS, "00");
            if (lnCompareResult > 0 && loViewModel.loAllocationDetail.CGL_REF_NO != "" && eventArgs.Enable)
            {
                IsJournalButtonEnabled = true;
            }
            else
            {
                IsJournalButtonEnabled = false;
            }
        }


        private void RefreshFromProcess()
        {
            //CALLER_LABEL = loViewModel.loCallerTrxInfo.CTRANSACTION_NAME;
            PMF00110DTO loData = (PMF00110DTO)_conductorRef.R_GetCurrentData();
            loData.CTRANSACTION_NAME = loViewModel.loCallerTrxInfo.CTRANSACTION_NAME;

            loData.NCALLER_REMAINING = loViewModel.loCallerTrxInfo.NAR_REMAINING;
            loData.NCALLER_TAX_REMAINING = loViewModel.loCallerTrxInfo.NTAX_REMAINING;
            loData.NCALLER_TOTAL_REMAINING = loViewModel.loCallerTrxInfo.NTOTAL_REMAINING;
            loData.NCALLER_TAX_RATE = loViewModel.loCallerTrxInfo.NTAX_CURRENCY_RATE;
            loData.NCALLER_TAX_BASE_RATE = loViewModel.loCallerTrxInfo.NTAX_BASE_RATE;

            loData.NLCALLER_REMAINING = loViewModel.loCallerTrxInfo.NLAR_REMAINING;
            loData.NLCALLER_TAX_REMAINING = loViewModel.loCallerTrxInfo.NLTAX_REMAINING;
            loData.NLCALLER_TOTAL_REMAINING = loViewModel.loCallerTrxInfo.NLTOTAL_REMAINING;
            loData.NLCALLER_BASE_RATE = loViewModel.loCallerTrxInfo.NLBASE_RATE;

            loData.NBCALLER_REMAINING = loViewModel.loCallerTrxInfo.NBAR_REMAINING;
            loData.NBCALLER_TAX_REMAINING = loViewModel.loCallerTrxInfo.NBTAX_REMAINING;
            loData.NBCALLER_TOTAL_REMAINING = loViewModel.loCallerTrxInfo.NBTOTAL_REMAINING;
            loData.NBCALLER_BASE_RATE = loViewModel.loCallerTrxInfo.NBBASE_RATE;

            loData.NBCALLER_CURRENCY_RATE = loViewModel.loCallerTrxInfo.NBCURRENCY_RATE;

            //CALLER_LABEL = loViewModel.loCallerTrxInfo.CTRANSACTION_NAME;
        }

        private async Task Allocation_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMF00110DTO loData = (PMF00110DTO)eventArgs.Data;
                await loViewModel.GetAllocationAsync(loData);
                TransStatusChanged();
                eventArgs.Result = loViewModel.loAllocationDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Allocation_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.DeleteAllocationAsync((PMF00110DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Allocation_AfterDelete()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loViewModel.loAllocationDetail = new PMF00110DTO();
                TransStatusChanged();
                await R_MessageBox.Show("", _localizer["M005"], R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void Allocation_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.ValidationAllocation((PMF00110DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Allocation_BeforeDelete(R_BeforeDeleteEventArgs eventArgs)
        {
            //await _conductorRef.R_GetEntity(new PMF00110DTO { CREC_ID = loViewModel.loAllocationDetail.CREC_ID });
            R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["M001"], R_eMessageBoxButtonType.YesNo);
            if (loValidate == R_eMessageBoxResult.No)
            {
                eventArgs.Cancel = true;
            }
        }

        private async Task Allocation_BeforeAdd(R_BeforeAddEventArgs eventArgs)
        {

        }

        private void Allocation_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                PMF00110DTO loData = (PMF00110DTO)eventArgs.Data;
                loData.NCALLER_AMOUNT = loViewModel.loCallerTrxInfo.NAR_REMAINING;
                loData.NCALLER_REMAINING = loViewModel.loCallerTrxInfo.NAR_REMAINING;
                loData.NCALLER_TAX_AMOUNT = loViewModel.loCallerTrxInfo.NTAX_REMAINING;
                loData.NCALLER_TAX_REMAINING = loViewModel.loCallerTrxInfo.NTAX_REMAINING;
                loData.NCALLER_TOTAL_REMAINING = loViewModel.loCallerTrxInfo.NTOTAL_REMAINING;
                loData.CCALLER_CURRENCY_CODE = loViewModel.loCallerTrxInfo.CCURRENCY_CODE;

                loData.NLCALLER_AMOUNT = loViewModel.loCallerTrxInfo.NLAR_REMAINING;
                loData.NLCALLER_REMAINING = loViewModel.loCallerTrxInfo.NLAR_REMAINING;
                loData.NLCALLER_TAX_AMOUNT = loViewModel.loCallerTrxInfo.NLTAX_REMAINING;
                loData.NLCALLER_TAX_REMAINING = loViewModel.loCallerTrxInfo.NLTAX_REMAINING;
                loData.NLCALLER_TOTAL_REMAINING = loViewModel.loCallerTrxInfo.NLTOTAL_REMAINING;

                loData.NBCALLER_AMOUNT = loViewModel.loCallerTrxInfo.NBAR_REMAINING;
                loData.NBCALLER_REMAINING = loViewModel.loCallerTrxInfo.NBAR_REMAINING;
                loData.NBCALLER_TAX_AMOUNT = loViewModel.loCallerTrxInfo.NBTAX_REMAINING;
                loData.NBCALLER_TAX_REMAINING = loViewModel.loCallerTrxInfo.NBTAX_REMAINING;
                loData.NBCALLER_TOTAL_REMAINING = loViewModel.loCallerTrxInfo.NBTOTAL_REMAINING;

                loData.NLCALLER_BASE_RATE = loViewModel.loCallerTrxInfo.NLBASE_RATE;
                loData.NLCALLER_CURRENCY_RATE = loViewModel.loCallerTrxInfo.NLCURRENCY_RATE;
                loData.NCALLER_TAX_BASE_RATE = loViewModel.loCallerTrxInfo.NTAX_BASE_RATE;
                loData.NCALLER_TAX_RATE = loViewModel.loCallerTrxInfo.NTAX_CURRENCY_RATE;
                loData.NBCALLER_BASE_RATE = loViewModel.loCallerTrxInfo.NBBASE_RATE;
                loData.NBCALLER_CURRENCY_RATE = loViewModel.loCallerTrxInfo.NBCURRENCY_RATE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void Allocation_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
        }

        private async Task Allocation_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["M004"], R_eMessageBoxButtonType.YesNo);
            if (loValidate == R_eMessageBoxResult.No)
            {
                eventArgs.Cancel = true;
            }
        }

        private void Allocation_Saving(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        private void Allocation_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();   
            R_PopupResult loResultDetail = null;
            R_PopupResult loResultSummary = null;

            try
            {
                //RefreshFromProcess();
                TransStatusChanged();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Allocation_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await loViewModel.SaveAllocationAsync((PMF00110DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = loViewModel.loAllocationDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region OnChange

        private async Task  TransactionType_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.CTARGET_TRANS_CODE = poParam;
                loViewModel.Data.CREF_NO = "";
                loViewModel.Data.CREF_DATE = "";
                loViewModel.Data.DREF_DATE = DateTime.Today;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task TargetAmount_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.NTARGET_AMOUNT = poParam;
                CalculateAllocationProcess();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void TargetTaxAmount_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.NTARGET_TAX_AMOUNT = poParam;
                CalculateAllocationProcess();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void TargetDiscountAmount_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.NTARGET_DISC_AMOUNT = poParam;
                CalculateAllocationProcess();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void CallerAmount_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.NCALLER_AMOUNT = poParam;
                CalculateAllocationProcess();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void CallerTaxAmount_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.NCALLER_TAX_AMOUNT = poParam;
                CalculateAllocationProcess();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void CallerDiscountAmount_ValueChanged(decimal poParam)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.Data.NCALLER_DISC_AMOUNT = poParam;
                CalculateAllocationProcess();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }



        //private async Task CurrencyComboBox_ValueChanged(string poParam)
        //{
        //    R_Exception loEx = new R_Exception();

        //    try
        //    {
        //        loViewModel.loCurrencyOrTaxRateParameter.CCURRENCY_CODE = poParam;
        //        loViewModel.loCurrencyOrTaxRateParameter.CRATETYPE_CODE = loViewModel.loAPSystemParam.CCUR_RATETYPE_CODE;
        //        loViewModel.loCurrencyOrTaxRateParameter.CREF_DATE = loViewModel.Data.DREF_DATE.ToString("yyyyMMdd");
        //        loViewModel.Data.CCURRENCY_CODE = poParam;
        //        await loViewModel.GetCurrencyRateAsync();
        //        if (loViewModel.loCurrencyRate != null)
        //        {
        //            loViewModel.Data.NLBASE_RATE = loViewModel.loCurrencyRate.NLBASE_RATE_AMOUNT;
        //            loViewModel.Data.NLCURRENCY_RATE = loViewModel.loCurrencyRate.NLCURRENCY_RATE_AMOUNT;
        //            loViewModel.Data.NBBASE_RATE = loViewModel.loCurrencyRate.NBBASE_RATE_AMOUNT;
        //            loViewModel.Data.NBCURRENCY_RATE = loViewModel.loCurrencyRate.NBCURRENCY_RATE_AMOUNT;
        //        }
        //        else
        //        {
        //            loViewModel.Data.NLBASE_RATE = 1;
        //            loViewModel.Data.NLCURRENCY_RATE = 1;
        //            loViewModel.Data.NBBASE_RATE = 1;
        //            loViewModel.Data.NBCURRENCY_RATE = 1;
        //        }

        //        loViewModel.loCurrencyOrTaxRateParameter.CRATETYPE_CODE = loViewModel.loAPSystemParam.CTAX_RATETYPE_CODE;
        //        await loViewModel.GetTaxRateAsync();
        //        if (loViewModel.loTaxRate != null)
        //        {
        //            loViewModel.Data.NTAX_BASE_RATE = loViewModel.loTaxRate.NLBASE_RATE_AMOUNT;
        //            loViewModel.Data.NTAX_CURRENCY_RATE = loViewModel.loTaxRate.NLCURRENCY_RATE_AMOUNT;
        //        }
        //        else
        //        {
        //            loViewModel.Data.NTAX_BASE_RATE = 1;
        //            loViewModel.Data.NTAX_CURRENCY_RATE = 1;
        //        }

        //        if (loViewModel.Data.CCURRENCY_CODE != loViewModel.Data.CLOCAL_CURRENCY_CODE)
        //        {
        //            IsLocalCurrencyEnabled = true;
        //            if (loViewModel.Data.LTAXABLE == true)
        //            {
        //                IsTaxCurrencyEnabled = true;
        //            }
        //            else
        //            {
        //                IsTaxCurrencyEnabled = false;
        //            }
        //        }
        //        else
        //        {
        //            IsTaxCurrencyEnabled = false;
        //            IsLocalCurrencyEnabled = false;
        //        }

        //        if (loViewModel.Data.CCURRENCY_CODE != loViewModel.Data.CBASE_CURRENCY_CODE)
        //        {
        //            IsBaseCurrencyEnabled = true;
        //        }
        //        else
        //        {
        //            IsBaseCurrencyEnabled = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }
        //    loEx.ThrowExceptionIfErrors();
        //}
        #endregion


        #region Lookup
        private void R_Before_Open_LookupReferenceNo(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00900ParameterDTO loParam = new LML00900ParameterDTO()
            {
                CPROPERTY_ID = loViewModel.loAllocationEntryParameter.CPROPERTY_ID,
                CDEPT_CODE = loViewModel.loAllocationEntryParameter.CDEPT_CODE,
                CTENANT_ID = loViewModel.loCallerTrxInfo.CTENANT_ID,
                CTRANS_CODE = loViewModel.Data.CTARGET_TRANS_CODE,
                CTRANS_NAME = loViewModel.loCallerTrxInfo.CTRANSACTION_NAME,
                LHAS_REMAINING = true,
                LNO_REMAINING = false
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(LML00900);
        }

        private void R_After_Open_LookupReferenceNo(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                LML00900FrontDTO loTempResult = R_FrontUtility.ConvertObjectToObject<LML00900FrontDTO>(eventArgs.Result);
                if (loTempResult == null)
                {
                    return;
                }
                var loGetData = (PMF00110DTO)_conductorRef.R_GetCurrentData();
                loViewModel.Data.CTARGET_REC_ID = loTempResult.CREC_ID;
                loViewModel.Data.CTARGET_REF_NO = loTempResult.CREF_NO;
                //loViewModel.Data.CTARGET_REF_DATE = loTempResult.CREF_DATE;
                //loViewModel.Data.DTARGET_REF_DATE = DateTime.ParseExact(loViewModel.Data.CTARGET_REF_DATE, "yyyyMMdd", null);
                loViewModel.Data.CTARGET_REF_DATE = loTempResult.DREF_DATE.Value.ToString("yyyyMMdd");
                loViewModel.Data.DTARGET_REF_DATE = loTempResult.DREF_DATE;
                loViewModel.Data.CTARGET_DEPT_CODE = loTempResult.CDEPT_CODE;
                loViewModel.Data.CTARGET_DEPT_NAME = loTempResult.CDEPT_NAME;

                loViewModel.Data.NTARGET_REMAINING = loTempResult.NAR_REMAINING;
                loViewModel.Data.NLTARGET_REMAINING = loTempResult.NLAR_REMAINING;
                loViewModel.Data.NBTARGET_REMAINING = loTempResult.NBAR_REMAINING;

                loViewModel.Data.NTARGET_TAX_REMAINING = loTempResult.NTAX_REMAINING;
                loViewModel.Data.NLTARGET_TAX_REMAINING = loTempResult.NLTAX_REMAINING;
                loViewModel.Data.NBTARGET_TAX_REMAINING = loTempResult.NBTAX_REMAINING;

                loViewModel.Data.NTARGET_TOTAL_REMAINING = loTempResult.NTOTAL_REMAINING;
                loViewModel.Data.NLTARGET_TOTAL_REMAINING = loTempResult.NLTOTAL_REMAINING;
                loViewModel.Data.NBTARGET_TOTAL_REMAINING = loTempResult.NBTOTAL_REMAINING;

                loViewModel.Data.CTARGET_CURRENCY_CODE = loTempResult.CCURRENCY_CODE;

                loViewModel.Data.NLTARGET_BASE_RATE = loTempResult.NLBASE_RATE;
                loViewModel.Data.NLTARGET_CURRENCY_RATE = loTempResult.NLCURRENCY_RATE;

                loViewModel.Data.NBTARGET_BASE_RATE = loTempResult.NBBASE_RATE;
                loViewModel.Data.NBTARGET_CURRENCY_RATE = loTempResult.NBCURRENCY_RATE;

                loViewModel.Data.NTARGET_TAX_BASE_RATE = loTempResult.NTAX_BASE_RATE;
                loViewModel.Data.NTARGET_TAX_RATE = loTempResult.NTAX_CURRENCY_RATE;

                if (loViewModel.Data.CTARGET_CURRENCY_CODE != loViewModel.loCompanyInfo.CLOCAL_CURRENCY_CODE ||
                    loViewModel.Data.CTARGET_CURRENCY_CODE != loViewModel.loCompanyInfo.CBASE_CURRENCY_CODE ||
                    loViewModel.Data.CCALLER_CURRENCY_CODE != loViewModel.loCompanyInfo.CLOCAL_CURRENCY_CODE ||
                    loViewModel.Data.CCALLER_CURRENCY_CODE != loViewModel.loCompanyInfo.CBASE_CURRENCY_CODE)
                {
                    loViewModel.Data.LSINGLE_CURRENCY = false;
                }
                else
                {
                    loViewModel.Data.LSINGLE_CURRENCY = true;
                }
                InitialAllocationProcess();
                CalculateAllocationProcess();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void InitialAllocationProcess()
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (loViewModel.loAllocationEntryParameter.CTRANS_CODE == TransCodeConstant.VAR_SALES_INVOICE)
                {
                    IsCallerDiscountEnable = true;
                }
                else
                {
                    IsCallerDiscountEnable = false;
                }
                if (loViewModel.Data.CTARGET_TRANS_CODE == TransCodeConstant.VAR_SALES_INVOICE)
                {
                    IsTargetDiscountEnable = true;
                }
                else
                {
                    IsTargetDiscountEnable = false;
                }
                if (loViewModel.Data.LSINGLE_CURRENCY)
                {
                    IsCallerAmountEnable = false;
                    IsCallerTaxAmountEnable = false;
                    if (loViewModel.Data.NTARGET_TAX_REMAINING > 0 && loViewModel.Data.NCALLER_TAX_REMAINING > 0)
                    {
                        IsTargetTaxAmountEnable = true;
                        if (loViewModel.Data.NTARGET_TAX_REMAINING >= loViewModel.Data.NCALLER_TAX_REMAINING)
                        {
                            loViewModel.Data.NTARGET_TAX_AMOUNT = loViewModel.Data.NCALLER_TAX_REMAINING;
                            loViewModel.Data.NCALLER_TAX_AMOUNT = loViewModel.Data.NCALLER_TAX_REMAINING;
                        }
                        else
                        {
                            loViewModel.Data.NTARGET_TAX_AMOUNT = loViewModel.Data.NTARGET_TAX_REMAINING;
                            loViewModel.Data.NCALLER_TAX_AMOUNT = loViewModel.Data.NTARGET_TAX_REMAINING;
                        }
                        if (loViewModel.Data.NTARGET_REMAINING >= loViewModel.Data.NCALLER_REMAINING)
                        {
                            loViewModel.Data.NTARGET_AMOUNT = loViewModel.Data.NCALLER_REMAINING;
                            loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NCALLER_REMAINING;
                        }
                        else
                        {
                            loViewModel.Data.NTARGET_AMOUNT = loViewModel.Data.NTARGET_REMAINING;
                            loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NTARGET_REMAINING;
                        }
                    }
                    else if (loViewModel.Data.NTARGET_TAX_REMAINING > 0 && loViewModel.Data.NCALLER_TAX_REMAINING == 0)
                    {
                        if (loViewModel.loAllocationEntryParameter.CTRANS_CODE == TransCodeConstant.VAR_RECEIVE_FROM_CUSTOMER)
                        {
                            IsTargetTaxAmountEnable = true;
                            if (loViewModel.Data.NCALLER_REMAINING >= loViewModel.Data.NTARGET_TAX_REMAINING)
                            {
                                loViewModel.Data.NTARGET_TAX_AMOUNT = loViewModel.Data.NTARGET_TAX_REMAINING;
                                if ((loViewModel.Data.NCALLER_REMAINING - loViewModel.Data.NTARGET_TAX_REMAINING) >= loViewModel.Data.NTARGET_REMAINING)
                                {
                                    loViewModel.Data.NTARGET_AMOUNT = loViewModel.Data.NTARGET_REMAINING;
                                    loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NTARGET_REMAINING + loViewModel.Data.NTARGET_TAX_REMAINING;
                                }
                                else
                                {
                                    loViewModel.Data.NTARGET_AMOUNT = loViewModel.Data.NCALLER_REMAINING - loViewModel.Data.NTARGET_TAX_REMAINING;
                                    loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NTARGET_REMAINING + loViewModel.Data.NTARGET_TAX_REMAINING;
                                }
                            }
                            else
                            {
                                loViewModel.Data.NTARGET_TAX_AMOUNT = loViewModel.Data.NCALLER_REMAINING;
                                loViewModel.Data.NTARGET_AMOUNT = 0;
                                loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NCALLER_REMAINING;
                            }
                        }
                        else
                        {
                            IsTargetTaxAmountEnable = false;
                            loViewModel.Data.NCALLER_TAX_AMOUNT = 0;
                            if (loViewModel.Data.NTARGET_REMAINING >= loViewModel.Data.NCALLER_REMAINING)
                            {
                                loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NCALLER_REMAINING;
                                loViewModel.Data.NTARGET_AMOUNT = loViewModel.Data.NCALLER_REMAINING;
                            }
                            else
                            {
                                loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NTARGET_REMAINING;
                                loViewModel.Data.NTARGET_AMOUNT = loViewModel.Data.NTARGET_REMAINING;
                            }
                        }
                        //if (loViewModel.Data.NTARGET_TAX_REMAINING >= loViewModel.Data.NCALLER_REMAINING)
                        //{
                        //    loViewModel.Data.NCALLER_TAX_AMOUNT = loViewModel.Data.NCALLER_REMAINING;
                        //    loViewModel.Data.NTARGET_AMOUNT = 0;
                        //    loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NCALLER_REMAINING;
                        //}
                        //else
                        //{
                        //    loViewModel.Data.NTARGET_TAX_AMOUNT = loViewModel.Data.NTARGET_TAX_REMAINING;
                        //    if (loViewModel.Data.NTARGET_REMAINING >= (loViewModel.Data.NCALLER_REMAINING - loViewModel.Data.NTARGET_TAX_REMAINING))
                        //    {
                        //        loViewModel.Data.NTARGET_AMOUNT = loViewModel.Data.NCALLER_REMAINING - loViewModel.Data.NTARGET_TAX_REMAINING;
                        //        loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NCALLER_REMAINING;
                        //    }
                        //    else
                        //    {
                        //        loViewModel.Data.NTARGET_AMOUNT = loViewModel.Data.NTARGET_REMAINING;
                        //        loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NTARGET_REMAINING + loViewModel.Data.NTARGET_TAX_REMAINING;
                        //    }
                        //}
                    }
                    else if (loViewModel.Data.NTARGET_TAX_REMAINING == 0 && loViewModel.Data.NCALLER_TAX_REMAINING > 0)
                    {
                        IsTargetTaxAmountEnable = false;
                        if (loViewModel.Data.CTARGET_TRANS_CODE == TransCodeConstant.VAR_RECEIVE_FROM_CUSTOMER)
                        {
                            if (loViewModel.Data.NTARGET_REMAINING >= loViewModel.Data.NCALLER_TAX_REMAINING)
                            {
                                loViewModel.Data.NCALLER_TAX_AMOUNT = loViewModel.Data.NCALLER_TAX_REMAINING;
                                if ((loViewModel.Data.NTARGET_REMAINING - loViewModel.Data.NCALLER_TAX_REMAINING) >= loViewModel.Data.NCALLER_REMAINING)
                                {
                                    loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NCALLER_REMAINING;
                                    loViewModel.Data.NTARGET_AMOUNT = loViewModel.Data.NCALLER_REMAINING + loViewModel.Data.NCALLER_TAX_REMAINING;
                                }
                                else
                                {
                                    loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NTARGET_REMAINING - loViewModel.Data.NCALLER_TAX_REMAINING;
                                    loViewModel.Data.NTARGET_AMOUNT = loViewModel.Data.NTARGET_REMAINING;
                                }
                            }
                            else
                            {
                                loViewModel.Data.NCALLER_TAX_AMOUNT = loViewModel.Data.NTARGET_REMAINING;
                                loViewModel.Data.NCALLER_AMOUNT = 0;
                                loViewModel.Data.NTARGET_AMOUNT = loViewModel.Data.NTARGET_REMAINING;
                            }
                        }
                        else
                        {
                            loViewModel.Data.NCALLER_TAX_AMOUNT = 0;
                            if (loViewModel.Data.NTARGET_REMAINING >= loViewModel.Data.NCALLER_REMAINING)
                            {
                                loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NCALLER_REMAINING;
                                loViewModel.Data.NTARGET_AMOUNT = loViewModel.Data.NCALLER_REMAINING;
                            }
                            else
                            {
                                loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NTARGET_REMAINING;
                                loViewModel.Data.NTARGET_AMOUNT = loViewModel.Data.NTARGET_REMAINING;
                            }
                        }
                        //if (loViewModel.Data.NTARGET_REMAINING >= loViewModel.Data.NCALLER_TAX_REMAINING)
                        //{
                        //    loViewModel.Data.NCALLER_TAX_AMOUNT = loViewModel.Data.NCALLER_TAX_REMAINING;
                        //    if ((loViewModel.Data.NTARGET_REMAINING - loViewModel.Data.NCALLER_TAX_REMAINING) >= loViewModel.Data.NCALLER_REMAINING)
                        //    {
                        //        loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NCALLER_REMAINING;
                        //        loViewModel.Data.NTARGET_AMOUNT = loViewModel.Data.NCALLER_REMAINING + loViewModel.Data.NCALLER_TAX_REMAINING;
                        //    }
                        //    else
                        //    {
                        //        loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NTARGET_REMAINING - loViewModel.Data.NCALLER_TAX_REMAINING;
                        //        loViewModel.Data.NTARGET_AMOUNT = loViewModel.Data.NCALLER_REMAINING + loViewModel.Data.NCALLER_TAX_REMAINING;
                        //    }
                        //}
                        //else
                        //{
                        //    loViewModel.Data.NCALLER_TAX_AMOUNT = loViewModel.Data.NTARGET_REMAINING;
                        //    loViewModel.Data.NCALLER_AMOUNT = 0;
                        //    loViewModel.Data.NTARGET_TAX_AMOUNT = loViewModel.Data.NTARGET_REMAINING;
                        //}
                    }
                    else if (loViewModel.Data.NTARGET_TAX_REMAINING == 0 && loViewModel.Data.NCALLER_TAX_REMAINING == 0)
                    {
                        IsTargetTaxAmountEnable = false;
                        if (loViewModel.Data.NTARGET_REMAINING >= loViewModel.Data.NCALLER_REMAINING)
                        {
                            loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NCALLER_REMAINING;
                            loViewModel.Data.NTARGET_AMOUNT = loViewModel.Data.NCALLER_REMAINING;
                        }
                        else
                        {
                            loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NTARGET_REMAINING;
                            loViewModel.Data.NTARGET_AMOUNT = loViewModel.Data.NTARGET_REMAINING;
                        }
                    }
                    else
                    {
                        IsCallerAmountEnable = true;
                        if (loViewModel.Data.NTARGET_TAX_REMAINING > 0 && loViewModel.Data.NCALLER_TAX_REMAINING > 0)
                        {
                            IsTargetTaxAmountEnable = true;
                            IsCallerTaxAmountEnable = true;
                        }
                        else if (loViewModel.Data.NTARGET_TAX_REMAINING > 0 && loViewModel.Data.NCALLER_TAX_REMAINING == 0)
                        {
                            IsCallerTaxAmountEnable = false;
                            if (loViewModel.loAllocationEntryParameter.CTRANS_CODE == TransCodeConstant.VAR_RECEIVE_FROM_CUSTOMER)
                            {
                                IsTargetTaxAmountEnable = true;
                            }
                            else
                            {
                                IsTargetTaxAmountEnable = false;
                                loViewModel.Data.NTARGET_TAX_AMOUNT = 0;
                            }
                        }
                        else if (loViewModel.Data.NTARGET_TAX_REMAINING == 0 && loViewModel.Data.NCALLER_TAX_REMAINING > 0)
                        {
                            IsTargetTaxAmountEnable = false;
                            loViewModel.Data.NTARGET_TAX_AMOUNT = 0;
                            if (loViewModel.Data.CTARGET_TRANS_CODE == TransCodeConstant.VAR_RECEIVE_FROM_CUSTOMER)
                            {
                                IsCallerTaxAmountEnable = true;
                            }
                            else
                            {
                                IsCallerTaxAmountEnable = false;
                            }
                        }
                        else if (loViewModel.Data.NTARGET_TAX_REMAINING == 0 && loViewModel.Data.NCALLER_TAX_REMAINING == 0)
                        {
                            IsTargetTaxAmountEnable = false;
                            IsCallerTaxAmountEnable = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            R_DisplayException(loException);
        }

        private void CalculateAllocationProcess()
        {
            R_Exception loException = new R_Exception();
            try
            {
                loViewModel.Data.NTARGET_ALLOCATION = loViewModel.Data.NTARGET_AMOUNT + loViewModel.Data.NTARGET_TAX_AMOUNT - loViewModel.Data.NTARGET_DISC_AMOUNT;
                loViewModel.Data.NCALLER_ALLOCATION = loViewModel.Data.NCALLER_AMOUNT + loViewModel.Data.NCALLER_TAX_AMOUNT - loViewModel.Data.NCALLER_DISC_AMOUNT;

                loViewModel.Data.NLTARGET_AMOUNT = (loViewModel.Data.NTARGET_AMOUNT / loViewModel.Data.NLTARGET_BASE_RATE) * loViewModel.Data.NLTARGET_CURRENCY_RATE;
                loViewModel.Data.NBTARGET_AMOUNT = (loViewModel.Data.NTARGET_AMOUNT / loViewModel.Data.NBTARGET_BASE_RATE) * loViewModel.Data.NBTARGET_CURRENCY_RATE;
                loViewModel.Data.NLTARGET_TAX_AMOUNT = (loViewModel.Data.NTARGET_TAX_AMOUNT / loViewModel.Data.NTARGET_TAX_BASE_RATE) * loViewModel.Data.NTARGET_TAX_RATE;
                loViewModel.Data.NBTARGET_TAX_AMOUNT = (loViewModel.Data.NTARGET_TAX_AMOUNT / loViewModel.Data.NBTARGET_BASE_RATE) * loViewModel.Data.NBTARGET_CURRENCY_RATE;
                loViewModel.Data.NLTARGET_DISC_AMOUNT = (loViewModel.Data.NTARGET_DISC_AMOUNT / loViewModel.Data.NLTARGET_BASE_RATE) * loViewModel.Data.NLTARGET_CURRENCY_RATE;
                loViewModel.Data.NBTARGET_DISC_AMOUNT = (loViewModel.Data.NTARGET_DISC_AMOUNT / loViewModel.Data.NBTARGET_BASE_RATE) * loViewModel.Data.NBTARGET_CURRENCY_RATE;
                loViewModel.Data.NTARGET_ALLOCATION = loViewModel.Data.NTARGET_AMOUNT + loViewModel.Data.NTARGET_TAX_AMOUNT - loViewModel.Data.NTARGET_DISC_AMOUNT;

                loViewModel.Data.NLCALLER_AMOUNT = (loViewModel.Data.NCALLER_AMOUNT / loViewModel.Data.NLCALLER_BASE_RATE) * loViewModel.Data.NLCALLER_CURRENCY_RATE;
                loViewModel.Data.NBCALLER_AMOUNT = (loViewModel.Data.NCALLER_AMOUNT / loViewModel.Data.NBCALLER_BASE_RATE) * loViewModel.Data.NBCALLER_CURRENCY_RATE;
                loViewModel.Data.NLCALLER_TAX_AMOUNT = (loViewModel.Data.NCALLER_TAX_AMOUNT / loViewModel.Data.NCALLER_TAX_BASE_RATE) * loViewModel.Data.NCALLER_TAX_RATE;
                loViewModel.Data.NBCALLER_TAX_AMOUNT = (loViewModel.Data.NCALLER_TAX_AMOUNT / loViewModel.Data.NBCALLER_BASE_RATE) * loViewModel.Data.NBCALLER_CURRENCY_RATE;
                loViewModel.Data.NLCALLER_DISC_AMOUNT = (loViewModel.Data.NCALLER_DISC_AMOUNT / loViewModel.Data.NLCALLER_BASE_RATE) * loViewModel.Data.NLCALLER_CURRENCY_RATE;
                loViewModel.Data.NBCALLER_DISC_AMOUNT = (loViewModel.Data.NCALLER_DISC_AMOUNT / loViewModel.Data.NBCALLER_BASE_RATE) * loViewModel.Data.NBCALLER_CURRENCY_RATE;
                loViewModel.Data.NCALLER_ALLOCATION = loViewModel.Data.NCALLER_AMOUNT + loViewModel.Data.NCALLER_TAX_AMOUNT - loViewModel.Data.NCALLER_DISC_AMOUNT;

                if (loViewModel.loAllocationEntryParameter.CTRANS_CODE == TransCodeConstant.VAR_SALES_INVOICE || loViewModel.loAllocationEntryParameter.CTRANS_CODE == TransCodeConstant.VAR_SALES_CREDIT_NOTE)
                {
                    loViewModel.Data.NLFOREX_GAINLOSS = (loViewModel.Data.NLCALLER_AMOUNT + loViewModel.Data.NLCALLER_TAX_AMOUNT - loViewModel.Data.NLCALLER_DISC_AMOUNT) - (loViewModel.Data.NLTARGET_AMOUNT + loViewModel.Data.NLTARGET_TAX_AMOUNT - loViewModel.Data.NLTARGET_DISC_AMOUNT);
                    loViewModel.Data.NBFOREX_GAINLOSS = (loViewModel.Data.NBCALLER_AMOUNT + loViewModel.Data.NBCALLER_TAX_AMOUNT - loViewModel.Data.NBCALLER_DISC_AMOUNT) - (loViewModel.Data.NBTARGET_AMOUNT + loViewModel.Data.NBTARGET_TAX_AMOUNT - loViewModel.Data.NBTARGET_DISC_AMOUNT);
                }
                else
                {
                    loViewModel.Data.NLFOREX_GAINLOSS = (loViewModel.Data.NLTARGET_AMOUNT + loViewModel.Data.NLTARGET_TAX_AMOUNT - loViewModel.Data.NLTARGET_DISC_AMOUNT) - (loViewModel.Data.NLCALLER_AMOUNT + loViewModel.Data.NLCALLER_TAX_AMOUNT - loViewModel.Data.NLCALLER_DISC_AMOUNT);
                    loViewModel.Data.NBFOREX_GAINLOSS = (loViewModel.Data.NBTARGET_AMOUNT + loViewModel.Data.NBTARGET_TAX_AMOUNT - loViewModel.Data.NBTARGET_DISC_AMOUNT) - (loViewModel.Data.NBCALLER_AMOUNT + loViewModel.Data.NBCALLER_TAX_AMOUNT - loViewModel.Data.NBCALLER_DISC_AMOUNT);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            R_DisplayException(loException);
        }
        #endregion

        #region Button
        private async Task OnClickTax()
        {
            R_Exception loEx = new R_Exception();
            try
            {
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickClose()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await this.Close(true, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void TransStatusChanged()
        {
            if (loViewModel.loAllocationDetail.CTRANS_STATUS == "00")
            {
                IsTransStatus00 = true;
            }
            else
            {
                IsTransStatus00 = false;
            }

            if (loViewModel.loAllocationDetail.CTRANS_STATUS == "10")
            {
                IsTransStatus10 = true;
            }
            else
            {
                IsTransStatus10 = false;
            }
        }

        private async Task OnClickSubmit()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["M002"], R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.No)
                {
                    return;
                }

                await loViewModel.SubmitAllocationProcessAsync();
                if (!loEx.HasError && !string.IsNullOrEmpty(loViewModel.loAllocationDetail.CREC_ID))
                {
                    await _conductorRef.R_GetEntity(new PMF00110DTO { CREC_ID = loViewModel.loAllocationDetail.CREC_ID });
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickRedraft()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["M003"], R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.No)
                {
                    return;
                }

                await loViewModel.RedraftAllocationProcessAsync();
                if (!loEx.HasError && !string.IsNullOrEmpty(loViewModel.loAllocationDetail.CREC_ID))
                {
                    await _conductorRef.R_GetEntity(new PMF00110DTO { CREC_ID = loViewModel.loAllocationDetail.CREC_ID });
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Before_Open_Journal_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GLF00100ParameterDTO()
            {
                CDEPT_CODE = loViewModel.loAllocationEntryParameter.CDEPT_CODE,
                CTRANS_CODE = TransCodeConstant.VAR_TRANS_CODE,
                CREF_NO = loViewModel.loAllocationDetail.CGL_REF_NO
            };

            eventArgs.TargetPageType = typeof(GLF00100);
        }

        private void After_Open_Journal_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
        }

        #endregion
    }
}
