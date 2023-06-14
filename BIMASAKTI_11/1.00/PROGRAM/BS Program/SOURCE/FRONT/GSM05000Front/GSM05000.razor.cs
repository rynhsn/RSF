using GSM05000Common.DTOs;
using GSM05000Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
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
    
    private GSM05000ApprovalUserViewModel _GSM05000ApprovalUserViewModel = new();
    private R_ConductorGrid _conductorRefDept;
    private R_ConductorGrid _conductorRefApprover;
    private R_Grid<GSM05000ApprovalDepartmentDTO> _gridRefDept;
    private R_Grid<GSM05000ApprovalUserDTO> _gridRefApprover;
    
    private GSM05000ApprovalReplacementViewModel _GSM05000ApprovalReplacementViewModel = new();
    private R_ConductorGrid _conductorRefReplacement;
    private R_Grid<GSM05000ApprovalReplacementDTO> _gridRefReplacement;

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

    private async Task ChangeTab(R_TabStripTab eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Id == "tabNumbering")
            {
                await _GSM05000NumberingViewModel.GetNumberingHeader();
                await _gridRefNumbering.R_RefreshGrid(null);
            }

            if (eventArgs.Id == "tabApproval")
            {
                await _GSM05000ApprovalUserViewModel.GetApprovalHeader();
                await _gridRefDept.R_RefreshGrid(null);
                await _gridRefApprover.R_RefreshGrid(null);
                await _gridRefReplacement.R_RefreshGrid(null);
            }
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
            var loParamApproval = R_FrontUtility.ConvertObjectToObject<GSM05000ApprovalHeaderDTO>(eventArgs.Data);
            
            _GSM05000NumberingViewModel.TransactionCode = loParamNumbering.CTRANSACTION_CODE;
            _GSM05000ApprovalUserViewModel.TransactionCode = loParamApproval.CTRANSACTION_CODE;

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
    
    private async Task Conductor_AfterServiceSave(R_AfterSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _gridRef.R_RefreshGrid((GSM05000DTO)eventArgs.Data);
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
    
    #endregion
    
    #region Tab Numbering
    
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
    
    private async Task Grid_GetRecordNumbering(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000NumberingGridDTO>(eventArgs.Data);
            await _GSM05000NumberingViewModel.GetEntityNumbering(loParam);
            eventArgs.Result = _GSM05000NumberingViewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task Grid_AfterAddNumbering(R_AfterAddEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GSM05000NumberingGridDTO)eventArgs.Data;
            await _GSM05000NumberingViewModel.GeneratePeriod(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task Grid_ServiceSaveNumbering(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000NumberingGridDTO>(eventArgs.Data);
            await _GSM05000NumberingViewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _GSM05000NumberingViewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task Grid_ServiceAfterSaveNumbering(R_AfterSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _gridRefNumbering.R_RefreshGrid((GSM05000NumberingGridDTO)eventArgs.Data);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task Grid_ServiceDeleteNumbering(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000NumberingGridDTO>(eventArgs.Data);
            await _GSM05000NumberingViewModel.DeleteEntity(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private void BeforeLookupNumbering(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            eventArgs.Parameter = new GSL00700ParameterDTO();
            eventArgs.TargetPageType = typeof(GSL00700);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private void AfterLookupNumbering(R_AfterOpenGridLookupColumnEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            var loGetData = (GSM05000NumberingGridDTO)eventArgs.ColumnData;
            if (loTempResult == null)
                return;

            loGetData.CDEPT_CODE = loTempResult.CDEPT_CODE;
            loGetData.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }
    
    #endregion
    
    
    
    #region Tab Approver
    
    private async Task GetListDept(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _GSM05000ApprovalUserViewModel.GetDepartmentList();
            eventArgs.ListEntityResult = _GSM05000ApprovalUserViewModel.DepartmentList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task DisplayDept(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();
    
        try
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM05000ApprovalDepartmentDTO)eventArgs.Data;
                await _GSM05000ApprovalUserViewModel.GetDepartmentEntity(loParam);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    
        loEx.ThrowExceptionIfErrors();
    }
    
    
    private async Task GetListApprover(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _GSM05000ApprovalUserViewModel.GetApproverList();
            eventArgs.ListEntityResult = _GSM05000ApprovalUserViewModel.ApproverList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetRecordApprover(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        
        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000ApprovalUserDTO>(eventArgs.Data);
            await _GSM05000ApprovalUserViewModel.GetApproverEntity(loParam);
            eventArgs.Result = _GSM05000ApprovalUserViewModel.ApproverEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    }

    private async Task SaveApprover(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000ApprovalUserDTO>(eventArgs.Data);
            await _GSM05000ApprovalUserViewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _GSM05000ApprovalUserViewModel.ApproverEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task DisplayApprover(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM05000ApprovalUserDTO)eventArgs.Data;
                await _GSM05000ApprovalUserViewModel.GetApproverEntity(loParam);
                await _gridRefReplacement.R_RefreshGrid(null);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task AfterAddApprover(R_AfterAddEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        
        try
        {
            var loParam = (GSM05000ApprovalUserDTO)eventArgs.Data;
            await _GSM05000ApprovalUserViewModel.GenerateSequence(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    }

    private async Task BeforeLookupApprover(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            eventArgs.Parameter = new GSL01100ParameterDTO()
            {
                CTRANSACTION_CODE = _GSM05000ApprovalUserViewModel.HeaderEntity.CTRANSACTION_CODE
            };
            eventArgs.TargetPageType = typeof(GSL01100);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task AfterLookupApprover(R_AfterOpenGridLookupColumnEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GSL01100DTO)eventArgs.Result;
            var loGetData = (GSM05000ApprovalUserDTO)eventArgs.ColumnData;
            if (loTempResult == null)
                return;

            loGetData.CUSER_ID = loTempResult.CUSER_ID;
            loGetData.CUSER_NAME = loTempResult.CUSER_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task DeleteApprover(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        
        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000ApprovalUserDTO>(eventArgs.Data);
            await _GSM05000ApprovalUserViewModel.DeleteEntity(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    }

    #endregion
    
    private async Task GetListReplacement(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _GSM05000ApprovalReplacementViewModel.DeptCode = _GSM05000ApprovalUserViewModel.DepartmentEntity.CDEPT_CODE;
            _GSM05000ApprovalReplacementViewModel.TransactionCode = _GSM05000ApprovalUserViewModel.HeaderEntity.CTRANSACTION_CODE;
            _GSM05000ApprovalReplacementViewModel.SelectedUserId = _GSM05000ApprovalUserViewModel.ApproverEntity.CUSER_ID;

            await _GSM05000ApprovalReplacementViewModel.GetReplacementList();
            eventArgs.ListEntityResult = _GSM05000ApprovalReplacementViewModel.ReplacementList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task DisplayReplacement(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM05000ApprovalReplacementDTO)eventArgs.Data;
                await _GSM05000ApprovalReplacementViewModel.GetReplacementEntity(loParam);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task SaveReplacement(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000ApprovalReplacementDTO>(eventArgs.Data);
            
            loParam.CDEPT_CODE = _GSM05000ApprovalUserViewModel.DepartmentEntity.CDEPT_CODE;
            loParam.CTRANSACTION_CODE = _GSM05000ApprovalUserViewModel.HeaderEntity.CTRANSACTION_CODE;
            loParam.CUSER_REPLACEMENT = _GSM05000ApprovalUserViewModel.ApproverEntity.CUSER_ID;
            
            loParam.CVALID_TO = loParam.DVALID_TO.ToString("yyyyMMdd");
            loParam.CVALID_FROM = loParam.DVALID_FROM.ToString("yyyyMMdd");
            
            await _GSM05000ApprovalReplacementViewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _GSM05000ApprovalReplacementViewModel.ReplacementEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task DeleteReplacement(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        
        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000ApprovalReplacementDTO>(eventArgs.Data);
            await _GSM05000ApprovalReplacementViewModel.DeleteEntity(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    }

    private async Task BeforeLookupReplacement(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            eventArgs.Parameter = new GSL01100ParameterDTO()
            {
                CTRANSACTION_CODE = _GSM05000ApprovalUserViewModel.HeaderEntity.CTRANSACTION_CODE
            };
            eventArgs.TargetPageType = typeof(GSL01100);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task AfterLookupReplacement(R_AfterOpenGridLookupColumnEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GSL01100DTO)eventArgs.Result;
            var loGetData = (GSM05000ApprovalReplacementDTO)eventArgs.ColumnData;
            if (loTempResult == null)
                return;

            loGetData.CUSER_ID = loTempResult.CUSER_ID;
            loGetData.CUSER_NAME = loTempResult.CUSER_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }

    private async Task AfterAddReplacement(R_AfterAddEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        
        try
        {
            var loParam = (GSM05000ApprovalReplacementDTO)eventArgs.Data;
            var lcCurrentDate = (DateTime.Now);
            loParam.DVALID_TO = lcCurrentDate;
            loParam.DVALID_FROM = lcCurrentDate;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    }

    private async Task SavingReplacement(R_SavingEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        
        try
        {
            var loParam = (GSM05000ApprovalReplacementDTO)eventArgs.Data;
            loParam.CVALID_TO = loParam.DVALID_TO.ToString("yyyyMMdd");
            loParam.CVALID_FROM = loParam.DVALID_FROM.ToString("yyyyMMdd");
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
    }
}