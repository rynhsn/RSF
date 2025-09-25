using HDI00100Common.DTOs;
using HDI00100Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_HDCOMMON.DTOs.HDL00400;
using Lookup_HDFRONT;
using Lookup_HDModel.ViewModel.HDL00400;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace HDI00100Front;

public partial class HDI00100Next : R_ITabPage
{
    private HDI00100ViewModel _viewModel = new();
    private R_Conductor _conductorRef = new();
    private R_Grid<HDI00100TaskSchedulerDTO> _gridRef = new();
    public R_ComboBox<HDI00100PropertyDTO, string> _comboPropertyRef { get; set; }


    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init(poTabStatus: TabStatus.Next, (string)poParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnLostFocusBuilding(object obj)
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL02200ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.Param.CBUILDING_ID))
            {
                _viewModel.Param.CBUILDING_NAME = "";
                _viewModel.Param.CASSET_CODE = "";
                _viewModel.Param.CASSET_NAME = "";
                return;
            }

            var param = new GSL02200ParameterDTO
            {
                CPROPERTY_ID = _viewModel.Param.CPROPERTY_ID,
                CSEARCH_TEXT = _viewModel.Param.CBUILDING_ID
            };

            GSL02200DTO loResult = null;

            loResult = await loLookupViewModel.GetBuilding(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.Param.CBUILDING_ID = "";
                _viewModel.Param.CBUILDING_NAME = "";
                _viewModel.Param.CASSET_CODE = "";
                _viewModel.Param.CASSET_NAME = "";
                goto EndBlock;
            }

            _viewModel.Param.CBUILDING_ID = loResult.CBUILDING_ID;
            _viewModel.Param.CBUILDING_NAME = loResult.CBUILDING_NAME;
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
        eventArgs.Parameter = new GSL02200ParameterDTO()
        {
            CPROPERTY_ID = _viewModel.Param.CPROPERTY_ID
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

            if (_viewModel.Param.CBUILDING_ID != loTempResult.CBUILDING_ID)
            {
                _viewModel.Param.CBUILDING_ID = loTempResult.CBUILDING_ID;
                _viewModel.Param.CBUILDING_NAME = loTempResult.CBUILDING_NAME;
                _viewModel.Param.CASSET_CODE = "";
                _viewModel.Param.CASSET_NAME = "";
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private async Task OnLostFocusAsset(object obj)
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupHDL00400ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.Param.CASSET_CODE))
            {
                _viewModel.Param.CASSET_NAME = "";
                return;
            }

            var param = new HDL00400ParameterDTO
            {
                CPROPERTY_ID = _viewModel.Param.CPROPERTY_ID,
                CBUILDING_ID = _viewModel.Param.CBUILDING_ID,
                CSEARCH_TEXT_ID = _viewModel.Param.CASSET_CODE
            };

            HDL00400DTO loResult = null;

            loResult = await loLookupViewModel.GetAssetRecord(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_HDFrontResources.Resources_Dummy_Class_LookupHD),
                    "_ErrLookup01"));
                _viewModel.Param.CASSET_CODE = "";
                _viewModel.Param.CASSET_NAME = "";
                goto EndBlock;
            }

            _viewModel.Param.CASSET_CODE = loResult.CASSET_CODE;
            _viewModel.Param.CASSET_NAME = loResult.CASSET_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        R_DisplayException(loEx);
    }

    private void BeforeLookupAsset(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(HDL00400);
        eventArgs.Parameter = new HDL00400ParameterDTO()
        {
            CPROPERTY_ID = _viewModel.Param.CPROPERTY_ID,
            CBUILDING_ID = _viewModel.Param.CBUILDING_ID,
        };
    }

    private void AfterLookupAsset(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (HDL00400DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.Param.CASSET_CODE = loTempResult.CASSET_CODE;
            _viewModel.Param.CASSET_NAME = loTempResult.CASSET_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private void OnChangedBuilding(object obj)
    {
        var llValue = (bool)obj;
        if (llValue || string.IsNullOrEmpty(_viewModel.Param.CBUILDING_ID)) return;
        _viewModel.Param.CBUILDING_ID = "";
        _viewModel.Param.CBUILDING_NAME = "";
        _viewModel.Param.CASSET_CODE = "";
        _viewModel.Param.CASSET_NAME = "";
    }

    private void OnChangedAsset(object obj)
    {
        var llValue = (bool)obj;
        if (llValue || string.IsNullOrEmpty(_viewModel.Param.CASSET_CODE)) return;
        _viewModel.Param.CASSET_CODE = "";
        _viewModel.Param.CASSET_NAME = "";
    }

    private async Task ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
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

    private async Task RefreshPage()
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

        await R_DisplayExceptionAsync(loEx);
    }

    public Task RefreshTabPageAsync(object poParam)
    {
        _viewModel.Param.CPROPERTY_ID = (string)poParam;
        _viewModel.Param.CBUILDING_ID = "";
        _viewModel.Param.CBUILDING_NAME = "";
        _viewModel.Param.CASSET_CODE = "";
        _viewModel.Param.CASSET_NAME = "";
        
        _viewModel.GridList.Clear();
        
        return Task.CompletedTask;
    }
}