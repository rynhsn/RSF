using PMT06500Common.DTOs;
using PMT06500Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Extensions;
using R_BlazorFrontEnd.Helpers;

namespace PMT06500Front;

public partial class PMT06500Invoice : R_Page
{
    private PMT06500ViewModel _viewModel = new();
    private R_ConductorGrid _conductorRefInvoice;
    private R_Grid<PMT06500InvoiceDTO> _gridRefInvoice;

    private R_ConductorGrid _conductorRefSummary;
    private R_Grid<PMT06500SummaryDTO> _gridRefSummary;

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.InvoicePageParam = (PMT06500InvoicePageParam)poParam;
            _viewModel.SelectedPropertyId = _viewModel.InvoicePageParam.CPROPERTY_ID;
            _viewModel.SelectedPeriod = _viewModel.InvoicePageParam.CPERIOD;
            _viewModel.SelectedAgreementNo = _viewModel.InvoicePageParam.OAGREEMENT.CAGREEMENT_NO;

            await _gridRefInvoice.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #region Invoice

    private async Task GetInvoiceListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetInvoiceGridList();
            eventArgs.ListEntityResult = _viewModel.InvoiceGridList;

            // await _gridRefSummary.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task DisplayInvoice(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.EntityInvoice = (PMT06500InvoiceDTO)eventArgs.Data;
            // await _conductorRefInvoice.R_GetEntity(_viewModel.EntityInvoice);
            await _viewModel.GetInvoiceRecord(_viewModel.EntityInvoice);

            _viewModel.SelectedPropertyId = _viewModel.EntityInvoice.CPROPERTY_ID;
            _viewModel.SelectedPeriod = _viewModel.EntityInvoice.CINV_PRD;
            _viewModel.SelectedAgreementNo = _viewModel.EntityInvoice.CAGREEMENT_NO;
            await _gridRefSummary.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    // private void GetRecordInvoice(R_ServiceGetRecordEventArgs eventArgs)
    // {
    //     var loEx = new R_Exception();
    //
    //     try
    //     {
    //         _viewModel.EntityInvoice = R_FrontUtility.ConvertObjectToObject<PMT06500InvoiceDTO>(eventArgs.Data);
    //         eventArgs.Result = _viewModel.GetInvoiceRecord(_viewModel.EntityInvoice);
    //
    //         // await _gridRefSummary.R_RefreshGrid(null);
    //     }
    //     catch (Exception ex)
    //     {
    //         loEx.Add(ex);
    //     }
    //
    //     loEx.ThrowExceptionIfErrors();
    // }

    private async Task DeleteInvoice()
    {
        var loEx = new R_Exception();

        try
        {
            if ((_viewModel.InvoiceGridList.Count > 0) && (_viewModel.EntityInvoice != null))
            {
                var leMsg = await R_MessageBox.Show("", _localizer["MSG_BEFORE_DELETE"],
                    R_eMessageBoxButtonType.YesNo);
                if (leMsg == R_eMessageBoxResult.Yes)
                {
                    var loParam = _viewModel.EntityInvoice;
                    await _viewModel.DeleteEntity(loParam);
                    await _gridRefInvoice.R_RefreshGrid(null);
                    var leMsg2 = await R_MessageBox.Show("", _localizer["MSG_AFTER_DELETE"],
                        R_eMessageBoxButtonType.OK);
                }
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region Summary

    private async Task GetSummaryListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetSummaryGridList(_viewModel.EntityInvoice.CREF_NO, _viewModel.EntityInvoice.CDEPT_CODE,
                _viewModel.EntityInvoice.CLINK_DEPT_CODE, _viewModel.EntityInvoice.CLINK_TRANS_CODE,
                _viewModel.InvoicePageParam.CACTION);
            eventArgs.ListEntityResult = _viewModel.SummaryGridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    private void BeforeOpenPopupEditInvoice(R_BeforeOpenPopupEventArgs eventArgs)
    {
        var loParam = new PMT06500InvoicePopupParam
        {
            EMODE = R_eConductorMode.Edit,
            CREF_NO = _viewModel.InvoicePageParam.CREF_NO,
            CDEPT_CODE = _viewModel.InvoicePageParam.CDEPT_CODE,
            CLINK_DEPT_CODE = _viewModel.InvoicePageParam.CLINK_DEPT_CODE,
            CLINK_TRANS_CODE = _viewModel.InvoicePageParam.CLINK_TRANS_CODE,
            CACTION = "EDIT",
            OINVOICE = _viewModel.EntityInvoice
        };
        eventArgs.Parameter = loParam;
        eventArgs.PageTitle = "Invoice Detail";
        eventArgs.FormAccess = R_eFormAccess.Update.ToDescription();
        eventArgs.TargetPageType = typeof(PMT06500InvoicePopup);
    }

    private async Task AfterOpenPopupEditInvoice(R_AfterOpenPopupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (!eventArgs.Success) return;
            await _gridRefInvoice.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void BeforeOpenPopupViewInvoice(R_BeforeOpenPopupEventArgs eventArgs)
    {
        var loParam = new PMT06500InvoicePopupParam
        {
            EMODE = R_eConductorMode.Normal,
            CREF_NO = _viewModel.InvoicePageParam.CREF_NO,
            CDEPT_CODE = _viewModel.InvoicePageParam.CDEPT_CODE,
            CLINK_DEPT_CODE = _viewModel.InvoicePageParam.CLINK_DEPT_CODE,
            CLINK_TRANS_CODE = _viewModel.InvoicePageParam.CLINK_TRANS_CODE,
            CACTION = "EDIT",
            OINVOICE = _viewModel.EntityInvoice
        };
        eventArgs.Parameter = loParam;
        eventArgs.FormAccess = R_eFormAccess.View.ToDescription();
        eventArgs.PageTitle = "Invoice Detail";
        eventArgs.TargetPageType = typeof(PMT06500InvoicePopup);
    }

    private async Task OnClickSubmit()
    {
        var loEx = new R_Exception();

        try
        {
            var leMsg = await R_MessageBox.Show("",
                _viewModel.EntityInvoice.CTRANS_STATUS switch
                {
                    "00" => _localizer["MSG_BEFORE_SUBMIT"],
                    "10" => _localizer["MSG_BEFORE_REDRAFT"],
                    _ => ""
                }, R_eMessageBoxButtonType.YesNo);

            if (leMsg == R_eMessageBoxResult.No)
            {
                return;
            }

            await _viewModel.ProcessSubmit();

            var leMsgAfter = await R_MessageBox.Show("",
                _viewModel.EntityInvoice.CTRANS_STATUS switch
                {
                    "00" => _localizer["MSG_AFTER_SUBMIT"],
                    "10" => _localizer["MSG_AFTER_REDRAFT"],
                    _ => ""
                });

            await _gridRefInvoice.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}