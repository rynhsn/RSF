using GSM02500COMMON.DTOs.GSM02500;
using GSM02500COMMON.DTOs.GSM02531;
using GSM02500FRONT;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using PMT03500Common.DTOs;
using PMT03500Model.ViewModel;
using Microsoft.AspNetCore.Components.Web;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace PMT03500Front;

public partial class PMT03500UpdateMeter : R_ITabPage
{
    private PMT03500UpdateMeterViewModel _viewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<PMT03500UtilityMeterDTO> _gridRef = new();

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

        var loLookupViewModel = new LookupGSL02200ViewModel();
        try
        {

            if (_viewModel.Header.CBUILDING_ID == null || _viewModel.Header.CBUILDING_ID.Trim().Length <= 0) return;
            
            var param = new GSL02200ParameterDTO
            {
                CPROPERTY_ID = _viewModel.Header.CPROPERTY_ID,
                CSEARCH_TEXT = _viewModel.Header.CBUILDING_ID,
                LAGREEMENT = true
            };

            GSL02200DTO loResult = null;

            loResult = await loLookupViewModel.GetBuilding(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.Header.CBUILDING_ID = "";
                goto EndBlock;
            }

            _viewModel.Header.CBUILDING_ID = loResult.CBUILDING_ID;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        R_DisplayException(loEx);
    }

    private void BeforeLookupBuilding(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(GSL02200);
        eventArgs.Parameter = new GSL02200ParameterDTO
        {
            CPROPERTY_ID = _viewModel.Header.CPROPERTY_ID,
            LAGREEMENT = true
        };
    }

    private void AfterLookupBuilding(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GSL02200DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.Header.CBUILDING_ID = loTempResult.CBUILDING_ID;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnLostFocusLookupUnit()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL02300ViewModel();
        try
        {
            if (_viewModel.Header.CUNIT_ID == null || _viewModel.Header.CUNIT_ID.Trim().Length <= 0)
            {
                _viewModel.Header.CUNIT_NAME = "";
                return;
            }

            var param = new GSL02300ParameterDTO
            {
                CPROPERTY_ID = _viewModel.Header.CPROPERTY_ID,
                CBUILDING_ID = _viewModel.Header.CBUILDING_ID,
                LAGREEMENT = true,
                CFLOOR_ID = "",
                CSEARCH_TEXT = _viewModel.Header.CUNIT_ID
            };

            GSL02300DTO loResult = null;

            loResult = await loLookupViewModel.GetBuildingUnit(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.Header.CUNIT_ID = "";
                _viewModel.Header.CUNIT_NAME = "";
                _viewModel.Header.CREF_NO = "";
                _viewModel.Header.CTENANT_ID = "";
                _viewModel.Header.CTENANT_NAME = "";
                goto EndBlock;
            }

            _viewModel.Header.CUNIT_ID = loResult.CUNIT_ID;
            _viewModel.Header.CUNIT_NAME = loResult.CUNIT_NAME;            
            await OnChangeHeader();

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        R_DisplayException(loEx);
    }

    private void BeforeLookupBuildingUnit(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(GSL02300);
        eventArgs.Parameter = new GSL02300ParameterDTO
        {
            CPROPERTY_ID = _viewModel.Header.CPROPERTY_ID,
            CBUILDING_ID = _viewModel.Header.CBUILDING_ID,
            LAGREEMENT = true
        };
        
    }

    private async Task AfterLookupBuildingUnit(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GSL02300DTO)eventArgs.Result;
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
            await _viewModel.GetList();
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
            var loParam = R_FrontUtility.ConvertObjectToObject<PMT03500UtilityMeterDTO>(eventArgs.Data);
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
            await _viewModel.GetAgreementTenant(_viewModel.Header);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }

    public async Task RefreshTabPageAsync(object poParam)
    {
        await _viewModel.Init(poParam);
        _viewModel.Header.CBUILDING_ID = "";
        _viewModel.Header.CUNIT_ID = "";
        _viewModel.Header.CUNIT_NAME = "";
        _viewModel.Header.CREF_NO = "";
        _viewModel.Header.CTENANT_ID = "";
        _viewModel.Header.CTENANT_NAME = "";
    }

    private async Task BeforeOpenAddMeterNo(R_BeforeOpenDetailEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            // eventArgs.Parameter = _depositViewModel._currentDataAgreement;
            eventArgs.Parameter = new UploadUnitUtilityParameterDTO
            {
                PropertyData = new SelectedPropertyDTO
                {
                    CPROPERTY_ID = _viewModel.Header.CPROPERTY_ID,
                },
                BuildingData = new SelectedBuildingDTO
                {
                    CBUILDING_ID = _viewModel.Header.CBUILDING_ID
                },
                UnitData = new SelectedUnitDTO
                {
                  CUNIT_ID  = _viewModel.Header.CUNIT_ID,
                  CUNIT_NAME = _viewModel.Header.CUNIT_NAME
                },
                FloorData = new SelectedFloorDTO
                {
                    CFLOOR_ID = _viewModel.Header.CFLOOR_ID
                },
                SelectedUtilityTypeId = _viewModel.Entity.CCHARGES_TYPE
            };
            eventArgs.TargetPageType = typeof(GSM02531);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenUpdate(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(PMT03500UpdateMeterPopup);
        _viewModel.Data.CUNIT_NAME = _viewModel.Header.CUNIT_NAME;
        _viewModel.Data.CTENANT_ID = _viewModel.Header.CTENANT_ID;
        _viewModel.Data.CTENANT_NAME = _viewModel.Header.CTENANT_NAME;
        eventArgs.Parameter = _viewModel.Data;
    }

    private void BeforeOpenChange(R_BeforeOpenPopupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(PMT03500ChangeMeterPopup);
        _viewModel.Data.CBUILDING_ID = _viewModel.Header.CBUILDING_ID;
        _viewModel.Data.CUNIT_NAME = _viewModel.Header.CUNIT_NAME;
        _viewModel.Data.CTENANT_ID = _viewModel.Header.CTENANT_ID;
        _viewModel.Data.CTENANT_NAME = _viewModel.Header.CTENANT_NAME;
        eventArgs.Parameter = _viewModel.Data;
    }

    private async Task AfterOpenUpdate(R_AfterOpenPopupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            //fungsi ini menunggu RnD 
            // if (eventArgs.Success) await _conductorRef.R_SetCurrentData((PMT03500UtilityMeterDetailDTO)eventArgs.Result);
            if (eventArgs.Success) await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }

    private async Task AfterOpenChange(R_AfterOpenPopupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            if (eventArgs.Success) await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        
        loEx.ThrowExceptionIfErrors();
    }
}