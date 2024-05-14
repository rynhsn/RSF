using GSM02500COMMON.DTOs.GSM02520;
using GSM02500MODEL.View_Model;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs.GSM02530;
using System.Reflection.Emit;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02510;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls.MessageBox;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using GSM02500COMMON.DTOs.GSM02531;
using GSM02500COMMON.DTOs.GSM02500;
using GFF00900COMMON.DTOs;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Interfaces;
using BlazorClientHelper;
using R_BlazorFrontEnd.Controls.Enums;
using R_LockingFront;

namespace GSM02500FRONT
{
    public partial class GSM02530 : R_Page
    {
        //Unit Info
        [Inject] IJSRuntime JS { get; set; }
        [Inject] R_PopupService PopupService { get; set; }
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private GSM02530ViewModel loUnitInfoViewModel = new GSM02530ViewModel();

        private R_Conductor _conductorUnitInfoRef;

        private R_Grid<GSM02530DTO> _gridUnitInfoRef;

        private string loUnitInfoLabel = "";

        private string lcCUOMLabel;

        private R_TextBox _unitInfoIdRef;

        private R_TextBox _unitInfoNameRef;

        private bool IsUnitInfoListExit = false;

        private bool IsActiveInactiveEnable = true;

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
                GSM02530DetailDTO loData = (GSM02530DetailDTO)eventArgs.Data;

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
                        Table_Name = "GSM_PROPERTY_UNIT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loUnitInfoViewModel.loTabParameter.CSELECTED_PROPERTY_ID, loUnitInfoViewModel.loTabParameter.CSELECTED_BUILDING_ID, loUnitInfoViewModel.loTabParameter.CSELECTED_FLOOR_ID, loData.CUNIT_ID)
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
                        Table_Name = "GSM_PROPERTY_UNIT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loUnitInfoViewModel.loTabParameter.CSELECTED_PROPERTY_ID, loUnitInfoViewModel.loTabParameter.CSELECTED_BUILDING_ID, loUnitInfoViewModel.loTabParameter.CSELECTED_FLOOR_ID, loData.CUNIT_ID)
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
                loUnitInfoLabel = _localizer["_Activate"];
                loUnitInfoViewModel.loTabParameter = (TabParameterDTO)poParameter;

                await loUnitInfoViewModel.GetSelectedPropertyAsync();
                await loUnitInfoViewModel.GetSelectedBuildingAsync();
                await loUnitInfoViewModel.GetSelectedFloorAsync();
                await loUnitInfoViewModel.GetCUOMFromPropertyAsync();

                await loUnitInfoViewModel.GetUnitTypeListStreamAsync();
                await loUnitInfoViewModel.GetUnitViewListStreamAsync();
                await loUnitInfoViewModel.GetUnitInfoListStreamAsync();
                await loUnitInfoViewModel.GetUnitCategoryListStreamAsync();


                lcCUOMLabel = loUnitInfoViewModel.loCUOM.CUOM;

                await _gridUnitInfoRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        /*
                private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
                {
                    if (eventArgs.TabStripTab.Id == "Utilities" && loUnitInfoViewModel.loUnitInfoList.Count() == 0)
                    {
                        eventArgs.Cancel = true;
                    }
                }*/

        private async Task OnClickTemplate()
        {
            R_Exception loException = new R_Exception();
            var loData = new List<TemplateUnitDTO>();
            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["M001"], R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await loUnitInfoViewModel.DownloadTemplateUnitAsync();

                    var saveFileName = $"Unit.xlsx";
                    
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
            eventArgs.Parameter = new UploadUnitParameterDTO()
            {
                PropertyData = loUnitInfoViewModel.SelectedProperty,
                BuildingData = loUnitInfoViewModel.SelectedBuilding,
                FloorData = loUnitInfoViewModel.SelectedFloor
            };
            eventArgs.TargetPageType = typeof(UploadUnit);
        }

        private async Task After_Open_Upload_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
            if ((bool)eventArgs.Result == true)
            {
                await _gridUnitInfoRef.R_RefreshGrid(null);
            }
        }


        private void R_Before_Open_UtilitiesTabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            UploadUnitUtilityParameterDTO loUtilityParameter = new UploadUnitUtilityParameterDTO()
            {
                PropertyData = loUnitInfoViewModel.SelectedProperty,
                BuildingData = loUnitInfoViewModel.SelectedBuilding,
                FloorData = loUnitInfoViewModel.SelectedFloor,
                UnitData = new SelectedUnitDTO() { CUNIT_ID = loUnitInfoViewModel.loUnitInfoDetail.CUNIT_ID, CUNIT_NAME = loUnitInfoViewModel.loUnitInfoDetail.CUNIT_NAME}
            };

            eventArgs.Parameter = loUtilityParameter;
            eventArgs.TargetPageType = typeof(GSM02531);
        }

        #region Unit Info
        private async Task Grid_DisplayUnitInfo(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02530DetailDTO)eventArgs.Data;
                loUnitInfoViewModel.loUnitInfoDetail = loParam;
                loUnitInfoViewModel.SelectedActiveInactiveLACTIVE = loParam.LACTIVE;
                if (loParam.LACTIVE)
                {
                    loUnitInfoLabel = _localizer["_InActive"];
                    loUnitInfoViewModel.SelectedActiveInactiveLACTIVE = false;
                }
                else
                {
                    loUnitInfoLabel = _localizer["_Activate"];
                    loUnitInfoViewModel.SelectedActiveInactiveLACTIVE = true;
                }

                if (loUnitInfoViewModel.loUnitInfoList.Count() == 0)
                {
                    IsUnitInfoListExit = false;
                }
                else if (loUnitInfoViewModel.loUnitInfoList.Count() > 0)
                {
                    IsUnitInfoListExit = true;
                }
            }
            if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            {
                await _unitInfoNameRef.FocusAsync();
            }
        }

        private async Task Grid_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            await _unitInfoIdRef.FocusAsync();
            GSM02530DetailDTO loAddField = (GSM02530DetailDTO)eventArgs.Data;
            loAddField.DCREATE_DATE = DateTime.Now;
            loAddField.DUPDATE_DATE = DateTime.Now;

            if (loUnitInfoViewModel.loUnitCategoryList.Count() > 0)
            {
                loAddField.CUNIT_CATEGORY_ID = loUnitInfoViewModel.loUnitCategoryList.FirstOrDefault().CCODE;
            }
            IsActiveInactiveEnable = false;
        }

        private void Grid_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            if (loUnitInfoViewModel.loUnitInfoList.Count() > 0)
            {
                IsActiveInactiveEnable = true;
            }
        }
        private async Task Grid_SetEdit(R_SetEventArgs eventArgs)
        {
            if (eventArgs.Enable)
            {
                IsActiveInactiveEnable = false;
            }
        }

        //private async Task Grid_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        //{
        //    IsActiveInactiveEnable = false;
        //    //await _propertyNameRef.FocusAsync();
        //}

        private async Task Grid_R_ServiceGetUnitInfoListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUnitInfoViewModel.GetUnitInfoListStreamAsync();

                if (loUnitInfoViewModel.loUnitInfoList.Count() == 0)
                {
                    IsUnitInfoListExit = false;
                    IsActiveInactiveEnable = false;
                }
                else if (loUnitInfoViewModel.loUnitInfoList.Count() > 0)
                {
                    IsUnitInfoListExit = true;
                    IsActiveInactiveEnable = true;
                }

                eventArgs.ListEntityResult = loUnitInfoViewModel.loUnitInfoList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetUnitInfoRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02530DetailDTO loParam = new GSM02530DetailDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02530DetailDTO>(eventArgs.Data);
                await loUnitInfoViewModel.GetUnitInfoAsync(loParam);

                eventArgs.Result = loUnitInfoViewModel.loUnitInfoDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }


        private async Task Grid_ServiceSaveUnitInfo(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUnitInfoViewModel.SaveUnitInfoAsync(
                    (GSM02530DetailDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                if (loUnitInfoViewModel.loUnitInfoList.Count() == 0)
                {
                    IsUnitInfoListExit = false;
                    IsActiveInactiveEnable = false;
                }
                else if (loUnitInfoViewModel.loUnitInfoList.Count() > 0)
                {
                    IsUnitInfoListExit = true;
                    IsActiveInactiveEnable = true;
                }

                eventArgs.Result = loUnitInfoViewModel.loUnitInfoDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_SavingUnitInfo(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loUnitInfoViewModel.UnitInfoValidation((GSM02530DetailDTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void Grid_AfterDelete()
        {
            if (loUnitInfoViewModel.loUnitInfoList.Count() == 0)
            {
                IsUnitInfoListExit = false;
                IsActiveInactiveEnable = false;
            }
            else if (loUnitInfoViewModel.loUnitInfoList.Count() > 0)
            {
                IsUnitInfoListExit = true;
                IsActiveInactiveEnable = true;
            }
        }

        private async Task Grid_ServiceDeleteUnitInfo(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                GSM02530DetailDTO loData = (GSM02530DetailDTO)eventArgs.Data;
                await loUnitInfoViewModel.DeleteUnitInfoAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private async Task Grid_ValidationUnitInfo(R_ValidationEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            R_PopupResult loResult = null;
            GFF00900ParameterDTO loParam = null;
            GSM02530DetailDTO loData = null;
            try
            {
                loData = (GSM02530DetailDTO)eventArgs.Data;
                if (loData.LACTIVE == true && _conductorUnitInfoRef.R_ConductorMode == R_eConductorMode.Add)
                {
                    var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                    loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM02504";
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
                            IAPPROVAL_CODE = "GSM02504"
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

        private async Task R_Before_Open_Popup_ActivateInactiveUnitInfo(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM02504"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await loUnitInfoViewModel.ActiveInactiveProcessAsync(); //Ganti jadi method ActiveInactive masing masing
                    await _gridUnitInfoRef.R_RefreshGrid(null);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "GSM02504" //Uabh Approval Code sesuai Spec masing masing
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

        private async Task R_After_Open_Popup_ActivateInactiveUnitInfo(R_AfterOpenPopupEventArgs eventArgs)
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
                    await loUnitInfoViewModel.ActiveInactiveProcessAsync();
                    await _gridUnitInfoRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        #endregion

    }
}