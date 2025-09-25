using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICR00100Common;
using ICR00100Common.DTOs;
using ICR00100Common.DTOs.Print;
using ICR00100FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace ICR00100Model.ViewModel
{
    public class ICR00100ViewModel : R_ViewModel<ICR00100DataResultDTO>
    {
        private ICR00100Model _model = new ICR00100Model();
        public ICR00100ReportParam ReportParam = new ICR00100ReportParam();
        public ICR00100PeriodYearRangeDTO PeriodYearRange = new ICR00100PeriodYearRangeDTO();
        public List<ICR00100PropertyDTO> PropertyList = new List<ICR00100PropertyDTO>();
        public List<string> FileType = new List<string> { "XLSX", "XLS", "CSV" };

        public List<KeyValuePair<string, string>> PrintOption = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>(R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "QTY"),
                R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "ByQtyUnit")),
            new KeyValuePair<string, string>(R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "UNIT1"),
                R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "ByUnit1"))
        };

        #region Print Mode

        public List<KeyValuePair<string, string>> PrintPeriodMode = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>(R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "PERIOD"),
                R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Period"))
        };

        public List<KeyValuePair<string, string>> PrintDateMode = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>(R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "DATE"),
                R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "DateFrom"))
        };

        #endregion

        #region FilterBy

        // public List<KeyValuePair<string, string>> FilterBy = new List<KeyValuePair<string, string>>
        // {
        //     new KeyValuePair<string, string>("P",
        //         R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Product")),
        //     new KeyValuePair<string, string>("A",
        //         R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Attribute")),
        //     new KeyValuePair<string, string>("C",
        //         R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Category")),
        //     new KeyValuePair<string, string>("J",
        //         R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "JournalGroup"))
        // };
        public List<KeyValuePair<string, string>> FilterByProduct = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>(R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "PROD"),
                R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Product"))
        };

        public List<KeyValuePair<string, string>> FilterByCategory = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>(R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "CATEGORY"),
                R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "Category"))
        };

        public List<KeyValuePair<string, string>> FilterByJournalGroup = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>(R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "JOURNAL"),
                R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "JournalGroup"))
        };

        #endregion

        // public List<KeyValuePair<string, string>> SupressMode = new List<KeyValuePair<string, string>>
        // {
        //     new KeyValuePair<string, string>(
        //         R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "PrintAllTransactions"),
        //         R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "PrintAllTransactions")
        //     ),
        //
        //     new KeyValuePair<string, string>(
        //         R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class),
        //             "SuppressNonActiveTransactionsWithZeroBalance"),
        //         R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class),
        //             "SuppressNonActiveTransactionsWithZeroBalance")
        //     ),
        //
        //     new KeyValuePair<string, string>(
        //         R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "SuppressNonActiveTransactions"),
        //         R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "SuppressNonActiveTransactions")
        //     ),
        // };

        public List<KeyValuePair<string, string>> PeriodList = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("01", "01"),
            new KeyValuePair<string, string>("02", "02"),
            new KeyValuePair<string, string>("03", "03"),
            new KeyValuePair<string, string>("04", "04"),
            new KeyValuePair<string, string>("05", "05"),
            new KeyValuePair<string, string>("06", "06"),
            new KeyValuePair<string, string>("07", "07"),
            new KeyValuePair<string, string>("08", "08"),
            new KeyValuePair<string, string>("09", "09"),
            new KeyValuePair<string, string>("10", "10"),
            new KeyValuePair<string, string>("11", "11"),
            new KeyValuePair<string, string>("12", "12")
        };

        public async Task Init()
        {
            var loEx = new R_Exception();

            try
            {
                await GetPropertyList();
                await GetYearRange();
                ResetParam();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void ResetParam()
        {
            var loEx = new R_Exception();

            try
            {
                ReportParam.IPERIOD_YEAR = DateTime.Now.Year;
                ReportParam.CPERIOD_MONTH = DateTime.Now.ToString("MM");
                // ReportParam.DFROM_DATE = DateTime.Now;
                // ReportParam.DTO_DATE = DateTime.Now;
                ReportParam.COPTION_PRINT = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "QTY");
                ReportParam.CDATE_FILTER = PrintPeriodMode[0].Key;
                ReportParam.CFILTER_BY = FilterByProduct[0].Key;
                // ReportParam.CSUPRESS_MODE = SupressMode[0].Key;
                //ReportParam.CWAREHOUSE_CODE = "";
                //ReportParam.CWAREHOUSE_NAME = "";
                //ReportParam.CFROM_PROD_ID = "";
                //ReportParam.CFROM_PROD_NAME = "";
                ReportParam.CDEPT_CODE = "";
                ReportParam.CDEPT_NAME = "";
                ReportParam.CFILTER_DATA_CATEGORY = "";
                ReportParam.CFILTER_DATA_CATEGORY_NAME = "";
                ReportParam.CFILTER_DATA_JOURNAL = "";
                ReportParam.CFILTER_DATA_JOURNAL_NAME = "";
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
                    await _model.GetAsync<ICR00100SingleDTO<ICR00100PeriodYearRangeDTO>>(
                        nameof(IICR00100.ICR00100GetPeriodYearRange));

                if (loReturn?.Data == null) return;

                PeriodYearRange = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<ICR00100ListDTO<ICR00100PropertyDTO>>(
                        nameof(IICR00100.ICR00100GetPropertyList));

                if (loReturn?.Data == null) return;
                PropertyList = loReturn.Data;
                ReportParam.CPROPERTY_ID = PropertyList.Count > 0 ? PropertyList[0].CPROPERTY_ID : "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}