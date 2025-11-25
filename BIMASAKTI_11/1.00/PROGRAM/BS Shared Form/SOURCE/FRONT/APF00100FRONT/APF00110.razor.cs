using APF00100COMMON.DTOs.APF00100;
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
using APF00100Model.ViewModel;
using APF00100COMMON.DTOs.APF00110;
using GLF00100FRONT;
using GLF00100COMMON;
using Lookup_APFRONT;
using Lookup_APCOMMON.DTOs.APL00500;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using APF00100Model.Constant;
using R_BlazorFrontEnd.Helpers;
using GFF00900COMMON.DTOs;
using Lookup_APModel.ViewModel.APL00500;
using GFF00900Model;

namespace APF00100FRONT
{
    public partial class APF00110 : R_Page
    {
        private APF00110ViewModel loViewModel = new APF00110ViewModel();

        private R_Conductor _conductorRef;

        [Inject] private R_PopupService PopupService { get; set; }
        [Inject] private R_ILocalizer<APF00100FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        private bool IsCallerDiscountEnable = false;

        private bool IsTargetDiscountEnable = false;

        private bool IsCallerAmountEnable = false;

        private bool IsTargetAmountEnable = false;

        private bool IsCallerTaxAmountEnable = false;

        private bool IsTargetTaxAmountEnable = false;

        private bool IsFieldEnabled = false;


        private bool IsTransStatus00 = false;
        private bool EnableDelete = false;

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
                await loViewModel.InitialProcess();
                await loViewModel.GetTransactionTypeListStreamAsync();
                if (loViewModel.loAllocationEntryParameter.LDISPLAY_ONLY)
                {
                    //PageWidth = "width: 1250px;";
                    loViewModel.loAllocationDetail.CREC_ID = loViewModel.loAllocationEntryParameter.CALLOCATION_ID;
                }
                else
                {
                    loViewModel.loAllocationDetail.CREC_ID = loViewModel.loAllocationEntryParameter.CALLOCATION_ID;
                }
                if (string.IsNullOrWhiteSpace(loViewModel.loAllocationEntryParameter.CREC_ID))
                {
                    return;
                }
                //if ((!string.IsNullOrWhiteSpace(loViewModel.loAllocationEntryParameter.CALLOCATION_ID) && !loViewModel.loAllocationEntryParameter.LDISPLAY_ONLY) || (!string.IsNullOrWhiteSpace(loViewModel.loAllocationEntryParameter.CREF_NO) && loViewModel.loAllocationEntryParameter.LDISPLAY_ONLY))
                if (!string.IsNullOrWhiteSpace(loViewModel.loAllocationEntryParameter.CALLOCATION_ID))
                {
                    await _conductorRef.R_GetEntity(new APF00110DTO());
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
        }


        private async Task Allocation_SetEdit(R_SetEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Enable)
                {
                    IsFieldEnabled = true;

                    if (loViewModel.Data.CTARGET_CURRENCY_CODE != loViewModel.Data.CCALLER_CURRENCY_CODE)
                    {
                        loViewModel.IsSingleCurrency = false;
                    }
                    else
                    {
                        loViewModel.IsSingleCurrency = true;
                    }

                    if (loViewModel.loCallerTrxInfo.DREF_DATE >= loViewModel.Data.DTARGET_REF_DATE)
                    {
                        loViewModel.loLimitAllocationDate = loViewModel.loCallerTrxInfo.DREF_DATE.Value;
                    }
                    else
                    {
                        if (loViewModel.Data.DTARGET_REF_DATE.HasValue)
                        {
                            loViewModel.loLimitAllocationDate = loViewModel.Data.DTARGET_REF_DATE.Value;
                        }
                        else
                        {
                            loViewModel.loLimitAllocationDate = DateTime.Today;
                        }
                    }
                    loViewModel.Data.DREF_DATE = loViewModel.loLimitAllocationDate;

                    if (loViewModel.loAllocationEntryParameter.CTRANS_CODE == TransCodeConstant.VAR_PURCHASE_INVOICE)
                    {
                        IsCallerDiscountEnable = true;
                    }
                    else
                    {
                        IsCallerDiscountEnable = false;
                    }
                    if (loViewModel.Data.CTARGET_TRANS_CODE == TransCodeConstant.VAR_PURCHASE_INVOICE)
                    {
                        IsTargetDiscountEnable = true;
                    }
                    else
                    {
                        IsTargetDiscountEnable = false;
                    }
                    if (loViewModel.IsSingleCurrency)
                    {
                        IsCallerAmountEnable = false;
                        IsCallerTaxAmountEnable = false;
                    }
                    else
                    {
                        IsCallerAmountEnable = true;
                        IsCallerTaxAmountEnable = true;
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            R_DisplayException(loException);
        }


        private void RefreshFromProcess()
        {
            //CALLER_LABEL = loViewModel.loCallerTrxInfo.CTRANSACTION_NAME;
            APF00110DTO loData = (APF00110DTO)_conductorRef.R_GetCurrentData();
            loData.CTRANSACTION_NAME = loViewModel.loCallerTrxInfo.CTRANSACTION_NAME;

            loData.NCALLER_REMAINING = loViewModel.loCallerTrxInfo.NAP_REMAINING;
            loData.NCALLER_TAX_REMAINING = loViewModel.loCallerTrxInfo.NTAX_REMAINING;
            loData.NCALLER_TOTAL_REMAINING = loViewModel.loCallerTrxInfo.NTOTAL_REMAINING;
            loData.NCALLER_TAX_CURRENCY_RATE = loViewModel.loCallerTrxInfo.NTAX_CURRENCY_RATE;
            loData.NCALLER_TAX_BASE_RATE = loViewModel.loCallerTrxInfo.NTAX_BASE_RATE;

            loData.NLCALLER_REMAINING = loViewModel.loCallerTrxInfo.NLAP_REMAINING;
            loData.NLCALLER_TAX_REMAINING = loViewModel.loCallerTrxInfo.NLTAX_REMAINING;
            loData.NLCALLER_TOTAL_REMAINING = loViewModel.loCallerTrxInfo.NLTOTAL_REMAINING;
            loData.NLCALLER_BASE_RATE = loViewModel.loCallerTrxInfo.NLBASE_RATE;

            loData.NBCALLER_REMAINING = loViewModel.loCallerTrxInfo.NBAP_REMAINING;
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
                APF00110DTO loData = (APF00110DTO)eventArgs.Data;
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
                await loViewModel.DeleteAllocationAsync((APF00110DTO)eventArgs.Data);
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
                loViewModel.loAllocationDetail = new APF00110DTO();
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
            bool llCancel = false;
            R_Exception loEx = new R_Exception();
            APF00110DTO loData = (APF00110DTO)eventArgs.Data;

            try
            {
                llCancel = string.IsNullOrWhiteSpace(loData.CTARGET_TRANS_CODE);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(APF00100FrontResources.Resources_Dummy_Class),
                        "V001"));
                }

                llCancel = string.IsNullOrWhiteSpace(loData.CTARGET_REF_NO);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(APF00100FrontResources.Resources_Dummy_Class),
                        "V002"));
                }

                if (loData.DREF_DATE.HasValue)
                {
                    llCancel = loData.DREF_DATE.Value < DateTime.ParseExact(loViewModel.loSoftPeriod.CSTART_DATE, "yyyyMMdd", null);
                    if (llCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(APF00100FrontResources.Resources_Dummy_Class),
                            "V003"));
                    }
                }
                else
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(APF00100FrontResources.Resources_Dummy_Class),
                        "V014"));
                }

                llCancel = loData.NTARGET_AMOUNT > loData.NTARGET_REMAINING;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(APF00100FrontResources.Resources_Dummy_Class),
                        "V004"));
                }

                llCancel = loData.NTARGET_TAX_AMOUNT > loData.NTARGET_TAX_REMAINING;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(APF00100FrontResources.Resources_Dummy_Class),
                        "V005"));
                }

                llCancel = (loData.NTARGET_TAX_AMOUNT > 0) && (loData.NCALLER_TAX_AMOUNT > 0) && (loData.NTARGET_TAX_AMOUNT != loData.NCALLER_TAX_AMOUNT);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(APF00100FrontResources.Resources_Dummy_Class),
                        "V006"));
                }

                llCancel = loData.NCALLER_AMOUNT > loData.NCALLER_REMAINING;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(APF00100FrontResources.Resources_Dummy_Class),
                        "V007"));
                }

                llCancel = loData.NCALLER_TAX_AMOUNT > loData.NCALLER_TAX_REMAINING;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(APF00100FrontResources.Resources_Dummy_Class),
                        "V008"));
                }

                llCancel = loData.NCALLER_TAX_AMOUNT > 0 && loData.NTARGET_TAX_AMOUNT > 0 && loData.NCALLER_TAX_AMOUNT != loData.NTARGET_TAX_AMOUNT;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(APF00100FrontResources.Resources_Dummy_Class),
                        "V009"));
                }

                llCancel = loData.NTARGET_ALLOCATION == 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(APF00100FrontResources.Resources_Dummy_Class),
                        "V010"));
                }

                llCancel = (loData.NTARGET_ALLOCATION > 0) && (loData.LSINGLE_CURRENCY) && (loData.NTARGET_ALLOCATION != loData.NCALLER_ALLOCATION);
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(APF00100FrontResources.Resources_Dummy_Class),
                        "V011"));
                }
                llCancel = loData.NCALLER_ALLOCATION == 0;
                if (llCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(APF00100FrontResources.Resources_Dummy_Class),
                        "V012"));
                }

                llCancel = loData.DREF_DATE < loViewModel.loLimitAllocationDate;
                if (llCancel)
                {
                    string monthName = string.Format(R_FrontUtility.R_GetMessage(typeof(APF00100FrontResources.Resources_Dummy_Class), "V013"), loViewModel.loLimitAllocationDate.ToString("dd-MMM-yyyy"));
                    loEx.Add("V013", monthName);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Allocation_BeforeDelete(R_BeforeDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            R_PopupResult loPopUpResult = null;
            try
            {
                //await _conductorRef.R_GetEntity(new APF00110DTO { CREC_ID = loViewModel.loAllocationDetail.CREC_ID });
                R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["M001"], R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.No)
                {
                    eventArgs.Cancel = true;
                    return;
                }
                else if (loValidate == R_eMessageBoxResult.Yes)
                {
                    if (loViewModel.Data.CTRANS_STATUS == "80")
                    {
                        var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                        loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "APF00101"; //Uabh Approval Code sesuai Spec masing masing
                        await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                        //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                        if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                        {
                        }
                        else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                        {
                            loPopUpResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), new GFF00900ParameterDTO()
                            {
                                Data = loValidateViewModel.loRspActivityValidityList,
                                IAPPROVAL_CODE = "APF00101" //Uabh Approval Code sesuai Spec masing masing
                            });

                            if (loPopUpResult.Success == false || (bool)loPopUpResult.Result == false)
                            {
                                eventArgs.Cancel = true;
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task Allocation_BeforeAdd(R_BeforeAddEventArgs eventArgs)
        {

        }

        private void Allocation_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                APF00110DTO loData = (APF00110DTO)eventArgs.Data;
                loData.NCALLER_AMOUNT = loViewModel.loCallerTrxInfo.NAP_REMAINING;
                loData.NCALLER_REMAINING = loViewModel.loCallerTrxInfo.NAP_REMAINING;
                loData.NCALLER_TAX_AMOUNT = loViewModel.loCallerTrxInfo.NTAX_REMAINING;
                loData.NCALLER_TAX_REMAINING = loViewModel.loCallerTrxInfo.NTAX_REMAINING;
                loData.NCALLER_TOTAL_REMAINING = loViewModel.loCallerTrxInfo.NTOTAL_REMAINING;
                loData.CCALLER_CURRENCY_CODE = loViewModel.loCallerTrxInfo.CCURRENCY_CODE;

                loData.NLCALLER_AMOUNT = loViewModel.loCallerTrxInfo.NLAP_REMAINING;
                loData.NLCALLER_REMAINING = loViewModel.loCallerTrxInfo.NLAP_REMAINING;
                loData.NLCALLER_TAX_AMOUNT = loViewModel.loCallerTrxInfo.NLTAX_REMAINING;
                loData.NLCALLER_TAX_REMAINING = loViewModel.loCallerTrxInfo.NLTAX_REMAINING;
                loData.NLCALLER_TOTAL_REMAINING = loViewModel.loCallerTrxInfo.NLTOTAL_REMAINING;

                loData.NBCALLER_AMOUNT = loViewModel.loCallerTrxInfo.NBAP_REMAINING;
                loData.NBCALLER_REMAINING = loViewModel.loCallerTrxInfo.NBAP_REMAINING;
                loData.NBCALLER_TAX_AMOUNT = loViewModel.loCallerTrxInfo.NBTAX_REMAINING;
                loData.NBCALLER_TAX_REMAINING = loViewModel.loCallerTrxInfo.NBTAX_REMAINING;
                loData.NBCALLER_TOTAL_REMAINING = loViewModel.loCallerTrxInfo.NBTOTAL_REMAINING;

                loData.NLCALLER_BASE_RATE = loViewModel.loCallerTrxInfo.NLBASE_RATE;
                loData.NLCALLER_CURRENCY_RATE = loViewModel.loCallerTrxInfo.NLCURRENCY_RATE;
                loData.NCALLER_TAX_BASE_RATE = loViewModel.loCallerTrxInfo.NTAX_BASE_RATE;
                loData.NCALLER_TAX_CURRENCY_RATE = loViewModel.loCallerTrxInfo.NTAX_CURRENCY_RATE;
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
            else
            {
                IsFieldEnabled = false;
            }
        }

        private void Allocation_Saving(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                //loViewModel.ValidationAllocation((APF00110DTO)eventArgs.Data);
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
                IsFieldEnabled = false;
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
                await loViewModel.SaveAllocationAsync((APF00110DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);
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

                loViewModel.Data.CTARGET_REC_ID = "";
                loViewModel.Data.CTARGET_REF_NO = "";
                //loViewModel.Data.CTARGET_REF_DATE = loResult.CREF_DATE;
                //loViewModel.Data.DTARGET_REF_DATE = DateTime.ParseExact(loViewModel.Data.CTARGET_REF_DATE, "yyyyMMdd", null);
                loViewModel.Data.CTARGET_REF_DATE = "";
                loViewModel.Data.DTARGET_REF_DATE = null;
                loViewModel.Data.CTARGET_DEPT_CODE = "";
                loViewModel.Data.CTARGET_DEPT_NAME = "";

                loViewModel.Data.NTARGET_REMAINING = 0;
                loViewModel.Data.NLTARGET_REMAINING = 0;
                loViewModel.Data.NBTARGET_REMAINING = 0;

                loViewModel.Data.NTARGET_TAX_REMAINING = 0;
                loViewModel.Data.NLTARGET_TAX_REMAINING = 0;
                loViewModel.Data.NBTARGET_TAX_REMAINING = 0;

                loViewModel.Data.NTARGET_TOTAL_REMAINING = 0;
                loViewModel.Data.NLTARGET_TOTAL_REMAINING = 0;
                loViewModel.Data.NBTARGET_TOTAL_REMAINING = 0;

                loViewModel.Data.CTARGET_CURRENCY_CODE = "";

                loViewModel.Data.NLTARGET_BASE_RATE = 0;
                loViewModel.Data.NLTARGET_CURRENCY_RATE = 0;

                loViewModel.Data.NBTARGET_BASE_RATE = 0;
                loViewModel.Data.NBTARGET_CURRENCY_RATE = 0;

                loViewModel.Data.NTARGET_TAX_BASE_RATE = 0;
                loViewModel.Data.NTARGET_TAX_CURRENCY_RATE = 0;

                loViewModel.IsSingleCurrency = true;
                //InitialAllocationProcess();
                loViewModel.Data.NTARGET_ALLOCATION = 0;
                loViewModel.Data.NCALLER_ALLOCATION = 0;

                loViewModel.Data.NTARGET_AMOUNT = 0;
                loViewModel.Data.NLTARGET_AMOUNT = 0;
                loViewModel.Data.NBTARGET_AMOUNT = 0;
                loViewModel.Data.NTARGET_TAX_AMOUNT = 0;
                loViewModel.Data.NLTARGET_TAX_AMOUNT = 0;
                loViewModel.Data.NBTARGET_TAX_AMOUNT = 0;
                loViewModel.Data.NTARGET_DISC_AMOUNT = 0;
                loViewModel.Data.NLTARGET_DISC_AMOUNT = 0;
                loViewModel.Data.NBTARGET_DISC_AMOUNT = 0;

                loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NCALLER_REMAINING;
                loViewModel.Data.NCALLER_TAX_AMOUNT = loViewModel.Data.NCALLER_TAX_REMAINING;

                loViewModel.Data.NLCALLER_AMOUNT = loViewModel.Data.NLCALLER_REMAINING;
                loViewModel.Data.NBCALLER_AMOUNT = loViewModel.Data.NBCALLER_REMAINING;
                loViewModel.Data.NLCALLER_TAX_AMOUNT = loViewModel.Data.NLCALLER_TAX_REMAINING;
                loViewModel.Data.NBCALLER_TAX_AMOUNT = loViewModel.Data.NBCALLER_TAX_REMAINING;
                loViewModel.Data.NLCALLER_DISC_AMOUNT = 0;
                loViewModel.Data.NBCALLER_DISC_AMOUNT = 0;

                loViewModel.Data.NLFOREX_GAINLOSS = 0;
                loViewModel.Data.NBFOREX_GAINLOSS = 0;
                IsFieldEnabled = false;
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
            APL00500ParameterDTO loParam = new APL00500ParameterDTO()
            {
                CPROPERTY_ID = loViewModel.loAllocationEntryParameter.CPROPERTY_ID,
                CDEPT_CODE = loViewModel.loAllocationEntryParameter.CDEPT_CODE,
                CSUPPLIER_ID = loViewModel.loCallerTrxInfo.CSUPPLIER_ID,
                CTRANS_CODE = loViewModel.Data.CTARGET_TRANS_CODE,
                LHAS_REMAINING = true,
                LNO_REMAINING = false
            };
            if (loViewModel.loTransactionTypeList.Count > 0)
            {
                loParam.CTRANS_NAME = loViewModel.loTransactionTypeList.Where(x => x.CTRANS_CODE == loViewModel.Data.CTARGET_TRANS_CODE).FirstOrDefault().CTRANSACTION_NAME;
            }
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(APL00500);
        }

        private void R_After_Open_LookupReferenceNo(R_AfterOpenLookupEventArgs eventArgs)
        {
            APL00500DTO loTempResult = (APL00500DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loGetData = (APF00110DTO)_conductorRef.R_GetCurrentData();
            loViewModel.Data.CTARGET_REC_ID = loTempResult.CREC_ID;
            loViewModel.Data.CTARGET_REF_NO = loTempResult.CREF_NO;
            loViewModel.Data.CTARGET_REF_DATE = loTempResult.CREF_DATE;
            loViewModel.Data.DTARGET_REF_DATE = DateTime.ParseExact(loViewModel.Data.CTARGET_REF_DATE, "yyyyMMdd", null);
            loViewModel.Data.CTARGET_DEPT_CODE = loTempResult.CDEPT_CODE;
            loViewModel.Data.CTARGET_DEPT_NAME = loTempResult.CDEPT_NAME;

            loViewModel.Data.NTARGET_REMAINING = loTempResult.NAP_REMAINING;
            loViewModel.Data.NLTARGET_REMAINING = loTempResult.NLAP_REMAINING;
            loViewModel.Data.NBTARGET_REMAINING = loTempResult.NBAP_REMAINING;

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
            loViewModel.Data.NTARGET_TAX_CURRENCY_RATE = loTempResult.NTAX_CURRENCY_RATE;

            //if (loViewModel.Data.CTARGET_CURRENCY_CODE != loViewModel.loCompanyInfo.CLOCAL_CURRENCY_CODE || 
            //    loViewModel.Data.CTARGET_CURRENCY_CODE != loViewModel.loCompanyInfo.CBASE_CURRENCY_CODE ||
            //    loViewModel.Data.CCALLER_CURRENCY_CODE != loViewModel.loCompanyInfo.CLOCAL_CURRENCY_CODE ||
            //    loViewModel.Data.CCALLER_CURRENCY_CODE != loViewModel.loCompanyInfo.CBASE_CURRENCY_CODE)
            //{
            //    loViewModel.Data.LSINGLE_CURRENCY = false;
            //}
            //else
            //{
            //    loViewModel.Data.LSINGLE_CURRENCY = true;
            //}

            if (loViewModel.Data.CTARGET_CURRENCY_CODE != loViewModel.Data.CCALLER_CURRENCY_CODE)
            {
                loViewModel.IsSingleCurrency = false;
            }
            else
            {
                loViewModel.IsSingleCurrency = true;
            }
            InitialAllocationProcess();
            CalculateAllocationProcess();
            IsFieldEnabled = true;
        }

        private async Task OnLostFocusReferenceNo()
        {
            R_Exception loEx = new R_Exception();
            string lcTransactionName = "";

            try
            {
                APF00110DTO loGetData = (APF00110DTO)loViewModel.Data;

                if (string.IsNullOrWhiteSpace(loGetData.CTARGET_REF_NO) || string.IsNullOrWhiteSpace(loGetData.CTARGET_TRANS_CODE))
                {
                    loGetData.CREF_DATE = "";
                    loGetData.DREF_DATE = DateTime.Today;

                    loGetData.CTARGET_REC_ID = "";
                    loGetData.CTARGET_REF_NO = "";
                    //loGetData.CTARGET_REF_DATE = loResult.CREF_DATE;
                    //loGetData.DTARGET_REF_DATE = DateTime.ParseExact(loGetData.CTARGET_REF_DATE, "yyyyMMdd", null);
                    loGetData.CTARGET_REF_DATE = "";
                    loGetData.DTARGET_REF_DATE = null;
                    loGetData.CTARGET_DEPT_CODE = "";
                    loGetData.CTARGET_DEPT_NAME = "";

                    loGetData.NTARGET_REMAINING = 0;
                    loGetData.NLTARGET_REMAINING = 0;
                    loGetData.NBTARGET_REMAINING = 0;

                    loGetData.NTARGET_TAX_REMAINING = 0;
                    loGetData.NLTARGET_TAX_REMAINING = 0;
                    loGetData.NBTARGET_TAX_REMAINING = 0;

                    loGetData.NTARGET_TOTAL_REMAINING = 0;
                    loGetData.NLTARGET_TOTAL_REMAINING = 0;
                    loGetData.NBTARGET_TOTAL_REMAINING = 0;

                    loGetData.CTARGET_CURRENCY_CODE = "";

                    loGetData.NLTARGET_BASE_RATE = 0;
                    loGetData.NLTARGET_CURRENCY_RATE = 0;

                    loGetData.NBTARGET_BASE_RATE = 0;
                    loGetData.NBTARGET_CURRENCY_RATE = 0;

                    loGetData.NTARGET_TAX_BASE_RATE = 0;
                    loGetData.NTARGET_TAX_CURRENCY_RATE = 0;

                    loViewModel.IsSingleCurrency = true;
                    //InitialAllocationProcess();
                    loGetData.NTARGET_ALLOCATION = 0;
                    loGetData.NCALLER_ALLOCATION = 0;

                    loGetData.NTARGET_AMOUNT = 0;
                    loGetData.NLTARGET_AMOUNT = 0;
                    loGetData.NBTARGET_AMOUNT = 0;
                    loGetData.NTARGET_TAX_AMOUNT = 0;
                    loGetData.NLTARGET_TAX_AMOUNT = 0;
                    loGetData.NBTARGET_TAX_AMOUNT = 0;
                    loGetData.NTARGET_DISC_AMOUNT = 0;
                    loGetData.NLTARGET_DISC_AMOUNT = 0;
                    loGetData.NBTARGET_DISC_AMOUNT = 0;

                    loGetData.NCALLER_AMOUNT = loGetData.NCALLER_REMAINING;
                    loGetData.NCALLER_TAX_AMOUNT = loGetData.NCALLER_TAX_REMAINING;

                    loGetData.NLCALLER_AMOUNT = loGetData.NCALLER_REMAINING;
                    loGetData.NBCALLER_AMOUNT = loGetData.NCALLER_REMAINING;
                    loGetData.NLCALLER_TAX_AMOUNT = loGetData.NCALLER_TAX_REMAINING;
                    loGetData.NBCALLER_TAX_AMOUNT = loGetData.NCALLER_TAX_REMAINING;
                    loGetData.NLCALLER_DISC_AMOUNT = 0;
                    loGetData.NBCALLER_DISC_AMOUNT = 0;

                    loGetData.NLFOREX_GAINLOSS = 0;
                    loGetData.NBFOREX_GAINLOSS = 0;
                    IsFieldEnabled = false;
                    return;
                }

                lcTransactionName = loViewModel.loTransactionTypeList.Where(x => x.CTRANS_CODE == loGetData.CTARGET_TRANS_CODE).FirstOrDefault().CTRANSACTION_NAME;

                LookupAPL00500ViewModel loLookupViewModel = new LookupAPL00500ViewModel();
                APL00500ParameterDTO loParam = new APL00500ParameterDTO()
                {
                    CPROPERTY_ID = loViewModel.loAllocationEntryParameter.CPROPERTY_ID,
                    CDEPT_CODE = loViewModel.loAllocationEntryParameter.CDEPT_CODE,
                    CSUPPLIER_ID = loViewModel.loCallerTrxInfo.CSUPPLIER_ID,
                    CTRANS_CODE = loViewModel.Data.CTARGET_TRANS_CODE,
                    CTRANS_NAME = loViewModel.loCallerTrxInfo.CTRANSACTION_NAME,
                    LHAS_REMAINING = true,
                    LNO_REMAINING = false,
                    CSEARCH_CODE = loGetData.CTARGET_REF_NO
                };

                APL00500DTO loResult = await loLookupViewModel.GetTransactionLookup(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(LookupAPFrontResources.Resources_Dummy_Class_LookupAP),
                            "_ErrLookup01"));

                    loGetData.CREF_DATE = "";
                    loGetData.DREF_DATE = DateTime.Today;

                    loGetData.CTARGET_REC_ID = "";
                    loGetData.CTARGET_REF_NO = "";
                    //loGetData.CTARGET_REF_DATE = loResult.CREF_DATE;
                    //loGetData.DTARGET_REF_DATE = DateTime.ParseExact(loGetData.CTARGET_REF_DATE, "yyyyMMdd", null);
                    loGetData.CTARGET_REF_DATE = "";
                    loGetData.DTARGET_REF_DATE = null;
                    loGetData.CTARGET_DEPT_CODE = "";
                    loGetData.CTARGET_DEPT_NAME = "";

                    loGetData.NTARGET_REMAINING = 0;
                    loGetData.NLTARGET_REMAINING = 0;
                    loGetData.NBTARGET_REMAINING = 0;

                    loGetData.NTARGET_TAX_REMAINING = 0;
                    loGetData.NLTARGET_TAX_REMAINING = 0;
                    loGetData.NBTARGET_TAX_REMAINING = 0;

                    loGetData.NTARGET_TOTAL_REMAINING = 0;
                    loGetData.NLTARGET_TOTAL_REMAINING = 0;
                    loGetData.NBTARGET_TOTAL_REMAINING = 0;

                    loGetData.CTARGET_CURRENCY_CODE = "";

                    loGetData.NLTARGET_BASE_RATE = 0;
                    loGetData.NLTARGET_CURRENCY_RATE = 0;

                    loGetData.NBTARGET_BASE_RATE = 0;
                    loGetData.NBTARGET_CURRENCY_RATE = 0;

                    loGetData.NTARGET_TAX_BASE_RATE = 0;
                    loGetData.NTARGET_TAX_CURRENCY_RATE = 0;

                    loViewModel.IsSingleCurrency = true;
                    //InitialAllocationProcess();
                    loGetData.NTARGET_ALLOCATION = 0;
                    loGetData.NCALLER_ALLOCATION = 0;

                    loGetData.NTARGET_AMOUNT = 0;
                    loGetData.NLTARGET_AMOUNT = 0;
                    loGetData.NBTARGET_AMOUNT = 0;
                    loGetData.NLTARGET_TAX_AMOUNT = 0;
                    loGetData.NBTARGET_TAX_AMOUNT = 0;
                    loGetData.NLTARGET_DISC_AMOUNT = 0;
                    loGetData.NBTARGET_DISC_AMOUNT = 0;

                    loGetData.NCALLER_AMOUNT = loGetData.NCALLER_REMAINING;
                    loGetData.NCALLER_TAX_AMOUNT = loGetData.NCALLER_TAX_REMAINING;

                    loGetData.NLCALLER_AMOUNT = loGetData.NCALLER_REMAINING;
                    loGetData.NBCALLER_AMOUNT = loGetData.NCALLER_REMAINING;
                    loGetData.NLCALLER_TAX_AMOUNT = loGetData.NCALLER_TAX_REMAINING;
                    loGetData.NBCALLER_TAX_AMOUNT = loGetData.NCALLER_TAX_REMAINING;
                    loGetData.NLCALLER_DISC_AMOUNT = 0;
                    loGetData.NBCALLER_DISC_AMOUNT = 0;

                    loGetData.NLFOREX_GAINLOSS = 0;
                    loGetData.NBFOREX_GAINLOSS = 0;
                    IsFieldEnabled = false;
                }
                else
                {
                    loGetData.CTARGET_REC_ID = loResult.CREC_ID;
                    loGetData.CTARGET_REF_NO = loResult.CREF_NO;
                    //loGetData.CTARGET_REF_DATE = loResult.CREF_DATE;
                    //loGetData.DTARGET_REF_DATE = DateTime.ParseExact(loGetData.CTARGET_REF_DATE, "yyyyMMdd", null);
                    loGetData.CTARGET_REF_DATE = loResult.CREF_DATE;
                    if (!string.IsNullOrWhiteSpace(loGetData.CTARGET_REF_DATE))
                    {
                        loGetData.DTARGET_REF_DATE = DateTime.ParseExact(loGetData.CTARGET_REF_DATE, "yyyyMMdd", null);
                    }
                    loGetData.CTARGET_DEPT_CODE = loResult.CDEPT_CODE;
                    loGetData.CTARGET_DEPT_NAME = loResult.CDEPT_NAME;

                    loGetData.NTARGET_REMAINING = loResult.NAP_REMAINING;
                    loGetData.NLTARGET_REMAINING = loResult.NLAP_REMAINING;
                    loGetData.NBTARGET_REMAINING = loResult.NBAP_REMAINING;

                    loGetData.NTARGET_TAX_REMAINING = loResult.NTAX_REMAINING;
                    loGetData.NLTARGET_TAX_REMAINING = loResult.NLTAX_REMAINING;
                    loGetData.NBTARGET_TAX_REMAINING = loResult.NBTAX_REMAINING;

                    loGetData.NTARGET_TOTAL_REMAINING = loResult.NTOTAL_REMAINING;
                    loGetData.NLTARGET_TOTAL_REMAINING = loResult.NLTOTAL_REMAINING;
                    loGetData.NBTARGET_TOTAL_REMAINING = loResult.NBTOTAL_REMAINING;

                    loGetData.CTARGET_CURRENCY_CODE = loResult.CCURRENCY_CODE;

                    loGetData.NLTARGET_BASE_RATE = loResult.NLBASE_RATE;
                    loGetData.NLTARGET_CURRENCY_RATE = loResult.NLCURRENCY_RATE;

                    loGetData.NBTARGET_BASE_RATE = loResult.NBBASE_RATE;
                    loGetData.NBTARGET_CURRENCY_RATE = loResult.NBCURRENCY_RATE;

                    loGetData.NTARGET_TAX_BASE_RATE = loResult.NTAX_BASE_RATE;
                    loGetData.NTARGET_TAX_CURRENCY_RATE = loResult.NTAX_CURRENCY_RATE;

                    //if (loGetData.CTARGET_CURRENCY_CODE != loViewModel.loCompanyInfo.CLOCAL_CURRENCY_CODE ||
                    //    loGetData.CTARGET_CURRENCY_CODE != loViewModel.loCompanyInfo.CBASE_CURRENCY_CODE ||
                    //    loGetData.CCALLER_CURRENCY_CODE != loViewModel.loCompanyInfo.CLOCAL_CURRENCY_CODE ||
                    //    loGetData.CCALLER_CURRENCY_CODE != loViewModel.loCompanyInfo.CBASE_CURRENCY_CODE)
                    //{
                    //    loViewModel.IsSingleCurrency = false;
                    //}
                    //else
                    //{
                    //    loViewModel.IsSingleCurrency = true;
                    //}
                    if (loGetData.CTARGET_CURRENCY_CODE != loGetData.CCALLER_CURRENCY_CODE)
                    {
                        loViewModel.IsSingleCurrency = false;
                    }
                    else
                    {
                        loViewModel.IsSingleCurrency = true;
                    }
                    InitialAllocationProcess();
                    CalculateAllocationProcess();
                    IsFieldEnabled = true;
                }
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
                if (loViewModel.loCallerTrxInfo.DREF_DATE >= loViewModel.Data.DTARGET_REF_DATE)
                {
                    loViewModel.loLimitAllocationDate = loViewModel.loCallerTrxInfo.DREF_DATE.Value;
                }
                else
                {
                    if (loViewModel.Data.DTARGET_REF_DATE.HasValue)
                    {
                        loViewModel.loLimitAllocationDate = loViewModel.Data.DTARGET_REF_DATE.Value;
                    }
                    else
                    {
                        loViewModel.loLimitAllocationDate = DateTime.Today;
                    }
                }
                loViewModel.Data.DREF_DATE = loViewModel.loLimitAllocationDate;

                if (loViewModel.loAllocationEntryParameter.CTRANS_CODE == TransCodeConstant.VAR_PURCHASE_INVOICE)
                {
                    IsCallerDiscountEnable = true;
                }
                else
                {
                    IsCallerDiscountEnable = false;
                }
                if (loViewModel.Data.CTARGET_TRANS_CODE == TransCodeConstant.VAR_PURCHASE_INVOICE)
                {
                    IsTargetDiscountEnable = true;
                }
                else
                {
                    IsTargetDiscountEnable = false;
                }
                if (loViewModel.IsSingleCurrency)
                {
                    IsCallerAmountEnable = false;
                    IsCallerTaxAmountEnable = false;
                    if (loViewModel.Data.NTARGET_TAX_REMAINING > 0 && loViewModel.Data.NCALLER_TAX_REMAINING > 0)
                    {
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
                        if (loViewModel.Data.NTARGET_TAX_REMAINING >= loViewModel.Data.NCALLER_REMAINING)
                        {
                            loViewModel.Data.NCALLER_TAX_AMOUNT = loViewModel.Data.NCALLER_REMAINING;
                            loViewModel.Data.NTARGET_AMOUNT = 0;
                            loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NCALLER_REMAINING;
                        }
                        else
                        {
                            loViewModel.Data.NTARGET_TAX_AMOUNT = loViewModel.Data.NTARGET_TAX_REMAINING;
                            if (loViewModel.Data.NTARGET_REMAINING >= (loViewModel.Data.NCALLER_REMAINING - loViewModel.Data.NTARGET_TAX_REMAINING))
                            {
                                loViewModel.Data.NTARGET_AMOUNT = loViewModel.Data.NCALLER_REMAINING - loViewModel.Data.NTARGET_TAX_REMAINING;
                                loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NCALLER_REMAINING;
                            }
                            else
                            {
                                loViewModel.Data.NTARGET_AMOUNT = loViewModel.Data.NTARGET_REMAINING;
                                loViewModel.Data.NCALLER_AMOUNT = loViewModel.Data.NTARGET_REMAINING + loViewModel.Data.NTARGET_TAX_REMAINING;
                            }
                        }
                    }
                    else if (loViewModel.Data.NTARGET_TAX_REMAINING == 0 && loViewModel.Data.NCALLER_TAX_REMAINING > 0)
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
                                loViewModel.Data.NTARGET_AMOUNT = loViewModel.Data.NCALLER_REMAINING + loViewModel.Data.NCALLER_TAX_REMAINING;
                            }
                        }
                        else
                        {
                            loViewModel.Data.NCALLER_TAX_AMOUNT = loViewModel.Data.NTARGET_REMAINING;
                            loViewModel.Data.NCALLER_AMOUNT = 0;
                            loViewModel.Data.NTARGET_TAX_AMOUNT = loViewModel.Data.NTARGET_REMAINING;
                        }
                    }
                    else if (loViewModel.Data.NTARGET_TAX_REMAINING == 0 && loViewModel.Data.NCALLER_TAX_REMAINING == 0)
                    {
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
                        IsCallerTaxAmountEnable = true;
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
                loViewModel.Data.NLTARGET_TAX_AMOUNT = (loViewModel.Data.NTARGET_TAX_AMOUNT / loViewModel.Data.NTARGET_TAX_BASE_RATE) * loViewModel.Data.NTARGET_TAX_CURRENCY_RATE;
                loViewModel.Data.NBTARGET_TAX_AMOUNT = (loViewModel.Data.NTARGET_TAX_AMOUNT / loViewModel.Data.NBTARGET_BASE_RATE) * loViewModel.Data.NBTARGET_CURRENCY_RATE;
                loViewModel.Data.NLTARGET_DISC_AMOUNT = (loViewModel.Data.NTARGET_DISC_AMOUNT / loViewModel.Data.NLTARGET_BASE_RATE) * loViewModel.Data.NLTARGET_CURRENCY_RATE;
                loViewModel.Data.NBTARGET_DISC_AMOUNT = (loViewModel.Data.NTARGET_DISC_AMOUNT / loViewModel.Data.NBTARGET_BASE_RATE) * loViewModel.Data.NBTARGET_CURRENCY_RATE;
                loViewModel.Data.NTARGET_ALLOCATION = loViewModel.Data.NTARGET_AMOUNT + loViewModel.Data.NTARGET_TAX_AMOUNT - loViewModel.Data.NTARGET_DISC_AMOUNT;

                loViewModel.Data.NLCALLER_AMOUNT = (loViewModel.Data.NCALLER_AMOUNT / loViewModel.Data.NLCALLER_BASE_RATE) * loViewModel.Data.NLCALLER_CURRENCY_RATE;
                loViewModel.Data.NBCALLER_AMOUNT = (loViewModel.Data.NCALLER_AMOUNT / loViewModel.Data.NBCALLER_BASE_RATE) * loViewModel.Data.NBCALLER_CURRENCY_RATE;
                loViewModel.Data.NLCALLER_TAX_AMOUNT = (loViewModel.Data.NCALLER_TAX_AMOUNT / loViewModel.Data.NCALLER_TAX_BASE_RATE) * loViewModel.Data.NCALLER_TAX_CURRENCY_RATE;
                loViewModel.Data.NBCALLER_TAX_AMOUNT = (loViewModel.Data.NCALLER_TAX_AMOUNT / loViewModel.Data.NBCALLER_BASE_RATE) * loViewModel.Data.NBCALLER_CURRENCY_RATE;
                loViewModel.Data.NLCALLER_DISC_AMOUNT = (loViewModel.Data.NCALLER_DISC_AMOUNT / loViewModel.Data.NLCALLER_BASE_RATE) * loViewModel.Data.NLCALLER_CURRENCY_RATE;
                loViewModel.Data.NBCALLER_DISC_AMOUNT = (loViewModel.Data.NCALLER_DISC_AMOUNT / loViewModel.Data.NBCALLER_BASE_RATE) * loViewModel.Data.NBCALLER_CURRENCY_RATE;
                loViewModel.Data.NCALLER_ALLOCATION = loViewModel.Data.NCALLER_AMOUNT + loViewModel.Data.NCALLER_TAX_AMOUNT - loViewModel.Data.NCALLER_DISC_AMOUNT;

                if (loViewModel.loAllocationEntryParameter.CTRANS_CODE == TransCodeConstant.VAR_PURCHASE_INVOICE || loViewModel.loAllocationEntryParameter.CTRANS_CODE == TransCodeConstant.VAR_PURCHASE_CREDIT_NOTE)
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
            EnableDelete = loViewModel.loAllocationDetail.CTRANS_STATUS == "00" || loViewModel.loAllocationDetail.CTRANS_STATUS == "80";

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
                    await _conductorRef.R_GetEntity(new APF00110DTO { CREC_ID = loViewModel.loAllocationDetail.CREC_ID });
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
                    await _conductorRef.R_GetEntity(new APF00110DTO { CREC_ID = loViewModel.loAllocationDetail.CREC_ID });
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
                CREF_NO = loViewModel.loCallerTrxInfo.CREF_NO
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
