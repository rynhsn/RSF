using PMR02000COMMON;
using PMR02000COMMON.DTO_s;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using PMR02000FrontResources;
using R_BlazorFrontEnd.Interfaces;
using System.Globalization;
using PMR02000COMMON.DTO_s.Print;
using System.Net.NetworkInformation;


namespace PMR02000MODEL.View_Models
{
    public class PMR02000ViewModel
    {
        private PMR02000Model PMR02000model = new PMR02000Model();

        public DateTime _InitToday = new DateTime();

        public PeriodYearDTO _PeriodYear = new PeriodYearDTO();

        public ObservableCollection<PropertyDTO> properties = new ObservableCollection<PropertyDTO>();

        public ReportParamDTO ReportParam = new ReportParamDTO();

        public List<GeneralTypeDTO> _radioDataBasedOnCustomer { get; set; } = new List<GeneralTypeDTO>();
        public List<GeneralTypeDTO> _radioDataBasedOnJrnGrp { get; set; } = new List<GeneralTypeDTO>();

        public string _dataBasedOn { get; set; } = "T";

        public List<GeneralTypeDTO> _radioRemainingDataBasedOnC { get; set; } = new List<GeneralTypeDTO>();
        public List<GeneralTypeDTO> _radioRemainingDataBasedOnL { get; set; } = new List<GeneralTypeDTO>();

        public string _dateBasedOn { get; set; } = "C";

        public DateTime _dateCutOff { get; set; } = new DateTime();

        public List<GeneralTypeDTO> _monthList { get; set; } = Enumerable.Range(1, 12).Select(i => new GeneralTypeDTO
        {
            CTYPE_CODE = i.ToString("D2"),
            CTYPE_NAME = DateTimeFormatInfo.InvariantInfo.GetMonthName(i)
        }).ToList();
        public string _MonthPeriod { get; set; } = "";

        public int _YearPeriod { get; set; } = 0;

        public List<GeneralTypeDTO> _radioReportType { get; set; } = new List<GeneralTypeDTO>();

        public List<GeneralTypeDTO> _radioCurrencyType { get; set; } = new List<GeneralTypeDTO>();

        public List<TransactionTypeDTO> _transTypeList { get; set; } = new List<TransactionTypeDTO>();

        public List<CategoryTypeDTO> _categoryTypeList { get; set; } = new List<CategoryTypeDTO>();

        public bool _enableFilterDept, _enableFilterAllocation, _enableFilterTransType, _enableFilterCustCtg = true;

        public async Task InitProcess(R_ILocalizer<Resources_Dummy_Class> poParamLocalizer)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                //generate type for radio
                _radioDataBasedOnCustomer = new List<GeneralTypeDTO> {
                    new GeneralTypeDTO { CTYPE_CODE = "C", CTYPE_NAME = poParamLocalizer["_radio_Customer"] },
                };

                _radioDataBasedOnJrnGrp = new List<GeneralTypeDTO> {
                    new GeneralTypeDTO { CTYPE_CODE = "J", CTYPE_NAME = poParamLocalizer ["_radio_JournalGroup"] },
                };

                _radioRemainingDataBasedOnC = new List<GeneralTypeDTO> {
                    new GeneralTypeDTO { CTYPE_CODE = "C", CTYPE_NAME = poParamLocalizer["_radio_CutoffRemaining"] },
                };
                _radioRemainingDataBasedOnL = new List<GeneralTypeDTO> {
                    new GeneralTypeDTO { CTYPE_CODE = "L", CTYPE_NAME = poParamLocalizer ["_radio_LastRemaining"] },
                };

                _radioReportType = new List<GeneralTypeDTO> {
                    new GeneralTypeDTO { CTYPE_CODE = "S", CTYPE_NAME = poParamLocalizer["_radio_Summary"] },
                    new GeneralTypeDTO { CTYPE_CODE = "D", CTYPE_NAME = poParamLocalizer ["_radio_Detail"] },
                };

                _radioCurrencyType = new List<GeneralTypeDTO> {
                    new GeneralTypeDTO { CTYPE_CODE = "T", CTYPE_NAME = poParamLocalizer ["_radio_TransCurr"] },
                    new GeneralTypeDTO { CTYPE_CODE = "B", CTYPE_NAME = poParamLocalizer["_radio_BaseCurr"] },
                    new GeneralTypeDTO { CTYPE_CODE = "L", CTYPE_NAME = poParamLocalizer ["_radio_LocalCurr"] },
                };

                //get init data
                properties = new ObservableCollection<PropertyDTO>(await GetPropertyAsync()) ?? new ObservableCollection<PropertyDTO>();
                _PeriodYear = await GetPeriodYearAsync();
                _InitToday = await GetTodayAsync();
                _transTypeList = await GetTransListAsync() ?? new List<TransactionTypeDTO>();


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
                await GetCategoryTypeAsync(new CategoryTypeParamDTO() { CCATEGORY_TYPE = "20", CPROPERTY_ID = properties.FirstOrDefault().CPROPERTY_ID });
                _enableFilterDept = false;
                _enableFilterCustCtg = false;
                _enableFilterTransType= false;
                _YearPeriod = _InitToday.Year;
                _MonthPeriod = _InitToday.Month.ToString("D2");
                _dateCutOff = _InitToday;
                _dataBasedOn = "C";
                ReportParam.CREMAINING_BASED_ON = "C";
                ReportParam.CREPORT_TYPE = "S";
                ReportParam.CCURRENCY_TYPE_CODE = "T";
                ReportParam.CTENANT_CATEGORY_ID = "";
                ReportParam.CFROM_JRNGRP_CODE = "";
                ReportParam.CTO_JRNGRP_CODE = "";
                ReportParam.CTO_DEPT_CODE = "";
                ReportParam.CFR_DEPT_CODE = "";
                ReportParam.CTRANS_CODE = "";
                ReportParam.CTENANT_CATEGORY_ID = "";
                ReportParam.CSORT_BY = "C";
                ReportParam.CREPORT_FILETYPE = "PDF";
                ReportParam.CREPORT_FILENAME = "";
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
                var loResult = await PMR02000model.GetPropertyListAsync();
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
                var loData = await PMR02000model.GetTodayDateAsync();
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
                loRtn = await PMR02000model.GetPeriodYearRecordAsync(new PeriodYearDTO() { CMODE = "", CYEAR = "" });
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public async Task<List<TransactionTypeDTO>> GetTransListAsync()
        {
            R_Exception loEx = new R_Exception();
            List<TransactionTypeDTO> loRtn = null;
            try
            {
                var loResult = await PMR02000model.GetTransTypeListAsync();
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public async Task GetCategoryTypeAsync(CategoryTypeParamDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMR02000ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMR02000ContextConstant.CCATEGORY_TYPE, "20");
                _categoryTypeList = await PMR02000model.GetCustomerTypeListAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
    }
}
