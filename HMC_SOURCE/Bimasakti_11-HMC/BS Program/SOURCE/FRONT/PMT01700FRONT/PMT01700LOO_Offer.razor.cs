using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using PMT01700COMMON.DTO._2._LOO._2._LOO___Offer;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Interfaces;
using PMT01700MODEL.ViewModel;
using PMT01700COMMON.DTO.Utilities;
using PMT01700COMMON.DTO.Utilities.Front;
using System.Text.Json;
using PMT01700COMMON.DTO._1._Other_Unit_List;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_CommonFrontBackAPI;
using R_LockingFront;
using R_BlazorFrontEnd.Controls.MessageBox;
using PMT01700FrontResources;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00600;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMModel.ViewModel.LML00500;
using R_BlazorFrontEnd.Enums;
using Lookup_PMCOMMON.DTOs.LML01000;
using Lookup_PMModel.ViewModel.LML01000;
using Lookup_PMCOMMON.DTOs.LML01100;
using Lookup_PMModel.ViewModel.LML01100;
using PMT01700COMMON.DTO.Utilities.ParamDb.LOO;
using PMT01700COMMON.DTO._3._LOC._2._LOC;
using R_BlazorFrontEnd.Extensions;
using System.Diagnostics.Tracing;
using PMT01700COMMON.DTO._2._LOO._1._LOO___Offer_List;
using R_BlazorFrontEnd.Controls.Interfaces;
using PMT01700COMMON.DTO._2._LOO._3._LOO___Unit___Charges.LOO___Unit___Charges___Charges;
using PMT01700COMMON.DTO.Utilities.Print;

namespace PMT01700FRONT
{
    public partial class PMT01700LOO_Offer : R_Page
    {
        private readonly PMT01700LOO_OfferViewModel _viewModel = new();
        readonly PMT01700LOO_OfferListViewModel _viewModelOfferList = new();
        private R_Conductor? _conductor;
        private R_Grid<PMT01700LOO_Offer_SelectedOfferDTO>? _gridRef;

        //PMT01500EventCallBackDTO _oEventCallBack = new PMT01500EventCallBackDTO();

        [Inject] private IClientHelper? _clientHelper { get; set; }

        PMT01700EventCallBackDTO _oEventCallBack = new PMT01700EventCallBackDTO();
        private bool _isCheckerDataFound = false;
        private bool _lDataCREF_NO = false;
        private bool _lViewMode = false;
        //Tambahan
        bool _isAllDataReady = false;
        public bool lControlChoosenData = true;
        private R_TextBox? _componentCTENANT_IDTextBox;

        //For New Open Page
        bool _LOpenCustomPage = true;
        private string PageWidth = "width: auto;";
        private bool lEnabledFieldBuilding = false;

        #region Handle Called Program

        private void BeforeOpenPMT01700(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMT01700ParameterLOO_Offer_SelectedOfferDTO
                {
                    Data = _viewModel.Data,
                    CALLER_ACTION = _viewModel.oParameter.CALLER_ACTION
                };

                //PMT01700ParameterLOO_Offer_SelectedOfferDTO loParam.Data = R_FrontUtility.ConvertObjectToObject<PMT01700ParameterLOO_Offer_SelectedOfferDTO>(_viewModel.Data);
                //loParam.CALLER_ACTION = _viewModel.oParameter.CALLER_ACTION;

                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(PMT01700LOC_LOC);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_After_ServiceOpenOthersProgram(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await Close(true, null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                //if (_lViewMode)
                //{
                //    true
                //}
                //else
                //{
                //    false
                //}
                //  poParameter.
                lControlChoosenData = _LOpenCustomPage = true;

                _viewModel.oParameter = R_FrontUtility.ConvertObjectToObject<PMT01700ParameterFrontChangePageDTO>(poParameter);
                lEnabledFieldBuilding = false;

                _LOpenCustomPage = _viewModel.oParameter.CALLER_ACTION == "ADD";
                _lViewMode = _viewModel.oParameter.CALLER_ACTION == "VIEW";

                if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
                {
                    await _viewModel.GetVAR_GSM_TRANSACTION_CODE();
                    await _viewModel.GetComboBoxDataTenantCategory();
                    await _viewModel.GetComboBoxDataTaxType();
                    await _viewModel.GetComboBoxDataIDTypeAsync();
                    _isAllDataReady = true;

                    //style tab to popup
                    if (!string.IsNullOrEmpty(_viewModel.oParameter.ODataUnitList) || _LOpenCustomPage)
                    {
                        PageWidth = "width: 1100px;";
                    }
                    //Btn New Offer
                    if (!string.IsNullOrEmpty(_viewModel.oParameter.ODataUnitList))
                    {
                        var loTempDataUnitList = JsonSerializer.Deserialize<List<PMT01700OtherUnitList_OtherUnitListDTO>>(_viewModel.oParameter.ODataUnitList);

                        _viewModel.TempDataUnitList = loTempDataUnitList!.Select(unit => new PMT01700LOO_Offer_SelectedOtherDataUnitListDTO
                        {
                            CCOMPANY_ID = _clientHelper.CompanyId,
                            CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID,
                            CDEPT_CODE = _viewModel.oParameter.CDEPT_CODE,
                            CTRANS_CODE = _viewModel.oParameter.CTRANS_CODE,
                            //CREF_NO = "",
                            //CBUILDING_ID = _viewModel.oParameter.CBUILDING_ID,
                            CBUILDING_NAME = _viewModel.oParameter.CBUILDING_NAME,
                            COTHER_UNIT_ID = unit.COTHER_UNIT_ID,
                            CFLOOR_ID = unit.CFLOOR_ID,
                            //NACTUAL_AREA_SIZE = unit.NGROSS_AREA_SIZE,
                            //NCOMMON_AREA_SIZE = unit.NCOMMON_AREA_SIZE,
                            CUSER_ID = _clientHelper.UserId
                        }).ToList();

                        await _conductor.Add();

                        goto EndBlock;
                    }
                    //Btn revise
                    if (_viewModel.oParameter.LREVISE_BUTTON)
                    {
                        await _conductor!.Add();
                    }
                    //normal tab
                    else if (!string.IsNullOrEmpty(_viewModel.oParameter.CREF_NO))
                    {
                        lEnabledFieldBuilding = true;
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
         
        public void FuncRadioGroupChoosenData()
        {
            R_Exception loException = new R_Exception();

            try
            {
                switch (_viewModel.cDataChoosen)
                {
                    case "1":
                        lControlChoosenData = _viewModel.lControlExistingTenant = true;
                        _viewModel.lControlExistingTenantOriginal = false;
                        break;
                    case "2":
                        _viewModel.oTempExistingEntity.CTENANT_ID = _viewModel.oTempExistingEntity.CTENANT_NAME = "";
                        lControlChoosenData = _viewModel.lControlExistingTenant = false;
                        _viewModel.lControlExistingTenantOriginal = true;

                        //update to empty data on tenant
                        var loData = (PMT01700LOO_Offer_SelectedOfferDTO)_conductor!.R_GetCurrentData();
                        loData.CTENANT_ID = "";
                        loData.CTENANT_NAME = "";
                        loData.CADDRESS = "";
                        loData.CEMAIL = "";
                        loData.CPHONE1 = "";
                        loData.CPHONE2 = "";
                        //      loData.CTENANT_CATEGORY_ID = "";
                        loData.CATTENTION1_NAME = "";
                        loData.CATTENTION1_EMAIL = "";
                        loData.CATTENTION1_MOBILE_PHONE1 = "";
                        loData.CSALESMAN_ID = "";
                        loData.CSALESMAN_NAME = "";
                        //    loData.CTAX_TYPE = "";
                        loData.CTAX_ID = "";
                        loData.CTAX_NAME = "";
                        //    loData.CID_TYPE = "";
                        loData.CID_NO = "";
                        loData.CID_EXPIRED_DATE = "";
                        loData.CTAX_ADDRESS = "";

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
        #region PopUp Print
        private async Task BeforeOpen_PopupPrint(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = (PMT01700LOO_Offer_SelectedOfferDTO)_conductor.R_GetCurrentData();

                ParameterPrintDTO loParam = new ParameterPrintDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID,
                    CDEPT_CODE = loData.CDEPT_CODE ?? _viewModel.oParameter.CDEPT_CODE,
                    CTRANS_CODE = _viewModel.oParameter.CTRANS_CODE,
                    CREF_NO = loData.CREF_NO ?? _viewModel.oParameter.CREF_NO,
                    CUSER_ID = _clientHelper.UserId,

                };

                if (loData == null)
                {
                    var loValidate = await R_MessageBox.Show("", _localizer["ValidationNoRecord"], R_eMessageBoxButtonType.OK);
                    goto EndBlock;
                }

                eventArgs.PageTitle = _localizer["_Print"];
                eventArgs.Parameter = loParam;
                eventArgs.TargetPageType = typeof(PMT01700Print);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:

            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }
        #endregion
        #region Button
        private async Task SubmitBtn()
        {
            var loEx = new R_Exception();
            await lockingButton(true);
            try
            {
                //SUBMIT CODE == "10"
                bool llConfirmation = await R_MessageBox.Show("Confirmation",
                    R_FrontUtility.R_GetMessage(typeof(Resources_PMT01700_Class), "_ConfirmationSubmit"),
                    R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.Yes;

                if (llConfirmation)
                {
                    _viewModelOfferList.loEntityOffer = R_FrontUtility.ConvertObjectToObject<PMT01700LOO_OfferList_OfferListDTO>(_conductor.R_GetCurrentData());

                    var loReturn = await _viewModelOfferList.ProcessUpdateAgreement(lcNewStatus: "10");
                    if (loReturn.IS_PROCESS_SUCCESS)
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT01700_Class), "_SuccessMessageOfferSubmit"));
                        await _conductor.R_GetEntity(null);
                    }
                    else
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT01700_Class), "_FailedUpdate"));
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
                  R_FrontUtility.R_GetMessage(typeof(Resources_PMT01700_Class), "_ConfirmationRedraft"),
                  R_eMessageBoxButtonType.YesNo) == R_eMessageBoxResult.Yes;

                if (llConfirmation)
                {
                    _viewModelOfferList.loEntityOffer = R_FrontUtility.ConvertObjectToObject<PMT01700LOO_OfferList_OfferListDTO>(_conductor.R_GetCurrentData());
                    var loReturn = await _viewModelOfferList.ProcessUpdateAgreement(lcNewStatus: "00");
                    if (loReturn.IS_PROCESS_SUCCESS)
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT01700_Class), "_SuccessMessageOfferRedraft"));
                        await _conductor.R_GetEntity(null);
                    }
                    else
                    {
                        await R_MessageBox.Show(R_FrontUtility.R_GetMessage(typeof(Resources_PMT01700_Class), "_FailedUpdate"));
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
        #region Master HandleFunction

        private void OnChangedDEXPIRED_DATE(DateTime? poParameter)
        {
            PMT01700LOO_Offer_SelectedOfferDTO loData = _viewModel.Data;
            loData.DEXPIRED_DATE = poParameter;
        }

        private void OnChangedDFOLLOW_UP_DATE(DateTime? poParameter)
        {
            PMT01700LOO_Offer_SelectedOfferDTO loData = _viewModel.Data;
            loData.DFOLLOW_UP_DATE = poParameter;
        }

        private void OnChangedDHAND_OVER_DATE(DateTime? poParameter)
        {
            PMT01700LOO_Offer_SelectedOfferDTO loData = _viewModel.Data;
            loData.DHAND_OVER_DATE = poParameter;
        }

        private void OnChangedDID_EXPIRED_DATE(DateTime? poParameter)
        {
            PMT01700LOO_Offer_SelectedOfferDTO loData = _viewModel.Data;
            loData.DID_EXPIRED_DATE = poParameter;
        }

        private void OnChangedDREF_DATE(DateTime? poParameter)
        {
            PMT01700LOO_Offer_SelectedOfferDTO loData = _viewModel.Data;
            loData.DREF_DATE = poParameter;
        }

        #region DateTime Function

        #endregion

        private void OnChangedCYEAR(Int32 poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = _viewModel.Data;
                PMT01700ControlYMD llControl = _viewModel.oControlYMD;
                loData.IYEARS = poParam;
                loData.IHOURS = 0;
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
                var loData = _viewModel.Data;
                var llControl = _viewModel.oControlYMD;
                loData.IMONTHS = poParam;
                loData.IHOURS = 0;
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
                var loData = _viewModel.Data;
                var llControl = _viewModel.oControlYMD;
                loData.IDAYS = poParam;
                loData.IHOURS = 0;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else if (poParam == 1)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value
                          .AddHours(23)
                          .AddMinutes(59);
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value
                            .AddYears(loData.IYEARS)
                            .AddMonths(loData.IMONTHS)
                            .AddDays(loData.IDAYS)
                            .AddDays(-1);
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
        private void OnChangedIHOUR(Int32 poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = _viewModel.Data;
                var llControl = _viewModel.oControlYMD;

                // Mengatur nilai IHOUR
                loData.IHOURS = poParam;

                loData.IYEARS = loData.IMONTHS = loData.IDAYS = 0;

                if (llControl.LYEAR || llControl.LMONTH || llControl.LDAY)
                {
                    if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0 && loData.IHOURS == 0)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE;
                    }
                    else
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value
                            .AddHours(loData.IHOURS);
                    }
                }
                else
                {
                    llControl.LYEAR = true;
                    llControl.LMONTH = true;
                    llControl.LDAY = true;

                    loData.DEND_DATE = loData.DSTART_DATE!.Value
                         .AddHours(loData.IHOURS);
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
                PMT01700LOO_Offer_SelectedOfferDTO loData = _viewModel.Data;
                loData.DSTART_DATE = poValue;
                tStartDate = poValue ?? DateTime.Now;

                if (loData.DEND_DATE == null)
                {
                    loData.DEND_DATE = loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0
                        ? loData.DSTART_DATE
                        : loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
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
                PMT01700LOO_Offer_SelectedOfferDTO loData = _viewModel.Data;
                loData.DEND_DATE = poValue;

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
            PMT01700LOO_Offer_SelectedOfferDTO loData = _viewModel.Data;

            try
            {
                if (poEndDate != null && poStartDate != null)
                {

                    if (loData.IYEARS == 0 && loData.IMONTHS == 0 && loData.IDAYS == 0)
                    {
                        loData.IDAYS = 1;
                        loData.IMONTHS = loData.IYEARS = 0;
                        //loData.IHOURS = 24; // Set to 24 hours for 1 day difference
                        if (string.IsNullOrEmpty(pcStart))
                            loData.DSTART_DATE = loData.DEND_DATE;
                        else
                        {
                            loData.IDAYS = 0;
                            loData.DEND_DATE = loData.DSTART_DATE;
                        }
                    }
                    else
                    {
                        if (pcStart == "S")
                        {
                            loData.DEND_DATE = loData.DSTART_DATE!.Value.AddYears(loData.IYEARS).AddMonths(loData.IMONTHS).AddDays(loData.IDAYS).AddDays(-1);
                        }
                        else
                        {
                            loData.DSTART_DATE = loData.DEND_DATE!.Value.AddYears(-loData.IYEARS).AddMonths(-loData.IMONTHS).AddDays(-loData.IDAYS).AddDays(1);
                        }
                    }
                    //    int liChecker = poEndDate!.Value.Day - poStartDate!.Value.Day;
                    //if (liChecker < 0)
                    //{
                    //    loData.IDAYS = 1;
                    //    loData.IMONTHS = loData.IYEARS = 0;
                    //    loData.IHOURS = 24; // Set to 24 hours for 1 day difference
                    //    if (string.IsNullOrEmpty(pcStart))
                    //        loData.DSTART_DATE = loData.DEND_DATE;
                    //    else
                    //        loData.DEND_DATE = loData.DSTART_DATE;

                    //}
                    //else
                    //{

                    //    loData.IDAYS = dValueEndDate.Day - poStartDate!.Value.Day;
                    //    if (loData.IDAYS < 0)
                    //    {
                    //        DateTime dValueEndDateForHandleDay = dValueEndDate.AddMonths(-1);
                    //        int liTempDayinMonth = DateTime.DaysInMonth(dValueEndDateForHandleDay.Year, dValueEndDateForHandleDay.Month);
                    //        loData.IDAYS = liTempDayinMonth + loData.IDAYS;
                    //        if (loData.IDAYS < 0)
                    //        {
                    //            loException.Add("DevError", "ERROR HARINYA MINES");
                    //        }

                    //        loData.IMONTHS = dValueEndDateForHandleDay.Month - poStartDate!.Value.Month;
                    //        if (loData.IMONTHS < 0)
                    //        {
                    //            loData.IMONTHS = 12 + loData.IMONTHS;
                    //            DateTime dValueEndDateForHandleMonth = dValueEndDate.AddYears(-1);
                    //            loData.IYEARS = dValueEndDateForHandleMonth.Year - poStartDate!.Value.Year;
                    //            if (loData.IYEARS < 0)
                    //            {
                    //                loData.IYEARS = 0;
                    //            }
                    //        }

                    //    }
                    //    else
                    //    {
                    //        loData.IMONTHS = dValueEndDate.Month - poStartDate!.Value.Month;
                    //        if (loData.IMONTHS < 0)
                    //        {
                    //            loData.IMONTHS = 12 + loData.IMONTHS;
                    //            DateTime dValueEndDateForHandleMonth = dValueEndDate.AddYears(-1);
                    //            loData.IYEARS = dValueEndDateForHandleMonth.Year - poStartDate!.Value.Year;
                    //            if (loData.IYEARS < 0)
                    //            {
                    //                loData.IYEARS = 0;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            loData.IYEARS = dValueEndDate.Year - poStartDate!.Value.Year;
                    //        }
                    //    }
                    //    // Calculate hours
                    //    loData.IHOURS = (int)(poEndDate.Value - poStartDate.Value).TotalHours % 24;
                    //}
                }
                else
                {
                    if (poStartDate != null)
                    {
                        loData.DEND_DATE = loData.DSTART_DATE!.Value.AddDays(1);
                        loData.IMONTHS = loData.IYEARS = 00;
                        loData.IDAYS = 2;
                        loData.IHOURS = 24; // Set to 24 hours for 1 day difference
                    }


                }
            }
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
                var loData = (PMT01700LOO_Offer_SelectedOfferDTO)eventArgs.Data;

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
                        Program_Id = "PMT01700",
                        Table_Name = "PMT_AGREEMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CREF_NO, loData.CTRANS_CODE)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT01700",
                        Table_Name = "PMT_AGREEMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CREF_NO, loData.CTRANS_CODE)

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
        private async Task lockingButton(bool param)
        {
            var loEx = new R_Exception();
            R_LockingFrontResult loLockResult = null;
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<PMT01700ParameterFrontChangePageDTO>(_conductor!.R_GetCurrentData());
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
                        Program_Id = "PMT01700",
                        Table_Name = "PMT_AGREEMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CREF_NO, loData.CTRANS_CODE)
                    };
                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else // Unlock
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT01700",
                        Table_Name = "PMT_AGREEMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CREF_NO, loData.CTRANS_CODE)
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
            PMT01700LOO_Offer_SelectedOfferDTO loParam;

            try
            {
                loParam = new PMT01700LOO_Offer_SelectedOfferDTO();
                if (eventArgs.Data != null)
                {
                    loParam = R_FrontUtility.ConvertObjectToObject<PMT01700LOO_Offer_SelectedOfferDTO>(eventArgs.Data);

                }
                else
                {
                    loParam = R_FrontUtility.ConvertObjectToObject<PMT01700LOO_Offer_SelectedOfferDTO>(_viewModel.oParameter);
                    //  loParam.CTRANS_CODE = "802043";
                };

                await _viewModel.GetEntity(loParam);

                _viewModel.oTempExistingEntity.CTENANT_ID = _viewModel.oEntity.CTENANT_ID;
                _viewModel.oTempExistingEntity.CTENANT_NAME = _viewModel.oEntity.CTENANT_NAME;

                eventArgs.Result = _viewModel.oEntity;

                switch (_viewModel.oEntity.CTRANS_STATUS)
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
                        _viewModel.lControlButtonRedraft = _viewModel.lControlButtonSubmit = false;
                        break;
                    case "80":
                    case "98":
                        _viewModel.lControlButtonRedraft =
                        _viewModel.lControlButtonSubmit = false;
                        break;
                }
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
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT01700LOO_Offer_SelectedOfferDTO>(eventArgs.Data);

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
                var loData = (PMT01700LOO_Offer_SelectedOfferDTO)eventArgs.Data;
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

        #region Conductor Event

        private async Task AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            var loData = (PMT01700LOO_Offer_SelectedOfferDTO)eventArgs.Data;

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
                _viewModel.oTempExistingEntityBackUp =
                  _viewModel.oTempExistingEntity ?? new PMT01700LOO_Offer_ExistingDataDTO();
                _viewModel.oTempExistingEntity = new PMT01700LOO_Offer_ExistingDataDTO();
                //FROM REVISE BUTTON
                if (_viewModel.oParameter.LREVISE_BUTTON)
                {
                    loData.CORIGINAL_REF_NO = _viewModel.oParameter.CREF_NO;

                    var loParamGetAgreementDetail = R_FrontUtility.ConvertObjectToObject<PMT01700LOO_Offer_SelectedOfferDTO>(_viewModel.oParameter);

                    var loAgreementDetail = await _viewModel.GetAgreementDetail(poParameter: loParamGetAgreementDetail);

                    PMT01700LOO_Offer_TenantParamDTO poParameter = new PMT01700LOO_Offer_TenantParamDTO
                    {
                        CPROPERTY_ID = loAgreementDetail.CPROPERTY_ID,
                        CTENANT_ID = loAgreementDetail.CTENANT_ID
                    };

                    if (!string.IsNullOrEmpty(loAgreementDetail.CTENANT_ID))
                    {
                        await _viewModel.GetTenantDetail(poParameter);


                        _viewModel.oTempExistingEntity.CTENANT_ID = _viewModel.TenantDetail.CTENANT_ID;
                        _viewModel.oTempExistingEntity.CTENANT_NAME = _viewModel.TenantDetail.CTENANT_NAME;
                        loData.CPROPERTY_ID = _viewModel.TenantDetail.CPROPERTY_ID;
                        loData.CTENANT_ID = _viewModel.TenantDetail.CTENANT_ID;
                        loData.CTENANT_NAME = _viewModel.TenantDetail.CTENANT_NAME;
                        loData.CADDRESS = _viewModel.TenantDetail.CADDRESS;
                        loData.CEMAIL = _viewModel.TenantDetail.CEMAIL;
                        loData.CPHONE1 = _viewModel.TenantDetail.CPHONE1;
                        loData.CPHONE2 = _viewModel.TenantDetail.CPHONE2;
                        loData.CTENANT_CATEGORY_ID = _viewModel.TenantDetail.CTENANT_CATEGORY_ID;
                        loData.CATTENTION1_NAME = _viewModel.TenantDetail.CATTENTION1_NAME;
                        loData.CATTENTION1_EMAIL = _viewModel.TenantDetail.CATTENTION1_EMAIL;
                        loData.CATTENTION1_MOBILE_PHONE1 = _viewModel.TenantDetail.CATTENTION1_MOBILE_PHONE1;

                        loData.CBUILDING_ID = _viewModel.TenantDetail.CBUILDING_ID;
                        loData.CBUILDING_NAME = _viewModel.TenantDetail.CBUILDING_NAME;
                        loData.CTAX_TYPE = _viewModel.TenantDetail.CTAX_TYPE;
                        loData.CTAX_ID = _viewModel.TenantDetail.CTAX_ID;
                        loData.CTAX_NAME = _viewModel.TenantDetail.CTAX_NAME;
                        loData.CID_TYPE = _viewModel.TenantDetail.CID_TYPE;
                        loData.CID_NO = _viewModel.TenantDetail.CID_NO;
                        loData.CID_EXPIRED_DATE = _viewModel.TenantDetail.CID_EXPIRED_DATE;
                        loData.CTAX_ADDRESS = _viewModel.TenantDetail.CTAX_ADDRESS;
                    }

                    loData.CBUILDING_ID = loAgreementDetail.CBUILDING_ID;
                    loData.CBUILDING_NAME = loAgreementDetail.CBUILDING_NAME;
                    loData.CDEPT_CODE = loAgreementDetail.CDEPT_CODE;
                    loData.CDEPT_NAME = loAgreementDetail.CDEPT_NAME;
                    loData.CSALESMAN_ID = loAgreementDetail.CSALESMAN_ID;
                    loData.CSALESMAN_NAME = loAgreementDetail.CSALESMAN_NAME;
                    loData.CORIGINAL_REF_NO = loAgreementDetail.CREF_NO;
                    loData.CTRANS_CODE = loAgreementDetail.CTRANS_CODE;
                    /* in the top is a mandatory field */
                    loData.CTRANS_CODE = _viewModel.oParameter.CTRANS_CODE;
                    loData.CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID;
                    /* in the top is a mandatory field */
                    loData.CLEASE_MODE = _viewModel.oComboBoxTaxType.First().CCODE;
                    loData.CCHARGE_MODE = _viewModel.oComboBoxIdType.First().CCODE;
                    loData.IYEARS = loAgreementDetail.IYEARS;
                    loData.IMONTHS = loAgreementDetail.IMONTHS;
                    loData.IDAYS = loAgreementDetail.IDAYS;
                    loData.LWITH_FO = loAgreementDetail.LWITH_FO;
                    loData.CUNIT_DESCRIPTION = loAgreementDetail.CUNIT_DESCRIPTION;
                    loData.CBILLING_RULE_CODE = loAgreementDetail.CBILLING_RULE_CODE;
                    loData.NBOOKING_FEE = loAgreementDetail.NBOOKING_FEE;
                    loData.CTC_CODE = loAgreementDetail.CTC_CODE;
                    _viewModel.cDataChoosen = "1";
                    _viewModel.lControlExistingTenant = true;
                    _viewModel.lControlExistingTenantOriginal = false;
                }
                else
                {
                    loData.CTENANT_CATEGORY_ID = _viewModel.oComboBoxTenantCategory.FirstOrDefault().CCATEGORY_ID;

                    loData.CTAX_TYPE = _viewModel.oComboBoxTaxType.FirstOrDefault(x => x.CCODE == "02")?.CCODE;
                    loData.CID_TYPE = _viewModel.oComboBoxIdType.FirstOrDefault().CCODE;
                    tStartDate = DateTime.Now.AddDays(-1);
                    loData.CLEASE_MODE = _viewModel.oComboBoxTaxType.First().CCODE;
                    loData.CCHARGE_MODE = _viewModel.oComboBoxIdType.First().CCODE;
                    _viewModel.lControlExistingTenant = true;
                    _viewModel.lControlExistingTenantOriginal = false;

                    loData.CBUILDING_ID = _viewModel.oParameter.CBUILDING_ID;
                    loData.CBUILDING_NAME = _viewModel.oParameter.CBUILDING_NAME;

                    loData.IDAYS = 1;
                    loData.DEND_DATE = DateTime.Now.AddHours(23).AddMinutes(50);
                }

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
                if (!_LOpenCustomPage)
                {
                    await Close(true, "SUCCESS");
                }

                _isCheckerDataFound = true;
                _oEventCallBack.LCRUD_MODE = true;
                //_oEventCallBack.LACTIVEUnitInfoHasData = true;
                _oEventCallBack.CCRUD_MODE = "A_ADD";//Meaning of Agreement Add
                _oEventCallBack.ODATA_PARAMETER = new PMT01700ParameterFrontChangePageDTO()
                {
                    CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                    CDEPT_CODE = _viewModel.Data.CDEPT_CODE,
                    CTRANS_CODE = _viewModel.Data.CTRANS_CODE,
                    CREF_NO = _viewModel.Data.CREF_NO,
                    CBUILDING_ID = _viewModel.Data.CBUILDING_ID,
                };

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

        private async Task R_SetEdit(R_SetEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                _lDataCREF_NO = false;
                _viewModel.lControlExistingTenant = _viewModel.lControlExistingTenantOriginal = false;

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

        public async Task R_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();

            try
            {
                var res = await R_MessageBox.Show("", @_localizer["ValidationBeforeCancel"],
                    R_eMessageBoxButtonType.YesNo);
                if (res == R_eMessageBoxResult.No)
                {
                    eventArgs.Cancel = true;
                }
                else
                {

                    _oEventCallBack.LCRUD_MODE = true;
                    _oEventCallBack.CCRUD_MODE = "A_CANCEL";//Meaning of Agreement Add
                    await InvokeTabEventCallbackAsync(_oEventCallBack);
                    // Setelah pemanggilan selesai, lanjutkan dengan kode selanjutnya
                    _oEventCallBack.CCRUD_MODE = "";
                    _viewModel.lControlExistingTenant = _viewModel.lControlExistingTenantOriginal = false;

                    _viewModel.cDataChoosen = "1";
                    lControlChoosenData = true;
                    _viewModel.oTempExistingEntity =
                        _viewModel.oTempExistingEntityBackUp ?? new PMT01700LOO_Offer_ExistingDataDTO();
                    _viewModel.oTempExistingEntityBackUp = new PMT01700LOO_Offer_ExistingDataDTO();

                    await Close(false, false);
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
        private void R_CheckEdit(R_CheckEditEventArgs eventArgs)
        {
            var loException = new R_Exception();
            //Task loTask;

            try
            {
                eventArgs.Allow = _viewModel.oEntity.CTRANS_STATUS == "00";
            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }

            R_DisplayException(loException);
        }
        private void R_CheckDelete(R_CheckDeleteEventArgs eventArgs)
        {
            var loException = new R_Exception();
            //Task loTask;

            try
            {
                eventArgs.Allow = _viewModel.oEntity.CTRANS_STATUS == "00";
            }
            catch (Exception Ex)
            {
                loException.Add(Ex);
            }

            R_DisplayException(loException);
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
                var loData = (PMT01700LOO_Offer_SelectedOfferDTO)eventArgs.Data;
                //await LostFocusCUNIT_ID();

                if (_viewModel.cDataChoosen == "1")
                {
                    if (string.IsNullOrWhiteSpace(_viewModel.oTempExistingEntity.CTENANT_ID))
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationTenant");
                        loException.Add(loErr);
                    }
                }
                else if (_viewModel.cDataChoosen == "2")
                {
                    if (_lDataCREF_NO)
                    {
                        if (string.IsNullOrWhiteSpace(loData.CREF_NO))
                        {
                            var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationAgreementNo");
                            loException.Add(loErr);
                        }
                    }
                    if (string.IsNullOrWhiteSpace(loData.CTENANT_ID))
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationTenant");
                        loException.Add(loErr);
                    }

                    if (string.IsNullOrWhiteSpace(loData.CADDRESS))
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationAddress");
                        loException.Add(loErr);
                    }
                    if (string.IsNullOrWhiteSpace(loData.CEMAIL))
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationEmail");
                        loException.Add(loErr);
                    }

                    if (string.IsNullOrWhiteSpace(loData.CSALESMAN_ID))
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationSalesman");
                        loException.Add(loErr);
                    }

                }
                if (loData.DSTART_DATE > loData.DEND_DATE)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationDate");
                    loException.Add(loErr);
                }
                if (loData.DREF_DATE == null)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationOfferDate");
                    loException.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(loData.CUNIT_DESCRIPTION))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationEventName");
                    loException.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(loData.CCURRENCY_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationCurrency");
                    loException.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(loData.CDEPT_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationDepartment");
                    loException.Add(loErr);
                }
                if (loData.IYEARS <= 0 && loData.IMONTHS <= 0 && loData.IDAYS <= 0 && loData.IHOURS <= 0)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationTenure");
                    loException.Add(loErr);
                }
                if (loData.NBOOKING_FEE < 0)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationBookingFee");
                    loException.Add(loErr);
                }
                if (loData.DREF_DATE > loData.DFOLLOW_UP_DATE)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationFollowUpDate");
                    loException.Add(loErr);
                }
                if (loData.DREF_DATE > loData.DEXPIRED_DATE)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01700_Class), "ValidationExpiredDate");
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

        #region Lookup Tenant

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

        private async Task AfterOpenLookUpTenantLookupAsync(R_AfterOpenLookupEventArgs eventArgs)
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
                var loData = (PMT01700LOO_Offer_SelectedOfferDTO)_conductor!.R_GetCurrentData();
                _viewModel.oTempExistingEntity.CTENANT_ID = loTempResult.CTENANT_ID;
                _viewModel.oTempExistingEntity.CTENANT_NAME = loTempResult.CTENANT_NAME;

                //UPDATED TO GET TENANT DETAIL
                PMT01700LOO_Offer_TenantParamDTO poParameter = new PMT01700LOO_Offer_TenantParamDTO
                {
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID,
                    CTENANT_ID = loTempResult.CTENANT_ID
                };

                await _viewModel.GetTenantDetail(poParameter: poParameter);

                if (!string.IsNullOrEmpty(_viewModel.TenantDetail.CTENANT_ID))
                {
                    loData.CTENANT_ID = _viewModel.TenantDetail.CTENANT_ID;
                    loData.CTENANT_NAME = _viewModel.TenantDetail.CTENANT_NAME;
                    loData.CADDRESS = _viewModel.TenantDetail.CADDRESS;
                    loData.CEMAIL = _viewModel.TenantDetail.CEMAIL;
                    loData.CPHONE1 = _viewModel.TenantDetail.CPHONE1;
                    loData.CPHONE2 = _viewModel.TenantDetail.CPHONE2;
                    loData.CTENANT_CATEGORY_ID = _viewModel.TenantDetail.CTENANT_CATEGORY_ID;
                    loData.CATTENTION1_NAME = _viewModel.TenantDetail.CATTENTION1_NAME;
                    loData.CATTENTION1_EMAIL = _viewModel.TenantDetail.CATTENTION1_EMAIL;
                    loData.CATTENTION1_MOBILE_PHONE1 = _viewModel.TenantDetail.CATTENTION1_MOBILE_PHONE1;
                    loData.CSALESMAN_ID = _viewModel.TenantDetail.CSALESMAN_ID;
                    loData.CSALESMAN_NAME = _viewModel.TenantDetail.CSALESMAN_NAME;
                    loData.CTAX_TYPE = _viewModel.TenantDetail.CTAX_TYPE;
                    loData.CTAX_ID = _viewModel.TenantDetail.CTAX_ID;
                    loData.CTAX_NAME = _viewModel.TenantDetail.CTAX_NAME;
                    loData.CID_TYPE = _viewModel.TenantDetail.CID_TYPE;
                    loData.CID_NO = _viewModel.TenantDetail.CID_NO;
                    loData.CID_EXPIRED_DATE = _viewModel.TenantDetail.CID_EXPIRED_DATE;
                    loData.CTAX_ADDRESS = _viewModel.TenantDetail.CTAX_ADDRESS;
                };

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
                PMT01700LOO_Offer_ExistingDataDTO loGetData = _viewModel.oTempExistingEntity;

                //  var loGetData = _viewModel.Data;

                if (string.IsNullOrWhiteSpace(loGetData.CTENANT_ID))
                {
                    loGetData.CTENANT_ID = "";
                    loGetData.CTENANT_NAME = "";
                    return;
                }

                LookupLML00600ViewModel loLookupViewModel = new LookupLML00600ViewModel();
                LML00600ParameterDTO loParam = new LML00600ParameterDTO()
                {

                    CCOMPANY_ID = _clientHelper!.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID,
                    CCUSTOMER_TYPE = "01",
                    CSEARCH_TEXT = loGetData.CTENANT_ID ?? "",
                };

                var loResult = await loLookupViewModel.GetTenant(loParam);
                var loData = (PMT01700LOO_Offer_SelectedOfferDTO)_conductor!.R_GetCurrentData();

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CTENANT_ID = "";
                    loGetData.CTENANT_NAME = "";
                    loData.CPROPERTY_ID = "";
                    loData.CTENANT_ID = "";
                    loData.CTENANT_NAME = "";
                    loData.CADDRESS = "";
                    loData.CEMAIL = "";
                    loData.CPHONE1 = "";
                    loData.CPHONE2 = "";
                    loData.CATTENTION1_NAME = "";
                    loData.CATTENTION1_EMAIL = "";
                    loData.CATTENTION1_MOBILE_PHONE1 = "";
                    loData.CSALESMAN_ID = "";
                    loData.CTAX_ID = "";
                    loData.CTAX_NAME = "";

                    loData.CTENANT_CATEGORY_ID = _viewModel.oComboBoxTenantCategory.FirstOrDefault().CCATEGORY_ID;
                    loData.CTAX_TYPE = _viewModel.oComboBoxTaxType.FirstOrDefault(x => x.CCODE == "02")?.CCODE;
                    loData.CID_TYPE = _viewModel.oComboBoxIdType.FirstOrDefault().CCODE;

                    loData.CID_NO = "";
                    loData.CID_EXPIRED_DATE = "";
                    loData.CTAX_ADDRESS = "";
                }
                else
                {
                    loGetData.CTENANT_ID = loResult.CTENANT_ID;
                    loGetData.CTENANT_NAME = loResult.CTENANT_NAME;

                    //UPDATED TO GET TENANT DETAIL
                    PMT01700LOO_Offer_TenantParamDTO poParameter = new PMT01700LOO_Offer_TenantParamDTO
                    {
                        CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID,
                        CTENANT_ID = loResult.CTENANT_ID
                    };

                    await _viewModel.GetTenantDetail(poParameter: poParameter);

                    if (!string.IsNullOrEmpty(_viewModel.TenantDetail.CTENANT_ID))
                    {
                        loData.CADDRESS = _viewModel.TenantDetail.CADDRESS;
                        loData.CEMAIL = _viewModel.TenantDetail.CEMAIL;
                        loData.CPHONE1 = _viewModel.TenantDetail.CPHONE1;
                        loData.CPHONE2 = _viewModel.TenantDetail.CPHONE2;
                        loData.CTENANT_CATEGORY_ID = _viewModel.TenantDetail.CTENANT_CATEGORY_ID;
                        loData.CATTENTION1_NAME = _viewModel.TenantDetail.CATTENTION1_NAME;
                        loData.CATTENTION1_EMAIL = _viewModel.TenantDetail.CATTENTION1_EMAIL;
                        loData.CATTENTION1_MOBILE_PHONE1 = _viewModel.TenantDetail.CATTENTION1_MOBILE_PHONE1;
                        loData.CSALESMAN_ID = _viewModel.TenantDetail.CSALESMAN_ID;
                        loData.CTAX_TYPE = _viewModel.TenantDetail.CTAX_TYPE;
                        loData.CTAX_ID = _viewModel.TenantDetail.CTAX_ID;
                        loData.CTAX_NAME = _viewModel.TenantDetail.CTAX_NAME;
                        loData.CID_TYPE = _viewModel.TenantDetail.CID_TYPE;
                        loData.CID_NO = _viewModel.TenantDetail.CID_NO;
                        loData.CID_EXPIRED_DATE = _viewModel.TenantDetail.CID_EXPIRED_DATE;
                        loData.CTAX_ADDRESS = _viewModel.TenantDetail.CTAX_ADDRESS;
                    };
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion
        #region Lookup Button Building
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
                PMT01700LOO_Offer_SelectedOfferDTO loGetData = (PMT01700LOO_Offer_SelectedOfferDTO)_viewModel.Data;

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
                PMT01700LOO_Offer_SelectedOfferDTO loGetData = (PMT01700LOO_Offer_SelectedOfferDTO)_viewModel.Data;

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
                PMT01700LOO_Offer_SelectedOfferDTO loGetData = (PMT01700LOO_Offer_SelectedOfferDTO)_viewModel.Data;

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
        #region Lookup Button Currency Lookup

        private R_Lookup? R_LookupCurrencyLookup;

        private void BeforeOpenLookUpCurrencyLookup(R_BeforeOpenLookupEventArgs eventArgs)
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

        private void AfterOpenLookUpCurrencyLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            GSL00300DTO? loTempResult = null;

            try
            {
                loTempResult = (GSL00300DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;


                _viewModel.Data.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }

        private async Task OnLostCurrencyRule()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMT01700LOO_Offer_SelectedOfferDTO loGetData = _viewModel.Data;

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
                    loGetData.CBILLING_RULE_CODE = "";
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
            LML01100ParameterDTO? param = null;
            if (!string.IsNullOrEmpty(_viewModel.oParameter.CPROPERTY_ID))
            {
                param = new LML01100ParameterDTO
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID
                };
            }
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML01100);
        }

        private void AfterOpenLookUpTandCLookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            LML01100DTO? loTempResult = null;

            try
            {
                loTempResult = (LML01100DTO)eventArgs.Result;
                if (loTempResult == null)
                    return;

                _viewModel.Data.CTC_CODE = loTempResult.CTC_CODE;
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
                PMT01700LOO_Offer_SelectedOfferDTO loGetData = _viewModel.Data;

                if (string.IsNullOrWhiteSpace(_viewModel.Data.CTC_CODE))
                {
                    loGetData.CTC_CODE = "";
                    return;
                }

                LookupLML01100ViewModel loLookupViewModel = new LookupLML01100ViewModel();
                LML01100ParameterDTO loParam = new LML01100ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.oParameter.CPROPERTY_ID,
                    CSEARCH_TEXT = loGetData.CTC_CODE ?? "",
                };

                var loResult = await loLookupViewModel.GetTermNCondition(loParam);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CTC_CODE = "";
                    //await GLAccount_TextBox.FocusAsync();
                }
                else
                {
                    loGetData.CTC_CODE = loResult.CTC_CODE;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion
    }
}
