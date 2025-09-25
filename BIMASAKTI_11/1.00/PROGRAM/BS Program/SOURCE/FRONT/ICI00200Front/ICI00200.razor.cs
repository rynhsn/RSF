using ICI00200Common.DTOs;
using ICI00200Front.DTOs;
using ICI00200Model.DTOs;
using ICI00200Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace ICI00200Front;

public partial class ICI00200 : R_Page
{
    private ICI00200ViewModel _viewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<ICI00200ProductDTO> _gridRef = new();

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void BeforeCategoryLookup(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
           eventArgs.Parameter = new GSL01800DTOParameter
            {
                CPROPERTY_ID = ICI00200LookupCategoryParam.PropertyId,
                CCATEGORY_TYPE = ICI00200LookupCategoryParam.CategoryType,
            };
           eventArgs.TargetPageType = typeof(GSL01800);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task AfterCategoryLookup(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loResult = (GSL01800DTO)eventArgs.Result;
            if (loResult == null)
                return;

            _viewModel.SelectedCategoryId = loResult.CCATEGORY_ID;
            _viewModel.SelectedCategoryName = loResult.CCATEGORY_NAME;
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnLostFocusLookupCategory()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL01800ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.SelectedCategoryId))
            {
                _viewModel.SelectedCategoryId = "";
                _viewModel.SelectedCategoryName = "";
                await _gridRef.R_RefreshGrid(null);
                return;
            }

            var param = new GSL01800DTOParameter()
            {
                CPROPERTY_ID = ICI00200LookupCategoryParam.PropertyId,
                CCATEGORY_TYPE = ICI00200LookupCategoryParam.CategoryType,
                CSEARCH_TEXT = _viewModel.SelectedCategoryId
            };

            GSL01800DTO loResult = null;

            loResult = await loLookupViewModel.GetCategory(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.SelectedCategoryId = "";
                _viewModel.SelectedCategoryName = "";
                await _gridRef.R_RefreshGrid(null);
                goto EndBlock;
            }

            _viewModel.SelectedCategoryId = loResult.CCATEGORY_ID;
            _viewModel.SelectedCategoryName = loResult.CCATEGORY_NAME;
            
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        R_DisplayException(loEx);
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

    private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loData = (ICI00200ProductDTO)eventArgs.Data;
            await _viewModel.GetEntity(loData.CPRODUCT_ID);
            eventArgs.Result = _viewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void BeforeLastInfoPopup(R_BeforeOpenPopupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            eventArgs.Parameter = _viewModel.Data.CPRODUCT_ID;
            eventArgs.TargetPageType = typeof(ICI00200PopupLastInfo);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void BeforeDeptPopup(R_BeforeOpenPopupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            eventArgs.Parameter = new ICI00200PopupDeptWareParamDTO()
            {
                CCATEGORY_ID = _viewModel.Data.CCATEGORY_ID,
                CPRODUCT_ID = _viewModel.Data.CPRODUCT_ID,
                CPRODUCT_NAME = _viewModel.Data.CPRODUCT_NAME
            };
            eventArgs.TargetPageType = typeof(ICI00200PopupDept);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private void BeforeWarePopup(R_BeforeOpenPopupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            eventArgs.Parameter = new ICI00200PopupDeptWareParamDTO()
            {
                CCATEGORY_ID = _viewModel.Data.CCATEGORY_ID,
                CPRODUCT_ID = _viewModel.Data.CPRODUCT_ID,
                CPRODUCT_NAME = _viewModel.Data.CPRODUCT_NAME
            };
            eventArgs.TargetPageType = typeof(ICI00200PopupWare);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}