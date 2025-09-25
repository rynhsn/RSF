using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Interfaces;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Popup;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd.Helpers;
using PMT01300COMMON;
using PMT01300MODEL;
using R_LockingFront;

namespace PMT01300FRONT
{
    public partial class PMT01331 : R_Page
    {
        #region Inject
        [Inject] private R_ILocalizer<PMT01300FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
        #endregion

        private PMT01331ViewModel _viewModel = new PMT01331ViewModel();

        #region Conductor
        private R_ConductorGrid _conductorRef;
        private R_ConductorGrid _conductorHeaderRef;
        private R_ConductorGrid _conductorDetail1Ref;
        private R_ConductorGrid _conductorDetail2Ref;
        #endregion

        #region Grid
        private R_Grid<PMT01331DTO> _gridRevenueSharingHDRef;
        private R_Grid<PMT01332DTO> _gridRevenueSharingRef;
        private R_Grid<PMT01333DTO> _gridRevenueMintRentRef;
        private R_Grid<PMT01332DTO> _gridRevenueSharingDisplayOnlyRef;
        private R_Grid<PMT01333DTO> _gridRevenueMintRentDisplayOnlyRef;
        #endregion

        #region Private Property
        private bool EnableRevenueSharingHD = true;
        private bool EnableRevenueSharing = true;
        private bool EnableRevenueMintRent = true;
        #endregion
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = (PMT01330DTO)poParameter;
                _viewModel.LOI_Charges = loData;

                await _gridRevenueSharingHDRef.R_RefreshGrid(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #region Locking
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_MODULE_NAME = "PM";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (PMT01331DTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMT01331",
                        Table_Name = "PMT_AGREEMENT_REVENUE_SHARING_HD",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO, loData.CCHARGE_SEQ_NO, loData.CREVENUE_SHARING_ID)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMT01331",
                        Table_Name = "PMT_AGREEMENT_REVENUE_SHARING_HD",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO, loData.CCHARGE_SEQ_NO, loData.CREVENUE_SHARING_ID)
                    };

                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }
        #endregion

        protected override async Task R_PageClosing(R_PageClosingEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                eventArgs.Cancel = EnableRevenueSharingHD == false || EnableRevenueSharing == false || EnableRevenueMintRent == false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        #region Revenue Sharing HD
        private async Task RevenueSharingHD_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParameter = R_FrontUtility.ConvertObjectToObject<PMT01331DTO>(eventArgs.Parameter);
                await _viewModel.GetRevenueSharingHDList(loParameter);

                eventArgs.ListEntityResult = _viewModel.RevenueSharingHDGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RevenueSharingHD_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.GetRevenueSharingHD((PMT01331DTO)eventArgs.Data);
                eventArgs.Result = _viewModel.RevenueSharingHD;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RevenueSharingHD_Display(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loData =  (PMT01331DTO)eventArgs.Data;
                    if (loData != null)
                    {
                        if (string.IsNullOrWhiteSpace(loData.CREVENUE_SHARING_ID) == false)
                        {
                            if (_viewModel.LOI_Charges.LIS_CLOSE_STATUS == true)
                            {
                                await _gridRevenueSharingRef.R_RefreshGrid(loData);
                                await _gridRevenueMintRentRef.R_RefreshGrid(loData);
                            }
                            else
                            {
                                await _gridRevenueSharingDisplayOnlyRef.R_RefreshGrid(loData);
                                await _gridRevenueMintRentDisplayOnlyRef.R_RefreshGrid(loData);
                            }
                        }
                        else
                        {
                            if (_gridRevenueSharingRef.DataSource.Count > 0)
                                _gridRevenueSharingRef.DataSource.Clear();
                            if (_gridRevenueMintRentRef.DataSource.Count > 0)
                                _gridRevenueMintRentRef.DataSource.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void RevenueSharingHD_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                //bool lCancel;
                //PMT01310DTO loData = (PMT01310DTO)eventArgs.Data;

                //lCancel = string.IsNullOrWhiteSpace(loData.CADD_DEPT_CODE);
                //if (lCancel)
                //{
                //    loEx.Add(R_FrontUtility.R_GetError(
                //        typeof(PMT01300FrontResources.Resources_Dummy_Class),
                //        "V042"));
                //}

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            eventArgs.Cancel = loEx.HasError;
        }
        private async Task RevenueSharingHD_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loData = (PMT01331DTO)eventArgs.Data;
                await _viewModel.SaveRevenueSharingHD(loData, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.RevenueSharingHD;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RevenueSharingHD_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.DeleteRevenueSharingHD((PMT01331DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void RevenueSharingHD_SetOther(R_SetEventArgs eventArgs)
        {
            EnableRevenueSharingHD = eventArgs.Enable;
        }
        private void RevenueSharingHD_CheckGridAdd(R_CheckGridEventArgs eventArgs)
        {
            eventArgs.Allow = _viewModel.LOI_Charges.LIS_CLOSE_STATUS;
        }
        private void RevenueSharingHD_CheckGridDelete(R_CheckGridEventArgs eventArgs)
        {
            eventArgs.Allow = _viewModel.LOI_Charges.LIS_CLOSE_STATUS;
        }
        private void RevenueSharingHD_CheckGridEdit(R_CheckGridEventArgs eventArgs)
        {
            eventArgs.Allow = _viewModel.LOI_Charges.LIS_CLOSE_STATUS;
        }
        #endregion

        #region Revenue Sharing 
        private async Task RevenueSharing_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParameter = R_FrontUtility.ConvertObjectToObject<PMT01332DTO>(eventArgs.Parameter);
                await _viewModel.GetRevenueSharingList(loParameter);
                eventArgs.ListEntityResult = _viewModel.RevenueSharingGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RevenueSharing_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.GetRevenueSharing((PMT01332DTO)eventArgs.Data);
                eventArgs.Result = _viewModel.RevenueSharing;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void RevenueSharing_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loData = (PMT01332DTO)eventArgs.Data;
            var loHeaderData = _gridRevenueSharingHDRef.CurrentSelectedData;
            loData.CREVENUE_SHARING_ID = loHeaderData.CREVENUE_SHARING_ID;
        }
        private void RevenueSharing_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            decimal liMaxVal;
            try
            {
                bool lCancel;
                PMT01332DTO loData = (PMT01332DTO)eventArgs.Data;
                var loListInt = _gridRevenueSharingRef.DataSource.Select(x => x.NMONTHLY_REVENUE_TO).ToList();

                if (loListInt.Count > 0)
                {
                    decimal loMaxVal = loListInt.Max();
                    string decimalPart = loMaxVal.ToString().Split('.')[1]; // Get the part after the decimal point
                    int decimalPlaces = decimalPart.Length;
                    if(decimalPart.Length > 1)
                    {
                        liMaxVal = loMaxVal;
                    }
                    else
                    {
                        liMaxVal = Math.Round(loMaxVal, 2);
                    }
                     

                    lCancel = loData.NMONTHLY_REVENUE_FROM < liMaxVal;
                    if (lCancel)
                    {
                        
                        loEx.Add("V030", string.Format(_localizer["V030"], liMaxVal));
                    }
                }

                lCancel = loData.NMONTHLY_REVENUE_FROM > loData.NMONTHLY_REVENUE_TO;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT01300FrontResources.Resources_Dummy_Class),
                        "V028"));
                }


            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            eventArgs.Cancel = loEx.HasError;
        }
        private async Task RevenueSharing_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loData = (PMT01332DTO)eventArgs.Data;
                await _viewModel.SaveRevenueSharing(loData, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.RevenueSharing;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RevenueSharing_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.DeleteRevenueSharing((PMT01332DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void RevenueSharing_SetOther(R_SetEventArgs eventArgs)
        {
            EnableRevenueSharing = eventArgs.Enable;
        }
        #endregion

        #region Revenue Mint Rent 
        private async Task RevenueMintRent_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParameter = R_FrontUtility.ConvertObjectToObject<PMT01333DTO>(eventArgs.Parameter);
                await _viewModel.GetRevenueMintRentList(loParameter);
                eventArgs.ListEntityResult = _viewModel.RevenueMintRentGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RevenueMintRent_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.GetRevenueMintRent((PMT01333DTO)eventArgs.Data);
                eventArgs.Result = _viewModel.RevenueMintRent;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task RevenueMintRent_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loData = (PMT01333DTO)eventArgs.Data;
                var loHeaderData = _gridRevenueSharingHDRef.CurrentSelectedData;
                loData.CREVENUE_SHARING_ID = loHeaderData.CREVENUE_SHARING_ID;
                await _viewModel.SaveRevenueMintRent(loData, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.RevenueMintRent;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void RevenueMintRent_SetOther(R_SetEventArgs eventArgs)
        {
            EnableRevenueMintRent = eventArgs.Enable;
        }
        #endregion
    }
}
