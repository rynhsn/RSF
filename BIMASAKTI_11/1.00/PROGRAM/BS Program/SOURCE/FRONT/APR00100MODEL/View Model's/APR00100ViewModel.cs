using APR00100COMMON;
using APR00100COMMON.DTO_s;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using APR00100FrontResources;
using R_BlazorFrontEnd.Interfaces;
using System.Globalization;
using APR00100COMMON.DTO_s.Print;
using System.Net.NetworkInformation;


namespace APR00100MODEL.View_Models
{
    public class APR00100ViewModel
    {
        public APR00100Model APR00100model = new APR00100Model();

        public DateTime _InitToday = new DateTime();

        public PeriodYearDTO _PeriodYear = new PeriodYearDTO();

        public ObservableCollection<PropertyDTO> properties = new ObservableCollection<PropertyDTO>();

        public ReportParamDTO ReportParam = new ReportParamDTO();
        public ReportParamDTO SaveAsParam { get; set; } = new ReportParamDTO();
        public List<DropDownDTO> CurrencyTypeRadio1 = new List<DropDownDTO>();
        public List<DropDownDTO> CurrencyTypeRadio2 = new List<DropDownDTO>();
        public List<DropDownDTO> CurrencyTypeRadio3 = new List<DropDownDTO>();
        public string CurrencyTypeRadioSelected = "";
        public List<string> FileType = new List<string> { "XLSX", "XLS", "CSV" };

        public List<GeneralTypeDTO> _radioDataBasedOnCustomer { get; set; } = new List<GeneralTypeDTO>();
        public List<GeneralTypeDTO> _radioDataBasedOnJrnGrp { get; set; } = new List<GeneralTypeDTO>();

        public string _dataBasedOn { get; set; } = "1";

        public List<GeneralTypeDTO> _radioRemainingDataBasedOn { get; set; } = new List<GeneralTypeDTO>();

        public List<GeneralTypeDTO> _radioDateBasedOnCutOff { get; set; } = new List<GeneralTypeDTO>();
        public List<GeneralTypeDTO> _radioDateBasedOnPeriod { get; set; } = new List<GeneralTypeDTO>();

        public string _dateBasedOn { get; set; } = "1";
        public DateTime _dateCutOff { get; set; } = new DateTime();

        public DateTime _dateBasedOnCutOff { get; set; } = new DateTime();

        public List<GeneralTypeDTO> _monthList { get; set; } = Enumerable.Range(1, 12).Select(i => new GeneralTypeDTO
        {
            CTYPE_CODE = i.ToString("D2"),
            CTYPE_NAME = DateTimeFormatInfo.InvariantInfo.GetMonthName(i)
        }).ToList();
        public string _MonthPeriod { get; set; } = "";

        public int _YearPeriod { get; set; } = 0;

        public List<GeneralTypeDTO> _radioReportType { get; set; } = new List<GeneralTypeDTO>();

        public List<GeneralTypeDTO> _radioSortBy { get; set; } = new List<GeneralTypeDTO>();

        public List<GeneralTypeDTO> _radioCurrencyType { get; set; } = new List<GeneralTypeDTO>();

        public List<TransTypeSuppCattDTO> _transTypeList { get; set; } = new List<TransTypeSuppCattDTO>();
        public List<TransTypeSuppCattDTO> _suppCattegoryList { get; set; } = new List<TransTypeSuppCattDTO>();

        public List<CustomerTypeDTO> _customerCtgList { get; set; } = new List<CustomerTypeDTO>();

        public bool _enableFilterDept, _enableFilterAllocation, _enableFilterTransType, _enableFilterSuppCtg = false;

        public async Task InitProcess(R_ILocalizer<Resources_Dummy_Class> poParamLocalizer)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                //generate type for radio
                _radioDataBasedOnCustomer = new List<GeneralTypeDTO> {
                    new GeneralTypeDTO { CTYPE_CODE = "1", CTYPE_NAME = poParamLocalizer["_radio_Customer"] },
                };

                _radioDataBasedOnJrnGrp = new List<GeneralTypeDTO> {
                    new GeneralTypeDTO { CTYPE_CODE = "2", CTYPE_NAME = poParamLocalizer ["_radio_JournalGroup"] },
                };

                _radioRemainingDataBasedOn = new List<GeneralTypeDTO> {
                    new GeneralTypeDTO { CTYPE_CODE = "1", CTYPE_NAME = poParamLocalizer["_radio_CutoffRemaining"] },
                    new GeneralTypeDTO { CTYPE_CODE = "2", CTYPE_NAME = poParamLocalizer ["_radio_LastRemaining"] },
                };

                _radioDateBasedOnCutOff = new List<GeneralTypeDTO> {
                    new GeneralTypeDTO { CTYPE_CODE = "1", CTYPE_NAME = poParamLocalizer["_radio_CutOff"] },
                };

                _radioDateBasedOnPeriod = new List<GeneralTypeDTO> {
                    new GeneralTypeDTO { CTYPE_CODE = "2", CTYPE_NAME = poParamLocalizer ["_radio_Period"] },
                };

                _radioReportType = new List<GeneralTypeDTO> {
                    new GeneralTypeDTO { CTYPE_CODE = "1", CTYPE_NAME = poParamLocalizer["_radio_Summary"] },
                    new GeneralTypeDTO { CTYPE_CODE = "2", CTYPE_NAME = poParamLocalizer ["_radio_Detail"] },
                };

                _radioSortBy = new List<GeneralTypeDTO> {
                    new GeneralTypeDTO { CTYPE_CODE = "1", CTYPE_NAME = poParamLocalizer["_radio_ShortByCustomer"] },
                    new GeneralTypeDTO { CTYPE_CODE = "2", CTYPE_NAME = poParamLocalizer ["_radio_ShortByDate"] },
                };

                _radioCurrencyType = new List<GeneralTypeDTO> {
                    new GeneralTypeDTO { CTYPE_CODE = "1", CTYPE_NAME = poParamLocalizer["_radio_BaseCurr"] },
                    new GeneralTypeDTO { CTYPE_CODE = "2", CTYPE_NAME = poParamLocalizer ["_radio_LocalCurr"] },
                };

                //get init data
                _customerCtgList = await APR00100model.GetCustomerTypeListAsync();
                _transTypeList = await APR00100model.GetTransTypeListAsync();
                _suppCattegoryList = await APR00100model.GetSuppCattegoryListAsync();
                properties = new ObservableCollection<PropertyDTO>(await GetPropertyAsync());
                _PeriodYear = await GetPeriodYearAsync();
                _InitToday = await GetTodayAsync();


                //set default data
                ReportParam.CPROPERTY_ID = properties.Count > 0 ? properties.FirstOrDefault().CPROPERTY_ID : "";
                if (_InitToday == null)
                {
                    _InitToday = DateTime.Now;
                }
                if (_PeriodYear == null)
                {
                    _PeriodYear = new PeriodYearDTO() { IMAX_YEAR = 9999, IMIN_YEAR = 0 };
                }
                _YearPeriod=_InitToday.Year;
                _MonthPeriod = _InitToday.Month.ToString("D2");
                _dateBasedOnCutOff = _InitToday;
                ReportParam.CREMAINING_BASED_ON = "1";
                ReportParam.CREPORT_TYPE = "1";
                ReportParam.CSORT_BY = "1";
                ReportParam.CCURRENCY_TYPE_CODE = "1";
                ReportParam.LALLOCATION = false;
                ReportParam.CCURRENCY_TYPE_CODE = "";
                ReportParam.CSUPPLIER_CATEGORY_CODE = "";
                ReportParam.CFROM_JRNGRP_CODE = "";
                ReportParam.CTO_JRNGRP_CODE = "";
                ReportParam.CTO_DEPT_CODE = "";
                ReportParam.CFROM_DEPT_CODE = "";
                ReportParam.CTRANSACTION_TYPE_CODE = "";
                ReportParam.CSUPPLIER_CATEGORY_CODE = "";
                ReportParam.CREPORT_FILETYPE = "PDF";
                ReportParam.LIS_PRINT = true;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task<List<PropertyDTO>> GetPropertyAsync()
        {
            R_Exception loEx = new R_Exception();
            List<PropertyDTO> loRtn = null;
            try
            {
                var loResult = await APR00100model.GetPropertyListAsync();
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public async Task<DateTime> GetTodayAsync()
        {
            R_Exception loEx = new R_Exception();
            DateTime loRtn = new DateTime();
            try
            {
                var loData = await APR00100model.GetTodayDateAsync();
                loRtn = loData.DTODAY;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public async Task<PeriodYearDTO> GetPeriodYearAsync()
        {
            R_Exception loEx = new R_Exception();
            PeriodYearDTO loRtn = null;
            try
            {
                loRtn = await APR00100model.GetPeriodYearRecordAsync(new PeriodYearDTO() { CMODE = "", CYEAR = "" });
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}
