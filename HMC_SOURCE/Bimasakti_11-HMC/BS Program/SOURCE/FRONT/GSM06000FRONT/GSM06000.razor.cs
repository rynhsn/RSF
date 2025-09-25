using GSM06000Common;
using GSM06000Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM06000Front;

public partial class GSM06000 : R_Page
{
    #region GSM06000

    private readonly GSM06000ViewModel _viewModel = new();
    private R_Conductor? _conductorRef;
    private R_Grid<GSM06000DTO>? _gridRef;
    [Inject] private IJSRuntime? JS { get; set; }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetBankList();
            await _viewModel.GetTypeList();
            await OnChanged(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private void PreDock_InstantiateDock(R_InstantiateDockEventArgs eventArgs)
    {
        eventArgs.Parameter = R_FrontUtility.ConvertObjectToObject<GSM06000DTO>(_viewModel.loEntity);
        eventArgs.TargetPageType = typeof(GSM06010Info);
    }

    private void R_AfterOpenPredefinedDock(R_AfterOpenPredefinedDockEventArgs eventArgs)
    {
    }
    
    private void Before_Open_Upload_Popup(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(GSM06000UploadCenter);
        GSM06000ParamDTO loParameter = new GSM06000ParamDTO
        {
            CCB_TYPE = _viewModel.loGridList.Select(item => item.CCB_TYPE).FirstOrDefault(),
            CBANK_TYPE = _viewModel.loGridList.Select(item => item.CBANK_TYPE).FirstOrDefault(),
            CCOMPANY_ID = _viewModel.loGridList.Select(item => item.CCOMPANY_ID).FirstOrDefault()
        };
        eventArgs.Parameter = loParameter;
    }

    private async Task After_Open_Upload_Popup(R_AfterOpenPopupEventArgs eventArgs)
    {
        if (eventArgs.Success == false)
        {
            return;
        }
        if ((bool)eventArgs.Result)
        {
            await _gridRef.R_RefreshGrid(null);
        }
    }
    
    private async Task OnChanged(object? poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetAllCashBankList();
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM06000DTO>(eventArgs.Data);

            await _viewModel.GetEntity(loParam);
            eventArgs.Result = _viewModel.loEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    /*
    private async Task Validation(R_ValidationEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await _viewModel.R_SaveValidation((GSM06000DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        eventArgs.Cancel = loEx.HasError;
        loEx.ThrowExceptionIfErrors();
    }
    */

    private async Task DownloadTemplate()
    {
        var loEx = new R_Exception();

        try
        {
            var loValidate = await R_MessageBox.Show("", "Are you sure download this template?", R_eMessageBoxButtonType.YesNo);

            if (loValidate == R_eMessageBoxResult.Yes)
            {
                var loByteFile = await _viewModel.DownloadTemplate();

                var saveFileName = $"Cash and Bank.xlsx";

                await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
            }
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
            var loParam = (GSM06000DTO)eventArgs.Data;

            await _viewModel.SaveCashBank(loParam, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _viewModel.loEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void ValidationGSM06000(R_ValidationEventArgs eventArgs)
    {
        var loException = new R_Exception();

        try
        {
            var loData = (GSM06000DTO)eventArgs.Data;

            if (string.IsNullOrEmpty(loData.CCB_CODE))
                loException.Add("001", "CCB CODE cannot be null.");

            if (string.IsNullOrEmpty(loData.CCB_NAME))
                loException.Add("002", "CCB NAME cannot be null.");

            if (string.IsNullOrEmpty(loData.CADDRESS))
                loException.Add("003", "Address cannot be null.");
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        eventArgs.Cancel = loException.HasError;

        loException.ThrowExceptionIfErrors();
    }

    private Task BeforeAdd(R_BeforeAddEventArgs eventArgs)
    {
        _viewModel.loEntity = new GSM06000DTO
        {
            CBANK_TYPE = _viewModel.PropertyBankContext,
            CCB_TYPE = _viewModel.PropertyTypeContext
        };

        return Task.CompletedTask;
    }

    private async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loData = (GSM06000DTO)eventArgs.Data;
            await _viewModel.DeleteCashBank(loData);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task R_AfterDelete()
    {
        _viewModel.loEntity = new GSM06000DTO();
        await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
    }

    private async Task ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetAllCashBankList();
            eventArgs.ListEntityResult = _viewModel.loGridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    #endregion

    

}