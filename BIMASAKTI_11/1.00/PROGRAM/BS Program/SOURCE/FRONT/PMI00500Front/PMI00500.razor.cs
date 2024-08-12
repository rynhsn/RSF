using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using PMB03000Common.DTOs;
using PMB03000Front;
using PMI00500Common.DTOs;
using PMI00500Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace PMI00500Front;

public partial class PMI00500 : R_Page
{
    private PMI00500ViewModel _viewModel = new();
    private R_Conductor _conductorRefHeader;
    private R_Grid<PMI00500HeaderDTO> _gridRefHeader = new();
    private R_ConductorGrid _conductorRefAgreement;
    private R_Grid<PMI00500DTAgreementDTO> _gridRefAgreement = new();
    private R_ConductorGrid _conductorRefReminder;
    private R_Grid<PMI00500DTReminderDTO> _gridRefReminder = new();
    private R_ConductorGrid _conductorRefInvoice;
    private R_Grid<PMI00500DTInvoiceDTO> _gridRefInvoice = new();

    protected override async Task R_Init_From_Master(object eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetPropertyList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void _clearAll()
    {
        _viewModel.CDEPT_CODE = "";
        _viewModel.CDEPT_NAME = "";
        // _viewModel.HeaderList.Clear();
        // _viewModel.AgreementList.Clear();
        // _viewModel.ReminderList.Clear();
        // _viewModel.InvoiceList.Clear();
    }

    private async Task OnChangeProperty(string? value)
    {
        var loEx = new R_Exception();

        try
        {
            if (value == null || value.Trim().Length <= 0) return;
            _viewModel.CPROPERTY_ID = value;
            _clearAll();
            await _gridRefHeader.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task OnLostFocusDept(object eventArgs)
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL00710ViewModel();
        try
        {
            if (_viewModel.CDEPT_CODE == null ||
                _viewModel.CDEPT_CODE.Trim().Length <= 0)
            {
                _clearAll();
                await _gridRefHeader.R_RefreshGrid(null);
                await _gridRefAgreement.R_RefreshGrid(null);
                await _gridRefReminder.R_RefreshGrid(null);
                return;
            }

            var param = new GSL00710ParameterDTO
            {
                CPROPERTY_ID = _viewModel.CPROPERTY_ID,
                CSEARCH_TEXT = _viewModel.CDEPT_CODE
            };

            GSL00710DTO loResult = null;

            loResult = await loLookupViewModel.GetDepartmentProperty(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _clearAll();
                await _gridRefHeader.R_RefreshGrid(null);
                await _gridRefAgreement.R_RefreshGrid(null);
                await _gridRefReminder.R_RefreshGrid(null);
                goto EndBlock;
            }

            _viewModel.CDEPT_CODE = loResult.CDEPT_CODE;
            _viewModel.CDEPT_NAME = loResult.CDEPT_NAME;
            await _gridRefHeader.R_RefreshGrid(null);
            await _gridRefAgreement.R_RefreshGrid(null);
            await _gridRefReminder.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        R_DisplayException(loEx);
    }

    private void BeforeLookupDept(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(GSL00710);
        eventArgs.Parameter = new GSL00710ParameterDTO
        {
            CPROPERTY_ID = _viewModel.CPROPERTY_ID
        };
    }

    private async Task AfterLookupDept(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GSL00710DTO)eventArgs.Result;
            if (loTempResult == null)
                return;

            _viewModel.CDEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel.CDEPT_NAME = loTempResult.CDEPT_NAME;
            await _gridRefHeader.R_RefreshGrid(null);
            await _gridRefAgreement.R_RefreshGrid(null);
            await _gridRefReminder.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetHeaderListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await _viewModel.GetHeaderList();
            eventArgs.ListEntityResult = _viewModel.HeaderList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetHeaderRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            _viewModel.HeaderEntity = (PMI00500HeaderDTO)eventArgs.Data;
            eventArgs.Result = _viewModel.HeaderEntity;
            _viewModel.CTENANT_ID = _viewModel.HeaderEntity.CTENANT_ID;
            await _gridRefAgreement.R_RefreshGrid(null);
            // await _gridRefReminder.R_RefreshGrid(null);
            // await _gridRefInvoice.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetDTAgreementListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await _viewModel.GetDTAgreementList();
            eventArgs.ListEntityResult = _viewModel.AgreementList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task DisplayDTAgreement(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            _viewModel.AgreementEntity = (PMI00500DTAgreementDTO)eventArgs.Data;
            _viewModel.CAGREEMENT_NO = _viewModel.AgreementEntity.CAGREEMENT_NO;
            await _gridRefReminder.R_RefreshGrid(null);
            // await _gridRefInvoice.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetDTReminderListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await _viewModel.GetDTReminderList();
            eventArgs.ListEntityResult = _viewModel.ReminderList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task DisplayDTReminder(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loData = (PMI00500DTReminderDTO)eventArgs.Data;
            _viewModel.CREMINDER_NO = loData.CREMINDER_NO;
            await _gridRefInvoice.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task GetDTInvoiceListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await _viewModel.GetDTInvoiceList();
            eventArgs.ListEntityResult = _viewModel.InvoiceList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void BeforeOpenDetail(R_BeforeOpenDetailEventArgs eventArgs)
    {
        var loParam = new PMB03000PageParameterDTO
        {
            CTENANT_ID = _viewModel.HeaderEntity.CTENANT_ID,
            CBUILDING_ID = _viewModel.AgreementEntity.CBUILDING_ID,
            ISCALLER = true
        };
        eventArgs.Parameter = loParam;
        eventArgs.TargetPageType = typeof(PMB03000);
    }
}