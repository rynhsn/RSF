using BlazorClientHelper;
using FAF00100FRONT;
using FAT00300Common.DTOs;
using FAT00300Common.Requests;
using FAT00300FrontResources;
using FAT00300Model.VMs;
using GLF00100COMMON;
using Lookup_FACommon.DTOs;
using Lookup_FAFront;
using Lookup_FAModel.ViewModel.FAL00200;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FAT00300Front
{
    public partial class FAT00301 : R_Page
    {
        private R_TabStrip _tabStripRef;
        private R_Conductor _conTransEntry;
        private R_TabPage _tabPageAssetInformationRef;
        private FAT00300DTO loParamGetTransEntry = new FAT00300DTO();
        [Inject] R_ILocalizer<Resources_Dummy_Class> Localizer { get; set; } = default!;
        [Inject] private R_PopupService PopupService { get; set; }

        public FAT00301ViewModel ViewModelFAT00301 = new FAT00301ViewModel();
        private string VAR_TRANS_CODE = "210020";
        public bool LCHANGE_DESC = false;
        public bool llEnableAssetCode = false;
        public bool llEnableDeprQty = false;

        public bool llEnableSubmit = true;
        public bool llEnableRedraft = true;

        public string lcAssetCode = "";
        public DateTime? ldTempRefDate = DateTime.Now;
        public string lcTempcRefDate = "";

        private R_TextBox txtDeptCode;
        private R_TextBox txtAssetCode;

        protected override async Task R_Init_From_Master(object? poParameter)
        {
            R_Exception loException = new R_Exception();

            try
            {
                if (poParameter != null)
                {
                    loParamGetTransEntry = poParameter as FAT00300DTO ?? new FAT00300DTO();
                    ViewModelFAT00301.paramTranEntry = loParamGetTransEntry;
                    lcAssetCode = loParamGetTransEntry.CASSET_CODE;

                    //var loParam = new FAT00300GetInitialProcessParameterDTO();
                    //loParam.CTRANS_CODE = ViewModelFAT00301.cTransCode;

                    //await ViewModelFAT00301.GetInitialProcessAsync(loParam);
                    await _conTransEntry.R_GetEntity(new FAT00300DTO());

                }

                var loParamAsset = new FAT00300GetAssetParameterDTO();
                loParamAsset.CASSET_CODE = ViewModelFAT00301.loTransEntry.CASSET_CODE;
                await ViewModelFAT00301.GetAssetData(loParamAsset);

            }
            catch (Exception ex)
            {

                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        public async Task GetTransactionEntryRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await ViewModelFAT00301.GetRecordTransEntry();
                eventArgs.Result = ViewModelFAT00301.loTransEntry;

                //Temp Value
                ldTempRefDate = ViewModelFAT00301.refDateValue;
                lcTempcRefDate = ViewModelFAT00301.cRefDate;
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Display
        public void DisplayTransactionEntry(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                //if (eventArgs.ConductorMode == R_eConductorMode.Add || eventArgs.ConductorMode == R_eConductorMode.Edit)
                //{
                //    ViewModelFAT00301.LENABLE_ASSET = false;
                //}

                if (eventArgs.ConductorMode == R_eConductorMode.Normal && string.IsNullOrEmpty(ViewModelFAT00301.loTransEntry.CDEPT_CODE))
                {
                    ViewModelFAT00301.llEnableJournal = false;
                    ViewModelFAT00301.llEnablePrint = false;
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Value Changed

        public void RefDateChanged(DateTime? pdValue)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loTemp = ViewModelFAT00301.Data;

                ViewModelFAT00301.Data.DREF_DATE = pdValue;
                ViewModelFAT00301.Data.CREF_DATE = R_FrontUtility.R_ConvertToDateTimeString(ViewModelFAT00301.Data.DREF_DATE, "yyyyMMdd");

                //Temp Value
                ldTempRefDate = ViewModelFAT00301.Data.DREF_DATE;
                lcTempcRefDate = ViewModelFAT00301.Data.CREF_DATE;

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        //public async Task AssetCodeChanged(string pcValue)
        //{
        //    R_Exception loEx = new R_Exception();

        //    try
        //    {
        //        ViewModelFAT00301.Data.CASSET_CODE = pcValue;

        //        var loParam = new FAT00300GetValidateOutstandTransParameterDTO();
        //        var loParam2 = new FAT00300GetValidationDataParameterDTO();
        //        var loParam3 = new FAT00300GetAssetParameterDTO();

        //        loParam.CASSET_CODE = ViewModelFAT00301.cAssetCode;
        //        loParam2.CASSET_CODE = ViewModelFAT00301.cAssetCode;
        //        loParam3.CASSET_CODE = ViewModelFAT00301.cAssetCode;

        //        await ViewModelFAT00301.GetValidateOutstandTrans(loParam);
        //        await ViewModelFAT00301.GetValidationData(loParam2);
        //        await ViewModelFAT00301.GetAssetData(loParam3);

        //        // Validate Outstand Transaction
        //        if (!string.IsNullOrEmpty(ViewModelFAT00301.ValidateOutStandTrans.CASSET_CODE))
        //        {
        //            loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "PS018"));
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        loEx.Add(ex);
        //    }

        //    R_DisplayException(loEx);
        //}

        public void DeprAmountChanged(decimal pnValue)
        {

            R_Exception loEx = new R_Exception();

            try
            {
                ViewModelFAT00301.Data.NTRANS_AMOUNT = pnValue;

                ViewModelFAT00301.RefreshBaseCurrency();
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }
        #endregion

        #region Set Other
        public void SetOtherTransEntry(R_SetEventArgs eventArgs)
        {
            llEnableSubmit = eventArgs.Enable;
            llEnableRedraft = eventArgs.Enable;
            ViewModelFAT00301.llEnableJournal = eventArgs.Enable;
            ViewModelFAT00301.llEnablePrint = eventArgs.Enable;
            ViewModelFAT00301.llEnableAdd = eventArgs.Enable;
            ViewModelFAT00301.llEnableEdit = eventArgs.Enable;
            ViewModelFAT00301.llEnableDelete = eventArgs.Enable;
            ViewModelFAT00301.LENABLE_ASSET = eventArgs.Enable;

        }
        #endregion

        #region After Add Trans Entry
        public void AfterAddTransEntry(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (LCHANGE_DESC == false)
                {
                    llEnableAssetCode = true;
                    llEnableDeprQty = true;
                }

                var loData = (FAT00300DTO)eventArgs.Data;
                //ViewModelFAT00301.cAssetCode = "";
                //ViewModelFAT00301.refDateValue = DateTime.Now;
                //ViewModelFAT00301.cRefDate = R_FrontUtility.R_ConvertToDateTimeString(ViewModelFAT00301.refDateValue, "yyyymmdd");

                loData.DREF_DATE = DateTime.Now;
                loData.CREF_DATE = R_FrontUtility.R_ConvertToDateTimeString(ViewModelFAT00301.Data.DREF_DATE, "yyyymmdd");
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Cancel Trans Entry
        public void CancelTransEntry()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                llEnableAssetCode = false;
                llEnableDeprQty = false;
                ViewModelFAT00301.cAssetCode = lcAssetCode;

                ViewModelFAT00301.refDateValue = ldTempRefDate;
                ViewModelFAT00301.cRefDate = lcTempcRefDate;
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Validation
        public void TransEntryValidation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                //await ViewModelFAT00301.ValidateRefDate();
                ViewModelFAT00301.ValidationTransEntry(eventArgs.Data);
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Saving & Delete
        public void SavingTransactionEntry(R_SavingEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loTemp = eventArgs.Data as FAT00300DTO ?? new FAT00300DTO();
                ViewModelFAT00301.SavingTransactionEntry(loTemp);

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveTransactionEntry(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await ViewModelFAT00301.SaveTransactionEntry((eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = ViewModelFAT00301.loTransEntry;
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteTransactionEntry()
        {
            R_Exception loEx = new R_Exception();
            R_PopupResult loResult = null;

            try
            {
                var loMessageResult = await R_MessageBox.Show(Localizer["_confirmTitle"],
                                        Localizer["_confirmMessagge"],
                                        R_BlazorFrontEnd.Controls.MessageBox.R_eMessageBoxButtonType.YesNo);

                if (loMessageResult == R_BlazorFrontEnd.Controls.MessageBox.R_eMessageBoxResult.Yes)
                {
                    if (ViewModelFAT00301.loTransEntry.CTRANS_STATUS == "30" || ViewModelFAT00301.loTransEntry.CTRANS_STATUS == "80")
                    {
                        loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), "FAT00301");
                    }
                    else
                    {
                        await ViewModelFAT00301.DeleteTransactionEntry(ViewModelFAT00301.loTransEntry);
                        if (!loEx.HasError)
                        {
                            await R_MessageBox.Show(Localizer["_success"], Localizer["_submitTransaction"]);
                        }
                    }
                    await _conTransEntry.R_GetEntity(new FAT00300DTO());
                }
                else
                {
                    await Task.CompletedTask;
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region OnClick Button
        public async Task SubmitTrnasaction()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loMessaggeResult = await R_MessageBox.Show(Localizer["_confirmTitle"],
                    Localizer["_confirmMessagge"],
                    R_BlazorFrontEnd.Controls.MessageBox.R_eMessageBoxButtonType.YesNo);

                if (loMessaggeResult == R_BlazorFrontEnd.Controls.MessageBox.R_eMessageBoxResult.Yes)
                {
                    await ViewModelFAT00301.SubmitTransaction();
                    await _conTransEntry.R_GetEntity(new FAT00300DTO());

                    if (!loEx.HasError)
                    {
                        await R_MessageBox.Show(Localizer["_success"], Localizer["_submitTransaction"]);

                    }
                }
                else
                {
                    await Task.CompletedTask;
                }


            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task RedraftTransaction()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loMessaggeResult = await R_MessageBox.Show(Localizer["_confirmTitle"],
                    Localizer["_confirmMessagge2"],
                    R_BlazorFrontEnd.Controls.MessageBox.R_eMessageBoxButtonType.YesNo);

                if (loMessaggeResult == R_BlazorFrontEnd.Controls.MessageBox.R_eMessageBoxResult.Yes)
                {
                    await ViewModelFAT00301.RedraftTransaction();
                    await _conTransEntry.R_GetEntity(new FAT00300DTO());

                    if (!loEx.HasError)
                    {
                        await R_MessageBox.Show(Localizer["_success"], Localizer["_redraftTransaction"]);
                    }
                }
                else
                {
                    await Task.CompletedTask;
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Before & After Open Page Asset Information
        public void BeforeOpenAssetInformation(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParam = ViewModelFAT00301.loTransEntry.CASSET_CODE;

                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(FAF00100);

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task AfterOpenAssetInformation(R_AfterOpenTabPageEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _conTransEntry.R_GetEntity(ViewModelFAT00301.paramTranEntry);
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Pop Up Form
        public void BeforeOpenJournal(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loParam = new GLF00100ParameterDTO();
            loParam.CDEPT_CODE = ViewModelFAT00301.loTransEntry.CDEPT_CODE;
            loParam.CTRANS_CODE = VAR_TRANS_CODE;
            loParam.CREF_NO = ViewModelFAT00301.loTransEntry.CGL_REF_NO;

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GLF00100FRONT.GLF00100);
        }

        public async Task AfterOpenJournal (R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _conTransEntry.R_GetEntity(new FAT00300DTO());
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }
        #endregion

        #region Lookup
        public void BeforeOpenLookUpDepartment(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParam = new GSL00700ParameterDTO();
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL00700);

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void AfterOpenLookUpDepartment(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (eventArgs.Result != null)
                {
                    var loResult = (GSL00700DTO)eventArgs.Result;

                    ViewModelFAT00301.Data.CDEPT_CODE = loResult.CDEPT_CODE;
                    ViewModelFAT00301.Data.CDEPT_NAME = loResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task OnLostFocusedDepartment()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParam = new GSL00700ParameterDTO();
                var loViewModel = new LookupGSL00700ViewModel();

                if (!string.IsNullOrWhiteSpace(ViewModelFAT00301.Data.CDEPT_CODE))
                {
                    loParam.CSEARCH_TEXT = ViewModelFAT00301.Data.CDEPT_CODE;

                    var loTemp = await loViewModel.GetDepartment(loParam);

                    if (loTemp != null)
                    {
                        ViewModelFAT00301.Data.CDEPT_CODE = loTemp.CDEPT_CODE;
                        ViewModelFAT00301.Data.CDEPT_NAME = loTemp.CDEPT_NAME;
                    }
                    else
                    {
                        await R_MessageBox.Show(Localizer["ErrLookupInformation"],
                        R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "ErrLookup01").ErrDescp);
                        await txtDeptCode.FocusAsync();
                        ViewModelFAT00301.Data.CDEPT_CODE = "";
                        ViewModelFAT00301.Data.CDEPT_NAME = "";

                    }
                }

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void BeforeOpenLookUpAsset(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParam = new FAL00300ParameterDTO();
                loParam.CTRANS_CODE = VAR_TRANS_CODE;
                loParam.CASSET_CODE = "";
                loParam.CCOMPANY_ID = "";
                loParam.CLANGUAGE_ID = "";
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(FAL00300);
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void AfterOpenLookUpAsset(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (eventArgs.Result != null)
                {
                    var loResult = (FAL00300DTO)eventArgs.Result;

                    ViewModelFAT00301.Data.CASSET_CODE = loResult.CASSET_CODE;
                    ViewModelFAT00301.Data.CASSET_NAME = loResult.CASSET_NAME;
                    ViewModelFAT00301.Data.CASSET_TRANS_SEQ_NO = loResult.CASSET_TRANS_SEQ_NO;
                    ViewModelFAT00301.Data.CASSET_DEPT_CODE = loResult.CASSET_DEPT_CODE;
                    ViewModelFAT00301.Data.CASSET_DEPT_NAME = loResult.CASSET_DEPT_NAME;
                    ViewModelFAT00301.Data.CLOCAL_CURRENCY_CODE = loResult.CCURRENCY_CODE;
                    ViewModelFAT00301.Data.CBASE_CURRENCY_CODE = loResult.CCURRENCY_CODE;
                    ViewModelFAT00301.Data.CCURRENCY_CODE = loResult.CCURRENCY_CODE;
                    ViewModelFAT00301.Data.IQTY = loResult.IBALANCE_QTY;
                    ViewModelFAT00301.CDEPR_AMOUN_CURRENCY_CODE = loResult.CCURRENCY_CODE;
                    ViewModelFAT00301.Data.CUNIT = loResult.CUNIT;
                    ViewModelFAT00301.Data.NLCURRENCY_RATE = loResult.NLCURRENCY_RATE;
                    ViewModelFAT00301.Data.NLBASE_RATE = loResult.NLBASE_RATE;
                    ViewModelFAT00301.Data.NBBASE_RATE = loResult.NBBASE_RATE;
                    ViewModelFAT00301.Data.NBCURRENCY_RATE = loResult.NBCURRENCY_RATE;

                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task OnLostFocusedAsset()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParam = new FAL00300ParameterDTO();
                var loViewModel = new LookupFAL00300ViewModel();

                if (!string.IsNullOrWhiteSpace(ViewModelFAT00301.Data.CASSET_CODE))
                {
                    loParam.CASSET_CODE = ViewModelFAT00301.Data.CDEPT_CODE;

                    var loTemp = await loViewModel.GetTaxCategory(loParam);

                    if (loTemp != null)
                    {
                        ViewModelFAT00301.Data.CASSET_CODE = loTemp.CASSET_CODE;
                        ViewModelFAT00301.Data.CASSET_NAME = loTemp.CASSET_NAME;
                    }
                    else
                    {
                        await R_MessageBox.Show(Localizer["ErrLookupInformation"],
                        R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "ErrLookup02").ErrDescp);
                        await txtAssetCode.FocusAsync();
                        ViewModelFAT00301.Data.CASSET_CODE = "";
                        ViewModelFAT00301.Data.CASSET_NAME = "";

                    }
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Before Cancel
        public async Task BeforeCancelTransaction(R_BeforeCancelEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                llEnableAssetCode = false;
                llEnableDeprQty = false;
                ViewModelFAT00301.cAssetCode = lcAssetCode;

                ViewModelFAT00301.refDateValue = ldTempRefDate;
                ViewModelFAT00301.cRefDate = lcTempcRefDate;

                var loMessageResult = await R_MessageBox.Show(Localizer["_confirmTitle"],
                    Localizer["_confirmMessage3"],
                    R_eMessageBoxButtonType.YesNo);

                if (loMessageResult == R_BlazorFrontEnd.Controls.MessageBox.R_eMessageBoxResult.Yes)
                {
                    eventArgs.Cancel = false;
                }
                else
                {
                    eventArgs.Cancel = true;
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
