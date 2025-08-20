using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM02500MODEL.View_Model;
using GSM02500COMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSCOMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02501;
using R_BlazorFrontEnd.Helpers;
using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON.DTOs.GSM02504;
using GSM02500COMMON.DTOs.GSM02503;
using GSM02500COMMON.DTOs.GSM02500;
using GFF00900COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02541;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls.Popup;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Interfaces;
using BlazorClientHelper;
using R_BlazorFrontEnd.Controls.Enums;
using R_LockingFront;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components.Forms;
using R_BlazorFrontEnd.Controls.MessageBox;

namespace GSM02500FRONT
{
    public partial class GSM02500 : R_Page
    {
        [Inject] private R_PopupService PopupService { get; set; }
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private R_TabStrip _TabStripRef;

        private bool IsFirstOpening = true;
        [Inject] IClientHelper clientHelper { get; set; }
        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Image };
        [Inject] private R_IReport _reportService { get; set; }

        private TabParameterDTO loTabParameter = new TabParameterDTO();

        //General Info

        private GSM02501ViewModel loGeneralInfoViewModel = new();

        private R_Conductor _conductorGeneralInfoRef;

        private R_Grid<GSM02501PropertyDTO> _gridGeneralInfoRef;

        private R_TextBox _propertyIdRef;

        private R_TextBox _propertyNameRef;

        private string loGeneralInfoLabel;

        private bool IsListExist = true;

        private bool _pagePropertyOnCRUDmode = false;

        private bool IsBuildingDetailEnabled = true;

        private bool IsCRUDMode = true;

        private bool IsErrorEmptyFile = false;

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
                GSM02501DetailDTO loData = (GSM02501DetailDTO)eventArgs.Data;

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
                        Table_Name = "GSM_PROPERTY",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID)
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
                        Table_Name = "GSM_PROPERTY",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID)
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
                loGeneralInfoLabel = _localizer["Activate"];
                await _gridGeneralInfoRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnChangeInputFile(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loByteFile = await R_FrontUtility.ConvertStreamToByteAsync(eventArgs.File.OpenReadStream());
                string loFile = eventArgs.File.Name;

                loGeneralInfoViewModel.Data.OIMAGE = loByteFile;
                loGeneralInfoViewModel.Data.CFILE_NAME = Path.GetFileNameWithoutExtension(loFile);
                loGeneralInfoViewModel.Data.CFILE_EXTENSION = Path.GetExtension(loFile);

                //if (eventArgs.File.Name.Length > 0)
                //{
                //    IsFileExist = true;
                //}
                //else
                //{
                //    IsFileExist = false;
                //}
            }
            catch (Exception ex)
            {
                if (IsErrorEmptyFile)
                {
                    await R_MessageBox.Show("", "File is Empty", R_eMessageBoxButtonType.OK);
                }
                else
                {
                    loEx.Add(ex);
                }
            }
        B:
            loEx.ThrowExceptionIfErrors();
        }

        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            if (_conductorGeneralInfoRef.R_ConductorMode == R_eConductorMode.Add || _conductorGeneralInfoRef.R_ConductorMode == R_eConductorMode.Edit)
            {
                eventArgs.Cancel = true;
                return;
            }
            eventArgs.Cancel = _pagePropertyOnCRUDmode;
        }

        private void R_TabEventCallback(object poValue)
        {
            var loEx = new R_Exception();

            try
            {
                _pagePropertyOnCRUDmode = !(bool)poValue;            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

            //_pageSupplierOnCRUDmode = !(bool)poValue;
        }

        private void Before_Open_UnitType_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = loTabParameter.CSELECTED_PROPERTY_ID;
            eventArgs.TargetPageType = typeof(GSM02502);
        }

        private void Before_Open_UnitView_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = loTabParameter.CSELECTED_PROPERTY_ID;
            eventArgs.TargetPageType = typeof(GSM02504);
        }

        #region GeneralInfo

        private async Task Grid_DisplayGeneralInfo(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02501DetailDTO)eventArgs.Data;
                loGeneralInfoViewModel.loPropertyDetail = loParam;/*
                loGeneralInfoViewModel.loPropertyDetail.CPROPERTY_NAME = loParam.CPROPERTY_NAME;*/
                loGeneralInfoViewModel.SelectedActiveInactiveLACTIVE = loParam.LACTIVE;
                if (loParam.LACTIVE)
                {
                    loGeneralInfoLabel = _localizer["InActive"];
                    loGeneralInfoViewModel.SelectedActiveInactiveLACTIVE = false;
                }
                else
                {
                    loGeneralInfoLabel = _localizer["Activate"];
                    loGeneralInfoViewModel.SelectedActiveInactiveLACTIVE = true;
                }
                loTabParameter.CSELECTED_PROPERTY_ID = loGeneralInfoViewModel.loPropertyDetail.CPROPERTY_ID;
            }
            if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            {
                await _propertyNameRef.FocusAsync();
            }
        }

        private void Grid_SetOtherGeneralInfo(R_SetEventArgs eventArgs)
        {
            IsCRUDMode = eventArgs.Enable;
        }

        private async Task Grid_R_ServiceGetGeneralInfoListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loGeneralInfoViewModel.GetPropertyListStreamAsync();
                eventArgs.ListEntityResult = loGeneralInfoViewModel.loPropertyList;
                if (loGeneralInfoViewModel.loPropertyList.Count() == 0)
                {
                    IsListExist = false;
                    IsBuildingDetailEnabled = false;
                }
                else if (loGeneralInfoViewModel.loPropertyList.Count > 0)
                {
                    IsListExist = true;
                    IsBuildingDetailEnabled = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetGeneralInfoRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02501DetailDTO loParam = new GSM02501DetailDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02501DetailDTO>(eventArgs.Data);
                await loGeneralInfoViewModel.GetPropertyAsync(loParam);

                eventArgs.Result = loGeneralInfoViewModel.loPropertyDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private void Grid_SavingGeneralInfo(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loGeneralInfoViewModel.GeneralInfoValidation((GSM02501DetailDTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSaveGeneralInfo(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loGeneralInfoViewModel.SavePropertyAsync(
                    (GSM02501DetailDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                if (loGeneralInfoViewModel.loPropertyList.Count() == 0)
                {
                    IsListExist = false;
                    IsBuildingDetailEnabled = false;
                }
                else if (loGeneralInfoViewModel.loPropertyList.Count > 0)
                {
                    IsListExist = true;
                    IsBuildingDetailEnabled = true;
                }

                eventArgs.Result = loGeneralInfoViewModel.loPropertyDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_AfterDeleteGeneralInfo()
        {
            if (loGeneralInfoViewModel.loPropertyList.Count() == 0)
            {
                IsListExist = false;
                IsBuildingDetailEnabled = false;
            }
            else if (loGeneralInfoViewModel.loPropertyList.Count > 0)
            {
                IsListExist = true;
                IsBuildingDetailEnabled = true;
            }
        }

        private async Task Grid_ServiceDeleteGeneralInfo(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM02501DetailDTO loData = (GSM02501DetailDTO)eventArgs.Data;
                await loGeneralInfoViewModel.DeletePropertyAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ValidationGeneralInfo(R_ValidationEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            R_PopupResult loResult = null;
            GFF00900ParameterDTO loParam = null;
            GSM02501DetailDTO loData = null;
            try
            {
                loData = (GSM02501DetailDTO)eventArgs.Data;
                if (loData.LACTIVE == true && _conductorGeneralInfoRef.R_ConductorMode == R_eConductorMode.Add)
                {
                    var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                    loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM02501";
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
                            IAPPROVAL_CODE = "GSM02501"
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

        private async Task R_Before_Open_Popup_ActivateInactiveGeneralInfo(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM02501"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await loGeneralInfoViewModel.ActiveInactiveProcessAsync();
                    var loGetDataParam = (GSM02501DetailDTO)_conductorGeneralInfoRef.R_GetCurrentData();
                    var loHeaderData = (GSM02501PropertyDTO)_gridGeneralInfoRef.GetCurrentData();
                    await _conductorGeneralInfoRef.R_GetEntity(loGetDataParam);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "GSM02501" //Uabh Approval Code sesuai Spec masing masing
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

        private async Task R_After_Open_Popup_ActivateInactiveGeneralInfo(R_AfterOpenPopupEventArgs eventArgs)
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
                    await loGeneralInfoViewModel.ActiveInactiveProcessAsync();
                    var loGetDataParam = (GSM02501DetailDTO)_conductorGeneralInfoRef.R_GetCurrentData();
                    var loHeaderData = (GSM02501PropertyDTO)_gridGeneralInfoRef.GetCurrentData();
                    await _conductorGeneralInfoRef.R_GetEntity(loGetDataParam);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #region OnLostFocus

        private async Task OnLostFocusSalesTax()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                GSM02501DetailDTO loGetData = (GSM02501DetailDTO)loGeneralInfoViewModel.Data;

                if (string.IsNullOrWhiteSpace(loGeneralInfoViewModel.Data.CSALES_TAX_ID))
                {
                    loGetData.CSALES_TAX_NAME = "";
                    return;
                }

                LookupGSL00100ViewModel loLookupViewModel = new LookupGSL00100ViewModel();
                GSL00100ParameterDTO loParam = new GSL00100ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CSEARCH_TEXT = loGeneralInfoViewModel.Data.CSALES_TAX_ID
                };

                var loResult = await loLookupViewModel.GetSalesTax(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CSALES_TAX_ID = "";
                    loGetData.CSALES_TAX_NAME = "";
                    loGetData.NTAX_PERCENTAGE = 0;
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CSALES_TAX_ID = loResult.CTAX_ID;
                    loGetData.CSALES_TAX_NAME = loResult.CTAX_NAME;
                    loGetData.NTAX_PERCENTAGE = loResult.NTAX_PERCENTAGE;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task OnLostFocusCurrency()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                GSM02501DetailDTO loGetData = (GSM02501DetailDTO)loGeneralInfoViewModel.Data;

                if (string.IsNullOrWhiteSpace(loGeneralInfoViewModel.Data.CCURRENCY))
                {
                    loGetData.CCURRENCY_NAME = "";
                    return;
                }

                LookupGSL00300ViewModel loLookupViewModel = new LookupGSL00300ViewModel();
                GSL00300ParameterDTO loParam = new GSL00300ParameterDTO()
                {
                    CSEARCH_TEXT = loGeneralInfoViewModel.Data.CCURRENCY
                };

                var loResult = await loLookupViewModel.GetCurrency(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CSALES_TAX_ID = "";
                    loGetData.CSALES_TAX_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CCURRENCY = loResult.CCURRENCY_CODE;
                    loGetData.CCURRENCY_NAME = loResult.CCURRENCY_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        private void R_After_Open_LookupSalesTaxId(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00100DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (GSM02501DetailDTO)_conductorGeneralInfoRef.R_GetCurrentData();
            loGetData.CSALES_TAX_ID = loTempResult.CTAX_ID;
            loGetData.CSALES_TAX_NAME = loTempResult.CTAX_NAME;
            loGetData.NTAX_PERCENTAGE = loTempResult.NTAX_PERCENTAGE;
        }

        private void R_Before_Open_LookupSalesTaxId(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00100ParameterDTO()
            {
                CCOMPANY_ID = "",
                CUSER_ID = ""
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00100);
        }

        private void R_After_Open_LookupCurrency(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00300DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (GSM02501DetailDTO)_conductorGeneralInfoRef.R_GetCurrentData();
            loGetData.CCURRENCY = loTempResult.CCURRENCY_CODE;
            loGetData.CCURRENCY_NAME = loTempResult.CCURRENCY_NAME;
        }

        private void R_Before_Open_LookupCurrency(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GSL00300);
        }

        private void R_Before_OpenBuilding_Detail(R_BeforeOpenDetailEventArgs eventArgs)
        {
            eventArgs.Parameter = loGeneralInfoViewModel.loPropertyDetail.CPROPERTY_ID;
            if (loGeneralInfoViewModel.loPropertyList.Count() > 0)
            {
                eventArgs.TargetPageType = typeof(GSM02510);
            }
        }

        private void R_After_OpenBuilding_Detail()
        {

        }

        private void PreDockOtherUnit_InstantiateDock(R_InstantiateDockEventArgs eventArgs)
        {
            eventArgs.Parameter = loGeneralInfoViewModel.loPropertyDetail.CPROPERTY_ID;
            if (_conductorGeneralInfoRef.R_ConductorMode == R_eConductorMode.Normal && _pagePropertyOnCRUDmode == false)
            {
                eventArgs.TargetPageType = typeof(GSM02540);
            }
        }

        private void R_AfterOpenOtherUnitPredefinedDock(R_AfterOpenPredefinedDockEventArgs eventArgs)
        {

        }

        private void PreDockUsers_InstantiateDock(R_InstantiateDockEventArgs eventArgs)
        {
            eventArgs.Parameter = loGeneralInfoViewModel.loPropertyDetail.CPROPERTY_ID;
            if (_conductorGeneralInfoRef.R_ConductorMode == R_eConductorMode.Normal && _pagePropertyOnCRUDmode == false)
            {
                eventArgs.TargetPageType = typeof(GSM02550);
            }
        }

        private void R_AfterOpenUsersPredefinedDock(R_AfterOpenPredefinedDockEventArgs eventArgs)
        {

        }

        private void PreDockDepartment_InstantiateDock(R_InstantiateDockEventArgs eventArgs)
        {
            eventArgs.Parameter = loGeneralInfoViewModel.loPropertyDetail.CPROPERTY_ID;
            if (_conductorGeneralInfoRef.R_ConductorMode == R_eConductorMode.Normal && _pagePropertyOnCRUDmode == false)
            {
                eventArgs.TargetPageType = typeof(GSM02560);
            }
        }

        private void R_AfterOpenDepartmentPredefinedDock(R_AfterOpenPredefinedDockEventArgs eventArgs)
        {

        }

        private void PreDockGallery_InstantiateDock(R_InstantiateDockEventArgs eventArgs)
        {
            eventArgs.Parameter = new ImageTabParameterDTO()
            {
                CSELECTED_PROPERTY_ID = loGeneralInfoViewModel.loPropertyDetail.CPROPERTY_ID,
                CSELECTED_PROPERTY_NAME = loGeneralInfoViewModel.loPropertyDetail.CPROPERTY_NAME
            };

            //loGeneralInfoViewModel.loPropertyDetail.CPROPERTY_ID;
            if (_conductorGeneralInfoRef.R_ConductorMode == R_eConductorMode.Normal && _pagePropertyOnCRUDmode == false)
            {
                eventArgs.TargetPageType = typeof(GSM02570);
            }
        }

        private void R_AfterOpenGalleryPredefinedDock(R_AfterOpenPredefinedDockEventArgs eventArgs)
        {

        }

        private void PreDockOperationalHour_InstantiateDock(R_InstantiateDockEventArgs eventArgs)
        {
            eventArgs.Parameter = loGeneralInfoViewModel.loPropertyDetail.CPROPERTY_ID;
            if (_conductorGeneralInfoRef.R_ConductorMode == R_eConductorMode.Normal && _pagePropertyOnCRUDmode == false)
            {
                eventArgs.TargetPageType = typeof(GSM02580);
            }
        }

        private void R_AfterOpenOperationalHourPredefinedDock(R_AfterOpenPredefinedDockEventArgs eventArgs)
        {

        }

        #endregion

        private async Task Grid_AfterAddGeneralInfo(R_AfterAddEventArgs eventArgs)
        {
            GSM02501DetailDTO loAddField = (GSM02501DetailDTO)eventArgs.Data;

            loAddField.DUPDATE_DATE = DateTime.Now;
            loAddField.DCREATE_DATE = DateTime.Now;

            IsBuildingDetailEnabled = false;

            await _propertyIdRef.FocusAsync();
        }

        private void Grid_BeforeCancelGeneralInfo(R_BeforeCancelEventArgs eventArgs)
        {
            if (loGeneralInfoViewModel.loPropertyList.Count() > 0)
            {
                IsBuildingDetailEnabled = true;
            }
        }
        
        private void Grid_SetEditGeneralInfo(R_SetEventArgs eventArgs)
        {
            if (eventArgs.Enable)
            {
                IsBuildingDetailEnabled = false;
            }
        }

        #region Button Process
        private async Task OnClickPrintBtn()
        {
            R_Exception loEx = new R_Exception();
            try
            {

                var loParam = R_FrontUtility.ConvertObjectToObject<GSM02500PrintParamDTO>(_conductorGeneralInfoRef.R_GetCurrentData());
                loParam.CCOMPANY_ID = clientHelper.CompanyId;
                loParam.CREPORT_CULTURE = clientHelper.ReportCulture;
                loParam.CUSER_LOGIN_ID = clientHelper.UserId;

                var loValidate = await R_MessageBox.Show("", _localizer["_NotifPrint"], R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _reportService.GetReport(
                    "R_DefaultServiceUrl",
                    "GS",
                    "rpt/GSM02500Print/PropertyProfilePost",
                    "rpt/GSM02500Print/AllStreamPropertyProfileGet",
                    loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            await R_DisplayExceptionAsync(loEx);
        }
        #endregion
        //private async Task Grid_BeforeEditGeneralInfo(R_BeforeEditEventArgs eventArgs)
        //{
        //    IsBuildingDetailEnabled = false;
        //    await _propertyNameRef.FocusAsync();
        //}
    }
}
