using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICR00600Common;
using ICR00600Common.DTOs;
using ICR00600Common.DTOs.Print;
using ICR00600Common.Params;
using ICR00600FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace ICR00600Model.ViewModel
{
    public class ICR00600ViewModel : R_ViewModel<ICR00600DataResultDTO>
    {
        private ICR00600Model _model = new ICR00600Model();
        public ICR00600ReportParam ReportParam = new ICR00600ReportParam();

        public List<ICR00600PropertyDTO> PropertyList = new List<ICR00600PropertyDTO>();
        public ICR00600PeriodYearRangeDTO PeriodYearRange = new ICR00600PeriodYearRangeDTO();
        public ICR00600TransCodeDTO TransCode = new ICR00600TransCodeDTO();
        public List<ICR00600PeriodDTO> PeriodList = new List<ICR00600PeriodDTO>();
        
        public List<KeyValuePair<string, string>> OptionPrintType = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("QTY", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "ByQtyUnit")),
            new KeyValuePair<string, string>("UNIT1", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "ByUnit1"))
        };

        public List<string> FileType = new List<string> { "XLSX", "XLS", "CSV" };

        public async Task Init()
        {
            ReportParam.IPERIOD_YEAR = DateTime.Now.Year;
            ReportParam.COPTION_PRINT = "QTY";
            await GetPropertyList();
            await GetYearRange();
            await GetPeriodList();
            await GetTransCode();
        }

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<ICR00600ListDTO<ICR00600PropertyDTO>>(
                        nameof(IICR00600.ICR00600GetPropertyList));

                if (loReturn.Data == null) return;

                PropertyList = loReturn.Data;
                ReportParam.CPROPERTY_ID = PropertyList[0].CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetYearRange()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<ICR00600SingleDTO<ICR00600PeriodYearRangeDTO>>(
                        nameof(IICR00600.ICR00600GetPeriodYearRange));

                if (loReturn?.Data == null) return;

                PeriodYearRange = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPeriodList()
        {
            var loEx = new R_Exception();
            try
            {
                var lcYear = DateTime.Now.Year.ToString();
                var loParam = new ICR00600PeriodParam() { CYEAR = ReportParam.IPERIOD_YEAR.ToString() };
                var loResult =
                    await _model.GetAsync<ICR00600ListDTO<ICR00600PeriodDTO>, ICR00600PeriodParam>(
                        nameof(IICR00600.ICR00600GetPeriodList), loParam);

                if (loResult.Data == null) return;

                PeriodList = loResult.Data;
                ReportParam.CPERIOD_MONTH = DateTime.Now.Month.ToString("D2");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTransCode()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<ICR00600SingleDTO<ICR00600TransCodeDTO>>(
                        nameof(IICR00600.ICR00600GetTransCode));

                if (loReturn?.Data == null) return;

                TransCode = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }
    }
}