using BlazorClientHelper;
using PMT01500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls;
using PMT01500Common.DTO._5._Invoice_Plan;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Controls.Events;
using PMT01500Common.Utilities.Front;

namespace PMT01500Front
{
    public partial class PMT01500InvoicePlan
    {
        private readonly PMT01500InvoicePlanViewModel _viewModelInvoicePlan = new();
        [Inject] private IClientHelper? _clientHelper { get; set; }

        private R_ConductorGrid? _conductorInvoicePlanCharge;
        private R_ConductorGrid? _conductorInvoicePlan;
        private R_Grid<PMT01500InvoicePlanChargesListDTO>? _gridInvoicePlanCharge;
        private R_Grid<PMT01500InvoicePlanListDTO>? _gridInvoicePlan;

        PMT01500EventCallBackDTO _oEventCallBack = new PMT01500EventCallBackDTO();

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                //GET PARAMETER

                //_viewModelInvoicePlan.loParameterList = (PMT01500GetHeaderParameterDTO)poParameter;
                //   _viewModelPMT01500UnitInfo_Utilities.loParameterList = (PMT01500GetHeaderParameterDTO)poParameter;

                loEx.Add("PMT01500", "Bagian/Page ini di SKIP dahulu!");

                if (!string.IsNullOrEmpty(_viewModelInvoicePlan.loParameterList.CREF_NO))
                {
                    await _viewModelInvoicePlan.GetInvoicePlanHeader();
                    await _gridInvoicePlanCharge.R_RefreshGrid(null);
                    await _gridInvoicePlan.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task GetListInvoicePlanCharge(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelInvoicePlan.GetInvoicePlanChargeList();
                eventArgs.ListEntityResult = _viewModelInvoicePlan.loListPMT01500InvoicePlanChargesList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }
        
        private async Task GetListInvoicePlan(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModelInvoicePlan.GetInvoicePlanList();
                eventArgs.ListEntityResult = _viewModelInvoicePlan.loListPMT01500InvoicePlanList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }

        private void R_DisplayInvoicePlanCharge(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                /*
                //Untuk Tab - 1
                var loConvertTabParameter = R_FrontUtility.ConvertObjectToObject<LMM02500TabParameterDTO>(eventArgs.Data);
                _loTabParameter = loConvertTabParameter;
                if (_clientHelper?.UserId != null) _loTabParameter.CUSER_LOGIN_ID = _clientHelper.UserId;
                await Task.CompletedTask;
                */

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        private void R_DisplayInvoicePlan(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                /*
                //Untuk Tab - 1
                var loConvertTabParameter = R_FrontUtility.ConvertObjectToObject<LMM02500TabParameterDTO>(eventArgs.Data);
                _loTabParameter = loConvertTabParameter;
                if (_clientHelper?.UserId != null) _loTabParameter.CUSER_LOGIN_ID = _clientHelper.UserId;
                await Task.CompletedTask;
                */

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    
    }
}
