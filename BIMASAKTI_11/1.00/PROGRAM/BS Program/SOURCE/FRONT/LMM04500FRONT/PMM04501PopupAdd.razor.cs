using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Microsoft.AspNetCore.Components;
using PMM04500COMMON;
using PMM04500COMMON.DTO_s;
using PMM04500MODEL;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using static PMM04500MODEL.PMM04500ViewModel;

namespace PMM04500FRONT
{
    public partial class PMM04501PopupAdd : R_Page
    {
        private R_ConductorGrid _conGridPricing;

        private PMM04500ViewModel _pricingAdd_ViewModel = new();

        private R_Grid<PricingBulkSaveDTO> _gridPricing;

        [Inject] private R_ILocalizer<PMM04500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        [Inject] private IClientHelper? _clientHelper { get; set; }

        [Inject] R_PopupService PopupService { get; set; }

        public R_DatePicker<DateTime?> _validDateForm;

        private R_CheckBox _checkBoxActive;

        //private bool _enableGridAdd = true;

        //private bool _enableGridEdit = true;

        //private bool _enableGridDelete = true;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                PricingParamDTO loParam = R_FrontUtility.ConvertObjectToObject<PricingParamDTO>(poParameter);
                _pricingAdd_ViewModel._propertyId = loParam.CPROPERTY_ID;
                _pricingAdd_ViewModel._unitTypeCategoryId = loParam.CUNIT_TYPE_CATEGORY_ID;
                _pricingAdd_ViewModel._unitTypeCategoryName = loParam.CUNIT_TYPE_CATEGORY_NAME;
                _pricingAdd_ViewModel._action = loParam.CACTION;
                _pricingAdd_ViewModel._active = loParam.LACTIVE;
                switch (_pricingAdd_ViewModel._action)
                {
                    case "ADD":
                        _checkBoxActive.Enabled = true;
                        //_enableGridEdit = false;
                        //_enableGridDelete = false;
                        _pricingAdd_ViewModel._validId = "";
                        break;
                    case "EDIT":
                        //_enableGridDelete = false;
                        //_enableGridAdd = true;
                        _validDateForm.Enabled = false;
                        _pricingAdd_ViewModel._validId = loParam.CVALID_INTERNAL_ID;
                        break;
                    case "DELETE":
                        //_enableGridAdd = false;
                        //_enableGridEdit = false;
                        _validDateForm.Enabled = false;
                        _pricingAdd_ViewModel._validId = loParam.CVALID_INTERNAL_ID;
                        break;
                    default:
                        break;
                }
                if (!string.IsNullOrWhiteSpace(loParam.CVALID_DATE) || !string.IsNullOrEmpty(loParam.CVALID_DATE))
                {
                    _pricingAdd_ViewModel._validDateForm = DateTime.ParseExact(loParam.CVALID_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                }

                await _pricingAdd_ViewModel.GetPriceType();//get list for price type
                await _pricingAdd_ViewModel.GetChargesType();//get list for charges type
                await _pricingAdd_ViewModel.GetInvoicePeriodTypeList();//get list for invoice period type
                await _gridPricing.R_RefreshGrid(null);//refresh grid param
                await _validDateForm.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task PricingAdd_GetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _pricingAdd_ViewModel.GetPricingForSaveList();
                eventArgs.ListEntityResult = _pricingAdd_ViewModel._pricingSaveList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void PricingAdd_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loCurrData = R_FrontUtility.ConvertObjectToObject<PricingBulkSaveDTO>(eventArgs.Data);
                _pricingAdd_ViewModel._currentSelectedChargesType = loCurrData.CCHARGES_TYPE;
                eventArgs.Result = loCurrData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task PricingAdd_AfterAddAsync(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = (PricingBulkSaveDTO)eventArgs.Data;
                loData.DUPDATE_DATE = DateTime.Now;
                loData.DCREATE_DATE = DateTime.Now;
                loData.CVALID_INTERNAL_ID = _pricingAdd_ViewModel._validId;
                loData.CCHARGES_ID = "";
                loData.CCHARGES_TYPE = "";
                loData.CPRICE_MODE = "";
                loData.CINVOICE_PERIOD = "";
                await _pricingAdd_ViewModel.GetPriceType();//get newest list for price type 
                await _pricingAdd_ViewModel.GetChargesType();//get newest list for charges type
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void PricingAdd_CellValueChanged(R_CellValueChangedEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.ColumnName == nameof(PricingBulkSaveDTO.CCHARGES_TYPE))
                {
                    //get current selected charges type
                    _pricingAdd_ViewModel._currentSelectedChargesType = eventArgs.Value.ToString();
                    var loCurrData = (PricingBulkSaveDTO)eventArgs.CurrentRow;
                    loCurrData.CCHARGES_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private bool _enableBtnProcess = true;

        private bool _enableBtnCancel = true;

        private void PricingAdd_SetOther(R_SetEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _enableBtnCancel = eventArgs.Enable;
                _enableBtnProcess = eventArgs.Enable;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region LookupColumn

        private void PricingAdd_BeforeLookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loParam = new LML00200ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CPROPERTY_ID = _pricingAdd_ViewModel._propertyId,
                CCHARGE_TYPE_ID = _pricingAdd_ViewModel._currentSelectedChargesType,
                CUSER_ID = _clientHelper.UserId,
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(LML00200);
        }

        private void PricingAdd_AfterLookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                switch (eventArgs.ColumnName)
                {
                    case nameof(PricingBulkSaveDTO.CCHARGES_NAME):
                        var loResult = R_FrontUtility.ConvertObjectToObject<LML00200DTO>(eventArgs.Result);
                        ((PricingBulkSaveDTO)eventArgs.ColumnData).CCHARGES_NAME = loResult.CCHARGES_NAME;
                        ((PricingBulkSaveDTO)eventArgs.ColumnData).CCHARGES_ID = loResult.CCHARGES_ID;
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region button

        private async Task PricingAdd_CancelAsync()
        {
            R_Exception loEx = new();
            try
            {
                await this.Close(false, null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task PricingAdd_Process()
        {
            GFF00900ParameterDTO loParam = null;
            R_PopupResult loResult = null;
            R_Exception loEx = new();
            try
            {
                //Approval before saving
                if (_pricingAdd_ViewModel._validDateForm < DateTime.Now.Date)
                {
                    //run validate
                    var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel
                    {
                        ACTIVATE_INACTIVE_ACTIVITY_CODE = "PMM04501"
                    };
                    await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync();

                    //check if activity code no need approval
                    if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                    {
                        await _gridPricing.R_RefreshGrid(null);
                    }
                    else
                    {
                        //if its need approval then run approval form
                        loParam = new GFF00900ParameterDTO()
                        {
                            Data = loValidateViewModel.loRspActivityValidityList,
                            IAPPROVAL_CODE = "PMM04501"
                        };
                        loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loParam);

                        //if not success show message
                        if (!loResult.Success || !(bool)loResult.Result)
                        {
                            var loMsgResult = await R_MessageBox.Show("", "Valid Date must be greater than Today", R_eMessageBoxButtonType.OK);
                            _pricingAdd_ViewModel._validDateForm = DateTime.Now; //set value
                            goto EndBlock;//end process if null
                        }

                    }
                }
                //~End approval

                _pricingAdd_ViewModel._validDate = _pricingAdd_ViewModel._validDateForm.Value.ToString("yyyyMMdd");

                await _pricingAdd_ViewModel.SavePricing(_gridPricing.DataSource.ToList());
                if (!loEx.HasError)
                {
                    R_eMessageBoxResult loMessageResult = await R_MessageBox.Show("", _localizer["_msg_process_complete"], R_eMessageBoxButtonType.OK);
                }
                await this.Close(true, null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region CheckGrid Add/Edit/Delete
        private void CheckGridAdd(R_CheckGridEventArgs eventArgs)
        {
            eventArgs.Allow = _pricingAdd_ViewModel._action is "ADD" or "EDIT";
        }
        private void CheckGridEdit(R_CheckGridEventArgs eventArgs)
        {
            eventArgs.Allow = _pricingAdd_ViewModel._action == "EDIT";
        }
        private void CheckGridDelete(R_CheckGridEventArgs eventArgs)
        {
            eventArgs.Allow = _pricingAdd_ViewModel._action == "DELETE";
        }
        #endregion

    }
}
