using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00200;
using Lookup_PMModel.ViewModel.LML00700;
using Microsoft.AspNetCore.Components;
using PMT05000COMMON.DTO_s;
using PMT05000FrontResources;
using PMT05000MODEL.ViewModel_s;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System.Collections.ObjectModel;

namespace PMT05000FRONT
{
    public partial class PMT05000 : R_Page
    {
        private PMT05000ViewModel _agreementChrgDiscViewModel = new PMT05000ViewModel();

        private R_ConductorGrid _conAgrChrgDisc;

        private R_Grid<AgreementChrgDiscDetailDTO> _GridAgrChrgDisc;

        private R_TabStrip _tabStripAgrChrgDisc; //ref Tabstrip

        private R_TabPage _tabUndoAgrChrgDisc; //tabpageNextPricing

        [Inject] IClientHelper _clientHelper { get; set; }

        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }

        private bool _comboboxPropertyEnabled = true; //to prevent combobox while crudmode

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _agreementChrgDiscViewModel.InitAsync(_localizer);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        public async Task ComboboxPropertyValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_ID = "";
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_NAME = ""; //assign bind textbox name kalo ada
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_ID = "";
                _agreementChrgDiscViewModel._chargesName = "";
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_ID = "";
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_NAME = "";
                await Task.Delay(300);
                if (_conAgrChrgDisc.R_ConductorMode == R_eConductorMode.Normal && _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID != "")
                {
                    //sending property to another tab (will be catch at RefreshTabPageAsync)
                    switch (_tabStripAgrChrgDisc.ActiveTab.Id)
                    {
                        case nameof(PMT05001):
                            await _tabUndoAgrChrgDisc.InvokeRefreshTabPageAsync(_agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        public void OnChangedCombo_UnitCharges()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrWhiteSpace(_agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_TYPE))
                {
                    _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_ID = "";
                    _agreementChrgDiscViewModel._chargesName = "";
                    _agreementChrgDiscViewModel._discountName = "";
                    _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_CODE = "";
                    _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_TYPE = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task OnChanged_YearPeriod()
        {
            var loEx = new R_Exception();
            try
            {
                var loData = await _agreementChrgDiscViewModel.GetPeriodDTList();
                _agreementChrgDiscViewModel._MonthPeriodList = new ObservableCollection<GSPeriodDT_DTO>(loData);
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_CODE = "";
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_TYPE = "";
                _agreementChrgDiscViewModel._discountName = "";

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public void OnChanged_MonthPeriod()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_CODE = "";
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_TYPE = "";
                _agreementChrgDiscViewModel._discountName = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void BeforeOpenTabPage_Undo(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID;
            eventArgs.TargetPageType = typeof(PMT05001);
        }

        private async Task AgrChrgDiscList_GetList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _agreementChrgDiscViewModel.GetAgreementChrgDiscListAsync(_clientHelper.CompanyId);
                eventArgs.ListEntityResult = _agreementChrgDiscViewModel._AgreementChrgDiscDetailList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void AgrChrgDiscList_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Result = eventArgs.Data as AgreementChrgDiscDetailDTO;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public void OnChanged_AllBuilding()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (_agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.LALL_BUILDING)
                {
                    _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_ID = "";
                    _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task BtnRefresh_Click()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _agreementChrgDiscViewModel.GetAgreementChrgDiscListAsync(_clientHelper.CompanyId);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task BtnProcess_ClickAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _agreementChrgDiscViewModel.ProcessAgreementChrgDiscAsync("PROCESS");
                if (!loEx.HasError)
                {
                    var loMsg = await R_MessageBox.Show("", _localizer["_msg_process_success"], R_eMessageBoxButtonType.OK);
                }
                await _agreementChrgDiscViewModel.GetAgreementChrgDiscListAsync(_clientHelper.CompanyId);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        private void AgrChrgDiscList_CheckBoxSelectValueChanged(R_CheckBoxSelectValueChangedEventArgs eventArgs)
        {
            eventArgs.Enabled = !eventArgs.Enabled;
        }


        #region lookup

        private void BeforeOpen_lookupUnitCharges(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LML00200ParameterDTO()
            {
                CPROPERTY_ID = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID ?? "",
                CCOMPANY_ID = _clientHelper.CompanyId,
                CUSER_ID = _clientHelper.UserId,
                CCHARGE_TYPE_ID = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_TYPE,
                CACTIVE_TYPE = "",
                CSEARCH_TEXT = "",
                CTAXABLE_TYPE = "",
                CTAX_DATE = ""
            };
            eventArgs.TargetPageType = typeof(LML00200);
        }

        private async Task AfterOpen_lookupUnitChargesAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00200DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_ID = loTempResult.CCHARGES_ID;
                _agreementChrgDiscViewModel._chargesName = loTempResult.CCHARGES_NAME;
            }
            else
            {
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_ID = "";
                _agreementChrgDiscViewModel._chargesName = "";
            }
        }

        private async Task OnLostFocus_LookupUnitChargesAsync()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_ID))
                {

                    LookupLML00200ViewModel loLookupViewModel = new(); //use GSL's model
                    var loResult = await loLookupViewModel.GetUnitCharges(new LML00200ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CPROPERTY_ID = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID ?? "",
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_ID,
                        CCHARGE_TYPE_ID = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_TYPE,
                        CACTIVE_TYPE = "",
                        CTAXABLE_TYPE = "",
                        CTAX_DATE = ""
                        // property that bindded to search textbox
                    }); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                "_ErrLookup01"));
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_ID = "";
                        _agreementChrgDiscViewModel._chargesName = "";//kosongin bind textbox name kalo gaada
                    }
                    else
                    {
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_ID = loResult.CCHARGES_ID;
                        _agreementChrgDiscViewModel._chargesName = loResult.CCHARGES_NAME;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }

        private void BeforeOpen_lookupDiscount(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LML00700ParameterDTO()
            {
                CPROPERTY_ID = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID ?? "",
                CCOMPANY_ID = _clientHelper.CompanyId,
                CUSER_ID = _clientHelper.UserId,
                CCHARGES_TYPE = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_TYPE ?? "",
                CINV_PRD = (_agreementChrgDiscViewModel._yearPeriod.ToString() + _agreementChrgDiscViewModel._monthPeriod) ?? ""
            };
            eventArgs.TargetPageType = typeof(LML00700);
        }

        private async Task AfterOpen_lookupDiscountAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00700DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_CODE = loTempResult.CDISCOUNT_CODE;
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_TYPE = loTempResult.CDISCOUNT_TYPE;
                _agreementChrgDiscViewModel._discountName = loTempResult.CDISCOUNT_NAME;
            }
            else
            {
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_CODE = "";
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_TYPE = "";
                _agreementChrgDiscViewModel._discountName = "";
            }
        }

        private async Task OnLostFocus_LookupDiscountAsync()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_CODE))
                {

                    LookupLML00700ViewModel loLookupViewModel = new(); //use GSL's model
                    var loResult = await loLookupViewModel.GetDiscount(new LML00700ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CPROPERTY_ID = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID ?? "",
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CCHARGES_TYPE = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CCHARGES_TYPE ?? "",
                        CINV_PRD = (_agreementChrgDiscViewModel._yearPeriod.ToString() + _agreementChrgDiscViewModel._monthPeriod) ?? "",
                        CSEARCH_TEXT = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_CODE, // property that bindded to search textbox
                    }); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                                "_ErrLookup01"));
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_CODE = ""; //kosongin bind textbox name kalo gaada
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_TYPE = ""; //kosongin bind textbox name kalo gaada
                        _agreementChrgDiscViewModel._discountName = "";//kosongin bind textbox name kalo gaada
                    }
                    else
                    {
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_CODE = loResult.CDISCOUNT_CODE;
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CDISCOUNT_TYPE = loResult.CDISCOUNT_TYPE;
                        _agreementChrgDiscViewModel._discountName = loResult.CDISCOUNT_NAME;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }

        private void BeforeOpen_lookupBuilding(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL02200ParameterDTO()
            {
                CPROPERTY_ID = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID ?? "",
                CSEARCH_TEXT = ""
            };
            eventArgs.TargetPageType = typeof(GSL02200);
        }

        private async Task AfterOpen_lookupBuildingAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL02200DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_ID = loTempResult.CBUILDING_ID;
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_NAME = loTempResult.CBUILDING_NAME;
            }
            else
            {
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_ID = "";
                _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_NAME = "";
            }

        }

        private async Task OnLostFocus_LookupBuilding()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_ID))
                {

                    LookupGSL02200ViewModel loLookupViewModel = new LookupGSL02200ViewModel(); //use GSL's model
                    var loParam = new GSL02200ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {

                        CPROPERTY_ID = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CPROPERTY_ID ?? "",
                        CSEARCH_TEXT = _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_ID, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetBuilding(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_NAME = ""; //kosongin bind textbox name kalo gaada
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_NAME = "";
                    }
                    else
                    {
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_ID = loResult.CBUILDING_ID;
                        _agreementChrgDiscViewModel._AgreementChrgDiscProcessParam.CBUILDING_NAME = loResult.CBUILDING_NAME; //assign bind textbox name kalo ada
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        #endregion

    }
}


