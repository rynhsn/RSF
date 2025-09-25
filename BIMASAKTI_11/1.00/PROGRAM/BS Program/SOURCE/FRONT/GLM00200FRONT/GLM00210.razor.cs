using BlazorClientHelper;
using GLM00200COMMON;
using GLM00200MODEL;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System.Globalization;
using R_BlazorFrontEnd.Controls.MessageBox;
using GLM00200FrontResources;
using Lookup_GSModel.ViewModel;
using System.Collections.ObjectModel;
using R_BlazorFrontEnd.Interfaces;

namespace GLM00200FRONT
{
    public partial class GLM00210 : R_Page //recurring entry
    {
        private GLM00201ViewModel _journalVM = new GLM00201ViewModel();
        private R_Grid<JournalDetailGridDTO> _gridJournalDet;
        private R_Conductor _conJournalNavigator;
        private R_ConductorGrid _conJournalDetail;
        private bool _enableCrudJournalDetail = false;

        #region Private Property
        private bool EnableEdit = false;
        private bool EnableDelete = false;
        private bool EnableSubmit = false;
        private bool EnableApprove = false;
        private bool EnableCommit = false;
        private bool EnableHaveRecId = false;
        #endregion
        [Inject] IClientHelper _clientHelper { get; set; }


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _journalVM.GetInitData();
                var loParam = R_FrontUtility.ConvertObjectToObject<JournalDTO>(poParameter);

                if (!string.IsNullOrWhiteSpace(loParam.CREC_ID))
                {
                    loParam.CJRN_ID = loParam.CREC_ID;
                    await _conJournalNavigator.R_GetEntity(loParam);
                }
                else
                {
                    _journalVM.RefDate = _journalVM.VAR_TODAY.DTODAY;
                    _journalVM.DocDate = _journalVM.VAR_TODAY.DTODAY;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region JournalForm
        private void JournalForm_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = (JournalDTO)eventArgs.Data;
                if (eventArgs.ConductorMode != R_eConductorMode.Normal)
                {

                    if (_journalVM.RefDate == null)
                    {
                        loEx.Add("", "Reference Date is required!");
                    }
                    if (_journalVM.RefDate < DateTime.ParseExact(_journalVM.CURRENT_PERIOD_START_DATE.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture))
                    {
                        loEx.Add("", "Reference Date cannot be before Current Period!");
                    }
                    if (_journalVM.RefDate > _journalVM.VAR_TODAY.DTODAY)
                    {
                        loEx.Add("", "Reference Date cannot be after today!");
                    }

                    if (_journalVM.RefDate > _journalVM.StartDate)
                    {
                        loEx.Add("", "Reference Date cannot be after Start Date!");
                    }
                    if (_journalVM.StartDate < DateTime.ParseExact(_journalVM.CSOFT_PERIOD_START_DATE.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture))
                    {
                        loEx.Add("", "Start Date cannot be before Current Period!");
                    }
                    if (_journalVM.StartDate < DateTime.Today)
                    {
                        loEx.Add("", "Start Date cannot be before Today!");
                    }
                    if (string.IsNullOrEmpty(loData.CDOC_NO) && _journalVM.DocDate.HasValue)
                    {
                        loEx.Add("", "Please input Document No.!");
                    }
                    if (_journalVM.DocDate == null && _journalVM.DocDate > DateTime.Now)
                    {
                        loEx.Add("", "Document Date cannot be after today");
                    }
                    if (_journalVM.DocDate == null && _journalVM.DocDate < DateTime.ParseExact(_journalVM.CURRENT_PERIOD_START_DATE.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture))
                    {
                        loEx.Add("", "Document Date cannot be before Current Period!");
                    }
                    if (!string.IsNullOrEmpty(loData.CDOC_NO) && _journalVM.DocDate == null)
                    {
                        loEx.Add("", "Please input Document Date!");
                    }
                    if (_journalVM.DocDate > _journalVM.StartDate)
                    {
                        loEx.Add("", "Document Date cannot be after Start Date!");
                    }
                    if (string.IsNullOrEmpty(loData.CTRANS_DESC) || string.IsNullOrWhiteSpace(loData.CTRANS_DESC))
                    {
                        loEx.Add("", "Description is required!");
                    }

                    if (loData.NPRELIST_AMOUNT > 0 && loData.NPRELIST_AMOUNT != loData.NDEBIT_AMOUNT)
                    {
                        loEx.Add("", "Journal amount is not equal to Prelist!");
                    }

                    if (loData.NLBASE_RATE <= 0)
                    {
                        loEx.Add("", "Local Currency Base Rate must be greater than 0!");
                    }

                    if (loData.NLCURRENCY_RATE <= 0)
                    {
                        loEx.Add("", "Local Currency Rate must be greater than 0!");
                    }

                    if (loData.NBBASE_RATE <= 0)
                    {
                        loEx.Add("", "Base Currency Base Rate must be greater than 0!");
                    }

                    if (loData.NBCURRENCY_RATE <= 0)
                    {
                        loEx.Add("", "Base Currency Rate must be greater than 0!");
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.HasError)
                eventArgs.Cancel = true;
            loEx.ThrowExceptionIfErrors();
        }
        private async Task JournalForm_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (JournalDTO)eventArgs.Data;
                var loData = R_FrontUtility.ConvertObjectToObject<JournalParamDTO>(loParam);
                loData.ListJournalDetail = _gridJournalDet.DataSource.ToList();
                await _journalVM.SaveJournal(loData, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _journalVM.Journal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task JournalForm_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (JournalDTO)eventArgs.Data;
                if (!string.IsNullOrWhiteSpace(loParam.CREC_ID))
                {
                    loParam.CJRN_ID = loParam.CREC_ID;
                }
                await _journalVM.GetJournal(loParam);
                eventArgs.Result = _journalVM.Journal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }

        private string lcSubmit = "Submit";
        private string lcCommit = "Commit";
        private async Task JournalForm_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loHeaderData = (JournalDTO)eventArgs.Data;

                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    if (eventArgs.Data != null)
                    {
                        if (DateTime.TryParseExact(loHeaderData.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate))
                        {
                            _journalVM.RefDate = ldRefDate;
                        }
                        else
                        {
                            _journalVM.RefDate = null;
                        }
                        if (DateTime.TryParseExact(loHeaderData.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDocDate))
                        {
                            _journalVM.DocDate = ldDocDate;
                        }
                        else
                        {
                            _journalVM.RefDate = null;
                        }

                        if (DateTime.TryParseExact(loHeaderData.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldStartDate))
                        {
                            _journalVM.StartDate = ldStartDate;
                        }
                        else
                        {
                            _journalVM.StartDate = null;
                        }

                        if (DateTime.TryParseExact(loHeaderData.CNEXT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldNextDate))
                        {
                            _journalVM.NextDate = ldNextDate;
                        }
                        else
                        {
                            _journalVM.NextDate = null;
                        }

                        if (DateTime.TryParseExact(loHeaderData.CLAST_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldLastDate))
                        {
                            _journalVM.LastDate = ldLastDate;
                        }
                        else
                        {
                            _journalVM.LastDate = null;
                        }

                        var loData = (JournalDTO)eventArgs.Data;
                        if (!string.IsNullOrWhiteSpace(loData.CREC_ID) || !string.IsNullOrWhiteSpace(loData.CJRN_ID))
                        {
                            await _gridJournalDet.R_RefreshGrid(loData);
                        }
                        lcSubmit = loData.CSTATUS == "10" ? "Undo Submit" : "Submit";
                        lcCommit = loData.CSTATUS == "80" ? "Undo Commit" : "Commit";
                        EnableCommit = loData.CSTATUS == "20" || loData.CSTATUS == "80";

                    }
                    if (_gridJournalDet.DataSource.Count > 0)
                    {
                        loHeaderData.NDEBIT_AMOUNT = _gridJournalDet.DataSource.Sum(x => x.NDEBIT);
                        loHeaderData.NCREDIT_AMOUNT = _gridJournalDet.DataSource.Sum(x => x.NCREDIT);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task JournalForm_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                //chose if copymode or not
                if (_journalVM._IsCopyMode)
                {
                    eventArgs.Data = R_FrontUtility.ConvertObjectToObject<JournalDTO>(_journalVM.Journal);
                    var loCopyData = (JournalDTO)eventArgs.Data;
                    loCopyData.CREF_NO = "";
                    _journalVM.RefDate = DateTime.ParseExact(_journalVM.Journal.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                    _journalVM.DocDate = DateTime.ParseExact(_journalVM.Journal.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture); ;
                    _journalVM.StartDate = DateTime.ParseExact(_journalVM.Journal.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                    _journalVM.NextDate = _journalVM.StartDate.Value.AddDays(1);
                    _journalVM.LastDate = null;
                    loCopyData.DCREATE_DATE = _journalVM.VAR_TODAY.DTODAY;
                    loCopyData.DUPDATE_DATE = _journalVM.VAR_TODAY.DTODAY;
                    _journalVM._IsCopyMode = false;
                }
                else
                {
                    //get data object
                    var loData = (JournalDTO)eventArgs.Data;

                    //store to temp
                    _journalVM.JournaDetailGridTemp = new(_gridJournalDet.DataSource.ToList());
                    _gridJournalDet.DataSource.Clear();
                    await _gridJournalDet.R_RefreshGrid(null);

                    //set default data
                    loData.CCREATE_BY = _clientHelper.UserId;
                    loData.CUPDATE_BY = _clientHelper.UserId;
                    loData.DCREATE_DATE = _journalVM.VAR_TODAY.DTODAY;
                    loData.DUPDATE_DATE = _journalVM.VAR_TODAY.DTODAY;
                    _journalVM.RefDate = _journalVM.VAR_TODAY.DTODAY;
                    _journalVM.DocDate = null;
                    _journalVM.StartDate = _journalVM.VAR_TODAY.DTODAY;
                    _journalVM.NextDate = _journalVM.VAR_TODAY.DTODAY.AddDays(1);
                    _journalVM.LastDate = null;
                    loData.IFREQUENCY = 1;
                    loData.IPERIOD = 1;
                    loData.CCURRENCY_CODE = _journalVM.GSM_COMPANY.CLOCAL_CURRENCY_CODE;//set default ccurrency data when addmode
                    loData.CDEPT_CODE = _journalVM.GL_SYSTEM_PARAM.CCLOSE_DEPT_CODE;
                    loData.CDEPT_NAME = _journalVM.GL_SYSTEM_PARAM.CCLOSE_DEPT_NAME;
                    loData.NLBASE_RATE = 1;
                    loData.NLBASE_RATE = 1;
                    loData.NLCURRENCY_RATE = 1;
                    loData.NBBASE_RATE = 1;
                    loData.NBCURRENCY_RATE = 1;

                    if (_gridJournalDet.DataSource.Count > 0)
                    {
                        await _gridJournalDet.R_RefreshGrid(null);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }
        private async Task JournalForm_BeforeCancelAsync(R_BeforeCancelEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var res = await R_MessageBox.Show("", "You haven’t saved your changes. Are you sure want to cancel?",
                                    R_eMessageBoxButtonType.YesNo);
                if (res == R_eMessageBoxResult.No)
                {
                    eventArgs.Cancel = true;
                }
                else
                {
                    _journalVM.JournaDetailGrid = _journalVM.JournaDetailGridTemp;
                    _journalVM.RefDate = string.IsNullOrWhiteSpace(_journalVM.Journal.CREF_DATE) ? _journalVM.VAR_TODAY.DTODAY : DateTime.ParseExact(_journalVM.Journal.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                    _journalVM.DocDate = string.IsNullOrWhiteSpace(_journalVM.Journal.CDOC_DATE) ? null : DateTime.ParseExact(_journalVM.Journal.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                    _journalVM.StartDate = string.IsNullOrWhiteSpace(_journalVM.Journal.CSTART_DATE) ? _journalVM.VAR_TODAY.DTODAY : DateTime.ParseExact(_journalVM.Journal.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                    _journalVM.NextDate = string.IsNullOrWhiteSpace(_journalVM.Journal.CNEXT_DATE) ? null : DateTime.ParseExact(_journalVM.Journal.CNEXT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                    _journalVM.LastDate = string.IsNullOrWhiteSpace(_journalVM.Journal.CLAST_DATE) ? null : DateTime.ParseExact(_journalVM.Journal.CLAST_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private R_TextBox _txt_CDEPT_CODE;
        private async Task CopyJournalEntryProcessAsync()
        {
            var loEx = new R_Exception();
            try
            {
                _journalVM._IsCopyMode = true;


                //handle data exist in detail implement afteradd detail filters
                var loDetailListData = _gridJournalDet.DataSource;

                foreach (var loDetailData in loDetailListData)
                {
                    loDetailData.NAMOUNT = loDetailData.NDEBIT + loDetailData.NCREDIT;
                    loDetailData.CDOCUMENT_NO = string.IsNullOrWhiteSpace(_journalVM.Data.CDOC_NO) ? "" : _journalVM.Data.CDOC_NO;
                    loDetailData.CDOCUMENT_DATE = _journalVM.DocDate.HasValue == true ? _journalVM.DocDate.Value.ToString("yyyyMMdd") : "";
                }

                //store detail data to temp
                _journalVM.JournaDetailGridTemp = loDetailListData;


                await _conJournalNavigator.Add();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

        }

        #endregion

        #region DepartmentLookup
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
                if (loTempResult == null)
                {
                    return;
                }
                _journalVM.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
                _journalVM.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;
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
                if (!String.IsNullOrWhiteSpace(_journalVM.Data.CDEPT_CODE))
                {

                    LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel(); //use GSL's model
                    var loParam = new GSL00700ParameterDTO // use match param as GSL's dto, send as type in search texbox
                    {
                        CSEARCH_TEXT = _journalVM.Data.CDEPT_CODE, // property that bindded to search textbox
                    };


                    var loResult = await loLookupViewModel.GetDepartment(loParam); //retrive single record 

                    //show result & show name/related another fields
                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _journalVM.Data.CDEPT_NAME = ""; //kosongin bind textbox name kalo gaada
                    }
                    else
                    {
                        _journalVM.Data.CDEPT_NAME = loResult.CDEPT_NAME; //assign bind textbox name kalo ada
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region JournalDetailGrid
        private void JurnalDetail_GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private async Task JournalDetGrid_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (eventArgs.Parameter != null)
                {
                    var loParam = (JournalDTO)eventArgs.Parameter;
                    await _journalVM.ShowAllJournalDetail(loParam);
                    eventArgs.ListEntityResult = _journalVM.JournaDetailGrid;
                }
                else
                {
                    eventArgs.ListEntityResult = new ObservableCollection<JournalDetailGridDTO>();
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private void JournalDetailBeforeOpenLookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var param = new GSL00500ParameterDTO
            {
                CPROGRAM_CODE = "GLM00200",
                CBSIS = "",
                CDBCR = "",
                CCENTER_CODE = "",
                CPROPERTY_ID = "",
                LCENTER_RESTR = false,
                LUSER_RESTR = false
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00500);
        }
        private void JournalDetailAfterOpenLookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loTempResult = (GSL00500DTO)eventArgs.Result;
            if (loTempResult == null)
                return;
            var loGetData = (JournalDetailGridDTO)eventArgs.ColumnData;
            loGetData.CGLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
            loGetData.CGLACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
            loGetData.CBSIS = loTempResult.CBSIS;
            loGetData.CCENTER_CODE = "";
            loGetData.CCENTER_NAME = "";
            //loGetData.CBSIS = loTempResult.CBSIS_DESCR.Contains("B") ? 'B' : (loTempResult.CBSIS_DESCR.Contains("I") ? 'I' :default(char));
        }
        private void JurnalDetail_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (JournalDetailGridDTO)eventArgs.Data;
                if (eventArgs.ConductorMode != R_eConductorMode.Normal)
                {
                    if (string.IsNullOrWhiteSpace(loData.CCENTER_CODE) && (loData.CBSIS == "B" && _journalVM.GSM_COMPANY.LENABLE_CENTER_BS == true) || (loData.CBSIS == "I" && _journalVM.GSM_COMPANY.LENABLE_CENTER_IS == true))
                    {
                        loEx.Add("", $"Center Code is required for Account No. {loData.CGLACCOUNT_NO}!");
                    }

                    if (loData.NDEBIT == 0 && loData.NCREDIT == 0)
                    {
                        loEx.Add("", "Journal amount cannot be 0!");
                    }

                    if (loData.NDEBIT > 0 && loData.NCREDIT > 0)
                    {
                        loEx.Add("", "Journal amount can only be either Debit or Credit!");
                    }

                    if (eventArgs.ConductorMode == R_eConductorMode.Add)
                    {
                        if (_journalVM.JournaDetailGrid.Any(item => item.CGLACCOUNT_NO == loData.CGLACCOUNT_NO))
                        {
                            loEx.Add("", $"Account No. {loData.CGLACCOUNT_NO} already exists!");
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
        private void JurnalDetail_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loHeaderData = (JournalDTO)_conJournalNavigator.R_GetCurrentData();
                if (eventArgs.Data != null)
                {
                    var lodata = (JournalDetailGridDTO)eventArgs.Data;

                    //findout credit or debit
                    lodata.CDBCR = lodata.NDEBIT > 0 ? "D" : lodata.NCREDIT > 0 ? "C" : "";

                    if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                    {
                        if (_gridJournalDet.DataSource.Count > 0)
                        {
                            loHeaderData.NDEBIT_AMOUNT = _gridJournalDet.DataSource.Sum(x => x.NDEBIT);
                            loHeaderData.NCREDIT_AMOUNT = _gridJournalDet.DataSource.Sum(x => x.NCREDIT);
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
        private void JurnalDetail_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loData = (JournalDetailGridDTO)eventArgs.Data;

            //create increment data grid
            loData.INO = _gridJournalDet.DataSource.Count + 1;
            loData.CCENTER_CODE = "";
            loData.CDETAIL_DESC = _journalVM.Data.CTRANS_DESC;
        }
        private void JurnalDetail_Saving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (JournalDetailGridDTO)eventArgs.Data;

                loData.CDBCR = loData.NDEBIT > 0 ? "D" : loData.NCREDIT > 0 ? "C" : "";
                loData.NAMOUNT = loData.NDEBIT + loData.NCREDIT;
                loData.CDOCUMENT_NO = string.IsNullOrWhiteSpace(_journalVM.Data.CDOC_NO) ? "" : _journalVM.Data.CDOC_NO;
                loData.CDOCUMENT_DATE = _journalVM.DocDate.HasValue == true ? _journalVM.DocDate.Value.ToString("yyyyMMdd") : "";


                var loHeaderData = (JournalDTO)_conJournalNavigator.R_GetCurrentData();


                loData.CCENTER_CODE = string.IsNullOrWhiteSpace(loData.CCENTER_CODE) ? "" : loData.CCENTER_CODE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Form Control
        private bool _enable_NLBASE_RATE = false;
        private bool _enable_NLCURRENCY_RATE = false;
        private bool _enable_NBBASE_RATE = false;
        private bool _enable_NBCURRENCY_RATE = false;
        private void OnChangedStartDate()
        {
            var loEx = new R_Exception();
            try
            {
                _journalVM.NextDate = _journalVM.StartDate;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private void OnChanged_LFIX_RATE(bool plParam)
        {
            var loEx = new R_Exception();
            try
            {
                _journalVM.Data.LFIX_RATE = plParam;
                //if (_journalVM.Data.LFIX_RATE)
                //{
                //    _enable_NLBASE_RATE = false;
                //    _enable_NBBASE_RATE = false;
                //    _enable_NLCURRENCY_RATE = false;
                //    _enable_NBCURRENCY_RATE = false;
                //}
                //else
                //{
                //    _enable_NLBASE_RATE = true;
                //    _enable_NBBASE_RATE = true;
                //    _enable_NLCURRENCY_RATE = true;
                //    _enable_NBCURRENCY_RATE = true;
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task OnChanged_CurrencyCodeAsync(string pcParam)
        {
            var loEx = new R_Exception();
            try
            {
                _journalVM.Data.CCURRENCY_CODE = pcParam;
                await _journalVM.RefreshCurrencyRate();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Print
        private void PrintBtn_Before_Open_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (JournalDTO)_conJournalNavigator.R_GetCurrentData();
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(GLM00201);
                eventArgs.PageTitle = _localizer["_pageTitlePrintPopup"];
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region UpdateStatus (delete,submit,approve,commit,close)
        private async Task JournalForm_BtnDelete()
        {
            var loEx = new R_Exception();
            try
            {
                //var loValidate = await R_MessageBox.Show("", _localizer["Q03"], R_eMessageBoxButtonType.YesNo);
                var loValidate = await R_MessageBox.Show("", "Are you sure want to delete this Recurring Journal?", R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.No)
                    goto EndBlock;

                var loData = (JournalDTO)_conJournalNavigator.R_GetCurrentData();
                var loParam = R_FrontUtility.ConvertObjectToObject<GLM00200UpdateStatusDTO>(loData);
                loParam.LAUTO_COMMIT = false;
                loParam.LUNDO_COMMIT = false;
                loParam.CNEW_STATUS = "99";

                await _journalVM.UpdateJournalStatusAsync(loParam);
                await _conJournalNavigator.R_GetEntity(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task JournalForm_BtnSubmit()
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (JournalDTO)_conJournalNavigator.R_GetCurrentData();

                if (loData.CSTATUS == "00" && int.Parse(loData.CSTART_DATE) < int.Parse(_journalVM.CSOFT_PERIOD_START_DATE.CSTART_DATE))
                {
                    loEx.Add("", "Cannot Submit Recurring Journal with Starting Date before Soft Close Period!");
                    //var loValidate = await R_MessageBox.Show("", _localizer["Q03"], R_eMessageBoxButtonType.YesNo);
                    goto EndBlock;
                }

                //confirmation
                string lcMessage = loData.CSTATUS == "10" ? "Undo submit" : "Submit";
                string lcConfirmationMessage = $"Are you sure want to {lcMessage} this journal? [Yes/No]";

                var loResult = await R_MessageBox.Show("", lcConfirmationMessage, R_eMessageBoxButtonType.YesNo);
                if (loResult == R_eMessageBoxResult.No)
                {
                    goto EndBlock;
                }

                //convert data to param
                var loParam = R_FrontUtility.ConvertObjectToObject<GLM00200UpdateStatusDTO>(loData);
                loParam.LUNDO_COMMIT = false;
                loParam.LAUTO_COMMIT = false;
                loParam.CNEW_STATUS = loData.CSTATUS == "00" ? "10" : "00";

                //update status
                await _journalVM.UpdateJournalStatusAsync(loParam);
                await _conJournalNavigator.R_GetEntity(new JournalDTO() { CJRN_ID = loParam.CREC_ID });
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task JournalForm_BtnApprove()
        {
            var loEx = new R_Exception();
            try
            {
                //get data
                var loData = (JournalDTO)_conJournalNavigator.R_GetCurrentData();

                //validate allo approve
                if (loData.LALLOW_APPROVE == false)
                {
                    loEx.Add("", "You don’t have right to approve this journal!");
                    goto EndBlock;
                }

                //validate start date
                if (int.Parse(_journalVM.StartDate.Value.ToString("yyyyMMdd")) < int.Parse(_journalVM.CSOFT_PERIOD_START_DATE.CSTART_DATE))
                {
                    loEx.Add("", "Cannot Approve Recurring Journal with Starting Date before Soft Close Period!");
                    goto EndBlock;
                }
                //var loValidate = await R_MessageBox.Show("", _localizer["Q03"], R_eMessageBoxButtonType.YesNo);
                var loValidate = await R_MessageBox.Show("", "Are you sure want to Approve this Recurring Journal?", R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.No)
                {
                    goto EndBlock;
                }

                //convert data to param
                var loParam = R_FrontUtility.ConvertObjectToObject<GLM00200UpdateStatusDTO>(loData);
                loParam.CNEW_STATUS = "20";
                loParam.LAUTO_COMMIT = _journalVM.GL_SYSTEM_PARAM.LCOMMIT_APVJRN;
                loParam.LUNDO_COMMIT = false;

                //update status
                await _journalVM.UpdateJournalStatusAsync(loParam);

                //get new data
                await _conJournalNavigator.R_GetEntity(new JournalDTO() { CJRN_ID = loParam.CREC_ID });


                //display message
                await R_MessageBox.Show("", "Recurring Journal Approved Successfully!", R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task JournalForm_BtnCommit()
        {
            var loEx = new R_Exception();
            R_eMessageBoxResult loResult;
            try
            {
                //get data
                var loData = (JournalDTO)_conJournalNavigator.R_GetCurrentData();

                if (int.Parse(loData.CSTART_DATE) < int.Parse(_journalVM.CSOFT_PERIOD_START_DATE.CSTART_DATE))
                {
                    loEx.Add("", "Cannot Commit Recurring Journal with Starting Date before Soft Close Period!");
                    goto EndBlock;
                }

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
                loParam.CNEW_STATUS = loData.CSTATUS == "80" ? (_journalVM.GSM_TRANSACTION_CODE.LAPPROVAL_FLAG ? "10" : "00") : "80";
                loParam.LAUTO_COMMIT = false;
                loParam.LUNDO_COMMIT = loData.CSTATUS == "80";

                //update journal status
                await _journalVM.UpdateJournalStatusAsync(loParam);

                //get new data
                await _conJournalNavigator.R_GetEntity(new JournalDTO() { CJRN_ID = loParam.CREC_ID });


                //show message
                await R_MessageBox.Show("", _journalVM.Data.CSTATUS == "80" ? "Recurring Journal Committed Successfully!" : "Recurring Journal Undo Committed Successfully!", R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task JournalForm_BtnClose()
        {
            var loEx = new R_Exception();
            try
            {
                //get data
                var loData = (JournalDTO)_conJournalNavigator.R_GetCurrentData();

                //confirmation
                if (loData.CSTATUS == "80")
                {
                    if (await R_MessageBox.Show("", "Are you sure want to close this recurring journal", R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.No)
                    {
                        goto EndBlock;
                    }
                }

                //convert data to param
                var loParam = R_FrontUtility.ConvertObjectToObject<GLM00200UpdateStatusDTO>(loData);
                loParam.CNEW_STATUS = "90";
                loParam.LAUTO_COMMIT = false;
                loParam.LUNDO_COMMIT = false;

                //update status
                await _journalVM.UpdateJournalStatusAsync(loParam);

                //get new data
                await _conJournalNavigator.R_SetCurrentData(new JournalDTO());
                await _gridJournalDet.R_RefreshGrid(null);


                //display message
                await R_MessageBox.Show("", "Recurring Journal Closed Successfully!", R_eMessageBoxButtonType.OK);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
