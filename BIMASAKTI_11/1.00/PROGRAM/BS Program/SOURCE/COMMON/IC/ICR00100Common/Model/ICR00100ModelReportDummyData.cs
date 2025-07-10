using System;
using System.Collections.Generic;
using System.Globalization;
using BaseHeaderReportCOMMON;
using ICR00100Common.DTOs.Print;

namespace ICR00100Common.Model
{
    public class ICR00100ModelReportDummyData
    {
        public static ICR00100ReportResultDTO DefaultData()
        {
            var poParam = new ICR00100ReportParam
            {
                CPROPERTY_ID = "ASHMD",
                CPROPERTY_NAME = "BIMASAKTI",
                CDATE_FILTER = "DATE", // PERIOD: Period, DATE: Date
                CDATE_FILTER_DESC = "Date", // Period, Date
                IPERIOD_YEAR = 2025,
                CPERIOD_MONTH = "01", // 01-12
                CPERIOD = "202501", // yyyyMM
                DFROM_DATE = DateTime.Now - TimeSpan.FromDays(30),
                DTO_DATE = DateTime.Now,
                CFROM_DATE = (DateTime.Now - TimeSpan.FromDays(30)).ToString("yyyyMMdd"), // yyyyMMdd
                CTO_DATE = DateTime.Now.ToString("yyyyMMdd"), // yyyyMMdd
                CWAREHOUSE_CODE = "A", // All
                CWAREHOUSE_NAME = "Any", // All
                // LDEPARTMENT = true, // All
                CDEPT_CODE = "00", // All
                CDEPT_NAME = "Department Name", // All
                LINC_FUTURE_TRANSACTION = true, // All
                // CSUPRESS_MODE = "Print All Transactions", // All
                CFILTER_BY = "CATEGORY", // P, A, C, J
                CFILTER_BY_DESC = "Category", // Product, All, Category, Journal Group
                CFILTER_DATA = "CAA", // All
                CFILTER_DATA_NAME = "Category Name", // All
                COPTION_PRINT = "UNIT1", // QTY, UNIT
                COPTION_PRINT_DESC = "By Qty Unit", // By Qty Unit, By Unit
                LINC_RV_AND_GR_REPLACEMENT = true, // All
                LINC_TRANS_DESC = true, // All
                LSUPRESS_TOTAL_QTY = true, // All
            };

            var loCollection = new List<ICR00100DataResultDTO>();

            for (var i = 0; i < 5; i++)
            {
                loCollection.Add(
                    new ICR00100DataResultDTO()
                    {
                        CPROPERTY_ID = "ASHMD",
                        CDEPT_CODE = "ENG",
                        CDEPT_NAME = "Engineering",
                        CREF_NO = "REFNO/123JJKK00",
                        CREF_DATE = "20220111",
                        CPRODUCT_ID = "PRD001",
                        CPRODUCT_NAME = "Product Name",
                        CUNIT3 = "PCS",
                        CUNIT2 = "MTR",
                        CUNIT1 = "KGS"
                    }
                );
            }

            var loData = new ICR00100ReportResultDTO
            {
                Title = "Stock Take Activity",
                Label = new ICR00100ReportLabelDTO(),
                Header = new ICR00100ReportHeaderDTO
                {
                    CPROPERTY = poParam.CPROPERTY_ID + " - " + poParam.CPROPERTY_NAME,
                    CDEPARTMENT = poParam.CDEPT_CODE == "" ? poParam.CDEPT_CODE + " - " + poParam.CDEPT_NAME : "All",
                    CWAREHOUSE = poParam.CWAREHOUSE_CODE + " - " + poParam.CWAREHOUSE_NAME,
                    CPERIOD = poParam.IPERIOD_YEAR + " - " + poParam.CPERIOD_MONTH,
                    CDATE = poParam.CDATE_FILTER == "DATE" ? poParam.DFROM_DATE + " To " + poParam.DTO_DATE : "",
                    LINC_FUTURE_TRANSACTION = poParam.LINC_FUTURE_TRANSACTION,
                    // CSUPRESS_MODE = poParam.CSUPRESS_MODE,
                    CFILTER_BY = poParam.CFILTER_BY,
                    CFILTER_BY_DESC = poParam.CFILTER_BY_DESC,
                    CFILTER_DATA = poParam.CFILTER_BY == "PROD"
                        ? poParam.CFROM_PROD_ID + (!string.IsNullOrEmpty(poParam.CTO_PROD_ID) ? " To " + poParam.CTO_PROD_ID : "")
                        : poParam.CFILTER_DATA + (!string.IsNullOrEmpty(poParam.CFILTER_DATA_NAME) ? " - " + poParam.CFILTER_DATA_NAME : ""),
                    // CPRODUCT = poParam.CFROM_PROD_ID + (!string.IsNullOrEmpty(poParam.CTO_PROD_ID) ? " - " + poParam.CTO_PROD_ID : ""),
                    COPTION_PRINT = poParam.COPTION_PRINT,
                    COPTION_PRINT_DESC = poParam.COPTION_PRINT == "QTY" ? "By Qty Unit" : "By Unit1"
                },
                Data = loCollection
            };

            foreach (var item in loData.Data)
            {
                item.DREF_DATE = DateTime.TryParseExact(item.CREF_DATE, "yyyyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var refDate)
                    ? refDate
                    : (DateTime?)null;
                item.DDOC_DATE = DateTime.TryParseExact(item.CDOC_DATE, "yyyyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var docDate)
                    ? docDate
                    : (DateTime?)null;
            }

            return loData;
        }

        public static ICR00100ReportWithBaseHeaderDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO
            {
                CCOMPANY_NAME = "PT. Realta Chakradarma",
                CPRINT_CODE = "001",
                CPRINT_NAME = "Product Card",
                CUSER_ID = "RHC"
            };

            var loData = new ICR00100ReportWithBaseHeaderDTO
            {
                BaseHeaderData = loParam,
                Data = DefaultData()
            };

            return loData;
        }
    }
}