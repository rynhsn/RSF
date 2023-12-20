using BlazorClientHelper;
using GSM04500Common.DTOs;
using GSM04500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM04500Front;

public partial class GSM04500 : R_Page
{
    
    private GSM04500JournalGroupViewModel _viewModel = new();
    private R_ConductorGrid _conductorRef;
    private R_Grid<GSM04500JournalGroupDTO> _gridRef;

    private R_TabStripTab _tabAccSetting;
    private R_TabStrip _tab;
    private bool _flagCombo;
    [Inject] private IJSRuntime JS { get; set; }
    [Inject] private IClientHelper _clientHelper { get; set; }

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
    
    private void Display(R_DisplayEventArgs eventArgs)
    {
        _viewModel.Entity = (GSM04500JournalGroupDTO)eventArgs.Data;
    }
    private async Task GetGridList(R_ServiceGetListRecordEventArgs eventArgs)
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
    
    private async Task GetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM04500JournalGroupDTO>(eventArgs.Data);

            await _viewModel.GetEntity(loParam);
            eventArgs.Result = _viewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task Save(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM04500JournalGroupDTO>(eventArgs.Data);
            loParam.CJRNGRP_CODE ??= string.Empty;
            loParam.CJRNGRP_NAME ??= string.Empty;
            await _viewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _viewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Delete(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM04500JournalGroupDTO>(eventArgs.Data);
            await _viewModel.DeleteEntity(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }

    private void AfterAdd(R_AfterAddEventArgs eventArgs)
    {
        var loData = (GSM04500JournalGroupDTO)eventArgs.Data;
        loData.DCREATE_DATE = DateTime.Now;
        loData.DUPDATE_DATE = DateTime.Now;
    }

    private void SetOther(R_SetEventArgs eventArgs)
    {
        _tabAccSetting.Enabled = eventArgs.Enable;
        _flagCombo = eventArgs.Enable;
    }

    private async Task OnChangeParam(object value, string type)
    {
        var loEx = new R_Exception();
        var lcType = (string)value;
        try
        {
            if (type == "property") _viewModel.PropertyId = lcType;
            else if (type == "type") _viewModel.TypeCode = lcType;

            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        R_DisplayException(loEx);
    }

    private void OnChangeTab(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
    {
        _flagCombo = eventArgs.TabStripTab.Id == "JournalGroup";
    }

    private void BeforeOpenAccSetting(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(GSM04500AccSetting);
        eventArgs.Parameter = _viewModel.Entity;
    }

    private async Task DownloadTemplate()
    {
        var loEx = new R_Exception();
        string loCompanyName = _clientHelper.CompanyId.ToUpper();

        try
        {
                var loByteFile = await _viewModel.DownloadTemplate();
                var saveFileName = $"Journal Group - {loCompanyName}.xlsx";
                await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
        
    }
    
    private void BeforeOpenUpload(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.Parameter = new GSM04500ParamDTO()
        {
            CCOMPANY_ID = _clientHelper.CompanyId,
            CUSER_ID = _clientHelper.UserId,
            CPROPERTY_ID = _viewModel.PropertyId,
            CPROPERTY_NAME = _viewModel.PropertyList.Where(x => x.CPROPERTY_ID == _viewModel.PropertyId).Select(x => x.CPROPERTY_NAME).FirstOrDefault(),
            CJRNGRP_TYPE = _viewModel.TypeCode
        };
        eventArgs.TargetPageType = typeof(GSM04500UploadPopup);
    }

    private async Task AfterOpenUpload(R_AfterOpenPopupEventArgs eventArgs)
    {
        await _gridRef.R_RefreshGrid(null);
        // await Task.CompletedTask;
    }
}