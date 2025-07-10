using BlazorClientHelper;
using CBT01100COMMON;
using CBT01100MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBT01100FRONT
{
    public partial class CBT01100 : R_Page
    {
        private CBT01100ViewModel _TransactionListViewModel = new();
        private CBT01110ViewModel _TransactionEntryViewModel = new();
        private R_Conductor _conductorRef;
        private R_Conductor _conductorDetailRef;
        private R_Grid<CBT01100DTO> _gridRef;
        private R_Grid<CBT01101DTO> _gridDetailRef;
        private int _bindPageSize_Header = 10;
        private int _bindPageSize_Detail = 10;
        [Inject] private IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _TransactionListViewModel.GetAllUniversalData();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Search

        public async Task OnclickSearch()
        {
            var loEx = new R_Exception();
            try
            {
                //validate
                if (string.IsNullOrEmpty(_TransactionListViewModel.JournalParam.CDEPT_CODE))
                {
                    loEx.Add(new Exception("Please input keyword to search!"));
                    goto EndBlock;
                }
                if (string.IsNullOrEmpty(_TransactionListViewModel.JournalParam.CSEARCH_TEXT))
                {
                    loEx.Add(new Exception("Please input keyword to search!"));
                    goto EndBlock;
                }
                if (!string.IsNullOrEmpty(_TransactionListViewModel.JournalParam.CSEARCH_TEXT)
                    && _TransactionListViewModel.JournalParam.CSEARCH_TEXT.Length < 3)
                {
                    loEx.Add(new Exception("Minimum search keyword is 3 characters!"));
                    goto EndBlock;
                }
                //set rule button
                _TransactionListViewModel._isShowAll = false;

                //clear data

                await _gridRef.R_RefreshGrid(null);
                if (_TransactionListViewModel.JournalGrid.Count <= 0)
                {
                    _TransactionListViewModel.JournalGrid.Clear();
                    _TransactionEntryViewModel.JournalDetailGrid.Clear();
                    var loMsg = await R_MessageBox.Show("", "Data Not Found!");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        public async Task OnClickShowAll()
        {
            var loEx = new R_Exception();
            try
            {
                //set param & rule button to view model
                _TransactionListViewModel._isShowAll = true;
                _TransactionListViewModel.JournalParam.CSEARCH_TEXT = "";

                await _gridRef.R_RefreshGrid(_TransactionListViewModel.JournalParam);
                if (_TransactionListViewModel.JournalGrid.Count <= 0)
                {
                    _TransactionListViewModel.JournalGrid.Clear();
                    _TransactionEntryViewModel.JournalDetailGrid.Clear();
                    var loMsg = await R_MessageBox.Show("", "Data Not Found!");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion Search

        #region JournalGrid

        private async Task JournalGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _TransactionListViewModel.GetJournalList();
                eventArgs.ListEntityResult = _TransactionListViewModel.JournalGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void JournalGrid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                eventArgs.Result = (CBT01100DTO)eventArgs.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task JournalGrid_Display(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = (CBT01100DTO)eventArgs.Data;
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    _TransactionListViewModel.CommitLabel = loData.CSTATUS == "80" ? _localizer["_UndoCommit"] : _localizer["_Commit"];
                    if (!string.IsNullOrWhiteSpace(loData.CREC_ID))
                    {
                        _TransactionEntryViewModel._CREC_ID = loData.CREC_ID;
                        await _gridDetailRef.R_RefreshGrid(null);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task ApproveJournalProcess()
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (CBT01100DTO)_conductorRef.R_GetCurrentData();
                if (!loData.LALLOW_APPROVE)
                {
                    loEx.Add("", "You don’t have right to approve this journal!");
                    goto EndBlock;
                }
                if (await R_MessageBox.Show("", "Are you sure want to undo committed this journal? ", R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.No)
                {
                    goto EndBlock;
                }
                var loParam = R_FrontUtility.ConvertObjectToObject<CBT01100UpdateStatusDTO>(loData);
                loParam.LAUTO_COMMIT = _TransactionEntryViewModel.VAR_GL_SYSTEM_PARAM.LCOMMIT_APVJRN;
                loParam.LUNDO_COMMIT = false;
                loParam.CNEW_STATUS = "30";

                await _TransactionEntryViewModel.UpdateJournalStatus(loParam);
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task CommitJournalProcess()
        {
            var loEx = new R_Exception();
            R_eMessageBoxResult loValidate;

            try
            {
                var loData = (CBT01100DTO)_conductorRef.R_GetCurrentData();

                //confirmation
                if (loData.CSTATUS == "80")
                {
                    if (await R_MessageBox.Show("", "Are you sure want to undo committed this journal? ", R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.No)
                    {
                        goto EndBlock;
                    }
                }
                else
                {
                    if (await R_MessageBox.Show("", "Are you sure want to commit this journal? ", R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.No)
                    {
                        goto EndBlock;
                    }
                }

                var loParam = R_FrontUtility.ConvertObjectToObject<CBT01100UpdateStatusDTO>(loData);
                loParam.LAUTO_COMMIT = _TransactionEntryViewModel.VAR_GL_SYSTEM_PARAM.LCOMMIT_APVJRN;
                loParam.LUNDO_COMMIT = loData.CSTATUS == "80" ? true : false;
                loParam.CNEW_STATUS = loData.CSTATUS == "80" ? (_TransactionEntryViewModel.VAR_GSM_TRANSACTION_CODE.LAPPROVAL_FLAG ? "10" : "00") : "80";

                await _TransactionEntryViewModel.UpdateJournalStatus(loParam);
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        #endregion JournalGrid

        #region JournalGridDetail

        private async Task JournalGridDetail_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _TransactionEntryViewModel.GetJournalDetailList();
                eventArgs.ListEntityResult = _TransactionEntryViewModel.JournalDetailGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #endregion JournalGridDetail

        #region lookupDept

        private void BeforeOpen_lookupDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var param = new GSL00700ParameterDTO
                {
                    CUSER_ID = clientHelper.UserId,
                    CCOMPANY_ID = clientHelper.CompanyId
                };
                eventArgs.Parameter = param;
                eventArgs.TargetPageType = typeof(GSL00700);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void AfterOpen_lookupDept(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _TransactionListViewModel.JournalParam.CDEPT_CODE = loTempResult.CDEPT_CODE;
            _TransactionListViewModel.JournalParam.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }

        private async Task OnLostFocus_LookupDept()
        {
            var loEx = new R_Exception();

            try
            {
                LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel(); //use GSL's model
                var loParam = new GSL00700ParameterDTO // use match param as GSL's dto, send as type in search texbox
                {
                    CSEARCH_TEXT = _TransactionListViewModel.JournalParam.CDEPT_CODE, // property that bindded to search textbox
                };

                var loResult = await loLookupViewModel.GetDepartment(loParam); //retrive single record

                //show result & show name/related another fields
                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    _TransactionListViewModel.JournalParam.CDEPT_NAME = ""; //kosongin bind textbox name kalo gaada
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                    _TransactionListViewModel.JournalParam.CDEPT_NAME = loResult.CDEPT_NAME; //assign bind textbox name kalo ada
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion lookupDept

        #region Predefine Journal Entry

        private void Predef_JournalEntry(R_InstantiateDockEventArgs eventArgs)
        {
            var loParam = (CBT01100DTO)_conductorRef.R_GetCurrentData();
            eventArgs.TargetPageType = typeof(CBT01110);
            eventArgs.Parameter = loParam;
        }

        private async Task AfterPredef_JournalEntry(R_AfterOpenPredefinedDockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
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

        #endregion Predefine Journal Entry
    }
}