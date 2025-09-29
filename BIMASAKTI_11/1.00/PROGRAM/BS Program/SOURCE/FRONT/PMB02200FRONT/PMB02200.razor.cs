using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using PMB02200MODEL.View_Models;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Interfaces;
using PMB02200FrontResources;
using R_BlazorFrontEnd.Exceptions;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Helpers;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00600;
using PMB02200COMMON.DTO_s;
using PMB02200COMMON;
using R_BlazorFrontEnd.Controls.MessageBox;
using Lookup_PMModel.ViewModel.LML00400;
using R_APICommonDTO;

namespace PMB02200FRONT
{
    public partial class PMB02200 : R_Page
    {
        private PMB02200BatchViewModel _viewModel = new();

        private R_ConductorGrid _conductorGridRef;

        private R_Grid<UtilityChargesDTO> _gridRef;
        private int _pageUtilCharge = 10;

        #region Action

        private void StateChangeInvoke()
        {
            StateHasChanged();
        }

        private void DisplayErrorInvoke(R_APIException poEx)
        {
            var loEx = R_FrontUtility.R_ConvertFromAPIException(poEx);
            this.R_DisplayException(loEx);
        }

        private async Task ShowSuccessInvoke()
        {
            R_Exception loEx = new();
            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["_msg_batchComplete"], R_eMessageBoxButtonType.OK);
                await _gridRef.R_RefreshGrid(eListUtilChrgActionType.GetList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        [Inject] IClientHelper _clientHelper { get; set; }

        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel.StateChangeAction = StateChangeInvoke;
                _viewModel.DisplayErrorAction = DisplayErrorInvoke;
                _viewModel.ShowSuccessAction = async () =>
                {
                    await ShowSuccessInvoke();
                };
                await _viewModel.InitProcessAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #region OnChange & OnvalueChanged

        private void ComboboxProperty_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel.UtilityChargesParam.CPROPERTY_ID = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;
                _viewModel.UtilityChargesParam.CDEPT_CODE = "";
                _viewModel.UtilityChargesParam.CDEPT_NAME = "";
                _viewModel.UtilityChargesParam.CBUILDING_ID = "";
                _viewModel.UtilityChargesParam.CBUILDING_NAME = "";
                _viewModel.UtilityChargesParam.CTENANT_ID = "";
                _viewModel.UtilityChargesParam.CTENANT_NAME = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void ComboboxUtilType_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel.UtilityChargesParam.CUTILITY_TYPE = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;
                _viewModel.NewChargeId = "";
                _viewModel.OldChargeId = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void CheckBoxAllBuilding_OnChange()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel.UtilityChargesParam.CBUILDING_ID = "";
                _viewModel.UtilityChargesParam.CBUILDING_NAME = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #endregion

        #region Grid Utility Charges

        private async Task ChargesList_GetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eListUtilChrgActionType loListUtilChrgActionType = (eListUtilChrgActionType)eventArgs.Parameter;

                switch (loListUtilChrgActionType)
                {
                    case eListUtilChrgActionType.GetList:
                        await _viewModel.GetUtilityChargesListAsync();
                        break;
                    case eListUtilChrgActionType.ApplyNewCharge:
                        _viewModel.ApplyNewIdOnList(eListUtilChrgActionType.ApplyNewCharge);
                        break;
                    case eListUtilChrgActionType.ApplyNewTax:
                        _viewModel.ApplyNewIdOnList(eListUtilChrgActionType.ApplyNewTax);
                        break;
                    default:
                        break;
                }
                eventArgs.ListEntityResult = _viewModel._utilityChargesList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void ChargesList_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data as UtilityChargesDTO;
        }

        #endregion

        #region Buttons

        private async Task BtnOnclick_UtilChrgRefresh()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                RefreshUtilChrg_Validation();
                await _gridRef.R_RefreshGrid(eListUtilChrgActionType.GetList);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task BtnOnclick_ChangeChargeAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                ApplyNewChrg_Validaiton();
                await _gridRef.R_RefreshGrid(eListUtilChrgActionType.ApplyNewCharge);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task BtnOnclick_ChangeTaxAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                ApplyNewTax_Validation();
                await _gridRef.R_RefreshGrid(eListUtilChrgActionType.ApplyNewTax);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task BtnOnclick_ProcessAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                Process_Validation();
                await _viewModel.ProcessSelectedData(_clientHelper.CompanyId, _clientHelper.UserId);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Validation

        private void RefreshUtilChrg_Validation()
        {
            R_Exception loEx = new R_Exception();
            if (string.IsNullOrWhiteSpace(_viewModel.UtilityChargesParam.CPROPERTY_ID))
            {
                loEx.Add("", _localizer["_val_RefreshProcess1"]);
            }
            if (string.IsNullOrWhiteSpace(_viewModel.UtilityChargesParam.CDEPT_CODE))
            {
                loEx.Add("", _localizer["_val_RefreshProcess2"]);
            }
            if (_viewModel.UtilityChargesParam.LALL_BUILDING == false && string.IsNullOrWhiteSpace(_viewModel.UtilityChargesParam.CBUILDING_ID))
            {
                loEx.Add("", _localizer["_val_RefreshProcess3"]);
            }
            if (string.IsNullOrWhiteSpace(_viewModel.UtilityChargesParam.CUTILITY_TYPE))
            {
                loEx.Add("", _localizer["_val_RefreshProcess4"]);
            }
            if (loEx.HasError)
            {
                loEx.ThrowExceptionIfErrors();
                return;
            }
        }

        private void ApplyNewChrg_Validaiton()
        {
            R_Exception loEx = new R_Exception();
            if (string.IsNullOrWhiteSpace(_viewModel.OldChargeId) || string.IsNullOrWhiteSpace(_viewModel.NewChargeId))
            {
                loEx.Add("", _localizer["_val_ApplyProcess1"]);
            }
            if (_viewModel.OldChargeId == _viewModel.NewChargeId)
            {
                loEx.Add("", _localizer["_val_ApplyProcess2"]);
            }
            if (loEx.HasError)
            {
                loEx.ThrowExceptionIfErrors();
            }
        }

        private void ApplyNewTax_Validation()
        {
            R_Exception loEx = new R_Exception();
            if (string.IsNullOrWhiteSpace(_viewModel.OldTaxId))
            {
                loEx.Add("", _localizer["_val_ApplyProcess3"]);
            }
            if (_viewModel.OldTaxId == _viewModel.NewTaxId)
            {
                loEx.Add("", _localizer["_val_ApplyProcess4"]);
            }
            if (loEx.HasError)
            {
                loEx.ThrowExceptionIfErrors();
            }
        }

        private void Process_Validation()
        {
            R_Exception loEx = new R_Exception();
            bool llSelected = _viewModel._utilityChargesList.Any(x => x.LSELECTED);

            if (llSelected == false)
            {
                loEx.Add("", _localizer["_val_UpdateUtilProcess1"]);
            }

            if (loEx.HasError)
            {
                loEx.ThrowExceptionIfErrors();
            }
        }

        #endregion

        #region Lookup

        private void BeforeOpen_lookupDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL00710ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CUSER_LOGIN_ID = _clientHelper.UserId,
                CPROPERTY_ID = _viewModel.UtilityChargesParam.CPROPERTY_ID ?? "",
            };
            eventArgs.TargetPageType = typeof(GSL00710);
        }

        private async Task AfterOpen_lookupDeptAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel.UtilityChargesParam.CDEPT_CODE = loTempResult.CDEPT_CODE;
                _viewModel.UtilityChargesParam.CDEPT_NAME = loTempResult.CDEPT_NAME;
            }
            await Task.CompletedTask;
        }

        private async Task OnLostFocus_LookupDept()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.UtilityChargesParam.CDEPT_CODE))
                {

                    LookupGSL00710ViewModel loLookupViewModel = new LookupGSL00710ViewModel(); //use GSL's model
                    var loParam = new GSL00710ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_LOGIN_ID = _clientHelper.UserId,
                        CPROPERTY_ID = _viewModel.UtilityChargesParam.CPROPERTY_ID ?? "",
                        CSEARCH_TEXT = _viewModel.UtilityChargesParam.CDEPT_CODE ?? "", // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetDepartmentProperty(loParam); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.UtilityChargesParam.CDEPT_NAME = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel.UtilityChargesParam.CDEPT_CODE = loResult.CDEPT_CODE;
                    _viewModel.UtilityChargesParam.CDEPT_NAME = loResult.CDEPT_NAME; //assign bind textbox name kalo ada
                }
                else
                {
                    _viewModel.UtilityChargesParam.CDEPT_CODE = "";
                    _viewModel.UtilityChargesParam.CDEPT_NAME = "";
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
                CSEARCH_TEXT = "",
                CPROPERTY_ID = _viewModel.UtilityChargesParam.CPROPERTY_ID,
            };
            eventArgs.TargetPageType = typeof(GSL02200);
        }

        private async Task AfterOpen_lookupBuilding(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL02200DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel.UtilityChargesParam.CBUILDING_ID = loTempResult.CBUILDING_ID;
                _viewModel.UtilityChargesParam.CBUILDING_NAME = loTempResult.CBUILDING_NAME;
            }
            await Task.CompletedTask;
        }

        private async Task OnLostFocus_LookupBuilding()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.UtilityChargesParam.CBUILDING_ID))
                {

                    LookupGSL02200ViewModel loLookupViewModel = new LookupGSL02200ViewModel(); //use GSL's model
                    var loParam = new GSL02200ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {

                        CPROPERTY_ID = _viewModel.UtilityChargesParam.CPROPERTY_ID,
                        CSEARCH_TEXT = _viewModel.UtilityChargesParam.CBUILDING_ID, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetBuilding(loParam); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.UtilityChargesParam.CBUILDING_NAME = ""; //kosongin bind textbox name kalo gaada
                        _viewModel.UtilityChargesParam.CBUILDING_ID = ""; //kosongin bind textbox name kalo gaada
                        goto EndBlock;
                    }
                    _viewModel.UtilityChargesParam.CBUILDING_ID = loResult.CBUILDING_ID;
                    _viewModel.UtilityChargesParam.CBUILDING_NAME = loResult.CBUILDING_NAME; //assign bind textbox name kalo ada
                }
                else
                {
                    _viewModel.UtilityChargesParam.CBUILDING_ID = "";

                    _viewModel.UtilityChargesParam.CBUILDING_NAME = ""; //assign bind textbox name kalo ada

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);

        }

        private void BeforeOpen_lookupTenant(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LML00600ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CPROPERTY_ID = _viewModel.UtilityChargesParam.CPROPERTY_ID,
                CCUSTOMER_TYPE = "",
                CUSER_ID = _clientHelper.UserId,
                CSEARCH_TEXT = ""
            };
            eventArgs.TargetPageType = typeof(LML00600);
        }

        private async Task AfterOpen_lookupTenantAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00600DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel.UtilityChargesParam.CTENANT_ID = loTempResult.CTENANT_ID;
                _viewModel.UtilityChargesParam.CTENANT_NAME = loTempResult.CTENANT_NAME;
            }
            await Task.CompletedTask;

        }

        private async Task OnLostFocus_LookupTenant()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.UtilityChargesParam.CTENANT_ID))
                {

                    LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel(); //use GSL's model
                    var loParam = new LML00600ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel.UtilityChargesParam.CPROPERTY_ID,
                        CCUSTOMER_TYPE = "",
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel.UtilityChargesParam.CTENANT_ID, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetTenant(loParam); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.UtilityChargesParam.CTENANT_NAME = ""; //kosongin bind textbox name kalo gaada
                        _viewModel.UtilityChargesParam.CTENANT_ID = "";
                    }
                    else
                    {
                        _viewModel.UtilityChargesParam.CTENANT_ID = loResult.CTENANT_ID;
                        _viewModel.UtilityChargesParam.CTENANT_NAME = loResult.CTENANT_NAME; //assign bind textbox name kalo ada
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void BeforeOpen_lookupOldCharges(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = new LML00400ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId ?? "",
                    CPROPERTY_ID = _viewModel.UtilityChargesParam.CPROPERTY_ID ?? "",
                    CCHARGE_TYPE_ID = _viewModel.UtilityChargesParam.CUTILITY_TYPE ?? "",
                    CUSER_ID = _clientHelper.UserId ?? "",
                    CTAXABLE_TYPE = "0",
                    CACTIVE_TYPE = "1",
                    CTAX_DATE = DateTime.Today.ToString("yyyyMMdd"),
                    CSEARCH_TEXT = "",
                };
                eventArgs.TargetPageType = typeof(LML00400);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }

        private async Task AfterOpen_lookupOldChargesAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00400DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel.OldChargeId = loTempResult.CCHARGES_ID;
            }
            await Task.CompletedTask;
        }

        private async Task OnLostFocus_OldChargesAsync()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.OldChargeId))
                {

                    LookupLML00400ViewModel loLookupViewModel = new LookupLML00400ViewModel(); //use GSL's model
                    var loParam = new LML00400ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId ?? "",
                        CPROPERTY_ID = _viewModel.UtilityChargesParam.CPROPERTY_ID ?? "",
                        CCHARGE_TYPE_ID = _viewModel.UtilityChargesParam.CUTILITY_TYPE ?? "",
                        CUSER_ID = _clientHelper.UserId ?? "",
                        CTAXABLE_TYPE = "0",
                        CACTIVE_TYPE = "1",
                        CTAX_DATE = DateTime.Today.ToString("yyyyMMdd"),
                        CSEARCH_TEXT = _viewModel.OldChargeId, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetUtitlityCharges(loParam); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.OldChargeId = "";
                    }
                    else
                    {
                        _viewModel.OldChargeId = loResult.CCHARGES_ID; //assign bind textbox name kalo ada
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void BeforeOpen_lookupNewCharges(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new LML00400ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId ?? "",
                CPROPERTY_ID = _viewModel.UtilityChargesParam.CPROPERTY_ID ?? "",
                CCHARGE_TYPE_ID = _viewModel.UtilityChargesParam.CUTILITY_TYPE ?? "",
                CUSER_ID = _clientHelper.UserId ?? "",
                CTAXABLE_TYPE = "0",
                CACTIVE_TYPE = "1",
                CTAX_DATE = DateTime.Today.ToString("yyyyMMdd"),
                CSEARCH_TEXT = "",
            };
            eventArgs.TargetPageType = typeof(LML00400);
        }

        private async Task AfterOpen_lookupNewChargesAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00400DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel.NewChargeId = loTempResult.CCHARGES_ID;
            }
            await Task.CompletedTask;
        }

        private async Task OnLostFocus_NewChargesAsync()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.NewChargeId))
                {

                    LookupLML00400ViewModel loLookupViewModel = new LookupLML00400ViewModel(); //use GSL's model
                    var loParam = new LML00400ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId ?? "",
                        CPROPERTY_ID = _viewModel.UtilityChargesParam.CPROPERTY_ID ?? "",
                        CCHARGE_TYPE_ID = _viewModel.UtilityChargesParam.CUTILITY_TYPE ?? "",
                        CUSER_ID = _clientHelper.UserId ?? "",
                        CTAXABLE_TYPE = "0",
                        CACTIVE_TYPE = "1",
                        CTAX_DATE = DateTime.Today.ToString("yyyyMMdd"),
                        CSEARCH_TEXT = _viewModel.NewChargeId ?? "", // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetUtitlityCharges(loParam); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.NewChargeId = "";
                    }
                    else
                    {
                        _viewModel.NewChargeId = loResult.CCHARGES_ID; //assign bind textbox name kalo ada
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void BeforeOpen_lookupOldTax(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL00100ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CUSER_ID = _clientHelper.UserId,
                CSEARCH_TEXT = ""
            };
            eventArgs.TargetPageType = typeof(GSL00100);
        }

        private async Task AfterOpen_lookupOldTaxAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00100DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel.OldTaxId = loTempResult.CTAX_ID;
            }
            await Task.CompletedTask;
        }

        private async Task OnLostFocus_OldTaxAsync()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.OldTaxId))
                {

                    LookupGSL00100ViewModel loLookupViewModel = new LookupGSL00100ViewModel(); //use GSL's model
                    var loParam = new GSL00100ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel.OldTaxId, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetSalesTax(loParam); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.OldTaxId = "";
                    }
                    else
                    {
                        _viewModel.OldTaxId = loResult.CTAX_ID; //assign bind textbox name kalo ada
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void BeforeOpen_lookupNewTax(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL00100ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CUSER_ID = _clientHelper.UserId,
                CSEARCH_TEXT = "",
            };
            eventArgs.TargetPageType = typeof(GSL00100);
        }

        private async Task AfterOpen_lookupNewTaxAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00100DTO)eventArgs.Result;
            if (loTempResult != null)
            {
                _viewModel.NewTaxId = loTempResult.CTAX_ID;
            }
            await Task.CompletedTask;
        }

        private async Task OnLostFocus_NewTaxAsync()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.NewTaxId))
                {

                    LookupGSL00100ViewModel loLookupViewModel = new LookupGSL00100ViewModel(); //use GSL's model
                    var loParam = new GSL00100ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel.NewTaxId, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetSalesTax(loParam); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModel.NewTaxId = "";
                    }
                    else
                    {
                        _viewModel.NewTaxId = loResult.CTAX_ID; //assign bind textbox name kalo ada
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
