using LMT03500Common.DTOs;
using LMT03500Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;

namespace LMT03500Front;

public partial class LMT03500UpdateMeter : R_Page
{
    private LMT03500UpdateMeterViewModel _viewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<LMT03500UtilityMeterDTO> _gridRef = new();

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init(poParameter);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void BeforeLookupBuilding(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(LMT03500BuildingLookup);
        eventArgs.Parameter = _viewModel.Header.CPROPERTY_ID;
    }

    private void AfterLookupBuilding(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (LMT03500BuildingDTO)eventArgs.Result;
        if (loTempResult == null)
            return;

        _viewModel.Header.CBUILDING_ID = loTempResult.CBUILDING_ID;
    }

    private void BeforeLookupBuildingUnit(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(LMT03500UnitLookup);
        eventArgs.Parameter = _viewModel.Header;
    }

    private async Task AfterLookupBuildingUnit(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (LMT03500BuildingUnitDTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.Header.CUNIT_ID = loTempResult.CUNIT_ID;
            _viewModel.Header.CUNIT_NAME = loTempResult.CUNIT_NAME;
            await OnChangeHeader();
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
            await _viewModel.GetList(eventArgs.Parameter);
            eventArgs.ListEntityResult = _viewModel.GridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void GetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        _viewModel.GetRecord((LMT03500UtilityMeterDTO)eventArgs.Data);
        eventArgs.Result = _viewModel.Entity;
    }

    private void OnChangeBuildingParam()
    {
        var loEx = new R_Exception();
        try
        {
            _viewModel.Header.CUNIT_ID = null;
            _viewModel.Header.CUNIT_NAME = null;
            _viewModel.Header.CREF_NO = null;
            _viewModel.Header.CTENANT_ID = null;
            _viewModel.Header.CTENANT_NAME = null;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        R_DisplayException(loEx);
    }

    private async Task OnChangeHeader()
    {
        var loEx = new R_Exception();
        try
        {
            if (_viewModel.Header.CBUILDING_ID is null or "")
            {
                goto EndBlock;
            }

            if (_viewModel.Header.CUNIT_ID is null or "")
            {
                goto EndBlock;
            }

            await _viewModel.GetAgreementTenant(_viewModel.Header);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }
}