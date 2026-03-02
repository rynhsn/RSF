using BlazorClientHelper;
using FAT00700Common.DTOs;
using FAT00700FrontResources;
using FAT00700Model.VMs;
using GLF00100COMMON;
using GLF00100FRONT;
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
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;

namespace FAT00700Front
{
    public partial class FAT00700_TransactionEntry : R_Page
    {
        private FAT00700TransactionEntryViewModel _viewModel = new();
        private R_Conductor _conductorRef;
        private R_TextBox txtDeptCode;

        [Inject] private IClientHelper ClientHelper { get; set; } = default!;
        [Inject] private R_ILocalizer<Resources_Dummy_Class> Localizer { get; set; } = default!;
        [Inject] private R_MessageBoxService MessageBoxService { get; set; } = default!;
        [Inject] private R_IReport _reportService { get; set; }
        [Inject] private R_PopupService PopupService { get; set; }

        private const string VAR_CTRANS_CODE = "260010";
        private const string VAR_CACTIVITY_CODE = "FA013001";

        protected override async Task R_Init_From_Master(object? poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var param = R_FrontUtility.ConvertObjectToObject<FAT00700DTO>(poParameter) ?? new FAT00700DTO();
                _viewModel.paramTransEntry = param;

                await _conductorRef.R_GetEntity(param);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            if (eventArgs.Id == "AssetInfo")
            {

            }
        }

        public void BeforeOpenTabAssetInfo(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            //eventArgs.TargetPageType = typeof(FAT00700_AssetInformation);
            eventArgs.Parameter = _viewModel.loTransEntry.CASSET_CODE;
            eventArgs.TargetPageType = typeof(FAF00100FRONT.FAF00100);
        }

        #region Conductor Event Handlers

        public async Task Conductor_R_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.SaveTransactionAsync();
                eventArgs.Result = _viewModel.CurrentRecord;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Conductor_R_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                //param.CTRANSACTION_CODE = VAR_CTRANS_CODE;
                await _viewModel.GetRecordTransEntry();
                eventArgs.Result = _viewModel.loTransEntry;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public void Conductor_R_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            //txtDeptCode.
            R_Exception loEx = new R_Exception();

            try
            {
                var loTemp = (FAT00700DTO)eventArgs.Data;

                loTemp.DREF_DATE = DateTime.Now; 
                loTemp.CREF_DATE = R_FrontUtility.R_ConvertToDateTimeString(loTemp.DREF_DATE, "yyyyMMdd");
                loTemp.CDEPT_CODE = _viewModel.paramTransEntry.CDEPT_CODE_DEFAULT;
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            // Set focus to first field after add
        }

        #endregion

        #region Value Changed
        public void RefDateChanged(DateTime? pdValue)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loTemp = _viewModel.Data;

                _viewModel.Data.DREF_DATE = pdValue;
                _viewModel.Data.CREF_DATE = R_FrontUtility.R_ConvertToDateTimeString(_viewModel.Data.DREF_DATE, "yyyyMMdd");

                //Temp Value
                //ldTempRefDate = _viewModel.Data.DREF_DATE;
                //lcTempcRefDate = _viewModel.Data.CREF_DATE;

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region On Click Button

        public async Task DeleteTransaction()
        {
            R_Exception loEx = new R_Exception();
            R_PopupResult loResult = null;

            try
            {
                var loMessaggeResult = await R_MessageBox.Show(Localizer["_confirmTitle"],
                    Localizer["_confirmMessage3"],
                    R_BlazorFrontEnd.Controls.MessageBox.R_eMessageBoxButtonType.YesNo);

                if (loMessaggeResult == R_BlazorFrontEnd.Controls.MessageBox.R_eMessageBoxResult.Yes)
                {
                    if (_viewModel.loTransEntry.CTRANS_STATUS == "30" || _viewModel.loTransEntry.CTRANS_STATUS == "80")
                    {
                        loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), "FAT00701");
                    }
                    else
                    {
                        await _viewModel.DeleteTransaction();
                        if (!loEx.HasError)
                        {
                            await R_MessageBox.Show(Localizer["_success"], Localizer["_deleteTransaction"]);
                        }
                    }
                    await _conductorRef.R_GetEntity(new FAT00700DTO());
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

        public async Task SubmitTransaction()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loMessaggeResult = await R_MessageBox.Show(Localizer["_confirmTitle"],
                    Localizer["_confirmMessage"],
                    R_BlazorFrontEnd.Controls.MessageBox.R_eMessageBoxButtonType.YesNo);

                if (loMessaggeResult == R_BlazorFrontEnd.Controls.MessageBox.R_eMessageBoxResult.Yes)
                {
                    await _viewModel.SubmitTransaction();
                    await _conductorRef.R_GetEntity(new FAT00700DTO());

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
                    Localizer["_confirmMessage2"],
                    R_BlazorFrontEnd.Controls.MessageBox.R_eMessageBoxButtonType.YesNo);

                if (loMessaggeResult == R_BlazorFrontEnd.Controls.MessageBox.R_eMessageBoxResult.Yes)
                {
                    await _viewModel.RedraftTransaction();
                    await _conductorRef.R_GetEntity(new FAT00700DTO());

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

        #region Look Up
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

                    _viewModel.Data.CDEPT_CODE = loResult.CDEPT_CODE;
                    _viewModel.Data.CDEPT_NAME = loResult.CDEPT_NAME;
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

                if (!string.IsNullOrWhiteSpace(_viewModel.Data.CDEPT_CODE))
                {
                    loParam.CSEARCH_TEXT = _viewModel.Data.CDEPT_CODE;

                    var loTemp = await loViewModel.GetDepartment(loParam);

                    if (loTemp != null)
                    {
                        _viewModel.Data.CDEPT_CODE = loTemp.CDEPT_CODE;
                        _viewModel.Data.CDEPT_NAME = loTemp.CDEPT_NAME;
                    }
                    else
                    {
                        await R_MessageBox.Show(Localizer["ErrLookupInformation"],
                        R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "ErrLookup01").ErrDescp);
                        //await txtDeptCode.FocusAsync();
                        _viewModel.Data.CDEPT_CODE = "";
                        _viewModel.Data.CDEPT_NAME = "";

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
                loParam.CTRANS_CODE = VAR_CTRANS_CODE;
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

                    _viewModel.Data.CASSET_CODE = loResult.CASSET_CODE;
                    _viewModel.Data.CASSET_NAME = loResult.CASSET_NAME;
                    _viewModel.Data.CASSET_TRANS_SEQ_NO = loResult.CASSET_TRANS_SEQ_NO;
                    _viewModel.Data.CASSET_DEPT_CODE = loResult.CASSET_DEPT_CODE;
                    _viewModel.Data.CASSET_DEPT_NAME = loResult.CASSET_DEPT_NAME;
                    _viewModel.Data.IQTY = loResult.IADDITION_QTY;
                    _viewModel.Data.CUNIT = loResult.CUNIT;
                    _viewModel.Data.NLTRANS_AMOUNT = loResult.NLBOOK_VALUE;
                    _viewModel.Data.NBTRANS_AMOUNT = loResult.NBBOOK_VALUE;
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

                if (!string.IsNullOrWhiteSpace(_viewModel.Data.CASSET_CODE))
                {
                    loParam.CASSET_CODE = _viewModel.Data.CASSET_CODE;

                    var loTemp = await loViewModel.GetTaxCategory(loParam);

                    if (loTemp != null)
                    {
                        _viewModel.Data.CASSET_CODE = loTemp.CASSET_CODE;
                        _viewModel.Data.CASSET_NAME = loTemp.CASSET_NAME;
                    }
                    else
                    {
                        await R_MessageBox.Show(Localizer["ErrLookupInformation"],
                        R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "ErrLookup02").ErrDescp);
                        //await txtAstCode.FocusAsync();
                        _viewModel.Data.CASSET_CODE = "";
                        _viewModel.Data.CASSET_CODE = "";

                    }
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void BeforeOpenAllocation(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParam = new GSL03200ParameterDTO();
                loParam.CDEPT_CODE = _viewModel.Data.CDEPT_CODE;
                loParam.CACTIVE_TYPE = "1";

                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSL03200);
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void AfterOpenAllocation(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (eventArgs.Result != null)
                {
                    var loTemp = (GSL03200DTO)eventArgs.Result;

                    _viewModel.Data.CEXPENSE_ALLOC_ID = loTemp.CALLOC_ID;
                    _viewModel.Data.CEXPENSE_ALLOC_NAME = loTemp.CALLOC_NAME;
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task OnLostFocusedAllocation()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParam = new GSL03200ParameterDTO();
                var loViewModel = new LookupGSL03200ViewModel();

                if (!string.IsNullOrWhiteSpace(_viewModel.Data.CEXPENSE_ALLOC_ID))
                {
                    loParam.CSEARCH_TEXT = _viewModel.Data.CEXPENSE_ALLOC_ID;

                    var loTemp = await loViewModel.GetProductAllocation(loParam);

                    if (loTemp != null)
                    {
                        _viewModel.Data.CEXPENSE_ALLOC_ID = loTemp.CALLOC_ID;
                        _viewModel.Data.CEXPENSE_ALLOC_NAME = loTemp.CALLOC_NAME;
                    }
                    else
                    {
                        await R_MessageBox.Show(Localizer["ErrLookupInformation"],
                        R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "ErrLookup03").ErrDescp);
                        //await txtAllocCode.FocusAsync();
                        _viewModel.Data.CEXPENSE_ALLOC_ID = "";
                        _viewModel.Data.CEXPENSE_ALLOC_NAME = "";

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

        #region CRUD
        public void SavingTransactionEntry(R_SavingEventArgs eventArgs)
        {
            var loTemp = (FAT00700DTO)eventArgs.Data;
            _viewModel.SavingTransactionEntry(loTemp);
        }

        public async Task SaveTransactionEntry(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.SaveTransactionEntry((eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.loTransEntry;
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task BeforeCancelTransaction(R_BeforeCancelEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                //llEnableAssetCode = false;
                //llEnableDeprQty = false;
                //ViewModelFAT00301.cAssetCode = lcAssetCode;

                //ViewModelFAT00301.refDateValue = ldTempRefDate;
                //ViewModelFAT00301.cRefDate = lcTempcRefDate;

                var loMessageResult = await R_MessageBox.Show(Localizer["_confirmTitle"],
                    Localizer["_confirmMessagge4"],
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

        #region Validation
        public void ValidationTransactionEntry(R_ValidationEventArgs eventArgs)
        {
            var loTemp = (FAT00700DTO)eventArgs.Data;
            _viewModel.ValidationTransactionEntry(loTemp);
        }
        #endregion


        #region Open Journal
        public void BeforeOpenJournal(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loParam = new GLF00100ParameterDTO();

            loParam.CDEPT_CODE = _viewModel.loTransEntry.CDEPT_CODE;
            loParam.CTRANS_CODE = VAR_CTRANS_CODE;
            loParam.CREF_NO = _viewModel.loTransEntry.CGL_REF_NO;

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GLF00100);
        }

        public async Task AfterOpenJournal(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _conductorRef.R_GetEntity(new FAT00700DTO());
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Set Other
        public void SetOtherFAT00700(R_SetEventArgs eventArgs)
        {
            _viewModel.llEnableSubmit = eventArgs.Enable;
            _viewModel.llEnableRedraft = eventArgs.Enable;
            _viewModel.llEnablePrint = eventArgs.Enable;
            _viewModel.llEnableJournal = eventArgs.Enable;
            _viewModel.llEnableDelete = eventArgs.Enable;
            _viewModel.llEnableAdd = eventArgs.Enable;
            _viewModel.llEnableTabInformation = eventArgs.Enable;
        }
        #endregion
    }
}
