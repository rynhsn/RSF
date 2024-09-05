using Microsoft.AspNetCore.Components.Web;
using PMT06500Common.DTOs;
using PMT06500Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Extensions;
using R_BlazorFrontEnd.Helpers;

namespace PMT06500Front;

public partial class PMT06500 : R_Page
{
    private PMT06500ViewModel _viewModel = new();
    private R_Conductor _conductorRefAgreement;
    private R_Grid<PMT06500AgreementDTO> _gridRefAgreement;

    private R_ConductorGrid _conductorRefOvertime;
    private R_Grid<PMT06500OvtDTO> _gridRefOvertime;

    private R_ConductorGrid _conductorRefService;
    private R_Grid<PMT06500ServiceDTO> _gridRefService;

    private R_ConductorGrid _conductorRefUnit;
    private R_Grid<PMT06500UnitDTO> _gridRefUnit;

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetPeriodList();
            await _viewModel.GetPropertyList();
            await _viewModel.GetYearRange();

            await _gridRefAgreement.R_RefreshGrid(null);
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
            if (string.IsNullOrEmpty(_viewModel.SelectedPropertyId))
            {
                loEx.Add("", _localizer["MSG_CHOOSE_PROPERTY"]);
            }

            if (string.IsNullOrEmpty(_viewModel.SelectedYear.ToString()))
            {
                loEx.Add("", _localizer["MSG_CHOOSE_YEAR_PERIOD"]);
            }

            if (string.IsNullOrEmpty(_viewModel.SelectedPeriodNo))
            {
                loEx.Add("", _localizer["MSG_CHOOSE_MONTH_PERIOD"]);
            }

            if (!loEx.HasError)
            {
                await _gridRefAgreement.R_RefreshGrid(null);
                // if (_viewModel.AgreementGridList.Count!=0)
                // {
                //     await _gridRefOvertime.R_RefreshGrid(null);
                //     await _gridRefService.R_RefreshGrid(null);
                //     await _gridRefUnit.R_RefreshGrid(null);
                // }
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void OnChangedCombo(string value, string formName)
    {
        _viewModel.OnChangedComboOnList(value, formName);
    }


    #region Agreement

    private async Task GetAgreementListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetAgreementGridList();
            eventArgs.ListEntityResult = _viewModel.AgreementGridList;

            if (_viewModel.AgreementGridList.Count == 0)
            {
                _viewModel.EntityAgreement = new PMT06500AgreementDTO();
                _viewModel.OvertimeGridList.Clear();
                _viewModel.ServiceGridList.Clear();
                _viewModel.UnitGridList.Clear();
                // _viewModel.EntityOvertime = new PMT06500OvtDTO();
            }

            // await _gridRefOvertime.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetRecordAgreement(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.EntityAgreement = R_FrontUtility.ConvertObjectToObject<PMT06500AgreementDTO>(eventArgs.Data);
            eventArgs.Result = _viewModel.EntityAgreement;
            await _gridRefOvertime.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region Overtime

    private async Task GetOvertimeListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetOvertimeGridList();
            eventArgs.ListEntityResult = _viewModel.OvertimeGridList;

            if (_viewModel.OvertimeGridList.Count == 0)
            {
                // _viewModel.EntityOvertime = new PMT06500OvtDTO();
                // _viewModel.EntityService = new PMT06500ServiceDTO();

                _viewModel.ServiceGridList.Clear();
                _viewModel.UnitGridList.Clear();
            }
            else
            {
                await DisplayOvertime(new R_DisplayEventArgs(_viewModel.EntityOvertime, R_eConductorMode.Normal));
            }
            // else
            // {
            //     await _gridRefService.R_RefreshGrid(null);
            // }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task DisplayOvertime(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.EntityOvertime = R_FrontUtility.ConvertObjectToObject<PMT06500OvtDTO>(eventArgs.Data);
            await _gridRefService.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region Service

    private async Task GetServiceListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetServiceGridList();
            eventArgs.ListEntityResult = _viewModel.ServiceGridList;

            if (_viewModel.ServiceGridList.Count == 0)
            {
                _viewModel.UnitGridList.Clear();
            }
            else
            {
                await DisplayService(new R_DisplayEventArgs(_viewModel.EntityService, R_eConductorMode.Normal));
            }
            // else
            // {
            //     await _gridRefUnit.R_RefreshGrid(null);
            // }

            // await _gridRefUnit.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task DisplayService(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.EntityService = R_FrontUtility.ConvertObjectToObject<PMT06500ServiceDTO>(eventArgs.Data);
            await _gridRefUnit.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region Unit

    private async Task GetUnitListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetUnitGridList();
            eventArgs.ListEntityResult = _viewModel.UnitGridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    private void BeforeOpenTabInvoice(R_BeforeOpenTabPageEventArgs eventArgs)
    {
        var loParam = new PMT06500InvoicePageParam
        {
            CPROPERTY_ID = _viewModel.SelectedPropertyId,
            CPERIOD = _viewModel.SelectedYear + _viewModel.SelectedPeriodNo,
            CREF_NO = _viewModel.EntityOvertime.CREF_NO,
            CDEPT_CODE = _viewModel.EntityOvertime.CDEPT_CODE,
            CLINK_DEPT_CODE = _viewModel.EntityOvertime.CLINK_DEPT_CODE,
            CLINK_TRANS_CODE = _viewModel.EntityOvertime.CLINK_TRANS_CODE,
            CACTION = "EDIT",
            OAGREEMENT = _viewModel.EntityAgreement,
        };
        eventArgs.Parameter = loParam;
        eventArgs.TargetPageType = typeof(PMT06500Invoice);
    }

    private void BeforeOpenInvoicePopup(R_BeforeOpenPopupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = new PMT06500InvoicePopupParam
            {
                EMODE = R_eConductorMode.Add,
                CLINK_DEPT_CODE = _viewModel.EntityOvertime.CLINK_DEPT_CODE,
                CLINK_TRANS_CODE = _viewModel.EntityOvertime.CLINK_TRANS_CODE,
                // CACTION = "ADD",
                OINVOICE = R_FrontUtility.ConvertObjectToObject<PMT06500InvoiceDTO>(_viewModel.EntityOvertime)
                // OINVOICE = new PMT06500InvoiceDTO
                // {
                //     CPROPERTY_ID = _viewModel.EntityOvertime.CPROPERTY_ID,
                //     CPROPERTY_NAME = _viewModel.EntityOvertime.CPROPERTY_NAME,
                //     CINV_PRD = _viewModel.EntityOvertime.CINV_PRD,
                //     CTENANT_ID = _viewModel.EntityOvertime.CTENANT_ID,
                //     CTENANT_NAME = _viewModel.EntityOvertime.CTENANT_NAME,
                //     CBUILDING_ID = _viewModel.EntityOvertime.CBUILDING_ID,
                //     CBUILDING_NAME = _viewModel.EntityOvertime.CBUILDING_NAME,
                //     CAGREEMENT_NO = _viewModel.EntityOvertime.CAGREEMENT_NO,
                // },
            };
            eventArgs.Parameter = loParam;
            // eventArgs.TargetPageType = typeof(PMT06500InvoicePopup);
            eventArgs.PageTitle = "Invoice Detail";
            eventArgs.FormAccess = R_eFormAccess.Add.ToDescription();
            eventArgs.TargetPageType = typeof(PMT06500InvoicePopup);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task AfterOpenInvoicePopup(R_AfterOpenPopupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (!eventArgs.Success) return;
            await _gridRefAgreement.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task AfterOpenTabInvoice(R_AfterOpenTabPageEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _gridRefAgreement.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private bool _enProperty = true;

    private void OnTabChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
    {
        _enProperty = eventArgs.TabStripTab.Id == nameof(PMT06500);
    }
}