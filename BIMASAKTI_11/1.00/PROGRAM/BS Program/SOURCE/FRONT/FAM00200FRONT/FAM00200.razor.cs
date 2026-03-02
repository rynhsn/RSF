using BlazorClientHelper;
using FAM00200Common.DTOs;
using FAM00200FrontResources;
using FAM00200Model.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace FAM00200Front
{
    public partial class FAM00200 : R_Page
    {

        #region Inject
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<FAM00200FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
        #endregion

        private FAM00200ViewModel _viewModel = new FAM00200ViewModel();
        private R_Grid<FAM00200DTO> _gridRef;
        private R_Conductor _conductorRef;
        private R_TextBox _TaxTypeId_TextBox;
        private R_TextBox _TaxTypeName_TextBox;
        private bool _PageOnCRUDMode;

        protected async override Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #region Locking
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlFA";
        private const string DEFAULT_MODULE_NAME = "FA";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (FAM00200DTO)eventArgs.Data;

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
                        Program_Id = "FAM00200",
                        Table_Name = "FAM_TAX_TYPE",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CTAX_TYPE_ID)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "FAM00200",
                        Table_Name = "FAM_TAX_TYPE",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CTAX_TYPE_ID)
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

        #region Form
        private async Task TaxType_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                await _viewModel.GetListTaxType();

                eventArgs.ListEntityResult = _viewModel.TaxTypeGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task TaxType_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                await _viewModel.GetTaxType((FAM00200DTO)eventArgs.Data);

                eventArgs.Result = _viewModel.TaxType;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task TaxType_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Edit)
                {
                    await _TaxTypeName_TextBox.FocusAsync();
                }
                if (eventArgs.ConductorMode == R_BlazorFrontEnd.Enums.R_eConductorMode.Add)
                {
                    await _TaxTypeId_TextBox.FocusAsync();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();


        }
        private void TaxType_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;
                var loData = (FAM00200DTO)eventArgs.Data;

                lCancel = string.IsNullOrEmpty(loData.CTAX_TYPE_ID);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V01"));
                }

                lCancel = string.IsNullOrEmpty(loData.CTAX_TYPE_NAME);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "V02"));
                }

                //lCancel = string.IsNullOrEmpty(loData.CTAX_TYPE_DESC);
                //if (lCancel)
                //{
                //    loEx.Add(R_FrontUtility.R_GetError(
                //        typeof(Resources_Dummy_Class),
                //        "V03"));
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task TaxType_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (FAM00200DTO)eventArgs.Data;
                loData.LACTIVE = true;
                if (_viewModel.TaxTypeTypeList.Count > 0)
                    loData.CTAX_TYPE_TYPE = _viewModel.TaxTypeTypeList[0].CCODE;
                loData.CTAX_TYPE_DESC = "";

                await _TaxTypeId_TextBox.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task TaxType_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.SaveTaxType((FAM00200DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _viewModel.TaxType;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task TaxType_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["N01"], R_eMessageBoxButtonType.YesNo);
                eventArgs.Cancel = loValidate == R_eMessageBoxResult.No;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void TaxType_SetOther(R_SetEventArgs eventArgs)
        {
            _PageOnCRUDMode = eventArgs.Enable;
        }
        private void R_RowRender(R_GridRowRenderEventArgs eventArgs)
        {
            var loData = (FAM00200DTO)eventArgs.Data;

            if (!loData.LACTIVE)
            {
                eventArgs.RowClass = "CustomFormatting";
            }
        }
        #endregion
    }
}
