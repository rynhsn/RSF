using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMR00460Common;
using PMR00460Common.DTOs;
using PMR00460Common.DTOs.Print;
using PMR00460FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace PMR00460Model.ViewModel
{
    public class PMR00460ViewModel : R_ViewModel<PMR00460DataResultDTO>
    {
        private PMR00460Model _model = new PMR00460Model();
        public List<PMR00460PropertyDTO> PropertyList = new List<PMR00460PropertyDTO>();
        public PMR00460ReportParam ReportParam = new PMR00460ReportParam();
        public PMR00460PeriodYearRangeDTO YearRange = new PMR00460PeriodYearRangeDTO();

        public KeyValuePair<string, string>[] ReportType = new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("S",
                R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Summary")),
            new KeyValuePair<string, string>("D", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Detail"))
        };

        public KeyValuePair<string, string>[] AgreementType =
        {
            new KeyValuePair<string, string>("S", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Strata")),
            new KeyValuePair<string, string>("O", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Owner")),
            new KeyValuePair<string, string>("L", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Lease"))
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


        public List<string> FileType = new List<string> { "XLSX", "XLS", "CSV" };


        public async Task Init()
        {
            await GetPropertyList();
            await GetYearRange();

            //get today dari datetime
            var today = DateTime.Today;
            ReportParam.IFROM_PERIOD_YEAR = today.Year;
            ReportParam.CFROM_PERIOD_MONTH = today.Month.ToString("00");
            ReportParam.ITO_PERIOD_YEAR = today.Year;
            ReportParam.CTO_PERIOD_MONTH = today.Month.ToString("00");
            
            ReportParam.CPROPERTY_ID = PropertyList[0].CPROPERTY_ID;
            ReportParam.CREPORT_TYPE = ReportType[0].Key;
            ReportParam.CTYPE = "S";
        }

        private async Task GetPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<PMR00460ListDTO<PMR00460PropertyDTO>>(
                        nameof(IPMR00460.PMR00460GetPropertyList));
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
                    await _model.GetAsync<PMR00460SingleDTO<PMR00460PeriodYearRangeDTO>>(
                        nameof(IPMR00460.PMR00460GetYearRange));
                YearRange = loReturn.Data;
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
                    loEx.Add("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "PropertyCannotBeEmpty"));
                }
                
                //building
                if (string.IsNullOrEmpty(ReportParam.CFROM_BUILDING_ID))
                {
                    loEx.Add("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "FromBuildingCannotBeEmpty"));
                }
                
                if (string.IsNullOrEmpty(ReportParam.CTO_BUILDING_ID))
                {
                    loEx.Add("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "ToBuildingCannotBeEmpty"));
                }
                
                if(ReportParam.LOPEN == false && ReportParam.LSCHEDULED == false && ReportParam.LCONFIRMED == false && ReportParam.LCLOSED == false)
                {
                    loEx.Add("", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "PleaseSelectStatus"));
                }
                
                ReportParam.CPROPERTY_NAME = PropertyList.Find(x => x.CPROPERTY_ID == ReportParam.CPROPERTY_ID).CPROPERTY_NAME;
                ReportParam.CFROM_PERIOD = ReportParam.IFROM_PERIOD_YEAR + ReportParam.CFROM_PERIOD_MONTH;
                ReportParam.CFROM_PERIOD_MONTH_NAME = Array.Find(PeriodList, x => x.Key == ReportParam.CFROM_PERIOD_MONTH).Value;
                ReportParam.CTO_PERIOD = ReportParam.ITO_PERIOD_YEAR + ReportParam.CTO_PERIOD_MONTH;
                ReportParam.CTO_PERIOD_MONTH_NAME = Array.Find(PeriodList, x => x.Key == ReportParam.CTO_PERIOD_MONTH).Value;
                ReportParam.CREPORT_TYPE_NAME = Array.Find(ReportType, x => x.Key == ReportParam.CREPORT_TYPE).Value;
                ReportParam.CTYPE_NAME = Array.Find(AgreementType, x => x.Key == ReportParam.CTYPE).Value;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}