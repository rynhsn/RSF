using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GLR00100Common;
using GLR00100Common.DTOs;
using GLR00100Common.Params;
using GLR00100FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GLR00100Model.ViewModel
{
    public class GLR00100ViewModel : R_ViewModel<GLR00100SystemParamDTO>
    {
        private GLR00100Model _model = new GLR00100Model();

        public GLR00100ReportParam ReportParam = new GLR00100ReportParam();

        // public GLR00100SystemParamDTO SystemParam = new GLR00100SystemParamDTO();
        public GLR00100PeriodDTO Period = new GLR00100PeriodDTO();
        public List<GLR00100PeriodDTDTO> PeriodListFrom = new List<GLR00100PeriodDTDTO>();
        public List<GLR00100PeriodDTDTO> PeriodListTo = new List<GLR00100PeriodDTDTO>();
        public List<GLR00100TransCodeDTO> TransCodeList = new List<GLR00100TransCodeDTO>();

        public List<KeyValuePair<string, string>> RadioByType = new List<KeyValuePair<string, string>>
        {
            // new KeyValuePair<string, string>("P", "Period"),
            
            new KeyValuePair<string, string>("P", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Period")),
            // new KeyValuePair<string, string>("D", "Date")
            new KeyValuePair<string, string>("D", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Date"))
        };

        public List<KeyValuePair<string, string>> RadioCurrencyType = new List<KeyValuePair<string, string>>
        {
            // new KeyValuePair<string, string>("L", "Local Currency"),
            new KeyValuePair<string, string>("L", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "LocalCurrency")),
            // new KeyValuePair<string, string>("B", "Base Currency"),
            new KeyValuePair<string, string>("B", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "BaseCurrency")),
            // new KeyValuePair<string, string>("T", "Transaction Currency")
            new KeyValuePair<string, string>("T", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "TransactionCurrency"))
        };
        
        public List<KeyValuePair<string, string>> RadioSortBy = new List<KeyValuePair<string, string>>
        {
            // new KeyValuePair<string, string>("D", "Date"),
            new KeyValuePair<string, string>("D", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Date")),
            // new KeyValuePair<string, string>("R", "Reference No.")
            new KeyValuePair<string, string>("R", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "ReferenceNo"))
        };
        
        public List<string> FileType = new List<string> { "XLSX", "XLS", "CSV" };

        public string FromPeriod;
        public string ToPeriod;
        public int YearPeriod;
        public string SuffixPeriod = "01";

        public DateTime? DateFrom;
        public DateTime? DateTo;

        public async Task Init()
        {
            var loEx = new R_Exception();
            try
            {
                await GetSystemParam();
                await GetPeriod();
                await GetPeriodDTList(YearPeriod.ToString());
                ReportParam.CREPORT_TYPE = FileType[0];
                ReportParam.CPERIOD_TYPE = RadioByType[0].Key;
                ReportParam.CCURRENCY_TYPE = RadioCurrencyType[0].Key;
                
                ReportParam.CSORT_BY = RadioSortBy[0].Key;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task GetSystemParam()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult =
                    await _model.GetAsync<GLR00100SingleDTO<GLR00100SystemParamDTO>>(
                        nameof(IGLR00100.GLR00100GetSystemParam));

                YearPeriod = int.Parse(loResult.Data.CSOFT_PERIOD_YY);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task GetPeriod()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult =
                    await _model.GetAsync<GLR00100SingleDTO<GLR00100PeriodDTO>>(nameof(IGLR00100.GLR00100GetPeriod));

                Period = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTransCodeList()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult =
                    await _model.GetListStreamAsync<GLR00100TransCodeDTO>(
                        nameof(IGLR00100.GLR00100GetTransCodeListStream));
                TransCodeList = loResult;
                ReportParam.CTRANS_CODE = TransCodeList[0].CTRANS_CODE;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPeriodDTList(string pcYear)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new GLR00100PeriodDTParam { CYEAR = pcYear };
                var loResult =
                    await _model.GetAsync<GLR00100ListDTO<GLR00100PeriodDTDTO>, GLR00100PeriodDTParam>(
                        nameof(IGLR00100.GLR00100GetPeriodList), loParam);
                PeriodListFrom = loResult.Data;
                PeriodListTo = loResult.Data;
                FromPeriod = PeriodListFrom[0].CPERIOD_NO;
                ToPeriod = PeriodListTo[0].CPERIOD_NO;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void ChangeByType(string pcType)
        {
            if (pcType == "P")
            {
                DateFrom = null;
                DateTo = null;
            }
            else
            {
                DateFrom = DateTime.Today;
                DateTo = DateTime.Today;
            }
        }
    }
}