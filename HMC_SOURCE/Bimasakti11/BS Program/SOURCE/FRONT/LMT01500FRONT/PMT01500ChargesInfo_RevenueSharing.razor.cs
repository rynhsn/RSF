using BlazorClientHelper;
using PMT01500Common.Utilities;
using PMT01500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using PMT01500Common.DTO._4._Charges_Info;

namespace PMT01500Front
{
    public partial class PMT01500ChargesInfo_RevenueSharing
    {
        private PMT01500ChargesInfo_RevenueSharingViewModel _viewModel = new();
        [Inject] private IClientHelper? _clientHelper { get; set; }
        private R_ConductorGrid? _conductorRSS;
        private R_ConductorGrid? _conductorRMin;
        private R_Grid<PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>? _gridRSS;
        private R_Grid<PMT01500ChargesInfo_RevenueMinimumRentDTO>? _gridRMin;


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.loParameterList = R_FrontUtility.ConvertObjectToObject<PMT01500ParameterForChargesInfo_RevenueSharingDTO>(poParameter);
                if (!string.IsNullOrEmpty(_viewModel.loParameterList.CCHARGE_SEQ_NO))
                {
                    await _gridRSS.R_RefreshGrid(null);
                    await _gridRMin.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region External Function

        private async Task OnClickCloseButton()
        {
            await Close(true, false);
        }
        #endregion

        #region Revenue Minimum Rent

        private async Task GetRevenueMinimumRentList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetRevenueMinimumRentList();
                eventArgs.ListEntityResult = _viewModel.loListLPMT01500ChargesInfo_RevenueMinimumRent;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }

        #endregion

        #region Revenue Sharing Scheme

        private async Task GetRevenueSharingSchemeList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetRevenueSharingSchemeList();
                eventArgs.ListEntityResult = _viewModel.loListLPMT01500ChargesInfo_RevenueSharing;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }

        #region Master CRUD

        private void ServiceR_Display(R_DisplayEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                switch (eventArgs.ConductorMode)
                {
                    case R_eConductorMode.Edit:
                        //Focus Async
                        break;
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }


        private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>(eventArgs.Data);
                /*
                PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO loParam = new PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _documentViewModel.loParameterList.CPROPERTY_ID,
                    CDEPT_CODE = _documentViewModel.loParameterList.CDEPT_CODE,
                    CREF_NO = _documentViewModel.loParameterList.CREF_NO,
                    CTRANS_CODE = _documentViewModel.loParameterList.CTRANS_CODE,
                    CDOC_NO = loData.CDOC_NO,
                    CUSER_ID = _clientHelper.UserId
                };
                */

                await _viewModel.GetEntity(loData);
                eventArgs.Result = _viewModel.loEntityChargesInfo_RevenueSharing;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO>(eventArgs.Data);

                await _viewModel.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.loEntityChargesInfo_RevenueSharing;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO)eventArgs.Data;

                if (_viewModel.loEntityChargesInfo_RevenueSharing != null)
                    await _viewModel.ServiceDelete(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Master UtilitiesConductor

        private void AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                var loData = (PMT01500ChargesInfo_RevenueSharingSchemeOriginalDTO)eventArgs.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        #endregion

        #endregion
    }
}
