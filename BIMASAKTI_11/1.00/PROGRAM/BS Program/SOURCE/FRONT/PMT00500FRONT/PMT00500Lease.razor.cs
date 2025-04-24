using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Helpers;
using PMT00500COMMON;
using PMT00500MODEL;
using R_BlazorFrontEnd;
using Microsoft.JSInterop;
using R_APICommonDTO;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_PMFRONT;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using System;


namespace PMT00500FRONT
{
    public partial class PMT00500Lease : R_Page
    {
        private PMT00503ViewModel _viewModelProcess = new ();
        private PMT00510ViewModel _viewModel = new PMT00510ViewModel();
        private PMT00520ViewModel _viewModelUnit = new PMT00520ViewModel();
        private PMT00521ViewModel _viewModelUtility = new PMT00521ViewModel();
        private PMT00530ViewModel _viewModelCharges = new PMT00530ViewModel();

        #region Inject
        [Inject] private R_ILocalizer<PMT00500FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] public R_IExcel ExcelInject { get; set; }
        [Inject] private IClientHelper _ClientHelper { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }
        #endregion

        #region Grid & Conductor
        private R_Grid<PMT00510DTO> _gridLOIUnitListRef;
        private R_Grid<PMT00520DTO> _gridAgreementUtilitiesListRef;
        private R_Grid<PMT00530DTO> _gridAgreementChargeListRef;

        private R_ConductorGrid _conUnitGrid;
        private R_ConductorGrid _conductorGridUtilityRef;
        private R_ConductorGrid _conductorGridChargesRef;
        #endregion

        #region Private Property 
        private R_TabStrip? _tabStripRef;
        private R_DatePicker<DateTime?> RefDate_DatePicker;
        private R_TextBox RefNo_TextBox;
        private int CountCheckedUnit = 0;
        private int CountCheckedUtilities = 0;
        private int CountCheckedCharges = 0;
        #endregion

        #region Upload Method
        // Create Method Action StateHasChange
        private void StateChangeInvoke()
        {
            StateHasChanged();
        }

        // Create Method Action if proses is Complete Success
        private async Task ActionFuncIsCompleteSuccess()
        {
            await R_MessageBox.Show("", _localizer["N18"], R_eMessageBoxButtonType.OK);
            await this.Close(true, true);
        }
        // Create Method Action For Error Unhandle
        private void ShowErrorInvoke(R_APIException poEx)
        {
            var loEx = R_FrontUtility.R_ConvertFromAPIException(poEx);
            this.R_DisplayException(loEx);
        }
        #endregion
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<PMT00500DTO>(poParameter);
                var loResultData = await _viewModel.GetLOIWithResult(loData);
                
                #region Initialized Method
                // Set Param Company to viewmodel
                _viewModelProcess.CompanyID = _ClientHelper.CompanyId;
                _viewModelProcess.UserId = _ClientHelper.UserId;

                //Assign Action
                _viewModelProcess.StateChangeAction = StateChangeInvoke;
                _viewModelProcess.ShowErrorAction = ShowErrorInvoke;
                _viewModelProcess.ActionIsCompleteSuccess = ActionFuncIsCompleteSuccess;
                #endregion
                
                await _viewModelProcess.GetInitialVar();
                if (_viewModelProcess.VAR_GSM_TRANS_CODE_LOI.LINCREMENT_FLAG)
                {
                   await  RefDate_DatePicker.FocusAsync();
                }
                else
                {
                    await RefNo_TextBox.FocusAsync();
                }
                
                _viewModelProcess.LOI.CPROPERTY_ID = loResultData.CPROPERTY_ID;
                _viewModelProcess.LOI.CCHARGE_MODE = loResultData.CCHARGE_MODE;
                _viewModelProcess.LOI.CBUILDING_ID = loResultData.CBUILDING_ID;
                _viewModelProcess.LOI.CBUILDING_NAME = loResultData.CBUILDING_NAME;
                _viewModelProcess.LOI.CDEPT_CODE = loData.CDEPT_CODE;
                _viewModelProcess.LOI.CLINK_REF_NO = loData.CREF_NO;
                _viewModelProcess.LOI.CNOTES = "";
                _viewModelProcess.LOI.CREF_NO = "";
                _viewModelProcess.LOI.CREC_ID = loResultData.CREC_ID;
                await PlanStartDate_ValueChanged(DateTime.Now);
                _viewModelProcess.LOI.IYEARS = 1;
                _viewModelProcess.LOI.IDAYS = 0;
                _viewModelProcess.LOI.IMONTHS = 0;
                await PlanEndDate_ValueChanged(DateTime.Now.AddYears(1).AddDays(-1));

                _viewModelProcess.LOI.DREF_DATE = DateTime.Now;
                _viewModel.oControlYMD.LYEAR = true;
                _viewModel.oControlYMD.LMONTH = true;
                _viewModel.oControlYMD.LMONTH = true;
                if (_viewModelProcess.VAR_LEASE_MODE.Count > 0)
                {
                    _viewModelProcess.LOI.CLEASE_MODE = _viewModelProcess.VAR_LEASE_MODE[0].CCODE;
                }

                await _gridLOIUnitListRef.R_RefreshGrid(loResultData);
                if (loResultData.CCHARGE_MODE == "01")
                {
                    PMT00500AddDeleteGrid<PMT00510DTO> loGridParameter = new PMT00500AddDeleteGrid<PMT00510DTO>();
                    loGridParameter.DATA = R_FrontUtility.ConvertObjectToObject<PMT00510DTO>(loResultData);
                    loGridParameter.DATA.CUNIT_ID = "";
                    loGridParameter.DATA.CFLOOR_ID = "";
                    loGridParameter.DATA.CBUILDING_ID = "";
                
                    loGridParameter.ACTION = "ADD";
                    await _gridAgreementChargeListRef.R_RefreshGrid(loGridParameter);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region LOI Unit
        private async Task Grid_LOI_Unit_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                var loParameter = R_FrontUtility.ConvertObjectToObject<PMT00510DTO>(eventArgs.Parameter);
                await _viewModelUnit.GetLOIUnitList(loParameter);
                eventArgs.ListEntityResult = _viewModelUnit.LOIUNITGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void Grid_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            eventArgs.Result = eventArgs.Data;
        }
        private async Task Grid_Unit_CheckBoxSelectValueChanged(R_CheckBoxSelectValueChangedEventArgs eventArgs)
        {
            var loCurrentRow = (PMT00510DTO)eventArgs.CurrentRow;
            PMT00500AddDeleteGrid<PMT00510DTO> loGridParameter = new PMT00500AddDeleteGrid<PMT00510DTO>();
            loGridParameter.DATA = loCurrentRow;

            if (eventArgs.Value)
            {
                loGridParameter.ACTION = "ADD";
                CountCheckedUnit++;
            }
            else
            {
                loGridParameter.ACTION = "DELETE";
                CountCheckedUnit--;
            }

            if (loCurrentRow.CCHARGE_MODE == "02")
            {
                await _gridAgreementChargeListRef.R_RefreshGrid(loGridParameter);
            }
            
            await _gridAgreementUtilitiesListRef.R_RefreshGrid(loGridParameter);

            eventArgs.Enabled = true;
        }
        #endregion

        #region Utilities Form
        private async Task Utilities_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (PMT00500AddDeleteGrid<PMT00510DTO>)eventArgs.Parameter;   
                var loData = R_FrontUtility.ConvertObjectToObject<PMT00520DTO>(loParam.DATA);
                loData.CDEPT_CODE = _viewModelProcess.LOI.CDEPT_CODE;

                if (loParam.ACTION == "ADD")
                {
                    await _viewModelUtility.GetLOIUtilitiesListGrid(loData);
                }
                else if (loParam.ACTION == "DELETE")
                {
                    await _viewModelUtility.RemoveLOIUtilityListGrid(loData);
                }

                eventArgs.ListEntityResult = _viewModelUtility.LOIUtiliesGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void Grid_Utilities_CheckBoxSelectValueChanged(R_CheckBoxSelectValueChangedEventArgs eventArgs)
        {
            eventArgs.Enabled = true;
            if (eventArgs.Value)
            {
                CountCheckedUtilities++;
            }
            else
            {
                CountCheckedUtilities--;
            }
        }
        #endregion

        #region Charge 
        private async Task Charge_Agreement_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (PMT00500AddDeleteGrid<PMT00510DTO>)eventArgs.Parameter;   
                var loData = R_FrontUtility.ConvertObjectToObject<PMT00530DTO>(loParam.DATA);
                loData.DSTART_DATE = _viewModelProcess.LOI.DSTART_DATE;
                loData.DEND_DATE = _viewModelProcess.LOI.DEND_DATE;

                if (loParam.ACTION == "ADD")
                {
                    await _viewModelCharges.GetLOIChargeListGrid(loData);
                }
                else if (loParam.ACTION == "DELETE")
                {
                    await _viewModelCharges.RemoveLOIChargeListGrid(loData);
                }
                
                eventArgs.ListEntityResult = _viewModelCharges.LOIChargeGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void Charge_Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;
                var loData = (PMT00530DTO)eventArgs.Data;
               
                 lCancel = loData.DSTART_DATE == null;
                 if (lCancel)
                 {
                     loEx.Add(R_FrontUtility.R_GetError(
                         typeof(PMT01300FrontResources.Resources_Dummy_Class),
                         "V005"));
                 }  
                 
                 lCancel = loData.DEND_DATE == null;
                 if (lCancel)
                 {
                     loEx.Add(R_FrontUtility.R_GetError(
                         typeof(PMT01300FrontResources.Resources_Dummy_Class),
                         "V006"));
                 }

                 if (loData.DEND_DATE != null && loData.DSTART_DATE != null)
                 {
                     int liStartDate = int.Parse(loData.DSTART_DATE.Value.ToString("yyyyMMdd"));
                     int liEndDate = int.Parse(loData.DEND_DATE.Value.ToString("yyyyMMdd"));
   
                     int liPlanEndDate = int.Parse(_viewModelProcess.LOI.CEND_DATE);
                     int liPlanStartDate = int.Parse(_viewModelProcess.LOI.CSTART_DATE);

                     lCancel = liStartDate > liEndDate;
                     if (lCancel)
                     {
                         loEx.Add(R_FrontUtility.R_GetError(
                             typeof(PMT01300FrontResources.Resources_Dummy_Class),
                             "V015"));
                     }  

                     if (loData.CBILLING_MODE == "01")
                     {
                         if (liEndDate > liPlanEndDate)
                         {
                             loEx.Add(R_FrontUtility.R_GetError(
                                 typeof(PMT01300FrontResources.Resources_Dummy_Class),
                                 "V040"));
                         }
                     }
                     else
                     {
                         if (liStartDate < liPlanStartDate && liEndDate > liPlanEndDate)
                         {
                             loEx.Add(R_FrontUtility.R_GetError(
                                 typeof(PMT01300FrontResources.Resources_Dummy_Class),
                                 "V037"));
                         }
                     }   
                     if (loData.CFEE_METHOD == "03")
                     {
                         lCancel = (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS != 0);
                         if (lCancel == false)
                         {
                             loEx.Add(R_FrontUtility.R_GetError(
                                 typeof(PMT01300FrontResources.Resources_Dummy_Class),
                                 "V035"));
                         }
                     }
                 }  
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            eventArgs.Cancel = loEx.HasError;
        }
        private void Grid_Charges_CheckBoxSelectValueChanged(R_CheckBoxSelectValueChangedEventArgs eventArgs)
        {
            eventArgs.Enabled = true;
            if (eventArgs.Value)
            {
                CountCheckedCharges++;
            }
            else
            {
                CountCheckedCharges--;
            }
        }
        #endregion
        
        #region Btn Process
        public async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();
            bool lCancel = false;

            try
            {
                var loData = (PMT00500DTO)_viewModelProcess.LOI;

                lCancel = string.IsNullOrWhiteSpace(loData.CREF_NO) && _viewModelProcess.VAR_GSM_TRANS_CODE_LOI.LINCREMENT_FLAG == false;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "V003"));
                }
                
                lCancel = loData.DREF_DATE == null;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "V004"));
                }
                else
                {
                    if (int.Parse(loData.DREF_DATE.Value.ToString("yyyyMMdd")) > int.Parse(DateTime.Now.ToString("yyyyMMdd")))
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(PMT00500FrontResources.Resources_Dummy_Class),
                            "V033"));
                    }
                }
                
                lCancel = string.IsNullOrWhiteSpace(loData.CTENANT_ID);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "V017"));
                }
                
                lCancel = loData.DSTART_DATE == null;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "V045"));
                }
                
                lCancel = loData.DEND_DATE == null;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT01300FrontResources.Resources_Dummy_Class),
                        "V046"));
                }
                
                if (loData.DEND_DATE != null && loData.DSTART_DATE != null)
                {
                    lCancel = int.Parse(loData.DSTART_DATE.Value.ToString("yyyyMMdd")) > int.Parse(loData.DEND_DATE.Value.ToString("yyyyMMdd"));
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(PMT01300FrontResources.Resources_Dummy_Class),
                            "V048"));
                    }
                }

                lCancel = string.IsNullOrWhiteSpace(loData.CLEASE_MODE);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT01300FrontResources.Resources_Dummy_Class),
                        "V010"));
                }
                
                lCancel = string.IsNullOrWhiteSpace(loData.CUNIT_DESCRIPTION);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "V008"));
                }

                if (CountCheckedUnit == 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT00500FrontResources.Resources_Dummy_Class),
                        "V049"));
                    lCancel = true;
                }
                else
                {
                    if (CountCheckedUtilities == 0)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(PMT00500FrontResources.Resources_Dummy_Class),
                            "V050"));
                        lCancel = true;
                    }

                    if (_gridAgreementChargeListRef.DataSource.Count != 0)
                    {
                        if (_gridAgreementChargeListRef.DataSource.Any(x => x.DSTART_DATE == null || x.DEND_DATE == null))
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                typeof(PMT00500FrontResources.Resources_Dummy_Class),
                                "V052"));
                            lCancel = true;
                        }
                        
                        if (_viewModelProcess.LOI.CCHARGE_MODE == "01")
                        {
                            if (CountCheckedUnit == 1 && CountCheckedCharges == 0)
                            {
                                loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(PMT00500FrontResources.Resources_Dummy_Class),
                                    "V051"));
                                lCancel = true;
                            }
                        }
                        else
                        {
                            if (CountCheckedCharges == 0)
                            {
                                loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(PMT00500FrontResources.Resources_Dummy_Class),
                                    "V051"));
                                lCancel = true;
                            }
                        }
                    }
                    else
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(PMT00500FrontResources.Resources_Dummy_Class),
                            "V053"));
                        lCancel = true;
                    }
                }
                
                if (lCancel == false)
                {
                    loData.CREF_DATE = loData.DREF_DATE.Value.ToString("yyyyMMdd");
                    loData.CSTART_DATE = loData.DSTART_DATE.Value.ToString("yyyyMMdd");
                    loData.CEND_DATE = loData.DEND_DATE.Value.ToString("yyyyMMdd");

                    await Task.Delay(1);
                    var loUnitList = _gridLOIUnitListRef.DataSource.Where(x => x.LCHECKED).ToList();
                    var loUtilityList = _gridAgreementUtilitiesListRef.DataSource.Where(x => x.LCHECKED).ToList();
                    List<PMT00530DTO> loChargesList = null;
                    if (_viewModelProcess.LOI.CCHARGE_MODE == "01")
                    {
                        if (CountCheckedUnit == 1)
                        {
                            loChargesList = _gridAgreementChargeListRef.DataSource.Where(x => x.LCHECKED).ToList();
                        }
                        else
                        {
                            loChargesList = _gridAgreementChargeListRef.DataSource.ToList();
                        }
                    }
                    else if (_viewModelProcess.LOI.CCHARGE_MODE == "02")
                    {
                        loChargesList = _gridAgreementChargeListRef.DataSource.Where(x => x.LCHECKED).ToList();
                    }
                    await _viewModelProcess.ProcessLeaseBatch(loUnitList, loUtilityList, loChargesList);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, false);
        }
        #endregion

        #region Value Change
        DateTime tStartDate = DateTime.Now.AddDays(-1);
        private async Task PlanStartDate_ValueChanged(DateTime? poParam)
        {
            R_Exception loException = new R_Exception();
            _viewModelProcess.LOI.DSTART_DATE = poParam;

            try
            {
                var loData = _viewModelProcess.LOI;
                if (poParam.HasValue)
                {
                    DateTime adjustedValue = new DateTime(poParam.Value.Year, poParam.Value.Month, poParam.Value.Day, poParam.Value.Hour, 0, 0);
                    loData.DSTART_DATE = poParam;
                }

                tStartDate = poParam ?? DateTime.Now;

                if (loData.DEND_DATE == null)
                {
                    loData.DEND_DATE = loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0
                        ? loData.DSTART_DATE
                        : loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
                else
                {
                    CalculateYMD(poStartDate: loData.DSTART_DATE, poEndDate: loData.DEND_DATE, plStart: true);
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);


        }
        private void YearInt_ValueChanged(int poParam)
        {
            var loData = _viewModelProcess.LOI;
            var llControl = _viewModel.oControlYMD;
            loData.IYEARS = poParam;

            if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
            {
                if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                {
                    loData.DEND_DATE = loData.DSTART_DATE;
                }
                else
                {
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
            }
            else
            {
                llControl.LYEAR = true;
                loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddDays(-1);
            }

        }
        private void MonthInt_ValueChanged(int poParam)
        {
            _viewModelProcess.LOI.IMONTHS = poParam;
            var loData = _viewModelProcess.LOI;
            var llControl = _viewModel.oControlYMD;

            if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
            {
                if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                {
                    loData.DEND_DATE = loData.DSTART_DATE;
                }
                else
                {
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
            }
            else
            {
                llControl.LMONTH = true;
                loData.DEND_DATE = loData.DSTART_DATE!.Value.AddMonths(loData.IMONTHS).AddDays(-1);
            }

        }
        private void DayInt_ValueChanged(int poParam)
        {
            _viewModelProcess.LOI.IDAYS = poParam;
            var loData = _viewModelProcess.LOI;
            var llControl = _viewModel.oControlYMD;

            if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
            {
                if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                {
                    loData.DEND_DATE = loData.DSTART_DATE;
                }
                else
                {
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
            }
            else
            {
                llControl.LYEAR = true;
                loData.DEND_DATE = loData.DSTART_DATE!.Value.AddDays(loData.IDAYS).AddDays(-1);
            }

        }
        private async Task PlanEndDate_ValueChanged(DateTime? poParam)
        {
            R_Exception loException = new R_Exception();

            try
            {
                _viewModelProcess.LOI.DEND_DATE = poParam;
                var loData = _viewModelProcess.LOI;

                if (loData.DSTART_DATE == null)
                {
                    loData.DSTART_DATE = loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0
                        ? loData.DEND_DATE
                        : loData.DEND_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                }
                else
                {
                    CalculateYMD(poStartDate: loData.DSTART_DATE, poEndDate: loData.DEND_DATE);
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }
        #endregion
        
        #region Helper
        private void CalculateYMD(DateTime? poStartDate, DateTime? poEndDate, bool plStart = false)
        {
            R_Exception loException = new R_Exception();
            var loData = _viewModelProcess.LOI;

            try
            {
                if (poEndDate != null && poStartDate != null)
                {
                    if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                    {
                        loData.IDAYS = 1;
                        loData.IMONTHS = loData.IYEARS = 0;
                        if (plStart)
                            loData.DSTART_DATE = loData.DEND_DATE;
                        else
                            loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        if (plStart)
                        {
                            loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                        }
                        else
                        {
                            loData.DSTART_DATE = loData.DEND_DATE!.Value.AddYears(-loData.IYEARS).AddMonths(-loData.IMONTHS).AddDays(-loData.IDAYS).AddDays(1);
                        }
                    }
                }
                else
                {
                    if (poStartDate != null)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddDays(1);
                        loData.IYEARS = loData.IMONTHS = 0;
                        loData.IDAYS = 2;
                    }
                }

            }

            //loData.IYEARS = dValueEndDate.Year - poStartDate!.Value.Year;
            //loData.IMONTHS = dValueEndDate.Month - poStartDate!.Value.Month;}
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            //EndBlocks:

            R_DisplayException(loException);
        }
        #endregion
        
        #region Tenant Lookup
        private async Task Tenant_OnLostFocus()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrWhiteSpace(_viewModelProcess.LOI.CTENANT_ID) == false)
                {
                    LML00600ParameterDTO loParam = new LML00600ParameterDTO()
                    {
                        CCOMPANY_ID = "",
                        CPROPERTY_ID = _viewModelProcess.LOI.CPROPERTY_ID,
                        CUSER_ID = "",
                        CCUSTOMER_TYPE = "01",
                        CSEARCH_TEXT = _viewModelProcess.LOI.CTENANT_ID
                    };

                    LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel();

                    var loResult = await loLookupViewModel.GetTenant(loParam);

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        _viewModelProcess.LOI.CTENANT_NAME = "";
                        goto EndBlock;
                    }
                    _viewModelProcess.LOI.CTENANT_ID = loResult.CTENANT_ID;
                    _viewModelProcess.LOI.CTENANT_NAME = loResult.CTENANT_NAME;
                }
                else
                {
                    _viewModelProcess.LOI.CTENANT_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
        }
        private void Tenant_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00600ParameterDTO loParam = new LML00600ParameterDTO()
            {
                CCOMPANY_ID = "",
                CPROPERTY_ID = _viewModelProcess.LOI.CPROPERTY_ID,
                CCUSTOMER_TYPE = "01",
                CUSER_ID = ""
            };
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(LML00600);
        }
        private void Tenant_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                LML00600DTO loTempResult = (LML00600DTO)eventArgs.Result;
                if (loTempResult == null)
                {
                    return;
                }
                _viewModelProcess.LOI.CTENANT_ID = loTempResult.CTENANT_ID;
                _viewModelProcess.LOI.CTENANT_NAME = loTempResult.CTENANT_NAME;
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
