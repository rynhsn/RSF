using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System.Xml.Linq;
using R_BlazorFrontEnd.Interfaces;
using PMT00500COMMON;
using PMT00500MODEL;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00500;
using Lookup_PMModel.ViewModel.LML01000;
using Lookup_PMModel.ViewModel.LML01100;
using R_BlazorFrontEnd.Enums;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMModel.ViewModel.LML00200;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls.MessageBox;
using System.Globalization;

namespace PMT00500FRONT
{
    public partial class PMT00550 : R_Page, R_ITabPage
    {
        #region ViewModel
        private PMT00550ViewModel _viewModel = new PMT00550ViewModel();
        private PMT00510ViewModel _viewModelDetail = new PMT00510ViewModel();
        #endregion

        #region Conductor
        private R_ConductorGrid _conductorHeaderRef;
        private R_Conductor _conductorRef;
        #endregion

        #region Grid
        private R_Grid<PMT00551DTO> _gridLOIChargesListRef;
        private R_Grid<PMT00550DTO> _gridInvoiceListRef;
        #endregion

        #region Inject
        [Inject] private R_ILocalizer<PMT00500FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
        #endregion

        #region Private Property
        private bool IsAddDataLOI = false;
        private bool EnableHasHeaderData;
        private PMT00551DTO _SelectedCharges = new PMT00551DTO();
        #endregion

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT00500LOIParameterInvoicePlanDTO)poParameter;
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT00500DTO>(poParameter);
                var loHeaderData = await _viewModelDetail.GetLOIWithResult(loParam);
                loHeaderData.CUNIT_ID = loData.CUNIT_ID;
                loHeaderData.CUNIT_NAME = loData.CUNIT_NAME;
                loHeaderData.CFLOOR_ID = loData.CFLOOR_ID;
                _viewModel.LOI = loHeaderData;

                await _gridLOIChargesListRef.R_RefreshGrid(loData);
                if (!string.IsNullOrWhiteSpace(loData.SELECTED_DATA_TAB_CHARGES.CCHARGES_ID) && _viewModel.LOIInvoicePlanChargesGrid.Count > 0)
                {
                    var loSelectedChargesData = _viewModel.LOIInvoicePlanChargesGrid.FirstOrDefault(x => x.CCHARGES_ID == loData.SELECTED_DATA_TAB_CHARGES.CCHARGES_ID);
                    await _gridLOIChargesListRef.R_SelectCurrentDataAsync(loSelectedChargesData);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Tab Refresh
        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT00500LOIParameterInvoicePlanDTO)poParam;
                if (string.IsNullOrWhiteSpace(loData.CREF_NO) ==  false && string.IsNullOrWhiteSpace(loData.CUNIT_ID) == false)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<PMT00500DTO>(poParam);
                    var loHeaderData = await _viewModelDetail.GetLOIWithResult(loParam);
                    loHeaderData.CUNIT_ID = loData.CUNIT_ID;
                    loHeaderData.CUNIT_NAME = loData.CUNIT_NAME;
                    _viewModel.LOI = loHeaderData;

                    await _gridLOIChargesListRef.R_RefreshGrid(loData);
                }
                else
                {
                    _viewModel.LOI = new PMT00500DTO();
                    if (_gridLOIChargesListRef.DataSource.Count > 0)
                    {
                        _gridLOIChargesListRef.DataSource.Clear();
                    }
                    if (_gridInvoiceListRef.DataSource.Count > 0)
                    {
                        _gridInvoiceListRef.DataSource.Clear();
                    }
                    _viewModel.InvoiceSeqNo = 0;
                    _viewModel.TotalSeqNo = 0;
                    _viewModel.PaidSeqNo = 0;
                    _viewModel.InvoiceAmount = 0;
                    _viewModel.TotalAmount = 0;
                    _viewModel.PaidAmount = 0;
                    _viewModel.Data.CDESCRIPTION = "";
                }

                EnableHasHeaderData = string.IsNullOrWhiteSpace(loData.CREF_NO) == false && string.IsNullOrWhiteSpace(loData.CFLOOR_ID) == false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region Charges 
        private async Task Charges_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParameter = R_FrontUtility.ConvertObjectToObject<PMT00551DTO>(eventArgs.Parameter);
                await _viewModel.GetLOIInvPlanChargeList(loParameter);

                eventArgs.ListEntityResult = _viewModel.LOIInvoicePlanChargesGrid;
                if (_viewModel.LOIInvoicePlanChargesGrid.Count <= 0)
                {
                    if (_gridInvoiceListRef.DataSource.Count > 0)
                    {
                        _gridInvoiceListRef.DataSource.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void Charges_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private async Task Charges_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loData = (PMT00551DTO)eventArgs.Data;
                    if (DateTime.TryParseExact(loData.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldStartDate))
                    {
                        loData.DSTART_DATE = ldStartDate;
                    }
                    else
                    {
                        loData.DSTART_DATE = null;
                    }
                    if (DateTime.TryParseExact(loData.CEND_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldEndDate))
                    {
                        loData.DEND_DATE = ldEndDate;
                    }
                    else
                    {
                        loData.DEND_DATE = null;
                    }
                    _SelectedCharges = loData;
                    await _gridInvoiceListRef.R_RefreshGrid(eventArgs.Data);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region InvoicePlan
        private async Task InvoicePlan_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParameter = R_FrontUtility.ConvertObjectToObject<PMT00550DTO>(eventArgs.Parameter);
                await _viewModel.GetLOIInvoicePlanList(loParameter);

                eventArgs.ListEntityResult = _viewModel.LOIInvoicePlanGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task InvoicePlan_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
            await Task.CompletedTask;
        }
        #endregion
    }
}
