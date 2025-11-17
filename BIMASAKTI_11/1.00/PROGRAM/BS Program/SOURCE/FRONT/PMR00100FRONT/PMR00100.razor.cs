using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00500;
using Microsoft.AspNetCore.Components;
using PMR00100Common.DTOs;
using PMR00100Model;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMR00100Front
{
    public partial class PMR00100 : R_Page
    {
        private PMR00100ViewModel _viewModel = new();
        private R_Conductor _conductorRef;

        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetPropertyList();
                await _viewModel.GetPeriod();
                await GetMonth();
                await GetReportType();
                _viewModel.lnPeriodYear = _viewModel.lnPeriodYearFrom = _viewModel.lnPeriodYearTo = DateTime.Now.Year;
                PMR00100ParamDTO param = new PMR00100ParamDTO()
                {
                    CYEAR = _viewModel.lnPeriodYear.ToString()
                };
                await _viewModel.GetPeriodDTList(param);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private async Task OnPrint()
        {
            R_Exception loException = new R_Exception();
            try
            {
                // Inisialisasi parameter global
                InitializePrintParam();
                // Panggil metode validasi
                ValidateParameters();
                // Konversi dan format nama periode
                ConvertPeriodNames();

                if (_viewModel.llValidation == false)
                {
                    // Panggil metode untuk mendapatkan report berdasarkan jenis
                    await GetReportByType();
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }

        public async Task GetMonth()
        {
            _viewModel.GetMonthList = new List<GetMonthDTO>();
            for (int i = 1;i <= 12;i++)
            {
                string monthId = i.ToString("D2");
                string monthName = _localizer[$"_labelMonth{i}"]; // Convert i to Month enum and get localized name
                GetMonthDTO month = new GetMonthDTO { Id = monthId,Name = monthName };
                _viewModel.GetMonthList.Add(month);
            }
            var firstMonth = _viewModel.GetMonthList.FirstOrDefault();
            if (firstMonth != null)
            {
                _viewModel.lcPeriodMonthTo = _viewModel.lcPeriodMonthFrom = firstMonth.Id;
            }
        }
        public async Task GetReportType()
        {
            _viewModel.GetReportTypeList = new List<ReportTypeDTO>();
            for (int i = 1;i <= 2;i++)
            {
                string ReportTypeId = i.ToString();
                string ReportTypeName = _localizer[$"_labelReportType{i}"];
                ReportTypeDTO ReportType = new ReportTypeDTO { Id = ReportTypeId,Name = ReportTypeName };
                _viewModel.GetReportTypeList.Add(ReportType);
            }
            var firstReportType = _viewModel.GetReportTypeList.FirstOrDefault();
            if (firstReportType != null)
            {
                _viewModel.lcReportType = firstReportType.Id;
            }
        }

        private async Task OnChangedProperty(object? poParam)
        {
            var loEx = new R_Exception();
            string lsProperty = (string)poParam;
            try
            {
                _viewModel.PropertyCode = lsProperty;
                var property = _viewModel.PropertyList.FirstOrDefault(item => item.CPROPERTY_ID == lsProperty);
                if (property != null)
                {
                    _viewModel.PropertyName = property.CPROPERTY_NAME;
                }
                _viewModel.lcDeptCodeTo = _viewModel.lcDeptNameTo = _viewModel.lcDeptCodeFrom = _viewModel.lcDeptNameFrom = _viewModel.lcSalesmanCodeFrom = _viewModel.lcSalesmanNameFrom = _viewModel.lcSalesmanCodeTo = _viewModel.lcSalesmanNameTo = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region lookupDeptFrom
        private R_Lookup R_LookupBtnDeptFrom;
        private R_TextBox R_TextBoxBtnDeptFrom;
        private async Task Before_Open_lookupDeptFrom(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00700ParameterDTO
            {
                CUSER_ID = clientHelper.UserId,
                CCOMPANY_ID = clientHelper.CompanyId
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }
        private void After_Open_lookupDeptFrom(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.lcDeptCodeFrom = loTempResult.CDEPT_CODE;
            _viewModel.lcDeptNameFrom = loTempResult.CDEPT_NAME;
        }
        private async Task OnLostFocusDeptFrom()
        {
            R_Exception loEx = new R_Exception();

            try
            {

                if (string.IsNullOrWhiteSpace(_viewModel.lcDeptCodeFrom))
                {
                    _viewModel.lcDeptCodeFrom = "";
                    _viewModel.lcDeptNameFrom = "";
                    return;
                }


                LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();
                var param = new GSL00700ParameterDTO
                {
                    CUSER_ID = clientHelper.UserId,
                    CCOMPANY_ID = clientHelper.CompanyId,
                    CSEARCH_TEXT = _viewModel.lcDeptCodeFrom,
                };
                var loResult = await loLookupViewModel.GetDepartment(param);

                if (loResult == null)
                {
                    await R_TextBoxBtnDeptTo.FocusAsync();
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    _viewModel.lcDeptCodeFrom = "";
                    _viewModel.lcDeptNameFrom = "";
                }
                else
                {
                    _viewModel.lcDeptCodeFrom = loResult.CDEPT_CODE;
                    _viewModel.lcDeptNameFrom = loResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion
        #region lookupDeptTo
        private R_Lookup R_LookupBtnDeptTo;
        private R_TextBox R_TextBoxBtnDeptTo;
        private async Task Before_Open_lookupDeptTo(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00700ParameterDTO
            {
                CUSER_ID = clientHelper.UserId,
                CCOMPANY_ID = clientHelper.CompanyId
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00700);
        }
        private void After_Open_lookupDeptTo(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00700DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.lcDeptCodeTo = loTempResult.CDEPT_CODE;
            _viewModel.lcDeptNameTo = loTempResult.CDEPT_NAME;
        }
        private async Task OnLostFocusDeptTo()
        {
            R_Exception loEx = new R_Exception();

            try
            {

                if (string.IsNullOrWhiteSpace(_viewModel.lcDeptCodeTo))
                {
                    _viewModel.lcDeptCodeTo = "";
                    _viewModel.lcDeptNameTo = "";
                    return;
                }


                LookupGSL00700ViewModel loLookupViewModel = new LookupGSL00700ViewModel();
                var param = new GSL00700ParameterDTO
                {
                    CUSER_ID = clientHelper.UserId,
                    CCOMPANY_ID = clientHelper.CompanyId,
                    CSEARCH_TEXT = _viewModel.lcDeptCodeTo,
                };
                var loResult = await loLookupViewModel.GetDepartment(param);

                if (loResult == null)
                {
                    await R_TextBoxBtnDeptTo.FocusAsync();
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    _viewModel.lcDeptCodeTo = "";
                    _viewModel.lcDeptNameTo = "";
                }
                else
                {
                    _viewModel.lcDeptCodeTo = loResult.CDEPT_CODE;
                    _viewModel.lcDeptNameTo = loResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region lookupSalesmanFrom
        private R_Lookup R_LookupBtnSalesmanFrom;
        private R_TextBox R_TextBoxBtnSalesmanFrom;
        private async Task Before_Open_lookupSalesmanFrom(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new LML00500ParameterDTO
            {
                CUSER_ID = clientHelper.UserId,
                CCOMPANY_ID = clientHelper.CompanyId,
                CPROPERTY_ID = _viewModel.PropertyCode
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00500);
        }
        private void After_Open_lookupSalesmanFrom(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00500DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.lcSalesmanCodeFrom = loTempResult.CSALESMAN_ID;
            _viewModel.lcSalesmanNameFrom = loTempResult.CSALESMAN_NAME;
        }
        private async Task OnLostFocusSalesmanFrom()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.lcSalesmanCodeFrom))
                {
                    _viewModel.lcSalesmanCodeFrom = "";
                    _viewModel.lcSalesmanNameFrom = "";
                    return;
                }
                LookupLML00500ViewModel loLookupViewModel = new LookupLML00500ViewModel();
                var param = new LML00500ParameterDTO
                {
                    CUSER_ID = clientHelper.UserId,
                    CCOMPANY_ID = clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModel.PropertyCode,
                    CSEARCH_TEXT = _viewModel.lcSalesmanCodeFrom

                };
                var loResult = await loLookupViewModel.GetSalesman(param);



                if (loResult == null)
                {
                    await R_TextBoxBtnSalesmanTo.FocusAsync();
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    _viewModel.lcSalesmanCodeFrom = "";
                    _viewModel.lcSalesmanNameFrom = "";
                }
                else
                {
                    _viewModel.lcSalesmanCodeFrom = loResult.CSALESMAN_ID;
                    _viewModel.lcSalesmanNameFrom = loResult.CSALESMAN_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion
        #region lookupSalesmanTo
        private R_Lookup R_LookupBtnSalesmanTo;
        private R_TextBox R_TextBoxBtnSalesmanTo;
        private async Task Before_Open_lookupSalesmanTo(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new LML00500ParameterDTO
            {
                CUSER_ID = clientHelper.UserId,
                CCOMPANY_ID = clientHelper.CompanyId,
                CPROPERTY_ID = _viewModel.PropertyCode
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00500);
        }
        private void After_Open_lookupSalesmanTo(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00500DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            _viewModel.lcSalesmanCodeTo = loTempResult.CSALESMAN_ID;
            _viewModel.lcSalesmanNameTo = loTempResult.CSALESMAN_NAME;
        }
        private async Task OnLostFocusSalesmanTo()
        {
            R_Exception loEx = new R_Exception();

            try
            {

                if (string.IsNullOrWhiteSpace(_viewModel.lcSalesmanCodeTo))
                {
                    _viewModel.lcSalesmanCodeTo = "";
                    _viewModel.lcSalesmanNameTo = "";
                    return;
                }


                LookupLML00500ViewModel loLookupViewModel = new LookupLML00500ViewModel();
                var param = new LML00500ParameterDTO
                {
                    CUSER_ID = clientHelper.UserId,
                    CCOMPANY_ID = clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModel.PropertyCode,
                    CSEARCH_TEXT = _viewModel.lcSalesmanCodeTo
                };
                var loResult = await loLookupViewModel.GetSalesman(param);

                if (loResult == null)
                {
                    await R_TextBoxBtnSalesmanTo.FocusAsync();
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    _viewModel.lcSalesmanCodeTo = "";
                    _viewModel.lcSalesmanNameTo = "";
                }
                else
                {
                    _viewModel.lcSalesmanCodeTo = loResult.CSALESMAN_ID;
                    _viewModel.lcSalesmanNameTo = loResult.CSALESMAN_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region Helper
        public string ConvertPeriodToDateString(string period)
        {
            if (period.Length != 6)
            {
                // Mengembalikan string kosong atau pesan kesalahan jika panjang tidak sesuai
                return string.Empty; // atau "Invalid period format"
            }

            int year = int.Parse(period.Substring(0,4));
            int month = int.Parse(period.Substring(4,2));

            // Pastikan bulan valid
            if (month < 1 || month > 12)
            {
                return string.Empty; // atau "Invalid month"
            }

            DateTime date = new DateTime(year,month,1);
            return date.ToString("MMMM yyyy",CultureInfo.InvariantCulture);
        }
        static DateTime? ConvertToDateTime(string dateString)
        {
            if (dateString.Length != 6)
            {
                // Mengembalikan nilai default jika panjang tidak sesuai
                return null;
            }
            // ParseExact akan mengonversi string menjadi objek DateTime sesuai format yang ditentukan
            return DateTime.ParseExact(dateString,"yyyyMM",CultureInfo.InvariantCulture);
        }
        private void InitializePrintParam()
        {
            R_Exception loException = new R_Exception();
            try
            {
                _viewModel.param = new PrintParamDTO()
                {
                    CCOMPANY_ID = clientHelper.CompanyId,
                    CUSER_ID = clientHelper.UserId,
                    CPROPERTY_ID = _viewModel.PropertyCode,
                    CPROPERTY_NAME = _viewModel.PropertyName,

                    CFROM_DEPT_CODE = _viewModel.lcDeptCodeFrom,
                    CFROM_DEPT_NAME = _viewModel.lcDeptNameFrom,

                    CTO_DEPT_CODE = _viewModel.lcDeptCodeTo,
                    CTO_DEPT_NAME = _viewModel.lcDeptNameTo,

                    CFROM_SALESMAN_ID = _viewModel.lcSalesmanCodeFrom,
                    CFROM_SALESMAN_NAME = _viewModel.lcSalesmanNameFrom,

                    CTO_SALESMAN_ID = _viewModel.lcSalesmanCodeTo,
                    CTO_SALESMAN_NAME = _viewModel.lcSalesmanNameTo,

                    CFROM_PERIOD = _viewModel.lnPeriodYearFrom + _viewModel.lcPeriodMonthFrom,
                    CTO_PERIOD = _viewModel.lnPeriodYearTo + _viewModel.lcPeriodMonthTo,
                    CLANG_ID = clientHelper.Culture.ToString(),
                    CREPORT_TYPE = _viewModel.lcReportType,
                    LIS_PRINT = true
                };
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

        }
        private void ConvertPeriodNames()
        {
            // Memeriksa panjang dari period
            if (_viewModel.param.CTO_PERIOD.Length != 6 || _viewModel.param.CFROM_PERIOD.Length != 6)
            {
                // Jika panjang tidak sesuai, lewati proses
                return;
            }

            DateTime? CTO_PERIOD = ConvertToDateTime(_viewModel.param.CTO_PERIOD);
            DateTime? CFROM_PERIOD = ConvertToDateTime(_viewModel.param.CFROM_PERIOD);

            // Memeriksa apakah nilai yang dihasilkan dari konversi tidak null sebelum digunakan
            if (CFROM_PERIOD.HasValue)
            {
                _viewModel.param.CFROM_PERIOD_NAME = CFROM_PERIOD.Value.ToString("MMMM yyyy",CultureInfo.InvariantCulture);
            }

            if (CTO_PERIOD.HasValue)
            {
                _viewModel.param.CTO_PERIOD_NAME = CTO_PERIOD.Value.ToString("MMMM yyyy",CultureInfo.InvariantCulture);
            }
        }

        private async Task GetReportByType()
        {
            if (_viewModel.lcReportType == "1")
            {
                _viewModel.param.CREPORT_TYPENAME = _localizer["_labelReportType1"];
                await _reportService.GetReport(
                    "R_DefaultServiceUrlPM",
                    "PM",
                    "rpt/PMR00100ReportSummary/LOOStatusReportPost",
                    "rpt/PMR00100ReportSummary/LOOStatusReportGet",
                    _viewModel.param);
            }
            else if (_viewModel.lcReportType == "2")
            {
                _viewModel.param.CREPORT_TYPENAME = _localizer["_labelReportType2"];
                await _reportService.GetReport(
                    "R_DefaultServiceUrlPM",
                    "PM",
                    "rpt/PMR00100ReportDetail/LOOStatusReportPost",
                    "rpt/PMR00100ReportDetail/LOOStatusReportGet",
                    _viewModel.param);
            }
        }
        private void ValidateParameters()
        {
            R_Exception loException = new R_Exception();
            try
            {
                _viewModel.llValidation = false;

                if (string.IsNullOrWhiteSpace(_viewModel.param.CPROPERTY_ID))
                {
                    _viewModel.llValidation = true;
                    loException.Add(new Exception(_localizer["_validationProperty"]));
                }
                if (string.IsNullOrWhiteSpace(_viewModel.param.CFROM_DEPT_CODE))
                {
                    _viewModel.llValidation = true;
                    loException.Add(new Exception(_localizer["_validationDeptFrom"]));
                }
                if (string.IsNullOrWhiteSpace(_viewModel.param.CTO_DEPT_CODE))
                {
                    _viewModel.llValidation = true;
                    loException.Add(new Exception(_localizer["_validationDeptTo"]));
                }
                if (string.IsNullOrWhiteSpace(_viewModel.param.CFROM_SALESMAN_ID))
                {
                    _viewModel.llValidation = true;
                    loException.Add(new Exception(_localizer["_validationSalesmanFrom"]));
                }
                if (string.IsNullOrWhiteSpace(_viewModel.param.CTO_SALESMAN_ID))
                {
                    _viewModel.llValidation = true;
                    loException.Add(new Exception(_localizer["_validationSalesmanTo"]));
                }

                if (_viewModel.lnPeriodYearFrom == 0 || _viewModel.lnPeriodYearFrom == null)
                {
                    _viewModel.llValidation = true;
                    loException.Add(new Exception(_localizer["_validationPeriodYearFrom"]));
                }
                if (_viewModel.lnPeriodYearTo == 0 || _viewModel.lnPeriodYearTo == null)
                {
                    _viewModel.llValidation = true;
                    loException.Add(new Exception(_localizer["_validationPeriodYearTo"]));
                }

                if (string.IsNullOrWhiteSpace(_viewModel.lcPeriodMonthFrom))
                {
                    _viewModel.llValidation = true;
                    loException.Add(new Exception(_localizer["_validationPeriodMonthFrom"]));
                }
                if (string.IsNullOrWhiteSpace(_viewModel.lcPeriodMonthTo))
                {
                    _viewModel.llValidation = true;
                    loException.Add(new Exception(_localizer["_validationPeriodMonthTo"]));
                }
                DateTime? CTO_PERIOD = ConvertToDateTime(_viewModel.param.CTO_PERIOD);
                DateTime? CFROM_PERIOD = ConvertToDateTime(_viewModel.param.CFROM_PERIOD);
                _viewModel.param.CFROM_PERIOD_NAME = ConvertPeriodToDateString(_viewModel.param.CFROM_PERIOD);
                _viewModel.param.CTO_PERIOD_NAME = ConvertPeriodToDateString(_viewModel.param.CTO_PERIOD);

                if (CTO_PERIOD < CFROM_PERIOD)
                {
                    _viewModel.llValidation = true;
                    loException.Add(new Exception(_localizer["_validationCTOnothigherthanCFROM"]));
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            R_DisplayException(loException);
            //loException.ThrowExceptionIfErrors();
        }

        #endregion

        #region SaveAs
        private void BeforeOpen_PopupSaveAsAsync(R_BeforeOpenPopupEventArgs eventArgs)
        {
            InitializePrintParam();
            ValidateParameters();
            _viewModel.param.LIS_PRINT = false;
            eventArgs.PageTitle = _localizer["_titleSaveAs"];
            eventArgs.TargetPageType = typeof(PMR00101);
        }
        private async Task AfterOpen_PopupSaveAs(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Success)
                {
                    var loReturn = R_FrontUtility.ConvertObjectToObject<SaveAsDTO>(eventArgs.Result);
                    ValidateParameters();
                    await Generate_Param(loReturn);
                    await GetReportByType();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }
        private async Task Generate_Param(SaveAsDTO poParam = null)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                InitializePrintParam();
                if (poParam != null)
                {
                    _viewModel.param.LIS_PRINT = poParam.LIS_PRINT;
                    _viewModel.param.CREPORT_FILENAME = poParam.CREPORT_FILENAME;
                    _viewModel.param.CREPORT_FILETYPE = poParam.CREPORT_FILETYPE;
                }
                else
                {
                    _viewModel.param.LIS_PRINT = true;
                    _viewModel.param.CREPORT_FILENAME = "";
                    _viewModel.param.CREPORT_FILETYPE = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }
        #endregion
    }
}
