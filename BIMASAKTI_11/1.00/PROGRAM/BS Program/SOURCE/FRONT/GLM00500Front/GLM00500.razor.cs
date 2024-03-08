using BlazorClientHelper;
using GLM00500Common.DTOs;
using GLM00500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GLM00500Front;

public partial class GLM00500 : R_Page
{
    private GLM00500HeaderViewModel _viewModel = new();
    private R_Conductor _conductorRef = new();
    private R_Grid<GLM00500BudgetHDDTO> _gridRef = new();
    [Inject] private IClientHelper _clientHelper { get; set; }
    [Inject] private IJSRuntime JS { get; set; }
    private R_TextBox _fieldBudgetNo { get; set; }
    private R_TextBox _fieldBudgetName { get; set; }


    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init();
            await _gridRef.R_RefreshGrid(null);
            // await _gridRef.AutoFitAllColumnsAsync();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private bool _gridEnabled;
    private void SetOther(R_SetEventArgs eventArgs)
    {
        _gridEnabled = eventArgs.Enable;
    }
    
    private async Task RefreshList()
    {
        await _gridRef.R_RefreshGrid(null);
    }

    //GetList
    private async Task GetBudgetHDList(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetBudgetHDList(_viewModel.SelectedYear);
            eventArgs.ListEntityResult = _viewModel.BudgetHDList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    //Get
    private async Task GetBudgetHD(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GLM00500BudgetHDDTO>(eventArgs.Data);
            await _viewModel.GetBudgetHD(loParam);
            eventArgs.Result = _viewModel.BudgetHDEntity;
            await CheckAttribute();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task ValidationBudgetHD(R_ValidationEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GLM00500BudgetHDDTO>(eventArgs.Data);
            if (loParam.CBUDGET_NO is null or "") loEx.Add(new Exception(_localizer["Exception01"]));
            if (loParam.CBUDGET_NAME is null or "") loEx.Add(new Exception(_localizer["Exception02"]));
            if (loParam.CCURRENCY_TYPE is null or "") loEx.Add(new Exception(_localizer["Exception03"]));
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    //save
    private async Task SaveBudgetHD(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GLM00500BudgetHDDTO>(eventArgs.Data);
            loParam.CLANGUAGE_ID = _clientHelper.Culture.TwoLetterISOLanguageName;
            await _viewModel.SaveBudgetHD(loParam, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _viewModel.BudgetHDEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    //delete
    private async Task DeleteBudgetHD(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GLM00500BudgetHDDTO>(eventArgs.Data);
            await _viewModel.DeleteBudgetHD(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task AfterDeleteBudgetHD(object eventArgs)
    {
        await R_MessageBox.Show(_localizer["SuccessLabel"], _localizer["SuccessDelete"], R_eMessageBoxButtonType.OK);
    }

    private async Task SetFinal(object eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loConfirm = await R_MessageBox.Show(_localizer["ConfirmLabel"], _localizer["ConfirmFinalize"],
                R_eMessageBoxButtonType.YesNo);

            if (loConfirm == R_eMessageBoxResult.Yes)
            {
                _viewModel.SetFinalizeBudget(_viewModel.BudgetHDEntity.CREC_ID);
                R_MessageBox.Show(_localizer["SuccessLabel"], _localizer["SuccessFinalize"], R_eMessageBoxButtonType.OK);
                _gridRef.R_RefreshGrid(_viewModel.BudgetHDEntity);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private Task CheckAttribute()
    {
        // _detailTab.Enabled = _viewModel.BudgetHDEntity.CREC_ID != null;
        return Task.CompletedTask;
    }

    private async Task DownloadTemplate()
    {
        var loEx = new R_Exception();

        try
        {
            //buat lcDate dengan format yyyyMMdd_HHmm
            // var lcDate = DateTime.Now.ToString("yyyyMMdd_HHmm");
            var loByteFile = await _viewModel.DownloadTemplate();
            var saveFileName = $"ACCOUNT_BUDGET_UPLOAD.xlsx";
            await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task BeforeCancelBudgetHD(R_BeforeCancelEventArgs eventArgs)
    {
        var loResult = await R_MessageBox.Show(_localizer["ConfirmLabel"], _localizer["CancelBudget"],
            R_eMessageBoxButtonType.YesNo);
        if (loResult == R_eMessageBoxResult.No) eventArgs.Cancel = true;
    }

    private async Task InstanceDetailTab(R_InstantiateDockEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(GLM00500Detail);
        eventArgs.Parameter = R_FrontUtility.ConvertObjectToObject<GLM00500BudgetHDDTO>(_viewModel.BudgetHDEntity);
    }

    private void BeforeOpenUpload(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = _viewModel.BudgetHDEntity;
        eventArgs.TargetPageType = typeof(GLM00500UploadPopup);
    }

    private async Task AfterOpenUpload(R_AfterOpenPopupEventArgs eventArgs)
    {
        await Task.CompletedTask;
    }

    private async Task Display(R_DisplayEventArgs eventArgs)
    {
        switch (eventArgs.ConductorMode)
        {
            case R_eConductorMode.Add:
                await _fieldBudgetNo.FocusAsync();
                break;
            case R_eConductorMode.Edit:
                await _fieldBudgetName.FocusAsync();
                break;
        }
    }
    
    private void AfterAdd(R_AfterAddEventArgs eventArgs)
    {
        var loData = (GLM00500BudgetHDDTO)eventArgs.Data;
        loData.DCREATE_DATE = DateTime.Now;
        loData.DUPDATE_DATE = DateTime.Now;
    }

}