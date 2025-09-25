using GLM00200COMMON;
using GLM00200FrontResources;
using GLM00200MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System.Threading.Tasks;

namespace GLM00200FRONT
{
    public partial class GLM00200 : R_Page //recurring list
    {
        private GLM00200ViewModel _journalVM = new GLM00200ViewModel();

        private GLM00201ViewModel _journalProcessVM = new GLM00201ViewModel();

        private R_Grid<JournalDTO> _gridJournal;

        private R_ConductorGrid _conJournal;

        private R_Conductor _conJournalDet;

        private R_TabPage _tabPage_RecurringEntry; //ref TabPage tab2

        private R_TabPage _tabPage_RecurringActualJournal; //ref TabPage tab2

        private R_TabStrip _tabStrip_Recurring; //ref Tabstrip

        private R_Grid<JournalDetailGridDTO> _gridJournalDet;

        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }

        [Inject] private IJSRuntime JS { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _journalVM.GetInitData();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        //tabpage

        private void BeforeOpenTabPage_RecurringEntry(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = _journalVM.RecurringJrnRecord;
                eventArgs.TargetPageType = typeof(GLM00210);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void BeforeOpenTabPage_RecurringActualJournal(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = _journalVM.RecurringJrnRecord;
                eventArgs.TargetPageType = typeof(GLM00220);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //search
        public async Task SearchAllAsync()
        {
            var loEx = new R_Exception();
            try
            {
                _journalVM.RecurringJrnListSearchParam.CSEARCH_TEXT = "";
                await _gridJournal.R_RefreshGrid(null);
                if (_journalVM.RecurringJrnList.Count == 0)
                {
                    _journalVM.RecurringJrnDtList.Clear();
                    loEx.Add("", _localizer["_msg_search1"]);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        public async Task SearchWithFilterAsync()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrEmpty(_journalVM.RecurringJrnListSearchParam.CSEARCH_TEXT))
                {
                    loEx.Add("", _localizer["_msg_search2"]);
                    goto EndBlock;
                }
                else
                {
                    if (_journalVM.RecurringJrnListSearchParam.CSEARCH_TEXT.Length < 3)
                    {
                        loEx.Add("", _localizer["_msg_search3"]);
                        goto EndBlock;
                    }
                }

                await _gridJournal.R_RefreshGrid(null);
                if (_journalVM.RecurringJrnList.Count == 0)
                {
                    _journalVM.RecurringJrnDtList.Clear();
                    loEx.Add("", _localizer["_msg_search1"]);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }

        //JournalGrid
        private async Task JournalGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _journalVM.ShowAllJournals();
                eventArgs.ListEntityResult = _journalVM.RecurringJrnList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void JournalGrid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                eventArgs.Result = eventArgs.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private string lcCommitLabel = "Commit";

        private async Task JournalGrid_Display(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = (JournalDTO)eventArgs.Data;
                _journalVM.RecurringJrnRecord = loData;
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    lcCommitLabel = loData.CSTATUS == "80" ? _localizer["_btn_UndoCommit"] : _localizer["_btn_Commit"];
                }
                await _gridJournalDet.R_RefreshGrid(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //approve commit
        private async Task BtnJournal_Approve()
        {
            var loEx = new R_Exception();
            try
            {
                //get data
                var loCurrentData = (JournalDTO)_conJournal.R_GetCurrentData();
                await _journalProcessVM.GetJournal(new JournalDTO() { CJRN_ID = loCurrentData.CREC_ID });
                var loData = _journalProcessVM.Journal;

                //validate allo approve
                if (loData.LALLOW_APPROVE == false)
                {
                    loEx.Add("", "You don’t have right to approve this journal!");
                    goto EndBlock;
                }

                //validate start date
                if (int.Parse(loData.CSTART_DATE) < int.Parse(_journalVM.InitDataRecord.SOFT_PERIOD_START_DATE.CSTART_DATE))
                {
                    loEx.Add("", "Cannot Approve Recurring Journal with Starting Date before Soft Close Period!");
                    goto EndBlock;
                }
                //var loValidate = await R_MessageBox.Show("", _localizer["Q03"], R_eMessageBoxButtonType.YesNo);
                var loValidate = await R_MessageBox.Show("", "Are you sure want to Approve this Recurring Journal?", R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.No)
                {
                    //if no cancel
                    goto EndBlock;
                }
                else //if yes continue process
                {
                    //convert data to param
                    var loParam = R_FrontUtility.ConvertObjectToObject<GLM00200UpdateStatusDTO>(loData);
                    loParam.CNEW_STATUS = "20";
                    loParam.LAUTO_COMMIT = _journalVM.InitDataRecord.GL_SYSTEM_PARAM.LCOMMIT_APVJRN;
                    loParam.LUNDO_COMMIT = false;

                    //update status
                    await _journalProcessVM.UpdateJournalStatusAsync(loParam);

                    //get new data
                    await _journalProcessVM.GetJournal(new JournalDTO() { CJRN_ID = loParam.CREC_ID });

                    //refresh grid
                    await _gridJournal.R_RefreshGrid(null);

                    //set data to grid
                    await _gridJournal.R_SelectCurrentDataAsync(_journalProcessVM.Journal);

                    //display message
                    await R_MessageBox.Show("", "Recurring Journal Approved Successfully!", R_eMessageBoxButtonType.OK);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task BtnJournal_Commit()
        {
            var loEx = new R_Exception();
            try
            {
                //get data
                var loCurrentData = (JournalDTO)_conJournal.R_GetCurrentData();
                await _journalProcessVM.GetJournal(new JournalDTO() { CJRN_ID = loCurrentData.CREC_ID });
                var loData = _journalProcessVM.Journal;

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
                var loParam = R_FrontUtility.ConvertObjectToObject<GLM00200UpdateStatusDTO>(loData);

                //set new status
                loParam.CNEW_STATUS = loData.CSTATUS == "80" ? (_journalVM.InitDataRecord.GSM_TRANSACTION_CODE.LAPPROVAL_FLAG ? "10" : "00") : "80";
                loParam.LAUTO_COMMIT = false;
                loParam.LUNDO_COMMIT = loData.CSTATUS == "80";

                //update journal status
                await _journalProcessVM.UpdateJournalStatusAsync(loParam);

                //get new data
                await _journalProcessVM.GetJournal(new JournalDTO() { CJRN_ID = loParam.CREC_ID });

                //refresh grid
                await _gridJournal.R_RefreshGrid(null);

                //set to grid
                await _gridJournal.R_SelectCurrentDataAsync(_journalProcessVM.Journal);

                //show message
                await R_MessageBox.Show("", _journalVM.RecurringJrnRecord.CSTATUS == "80" ? "Recurring Journal Committed Successfully!" : "Recurring Journal Undo Committed Successfully!", R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        //getlist record
        private async Task JournalDetGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = (JournalDTO)eventArgs.Parameter;
                await _journalVM.ShowAllJournalDetail(loData);
                eventArgs.ListEntityResult = _journalVM.RecurringJrnDtList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        //lookup
        private void Dept_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = new GSL00700ParameterDTO();
                eventArgs.TargetPageType = typeof(GSL00700);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void Dept_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loTempResult = R_FrontUtility.ConvertObjectToObject<GSL00700DTO>(eventArgs.Result);
                _journalVM.RecurringJrnListSearchParam.CDEPT_CODE = loTempResult.CDEPT_CODE;
                _journalVM.RecurringJrnListSearchParam.CDEPT_NAME = loTempResult.CDEPT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void BeforeOpenPopup_UploadReccr(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                eventArgs.Parameter = new object();
                eventArgs.PageTitle = _localizer["_pageTitleRecurringUploadPopup"];
                eventArgs.TargetPageType = typeof(GLM00202);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task AfterOpenLookup_UploadReccr(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _gridJournal.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }


        private async Task OnLostFocus_LookupDept()
        {
            var loEx = new R_Exception();

            try
            {
                if (!String.IsNullOrWhiteSpace(_journalVM.RecurringJrnListSearchParam.CDEPT_CODE))
                {
                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel(); //use GSL's model
                    var loParam = new GSL00700ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CSEARCH_TEXT = _journalVM.RecurringJrnListSearchParam.CDEPT_CODE, // property that bindded to search textbox
                    };
                    var loResult = await loLookupViewModel.GetDepartment(loParam); //retrive single record

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _journalVM.RecurringJrnListSearchParam.CDEPT_NAME = ""; //kosongin bind textbox name kalo gaada
                    }
                    else
                    {
                        _journalVM.RecurringJrnListSearchParam.CDEPT_NAME = loResult.CDEPT_NAME; //assign bind textbox name kalo ada
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        //download
        private async Task TemplateBtn_OnClick()
        {
            try
            {
                //var loValidate = await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_NotifTemplate"), R_eMessageBoxButtonType.YesNo);
                var loValidate = await R_MessageBox.Show("", "Are you sure download this template?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loByteFile = await _journalVM.DownloadTemplate();

                    var saveFileName = "GL_RECURRING_JOURNAL_UPLOAD.xlsx";

                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.data);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}