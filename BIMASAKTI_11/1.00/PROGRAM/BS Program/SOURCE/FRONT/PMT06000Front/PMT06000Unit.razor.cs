using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using PMT06000Common.DTOs;
using PMT06000Common.Params;
using PMT06000Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace PMT06000Front;

public partial class PMT06000Unit : R_ITabPage
{
    private PMT06000ViewModel _viewModel = new();
    private PMT06000UnitViewModel _viewModelUnit = new();
    private R_ConductorGrid _conductorRefUnit;
    private R_Grid<PMT06000OvtUnitDTO> _gridRefUnit;

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (PMT06000UnitParam)poParam;
            _viewModel.Entity = loParam.OVT;
            _viewModel.EntityService = loParam.SVC;
            await _gridRefUnit.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetOvertimeUnitListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetOvertimeUnitGridList();
            eventArgs.ListEntityResult = _viewModel.OvertimeUnitGridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void BeforeLookupUnit(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            eventArgs.Parameter = new GSL02300ParameterDTO()
            {
                CPROPERTY_ID = _viewModel.Entity.CPROPERTY_ID,
                CBUILDING_ID = _viewModel.Entity.CBUILDING_ID,
                CPROGRAM_ID = "PMT06000",
                CREF_NO = _viewModel.Entity.CAGREEMENT_NO
            };
            eventArgs.TargetPageType = typeof(GSL02300);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterLookupUnit(R_AfterOpenGridLookupColumnEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GSL02300DTO)eventArgs.Result;
            var loGetData = (PMT06000OvtUnitDTO)eventArgs.ColumnData;
            if (loTempResult == null)
                return;

            loGetData.CUNIT_ID = loTempResult.CUNIT_ID;
            loGetData.CUNIT_NAME = loTempResult.CUNIT_NAME;
            loGetData.CFLOOR_ID = loTempResult.CFLOOR_ID;
            loGetData.CFLOOR_NAME = loTempResult.CFLOOR_NAME;
            loGetData.NGROSS_AREA_SIZE = loTempResult.NGROSS_AREA_SIZE;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void Validation(R_ValidationEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loEntity = (PMT06000OvtUnitDTO)eventArgs.Data;
            if (string.IsNullOrEmpty(loEntity.CUNIT_ID))
            {
                loEx.Add(new Exception(_localizer["PLEASE_SELECT_UNIT"]));
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void Saving(R_SavingEventArgs eventArgs)
    {
        var loEntity = (PMT06000OvtUnitDTO)eventArgs.Data;
        loEntity.CPROPERTY_ID = _viewModel.Entity.CPROPERTY_ID;
        loEntity.CPARENT_ID = _viewModel.EntityService.CREC_ID;
        loEntity.CREC_ID = "";
        loEntity.CREF_NO = _viewModel.Entity.CREF_NO;
        loEntity.CDEPT_CODE = _viewModel.Entity.CDEPT_CODE;
        loEntity.CTRANS_CODE = _viewModel.TRANS_CODE;
        loEntity.CSEQ_NO = _viewModel.EntityService.CSEQ_NO;
        loEntity.CSERVICE_ID = _viewModel.EntityService.CSERVICE_ID;
        loEntity.CFLOOR_ID = loEntity.CFLOOR_ID;
        loEntity.CUNIT_ID = loEntity.CUNIT_ID;
    }

    private async Task Save(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<PMT06000OvtUnitDTO>(eventArgs.Data);
            await _viewModelUnit.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);

            eventArgs.Result = _viewModelUnit.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task BeforeDelete(R_BeforeDeleteEventArgs eventArgs)
    {
        var leMsg = await R_MessageBox.Show("", _localizer["MSG_BEFORE_DELETE_UNIT"], R_eMessageBoxButtonType.YesNo);
        eventArgs.Cancel = leMsg != R_eMessageBoxResult.Yes;
    }

    private async Task Delete(R_ServiceDeleteEventArgs eventArgs)
    {
        
        var loEx = new R_Exception();

        try
        {
            var loParam = (PMT06000OvtUnitDTO)eventArgs.Data;
            await _viewModelUnit.DeleteEntity(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task AfterDelete()
    {
        var loEx = new R_Exception();

        try
        {
            var leMsg = await R_MessageBox.Show("", _localizer["MSG_AFTER_DELETE_UNIT"]);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    // public Task RefreshTabPageAsync(object poParam)
    // {
    //     throw new NotImplementedException();
    // }

    private async Task GetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loEntity = R_FrontUtility.ConvertObjectToObject<PMT06000OvtUnitDTO>(eventArgs.Data);
            await _viewModelUnit.GetEntity(loEntity);
            eventArgs.Result = _viewModelUnit.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task RefreshTabPageAsync(object poParam)
    {
        _viewModel.Entity = (PMT06000OvtDTO)poParam;
        await Task.CompletedTask;
    }
}