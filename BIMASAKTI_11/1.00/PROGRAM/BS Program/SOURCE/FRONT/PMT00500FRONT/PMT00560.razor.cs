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
using System;
using Microsoft.AspNetCore.Components.Forms;

namespace PMT00500FRONT
{
    public partial class PMT00560 : R_Page, R_ITabPage
    {
        #region ViewModel
        private PMT00560ViewModel _viewModel = new PMT00560ViewModel();
        private PMT00510ViewModel _viewModelDetail = new PMT00510ViewModel();
        #endregion

        #region Conductor
        private R_Conductor _conductorRef;
        #endregion

        #region Grid
        private R_Grid<PMT00560DTO> _gridLOIDocumentListRef;
        #endregion

        #region Inject
        [Inject] private R_ILocalizer<PMT00500FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
        #endregion

        #region Private Property
        private R_TextBox DocumentId_TextBox;
        private R_DatePicker<DateTime?> DocumentDate_DatePicker;
        private bool EnableNormalMode = false;
        private bool EnableHasHeaderData = true;
        private bool EnableGreaterClosesSts = true;
        private bool IsAddDataLOI = false;

        private R_TabStrip _TabDocumentCharges;
        private R_TabPage _TabCharge;
        #endregion

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT00500DTO)poParameter;
                EnableGreaterClosesSts = int.Parse(loData.CTRANS_STATUS) >= 80 == false;
                _viewModel.LOI = loData;
                IsAddDataLOI = loData.LIS_ADD_DATA_LOI;
                var loHeaderData = await _viewModelDetail.GetLOIWithResult(loData);
                _viewModel.LOI = loHeaderData;
                PMT00500LOICallBackParameterDTO loCallBackData = new PMT00500LOICallBackParameterDTO { CRUD_MODE = true, SELECTED_DATA_TAB_LOI = _viewModel.LOI, LIS_ADD_DATA_LOI = IsAddDataLOI };
                await InvokeTabEventCallbackAsync(loCallBackData);

                await _gridLOIDocumentListRef.R_RefreshGrid(loData);
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
                var loData = (PMT00560DTO)eventArgs.Data;

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
                        Program_Id = "PMT00560",
                        Table_Name = "PMT_AGREEMENT_DOC",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO, loData.CDOC_NO)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMT00560",
                        Table_Name = "PMT_AGREEMENT_DOC",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO, loData.CDOC_NO)
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

        #region Tab Refresh
        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                IsAddDataLOI = false;
                var loData = (PMT00500DTO)poParam;
                if (string.IsNullOrWhiteSpace(loData.CREF_NO) ==  false && string.IsNullOrWhiteSpace(loData.CUNIT_ID) == false)
                {
                    EnableGreaterClosesSts = int.Parse(loData.CTRANS_STATUS) >= 80 == false;
                    await _gridLOIDocumentListRef.R_RefreshGrid(loData);
                    _viewModel.LOI = loData;
                }
                else
                {
                    _viewModel.LOI = new PMT00500DTO();
                    if (_gridLOIDocumentListRef.DataSource.Count > 0)
                    {
                        _viewModel.R_SetCurrentData(null);
                        _gridLOIDocumentListRef.DataSource.Clear();
                    }
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

        #region Document Form
        private async Task Document_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParameter = R_FrontUtility.ConvertObjectToObject<PMT00560DTO>(eventArgs.Parameter);
                await _viewModel.GetLOIDocumentList(loParameter);

                eventArgs.ListEntityResult = _viewModel.LOIDocumentGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Document_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetLOIDocument((PMT00560DTO)eventArgs.Data);

                eventArgs.Result = _viewModel.LOI_Document;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Document_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
                {
                    await DocumentDate_DatePicker.FocusAsync();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Document_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.SaveLOIDocument(
                    (PMT00560DTO)eventArgs.Data,
                    (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.LOI_Document;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Document_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.DeleteLOIDocument((PMT00560DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Grid_R_SetOther(R_SetEventArgs eventArgs)
        {
            EnableNormalMode = eventArgs.Enable;
            PMT00500LOICallBackParameterDTO loData = new PMT00500LOICallBackParameterDTO { CRUD_MODE = eventArgs.Enable, SELECTED_DATA_TAB_LOI = _viewModel.LOI, LIS_ADD_DATA_LOI = IsAddDataLOI };
            await InvokeTabEventCallbackAsync(loData);
        }
        private async void Document_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loData = (PMT00560DTO)eventArgs.Data;

            loData.CREF_NO = _viewModel.LOI.CREF_NO;
            loData.CDOC_FILE = "";
            loData.CSTORAGE_ID = "";
            loData.CPROPERTY_ID = _viewModel.LOI.CPROPERTY_ID;
            loData.CDEPT_CODE = _viewModel.LOI.CDEPT_CODE;
            loData.CDESCRIPTION = "";

            await DocumentId_TextBox.FocusAsync();
        }
        private void Document_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;
                var loData = (PMT00560DTO)eventArgs.Data;

                lCancel = string.IsNullOrWhiteSpace(loData.CDOC_NO);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "V042"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            eventArgs.Cancel = loEx.HasError;
        }
        private async Task Document_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            var res = await R_MessageBox.Show("", _localizer["Q04"],
                R_eMessageBoxButtonType.YesNo);

            eventArgs.Cancel = res == R_eMessageBoxResult.No;
        }
        #endregion

        #region  Upload File
        private R_eFileSelectAccept[] accepts = { R_eFileSelectAccept.Doc };
        private async Task DocumentFileUpload_OnChange(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT00560DTO)_conductorRef.R_GetCurrentData();

                // Set Data
                loData.FileNameExtension = eventArgs.File.Name;
                var loMS = new MemoryStream();
                await eventArgs.File.OpenReadStream().CopyToAsync(loMS);
                loData.Data = loMS.ToArray();
                loData.FileExtension = Path.GetExtension(eventArgs.File.Name);
                loData.FileName = Path.GetFileNameWithoutExtension(eventArgs.File.Name);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion
        
    }
}
