using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02501;
using GSM02500COMMON.DTOs.GSM02510;
using GSM02500COMMON.DTOs.GSM02540;
using GSM02500COMMON.DTOs.GSM02541;
using GSM02500MODEL.View_Model;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500FRONT
{
    public partial class GSM02541 : R_Page
    {
        //Other Unit
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_PopupService PopupService { get; set; }
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private GSM02541ViewModel loOtherUnitViewModel = new GSM02541ViewModel();

        private R_Conductor _conductorOtherUnitRef;

        private R_Grid<GSM02541DTO> _gridOtherUnitRef;

        private string loOtherUnitLabel = "";

        private R_TextBox OtherUnitIdRef;

        private R_TextBox OtherUnitNameRef;

        private string poPropertyId = "";

        private string lcCUOMLabel = "";

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
                GSM02541DetailDTO loData = (GSM02541DetailDTO)eventArgs.Data;

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
                        Table_Name = "GSM_PROPERTY_OTHER_UNIT_TYPE",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loOtherUnitViewModel.SelectedProperty.CPROPERTY_ID, loData.COTHER_UNIT_ID)
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
                        Table_Name = "GSM_PROPERTY_OTHER_UNIT_TYPE",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loOtherUnitViewModel.SelectedProperty.CPROPERTY_ID, loData.COTHER_UNIT_ID)
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
                loOtherUnitLabel = _localizer["_Activate"];
                loOtherUnitViewModel.SelectedProperty.CPROPERTY_ID = (string)poParameter;

                await loOtherUnitViewModel.GetSelectedPropertyAsync();

                await loOtherUnitViewModel.GetStrataLeaseListStreamAsync();

                await loOtherUnitViewModel.GetBuildingListStreamAsync();
                await loOtherUnitViewModel.GetCUOMFromPropertyAsync();

                await loOtherUnitViewModel.GetOtherUnitTypeListStreamAsync();
                await loOtherUnitViewModel.GetUnitViewListStreamAsync();
                //await loOtherUnitViewModel.GetOtherUnitListStreamAsync();
                //await loOtherUnitViewModel.GetUnitInfoListStreamAsync();
                //await loOtherUnitViewModel.GetUnitCategoryListStreamAsync();


                lcCUOMLabel = loOtherUnitViewModel.loCUOM.CUOM;
                await _gridOtherUnitRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task OtherUnit_SetOther(R_SetEventArgs eventArgs)
        {
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
        }
        private async Task OnClickTemplate()
        {
            R_Exception loException = new R_Exception();
            var loData = new List<TemplateOtherUnitTypeDTO>();
            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["M001"], R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await loOtherUnitViewModel.DownloadTemplateOtherUnitAsync();

                    var saveFileName = $"Other Unit.xlsx";
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
            eventArgs.Parameter = new UploadOtherUnitParameterDTO()
            {
                PropertyData = loOtherUnitViewModel.SelectedProperty
            };
            eventArgs.TargetPageType = typeof(UploadOtherUnit);
        }

        private async Task After_Open_Upload_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
            if ((bool)eventArgs.Result == true)
            {
                await _gridOtherUnitRef.R_RefreshGrid(null);
            }
        }

        private void R_Before_OpenUtilities_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = new TabParameterDTO()
            {
                CSELECTED_OTHER_UNIT_ID = loOtherUnitViewModel.loOtherUnitDetail.COTHER_UNIT_ID,
                CSELECTED_PROPERTY_ID = loOtherUnitViewModel.SelectedProperty.CPROPERTY_ID
            };
            eventArgs.TargetPageType = typeof(GSM02540Utilities);
        }


        #region Other Unit

        private async Task Grid_DisplayOtherUnit(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                GSM02541DetailDTO loParam = (GSM02541DetailDTO)eventArgs.Data;

                loOtherUnitViewModel.loOtherUnitDetail = loParam;
                loOtherUnitViewModel.SelectedActiveInactiveLACTIVE = loParam.LACTIVE;
                if (loParam.LACTIVE)
                {
                    loOtherUnitLabel = _localizer["_InActive"];
                    loOtherUnitViewModel.SelectedActiveInactiveLACTIVE = false;
                }
                else
                {
                    loOtherUnitLabel = _localizer["_Activate"];
                    loOtherUnitViewModel.SelectedActiveInactiveLACTIVE = true;
                }
            }
            if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            {
                await OtherUnitNameRef.FocusAsync();
            }
        }

        private void Grid_ConvertToGridEntityOtherUnitType(R_ConvertToGridEntityEventArgs eventArgs)
        {
            GSM02541DetailDTO loData = (GSM02541DetailDTO)eventArgs.Data;
            eventArgs.GridData = R_FrontUtility.ConvertObjectToObject<GSM02541DTO>(loData);
            GSM02541DTO loGridData = (GSM02541DTO)eventArgs.GridData;
        }

        private async Task Grid_R_ServiceGetOtherUnitListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loOtherUnitViewModel.GetOtherUnitListStreamAsync();
                eventArgs.ListEntityResult = loOtherUnitViewModel.loOtherUnitList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetOtherUnitRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02541DetailDTO loParam = new GSM02541DetailDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02541DetailDTO>(eventArgs.Data);
                await loOtherUnitViewModel.GetOtherUnitAsync(loParam);

                await loOtherUnitViewModel.GetOtherUnitTypeListStreamAsync();
                await loOtherUnitViewModel.GetBuildingListStreamAsync();
                loOtherUnitViewModel.SelectedBuildingId = loOtherUnitViewModel.loOtherUnitDetail.CBUILDING_ID;
                await loOtherUnitViewModel.GetFloorListStreamAsync();

                eventArgs.Result = loOtherUnitViewModel.loOtherUnitDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private void Grid_BeforeAddOtherUnit(R_BeforeAddEventArgs eventArgs)
        {
            OtherUnitIdRef.FocusAsync();
        }

        //private void Grid_BeforeEditOtherUnit(R_BeforeEditEventArgs eventArgs)
        //{
        //    OtherUnitNameRef.FocusAsync();
        //}

        private void Grid_SavingOtherUnit(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loOtherUnitViewModel.OtherUnitValidation((GSM02541DetailDTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSaveOtherUnit(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loOtherUnitViewModel.SaveOtherUnitAsync(
                    (GSM02541DetailDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loOtherUnitViewModel.loOtherUnitDetail;
                //_gridOtherUnitRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceDeleteOtherUnit(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                GSM02541DetailDTO loData = (GSM02541DetailDTO)eventArgs.Data;
                await loOtherUnitViewModel.DeleteOtherUnitAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_AfterAddOtherUnit(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await OtherUnitIdRef.FocusAsync();
                loOtherUnitViewModel.SelectedBuildingId = "";
                await loOtherUnitViewModel.GetFloorListStreamAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ValidationOtherUnit(R_ValidationEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            R_PopupResult loResult = null;
            GFF00900ParameterDTO loParam = null;
            GSM02541DetailDTO loData = null;
            try
            {
                loData = (GSM02541DetailDTO)eventArgs.Data;
                if (loData.LACTIVE == true && _conductorOtherUnitRef.R_ConductorMode == R_eConductorMode.Add)
                {
                    var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                    loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM02506";
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
                            IAPPROVAL_CODE = "GSM02506"
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

        private async Task R_Before_Open_Popup_ActivateInactiveOtherUnit(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM02506"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {/*
                    await CenterViewModel.ActiveInactiveProcessAsync(); //Ganti jadi method ActiveInactive masing masing
                    await _gridCenterRef.R_RefreshGrid(null);*/
                    await loOtherUnitViewModel.ActiveInactiveProcessAsync();
                    var loGetDataParam = (GSM02541DetailDTO)_conductorOtherUnitRef.R_GetCurrentData();
                    var loHeaderData = (GSM02541DTO)_gridOtherUnitRef.GetCurrentData();
                    await _conductorOtherUnitRef.R_GetEntity(loGetDataParam);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "GSM02506" //Uabh Approval Code sesuai Spec masing masing
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

        private async Task R_After_Open_Popup_ActivateInactiveOtherUnit(R_AfterOpenPopupEventArgs eventArgs)
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
                    await loOtherUnitViewModel.ActiveInactiveProcessAsync();
                    var loGetDataParam = (GSM02541DetailDTO)_conductorOtherUnitRef.R_GetCurrentData();
                    var loHeaderData = (GSM02541DTO)_gridOtherUnitRef.GetCurrentData();
                    await _conductorOtherUnitRef.R_GetEntity(loGetDataParam);/*
                    await CenterViewModel.ActiveInactiveProcessAsync();
                    await _gridCenterRef.R_RefreshGrid(null);*/
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task BuildingDropdownValueChanged(string poParam)
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(poParam))
                {
                    loOtherUnitViewModel.Data.CBUILDING_ID = "";
                    loOtherUnitViewModel.SelectedBuildingId = "";
                    loOtherUnitViewModel.Data.CFLOOR_ID = "";
                    loOtherUnitViewModel.loFloorList = new List<FloorDTO>();
                    return;
                }
                loOtherUnitViewModel.SelectedBuildingId = poParam;
                await loOtherUnitViewModel.GetFloorListStreamAsync();
                loOtherUnitViewModel.Data.CBUILDING_ID = poParam;
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