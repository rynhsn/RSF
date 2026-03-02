using BlazorClientHelper;
using FAT00700Common.DTOs;
using FAT00700FrontResources;
using FAT00700Model.VMs;
using Lookup_FAFront;
using Lookup_FACommon.DTOs;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using Lookup_FAModel.ViewModel.FAL00200;

namespace FAT00700Front
{
    public partial class FAT00700 : R_Page
    {
        private FAT00700ViewModel _viewModel = new();

        [Inject] private IClientHelper ClientHelper { get; set; } = default!;
        [Inject] private R_ILocalizer<Resources_Dummy_Class> Localizer { get; set; } = default!;
        [Inject] private R_MessageBoxService MessageBoxService { get; set; } = default!;
        [Inject] private R_IReport _reportService { get; set; }

        // Constants
        private const string VAR_CTRANS_CODE = "260010";
        private R_Conductor _conductorRef;
        private R_Grid<GetTransactionListResultDTO> _gridRef;

        protected override async Task R_Init_From_Master(object? poParameter)
        {
            var loEx = new R_Exception();

            try
            {

                var loParamGetCompanyInfo = new FAT00700CompanyInfoParameterDTO();
                var loParamGetSystemParam = new FAT00700SystemParamParameterDTO();
                var loParamGetTransCodeInfo = new FAT00700TransCodeInfoParamDTO();
                var loParamGetPeriodInfo = new FAT00700PeriodInfoParamDTO();
                var loParamGetPeriodRange = new FAT00700PeriodRangeParamDTO();

                _viewModel.SetPeriodFromTo();

                await _viewModel.GetCompanyInfoAsync(loParamGetCompanyInfo);
                await _viewModel.GetSystemParamAsync(loParamGetSystemParam);
                await _viewModel.GetDeptListAsync();
                await ValidationSystemParam();
                await _viewModel.GetPeriodInfoAsync(loParamGetPeriodInfo);
                await _viewModel.GetTransCodeInfoAsync(loParamGetTransCodeInfo);
                await _viewModel.GetPeriodRangeAsync(loParamGetPeriodRange);

                //await _viewModel.GetInitialProcessAsync(ClientHelper.CompanyId, ClientHelper.Culture.ToString(), VAR_CTRANS_CODE, VAR_CACTIVITY_CODE, ClientHelper.UserId);
                //await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetTransactionListAsync();
                eventArgs.ListEntityResult = _viewModel.TransactionList;

                if (_viewModel.TransactionList.Count == 0)
                {
                    await R_MessageBox.Show(Localizer["ErrorFound"], Localizer["NotFound"], R_eMessageBoxButtonType.OK);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<FAT00700DTO>(eventArgs.Data);
                eventArgs.Result = loData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.Data != null)
            {
                //loData.CTRANSACTION_CODE = VAR_CTRANS_CODE;
                _viewModel.CurrentRecord = R_FrontUtility.ConvertObjectToObject<FAT00700DTO>(eventArgs.Data) ?? new FAT00700DTO();
            }
        }

        private async Task R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<FAT00700DTO>(eventArgs.Data);

                // Ensure CompanyId is set
                if (string.IsNullOrWhiteSpace(loData.CCOMPANY_ID))
                {
                    loData.CCOMPANY_ID = ClientHelper.CompanyId;
                }

                // Determine CRUD mode from conductor
                eCRUDMode leMode = _conductorRef.R_ConductorMode == R_eConductorMode.Add
                    ? eCRUDMode.AddMode
                    : _conductorRef.R_ConductorMode == R_eConductorMode.Edit
                        ? eCRUDMode.EditMode
                        : eCRUDMode.NormalMode;

                await _viewModel.ValidateTransactionAsync(loData, leMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task OnClickRefresh()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                _viewModel.ValidationDepartment();

                if (!loEx.HasError)
                {
                    await _gridRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {

                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public void PredefinedTransEntry(R_InstantiateDockEventArgs eventArgs)
        {
            var loParam = _viewModel.CurrentRecord;
            loParam.CLOCAL_CURRENCY_CODE = _viewModel.CompanyInfo.CLOCAL_CURRENCY_CODE;
            loParam.CBASE_CURRENCY_CODE = _viewModel.CompanyInfo.CBASE_CURRENCY_CODE;
            loParam.CSOFT_PERIOD = _viewModel.SystemParam.CSOFT_PERIOD;
            loParam.LINCREMENT_FLAG = _viewModel.TransCodeInfo.LINCREMENT_FLAG;
            loParam.CREC_ID = _viewModel.CurrentRecord.CREC_ID;
            loParam.CDEPT_CODE = _viewModel.CurrentRecord.CDEPT_CODE;
            loParam.CREF_NO = _viewModel.CurrentRecord.CREF_NO;
            loParam.CDEPT_CODE_DEFAULT = _viewModel.SystemParam.CASSET_DEPT_CODE;
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(FAT00700_TransactionEntry);
        }

        public async Task AfterTransEntry(R_AfterOpenPredefinedDockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
               await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region On Changed

        public void OnChangedYearFrom(int value)
        {
            _viewModel.IYEAR_FROM = value;
            _viewModel.SetPeriodFromTo();
        }

        public void OnChangedYearTo(int value)
        {
            _viewModel.IYEAR_TO = value;
            _viewModel.SetPeriodFromTo();
        }

        public void OnChangedMonthFrom(string value)
        {
            _viewModel.CMONTH_FROM = value;
            _viewModel.SetPeriodFromTo();
        }

        public void OnChangedMonthTo(string value)
        {
            _viewModel.CMONTH_TO = value;
            _viewModel.SetPeriodFromTo();
        }

        #endregion

        #region Validation
        public async Task ValidationSystemParam()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var lcTemp = _viewModel.SystemParam.CTRANS_DEPT_CODE;

                if (_viewModel.SystemParam.CCOMPANY_ID == "")
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "ErrorSystemParam"));
                    await this.CloseProgramAsync();
                }
                else if (!_viewModel.loListDept.Any(x => x.CDEPT_CODE == lcTemp))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "ErrorDepartment"));
                }

                if (!loEx.HasError)
                {
                    //Set Default Value Department
                    _viewModel.SetDefaultLookUpDepartment();
                    await Task.CompletedTask;
                }
            }
            catch (Exception ex)    
            {

                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void ValidationDeptCode()
        {
            _viewModel.ValidationDepartment();
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
        #endregion
    }
}
