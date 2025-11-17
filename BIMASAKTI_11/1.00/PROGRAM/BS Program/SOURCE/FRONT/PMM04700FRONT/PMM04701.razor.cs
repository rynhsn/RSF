using PMM04700Common.DTOs;
using PMM4700MODEL;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GFF00900COMMON.DTOs;
using Microsoft.AspNetCore.Components;
using PMM4700MODEL.ViewModel;
using R_BlazorFrontEnd.Controls.Popup;

namespace PMM04700FRONT
{
    public partial class PMM04701 : R_Page, R_ITabPage
    {
        private PMM04700ViewModel _viewModelPricing = new();

        private R_Conductor _conUnitTypeCTG;
        private R_Grid<OtherUnitDTO> _gridOtherUnit;

        private R_Conductor _conPricing;
        private R_Grid<PricingDTO> _gridPricing;

        private R_Conductor _conPricingDate;

        private R_Grid<PricingDTO> _gridPricingDate;

        private R_Popup R_PopupAdd;
        private R_Popup R_PopupEdit;
        private R_Popup R_PopupDelete;

        private R_Button _btnActiveinactive;

        private string _lcActiveInactiveLabel = "Active/Inactive";

        [Inject] R_PopupService PopupService { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<string>(poParameter);
                _viewModelPricing._propertyId = loParam;
                if (_viewModelPricing._propertyId != "")
                {
                    await _gridOtherUnit.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
            await Task.CompletedTask;
        }

        public async Task RefreshTabPageAsync(object poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModelPricing._propertyId = (string)poParam;
                await Task.Delay(300);
                await _gridOtherUnit.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region OtherUnit
        private async Task OtherUnit_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelPricing.GetUnitCategoryList();
                eventArgs.ListEntityResult = _viewModelPricing._OtherUnitList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);

        }

        private void OtherUnit_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                eventArgs.Result = R_FrontUtility.ConvertObjectToObject<OtherUnitDTO>(eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OtherUnit_ServiceDisplay(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<OtherUnitDTO>(eventArgs.Data);
                _viewModelPricing._OtherUnitId = loParam.COTHER_UNIT_TYPE_ID;
                _viewModelPricing._OtherUnitName = loParam.COTHER_UNIT_TYPE_NAME;
                await _gridPricingDate.R_RefreshGrid(null);
                if (_viewModelPricing._pricingDateList.Count < 1)
                {
                    //disable button & popup if pricing date list doesnt exist
                    R_PopupDelete.Enabled = false;
                    R_PopupEdit.Enabled = false;
                    _btnActiveinactive.Enabled = false;

                    //clear pricing list
                    _viewModelPricing._pricingList = new();
                }
                else
                {
                    R_PopupDelete.Enabled = true;
                    R_PopupEdit.Enabled = true;
                    _btnActiveinactive.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region PricingDate

        private async Task NextPricingDate_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelPricing.GetPricingList(PMM04700ViewModel.eListPricingParamType.GetNext, true);

                eventArgs.ListEntityResult = _viewModelPricing._pricingDateList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void NextPricingDate_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                eventArgs.Result = R_FrontUtility.ConvertObjectToObject<PricingDTO>(eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task NextPricingDate_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<PricingDTO>(eventArgs.Data);
                _viewModelPricing._validDate = loData.CVALID_DATE;
                _viewModelPricing._validId = loData.CVALID_INTERNAL_ID;
                _viewModelPricing._active = loData.LACTIVE;
                _lcActiveInactiveLabel = loData.LACTIVE ? "Inactive" : "Active";
                if (_viewModelPricing._active)
                {
                    R_PopupAdd.Enabled = true;
                    R_PopupEdit.Enabled = true;
                    R_PopupDelete.Enabled = true;
                }
                else
                {
                    R_PopupAdd.Enabled = false;
                    R_PopupEdit.Enabled = false;
                    R_PopupDelete.Enabled = false;
                }
                await _gridPricing.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task Btn_Activeinactive_OnClickAsync()
        {
            var loEx = new R_Exception();
            GFF00900ParameterDTO loParam = null;
            R_PopupResult loResult = null;
            try
            {
                //run validate
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel
                {
                    ACTIVATE_INACTIVE_ACTIVITY_CODE = "PMM04502"
                };
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync();

                //check if activity code no need approval
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await _viewModelPricing.ActiveInacvitePricing();
                    await _gridPricingDate.R_RefreshGrid(null);
                }
                else
                {
                    //if its need approval then run approval form
                    loParam = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "PMM04502"
                    };
                    loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loParam);

                    //if not success show message
                    if (!loResult.Success || !(bool)loResult.Result)
                    {
                        //var loMsgResult = await R_MessageBox.Show("", "Valid Date must be greater than Today", R_eMessageBoxButtonType.OK);
                        //await _gridPricingDate.R_RefreshGrid(null);
                        goto EndBlock;//end process if null
                    }
                    else
                    {
                        await _viewModelPricing.ActiveInacvitePricing();
                        await _gridPricingDate.R_RefreshGrid(null);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Pricing

        private async Task NextPricing_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelPricing.GetPricingList(PMM04700ViewModel.eListPricingParamType.GetNext, false);
                eventArgs.ListEntityResult = _viewModelPricing._pricingList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void NextPricing_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = R_FrontUtility.ConvertObjectToObject<PricingDTO>(eventArgs.Data);
        }

        #endregion

        #region Add

        private void BeforeOpenPopup_AddNextPricing(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = new PricingParamDTO()
            {
                CPROPERTY_ID = _viewModelPricing._propertyId,
                COTHER_UNIT_TYPE_ID = _viewModelPricing._OtherUnitId,
                COTHER_UNIT_TYPE_NAME = _viewModelPricing._OtherUnitName,
                CVALID_INTERNAL_ID = "",
                LACTIVE = true,
                CACTION = "ADD"
            };
            eventArgs.PageTitle = @_localizer["_pageTitleAddPricing"];
            eventArgs.TargetPageType = typeof(PMM04701PopupAdd);
        }

        private async Task AfterOpenPopup_AddNextPricing(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Success == true)
                {
                    await _gridPricingDate.R_RefreshGrid(null);
                    if (_viewModelPricing._pricingDateList.Count < 1)
                    {
                        //disable button & popup if pricing date list doesnt exist
                        R_PopupDelete.Enabled = false;
                        R_PopupEdit.Enabled = false;
                        _btnActiveinactive.Enabled = false;

                        //clear pricing list
                        _viewModelPricing._pricingList = new();
                    }
                    else
                    {
                        R_PopupDelete.Enabled = true;
                        R_PopupEdit.Enabled = true;
                        _btnActiveinactive.Enabled = true;
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

        #region Delete

        private void BeforeOpenPopup_DeleteNextPricing(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = new PricingParamDTO()
            {
                CPROPERTY_ID = _viewModelPricing._propertyId,
                COTHER_UNIT_TYPE_ID = _viewModelPricing._OtherUnitId,
                COTHER_UNIT_TYPE_NAME = _viewModelPricing._OtherUnitName,
                CVALID_DATE = _viewModelPricing._validDate,
                CVALID_INTERNAL_ID = _viewModelPricing._validId,
                LACTIVE = _viewModelPricing._active,
                CACTION = "DELETE"
            };
            eventArgs.PageTitle = @_localizer["_pageTitleDeletePricing"];
            eventArgs.TargetPageType = typeof(PMM04701PopupAdd);
        }

        private async Task AfterOpenPopup_DeleteNextPricing(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Success == true)
                {
                    await _gridPricing.R_RefreshGrid(null);

                    if (_viewModelPricing._pricingDateList.Count < 1)
                    {
                        //disable button & popup if pricing date list doesnt exist
                        R_PopupDelete.Enabled = false;
                        R_PopupEdit.Enabled = false;
                        _btnActiveinactive.Enabled = false;

                        //clear pricing list
                        _viewModelPricing._pricingList = new();
                    }
                    else
                    {
                        R_PopupDelete.Enabled = true;
                        R_PopupEdit.Enabled = true;
                        _btnActiveinactive.Enabled = true;
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

        #region Edit

        private void BeforeOpenPopup_EditNextPricing(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = new PricingParamDTO()
            {
                CPROPERTY_ID = _viewModelPricing._propertyId,
                COTHER_UNIT_TYPE_ID = _viewModelPricing._OtherUnitId,
                COTHER_UNIT_TYPE_NAME = _viewModelPricing._OtherUnitName,
                CVALID_DATE = _viewModelPricing._validDate,
                CVALID_INTERNAL_ID = _viewModelPricing._validId,
                LACTIVE = _viewModelPricing._active,
                CACTION = "EDIT"
            };
            eventArgs.PageTitle = @_localizer["_pageTitleEditPricing"];
            eventArgs.TargetPageType = typeof(PMM04701PopupEdit);
        }

        private async Task AfterOpenPopup_EditNextPricing(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Success == true)
                {
                    await _gridPricing.R_RefreshGrid(null);
                    if (_viewModelPricing._pricingDateList.Count < 1)
                    {
                        //disable button & popup if pricing date list doesnt exist
                        R_PopupDelete.Enabled = false;
                        R_PopupEdit.Enabled = false;
                        _btnActiveinactive.Enabled = false;

                        //clear pricing list
                        _viewModelPricing._pricingList = new();
                    }
                    else
                    {
                        R_PopupDelete.Enabled = true;
                        R_PopupEdit.Enabled = true;
                        _btnActiveinactive.Enabled = true;
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
