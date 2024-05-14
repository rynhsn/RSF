using GSM02500COMMON.DTOs.GSM02510;
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
using GSM02500COMMON.DTOs.GSM02520;
using System.Diagnostics.Tracing;
using GSM02500COMMON.DTOs;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.MessageBox;
using Microsoft.JSInterop;
using GFF00900COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02541;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Interfaces;
using BlazorClientHelper;
using R_BlazorFrontEnd.Controls.Enums;
using R_LockingFront;

namespace GSM02500FRONT
{
    public partial class GSM02520 : R_Page
    {
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_PopupService PopupService { get; set; }
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private GSM02520ViewModel loViewModel = new GSM02520ViewModel();

        private R_Conductor _conductorRef;

        private R_Grid<GSM02520DTO> _gridRef;

        private R_TextBox _floordRef;

        private R_TextBox _floorNameRef;

        private string loLabel = "";

        private string lcCUOMLabel;

        private TabParameterDTO loTabParameter = new TabParameterDTO();

        private bool IsFloorListExist = false;

        //private int lnDecimal = 6;

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
                GSM02520DetailDTO loData = (GSM02520DetailDTO)eventArgs.Data;

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
                        Table_Name = "GSM_PROPERTY_FLOOR",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loTabParameter.CSELECTED_PROPERTY_ID, loTabParameter.CSELECTED_BUILDING_ID, loData.CFLOOR_ID)
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
                        Table_Name = "GSM_PROPERTY_FLOOR",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loTabParameter.CSELECTED_PROPERTY_ID, loTabParameter.CSELECTED_BUILDING_ID, loData.CFLOOR_ID)
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
                loLabel = _localizer["_Activate"];
                loTabParameter = (TabParameterDTO)poParameter;


                loViewModel.SelectedProperty.CPROPERTY_ID = loTabParameter.CSELECTED_PROPERTY_ID;
                loViewModel.SelectedBuilding.CBUILDING_ID = loTabParameter.CSELECTED_BUILDING_ID;


                await loViewModel.GetSelectedPropertyAsync();
                await loViewModel.GetSelectedBuildingAsync();
                await loViewModel.GetUnitCategoryListStreamAsync();
                await loViewModel.GetUnitTypeListStreamAsync();
                await loViewModel.GetCUOMFromPropertyAsync();

                lcCUOMLabel = loViewModel.loCUOM.CUOM;

                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //private async Task TaxPercentage_ValueChanged(decimal poParam)
        //{
        //    R_Exception loEx = new R_Exception();

        //    try
        //    {
        //        loViewModel.Data.NGROSS_AREA_SIZE = poParam;
        //        lnDecimal = CountTheDecimal(loViewModel.Data.NGROSS_AREA_SIZE);
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }
        //    loEx.ThrowExceptionIfErrors();
        //}

        //private int CountTheDecimal(decimal poParam)
        //{
        //    R_Exception loEx = new R_Exception();
        //    int decimalPlaces = 0;
        //    int decimalPointIndex = 0;
        //    string numberString;
        //    try
        //    {
        //        numberString = poParam.ToString();
        //        decimalPointIndex = numberString.IndexOf('.');

        //        if (decimalPointIndex == -1)
        //        {
        //            return 0; // No decimal point found
        //        }

        //        decimalPlaces = numberString.Length - decimalPointIndex - 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }
        //    loEx.ThrowExceptionIfErrors();
        //    return decimalPlaces;
        //}

        private async Task OnClickTemplate()
        {
            R_Exception loException = new R_Exception();
            var loData = new List<TemplateFloorDTO>();
            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["M001"], R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await loViewModel.DownloadTemplateFloorAsync();

                    var saveFileName = $"Floor.xlsx";
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
            eventArgs.Parameter = new UploadFloorParameterDTO()
            {
                PropertyData = loViewModel.SelectedProperty,
                BuildingData = loViewModel.SelectedBuilding
            };
            eventArgs.TargetPageType = typeof(UploadFloor);
        }

        private async Task After_Open_Upload_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
            if ((bool)eventArgs.Result == true)
            {
                await _gridRef.R_RefreshGrid(null);
            }
        }

        private async Task Grid_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02520DetailDTO)eventArgs.Data;
                //lnDecimal = CountTheDecimal(loParam.NGROSS_AREA_SIZE);
                loViewModel.loFloorDetail = loParam;
                loViewModel.SelectedActiveInactiveLACTIVE = loParam.LACTIVE;
                if (loParam.LACTIVE)
                {
                    loLabel = _localizer["_InActive"];
                    loViewModel.SelectedActiveInactiveLACTIVE = false;
                }
                else
                {
                    loLabel = _localizer["_Activate"];
                    loViewModel.SelectedActiveInactiveLACTIVE = true;
                }
            }
            if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            {
                await _floorNameRef.FocusAsync();
            }
        }

        private async Task Grid_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            await _floordRef.FocusAsync();
            GSM02520DetailDTO loAddField = (GSM02520DetailDTO)eventArgs.Data;
            loAddField.DCREATE_DATE = DateTime.Now;
            loAddField.DUPDATE_DATE = DateTime.Now;
            if (loViewModel.loUnitCategoryList.Count() > 0)
            {
                loAddField.CDEFAULT_UNIT_CATEGORY_ID = loViewModel.loUnitCategoryList.FirstOrDefault().CCODE;
            }
            IsFloorListExist = false;
        }

        private void Grid_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            if (loViewModel.loFloorList.Count() > 0)
            {
                IsFloorListExist = true;
            }
        }

        private async Task Grid_SetEdit(R_SetEventArgs eventArgs)
        {
            if (eventArgs.Enable)
            {
                IsFloorListExist = false;
            }
        }
        //private async Task Grid_BeforeEdit(R_BeforeEditEventArgs eventArgs)
        //{
        //    IsFloorListExist = false;
        //    //await _propertyNameRef.FocusAsync();
        //}

        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetFloowListStreamAsync();

                if (loViewModel.loFloorList.Count() == 0)
                {
                    IsFloorListExist = false;
                }
                else if (loViewModel.loFloorList.Count() > 0)
                {
                    IsFloorListExist = true;
                }

                eventArgs.ListEntityResult = loViewModel.loFloorList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02520DetailDTO loParam = new GSM02520DetailDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02520DetailDTO>(eventArgs.Data);
                await loViewModel.GetFloorAsync(loParam);

                eventArgs.Result = loViewModel.loFloorDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private void Grid_SavingFloor(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loViewModel.FloorValidation((GSM02520DetailDTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.SaveFloorAsync(
                    (GSM02520DetailDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                if (loViewModel.loFloorList.Count() == 0)
                {
                    IsFloorListExist = false;
                }
                else if (loViewModel.loFloorList.Count() > 0)
                {
                    IsFloorListExist = true;
                }

                eventArgs.Result = loViewModel.loFloorDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void Grid_AfterDelete()
        {
            if (loViewModel.loFloorList.Count() == 0)
            {
                IsFloorListExist = false;
            }
            else if (loViewModel.loFloorList.Count() > 0)
            {
                IsFloorListExist = true;
            }
        }

        private async Task Grid_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                GSM02520DetailDTO loData = (GSM02520DetailDTO)eventArgs.Data;
                await loViewModel.DeleteFloorAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ValidationFloor(R_ValidationEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            R_PopupResult loResult = null;
            GFF00900ParameterDTO loParam = null;
            GSM02520DetailDTO loData = null;
            try
            {
                loData = (GSM02520DetailDTO)eventArgs.Data;
                if (loData.LACTIVE == true && _conductorRef.R_ConductorMode == R_eConductorMode.Add)
                {
                    var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                    loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM02503";
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
                            IAPPROVAL_CODE = "GSM02503"
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

        private async Task R_Before_Open_Popup_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM02503"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await loViewModel.ActiveInactiveProcessAsync();
                    var loGetDataParam = (GSM02520DetailDTO)_conductorRef.R_GetCurrentData();
                    var loHeaderData = (GSM02520DTO)_gridRef.GetCurrentData();
                    await _conductorRef.R_GetEntity(loGetDataParam);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "GSM02503" //Uabh Approval Code sesuai Spec masing masing
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

        private async Task R_After_Open_Popup_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
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
                    await loViewModel.ActiveInactiveProcessAsync();
                    var loGetDataParam = (GSM02520DetailDTO)_conductorRef.R_GetCurrentData();
                    var loHeaderData = (GSM02520DTO)_gridRef.GetCurrentData();
                    await _conductorRef.R_GetEntity(loGetDataParam);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private void R_Before_OpenUnit_Detail(R_BeforeOpenDetailEventArgs eventArgs)
        {
            loTabParameter.CSELECTED_FLOOR_ID = loViewModel.loFloorDetail.CFLOOR_ID;
            eventArgs.Parameter = loTabParameter;
            if (loViewModel.loFloorList.Count() > 0)
            {
                eventArgs.TargetPageType = typeof(GSM02530);
            }
        }

        private void R_After_OpenUnit_Detail()
        {

        }
    }
}
