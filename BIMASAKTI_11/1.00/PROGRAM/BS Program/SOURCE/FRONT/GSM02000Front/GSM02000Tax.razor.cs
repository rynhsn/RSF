using GSM02000Common.DTOs;
using GSM02000Model;
using GSM02000Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM02000Front;

public partial class GSM02000Tax : R_Page
{
    private GSM02000TaxViewModel _viewModel = new();
    private R_ConductorGrid _conductorRef;
    private R_Grid<GSM02000TaxDTO> _gridRef;
    
    private R_Conductor _conductorSalesRef;
    private R_Grid<GSM02000TaxSalesDTO> _gridSalesRef;
    
    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _gridSalesRef.R_RefreshGrid(null);
            // await _gridRef.AutoFitAllColumnsAsync();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task GetSalesTaxList(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetSalesTaxList();
            eventArgs.ListEntityResult = _viewModel.SalesGridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task GetList(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loSalesTax = R_FrontUtility.ConvertObjectToObject<GSM02000TaxDTO>(eventArgs.Parameter);
            await _viewModel.GetGridList(loSalesTax.CTAX_ID);
            eventArgs.ListEntityResult = _viewModel.GridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetSalesTaxRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        
        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM02000TaxDTO>(eventArgs.Data);
            _viewModel.GetSalesTaxEntity(loParam.CTAX_ID);
            eventArgs.Result = _viewModel.Entity;
            await _gridRef.R_RefreshGrid(loParam);
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
    
    private async Task GetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM02000TaxDTO>(eventArgs.Data);

            await _viewModel.GetEntity(loParam);
            eventArgs.Result = _viewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private Task AfterAdd(R_AfterAddEventArgs eventArgs)
    {
        
        var loEx = new R_Exception();

        try
        {
            var loParam = (GSM02000TaxDTO)eventArgs.Data;
            loParam.DTAX_DATE = DateTime.Now;
            loParam.DCREATE_DATE = DateTime.Now;
            loParam.DUPDATE_DATE = DateTime.Now;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return Task.CompletedTask;
    }

    private Task Saving(R_SavingEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        
        try
        {
            var loParam = (GSM02000TaxDTO)eventArgs.Data;
            loParam.CTAX_ID = _viewModel.SelectedSalesTaxId;
            loParam.CTAX_DATE = loParam.DTAX_DATE.ToString("yyyyMMdd");
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
        return Task.CompletedTask;
    }
    
    //service save
    private async Task Save(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM02000TaxDTO>(eventArgs.Data);
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
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM02000TaxDTO>(eventArgs.Data);
            await _viewModel.DeleteEntity(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }

    private Task BeforeDelete(R_BeforeDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        
        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM02000TaxDTO>(eventArgs.Data);
            loParam.CTAX_ID = _viewModel.SelectedSalesTaxId;
            loParam.CTAX_DATE = loParam.DTAX_DATE.ToString("yyyyMMdd");
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
        return Task.CompletedTask;
    }

    private void Validation(R_ValidationEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        
        GSM02000TaxDTO loData;
        try
        {
            loData = (GSM02000TaxDTO)eventArgs.Data;
            if(loData.DTAX_DATE == null)
            {
                loEx.Add("Err05", _localizer["Err05"]);
            }
            if (loData.NTAX_PERCENTAGE == null)
            {
                loEx.Add("Err06", _localizer["Err06"]);
            }

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }
}