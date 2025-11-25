using System.Globalization;
using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using PMM04700Common.DTOs;
using PMM4700MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace PMM04700FRONT;

public partial class PMM04703PopupAdd : R_Page
{
    private R_ConductorGrid _conGridPricingRate;
    private R_Grid<PricingRateBulkSaveDTO> _gridPricingRate;
    private PMM04701ViewModel _viewModel_PricingRate = new();
    [Inject] private IClientHelper clientHelper { get; set; }
    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();
        try
        {
            //get & parse param
            var loParam = R_FrontUtility.ConvertObjectToObject<PricingRateDTO>(poParameter);

            //set param to class variable
            _viewModel_PricingRate._pricingRateDateDisplay = !string.IsNullOrWhiteSpace(loParam.CRATE_DATE) ? DateTime.ParseExact(loParam.CRATE_DATE, "yyyyMMdd", CultureInfo.InvariantCulture) : DateTime.Now;
            _viewModel_PricingRate._propertyId = loParam.CPROPERTY_ID ?? "";
            _viewModel_PricingRate._pricingRateDate = loParam.CRATE_DATE ?? "";

            await _gridPricingRate.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        R_DisplayException(loEx);

    }

    #region grid events
    private async Task PricingRateAdd_GetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await _viewModel_PricingRate.GetPricingRateAddList();

            eventArgs.ListEntityResult = _viewModel_PricingRate._pricingSaveList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private void PricingRateAdd_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        eventArgs.Result = eventArgs.Data;
    }

    #endregion

    #region form

    private async Task PricingRateAddForm_RateDateValueChangedAsync(DateTime? poDateParam)
    {
        R_Exception loEx = new R_Exception();
        try
        {
            _viewModel_PricingRate._pricingRateDateDisplay = poDateParam;
            _viewModel_PricingRate._pricingRateDate = poDateParam.Value.ToString("yyyyMMdd");
            await _gridPricingRate.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region button

    private async Task PricingRateAdd_CancelAsync()
    {
        R_Exception loEx = new();
        try
        {
            await this.Close(true, null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    private async Task PricingRateAdd_Process()
    {
        R_Exception loEx = new();
        try
        {
            await _viewModel_PricingRate.SavePricing();
            if (!loEx.HasError)
            {
                R_eMessageBoxResult r_eMessageBoxResult = await R_MessageBox.Show("", "Process Complete", R_eMessageBoxButtonType.OK);
            }
            await this.Close(true, null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    #endregion
    
    
      #region lookupCurrency

        private R_Lookup R_LookupBtnCurrency;
        private R_TextBox R_TextBoxBtnCurrency;

        private async Task Before_Open_lookupCurrency(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var param = new GSL00300ParameterDTO
            {
                CUSER_ID = clientHelper.UserId,
                CCOMPANY_ID = clientHelper.CompanyId
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00300);
        }

        private void After_Open_lookupCurrency(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loTempResult = (GSL00300DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loGetData = (PricingRateBulkSaveDTO)eventArgs.ColumnData;
            loGetData.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
        }

        //private async Task OnLostFocusCurrency()
        //{
        //    R_Exception loEx = new R_Exception();
        //    try
        //    {
        //        if (string.IsNullOrWhiteSpace(_viewModel_PricingRate.saveData.CCURRENCY_CODE))
        //        {
        //            _viewModel_PricingRate.saveData.CCURRENCY_CODE = "";
        //            return;
        //        }
        //        LookupGSL00300ViewModel loLookupViewModel = new LookupGSL00300ViewModel();
        //        var param = new GSL00300ParameterDTO
        //        {
        //            CUSER_ID = clientHelper.UserId,
        //            CCOMPANY_ID = clientHelper.CompanyId,
        //            CSEARCH_TEXT = _viewModel_PricingRate.saveData.CCURRENCY_CODE
        //        };
        //        var loResult = await loLookupViewModel.GetCurrency(param);

        //        if (loResult == null)
        //        {
        //            //await R_TextBoxBtnDept.FocusAsync();
        //            loEx.Add(R_FrontUtility.R_GetError(
        //                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
        //                    "_ErrLookup01"));
        //            _viewModel_PricingRate.saveData.CCURRENCY_CODE = "";
        //        }
        //        else
        //        {
        //            _viewModel_PricingRate.saveData.CCURRENCY_CODE = loResult.CCURRENCY_CODE;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    R_DisplayException(loEx);
        //}

        #endregion lookupCurrency
}
