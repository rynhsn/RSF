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

namespace GSM02500FRONT
{
    public partial class GSM02540 : R_Page
    {
        //Unit Promotion Type
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_PopupService PopupService { get; set; }
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private GSM02540ViewModel loUnitPromotionTypeViewModel = new GSM02540ViewModel();

        private R_Conductor _conductorUnitPromotionTypeRef;

        private R_Grid<GSM02540DTO> _gridUnitPromotionTypeRef;

        private string loUnitPromotionTypeLabel = "";

        private string lcCUOMLabel;

        private R_TextBox UnitPromotionTypeIdRef;

        private R_TextBox UnitPromotionTypeNameRef;

        private bool IsUnitPromotionTypeListExist = false;

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
                        Table_Name = "GSM_PROPERTY_UNIT_PROMOTION_TYPE",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loUnitPromotionTypeViewModel.SelectedProperty.CPROPERTY_ID, loData.CUNIT_PROMOTION_TYPE_ID)
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
                        Table_Name = "GSM_PROPERTY_UNIT_PROMOTION_TYPE",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loUnitPromotionTypeViewModel.SelectedProperty.CPROPERTY_ID, loData.CUNIT_PROMOTION_TYPE_ID)
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
                loUnitPromotionTypeLabel = _localizer["_Activate"];
                loUnitPromotionTypeViewModel.SelectedProperty.CPROPERTY_ID = (string)poParameter;

                await loUnitPromotionTypeViewModel.GetSelectedPropertyAsync();
                await loUnitPromotionTypeViewModel.GetCUOMFromPropertyAsync();

                await loUnitPromotionTypeViewModel.GetUnitPromotionTypeListStreamAsync();

                lcCUOMLabel = loUnitPromotionTypeViewModel.loCUOM.CUOM;

                await _gridUnitPromotionTypeRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            if (_conductorUnitPromotionTypeRef.R_ConductorMode == R_eConductorMode.Add || _conductorUnitPromotionTypeRef.R_ConductorMode == R_eConductorMode.Edit)
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
            var loData = new List<TemplateUnitPromotionTypeDTO>();
            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["M001"], R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await loUnitPromotionTypeViewModel.DownloadTemplateUnitPromotionTypeAsync();

                    var saveFileName = $"Unit Promotion Type.xlsx";
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
            eventArgs.Parameter = new UploadUnitPromotionTypeParameterDTO()
            {
                PropertyData = loUnitPromotionTypeViewModel.SelectedProperty
            };
            eventArgs.TargetPageType = typeof(UploadUnitPromotionType);
        }

        private async Task After_Open_Upload_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
            if ((bool)eventArgs.Result == true)
            {
                await _gridUnitPromotionTypeRef.R_RefreshGrid(null);
            }
        }


        /*
                private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
                {
                    if (eventArgs.TabStripTab.Id == "UP" && loUnitPromotionTypeViewModel.loUnitPromotionTypeList.Count() == 0)
                    {
                        eventArgs.Cancel = true;
                    }
                }*/


        #region Unit Promotion Type

        private void R_Before_OpenUnitPromotion_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.Parameter = loUnitPromotionTypeViewModel.SelectedProperty.CPROPERTY_ID;
            eventArgs.TargetPageType = typeof(GSM02541);
        }

        private async Task Grid_DisplayUnitPromotionType(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02540DetailDTO)eventArgs.Data;

                loUnitPromotionTypeViewModel.loUnitPromotionTypeDetail = loParam;
                loUnitPromotionTypeViewModel.SelectedActiveInactiveLACTIVE = loParam.LACTIVE;
                if (loParam.LACTIVE)
                {
                    loUnitPromotionTypeLabel = _localizer["_InActive"];
                    loUnitPromotionTypeViewModel.SelectedActiveInactiveLACTIVE = false;
                }
                else
                {
                    loUnitPromotionTypeLabel = _localizer["_Activate"];
                    loUnitPromotionTypeViewModel.SelectedActiveInactiveLACTIVE = true;
                }
            }
            if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            {
                await UnitPromotionTypeNameRef.FocusAsync();
            }
        }


        private async Task Grid_R_ServiceGetUnitPromotionTypeListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUnitPromotionTypeViewModel.GetUnitPromotionTypeListStreamAsync();

                if (loUnitPromotionTypeViewModel.loUnitPromotionTypeList.Count() == 0)
                {
                    IsUnitPromotionTypeListExist = false;
                }
                else if (loUnitPromotionTypeViewModel.loUnitPromotionTypeList.Count() > 0)
                {
                    IsUnitPromotionTypeListExist = true;
                }

                eventArgs.ListEntityResult = loUnitPromotionTypeViewModel.loUnitPromotionTypeList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetUnitPromotionTypeRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02540DetailDTO loParam = new GSM02540DetailDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02540DetailDTO>(eventArgs.Data);
                await loUnitPromotionTypeViewModel.GetUnitPromotionTypeAsync(loParam);

                eventArgs.Result = loUnitPromotionTypeViewModel.loUnitPromotionTypeDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private void Grid_ConvertToGridEntityUnitPromotionType(R_ConvertToGridEntityEventArgs eventArgs)
        {
            GSM02540DetailDTO loData = (GSM02540DetailDTO)eventArgs.Data;
            eventArgs.GridData = R_FrontUtility.ConvertObjectToObject<GSM02540DTO>(loData);
            GSM02540DTO loGridData = (GSM02540DTO)eventArgs.GridData;
            loGridData.CUNIT_PROMOTION_TYPE_ID_NAME = loGridData.CUNIT_PROMOTION_TYPE_NAME + '(' + loGridData.CUNIT_PROMOTION_TYPE_ID + ')';
        }
        private async Task Grid_AfterAddUnitPromotionType(R_AfterAddEventArgs eventArgs)
        {
            await UnitPromotionTypeIdRef.FocusAsync();
        }

        private void Grid_SavingUnitPromotionType(R_SavingEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                loUnitPromotionTypeViewModel.UnitPromotionTypeValidation((GSM02540DetailDTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceSaveUnitPromotionType(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUnitPromotionTypeViewModel.SaveUnitPromotionTypeAsync(
                    (GSM02540DetailDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                if (loUnitPromotionTypeViewModel.loUnitPromotionTypeList.Count() == 0)
                {
                    IsUnitPromotionTypeListExist = false;
                }
                else if (loUnitPromotionTypeViewModel.loUnitPromotionTypeList.Count() > 0)
                {
                    IsUnitPromotionTypeListExist = true;
                }

                eventArgs.Result = loUnitPromotionTypeViewModel.loUnitPromotionTypeDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_AfterSaveUnitPromotionType(R_AfterSaveEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            {
                await loUnitPromotionTypeViewModel.GetUnitPromotionTypeListStreamAsync();

                var loGridData = _gridUnitPromotionTypeRef.CurrentSelectedData;
                loUnitPromotionTypeViewModel.loCurrentUnitPromotionType = loUnitPromotionTypeViewModel.loUnitPromotionTypeList.FirstOrDefault(x => x.CUNIT_PROMOTION_TYPE_ID == loGridData.CUNIT_PROMOTION_TYPE_ID);
                loGridData = loUnitPromotionTypeViewModel.loCurrentUnitPromotionType;
            }
        }

        private void Grid_AfterDeleteUnitPromotionType()
        {
            if (loUnitPromotionTypeViewModel.loUnitPromotionTypeList.Count() == 0)
            {
                IsUnitPromotionTypeListExist = false;
            }
            else if (loUnitPromotionTypeViewModel.loUnitPromotionTypeList.Count() > 0)
            {
                IsUnitPromotionTypeListExist = true;
            }
        }

        private async Task Grid_ServiceDeleteUnitPromotionType(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                GSM02540DetailDTO loData = (GSM02540DetailDTO)eventArgs.Data;
                await loUnitPromotionTypeViewModel.DeleteUnitPromotionTypeAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ValidationUnitPromotionType(R_ValidationEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            R_PopupResult loResult = null;
            GFF00900ParameterDTO loParam = null;
            GSM02540DetailDTO loData = null;
            try
            {
                loData = (GSM02540DetailDTO)eventArgs.Data;
                if (loData.LACTIVE == true && _conductorUnitPromotionTypeRef.R_ConductorMode == R_eConductorMode.Add)
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

        private async Task R_Before_Open_Popup_ActivateInactiveUnitPromotionType(R_BeforeOpenPopupEventArgs eventArgs)
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
                    await loUnitPromotionTypeViewModel.ActiveInactiveProcessAsync();
                    var loGetDataParam = (GSM02540DetailDTO)_conductorUnitPromotionTypeRef.R_GetCurrentData();
                    var loHeaderData = (GSM02540DTO)_gridUnitPromotionTypeRef.GetCurrentData();
                    await _conductorUnitPromotionTypeRef.R_GetEntity(loGetDataParam);
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

        private async Task R_After_Open_Popup_ActivateInactiveUnitPromotionType(R_AfterOpenPopupEventArgs eventArgs)
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
                    await loUnitPromotionTypeViewModel.ActiveInactiveProcessAsync();
                    var loGetDataParam = (GSM02540DetailDTO)_conductorUnitPromotionTypeRef.R_GetCurrentData();
                    var loHeaderData = (GSM02540DTO)_gridUnitPromotionTypeRef.GetCurrentData();
                    await _conductorUnitPromotionTypeRef.R_GetEntity(loGetDataParam);
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