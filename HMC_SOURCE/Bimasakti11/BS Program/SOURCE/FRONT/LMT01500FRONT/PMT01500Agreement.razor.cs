using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls;
using PMT01500Model.ViewModel;
using PMT01500Common.DTO._2._Agreement;
using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using PMT01500Common.Utilities;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using PMT01500FrontResources;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Controls.MessageBox;
using Lookup_GSModel.ViewModel;
using Lookup_PMModel.ViewModel.LML00500;
using Lookup_PMModel.ViewModel.LML00600;
using PMT01500Common.Utilities.Front;
using R_BlazorFrontEnd.Controls.Enums;
using R_LockingFront;

namespace PMT01500Front
{
    public partial class PMT01500Agreement
    {
        private R_Grid<PMT01500FrontAgreementDetailDTO>? _gridRefPMT01500Agreement;
        private R_Conductor? _conductorFullPMT01500Agreement;
        private readonly PMT01500AgreementViewModel _viewModelPMT01500Agreement = new();
        //private R_TabStrip? _tabStripRef;
        private bool _lDataCREF_NO = false;
        private bool _isCheckerDataFound;

        PMT01500EventCallBackDTO _oEventCallBack = new PMT01500EventCallBackDTO();

        [Inject] private IClientHelper? _clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModelPMT01500Agreement.loParameter = R_FrontUtility.ConvertObjectToObject<PMT01500GetHeaderParameterDTO>(poParameter);
                _lDataCREF_NO = false;

                var test = _conductorFullPMT01500Agreement;

                await _viewModelPMT01500Agreement.GetComboBoxDataCLeaseMode();
                await _viewModelPMT01500Agreement.GetComboBoxDataCChargesMode();


                if (!string.IsNullOrEmpty(_viewModelPMT01500Agreement.loParameter.CREF_NO))
                {
                    _isCheckerDataFound = true;
                    await _conductorFullPMT01500Agreement.R_GetEntity(null);

                    switch (_viewModelPMT01500Agreement.loParameter.DataAgreement.CMODE)
                    {
                        case "EDIT":
                            await _conductorFullPMT01500Agreement.Edit();
                            break;
                        case "ADD":
                            await _conductorFullPMT01500Agreement.Add();
                            break;
                        default:
                            break;
                    }
                }
                else if ((_viewModelPMT01500Agreement.loParameter.DataAgreement.CMODE == "ADD") && (string.IsNullOrEmpty(_viewModelPMT01500Agreement.loParameter.CREF_NO)))
                {
                    await _conductorFullPMT01500Agreement.Add();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region Master HandleFunction

        private R_TextBox? _componentCBUILDING_IDTextBox;
        private R_TextBox? _componentCDOC_NOTextBox;
        private R_TextBox? _componentCREF_NOTextBox;

        private void OnChangedCYEAR(Int32 poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01500FrontAgreementDetailDTO)_viewModelPMT01500Agreement.Data;
                var llControl = _viewModelPMT01500Agreement._oControlYMD;
                loData.IYEAR = poParam;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAY == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAY).AddDays(-1);
                    }
                }
                else
                {
                    llControl.LYEAR = true;
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEAR).AddDays(-1);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void OnChangedCMONTH(Int32 poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01500FrontAgreementDetailDTO)_viewModelPMT01500Agreement.Data;
                var llControl = _viewModelPMT01500Agreement._oControlYMD;
                loData.IMONTH = poParam;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAY == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAY).AddDays(-1);
                    }
                }
                else
                {
                    llControl.LMONTH = true;
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddMonths(loData.IMONTH).AddDays(-1);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void OnChangedCDAY(Int32 poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01500FrontAgreementDetailDTO)_viewModelPMT01500Agreement.Data;
                var llControl = _viewModelPMT01500Agreement._oControlYMD;
                loData.IDAY = poParam;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAY == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAY).AddDays(-1);
                    }
                }
                else
                {
                    llControl.LYEAR = true;
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddDays(loData.IDAY).AddDays(-1);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void OnChangedDSTART_DATE(DateTime? poValue)
        {
            R_Exception loException = new R_Exception();

            try
            {
                PMT01500FrontAgreementDetailDTO loData = _viewModelPMT01500Agreement.Data;
                loData.DSTART_DATE = poValue;

                if (loData.DEND_DATE == null)
                {
                    loData.DEND_DATE = loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAY == 0
                        ? loData.DSTART_DATE
                        : loData.DSTART_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAY).AddDays(-1);
                }
                else
                {
                    CalculateYMD(poStartDate: loData.DSTART_DATE, poEndDate: loData.DEND_DATE, pcStart: "S");
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private void OnChangedDEND_DATE(DateTime? poValue)
        {
            R_Exception loException = new R_Exception();

            try
            {
                PMT01500FrontAgreementDetailDTO loData = _viewModelPMT01500Agreement.Data;
                loData.DEND_DATE = poValue;

                if (loData.DSTART_DATE == null)
                {
                    loData.DSTART_DATE = loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAY == 0
                        ? loData.DEND_DATE
                        : loData.DEND_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAY).AddDays(-1);
                }
                else
                {
                    CalculateYMD(poStartDate: loData.DSTART_DATE, poEndDate: loData.DEND_DATE);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private void CalculateYMD(DateTime? poStartDate, DateTime? poEndDate, string pcStart = "")
        {
            R_Exception loException = new R_Exception();
            PMT01500FrontAgreementDetailDTO loData = _viewModelPMT01500Agreement.Data;

            try
            {
                DateTime dValueEndDate = poEndDate!.Value.AddDays(1);

                int liChecker = poEndDate!.Value.Day - poStartDate!.Value.Day;
                if (liChecker < 0)
                {
                    loData.IDAY = 1;
                    loData.IMONTH = loData.IYEAR = 0;
                    if (string.IsNullOrEmpty(pcStart))
                        loData.DSTART_DATE = loData.DEND_DATE;
                    else
                        loData.DEND_DATE = loData.DSTART_DATE;

                }
                else
                {

                    loData.IDAY = dValueEndDate.Day - poStartDate!.Value.Day;
                    if (loData.IDAY < 0)
                    {
                        DateTime dValueEndDateForHandleDay = dValueEndDate.AddMonths(-1);
                        int liTempDayinMonth = DateTime.DaysInMonth(dValueEndDateForHandleDay.Year, dValueEndDateForHandleDay.Month);
                        loData.IDAY = liTempDayinMonth + loData.IDAY;
                        if (loData.IDAY < 0) { throw new Exception("ERROR HARINYA MINES"); }
                        loData.IMONTH = dValueEndDateForHandleDay.Month - poStartDate!.Value.Month;
                        if (loData.IMONTH < 0)
                        {
                            loData.IMONTH = 12 + loData.IMONTH;
                            DateTime dValueEndDateForHandleMonth = dValueEndDate.AddYears(-1);
                            loData.IYEAR = dValueEndDateForHandleMonth.Year - poStartDate!.Value.Year;
                            if (loData.IYEAR < 0)
                            {
                                loData.IYEAR = 0;
                            }
                        }

                    }
                    else
                    {
                        loData.IMONTH = dValueEndDate.Month - poStartDate!.Value.Month;
                        if (loData.IMONTH < 0)
                        {
                            loData.IMONTH = 12 + loData.IMONTH;
                            DateTime dValueEndDateForHandleMonth = dValueEndDate.AddYears(-1);
                            loData.IYEAR = dValueEndDateForHandleMonth.Year - poStartDate!.Value.Year;
                            if (loData.IYEAR < 0)
                            {
                                loData.IYEAR = 0;
                            }
                        }
                        else
                        {
                            loData.IYEAR = dValueEndDate.Year - poStartDate!.Value.Year;
                        }
                    }
                }
            }

            //loData.IYEAR = dValueEndDate.Year - poStartDate!.Value.Year;
            //loData.IMONTH = dValueEndDate.Month - poStartDate!.Value.Month;}
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        #endregion


        #region Locking

        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_MODULE_NAME = "PM";

        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult? loLockResult = null;

            try
            {
                var loData = (PMT01500FrontAgreementDetailDTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT01500",
                        Table_Name = "PMT_AGREEMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CREF_NO)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT01500",
                        Table_Name = "PMT_AGREEMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CREF_NO)
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

        #region Master UtilitiesConductor

        private async Task AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                _lDataCREF_NO = !_viewModelPMT01500Agreement.loParameter.DataAgreement.LINCREMENT_FLAGFORCREF_NO;
                var loData = (PMT01500FrontAgreementDetailDTO)eventArgs.Data;
                loData.CTRANS_CODE = "802030";
                loData.CTRANS_NAME = "Lease Agreement";
                loData.DSTART_DATE = DateTime.Now;
                loData.DEND_DATE = DateTime.Now.AddYears(1).AddDays(-1);
                loData.IYEAR = 1;
                _viewModelPMT01500Agreement._oControlYMD.LYEAR = true;
                /*
                loData.IDAY = loData.DEND_DATE.Day.ToString("00");
                loData.IMONTH = loData.DEND_DATE.Month.ToString("00");
                loData.IYEAR = loData.DEND_DATE.Year.ToString();
                */
                loData.CLEASE_MODE = _viewModelPMT01500Agreement.loComboBoxDataCLeaseMode.First().CCODE;
                loData.CCHARGE_MODE = _viewModelPMT01500Agreement.loComboBoxDataCChargesMode.First().CCODE;
                loData.DREF_DATE = DateTime.Now;
                loData.DDOC_DATE = DateTime.Now;

                await _componentCBUILDING_IDTextBox.FocusAsync();

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private async Task AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            var loException = new R_Exception();
            //Task loTask;

            try
            {
                _isCheckerDataFound = true;
                _oEventCallBack.LContractorOnCRUDmode = true;
                _oEventCallBack.LACTIVEUnitInfoHasData = true;
                _oEventCallBack.CFlagUnitInfoHasData = "A_ADD";//Meaning of Agreement Add
                _oEventCallBack.CDEPT_CODE = _viewModelPMT01500Agreement.Data.CDEPT_CODE!;
                _oEventCallBack.CREF_NO = _viewModelPMT01500Agreement.Data.CREF_NO!;
                _oEventCallBack.CTRANS_CODE = _viewModelPMT01500Agreement.Data.CTRANS_CODE!;
                _oEventCallBack.CBUILDING_ID = _viewModelPMT01500Agreement.Data.CBUILDING_ID!;
                _oEventCallBack.CCHARGE_MODE = _viewModelPMT01500Agreement.Data.CCHARGE_MODE!;
                _oEventCallBack.CCURRENCY_CODE = _viewModelPMT01500Agreement.Data.CCURRENCY_CODE!;

                // Lakukan pemanggilan async
                await InvokeTabEventCallbackAsync(_oEventCallBack);

                // Setelah pemanggilan selesai, lanjutkan dengan kode selanjutnya
                _oEventCallBack.CFlagUnitInfoHasData = "";
            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }

            R_DisplayException(loException);

        }

        private void R_SetEdit(R_SetEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                if (eventArgs.Enable)
                    _lDataCREF_NO = false;
                _lDataCREF_NO = false;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task R_SetOther(R_SetEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                _viewModelPMT01500Agreement._oControlYMD.LYEAR = false;
                _viewModelPMT01500Agreement._oControlYMD.LMONTH = false;
                _viewModelPMT01500Agreement._oControlYMD.LDAY = false;
                _oEventCallBack.LContractorOnCRUDmode = eventArgs.Enable;
                //_oEventCallBack.CREF_NO = _viewModelPMT01500Agreement.loEntityPMT01500AgreementDetail.CREF_NO!;
                await InvokeTabEventCallbackAsync(_oEventCallBack);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        public async Task AfterDelete()
        {


            if (!_viewModelPMT01500Agreement.HasData)
            {
                _oEventCallBack.CFlagUnitInfoHasData = "A_DELETE";//Meaning of Agreement Add
                await InvokeTabEventCallbackAsync(_oEventCallBack);

                // Setelah pemanggilan selesai, lanjutkan dengan kode selanjutnya
                _oEventCallBack.CFlagUnitInfoHasData = "";
            }
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }

        #endregion

        #region Master CRUD

        private async Task ServiceR_Display(R_DisplayEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                switch (eventArgs.ConductorMode)
                {
                    case R_eConductorMode.Edit:
                        await _componentCDOC_NOTextBox.FocusAsync();
                        OnChangedDEND_DATE(_viewModelPMT01500Agreement.Data.DEND_DATE);
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }

        private async Task ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            PMT01500FrontAgreementDetailDTO loParam;

            try
            {
                loParam = new PMT01500FrontAgreementDetailDTO();
                if (eventArgs.Data != null)
                {
                    loParam = R_FrontUtility.ConvertObjectToObject<PMT01500FrontAgreementDetailDTO>(eventArgs.Data);
                }
                else
                {
                    loParam.CREF_NO = _viewModelPMT01500Agreement.loParameter.CREF_NO;
                    loParam.CPROPERTY_ID = _viewModelPMT01500Agreement.loParameter.CPROPERTY_ID;
                    loParam.CDEPT_CODE = _viewModelPMT01500Agreement.loParameter.CDEPT_CODE;
                    loParam.CTRANS_CODE = "802030";
                    loParam.CCOMPANY_ID = _clientHelper.CompanyId;
                    loParam.CUSER_ID = _clientHelper.UserId;
                };
                await _viewModelPMT01500Agreement.GetEntity(loParam);

                if (_viewModelPMT01500Agreement.loEntityPMT01500AgreementDetail.DEND_DATE != null)
                {
                    OnChangedDEND_DATE(_viewModelPMT01500Agreement.loEntityPMT01500AgreementDetail.DEND_DATE);
                }

                eventArgs.Result = _viewModelPMT01500Agreement.loEntityPMT01500AgreementDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01500FrontAgreementDetailDTO>(eventArgs.Data);

                if ((R_eConductorMode)eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    loParam.CPROPERTY_ID = _viewModelPMT01500Agreement.loParameter.CPROPERTY_ID;
                }

                await _viewModelPMT01500Agreement.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModelPMT01500Agreement.loEntityPMT01500AgreementDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01500FrontAgreementDetailDTO)eventArgs.Data;

                await _viewModelPMT01500Agreement.GetEntity(loData);

                if (_viewModelPMT01500Agreement.loEntityPMT01500AgreementDetail != null)
                    await _viewModelPMT01500Agreement.ServiceDelete(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Utilities Conductor 

        private void R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                var loData = (PMT01500FrontAgreementDetailDTO)eventArgs.Data;
                //await LostFocusCUNIT_ID();

                if (_lDataCREF_NO)
                {
                    if (string.IsNullOrWhiteSpace(loData.CREF_NO))
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01500_Class), "ValidationAgreementNo");
                        loException.Add(loErr);
                    }
                }

                if (string.IsNullOrWhiteSpace(loData.CBUILDING_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01500_Class), "ValidationBuilding");
                    loException.Add(loErr);
                }

                if (string.IsNullOrWhiteSpace(loData.CDEPT_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01500_Class), "ValidationDepartment");
                    loException.Add(loErr);
                }

                if (string.IsNullOrWhiteSpace(loData.CSALESMAN_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01500_Class), "ValidationSalesman");
                    loException.Add(loErr);
                }

                if (string.IsNullOrWhiteSpace(loData.CTENANT_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01500_Class), "ValidationTenant");
                    loException.Add(loErr);
                }

                if (string.IsNullOrWhiteSpace(loData.CUNIT_DESCRIPTION))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01500_Class), "ValidationUnitDescription");
                    loException.Add(loErr);
                }

                if (string.IsNullOrWhiteSpace(loData.CCURRENCY_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01500_Class), "ValidationCurrency");
                    loException.Add(loErr);
                }

                if (string.IsNullOrWhiteSpace(loData.CLEASE_MODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01500_Class), "ValidationLeaseMode");
                    loException.Add(loErr);
                }

                if (string.IsNullOrWhiteSpace(loData.CCHARGE_MODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01500_Class), "ValidationChargeMode");
                    loException.Add(loErr);
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            eventArgs.Cancel = loException.HasError;


            loException.ThrowExceptionIfErrors();
        }

        #endregion

        #endregion

        #region Master LookUp!

        #region Lookup Button Building Lookup

        private R_Lookup? R_LookupBuildingLookup;

        private void BeforeOpenLookUpBuildingLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL02200ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModelPMT01500Agreement.loParameter.CPROPERTY_ID))
            {
                param = new GSL02200ParameterDTO
                {
                    CPROPERTY_ID = _viewModelPMT01500Agreement.loParameter.CPROPERTY_ID
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL02200);
        }

        private void AfterOpenLookUpBuildingLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL02200DTO? loTempResult = null;
            //PMT01500FrontAgreementDetailDTO? loGetData = null;

            try
            {
                loTempResult = (GSL02200DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;
                //loGetData = (PMT01500FrontAgreementDetailDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModelPMT01500Agreement.Data.CBUILDING_ID = loTempResult.CBUILDING_ID;
                _viewModelPMT01500Agreement.Data.CBUILDING_NAME = loTempResult.CBUILDING_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusBuilding()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT01500FrontAgreementDetailDTO loGetData = (PMT01500FrontAgreementDetailDTO)_viewModelPMT01500Agreement.Data;

                if (string.IsNullOrWhiteSpace(_viewModelPMT01500Agreement.Data.CBUILDING_ID))
                {
                    loGetData.CBUILDING_ID = "";
                    loGetData.CBUILDING_NAME = "";
                    return;
                }

                LookupGSL02200ViewModel loLookupViewModel = new LookupGSL02200ViewModel();
                GSL02200ParameterDTO loParam = new GSL02200ParameterDTO()
                {
                    CPROPERTY_ID = _viewModelPMT01500Agreement.loParameter.CPROPERTY_ID!,
                    CSEARCH_TEXT = loGetData.CBUILDING_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetBuilding(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CBUILDING_ID = "";
                    loGetData.CBUILDING_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CBUILDING_ID = loResult.CBUILDING_ID;
                    loGetData.CBUILDING_NAME = loResult.CBUILDING_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region Lookup Button Department Lookup

        private R_Lookup? R_LookupDepartmentLookup;

        private void BeforeOpenLookUpDepartmentLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00700ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModelPMT01500Agreement.loParameter.CPROPERTY_ID))
            {
                param = new GSL00700ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }

        private void AfterOpenLookUpDepartmentLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL00700DTO? loTempResult = null;
            //PMT01500FrontAgreementDetailDTO? loGetData = null;


            try
            {
                loTempResult = (GSL00700DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (PMT01500FrontAgreementDetailDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModelPMT01500Agreement.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
                _viewModelPMT01500Agreement.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusDepartment()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT01500FrontAgreementDetailDTO loGetData = (PMT01500FrontAgreementDetailDTO)_viewModelPMT01500Agreement.Data;

                if (string.IsNullOrWhiteSpace(_viewModelPMT01500Agreement.Data.CDEPT_CODE))
                {
                    loGetData.CDEPT_CODE = "";
                    loGetData.CDEPT_NAME = "";
                    return;
                }

                LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();
                GSL00700ParameterDTO loParam = new GSL00700ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CSEARCH_TEXT = loGetData.CDEPT_CODE ?? "",
                };

                var loResult = await loLookupViewModel.GetDepartment(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CDEPT_CODE = "";
                    loGetData.CDEPT_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CDEPT_CODE = loResult.CDEPT_CODE;
                    loGetData.CDEPT_NAME = loResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region Lookup Button Salesman Lookup

        private R_Lookup? R_LookupSalesmanLookup;

        private void BeforeOpenLookUpSalesmanLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00500ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModelPMT01500Agreement.loParameter.CPROPERTY_ID))
            {
                param = new LML00500ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModelPMT01500Agreement.loParameter.CPROPERTY_ID,
                    CUSER_ID = _clientHelper.UserId
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00500);
        }

        private void AfterOpenLookUpSalesmanLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML00500DTO? loTempResult = null;
            //PMT01500FrontAgreementDetailDTO? loGetData = null;


            try
            {
                loTempResult = (LML00500DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (PMT01500FrontAgreementDetailDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModelPMT01500Agreement.Data.CSALESMAN_ID = loTempResult.CSALESMAN_ID;
                _viewModelPMT01500Agreement.Data.CSALESMAN_NAME = loTempResult.CSALESMAN_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusSalesman()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT01500FrontAgreementDetailDTO loGetData = (PMT01500FrontAgreementDetailDTO)_viewModelPMT01500Agreement.Data;

                if (string.IsNullOrWhiteSpace(_viewModelPMT01500Agreement.Data.CSALESMAN_ID))
                {
                    loGetData.CSALESMAN_ID = "";
                    loGetData.CSALESMAN_NAME = "";
                    return;
                }

                LookupLML00500ViewModel loLookupViewModel = new LookupLML00500ViewModel();
                LML00500ParameterDTO loParam = new LML00500ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModelPMT01500Agreement.loParameter.CPROPERTY_ID!,
                    CUSER_ID = _clientHelper.UserId,
                    CSEARCH_TEXT = loGetData.CSALESMAN_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetSalesman(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CSALESMAN_ID = "";
                    loGetData.CSALESMAN_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CSALESMAN_ID = loResult.CSALESMAN_ID;
                    loGetData.CSALESMAN_NAME = loResult.CSALESMAN_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region Lookup Button Tenant Lookup

        private R_Lookup? R_LookupTenantLookup;

        private void BeforeOpenLookUpTenantLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00600ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModelPMT01500Agreement.loParameter.CPROPERTY_ID))
            {
                param = new LML00600ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModelPMT01500Agreement.loParameter.CPROPERTY_ID,
                    CCUSTOMER_TYPE = "01",
                    CUSER_ID = _clientHelper.UserId
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00600);
        }

        private void AfterOpenLookUpTenantLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML00600DTO? loTempResult = null;
            //PMT01500FrontAgreementDetailDTO? loGetData = null;


            try
            {
                loTempResult = (LML00600DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (PMT01500FrontAgreementDetailDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModelPMT01500Agreement.Data.CTENANT_ID = loTempResult.CTENANT_ID;
                _viewModelPMT01500Agreement.Data.CTENANT_NAME = loTempResult.CTENANT_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusTenant()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT01500FrontAgreementDetailDTO loGetData = (PMT01500FrontAgreementDetailDTO)_viewModelPMT01500Agreement.Data;

                if (string.IsNullOrWhiteSpace(_viewModelPMT01500Agreement.Data.CTENANT_ID))
                {
                    loGetData.CTENANT_ID = "";
                    loGetData.CTENANT_NAME = "";
                    return;
                }

                LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel();
                LML00600ParameterDTO loParam = new LML00600ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModelPMT01500Agreement.loParameter.CPROPERTY_ID!,
                    CCUSTOMER_TYPE = "01",
                    CUSER_ID = _clientHelper.UserId,
                    CSEARCH_TEXT = loGetData.CTENANT_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetTenant(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CTENANT_ID = "";
                    loGetData.CTENANT_NAME = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CTENANT_ID = loResult.CTENANT_ID;
                    loGetData.CTENANT_NAME = loResult.CTENANT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region Lookup Button Currency Lookup

        private R_Lookup? R_LookupCurrencyLookup;

        private void BeforeOpenLookUpCurrencyLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00300ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModelPMT01500Agreement.loParameter.CPROPERTY_ID))
            {
                param = new GSL00300ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00300);
        }

        private void AfterOpenLookUpCurrencyLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL00300DTO? loTempResult = null;
            //PMT01500FrontAgreementDetailDTO? loGetData = null;

            try
            {
                loTempResult = (GSL00300DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (PMT01500FrontAgreementDetailDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModelPMT01500Agreement.Data.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusCurrency()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT01500FrontAgreementDetailDTO loGetData = (PMT01500FrontAgreementDetailDTO)_viewModelPMT01500Agreement.Data;

                if (string.IsNullOrWhiteSpace(_viewModelPMT01500Agreement.Data.CCURRENCY_CODE))
                {
                    loGetData.CCURRENCY_CODE = "";
                    return;
                }

                LookupGSL00300ViewModel loLookupViewModel = new LookupGSL00300ViewModel();
                GSL00300ParameterDTO loParam = new GSL00300ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CSEARCH_TEXT = loGetData.CCURRENCY_CODE ?? "",
                };

                var loResult = await loLookupViewModel.GetCurrency(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CCURRENCY_CODE = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CCURRENCY_CODE = loResult.CCURRENCY_CODE;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #endregion

    }
}
