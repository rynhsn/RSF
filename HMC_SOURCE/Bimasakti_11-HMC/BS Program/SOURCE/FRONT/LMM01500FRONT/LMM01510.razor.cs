using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using LMM01500COMMON;
using LMM01500MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Security.Principal;
using System.Xml.Linq;

namespace LMM01500FRONT
{
    public partial class LMM01510 : R_Page, R_ITabPage
    {
        private LMM01510ViewModel _BankAccountGrid_viewModel = new LMM01510ViewModel();
        private LMM01511ViewModel _BankAccount_viewModel = new LMM01511ViewModel();

        private R_Grid<LMM01510DTO> _BankAccount_gridRef;

        private R_Conductor _BankAccount_conductorRef;
        private R_Conductor _BankAccountGrid_conductorRef;

        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01510DTO>(poParameter);
                _BankAccountGrid_viewModel.InvGrpCode = loParam.CINVGRP_CODE;
                _BankAccountGrid_viewModel.InvGrpName = loParam.CINVGRP_NAME;
                _BankAccountGrid_viewModel.PropertyValueContext = loParam.CPROPERTY_ID;

                await _BankAccount_gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Bank Account
        private async Task BankAccount_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _BankAccountGrid_viewModel.GetListTemplateBankAccount();

                eventArgs.ListEntityResult = _BankAccountGrid_viewModel.TemplateBankAccountGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void R_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private async Task R_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                await _BankAccount_conductorRef.R_GetEntity(eventArgs.Data);
            }
        }
        private async Task BankAccount_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01511DTO>(eventArgs.Data);
                await _BankAccount_viewModel.GetTemplateBankAccount(loParam);

                eventArgs.Result = _BankAccount_viewModel.TemplateBankAccount;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private R_TextBox DeptLookup_TextBox;
        private async Task BankAccount_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            await DeptLookup_TextBox.FocusAsync();
        }

        private async Task BankAccount_AfterDelete()
        {
            await _BankAccount_gridRef.RemoveDataAsync();
        }
        private async Task BankAccount_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Edit)
            {
                await DeptLookup_TextBox.FocusAsync();
            }
        }
        private bool BankAccountButtonEnable = false;
       
        private void BankAccount_BeforeAdd(R_BeforeAddEventArgs eventArgs)
        {
            BankAccountButtonEnable = false;
        }
        private void BankAccount_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            BankAccountButtonEnable = false;
        }
        private async Task BankAccount_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            
            BankAccountButtonEnable = false;
            var loData = R_FrontUtility.ConvertObjectToObject<LMM01510DTO>(eventArgs.Data);
            await _BankAccount_gridRef.AddDataAsync(loData);
        }
        private async Task BankAccount_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _BankAccount_viewModel.ValidationTemplateBankAccount((LMM01511DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task BankAccount_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LMM01511DTO loData = (LMM01511DTO)eventArgs.Data;

                if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    loData.CPROPERTY_ID = _BankAccountGrid_viewModel.PropertyValueContext;
                    loData.CINVGRP_CODE = _BankAccountGrid_viewModel.InvGrpCode;
                }

                await _BankAccount_viewModel.SaveTemplateBankAccount(loData, (eCRUDMode)eventArgs.ConductorMode);

                eventArgs.Result = _BankAccount_viewModel.TemplateBankAccount;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task BankAccount_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LMM01511DTO loData = (LMM01511DTO)eventArgs.Data;
                await _BankAccount_viewModel.DeleteTemplateBankAccount(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task _BankAccount_InvTemplateUpload_OnChange(InputFileChangeEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (LMM01511DTO)_BankAccount_conductorRef.R_GetCurrentData();

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

        private bool _pageSupplierOnCRUDmode = true;
        private async Task BankAccount_SetOther(R_SetEventArgs eventArgs)
        {
            _pageSupplierOnCRUDmode = eventArgs.Enable;
            await InvokeTabEventCallbackAsync(eventArgs.Enable);
            enableAdd = eventArgs.Enable && !string.IsNullOrWhiteSpace(_BankAccountGrid_viewModel.InvGrpCode);
        }
        #endregion

        #region Lookup
        private void BankAccountDeptCode_OnLostFocus(object poParam)
        {
            //_BankAccount_viewModel.Data.CDEPT_CODE = (string)poParam;
        }
        private void Department_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00700ParameterDTO();
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }
        private void BankAccount_Department_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _BankAccount_viewModel.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
            _BankAccount_viewModel.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;

            var loGetData = (LMM01511DTO)_BankAccount_conductorRef.R_GetCurrentData();
            BankAccountButtonEnable = !string.IsNullOrWhiteSpace(loGetData.CBANK_CODE) && !string.IsNullOrWhiteSpace(loGetData.CDEPT_CODE);
        }
        private void Bank_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loParam = new GSL01200ParameterDTO
            {
                CCB_TYPE = "B"
            };

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GSL01200);
        }
        private void BankAccountBankCode_OnLostFocus(object poParam)
        {
            //_BankAccount_viewModel.Data.CBANK_CODE = (string)poParam;
        }
        private void BankAccount_Bank_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL01200ParameterDTO()
            {
                CCB_TYPE = "B"
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL01200);
        }

        private void BankAccount_Bank_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL01200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _BankAccount_viewModel.Data.CBANK_CODE = loTempResult.CCB_CODE;
            _BankAccount_viewModel.Data.CBANK_NAME = loTempResult.CCB_NAME;
            BankAccountButtonEnable = !string.IsNullOrEmpty(_BankAccount_viewModel.Data.CDEPT_CODE) && !string.IsNullOrEmpty(_BankAccount_viewModel.Data.CBANK_CODE);
        }
      
        private void BankAcount_OnLostFocus(object poParam)
        {
            //_BankAccount_viewModel.Data.CBANK_ACCOUNT = (string)poParam;
        }
        private void BankAccount_BankAccount_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loGetData = (LMM01511DTO)_BankAccount_conductorRef.R_GetCurrentData();

            var param = new GSL01300ParameterDTO()
            {
                CBANK_TYPE = "B",
                CCB_CODE = loGetData.CBANK_CODE,
                CDEPT_CODE = loGetData.CDEPT_CODE,
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL01300);
        }

        private void BankAccount_BankAccount_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL01300DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _BankAccount_viewModel.Data.CBANK_ACCOUNT = loTempResult.CCB_ACCOUNT_NO;

            var loGetData = (LMM01511DTO)_BankAccount_conductorRef.R_GetCurrentData();
            BankAccountButtonEnable = !string.IsNullOrWhiteSpace(loGetData.CBANK_ACCOUNT) && !string.IsNullOrWhiteSpace(loGetData.CDEPT_CODE);
        }

        private bool enableAdd = true;
        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<LMM01510DTO>(poParam);
                enableAdd = loParam.LTabEnalbleDept;
                if (loParam.LTabEnalbleDept)
                {
                    _BankAccountGrid_viewModel.InvGrpCode = loParam.CINVGRP_CODE;
                    _BankAccountGrid_viewModel.InvGrpName = loParam.CINVGRP_NAME;
                    _BankAccountGrid_viewModel.PropertyValueContext = loParam.CPROPERTY_ID;

                    await _BankAccount_gridRef.R_RefreshGrid(null);
                }
                else
                {
                    _BankAccountGrid_viewModel.InvGrpCode = "";
                    _BankAccountGrid_viewModel.InvGrpName = "";
                    _BankAccountGrid_viewModel.PropertyValueContext = "";
                    _BankAccount_gridRef.DataSource.Clear();
                    await _BankAccount_gridRef.RemoveDataAsync();
                    await _BankAccount_conductorRef.R_SetCurrentData(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion
    }
}
