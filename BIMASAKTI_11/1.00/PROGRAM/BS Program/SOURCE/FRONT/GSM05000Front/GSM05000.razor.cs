using GSM05000Common.DTOs;
using GSM05000Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid.Columns;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;


namespace GSM05000Front;

public partial class GSM05000 : R_Page
{
    private GSM05000ViewModel _GSM05000ViewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<GSM05000GridDTO> _gridRef;

    private GSM05000NumberingViewModel _GSM05000NumberingViewModel = new();
    private R_ConductorGrid _conductorRefNumbering;
    private R_Grid<GSM05000NumberingGridDTO> _gridRefNumbering;

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _GSM05000ViewModel.GetDelimiterList();
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #region Tab Transaction

    private async Task Grid_Display(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM05000DTO)eventArgs.Data;
                await _GSM05000ViewModel.GetEntity(loParam);
                await GetUpdateSample();
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Grid_GetList(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _GSM05000ViewModel.GetGridList();
            eventArgs.ListEntityResult = _GSM05000ViewModel.GridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Conductor_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000DTO>(eventArgs.Data);
            await _GSM05000ViewModel.GetEntity(loParam);

            var loParamNumbering = R_FrontUtility.ConvertObjectToObject<GSM05000NumberingHeaderDTO>(eventArgs.Data);
            _GSM05000NumberingViewModel.TransactionCode = loParamNumbering.CTRANSACTION_CODE;

            eventArgs.Result = _GSM05000ViewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Conductor_ServiceSave(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000DTO>(eventArgs.Data);
            await _GSM05000ViewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _GSM05000ViewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetUpdateSample()
    {
        var loEx = new R_Exception();

        try
        {
            await _GSM05000ViewModel.getRefNo();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task Conductor_AfterServiceSave(R_AfterSaveEventArgs arg)
    {
        var loEx = new R_Exception();

        try
        {
            await _gridRef.R_RefreshGrid((GSM05000DTO)arg.Data);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion


    #region Tab Numbering

    private R_GridLookupColumn llDeptCode;

    private async Task Grid_GetListNumbering(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _GSM05000NumberingViewModel.GetNumberingList();
            eventArgs.ListEntityResult = _GSM05000NumberingViewModel.GridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task ChangeTab(R_TabStripTab arg)
    {
        var loEx = new R_Exception();

        try
        {
            if (arg.Id == "tabNumbering")
            {
                await _GSM05000NumberingViewModel.GetNumberingHeader();
                await _gridRefNumbering.R_RefreshGrid(null);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    private async Task Conductor_AfterAddNumbering(R_AfterAddEventArgs arg)
    {
        var ldYear = DateTime.Now;

        ((GSM05000NumberingGridDTO)arg.Data).CPERIOD = (_GSM05000NumberingViewModel.HeaderEntity.CPERIOD_MODE == "P")
            ? ldYear.Year.ToString("D4") + "-" + ldYear.Month.ToString("D2")
            : ldYear.Year.ToString("D4");
    }

    private async Task Conductor_SetAddNumbering(R_SetEventArgs arg)
    {
        if (_GSM05000NumberingViewModel.HeaderEntity.LDEPT_MODE == false)
        {
            llDeptCode.R_EnableAdd = arg.Enable;
        }
    }
}