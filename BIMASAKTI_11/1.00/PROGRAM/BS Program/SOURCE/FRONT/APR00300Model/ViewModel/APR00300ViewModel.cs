using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APR00300Common;
using APR00300Common.DTOs;
using APR00300Common.DTOs.Print;
using APR00300FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace APR00300Model.ViewModel
{
    public class APR00300ViewModel : R_ViewModel<APR00300DataResultDTO>
    {
        private APR00300Model _model = new APR00300Model();
        public List<APR00300PropertyDTO> PropertyList = new List<APR00300PropertyDTO>();
        public APR00300ReportParam ReportParam = new APR00300ReportParam();

        public APR00300PeriodYearRangeDTO YearRange = new APR00300PeriodYearRangeDTO();
        public APR00300TodayDTO Today = new APR00300TodayDTO();

        public KeyValuePair<string, string>[] DateBasedOnCutOff = new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("C", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "CutOff"))
        };

        public KeyValuePair<string, string>[] DateBasedOnPeriod = new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("P", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Period"))
        };

        public KeyValuePair<string, string>[] PeriodList = new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("01",
                R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "January")),
            new KeyValuePair<string, string>("02",
                R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "February")),
            new KeyValuePair<string, string>("03", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "March")),
            new KeyValuePair<string, string>("04", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "April")),
            new KeyValuePair<string, string>("05", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "May")),
            new KeyValuePair<string, string>("06", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "June")),
            new KeyValuePair<string, string>("07", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "July")),
            new KeyValuePair<string, string>("08",
                R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "August")),
            new KeyValuePair<string, string>("09",
                R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "September")),
            new KeyValuePair<string, string>("10",
                R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "October")),
            new KeyValuePair<string, string>("11",
                R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "November")),
            new KeyValuePair<string, string>("12",
                R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "December"))
        };

        public DateTime DCUT_OFF_DATE_TEMP { get; set; }
        public int IFROM_YEAR_TEMP { get; set; }
        public string CFROM_MONTH_TEMP { get; set; }
        public int ITO_YEAR_TEMP { get; set; }
        public string CTO_MONTH_TEMP { get; set; }


        public List<string> FileType = new List<string> { "XLSX", "XLS", "CSV" };

        public async Task Init()
        {
            await GetPropertyList();
            await GetYearRange();
            await GetToday();

            ReportParam.CPROPERTY_ID = PropertyList.Count > 0 ? PropertyList[0].CPROPERTY_ID : ReportParam.CPROPERTY_ID;
            ReportParam.DCUT_OFF_DATE = Today.DTODAY;
            ReportParam.IFROM_YEAR = Today.IYEAR;
            ReportParam.CFROM_MONTH = Today.CMONTH;
            ReportParam.ITO_YEAR = Today.IYEAR;
            ReportParam.CTO_MONTH = Today.CMONTH;
            ReportParam.DSTATEMENT_DATE = Today.DTODAY;
            ReportParam.CDATE_BASED_ON = "C";

            DCUT_OFF_DATE_TEMP = Today.DTODAY ?? DateTime.Now;
            IFROM_YEAR_TEMP = Today.IYEAR;
            CFROM_MONTH_TEMP = Today.CMONTH;
            ITO_YEAR_TEMP = Today.IYEAR;
            CTO_MONTH_TEMP = Today.CMONTH;
        }

        private async Task GetPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<APR00300ListDTO<APR00300PropertyDTO>>(
                        nameof(IAPR00300.APR00300GetPropertyList));
                PropertyList = loReturn.Data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task GetYearRange()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<APR00300SingleDTO<APR00300PeriodYearRangeDTO>>(
                        nameof(IAPR00300.APR00300GetYearRange));
                YearRange = loReturn.Data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task GetToday()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<APR00300SingleDTO<APR00300TodayDTO>>(nameof(IAPR00300.APR00300GetToday));
                Today = loReturn.Data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void ValidateDataBeforePrint()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrEmpty(ReportParam.CPROPERTY_ID))
                {
                    loEx.Add("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "PleaseSelectProperty"));
                }
                
                if(ReportParam.CDATE_BASED_ON == "C" && ReportParam.DCUT_OFF_DATE == null)
                {
                    loEx.Add("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "PleaseSelectCutOffDate"));
                }
                
                if(ReportParam.CDATE_BASED_ON == "P" && (ReportParam.IFROM_YEAR == 0 || string.IsNullOrEmpty(ReportParam.CFROM_MONTH) || ReportParam.ITO_YEAR == 0 || string.IsNullOrEmpty(ReportParam.CTO_MONTH)))
                {
                    loEx.Add("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "PleaseSelectPeriod"));
                }

                if (ReportParam.DSTATEMENT_DATE == null)
                {
                    loEx.Add("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "PleaseSelectStatementDate"));
                }

                if (ReportParam.CDATE_BASED_ON == "C")
                {
                    ReportParam.CFROM_PERIOD = "";
                    ReportParam.CTO_PERIOD = "";
                    ReportParam.CCUT_OFF_DATE = ReportParam.DCUT_OFF_DATE?.ToString("yyyyMMdd") ?? string.Empty;
                }
                else if(ReportParam.CDATE_BASED_ON == "P")
                {
                    ReportParam.CFROM_PERIOD = ReportParam.IFROM_YEAR + ReportParam.CFROM_MONTH;
                    ReportParam.CTO_PERIOD = ReportParam.ITO_YEAR + ReportParam.CTO_MONTH;
                    ReportParam.CCUT_OFF_DATE = "";
                }
                
                ReportParam.CPROPERTY_NAME = PropertyList.Find(x => x.CPROPERTY_ID == ReportParam.CPROPERTY_ID)?.CPROPERTY_NAME ?? string.Empty;
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}