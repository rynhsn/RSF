using LMT03500Common.DTOs;
using LMT03500Model.ViewModel;
using Microsoft.AspNetCore.Components.Web;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

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

    private async Task OnLostFocusLookupBuilding()
    {
        var loEx = new R_Exception();

        try
        {
            var param = new LMT03500SearchTextDTO()
            {
                CPROPERTY_ID = _viewModel.Header.CPROPERTY_ID,
                CSEARCH_TEXT = _viewModel.Header.CBUILDING_ID
            };
            var loLookupViewModel = new LMT03500BuildingLookupViewModel();
            var loResult = await loLookupViewModel.GetRecord(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(LMT03500FrontResources.Resources_Dummy_Class),
                    "_ErrLookup"));
                _viewModel.Header.CBUILDING_ID= "";
                goto EndBlock;
            }

            _viewModel.Header.CBUILDING_ID = loResult.CBUILDING_ID;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
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

    private async Task OnLostFocusLookupUnit()
    {
        var loEx = new R_Exception();

        try
        {
            var param = new LMT03500SearchTextDTO()
            {
                CPROPERTY_ID = _viewModel.Header.CPROPERTY_ID,
                CBUILDING_ID = _viewModel.Header.CBUILDING_ID,
                CFLOOR_ID = "",
                CSEARCH_TEXT = _viewModel.Header.CUNIT_ID
            };
            var loLookupViewModel = new LMT03500BuildingUnitLookupViewModel();
            var loResult = await loLookupViewModel.GetRecord(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(LMT03500FrontResources.Resources_Dummy_Class),
                    "_ErrLookup"));
                _viewModel.Header.CUNIT_ID= "";
                _viewModel.Header.CUNIT_NAME= "";
                goto EndBlock;
            }

            _viewModel.Header.CUNIT_NAME = loResult.CUNIT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
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

    private async Task OnClickRefresh()
    {
        var loEx = new R_Exception();
        try
        {
            await _gridRef.R_RefreshGrid(null);
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

    private async Task GetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<LMT03500UtilityMeterDTO>(eventArgs.Data);
            await _viewModel.GetRecord(loParam);
            eventArgs.Result = _viewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
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

    private void BeforeOpenUpdate(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(LMT03500UpdateMeterPopup);
        _viewModel.Data.CUNIT_NAME = _viewModel.Header.CUNIT_NAME;
        _viewModel.Data.CTENANT_ID = _viewModel.Header.CTENANT_ID;
        _viewModel.Data.CTENANT_NAME = _viewModel.Header.CTENANT_NAME;
        eventArgs.Parameter = _viewModel.Data;
    }
    
    private void BeforeOpenChange(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(LMT03500ChangeMeterPopup);
        _viewModel.Data.CUNIT_NAME = _viewModel.Header.CUNIT_NAME;
        _viewModel.Data.CTENANT_ID = _viewModel.Header.CTENANT_ID;
        _viewModel.Data.CTENANT_NAME = _viewModel.Header.CTENANT_NAME;
        eventArgs.Parameter = _viewModel.Data;
    }
}