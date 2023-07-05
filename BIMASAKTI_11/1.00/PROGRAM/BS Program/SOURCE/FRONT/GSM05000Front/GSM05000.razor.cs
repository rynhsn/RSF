using GSM05000Common.DTOs;
using GSM05000Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
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

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _GSM05000ViewModel.GetDelimiterList();

            var loGroupDescriptor = new List<R_GridGroupDescriptor>
            {
                new(){ FieldName = "CMODULE_NAME" }
            };
            await _gridRef.R_GroupBy(loGroupDescriptor);
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void ChangeTab(R_TabStripTab eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Id == "tabNumbering")
            {
                //await _GSM05000NumberingViewModel.GetNumberingHeader();
                //_GSM05000NumberingViewModel.HeaderEntity = R_FrontUtility.ConvertObjectToObject<GSM05000NumberingHeaderDTO>(_GSM05000ViewModel.Entity);
                //await _gridRefNumbering.R_RefreshGrid(null);
            }

            if (eventArgs.Id == "tabApproval")
            {
                //await _GSM05000ApprovalUserViewModel.GetApprovalHeader();
                //await _gridRefDept.R_RefreshGrid(null);
                //await _gridRefApprover.R_RefreshGrid(null);
                //await _gridRefReplacement.R_RefreshGrid(null);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #region Tab Transaction

    private void Grid_Display(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                //var loParam = (GSM05000DTO)eventArgs.Data;
                //await _GSM05000ViewModel.GetEntity(loParam);
                GetUpdateSample();
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

            //var loParamNumbering = R_FrontUtility.ConvertObjectToObject<GSM05000NumberingHeaderDTO>(eventArgs.Data);
            //var loParamApproval = R_FrontUtility.ConvertObjectToObject<GSM05000ApprovalHeaderDTO>(eventArgs.Data);

            //_GSM05000NumberingViewModel.TransactionCode = loParam.CTRANSACTION_CODE;
            //_GSM05000ApprovalUserViewModel.TransactionCode = loParam.CTRANSACTION_CODE;

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

    private void Conductor_AfterServiceSave(R_AfterSaveEventArgs eventArgs)
    {
        //var loEx = new R_Exception();

        //try
        //{
        //    await _gridRef.R_RefreshGrid((GSM05000DTO)eventArgs.Data);
        //}
        //catch (Exception ex)
        //{
        //    loEx.Add(ex);
        //}

        //loEx.ThrowExceptionIfErrors();
    }

    private void GetUpdateSample()
    {
        var loEx = new R_Exception();

        try
        {
            _GSM05000ViewModel.getRefNo();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    #endregion

    #region Tab Numbering
    private void R_Before_Open_TabPageNumbering(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(GSM05000Numbering);
        eventArgs.Parameter = R_FrontUtility.ConvertObjectToObject<GSM05000NumberingHeaderDTO>(_GSM05000ViewModel.Entity);
    }
    #endregion

    #region Tab Approver
    private void R_Before_Open_TabPageApproval(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(GSM05000Approval);
        eventArgs.Parameter = R_FrontUtility.ConvertObjectToObject<GSM05000ApprovalHeaderDTO>(_GSM05000ViewModel.Entity);
    }
    
    
    private void R_Before_Open_TabPageApproval1(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(GSM05000Approval);
        eventArgs.Parameter = R_FrontUtility.ConvertObjectToObject<GSM05000ApprovalHeaderDTO>(_GSM05000ViewModel.Entity);
    }
    #endregion
}