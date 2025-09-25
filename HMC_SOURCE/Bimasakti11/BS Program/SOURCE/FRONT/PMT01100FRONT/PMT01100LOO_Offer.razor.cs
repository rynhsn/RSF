using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00500;
using Lookup_PMModel.ViewModel.LML00600;
using Microsoft.AspNetCore.Components;
using PMT01100Common.DTO._1._Unit_List;
using PMT01100Common.DTO._2._LOO._2._LOO___Offer;
using PMT01100Common.Utilities;
using PMT01100Common.Utilities.Front;
using PMT01100FrontResources;
using PMT01100Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Validation;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System.Text.Json;

namespace PMT01100Front
{
    public partial class PMT01100LOO_Offer : R_Page
    {

        private readonly PMT01100LOO_OfferViewModel _viewModel = new();
        private R_Conductor? _conductor;
        private R_Grid<PMT01100LOO_Offer_SelectedOfferDTO>? _gridRef;

        //PMT01500EventCallBackDTO _oEventCallBack = new PMT01500EventCallBackDTO();
        [Inject] private R_ILocalizer<PMT01100FrontResources.Resources_PMT01100_Class>? _localizer { get; set; }
        [Inject] private IClientHelper? _clientHelper { get; set; }

        PMT01100EventCallBackDTO _oEventCallBack = new PMT01100EventCallBackDTO();
        private bool _isCheckerDataFound = false;
        private bool _lDataCREF_NO = false;


        //Tambahan
        bool _isAllDataReady = false;


        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                lControlChoosenData = true;
                _viewModel.oParameter = R_FrontUtility.ConvertObjectToObject<PMT01100ParameterFrontChangePageDTO>(poParameter);
                if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
                {
                    await _viewModel.GetVAR_GSM_TRANSACTION_CODE();
                    await _viewModel.GetComboBoxDataTenantCategory();
                    await _viewModel.GetComboBoxDataTaxType();
                    await _viewModel.GetComboBoxDataIDTypeAsync();
                    _isAllDataReady = true;
                    if (!string.IsNullOrEmpty(_viewModel.oParameter.ODataUnitList))
                    {
                        var loTempDataUnitList = JsonSerializer.Deserialize<List<PMT01100UnitList_UnitListDTO>>(_viewModel.oParameter.ODataUnitList);
                        _viewModel.TempDataUnitList = loTempDataUnitList!.Select(unit => new PMT01100LOO_Offer_SelectedDataUnitListDTO()
                        {
                            CCOMPANY_ID = _clientHelper.CompanyId,
                            CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID,
                            CDEPT_CODE = _viewModel.oParameter.CDEPT_CODE,
                            CTRANS_CODE = _viewModel.oParameter.CTRANS_CODE,
                            CREF_NO = "",
                            CBUILDING_ID = "",
                            CUNIT_ID = unit.CUNIT_ID,
                            CFLOOR_ID = unit.CFLOOR_ID,
                            NACTUAL_AREA_SIZE = unit.NGROSS_AREA_SIZE,
                            NCOMMON_AREA_SIZE = unit.NCOMMON_AREA_SIZE,
                            CUSER_ID = _clientHelper.UserId
                        }).ToList();

                        await _conductor.Add();
                        goto EndBlock;
                    }

                    if (!string.IsNullOrEmpty(_viewModel.oParameter.CREF_NO))
                    {
                        _isCheckerDataFound = true;
                        await _conductor.R_GetEntity(null);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        #region Helper

        private void BlankFunctionButton() { }

        public bool lControlChoosenData = true;

        public void FuncRadioGroupChoosenData()
        {
            R_Exception loException = new R_Exception();

            try
            {
                switch (_viewModel.cDataChoosen)
                {
                    case "1":
                        lControlChoosenData = _viewModel.lControlExistingTenant = true;
                        break;
                    case "2":
                        _viewModel.oTempExistingEntity.CTENANT_ID = _viewModel.oTempExistingEntity.CTENANT_NAME = "";
                        lControlChoosenData = _viewModel.lControlExistingTenant = false;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }


        #endregion

        #region Function Component

        #region Master HandleFunction

        private void OnChangedDEXPIRED_DATE(DateTime? poParameter)
        {
            PMT01100LOO_Offer_SelectedOfferDTO loData = _viewModel.Data;
            loData.DEXPIRED_DATE = poParameter;
        }

        private void OnChangedDFOLLOW_UP_DATE(DateTime? poParameter)
        {
            PMT01100LOO_Offer_SelectedOfferDTO loData = _viewModel.Data;
            loData.DFOLLOW_UP_DATE = poParameter;
        }

        private void OnChangedDHAND_OVER_DATE(DateTime? poParameter)
        {
            PMT01100LOO_Offer_SelectedOfferDTO loData = _viewModel.Data;
            loData.DHAND_OVER_DATE = poParameter;
        }

        private void OnChangedDID_EXPIRED_DATE(DateTime? poParameter)
        {
            PMT01100LOO_Offer_SelectedOfferDTO loData = _viewModel.Data;
            loData.DID_EXPIRED_DATE = poParameter;
        }

        private void OnChangedDREF_DATE(DateTime? poParameter)
        {
            PMT01100LOO_Offer_SelectedOfferDTO loData = _viewModel.Data;
            loData.DREF_DATE = poParameter;
        }

        #region DateTime Function

        #endregion

        private void OnChangedCYEAR(Int32 poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMT01100LOO_Offer_SelectedOfferDTO)_viewModel.Data;
                var llControl = _viewModel.oControlYMD;
                loData.IYEAR = poParam;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAYS == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAYS).AddDays(-1);
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
                var loData = (PMT01100LOO_Offer_SelectedOfferDTO)_viewModel.Data;
                var llControl = _viewModel.oControlYMD;
                loData.IMONTH = poParam;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAYS == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAYS).AddDays(-1);
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
                var loData = (PMT01100LOO_Offer_SelectedOfferDTO)_viewModel.Data;
                var llControl = _viewModel.oControlYMD;
                loData.IDAYS = poParam;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAYS == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAYS).AddDays(-1);
                    }
                }
                else
                {
                    llControl.LYEAR = true;
                    loData.DEND_DATE = loData.DSTART_DATE!.Value.AddDays(loData.IDAYS).AddDays(-1);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        DateTime tStartDate = DateTime.Now.AddDays(-1);

        private void OnChangedDSTART_DATE(DateTime? poValue)
        {
            R_Exception loException = new R_Exception();

            try
            {
                PMT01100LOO_Offer_SelectedOfferDTO loData = _viewModel.Data;
                loData.DSTART_DATE = poValue;
                tStartDate = poValue ?? DateTime.Now;

                if (loData.DEND_DATE == null)
                {
                    loData.DEND_DATE = loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAYS == 0
                        ? loData.DSTART_DATE
                        : loData.DSTART_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAYS).AddDays(-1);
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
                PMT01100LOO_Offer_SelectedOfferDTO loData = _viewModel.Data;
                loData.DEND_DATE = poValue;

                if (loData.DSTART_DATE == null)
                {
                    loData.DSTART_DATE = loData.IYEAR == 0 && loData.IMONTH == 0 && loData.IDAYS == 0
                        ? loData.DEND_DATE
                        : loData.DEND_DATE!.Value.AddYears(loData.IYEAR).AddMonths(loData.IMONTH).AddDays(loData.IDAYS).AddDays(-1);
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
            PMT01100LOO_Offer_SelectedOfferDTO loData = _viewModel.Data;

            try
            {
                if (poEndDate != null)
                {

                    DateTime dValueEndDate = poEndDate!.Value.AddDays(1);

                    int liChecker = poEndDate!.Value.Day - poStartDate!.Value.Day;
                    if (liChecker < 0)
                    {
                        loData.IDAYS = 1;
                        loData.IMONTH = loData.IYEAR = 0;
                        if (string.IsNullOrEmpty(pcStart))
                            loData.DSTART_DATE = loData.DEND_DATE;
                        else
                            loData.DEND_DATE = loData.DSTART_DATE;

                    }
                    else
                    {

                        loData.IDAYS = dValueEndDate.Day - poStartDate!.Value.Day;
                        if (loData.IDAYS < 0)
                        {
                            DateTime dValueEndDateForHandleDay = dValueEndDate.AddMonths(-1);
                            int liTempDayinMonth = DateTime.DaysInMonth(dValueEndDateForHandleDay.Year, dValueEndDateForHandleDay.Month);
                            loData.IDAYS = liTempDayinMonth + loData.IDAYS;
                            if (loData.IDAYS < 0) { throw new Exception("ERROR HARINYA MINES"); }
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
                else
                {
                    if (poStartDate != null)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddDays(1);
                        loData.IYEAR = loData.IMONTH = 0;
                        loData.IDAYS = 2;
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
                var loData = (PMT01100LOO_Offer_SelectedOfferDTO)eventArgs.Data;

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
                        Program_Id = "PMT01100",
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
                        Program_Id = "PMT01100",
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

        #region Conductor Event

        private async Task AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            var loData = (PMT01100LOO_Offer_SelectedOfferDTO)eventArgs.Data;

            try
            {
                loData.CTRANS_CODE = _viewModel.oParameter.CTRANS_CODE;
                //loData.CTRANS_NAME = "Lease Agreement";
                _lDataCREF_NO = !_viewModel.oVarGSMTransactionCode.LINCREMENT_FLAG;
                /*
                loData.CTENANT_CATEGORY_ID = _viewModel.oTempFirstDataDefault.CTENANT_CATEGORY_ID;
                loData.CTAX_TYPE = _viewModel.oTempFirstDataDefault.CTAX_TYPE;
                loData.CID_TYPE = _viewModel.oTempFirstDataDefault.CID_TYPE;
                */
                if (_isAllDataReady == false)
                {
                    await _viewModel.GetComboBoxDataTenantCategory();
                    await _viewModel.GetComboBoxDataTaxType();
                    await _viewModel.GetComboBoxDataIDTypeAsync();
                }

                loData.CTENANT_CATEGORY_ID = _viewModel.oComboBoxTenantCategory.FirstOrDefault().CCATEGORY_ID;

                loData.CTAX_TYPE = _viewModel.oComboBoxTaxType.FirstOrDefault().CCODE;
                loData.CID_TYPE = _viewModel.oComboBoxIdType.FirstOrDefault().CCODE;

                loData.DSTART_DATE = loData.DEXPIRED_DATE
                    = loData.DFOLLOW_UP_DATE = loData.DHAND_OVER_DATE
                    = loData.DID_EXPIRED_DATE = DateTime.Now;
                tStartDate = DateTime.Now.AddDays(-1);
                loData.DEND_DATE = DateTime.Now.AddYears(1).AddDays(-1);
                loData.IYEAR = 1;
                //_viewModel.oControlYMD.LYEAR = true;
                /*
                loData.IDAYS = loData.DEND_DATE.Day.ToString("00");
                loData.IMONTH = loData.DEND_DATE.Month.ToString("00");
                loData.IYEAR = loData.DEND_DATE.Year.ToString();
                */
                //loData.CLEASE_MODE = _viewModel.loComboBoxDataCLeaseMode.First().CCODE;
                //loData.CCHARGE_MODE = _viewModel.loComboBoxDataCChargesMode.First().CCODE;
                loData.DREF_DATE = DateTime.Now;
                //loData.DDOC_DATE = DateTime.Now;

                loData.CBUILDING_ID = _viewModel.oParameter.CBUILDING_ID;
                loData.CBUILDING_NAME = _viewModel.oParameter.CBUILDING_NAME;

                await _componentCTENANT_IDTextBox.FocusAsync();

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
                _oEventCallBack.LCRUD_MODE = true;
                //_oEventCallBack.LACTIVEUnitInfoHasData = true;
                _oEventCallBack.CCRUD_MODE = "A_ADD";//Meaning of Agreement Add
                _oEventCallBack.ODATA_PARAMETER = new PMT01100ParameterFrontChangePageDTO()
                {
                    CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                    CDEPT_CODE = _viewModel.Data.CDEPT_CODE,
                    CTRANS_CODE = _viewModel.Data.CTRANS_CODE,
                    CREF_NO = _viewModel.Data.CREF_NO,
                    CBUILDING_ID = _viewModel.Data.CBUILDING_ID,
                };
                /*
                _oEventCallBack.CDEPT_CODE = _viewModel.Data.CDEPT_CODE!;
                _oEventCallBack.CTRANS_CODE = _viewModel.Data.CTRANS_CODE!;
                _oEventCallBack.CBUILDING_ID = _viewModel.Data.CBUILDING_ID!;
                _oEventCallBack.CCHARGE_MODE = _viewModel.Data.CCHARGE_MODE!;
                _oEventCallBack.CCURRENCY_CODE = _viewModel.Data.CCURRENCY_CODE!;
                */

                // Lakukan pemanggilan async
                await InvokeTabEventCallbackAsync(_oEventCallBack);

                // Setelah pemanggilan selesai, lanjutkan dengan kode selanjutnya
                _viewModel.TempDataUnitList.Clear();
                _oEventCallBack.CCRUD_MODE = "";
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
                /*
                if (eventArgs.Enable)
                    _lDataCREF_NO = false;
                _lDataCREF_NO = false;
                */
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        public async Task R_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                _oEventCallBack.LCRUD_MODE = true;
                _oEventCallBack.CCRUD_MODE = "A_CANCEL";//Meaning of Agreement Add
                await InvokeTabEventCallbackAsync(_oEventCallBack);
                // Setelah pemanggilan selesai, lanjutkan dengan kode selanjutnya
                _oEventCallBack.CCRUD_MODE = "";

                if (!string.IsNullOrEmpty(_viewModel.oParameter.CREF_NO))
                {
                    _isCheckerDataFound = true;
                    await _conductor.R_GetEntity(null);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        public async Task AfterDelete()
        {


            if (!_viewModel.HasData)
            {
                _oEventCallBack.CCRUD_MODE = "A_DELETE";//Meaning of Agreement Add
                await InvokeTabEventCallbackAsync(_oEventCallBack);

                // Setelah pemanggilan selesai, lanjutkan dengan kode selanjutnya
                _oEventCallBack.CCRUD_MODE = "";
            }
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }

        private async Task R_SetOther(R_SetEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                _viewModel.lControlCRUDMode = eventArgs.Enable;
                _oEventCallBack.LCRUD_MODE = _viewModel.lControlCRUDMode = eventArgs.Enable;
                await InvokeTabEventCallbackAsync(_oEventCallBack);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private void R_Validation(R_ValidationEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                var loData = (PMT01100LOO_Offer_SelectedOfferDTO)eventArgs.Data;
                //await LostFocusCUNIT_ID();

                if (_viewModel.cDataChoosen == "1")
                {
                    if (string.IsNullOrWhiteSpace(_viewModel.oTempExistingEntity.CTENANT_ID))
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01100_Class), "ValidationTenant");
                        loException.Add(loErr);
                    }
                }
                else if (_viewModel.cDataChoosen == "2")
                {
                    if (_lDataCREF_NO)
                    {
                        if (string.IsNullOrWhiteSpace(loData.CREF_NO))
                        {
                            var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01100_Class), "ValidationAgreementNo");
                            loException.Add(loErr);
                        }
                    }


                    if (string.IsNullOrWhiteSpace(loData.CTENANT_ID))
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01100_Class), "ValidationTenant");
                        loException.Add(loErr);
                    }

                    if (string.IsNullOrWhiteSpace(loData.CADDRESS))
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01100_Class), "ValidationAddress");
                        loException.Add(loErr);
                    }


                    if (string.IsNullOrWhiteSpace(loData.CBUILDING_ID))
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01100_Class), "ValidationBuilding");
                        loException.Add(loErr);
                    }

                    if (string.IsNullOrWhiteSpace(loData.CEMAIL))
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01100_Class), "ValidationEmail");
                        loException.Add(loErr);
                    }

                    if (string.IsNullOrWhiteSpace(loData.CSALESMAN_ID))
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01100_Class), "ValidationSalesman");
                        loException.Add(loErr);
                    }

                    if (loData.DREF_DATE == null)
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01100_Class), "ValidationOfferDate");
                        loException.Add(loErr);
                    }

                    if (loData.DSTART_DATE > loData.DEND_DATE)
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01100_Class), "ValidationDate");
                        loException.Add(loErr);
                    }

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



        #region Master CRUD

        private void ServiceR_Display(R_DisplayEventArgs eventArgs)
        {
            var loException = new R_Exception();

            try
            {
                switch (eventArgs.ConductorMode)
                {
                    case R_eConductorMode.Edit:
                        //await _componentCDOC_NOTextBox.FocusAsync();
                        //OnChangedDEND_DATE(_viewModel.Data.DEND_DATE);
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
            PMT01100LOO_Offer_SelectedOfferDTO loParam;

            try
            {
                loParam = new PMT01100LOO_Offer_SelectedOfferDTO();
                if (eventArgs.Data != null)
                {
                    loParam = R_FrontUtility.ConvertObjectToObject<PMT01100LOO_Offer_SelectedOfferDTO>(eventArgs.Data);
                }
                else
                {
                    loParam.CREF_NO = _viewModel.oParameter.CREF_NO;
                    loParam.CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID;
                    loParam.CDEPT_CODE = _viewModel.oParameter.CDEPT_CODE;
                    //loParam.CTRANS_CODE = "802041";
                    loParam.CCOMPANY_ID = _clientHelper.CompanyId;
                    loParam.CUSER_ID = _clientHelper.UserId;
                };
                await _viewModel.GetEntity(loParam);

                _viewModel.oTempExistingEntity.CTENANT_ID = _viewModel.oEntity.CTENANT_ID;
                _viewModel.oTempExistingEntity.CTENANT_NAME = _viewModel.oEntity.CTENANT_NAME;

                eventArgs.Result = _viewModel.oEntity;
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
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01100LOO_Offer_SelectedOfferDTO>(eventArgs.Data);

                if ((R_eConductorMode)eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    loParam.CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID;
                }

                await _viewModel.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.oEntity;
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
                var loData = (PMT01100LOO_Offer_SelectedOfferDTO)eventArgs.Data;

                await _viewModel.GetEntity(loData);

                if (_viewModel.oEntity != null)
                    await _viewModel.ServiceDelete(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Master LookUp


        #region Lookup Button Tenant Lookup

        private R_Lookup? R_LookupTenantLookup;

        private void BeforeOpenLookUpTenantLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            LML00600ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
            {
                param = new LML00600ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID,
                    CCUSTOMER_TYPE = "01",
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00600);
        }

        private void AfterOpenLookUpTenantLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML00600DTO? loTempResult = null;
            //PMT01100LOO_Offer_SelectedOfferDTO? loGetData = null;

            try
            {
                loTempResult = (LML00600DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;
                //loGetData = (PMT01100LOO_Offer_SelectedOfferDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModel.oTempExistingEntity.CTENANT_ID = loTempResult.CTENANT_ID;
                _viewModel.oTempExistingEntity.CTENANT_NAME = loTempResult.CTENANT_NAME;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private R_TextBox? _componentCTENANT_IDTextBox;

        private async Task OnLostFocusTenant()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT01100TempExistingDataOfferDTO loGetData = _viewModel.oTempExistingEntity;

                if (string.IsNullOrWhiteSpace(loGetData.CTENANT_ID))
                {
                    loGetData.CTENANT_ID = "";
                    loGetData.CTENANT_NAME = "";
                    return;
                }

                LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel();
                LML00600ParameterDTO loParam = new LML00600ParameterDTO()
                {

                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID,
                    CCUSTOMER_TYPE = "01",
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

        #region Lookup Button Building Lookup

        private R_Lookup? R_LookupBuildingLookup;

        private void BeforeOpenLookUpBuildingLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL02200ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
            {
                param = new GSL02200ParameterDTO
                {
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL02200);
        }

        private void AfterOpenLookUpBuildingLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL02200DTO? loTempResult = null;
            //PMT01100LOO_Offer_SelectedOfferDTO? loGetData = null;

            try
            {
                loTempResult = (GSL02200DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;
                //loGetData = (PMT01100LOO_Offer_SelectedOfferDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModel.Data.CBUILDING_ID = loTempResult.CBUILDING_ID;
                _viewModel.Data.CBUILDING_NAME = loTempResult.CBUILDING_NAME;
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
                PMT01100LOO_Offer_SelectedOfferDTO loGetData = (PMT01100LOO_Offer_SelectedOfferDTO)_viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CBUILDING_ID))
                {
                    loGetData.CBUILDING_ID = "";
                    loGetData.CBUILDING_NAME = "";
                    return;
                }

                LookupGSL02200ViewModel loLookupViewModel = new LookupGSL02200ViewModel();
                GSL02200ParameterDTO loParam = new GSL02200ParameterDTO()
                {
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID!,
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
            GSL00710ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
            {
                param = new GSL00710ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_LOGIN_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID,
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00710);
        }

        private void AfterOpenLookUpDepartmentLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL00710DTO? loTempResult = null;
            //PMT01100LOO_Offer_SelectedOfferDTO? loGetData = null;


            try
            {
                loTempResult = (GSL00710DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (PMT01100LOO_Offer_SelectedOfferDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModel.Data.CDEPT_CODE = loTempResult.CDEPT_CODE;
                _viewModel.Data.CDEPT_NAME = loTempResult.CDEPT_NAME;
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
                PMT01100LOO_Offer_SelectedOfferDTO loGetData = (PMT01100LOO_Offer_SelectedOfferDTO)_viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CDEPT_CODE))
                {
                    loGetData.CDEPT_CODE = "";
                    loGetData.CDEPT_NAME = "";
                    return;
                }

                LookupGSL00710ViewModel loLookupViewModel = new LookupGSL00710ViewModel();
                GSL00710ParameterDTO loParam = new GSL00710ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_LOGIN_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID!,
                    CSEARCH_TEXT = loGetData.CDEPT_CODE ?? "",
                };

                var loResult = await loLookupViewModel.GetDepartmentProperty(loParam);

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
            if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
            {
                param = new LML00500ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID,
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
            //PMT01100LOO_Offer_SelectedOfferDTO? loGetData = null;


            try
            {
                loTempResult = (LML00500DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (PMT01100LOO_Offer_SelectedOfferDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModel.Data.CSALESMAN_ID = loTempResult.CSALESMAN_ID;
                _viewModel.Data.CSALESMAN_NAME = loTempResult.CSALESMAN_NAME;
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
                PMT01100LOO_Offer_SelectedOfferDTO loGetData = (PMT01100LOO_Offer_SelectedOfferDTO)_viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CSALESMAN_ID))
                {
                    loGetData.CSALESMAN_ID = "";
                    loGetData.CSALESMAN_NAME = "";
                    return;
                }

                LookupLML00500ViewModel loLookupViewModel = new LookupLML00500ViewModel();
                LML00500ParameterDTO loParam = new LML00500ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID!,
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

        #region Lookup Button BillingRule Lookup

        private R_Lookup? R_LookupBillingRuleLookup;

        private void BeforeOpenLookUpBillingRuleLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00300ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
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

        private void AfterOpenLookUpBillingRuleLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL00300DTO? loTempResult = null;
            //PMT01100LOO_Offer_SelectedOfferDTO? loGetData = null;

            try
            {
                loTempResult = (GSL00300DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (PMT01100LOO_Offer_SelectedOfferDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModel.Data.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusBillingRule()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT01100LOO_Offer_SelectedOfferDTO loGetData = (PMT01100LOO_Offer_SelectedOfferDTO)_viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CCURRENCY_CODE))
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

        #region Lookup Button TandC Lookup

        private R_Lookup? R_LookupTandCLookup;

        private void BeforeOpenLookUpTandCLookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            GSL00300ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
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

        private void AfterOpenLookUpTandCLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL00300DTO? loTempResult = null;
            //PMT01100LOO_Offer_SelectedOfferDTO? loGetData = null;

            try
            {
                loTempResult = (GSL00300DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                //loGetData = (PMT01100LOO_Offer_SelectedOfferDTO)_conductorFullPMT01500Agreement.R_GetCurrentData();

                _viewModel.Data.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);

        }

        private async Task OnLostFocusTandC()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT01100LOO_Offer_SelectedOfferDTO loGetData = (PMT01100LOO_Offer_SelectedOfferDTO)_viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CCURRENCY_CODE))
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
