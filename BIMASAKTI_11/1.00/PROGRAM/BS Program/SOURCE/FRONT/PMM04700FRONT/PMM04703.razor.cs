using Microsoft.AspNetCore.Components;
using PMM04700Common.DTOs;
using PMM4700MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace PMM04700FRONT;

public partial class PMM04703: R_Page,R_ITabPage
{
    private PMM04701ViewModel _viewModelPricingRate = new();

    private R_Conductor _conPricingRateDate;
    private R_Grid<PricingRateDTO> _gridPricingRateDate;

    private R_Conductor _conPricingRate;
    private R_Grid<PricingRateDTO> _gridPricingRate;

    private R_Popup Popup_PricingRate;


    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();
        try
        {
            _viewModelPricingRate._propertyId = (string)poParameter;
            await Task.Delay(300);
            await (_viewModelPricingRate._propertyId != "" ? _gridPricingRateDate.R_RefreshGrid(null) : Task.CompletedTask);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        R_DisplayException(loEx);
    }

    public async Task RefreshTabPageAsync(object poParam)
    {
        R_Exception loEx = new R_Exception();
        try
        {
            _viewModelPricingRate._propertyId = (string)poParam;
            _viewModelPricingRate._pricingRateList = new();
            await Task.Delay(300);
            await (_viewModelPricingRate._propertyId != "" ? _gridPricingRateDate.R_RefreshGrid(null) : Task.CompletedTask);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    #region PricingRateDate

    private async Task PricingRateDate_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModelPricingRate.GetPricingRateDateList();
            eventArgs.ListEntityResult = _viewModelPricingRate._pricingRateDateList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);

    }

    private void PricingRateDate_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            eventArgs.Result = R_FrontUtility.ConvertObjectToObject<PricingRateDTO>(eventArgs.Data);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    private async Task PricingRateDate_ServiceDisplay(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<PricingRateDTO>(eventArgs.Data);
            _viewModelPricingRate._pricingRateDate = loParam.CRATE_DATE;
            await _gridPricingRate.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region PricingRate

    private async Task PricingRate_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModelPricingRate.GetPricingRateList();
            eventArgs.ListEntityResult = _viewModelPricingRate._pricingRateList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);

    }

    private void PricingRate_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            eventArgs.Result = R_FrontUtility.ConvertObjectToObject<PricingRateDTO>(eventArgs.Data);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region Add

    private void BeforeOpenPopup_PricingRate(R_BeforeOpenPopupEventArgs eventArgs)
    {
        var loParam = new PricingRateDTO() { CPROPERTY_ID = _viewModelPricingRate._propertyId, CRATE_DATE = _viewModelPricingRate._pricingRateDate };
        eventArgs.Parameter = loParam;
        eventArgs.TargetPageType = typeof(PMM04703PopupAdd);
    }

    private async Task AfterOpenPopup_PricingRate(R_AfterOpenPopupEventArgs eventArgs)
    {
        R_Exception loEx = new R_Exception();
        try
        {
            await _gridPricingRateDate.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    #endregion
}

