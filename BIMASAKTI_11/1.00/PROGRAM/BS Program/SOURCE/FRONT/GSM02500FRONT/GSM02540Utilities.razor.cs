using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using GSM02500COMMON.DTOs;
using GSM02500COMMON.DTOs.GSM02502Charge;
using GSM02500COMMON.DTOs.GSM02510;
using GSM02500COMMON.DTOs.GSM02520;
using GSM02500COMMON.DTOs.GSM02530;
using GSM02500COMMON.DTOs.GSM02531;
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
    public partial class GSM02540Utilities : R_Page
    {
        //Utilities
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_PopupService PopupService { get; set; }
        [Inject] private R_ILocalizer<GSM02500FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private GSM02540UtilitiesViewModel loUtilitiesViewModel = new GSM02540UtilitiesViewModel();

        private R_Conductor _conductorUtilityTypeRef;

        private R_Conductor _conductorUtilitiesRef;

        private R_Grid<GSM02531UtilityTypeDTO> _gridUtilityTypeRef;

        private R_Grid<GSM02531UtilityDTO> _gridUtilitiesRef;

        private string loUtilitiesLabel = "";

        private R_TextBox _meterNoRef;

        private string loLabel = "";

        private bool IsKVAHidden = false;

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
                GSM02531UtilityDetailDTO loData = (GSM02531UtilityDetailDTO)eventArgs.Data;

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
                        Table_Name = "GSM_PROPERTY_UNIT_UTILITY_METER",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loUtilitiesViewModel.loTabParameter.CSELECTED_PROPERTY_ID,
                        loUtilitiesViewModel.loTabParameter.CSELECTED_BUILDING_ID, loUtilitiesViewModel.loTabParameter.CSELECTED_FLOOR_ID,
                        loUtilitiesViewModel.loTabParameter.CSELECTED_UNIT_ID, loUtilitiesViewModel.loUtilityType.CCODE, loData.CSEQUENCE)
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
                        Table_Name = "GSM_PROPERTY_UNIT_UTILITY_METER",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loUtilitiesViewModel.loTabParameter.CSELECTED_PROPERTY_ID,
                        loUtilitiesViewModel.loTabParameter.CSELECTED_BUILDING_ID, loUtilitiesViewModel.loTabParameter.CSELECTED_FLOOR_ID,
                        loUtilitiesViewModel.loTabParameter.CSELECTED_UNIT_ID, loUtilitiesViewModel.loUtilityType.CCODE, loData.CSEQUENCE)
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
                loLabel = _localizer["_Capacity(Electricity)"];
                loUtilitiesLabel = _localizer["_Activate"];
                loUtilitiesViewModel.loTabParameter = (TabParameterDTO)poParameter;

                //loUtilitiesViewModel.loTabParameter.CSELECTED_OTHER_UNIT_ID = loUtilitiesViewModel.loUploadUnitUtilityParameter.OtherUnitData.COTHER_UNIT_ID;

                await loUtilitiesViewModel.GetSelectedOtherUnitAsync();
                if (loUtilitiesViewModel.SelectedOtherUnit != null)
                {
                    loUtilitiesViewModel.loUploadUnitUtilityParameter.OtherUnitData.COTHER_UNIT_ID = loUtilitiesViewModel.SelectedOtherUnit.COTHER_UNIT_ID;
                    loUtilitiesViewModel.loUploadUnitUtilityParameter.OtherUnitData.COTHER_UNIT_NAME = loUtilitiesViewModel.SelectedOtherUnit.COTHER_UNIT_NAME;
                }

                //await loUtilitiesViewModel.GetUtilityTypeListStreamAsync();

                await _gridUtilityTypeRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickTemplate()
        {
            R_Exception loException = new R_Exception();
            var loData = new List<TemplateUnitUtilityDTO>();
            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["M001"], R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await loUtilitiesViewModel.DownloadTemplateUnitUtilityAsync();

                    var saveFileName = $"Unit Utility.xlsx";

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
            eventArgs.Parameter = new UploadUnitUtilityParameterDTO()
            {
                OtherUnitData = loUtilitiesViewModel.SelectedOtherUnit,
                SelectedUtilityTypeId = loUtilitiesViewModel.loUtilityType.CCODE
            };
            eventArgs.TargetPageType = typeof(UploadUnitUtility);
        }

        private async Task After_Open_Upload_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            if (eventArgs.Success == false)
            {
                return;
            }
            if ((bool)eventArgs.Result == true)
            {
                await _gridUtilitiesRef.R_RefreshGrid(null);
            }
        }

        #region Utilities

        private async Task Grid_Utility_Type_Display(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = (GSM02531UtilityTypeDTO)_gridUtilityTypeRef.GetCurrentData();
                    loUtilitiesViewModel.loUtilityType = loParam;
                    if (loParam.CCODE == "03" || loParam.CCODE == "04")
                    {
                        loLabel = _localizer["_PipeSize"];
                        IsKVAHidden = true;
                    }
                    else
                    {
                        loLabel = _localizer["_Capacity(Electricity)"];
                        IsKVAHidden = false;
                    }
                    await _gridUtilitiesRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Utilities_Display(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = (GSM02531UtilityDetailDTO)eventArgs.Data;
                    loUtilitiesViewModel.loUtilityDetail = loParam;
                    loUtilitiesViewModel.SelectedActiveInactiveLACTIVE = loParam.LACTIVE;
                    if (loParam.LACTIVE)
                    {
                        loUtilitiesLabel = _localizer["_InActive"];
                        loUtilitiesViewModel.SelectedActiveInactiveLACTIVE = false;
                    }
                    else
                    {
                        loUtilitiesLabel = _localizer["_Activate"];
                        loUtilitiesViewModel.SelectedActiveInactiveLACTIVE = true;
                    }
                }
                if (eventArgs.ConductorMode == R_eConductorMode.Edit)
                {
                    await _meterNoRef.FocusAsync();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_AfterAddUtilities(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                GSM02531UtilityDetailDTO loData = (GSM02531UtilityDetailDTO)eventArgs.Data;
                await _meterNoRef.FocusAsync();
                string loNewSequence = "";
                if (loUtilitiesViewModel.loUtilityList.Count() == 0)
                {
                    loNewSequence = "001";
                }
                else
                {
                    GSM02531UtilityDTO loLastData = loUtilitiesViewModel.loUtilityList.OrderBy(s => int.Parse(s.CSEQUENCE)).Last();
                    loNewSequence = (int.Parse(loLastData.CSEQUENCE) + 1).ToString("D3");
                }

                loData.CSEQUENCE = loNewSequence;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_R_ServiceGetUtilityTypeListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUtilitiesViewModel.GetUtilityTypeListStreamAsync();
                eventArgs.ListEntityResult = loUtilitiesViewModel.loUtilityTypeList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_R_ServiceGetUtilitiesListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUtilitiesViewModel.GetUtilitiesListStreamAsync();
                eventArgs.ListEntityResult = loUtilitiesViewModel.loUtilityList;
                if (loUtilitiesViewModel.loUtilityList.Count() == 0)
                {
                    loUtilitiesViewModel.loUtilityDetail.ResetToDefaultValues();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetUtilityTypeRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02531UtilityTypeDTO loParam = new GSM02531UtilityTypeDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02531UtilityTypeDTO>(eventArgs.Data);
                loUtilitiesViewModel.loUtilityType = loParam;
                eventArgs.Result = loUtilitiesViewModel.loUtilityDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceGetUtilitiesRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                GSM02531UtilityDetailDTO loParam = new GSM02531UtilityDetailDTO();

                loParam = R_FrontUtility.ConvertObjectToObject<GSM02531UtilityDetailDTO>(eventArgs.Data);

                await loUtilitiesViewModel.GetUtilityAsync(loParam);

                eventArgs.Result = loUtilitiesViewModel.loUtilityDetail;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task Grid_SavingUtilities(R_SavingEventArgs eventArgs)
        {
        }

        private async Task Grid_ServiceSaveUtilities(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loUtilitiesViewModel.SaveUtilityAsync(
                    (GSM02531UtilityDetailDTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = loUtilitiesViewModel.loUtilityDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ServiceDeleteUtilities(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                GSM02531UtilityDetailDTO loData = (GSM02531UtilityDetailDTO)eventArgs.Data;
                await loUtilitiesViewModel.DeleteUtilityAsync(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_ValidationUtilities(R_ValidationEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            R_PopupResult loResult = null;
            GFF00900ParameterDTO loParam = null;
            GSM02531UtilityDetailDTO loData = null;
            try
            {
                loData = (GSM02531UtilityDetailDTO)eventArgs.Data;

                loUtilitiesViewModel.UtilitiesValidation(loData);
                if (loException.HasError)
                {
                    goto EndBlock;
                }

                if (loData.LACTIVE == true && _conductorUtilitiesRef.R_ConductorMode == R_eConductorMode.Add)
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
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }


        private async Task R_Before_Open_Popup_ActivateInactiveUtilities(R_BeforeOpenPopupEventArgs eventArgs)
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
                    await loUtilitiesViewModel.ActiveInactiveProcessAsync(); //Ganti jadi method ActiveInactive masing masing
                    await _gridUtilitiesRef.R_RefreshGrid(null);
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

        private async Task R_After_Open_Popup_ActivateInactiveUtilities(R_AfterOpenPopupEventArgs eventArgs)
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
                    await loUtilitiesViewModel.ActiveInactiveProcessAsync();
                    await _gridUtilitiesRef.R_RefreshGrid(null);
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
