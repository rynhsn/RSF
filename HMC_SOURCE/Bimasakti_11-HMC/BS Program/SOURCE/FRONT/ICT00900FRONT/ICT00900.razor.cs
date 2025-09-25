using BlazorClientHelper;
using ICT00900COMMON;
using ICT00900COMMON.DTO;
using ICT00900COMMON.Param;
using ICT00900COMMON.Utility_DTO;
using ICT00900FrontResources;
using ICT00900MODEL.ICT00900ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT00900FRONT
{
    public partial class ICT00900 : R_Page
    {
        private ICT00900ViewModel _viewModel = new();
        private R_ConductorGrid? _conductorAdj;
        private R_Grid<ICT00900AdjustmentDTO>? _gridAdjustment;
        private R_TabStrip? _tabAdjustmentList;
        private R_TabPage? _tabAdjustmentDetail;

        public VarGsmTransactionCodeDTO VarTransaction = new VarGsmTransactionCodeDTO();
        private int _pageSize = 10;
        private bool _enabaledProperty = true;
        public bool _enableTabAdjDetail = true;
        //public bool _pageCRUDmode;
        private string? TransactionCode = "505010";
        [Inject] IClientHelper? _clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await PropertyListRecord(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region PropertyId
        private async Task PropertyListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetPropertyList();
                await _gridAdjustment.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task PropertyDropdown_OnChange(object poParam)
        {
            var loEx = new R_Exception();
            string lsProperty = (string)poParam;
            try
            {
                PropertyDTO PropertyTemp = _viewModel.PropertyList
                    .FirstOrDefault(data => data.CPROPERTY_ID == lsProperty)!;
                _viewModel.PropertyValue = PropertyTemp;
                _viewModel.ParameterGetList.CPROPERTY_ID = PropertyTemp.CPROPERTY_ID!;
                await _gridAdjustment.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }
        #endregion
        #region List
        private async Task GetListAdjustment(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetAdjustmentList();
                eventArgs.ListEntityResult = _viewModel.CostAdjustmentList;
                if (_viewModel.CostAdjustmentList.Count < 1)
                {
                        _viewModel.lControlButtonRedraft = false;
                        _viewModel.lControlButtonSubmit = false;
                    
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        private async Task TabChangingAsync(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            _viewModel._dropdownProperty = true;
            if (eventArgs.TabStripTab.Id == "TabAdjustment")
            {
                await _gridAdjustment!.R_RefreshGrid(null);
                _enabaledProperty = true;
            }
            //else if (!_pageCRUDmode)
            //{
            //    eventArgs.Cancel = true;
            //}
        }
        private void R_TabEventCallback(object poValue)
        {
            var loEx = new R_Exception();

            try
            {
                _enabaledProperty = (bool)poValue;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private void Before_Open_AdjustmentDetaill(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(ICT00900Detail);

            if (_viewModel.PropertyList.Any())
            {
                ICT00900AdjustmentDTO poParam = new ICT00900AdjustmentDTO
                {
                    CPROPERTY_ID = _viewModel.PropertyValue.CPROPERTY_ID!,
                    CREF_NO = _viewModel.oEntityAdjustmentDetail.CREF_NO!,
                };
                eventArgs.Parameter = poParam;
            }
            else
            {
                eventArgs.Parameter = null;
            }
        }
        private void R_DisplayAsync(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (ICT00900AdjustmentDTO)eventArgs.Data;
                if (loData.CREF_NO != null)
                {
                    _viewModel.oEntityAdjustmentDetail = R_FrontUtility.ConvertObjectToObject<ICT00900AjustmentDetailDTO>(loData);


                    switch (loData.CTRANS_STATUS)
                    {
                        case "00":
                            _viewModel.lControlButtonRedraft = false;
                            _viewModel.lControlButtonSubmit = true;
                            break;
                        case "10":
                             _viewModel.lControlButtonSubmit = false;
                            _viewModel.lControlButtonRedraft = true;
                            break;
                        case "30":
                            _viewModel.lControlButtonRedraft =
                            _viewModel.lControlButtonSubmit = false;
                            break;
                        case "80":
                        case "98":
                            _viewModel.lControlButtonRedraft =
                            _viewModel.lControlButtonSubmit = false;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void SetOther(R_SetEventArgs eventArgs)
        {
           // _pageCRUDmode = eventArgs.Enable;
        }
        #region Button
        private async Task SubmitBtn()
        {
            var loEx = new R_Exception();
            await lockingButton(true);
            try
            {
                //SUBMIT CODE == "10"
                bool llConfirmation = await R_MessageBox.Show("Confirmation",
                    R_FrontUtility.R_GetMessage(typeof(Resources_ICT00900_Class), "_ConfirmationSubmit"),
                    R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.Yes;

                if (llConfirmation)
                {
                    _viewModel.oEntityAdjustmentDetail = R_FrontUtility.ConvertObjectToObject<ICT00900AjustmentDetailDTO>(_conductorAdj.R_GetCurrentData());
                    _viewModel.oEntityAdjustmentDetail.CTRANS_CODE = TransactionCode;
                    var loReturn = await _viewModel.ChangeStatusAdjustment(lcNewStatus: "10");
                    if (loReturn.IS_PROCESS_CHANGESTS_SUCCESS)
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_ICT00900_Class), "_SuccessMessageOfferSubmit"));
                        await _conductorAdj.R_GetEntity(null);
                    }
                    else
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_ICT00900_Class), "_FailedUpdate"));
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            finally
            {
                await lockingButton(false);
            }
            R_DisplayException(loEx);
        }
        private async Task RedraftBtn()
        {
            var loEx = new R_Exception();
            await lockingButton(true);
            try
            {
                //REDRAFT CODE == "00"
                bool llConfirmation = await R_MessageBox.Show("Confirmation",
                  R_FrontUtility.R_GetMessage(typeof(Resources_ICT00900_Class), "_ConfirmationRedraft"),
                  R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.Yes;

                if (llConfirmation)
                {
                    _viewModel.oEntityAdjustmentDetail = R_FrontUtility.ConvertObjectToObject<ICT00900AjustmentDetailDTO>(_conductorAdj.R_GetCurrentData());
                    _viewModel.oEntityAdjustmentDetail.CTRANS_CODE = TransactionCode;
                    var loReturn = await _viewModel.ChangeStatusAdjustment(lcNewStatus: "00");
                    if (loReturn.IS_PROCESS_CHANGESTS_SUCCESS)
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_ICT00900_Class), "_SuccessMessageOfferRedraft"));
                        await _conductorAdj.R_GetEntity(null);
                    }
                    else
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_ICT00900_Class), "_FailedUpdate"));
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            finally
            {
                await lockingButton(false);
            }
            R_DisplayException(loEx);
        }
        #endregion
        #region Locking
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlIC";
        private const string DEFAULT_MODULE_NAME = "IC";

        private async Task lockingButton(bool param)
        {
            var loEx = new R_Exception();
            R_LockingFrontResult loLockResult = null;
            try
            {
                var loData = _viewModel.oEntityAdjustmentDetail;
                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                plSendWithContext: true,
                plSendWithToken: true,
                pcHttpClientName: DEFAULT_HTTP_NAME);
                if (param) // Lock
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "ICT00900",
                        Table_Name = "ICT_TRANS_HD",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, TransactionCode, loData.CREF_NO)
                    };
                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else // Unlock
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "ICT00900",
                        Table_Name = "ICT_TRANS_HD",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, TransactionCode, loData.CREF_NO)
                    };
                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
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
