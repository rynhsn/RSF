using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using HDM00400Common.DTOs;
using HDM00400Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace HDM00400Front;

public partial class HDM00400
{
    private HDM00400ViewModel _viewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<HDM00400PublicLocationDTO> _gridRef = new();
    private R_TextBox _publicLocIdRef;
    private R_TextBox _publicLocNameRef;
    public R_ComboBox<HDM00400PropertyDTO, string> _comboPropertyRef { get; set; }
    
    [Inject] private IClientHelper _clientHelper { get; set; }
    [Inject] private IJSRuntime JS { get; set; }

    #region Locking

    private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
    private const string DEFAULT_MODULE_NAME = "GS";

    protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        var llRtn = false;
        R_LockingFrontResult loLockResult;

        try
        {
            var loData = (HDM00400PublicLocationDTO)eventArgs.Data;

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
                    Program_Id = "HDM00400",
                    Table_Name = "HDM_PUBLIC_LOCATION",
                    Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CPUBLIC_LOC_ID)
                };

                loLockResult = await loCls.R_Lock(loLockPar);
            }
            else
            {
                var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                {
                    Company_Id = _clientHelper.CompanyId,
                    User_Id = _clientHelper.UserId,
                    Program_Id = "HDM00400",
                    Table_Name = "HDM_PUBLIC_LOCATION",
                    Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CPUBLIC_LOC_ID)
                };

                loLockResult = await loCls.R_UnLock(loUnlockPar);
            }

            llRtn = loLockResult.IsSuccess;
            if (loLockResult is { IsSuccess: false, Exception: not null })
                throw loLockResult.Exception;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return llRtn;
    }
    
    
    private async Task lockingButton(bool param = true)
    {
        var loEx = new R_Exception();
        R_LockingFrontResult loLockResult = null;
        try
        {
            var loData = _viewModel.Entity;
            var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                plSendWithContext: true,
                plSendWithToken: true,
                pcHttpClientName: DEFAULT_HTTP_NAME);
            if (param) // Lock
            {
                var loLockPar = new R_ServiceLockingLockParameterDTO
                {
                    Company_Id = _clientHelper.CompanyId,
                    User_Id = _clientHelper.UserId,
                    Program_Id = "HDM00400",
                    Table_Name = "HDM_PUBLIC_LOCATION",
                    Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CPUBLIC_LOC_ID)
                };
                loLockResult = await loCls.R_Lock(loLockPar);
            }
            else // Unlock
            {
                var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                {
                    Company_Id = _clientHelper.CompanyId,
                    User_Id = _clientHelper.UserId,
                    Program_Id = "HDM00400",
                    Table_Name = "HDM_PUBLIC_LOCATION",
                    Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CPUBLIC_LOC_ID)
                };
                loLockResult = await loCls.R_UnLock(loUnlockPar);
            }
            if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                throw loLockResult.Exception;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init();
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private async Task ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetGridList();
            eventArgs.ListEntityResult = _viewModel.GridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<HDM00400PublicLocationDTO>(eventArgs.Data);

            await _viewModel.GetEntity(loParam);
            eventArgs.Result = _viewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<HDM00400PublicLocationDTO>(eventArgs.Data);
            await _viewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);

            eventArgs.Result = _viewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (HDM00400PublicLocationDTO)eventArgs.Data;
            await _viewModel.DeleteEntity(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task ValueChangedProperty(string value)
    {
        var loEx = new R_Exception();
        try
        {
            if (string.IsNullOrEmpty(value))
            {
                value = _viewModel.SelectedProperty;
            }

            if (_viewModel.SelectedProperty != value)
            {
                _viewModel.SelectedProperty = value;
                await _gridRef.R_RefreshGrid(null);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task AfterAdd(R_AfterAddEventArgs obj)
    {
        var loData = (HDM00400PublicLocationDTO)obj.Data;
        loData.CPROPERTY_ID = _viewModel.SelectedProperty;
        loData.LACTIVE = true;
        await _publicLocIdRef.FocusAsync();
    }

    private async Task BeforeEdit(R_BeforeEditEventArgs obj)
    {
        await _publicLocNameRef.FocusAsync();
    }

    private void SetOther(R_SetEventArgs obj)
    {
        var hasOther = obj.Enable;
    }

    
    private async Task DownloadTemplate(MouseEventArgs obj)
    {
        var loEx = new R_Exception();

        try
        {
            //buat lcDate dengan format yyyyMMdd_HHmm
            var lcDate = DateTime.Now.ToString("yyyyMMdd_HHmm");
            var loByteFile = await _viewModel.DownloadTemplate();
            var saveFileName = $@"PublicLocation_{lcDate}.xlsx";
            await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    private void BeforeOpenUpload(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(HDM00400PopupUpload);
        var loParam = new HDM00400UploadParam()
        {
            CPROPERTY_ID = _viewModel.SelectedProperty,
            CPROPERTY_NAME = _viewModel.PropertyList.FirstOrDefault(x => x.CPROPERTY_ID == _viewModel.SelectedProperty)
                ?.CPROPERTY_NAME
        };
        eventArgs.Parameter = loParam;
    }

    private async Task AfterOpenUpload(R_AfterOpenPopupEventArgs eventArgs)
    {
        if (eventArgs.Success)
        {
            await _gridRef.R_RefreshGrid(null);
        }
    }
    
    
    private async Task BeforeOpenActiveInactive(R_BeforeOpenPopupEventArgs eventArgs)
    {
        var loException = new R_Exception();
        try
        {
            const string programCode = "HDM00401"; //Uabh Program Code sesuai Spec masing masing
            var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
            loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE =
                programCode; //Uabh Approval Code sesuai Spec masing masing
            await loValidateViewModel
                .RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

            //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
            if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" &&
                loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
            {
                await _viewModel.SetActiveInactive();
                await _gridRef.R_RefreshGrid(null);
                return;
            }
            else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
            {
                eventArgs.Parameter = new GFF00900ParameterDTO()
                {
                    Data = loValidateViewModel.loRspActivityValidityList,
                    IAPPROVAL_CODE = programCode //Uabh Approval Code sesuai Spec masing masing
                };
                eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
            }
            
            await lockingButton(true);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();
    }

    private async Task AfterOpenActiveInactive(R_AfterOpenPopupEventArgs eventArgs)
    {
        var loException = new R_Exception();
        try
        {
            if (eventArgs.Success == false)
                return;

            var result = (bool)eventArgs.Result;
            if (result)
            {
                await _viewModel.SetActiveInactive();
                await _gridRef.R_RefreshGrid(null);
            }
            
            await lockingButton(false);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();
    }
    
    
    
    private async Task OnLostFocusBuilding(object obj)
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL02200ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.Data.CBUILDING_ID))
            {
                _viewModel.Data.CBUILDING_NAME = "";
                _viewModel.Data.CFLOOR_ID = "";
                _viewModel.Data.CFLOOR_NAME = "";
                return;
            }

            var param = new GSL02200ParameterDTO
            {
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CSEARCH_TEXT = _viewModel.Data.CBUILDING_ID
            };

            GSL02200DTO loResult = null;

            loResult = await loLookupViewModel.GetBuilding(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.Data.CBUILDING_ID = "";
                _viewModel.Data.CBUILDING_NAME = "";
                goto EndBlock;
            }

            _viewModel.Data.CBUILDING_ID = loResult.CBUILDING_ID;
            _viewModel.Data.CBUILDING_NAME = loResult.CBUILDING_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        R_DisplayException(loEx);
    }
    private void BeforeLookupBuilding(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(GSL02200);
        eventArgs.Parameter = new GSL02200ParameterDTO()
        {
            CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID
        };
    }
    private void AfterLookupBuilding(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GSL02200DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.Data.CBUILDING_ID = loTempResult.CBUILDING_ID;
            _viewModel.Data.CBUILDING_NAME = loTempResult.CBUILDING_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    
    private async Task OnLostFocusFloor(object obj)
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL02400ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.Data.CFLOOR_ID))
            {
                _viewModel.Data.CFLOOR_NAME = "";
                return;
            }

            var param = new GSL02400ParameterDTO
            {
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CBUILDING_ID = _viewModel.Data.CBUILDING_ID,
                CSEARCH_TEXT = _viewModel.Data.CFLOOR_ID
            };

            GSL02400DTO loResult = null;

            loResult = await loLookupViewModel.GetFloor(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.Data.CFLOOR_ID = "";
                _viewModel.Data.CFLOOR_NAME = "";
                goto EndBlock;
            }

            _viewModel.Data.CFLOOR_ID = loResult.CFLOOR_ID;
            _viewModel.Data.CFLOOR_NAME = loResult.CFLOOR_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        R_DisplayException(loEx);
    }
    private void BeforeLookupFloor(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(GSL02400);
        eventArgs.Parameter = new GSL02400ParameterDTO()
        {
            CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
            CBUILDING_ID = _viewModel.Data.CBUILDING_ID
        };
    }
    private void AfterLookupFloor(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GSL02400DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.Data.CFLOOR_ID = loTempResult.CFLOOR_ID;
            _viewModel.Data.CFLOOR_NAME = loTempResult.CFLOOR_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

}