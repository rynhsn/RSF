using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components.Web;
using PMT06500Common.DTOs;
using PMT06500Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace PMT06500Front;

public partial class PMT06500InvoicePopup : R_Page
{
    private PMT06500InvoiceViewModel _viewModel = new();
    private R_Conductor _conductorRefInvoice;

    private R_ConductorGrid _conductorRefSummary;
    private R_Grid<PMT06500SummaryDTO> _gridRefSummary;
    private bool _btnCancel = true;
    private bool _txtRefNo = false;

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.InvoicePopupParam = (PMT06500InvoicePopupParam)poParam;
            _viewModel.SelectedPropertyId = _viewModel.InvoicePopupParam.oInvoice.CPROPERTY_ID;
            _viewModel.SelectedPeriod = _viewModel.InvoicePopupParam.oInvoice.CINV_PRD;
            _viewModel.SelectedAgreementNo = _viewModel.InvoicePopupParam.oInvoice.CAGREEMENT_NO;

            await _viewModel.GetTransCode();
            _txtRefNo = !_viewModel.TransCode.LINCREMENT_FLAG;

            await _gridRefSummary.R_RefreshGrid(null);

            switch (_viewModel.InvoicePopupParam.eMode)
            {
                case R_eConductorMode.Add:
                    await _conductorRefInvoice.Add();
                    break;
                case R_eConductorMode.Edit:
                    await _conductorRefInvoice.Edit();
                    break;
                case R_eConductorMode.Normal:
                    await _conductorRefInvoice.R_GetEntity(_viewModel.InvoicePopupParam.oInvoice);
                    _btnCancel = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetRecordInvoice(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loEntity = R_FrontUtility.ConvertObjectToObject<PMT06500InvoiceDTO>(eventArgs.Data);
            await _viewModel.GetEntity(loEntity);
            eventArgs.Result = _viewModel.EntityInvoice;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #region Lookup Dept

    private async Task OnLostFocusDept()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL00710ViewModel();
        try
        {
            if (_viewModel.Data.CDEPT_CODE == null || _viewModel.Data.CDEPT_CODE.Trim().Length <= 0)
            {
                _viewModel.Data.CDEPT_NAME = "";
                return;
            }

            var param = new GSL00710ParameterDTO
            {
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                CSEARCH_TEXT = _viewModel.Data.CDEPT_CODE
            };

            var loResult = await loLookupViewModel.GetDepartmentProperty(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.Data.CDEPT_CODE = "";
                _viewModel.Data.CDEPT_NAME = "";
                goto EndBlock;
            }

            _viewModel.Data.CDEPT_CODE = loResult.CDEPT_CODE;
            _viewModel.Data.CDEPT_NAME = loResult.CDEPT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupDept(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParameter = new GSL00710ParameterDTO
            {
                CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID
            };

            eventArgs.Parameter = loParameter;
            eventArgs.TargetPageType = typeof(GSL00710);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterOpenLookupDept(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            var loTempResult = (GSL00710DTO)eventArgs.Result;
            _viewModel.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;
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
            await _viewModel.GetSummaryGridList();
            eventArgs.ListEntityResult = _viewModel.SummaryGridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    private void AfterAddInvoice(R_AfterAddEventArgs eventArgs)
    {
        var loEntity = (PMT06500InvoiceDTO)eventArgs.Data;
        loEntity.CPROPERTY_ID = _viewModel.InvoicePopupParam.oInvoice.CPROPERTY_ID;
        loEntity.CTENANT_ID = _viewModel.InvoicePopupParam.oInvoice.CTENANT_ID;
        loEntity.CBUILDING_ID = _viewModel.InvoicePopupParam.oInvoice.CBUILDING_ID;
        loEntity.CINV_PRD = _viewModel.InvoicePopupParam.oInvoice.CINV_PRD;
        loEntity.CAGREEMENT_NO = _viewModel.InvoicePopupParam.oInvoice.CAGREEMENT_NO;
        loEntity.DREF_DATE = DateTime.Today;
    }

    private void ValidateInvoice(R_ValidationEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loEntity = (PMT06500InvoiceDTO)eventArgs.Data;
            if (string.IsNullOrEmpty(loEntity.CDEPT_CODE))
            {
                loEx.Add("", _localizer["MSG_SELECT_DEPT"]);
            }

            if (!_viewModel.TransCode.LINCREMENT_FLAG)
            {
                if (string.IsNullOrEmpty(loEntity.CREF_NO))
                {
                    loEx.Add("", _localizer["MSG_INPUT_REF_NO"]);
                }
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task ServiceSaveInvoice(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loEntity = (PMT06500InvoiceDTO)eventArgs.Data;
            await _viewModel.SaveEntity(loEntity, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _viewModel.EntityInvoice;

            await this.Close(true, _viewModel.EntityInvoice);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private async Task OnClickCancel(MouseEventArgs eventArgs)
    {
        var leMsg =
            await R_MessageBox.Show("", _localizer["MSG_BEFORE_CANCEL"], R_eMessageBoxButtonType.YesNo);
        if (leMsg == R_eMessageBoxResult.Yes)
        {
            await this.Close(false, false);
        }
    }
}