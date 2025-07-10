using BlazorClientHelper;
using GLT00100COMMON;
using GLT00100MODEL;
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
using R_BlazorFrontEnd.Interfaces;
using GLT00100FrontResources;

namespace GLT00100FRONT
{
    public partial class GLT00100 : R_Page
    {
        private GLT00100ViewModel _JournalListViewModel = new();

        private R_Conductor _conductorRef;

        private R_Grid<GLT00100DTO> _gridRef;

        private R_Grid<GLT00101DTO> _gridDetailRef;

        [Inject] private IClientHelper clientHelper { get; set; }

        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _JournalListViewModel.GetAllUniversalData();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        public async Task OnclickSearch()
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrEmpty(_JournalListViewModel.JournalParam.CSEARCH_TEXT))
                {
                    loEx.Add(new Exception("Please input keyword to search!"));
                    goto EndBlock;
                }
                if (!string.IsNullOrEmpty(_JournalListViewModel.JournalParam.CSEARCH_TEXT)
                    && _JournalListViewModel.JournalParam.CSEARCH_TEXT.Length < 3)
                {
                    loEx.Add(new Exception("Minimum search keyword is 3 characters!"));
                    goto EndBlock;
                }

                await _gridRef.R_RefreshGrid(null);

                if (_JournalListViewModel.JournalGrid.Count <= 0)
                {
                    _JournalListViewModel.JournalDetailGrid.Clear();
                    loEx.Add("", "Data Not Found!");
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
                //reset detail
                _JournalListViewModel.JournalParam.CSEARCH_TEXT = "";
                await _gridRef.R_RefreshGrid(null);
                if (_JournalListViewModel.JournalGrid.Count <= 0)
                {
                    _JournalListViewModel.JournalDetailGrid.Clear();
                    loEx.Add("", "Data Not Found!");
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region JournalGrid

        private async Task JournalGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _JournalListViewModel.GetJournalList();
                eventArgs.ListEntityResult = _JournalListViewModel.JournalGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void JournalGrid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }

        private string lcCommitLabel = "Commit";

        private bool _EnableApprove = false;

        private bool _EnableSubmit = false;

        private bool _EnableCommit = false;

        private async Task JournalGrid_Display(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = (GLT00100DTO)eventArgs.Data;
                _EnableApprove = _gridRef.DataSource.Count > 0 && loData.CSTATUS == "10" && _JournalListViewModel.VAR_GSM_TRANSACTION_CODE.LAPPROVAL_FLAG;
                _EnableCommit =
                    _gridRef.DataSource.Count > 0
                    && ((loData.CSTATUS == "20")
                    || (loData.CSTATUS == "80" && _JournalListViewModel.VAR_IUNDO_COMMIT_JRN.IOPTION != 1))
                    && int.Parse(loData.CREF_PRD) >= int.Parse(_JournalListViewModel.VAR_GL_SYSTEM_PARAM.CSOFT_PERIOD);
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    lcCommitLabel = loData.CSTATUS == "80" ? "Undo Commit" : "Commit";
                    if (!string.IsNullOrWhiteSpace(loData.CREC_ID))
                    {
                        await _gridDetailRef.R_RefreshGrid(eventArgs.Data);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task ApproveJournalProcess()
        {
            var loEx = new R_Exception();
            try
            {
                //get data
                var loCurrentData = (GLT00100DTO)_conductorRef.R_GetCurrentData();
                await _JournalListViewModel.GetJournal(loCurrentData);
                var loData = _JournalListViewModel.Journal;

                //validate allow approval
                if (loData.LALLOW_APPROVE == false)
                {
                    loEx.Add("", "You don’t have right to approve this journal!");
                    goto EndBlock;
                }

                //apprval confirmation
                if (await R_MessageBox.Show("", "Are you sure want to approve this journal? [Yes/No] ",
                        R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.No)
                {
                    goto EndBlock;
                }

                //convert data to param
                var loParam = R_FrontUtility.ConvertObjectToObject<GLT00100UpdateStatusDTO>(loData);
                loParam.CNEW_STATUS = "20";
                loParam.LAUTO_COMMIT = _JournalListViewModel.VAR_GL_SYSTEM_PARAM.LCOMMIT_APVJRN;
                loParam.LUNDO_COMMIT = false;

                //update status
                await _JournalListViewModel.UpdateJournalStatus(loParam);

                //get & set new data
                await _JournalListViewModel.GetJournal(loData);
                await _gridRef.R_RefreshGrid(null);
                await _conductorRef.R_SetCurrentData(_JournalListViewModel.Journal);

                //display message
                if (_JournalListViewModel.Journal.CSTATUS == "20")
                    await R_MessageBox.Show("", "Journal Approved Successfully!", R_eMessageBoxButtonType.OK);
                //set data
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
            try
            {
                //get data
                var loCurrentData = (GLT00100DTO)_conductorRef.R_GetCurrentData();
                await _JournalListViewModel.GetJournal(loCurrentData);
                var loData = _JournalListViewModel.Journal;

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

                //convert data to param
                var loParam = R_FrontUtility.ConvertObjectToObject<GLT00100UpdateStatusDTO>(loData);

                //set param
                loParam.CNEW_STATUS = loData.CSTATUS == "80" ? (_JournalListViewModel.VAR_GSM_TRANSACTION_CODE.LAPPROVAL_FLAG ? "10" : "00") : "80";
                loParam.LAUTO_COMMIT = false;
                loParam.LUNDO_COMMIT = loData.CSTATUS == "80";

                //update journal status
                await _JournalListViewModel.UpdateJournalStatus(loParam);

                //get & set new data
                await _JournalListViewModel.GetJournal(loData);
                await _gridRef.R_RefreshGrid(null);
                await _conductorRef.R_SetCurrentData(_JournalListViewModel.Journal);

                //show message
                await R_MessageBox.Show("", _JournalListViewModel.Journal.CSTATUS == "80" ? "Journal Committed Successfully!" : "Journal Undo Committed Successfully!", R_eMessageBoxButtonType.OK);
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
                var loParam = R_FrontUtility.ConvertObjectToObject<GLT00101DTO>(eventArgs.Parameter);
                await _JournalListViewModel.GetJournalDetailList(loParam);
                eventArgs.ListEntityResult = _JournalListViewModel.JournalDetailGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #endregion JournalGridDetail

        #region lookupDept

        private void Before_Open_lookupDept(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00700ParameterDTO
            {
                CUSER_ID = clientHelper.UserId,
                CCOMPANY_ID = clientHelper.CompanyId
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }

        private void After_Open_lookupDept(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _JournalListViewModel.JournalParam.CDEPT_CODE = loTempResult.CDEPT_CODE;
            _JournalListViewModel.JournalParam.CDEPT_NAME = loTempResult.CDEPT_NAME;
        }

        private async Task OnLostFocus_LookupDept()
        {
            var loEx = new R_Exception();

            try
            {
                if (!String.IsNullOrWhiteSpace(_JournalListViewModel.JournalParam.CDEPT_CODE))
                {
                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel(); //use GSL's model
                    var loParam = new GSL00700ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CSEARCH_TEXT = _JournalListViewModel.JournalParam.CDEPT_CODE, // property that bindded to search textbox
                    };

                    var loResult = await loLookupViewModel.GetDepartment(loParam); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _JournalListViewModel.JournalParam.CDEPT_NAME = ""; //kosongin bind textbox name kalo gaada
                    }
                    else
                    {
                        _JournalListViewModel.JournalParam.CDEPT_NAME = loResult.CDEPT_NAME; //assign bind textbox name kalo ada
                    }
                }
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
            var loEx = new R_Exception();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<GLT00110DTO>(_conductorRef.R_GetCurrentData());
                var loParam = new GLT01100FrontPredefinedParamDTO
                {
                    PARAM_FROM_INTERNAL_JOURNAL_LIST = loData,
                };
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GLT00110);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task AfterPredef_JournalEntryAsync(R_AfterOpenPredefinedDockEventArgs eventArgs)
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

        #region RapidApprove

        private async Task R_Before_Open_PopupRapidApprove(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (GLT00100DTO)_conductorRef.R_GetCurrentData();
                var loParam = R_FrontUtility.ConvertObjectToObject<GLT00100RapidApprovalValidationDTO>(loData);
                var loValidate = await _JournalListViewModel.ValidationRapidApproval(loParam);
                if (!loValidate)
                {
                    await R_MessageBox.Show("", "You don’t have right to approve this journal type!", R_eMessageBoxButtonType.OK);
                    goto EndBlock;
                }
                loData.CDEPT_NAME = _JournalListViewModel.JournalParam.CDEPT_NAME;
                eventArgs.Parameter = loData;
                eventArgs.TargetPageType = typeof(GLT00102);
                eventArgs.PageTitle = _localizer["_pageTitleRapidApproval"];
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_AfterOpen_PopupRapidApprove(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _JournalListViewModel.JournalDetailGrid.Clear();
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion RapidApprove

        #region RapidCommit

        private void R_Before_Open_PopupRapidCommit(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loData = (GLT00100DTO)_conductorRef.R_GetCurrentData();
            loData.CDEPT_NAME = _JournalListViewModel.JournalParam.CDEPT_NAME;
            eventArgs.Parameter = loData;
            eventArgs.TargetPageType = typeof(GLT00103);
            eventArgs.PageTitle = _localizer["_pageTitleRapitCommit"];
        }

        private async Task R_AfterOpen_PopupRapidCommit(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _JournalListViewModel.JournalDetailGrid.Clear();
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion RapidCommit
    }
}