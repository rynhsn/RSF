using GSM02500MODEL.View_Model;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using GSM02500COMMON.DTOs.GSM02502;
using R_BlazorFrontEnd;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02501;
using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02502Charge;
using GSM02500COMMON.DTOs.GSM02550;
using GSM02500COMMON;
using R_BlazorFrontEnd.Controls.Tab;
using Microsoft.AspNetCore.Components;
using BlazorClientHelper;
using GSM02500COMMON.DTOs.GSM02520;
using R_BlazorFrontEnd.Controls.MessageBox;
using Microsoft.JSInterop;
using System;
using GFF00900COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02541;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Interfaces;
using GSM02500COMMON.DTOs.GSM02503;
using R_BlazorFrontEnd.Controls.Enums;
using R_LockingFront;
using Lookup_PMModel.ViewModel.LML00400;
using Lookup_PMCOMMON.DTOs;

namespace GSM02500FRONT
{
    public partial class GSM02502 : R_Page
    {
        //Unit Type Category
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_PopupService PopupService { get; set; }
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private GSM02502ViewModel loUnitTypeCategoryViewModel = new();

        private GSM02502ChargeViewModel loChargeViewModel = new GSM02502ChargeViewModel();

        private R_Conductor _conductorUnitTypeCategoryRef;

        private R_ConductorGrid _conductorChargeRef;

        private R_Grid<GSM02502DTO> _gridUnitTypeCategoryRef;

        private R_Grid<GSM02502ChargeDTO> _gridChargeRef;

        private string loUnitTypeCategoryLabel = "";

        private TabParameterDTO loTabParameter = new TabParameterDTO();

        private UtilityTabParameterDTO loUtilityTabParameter = new UtilityTabParameterDTO();

        private R_TabStrip _tabStripRef;

        private R_TabPage _tabPageUtility;

        private R_TextBox _unitTypeCategoryIdRef;

        private R_TextBox _unitTypeCategoryNameRef;

        private bool llSingleUnit = false;

        private bool IsTabUtilityOnCRUDMode = false;

        private bool IsUnitTypeCategoryListExist = false;

        private bool IsGridUnitTypeCategoryEnabled = true;

        private bool _gridEnabled = true;

        [Inject] IClientHelper _clientHelper { get; set; }

        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_MODULE_NAME = "GS";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                GSM02502DetailDTO loData = (GSM02502DetailDTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "GSM02500",
                        Table_Name = "GSM_PROPERTY_UNIT_TYPE_CATEGORY",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loTabParameter.CSELECTED_PROPERTY_ID, loData.CUNIT_TYPE_CATEGORY_ID)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "GSM02500",
                        Table_Name = "GSM_PROPERTY_UNIT_TYPE_CATEGORY",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loTabParameter.CSELECTED_PROPERTY_ID, loData.CUNIT_TYPE_CATEGORY_ID)
                    };

                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loUnitTypeCategoryLabel = _localizer["Activate"];
                loUnitTypeCategoryViewModel.SelectedProperty.CPROPERTY_ID = (string)poParameter;
                loUtilityTabParameter.CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID;
                loTabParameter.CSELECTED_PROPERTY_ID = loUnitTypeCategoryViewModel.SelectedProperty.CPROPERTY_ID;
                await loUnitTypeCategoryViewModel.GetSelectedPropertyAsync();
                await loUnitTypeCategoryViewModel.GetPropertyTypeListStreamAsync();
                await loChargeViewModel.GetChargeComboBoxListStreamAsync();
                await _gridUnitTypeCategoryRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task UnitTypeCategory_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }

        private async Task Charge_SetOther(R_SetEventArgs eventArgs)
        {
            _gridEnabled = (bool)eventArgs.Enable;
            IsTabUtilityOnCRUDMode = !(bool)eventArgs.Enable;
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }

        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            if (_conductorUnitTypeCategoryRef.R_ConductorMode == R_eConductorMode.Add || _conductorUnitTypeCategoryRef.R_ConductorMode == R_eConductorMode.Edit)
            {
                eventArgs.Cancel = true;
                return;
            }
            eventArgs.Cancel = IsTabUtilityOnCRUDMode;
        }

        private async Task R_TabEventCallback(object poValue)
        {
            var loEx = new R_Exception();

            try
            {
                _gridEnabled = (bool)poValue;
                IsTabUtilityOnCRUDMode = !(bool)poValue;
                await InvokeTabEventCallbackAsync(poValue);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

            //_pageSupplierOnCRUDmode = !(bool)poValue;
        }

        private async Task OnClickTemplate()
        {
            R_Exception loException = new R_Exception();
            var loData = new List<TemplateUnitTypeCategoryDTO>();
            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["M001"], R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await loUnitTypeCategoryViewModel.DownloadTemplateUnitTypeCategoryAsync();

                    var saveFileName = $"Unit Type Category.xlsx";
                    /*
                                        var saveFileName = $"Staff {CenterViewModel.PropertyValueContext}.xlsx";*/

                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void Before_Open_Upload_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                eventArgs.Parameter = new UploadUnitTypeCategoryParameterDTO()
                {
                    PropertyData = loUnitTypeCategoryViewModel.SelectedProperty
                };
                eventArgs.TargetPageType = typeof(UploadUnitTypeCategory);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task After_Open_Upload_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
            if ((bool)eventArgs.Result == true)
            {
                await _gridUnitTypeCategoryRef.R_RefreshGrid(null);
            }
        }
        
        private void Before_Open_Utility_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loUtilityTabParameter.CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID;
                loUtilityTabParameter.CSELECTED_UNIT_TYPE_CATEGORY_ID = loUnitTypeCategoryViewModel.loUnitTypeCategoryDetail.CUNIT_TYPE_CATEGORY_ID;

                eventArgs.Parameter = loUtilityTabParameter;
                eventArgs.TargetPageType = typeof(GSM02502Utility);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void Before_Open_UnitType_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                UnitTypeTabParameterDTO loParam = new UnitTypeTabParameterDTO()
                {
                    CSELECTED_PROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID,
                    CSELECTED_UNIT_TYPE_CATEGORY_ID = loUnitTypeCategoryViewModel.loUnitTypeCategoryDetail.CUNIT_TYPE_CATEGORY_ID,
                    CSELECTED_UNIT_TYPE_CATEGORY_NAME = loUnitTypeCategoryViewModel.loUnitTypeCategoryDetail.CUNIT_TYPE_CATEGORY_NAME
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GSM02503);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #region Unit Type Category
        
        private void PropertyTypeComboBoxValueChanged(string value)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (value != null)
                {
                    var loCurrentData = loUnitTypeCategoryViewModel.loPropertyTypeList.Where(x => x.CPROPERTY_TYPE_CODE == value).FirstOrDefault();
                    if (loCurrentData != null)
                    {
                        llSingleUnit = loCurrentData.LSINGLE_UNIT;
                    }
                }
                loUnitTypeCategoryViewModel.Data.CPROPERTY_TYPE = value;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void Grid_SavingUnitTypeCategory(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loUnitTypeCategoryViewModel.UnitTypeCategoryValidation((GSM02502DetailDTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_DisplayUnitTypeCategory(R_DisplayEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = (GSM02502DetailDTO)eventArgs.Data;
                    loUnitTypeCategoryViewModel.loUnitTypeCategoryDetail = loParam;
                    loUnitTypeCategoryViewModel.SelectedActiveInactiveLACTIVE = loParam.LACTIVE;

                    loUtilityTabParameter.CSELECTED_UNIT_TYPE_CATEGORY_ID = loUnitTypeCategoryViewModel.loUnitTypeCategoryDetail.CUNIT_TYPE_CATEGORY_ID;

                    if (loParam.LACTIVE)
                    {
                        loUnitTypeCategoryLabel = _localizer["InActive"];
                        loUnitTypeCategoryViewModel.SelectedActiveInactiveLACTIVE = false;
                    }
                    else
                    {
                        loUnitTypeCategoryLabel = _localizer["Activate"];
                        loUnitTypeCategoryViewModel.SelectedActiveInactiveLACTIVE = true;
                    }

                    if (_tabStripRef.ActiveTab.Id == "Charge")
                    {
                        loChargeViewModel.loTabParameter.CPROPERTY_ID = loUnitTypeCategoryViewModel.SelectedProperty.CPROPERTY_ID;
                        loChargeViewModel.loTabParameter.CUNIT_TYPE_CATEGORY_ID = loParam.CUNIT_TYPE_CATEGORY_ID;
                        await _gridChargeRef.R_RefreshGrid(null);
                    }
                    else if (_tabStripRef.ActiveTab.Id == "Utility")
                    {
                        await loChargeViewModel.GetChargeListStreamAsync();
                        await _tabPageUtility.InvokeRefreshTabPageAsync(loUtilityTabParameter);
                    }
                    /*
                else if (_tabStripRef.ActiveTabIndex == 1)
                {
                    loChargeViewModel.SelectedProperty = loUnitTypeCategoryViewModel.SelectedProperty.CPROPERTY_ID;
                    loChargeViewModel.SelectedUnitTypeCategory = loParam.CUNIT_TYPE_CATEGORY_ID;
                    await _gridUtilityRef.R_RefreshGrid(null);
                }*/

                    llSingleUnit = loParam.LSINGLE_UNIT;


                }
                if (eventArgs.ConductorMode == R_eConductorMode.Edit)
                {
                    await _unitTypeCategoryNameRef.FocusAsync();
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_R_ServiceGetUnitTypeCategoryListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUnitTypeCategoryViewModel.GetUnitTypeCategoryListStreamAsync();
                eventArgs.ListEntityResult = loUnitTypeCategoryViewModel.loUnitTypeCategoryList;
                if (loUnitTypeCategoryViewModel.loUnitTypeCategoryList.Count() == 0)
                {
                    IsUnitTypeCategoryListExist = false;
                }
                else if (loUnitTypeCategoryViewModel.loUnitTypeCategoryList.Count() > 0)
                {
                    IsUnitTypeCategoryListExist = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetRecordUnitTypeCategory(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02502DetailDTO loParam = new GSM02502DetailDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02502DetailDTO>(eventArgs.Data);
                await loUnitTypeCategoryViewModel.GetUnitTypeCategoryAsync(loParam);

                eventArgs.Result = loUnitTypeCategoryViewModel.loUnitTypeCategoryDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSaveUnitTypeCategory(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUnitTypeCategoryViewModel.SaveUnitTypeCategoryAsync(
                    (GSM02502DetailDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loUnitTypeCategoryViewModel.loUnitTypeCategoryDetail;

                if (loUnitTypeCategoryViewModel.loUnitTypeCategoryList.Count() == 0)
                {
                    IsUnitTypeCategoryListExist = false;
                }
                else if (loUnitTypeCategoryViewModel.loUnitTypeCategoryList.Count() > 0)
                {
                    IsUnitTypeCategoryListExist = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_AfterDeleteUnitTypeCategory()
        {
            if (loUnitTypeCategoryViewModel.loUnitTypeCategoryList.Count() == 0)
            {
                IsUnitTypeCategoryListExist = false;
            }
            else if (loUnitTypeCategoryViewModel.loUnitTypeCategoryList.Count() > 0)
            {
                IsUnitTypeCategoryListExist = true;
            }
        }

        private async Task Grid_ServiceDeleteUnitTypeCategory(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                GSM02502DetailDTO loData = (GSM02502DetailDTO)eventArgs.Data;
                await loUnitTypeCategoryViewModel.DeleteUnitTypeCategoryAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private async Task Grid_ValidationUnitTypeCategory(R_ValidationEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            R_PopupResult loResult = null;
            GFF00900ParameterDTO loParam = null;
            GSM02502DetailDTO loData = null;
            try
            {
                loData = (GSM02502DetailDTO)eventArgs.Data;
                if (loData.LACTIVE == true && _conductorUnitTypeCategoryRef.R_ConductorMode == R_eConductorMode.Add)
                {
                    var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                    loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM02505";
                    await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync();

                    if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                    {
                        eventArgs.Cancel = false;
                    }
                    else
                    {
                        loParam = new GFF00900ParameterDTO()
                        {
                            Data = loValidateViewModel.loRspActivityValidityList,
                            IAPPROVAL_CODE = "GSM02505"
                        };
                        loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loParam);
                        if (loResult.Success == false || (bool)loResult.Result == false)
                        {
                            eventArgs.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task R_Before_Open_Popup_UnitTypeCategoryActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM02505"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await loUnitTypeCategoryViewModel.ActiveInactiveProcessAsync();
                    var loGetDataParam = (GSM02502DetailDTO)_conductorUnitTypeCategoryRef.R_GetCurrentData();
                    var loHeaderData = (GSM02502DTO)_gridUnitTypeCategoryRef.GetCurrentData();
                    await _conductorUnitTypeCategoryRef.R_GetEntity(loGetDataParam);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "GSM02505" //Uabh Approval Code sesuai Spec masing masing
                    };
                    eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task R_After_Open_Popup_UnitTypeCategoryActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (eventArgs.Success == false)
                {
                    return;
                }
                bool result = (bool)eventArgs.Result;
                if (result == true)
                {
                    await loUnitTypeCategoryViewModel.ActiveInactiveProcessAsync();
                    var loGetDataParam = (GSM02502DetailDTO)_conductorUnitTypeCategoryRef.R_GetCurrentData();
                    var loHeaderData = (GSM02502DTO)_gridUnitTypeCategoryRef.GetCurrentData();
                    await _conductorUnitTypeCategoryRef.R_GetEntity(loGetDataParam);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        //private async Task OnLostFocusCharge()
        //{
        //    R_Exception loEx = new R_Exception();

        //    try
        //    {
        //        GSM02502ChargeDTO loGetData = (GSM02502ChargeDTO)loChargeViewModel.Data;

        //        if (string.IsNullOrWhiteSpace(loGetData.CCHARGES_ID))
        //        {
        //            return;
        //        }

        //        LookupLML00400ViewModel loLookupViewModel = new LookupLML00400ViewModel();
        //        LML00400ParameterDTO loParam = new LML00400ParameterDTO()
        //        {
        //            CPROPERTY_ID = loChargeViewModel.loTabParameter.CPROPERTY_ID,
        //            CCHARGE_TYPE_ID = loChargeViewModel.Data.CCHARGES_TYPE,
        //            CCOMPANY_ID = _clientHelper.CompanyId,
        //            CUSER_ID = _clientHelper.UserId
        //            CSEARCH_TEXT = loChargeViewModel.Data.CCHARGES_ID
        //        };

        //        var loResult = await loLookupViewModel.GetUtitlityCharges(loParam);

        //        if (loResult == null)
        //        {
        //            loEx.Add(R_FrontUtility.R_GetError(
        //                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
        //                    "_ErrLookup01"));
        //            loGetData.CCHARGES_ID = "";
        //            //await GLAccount_TextBox.FocusAsync();
        //        }
        //        else
        //        {
        //            loGetData.CCHARGES_ID = loResult.CRATETYPE_CODE;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    R_DisplayException(loEx);
        //}

        private void Grid_R_Before_Open_ChargeId_Lookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            LML00400ParameterDTO loParam = new LML00400ParameterDTO()
            {
                CPROPERTY_ID = loChargeViewModel.loTabParameter.CPROPERTY_ID,
                CCHARGE_TYPE_ID = loChargeViewModel.Data.CCHARGES_TYPE,
                CCOMPANY_ID = _clientHelper.CompanyId,
                CUSER_ID = _clientHelper.UserId
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(Lookup_PMFRONT.LML00400);
        }

        private void Grid_R_After_Open_ChargeId_Lookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loTempResult = (LML00400DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (GSM02502ChargeDTO)eventArgs.ColumnData;
            loGetData.CCHARGES_ID = loTempResult.CCHARGES_ID;
        }
        #endregion

        #region Charge
        private async Task Grid_DisplayCharge(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02502ChargeDTO)eventArgs.Data;
                loChargeViewModel.loCharge = loParam;
            }
        }

        private async Task Grid_AfterAddCharge(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _unitTypeCategoryIdRef.FocusAsync();
                string loNewSequence = "";
                if (loChargeViewModel.loChargeList.Count() == 0)
                {
                    loNewSequence = "001";
                }
                else
                {
                    GSM02502ChargeDTO loLastData = loChargeViewModel.loChargeList.OrderBy(s => int.Parse(s.CSEQUENCE)).Last();
                    loNewSequence = (int.Parse(loLastData.CSEQUENCE) + 1).ToString("D3");
                }

                GSM02502ChargeDTO loData = (GSM02502ChargeDTO)eventArgs.Data;
                loData.CSEQUENCE = loNewSequence;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_R_ServiceGetChargeListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loChargeViewModel.GetChargeListStreamAsync();
                eventArgs.ListEntityResult = loChargeViewModel.loChargeList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetRecordCharge(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02502ChargeDTO loParam = new GSM02502ChargeDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02502ChargeDTO>(eventArgs.Data);
                await loChargeViewModel.GetChargeAsync(loParam);

                eventArgs.Result = loChargeViewModel.loCharge;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSaveCharge(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loChargeViewModel.SaveChargeAsync(
                    (GSM02502ChargeDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loChargeViewModel.loCharge;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceDeleteCharge(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM02502ChargeDTO loData = (GSM02502ChargeDTO)eventArgs.Data;
                await loChargeViewModel.DeleteChargeAsync(loData);
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
