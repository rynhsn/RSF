using PMR00600COMMON;
using PMR00600COMMON.DTO_s;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using PMR00600FrontResources;
using R_BlazorFrontEnd.Interfaces;
using System.Globalization;
using PMR00600COMMON.DTO_s.Print;
using System.Net.NetworkInformation;


namespace PMR00600MODEL.View_Models
{
    public class PMR00600ViewModel
    {
        private PMR00600Model _PMR00600model = new PMR00600Model();
        public ObservableCollection<PropertyDTO> _properties = new ObservableCollection<PropertyDTO>();
        public PMR00600ParamDTO _ReportParam = new PMR00600ParamDTO();
        public List<ReportTypeDTO> _radioReportTypeList { get; set; } = new List<ReportTypeDTO>();
        public List<GroupTypeDTO> _radioGroupingList { get; set; } = new List<GroupTypeDTO>();
        public List<StatusTypeDTO> _radioStatusList { get; set; } = new List<StatusTypeDTO>();
        public List<InvoiceTypeDTO> _radioInvoiceList { get; set; } = new List<InvoiceTypeDTO>();
        public List<MonthDTO> _monthList { get; set; } = Enumerable.Range(1, 12).Select(i => new MonthDTO
        {
            CNUMBER = i.ToString("D2"),
            CNAME = DateTimeFormatInfo.InvariantInfo.GetMonthName(i)
        }).ToList();

        public DateTime _InitToday = new DateTime();
        public PeriodYearDTO _PeriodYear = new PeriodYearDTO();

        public int _YearPeriod { get; set; } = 0;
        public string _MonthFromPeriod { get; set; } = "";
        public string _MonthToPeriod { get; set; } = "";
        public string _Report_Type { get; set; } = "";
        public string _GroupBy { get; set; } = "";
        public string _Status { get; set; } = "";
        public string _Invoice { get; set; } = "";

        public async Task InitProcess(R_ILocalizer<Resources_Dummy_Class> poParamLocalizer)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                //generate type for radio
                _radioReportTypeList = new List<ReportTypeDTO> {
                    new ReportTypeDTO { CTYPE_CODE = "1", CTYPE_NAME = poParamLocalizer["_radio_Summary"] },
                    new ReportTypeDTO { CTYPE_CODE = "2", CTYPE_NAME = poParamLocalizer ["_radio_Detail"] },
                };
                _radioGroupingList = new List<GroupTypeDTO> {
                    new GroupTypeDTO { CTYPE_CODE = "1", CTYPE_NAME = poParamLocalizer["_radio_Tenant"] },
                    new GroupTypeDTO { CTYPE_CODE = "2", CTYPE_NAME = poParamLocalizer["_radio_Charge"] },
                };
                _radioInvoiceList = new List<InvoiceTypeDTO> {
                 new InvoiceTypeDTO { CTYPE_CODE = "1", CTYPE_NAME = poParamLocalizer["_radio_Invoiced"] },
                 new InvoiceTypeDTO { CTYPE_CODE = "2", CTYPE_NAME = poParamLocalizer["_radio_Not_Invoiced"] },
                };
                _radioStatusList = new List<StatusTypeDTO> {
                 new StatusTypeDTO { CTYPE_CODE = "1", CTYPE_NAME = poParamLocalizer["_radio_Open"] },
                 new StatusTypeDTO { CTYPE_CODE = "2", CTYPE_NAME = poParamLocalizer["_radio_Closed"] },
                };

                //get init data
                _properties = new ObservableCollection<PropertyDTO>(await GetPropertyAsync());
                if (_properties.Count > 0)
                {
                    _ReportParam.CPROPERTY_ID = _properties.FirstOrDefault().CPROPERTY_ID;
                }

                _InitToday = await GetTodayAsync();
                _PeriodYear = await GetPeriodYearAsync();
                if (_InitToday == null)
                {
                    _InitToday = DateTime.Now;
                }

                //set default data
                _MonthFromPeriod = _InitToday.ToString("MM");
                _MonthToPeriod = _InitToday.ToString("MM");
                _YearPeriod = _InitToday.Year;

                _Report_Type = "1";
                _GroupBy = "1";
                _Status = "1";
                _Invoice = "1";
                _ReportParam.LTENANT = false;
                _ReportParam.LSERVICE = false;
                _ReportParam.LSTATUS = false;
                _ReportParam.LINVOICE = false;
                _ReportParam.CFROM_TENANT_ID = "";
                _ReportParam.CFROM_TENANT_NAME = "";
                _ReportParam.CTO_TENANT_ID = "";
                _ReportParam.CTO_TENANT_NAME = "";
                _ReportParam.CFROM_SERVICE_NAME = "";
                _ReportParam.CTO_SERVICE_NAME = "";
                _ReportParam.CFROM_SERVICE_ID = "";
                _ReportParam.CTO_SERVICE_ID = "";
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
                var loResult = await _PMR00600model.GetPropertyListAsync();
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
                var loData = await _PMR00600model.GetTodayDateAsync();
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
                loRtn = await _PMR00600model.GetPeriodYearRecordAsync(new PeriodYearDTO() { CMODE = "", CYEAR = "" });
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
