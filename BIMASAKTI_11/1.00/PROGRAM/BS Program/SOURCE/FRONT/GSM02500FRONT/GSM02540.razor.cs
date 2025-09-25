using GSM02500COMMON.DTOs.GSM02540;
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
using GSM02500COMMON.DTOs.GSM02541;
using GSM02500COMMON.DTOs.GSM02510;
using R_BlazorFrontEnd;
using GSM02500COMMON.DTOs.GSM02503;
using R_BlazorFrontEnd.Controls.MessageBox;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls.Tab;
using GFF00900COMMON.DTOs;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Interfaces;
using BlazorClientHelper;
using GSM02500COMMON.DTOs.GSM02530;
using R_BlazorFrontEnd.Controls.Enums;
using R_LockingFront;
using GSM02500COMMON.DTOs.GSM02531;
using Lookup_GSModel.ViewModel;
using Lookup_GSCOMMON.DTOs;

namespace GSM02500FRONT
{
    public partial class GSM02540 : R_Page
    {
        //Other Unit Type
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_PopupService PopupService { get; set; }
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private GSM02540ViewModel loOtherUnitTypeViewModel = new GSM02540ViewModel();

        private R_Conductor _conductorOtherUnitTypeRef;

        private R_Grid<GSM02540DTO> _gridOtherUnitTypeRef;

        private string loOtherUnitTypeLabel = "";

        private string lcCUOMLabel;

        private R_TextBox OtherUnitTypeIdRef;

        private R_TextBox OtherUnitTypeNameRef;

        private bool IsOtherUnitTypeListExist = false;

        private bool _pagePropertyOnCRUDmode = false;

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
                GSM02540DetailDTO loData = (GSM02540DetailDTO)eventArgs.Data;

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
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loOtherUnitTypeViewModel.SelectedProperty.CPROPERTY_ID, loData.COTHER_UNIT_TYPE_ID)
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
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loOtherUnitTypeViewModel.SelectedProperty.CPROPERTY_ID, loData.COTHER_UNIT_TYPE_ID)
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
                loOtherUnitTypeLabel = _localizer["_Activate"];
                loOtherUnitTypeViewModel.SelectedProperty.CPROPERTY_ID = (string)poParameter;

                await loOtherUnitTypeViewModel.GetSelectedPropertyAsync();
                await loOtherUnitTypeViewModel.GetPropertyTypeListStreamAsync();
                await loOtherUnitTypeViewModel.GetCUOMFromPropertyAsync();

                await loOtherUnitTypeViewModel.GetOtherUnitTypeListStreamAsync();

                lcCUOMLabel = loOtherUnitTypeViewModel.loCUOM.CUOM;

                await _gridOtherUnitTypeRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            if (_conductorOtherUnitTypeRef.R_ConductorMode == R_eConductorMode.Add || _conductorOtherUnitTypeRef.R_ConductorMode == R_eConductorMode.Edit)
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
                _pagePropertyOnCRUDmode = !(bool)poValue;
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
            var loData = new List<TemplateOtherUnitTypeDTO>();
            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["M001"], R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await loOtherUnitTypeViewModel.DownloadTemplateOtherUnitTypeAsync();

                    var saveFileName = $"Other Unit Type.xlsx";
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
            eventArgs.Parameter = new UploadOtherUnitTypeParameterDTO()
            {
                PropertyData = loOtherUnitTypeViewModel.SelectedProperty
            };
            eventArgs.TargetPageType = typeof(UploadOtherUnitType);
        }

        private async Task After_Open_Upload_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
            if ((bool)eventArgs.Result == true)
            {
                await _gridOtherUnitTypeRef.R_RefreshGrid(null);
            }
        }


        /*
                private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
                {
                    if (eventArgs.TabStripTab.Id == "UP" && loOtherUnitTypeViewModel.loOtherUnitTypeList.Count() == 0)
                    {
                        eventArgs.Cancel = true;
                    }
                }*/


        #region Other Unit Type

        private void R_Before_OpenOtherUnit_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = loOtherUnitTypeViewModel.SelectedProperty.CPROPERTY_ID;
            eventArgs.TargetPageType = typeof(GSM02541);
        }

        private void R_Before_OpenUtilities_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            //eventArgs.Parameter = new UploadUnitUtilityParameterDTO()
            //{
            //    OtherUnitData = new GSM02500COMMON.DTOs.GSM02500.SelectedOtherUnitDTO()
            //    {
            //        COTHER_UNIT_ID = lo
            //    }
            //};
            eventArgs.TargetPageType = typeof(GSM02541);
        }

        private async Task Grid_DisplayOtherUnitType(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02540DetailDTO)eventArgs.Data;

                loOtherUnitTypeViewModel.loOtherUnitTypeDetail = loParam;
                loOtherUnitTypeViewModel.SelectedActiveInactiveLACTIVE = loParam.LACTIVE;
                if (loParam.LACTIVE)
                {
                    loOtherUnitTypeLabel = _localizer["_InActive"];
                    loOtherUnitTypeViewModel.SelectedActiveInactiveLACTIVE = false;
                }
                else
                {
                    loOtherUnitTypeLabel = _localizer["_Activate"];
                    loOtherUnitTypeViewModel.SelectedActiveInactiveLACTIVE = true;
                }
            }
            if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            {
                await OtherUnitTypeNameRef.FocusAsync();
            }
        }


        private async Task Grid_R_ServiceGetOtherUnitTypeListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loOtherUnitTypeViewModel.GetOtherUnitTypeListStreamAsync();

                if (loOtherUnitTypeViewModel.loOtherUnitTypeList.Count() == 0)
                {
                    IsOtherUnitTypeListExist = false;
                }
                else if (loOtherUnitTypeViewModel.loOtherUnitTypeList.Count() > 0)
                {
                    IsOtherUnitTypeListExist = true;
                }

                eventArgs.ListEntityResult = loOtherUnitTypeViewModel.loOtherUnitTypeList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetOtherUnitTypeRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02540DetailDTO loParam = new GSM02540DetailDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02540DetailDTO>(eventArgs.Data);
                await loOtherUnitTypeViewModel.GetOtherUnitTypeAsync(loParam);

                eventArgs.Result = loOtherUnitTypeViewModel.loOtherUnitTypeDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private void Grid_ConvertToGridEntityOtherUnitType(R_ConvertToGridEntityEventArgs eventArgs)
        {
            GSM02540DetailDTO loData = (GSM02540DetailDTO)eventArgs.Data;
            eventArgs.GridData = R_FrontUtility.ConvertObjectToObject<GSM02540DTO>(loData);
            GSM02540DTO loGridData = (GSM02540DTO)eventArgs.GridData;
            loGridData.COTHER_UNIT_TYPE_ID_NAME = loGridData.COTHER_UNIT_TYPE_NAME + '(' + loGridData.COTHER_UNIT_TYPE_ID + ')';
        }
        private async Task Grid_AfterAddOtherUnitType(R_AfterAddEventArgs eventArgs)
        {
            await OtherUnitTypeIdRef.FocusAsync();
        }

        private void Grid_SavingOtherUnitType(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSaveOtherUnitType(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loOtherUnitTypeViewModel.SaveOtherUnitTypeAsync(
                    (GSM02540DetailDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                if (loOtherUnitTypeViewModel.loOtherUnitTypeList.Count() == 0)
                {
                    IsOtherUnitTypeListExist = false;
                }
                else if (loOtherUnitTypeViewModel.loOtherUnitTypeList.Count() > 0)
                {
                    IsOtherUnitTypeListExist = true;
                }

                eventArgs.Result = loOtherUnitTypeViewModel.loOtherUnitTypeDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_AfterSaveOtherUnitType(R_AfterSaveEventArgs eventArgs)
        {
            //if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            //{
            //    await loOtherUnitTypeViewModel.GetOtherUnitTypeListStreamAsync();

            //    var loGridData = _gridOtherUnitTypeRef.CurrentSelectedData;
            //    loOtherUnitTypeViewModel.loCurrentOtherUnitType = loOtherUnitTypeViewModel.loOtherUnitTypeList.FirstOrDefault(x => x.COTHER_UNIT_TYPE_ID == loGridData.COTHER_UNIT_TYPE_ID);
            //    loGridData = loOtherUnitTypeViewModel.loCurrentOtherUnitType;
            //}
        }

        private void Grid_AfterDeleteOtherUnitType()
        {
            if (loOtherUnitTypeViewModel.loOtherUnitTypeList.Count() == 0)
            {
                IsOtherUnitTypeListExist = false;
            }
            else if (loOtherUnitTypeViewModel.loOtherUnitTypeList.Count() > 0)
            {
                IsOtherUnitTypeListExist = true;
            }
        }

        private async Task Grid_ServiceDeleteOtherUnitType(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                GSM02540DetailDTO loData = (GSM02540DetailDTO)eventArgs.Data;
                await loOtherUnitTypeViewModel.DeleteOtherUnitTypeAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ValidationOtherUnitType(R_ValidationEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            R_PopupResult loResult = null;
            GFF00900ParameterDTO loParam = null;
            GSM02540DetailDTO loData = null;
            try
            {
                loData = (GSM02540DetailDTO)eventArgs.Data;

                loOtherUnitTypeViewModel.OtherUnitTypeValidation(loData);

                if (loData.LACTIVE == true && _conductorOtherUnitTypeRef.R_ConductorMode == R_eConductorMode.Add)
                {
                    var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                    loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM02508";
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
                            IAPPROVAL_CODE = "GSM02508"
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

        private async Task R_Before_Open_Popup_ActivateInactiveOtherUnitType(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "GSM02508"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    await loOtherUnitTypeViewModel.ActiveInactiveProcessAsync();
                    var loGetDataParam = (GSM02540DetailDTO)_conductorOtherUnitTypeRef.R_GetCurrentData();
                    var loHeaderData = (GSM02540DTO)_gridOtherUnitTypeRef.GetCurrentData();
                    await _conductorOtherUnitTypeRef.R_GetEntity(loGetDataParam);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "GSM02508" //Uabh Approval Code sesuai Spec masing masing
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

        private async Task R_After_Open_Popup_ActivateInactiveOtherUnitType(R_AfterOpenPopupEventArgs eventArgs)
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
                    await loOtherUnitTypeViewModel.ActiveInactiveProcessAsync();
                    var loGetDataParam = (GSM02540DetailDTO)_conductorOtherUnitTypeRef.R_GetCurrentData();
                    var loHeaderData = (GSM02540DTO)_gridOtherUnitTypeRef.GetCurrentData();
                    await _conductorOtherUnitTypeRef.R_GetEntity(loGetDataParam);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        #endregion

        #region Department
        private async Task OnLostFocusDepartment()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                GSM02540DetailDTO loGetData = (GSM02540DetailDTO)loOtherUnitTypeViewModel.Data;

                if (string.IsNullOrWhiteSpace(loOtherUnitTypeViewModel.Data.CDEPT_CODE))
                {
                    loGetData.CDEPT_NAME = "";
                    return;
                }

                LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();
                GSL00700ParameterDTO loParam = new GSL00700ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CSEARCH_TEXT = loOtherUnitTypeViewModel.Data.CDEPT_CODE
                };

                var loResult = await loLookupViewModel.GetDepartment(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CDEPT_CODE = "";
                    loGetData.CDEPT_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CDEPT_CODE = loResult.CDEPT_CODE;
                    loGetData.CDEPT_NAME = loResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void R_Before_Open_Department_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00700ParameterDTO loParam = new GSL00700ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CUSER_ID = _clientHelper.UserId
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(Lookup_GSFRONT.GSL00700);
        }

        private void R_After_Open_Department_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            GSL00700DTO loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (GSM02540DetailDTO)_conductorOtherUnitTypeRef.R_GetCurrentData();
            loGetData.CDEPT_CODE = loTempResult.CDEPT_CODE;
            loGetData.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }
        #endregion
    }
}