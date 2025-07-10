using System;
using System.Collections.Generic;
using System.Globalization;
using BaseHeaderReportCOMMON;
using ICR00600Common.DTOs.Print;

namespace ICR00600Common.Model
{
    public class ICR00600ModelReportDummyData
    {
        public static ICR00600ReportResultDTO DefaultData()
        {
            var poParam = new ICR00600ReportParam
            {
                CPROPERTY_ID = "ASHMD",
                CPROPERTY_NAME = "Harco Mas",
                LDEPT = true,
                CDEPT_CODE = "ENG",
                CDEPT_NAME = "Engineering",
                CPERIOD = "202502",
                IPERIOD_YEAR = 2025,
                CPERIOD_MONTH = "02",
                COPTION_PRINT = "QTY",
                COPTION_PRINT_NAME = "By Qty Unit",
            };

            var loCollection = new List<ICR00600DataResultDTO>();

            for (var i = 0; i < 5; i++)
            {
                loCollection.Add(
                    new ICR00600DataResultDTO()
                    {
                        CCOMPANY_ID = "BSI",
                        CPROPERTY_ID = "ASHMD",
                        CDEPT_CODE = "ENG",
                        CDEPT_NAME = "Engineering",
                        CTRANS_CODE = "TRN",
                        CREF_NO = "REFNO/123JJKK00",
                        CREF_DATE = "20220111",
                        CTRANS_STATUS = "TRN",
                        CSTATUS = "TRN",
                        CDESCRIPTION = "Description",
                        CWAREHOUSE_ID = "WH01",
                        CWAREHOUSE_NAME = "Warehouse 01",
                        CWAREHOUSE = "Warehouse 01",
                        CPRODUCT_ID = "PRD001",
                        CPRODUCT_NAME = "Product Name",
                        CPRODUCT = "Product Name",
                        CPROD_DEPT_CODE = "ENG",
                        CPROD_DEPT_NAME = "Engineering",
                        CPRODUCT_DEPT = "Engineering",
                        NSTOCK_QTY3 = 100,
                        CUNIT3 = "PCS",
                        NSTOCK_QTY2 = 50,
                        CUNIT2 = "MTR",
                        NSTOCK_QTY1 = 25,
                        CUNIT1 = "KGS",
                        NWH_QTY3 = 50,
                        NWH_QTY2 = 55,
                        NWH_QTY1 = 14,
                        NVAR_QTY3 = 10,
                        NVAR_QTY2 = 20,
                        NVAR_QTY1 = 16
                    }
                );
            }
            
            for (var i = 0; i < 2; i++)
            {
                loCollection.Add(
                    new ICR00600DataResultDTO()
                    {
                        CCOMPANY_ID = "BSI",
                        CPROPERTY_ID = "ASHMD",
                        CDEPT_CODE = "ACC",
                        CDEPT_NAME = "Accounting",
                        CTRANS_CODE = "TRN",
                        CREF_NO = "REFNO/123JJKK00",
                        CREF_DATE = "20220111",
                        CTRANS_STATUS = "TRN",
                        CSTATUS = "TRN",
                        CDESCRIPTION = "Description",
                        CWAREHOUSE_ID = "WH01",
                        CWAREHOUSE_NAME = "Warehouse 01",
                        CWAREHOUSE = "Warehouse 01",
                        CPRODUCT_ID = "PRD001",
                        CPRODUCT_NAME = "Product Name",
                        CPRODUCT = "Product Name",
                        CPROD_DEPT_CODE = "ENG",
                        CPROD_DEPT_NAME = "Engineering",
                        CPRODUCT_DEPT = "Engineering",
                        NSTOCK_QTY3 = 100,
                        CUNIT3 = "PCS",
                        NSTOCK_QTY2 = 50,
                        CUNIT2 = "MTR",
                        NSTOCK_QTY1 = 25,
                        CUNIT1 = "KGS",
                        NWH_QTY3 = 50,
                        NWH_QTY2 = 55,
                        NWH_QTY1 = 14,
                        NVAR_QTY3 = 10,
                        NVAR_QTY2 = 20,
                        NVAR_QTY1 = 16
                    }
                );
                
                loCollection.Add(
                    new ICR00600DataResultDTO()
                    {
                        CCOMPANY_ID = "BSI",
                        CPROPERTY_ID = "ASHMD",
                        CDEPT_CODE = "ENG",
                        CDEPT_NAME = "Engineering",
                        CTRANS_CODE = "TRN",
                        CREF_NO = "REFNO/123JJKK001",
                        CREF_DATE = "20220111",
                        CTRANS_STATUS = "TRN",
                        CSTATUS = "TRN",
                        CDESCRIPTION = "Description",
                        CWAREHOUSE_ID = "WH01",
                        CWAREHOUSE_NAME = "Warehouse 01",
                        CWAREHOUSE = "Warehouse 01",
                        CPRODUCT_ID = "PRD001",
                        CPRODUCT_NAME = "Product Name",
                        CPRODUCT = "Product Name",
                        CPROD_DEPT_CODE = "ENG",
                        CPROD_DEPT_NAME = "Engineering",
                        CPRODUCT_DEPT = "Engineering",
                        NSTOCK_QTY3 = 100,
                        CUNIT3 = "PCS",
                        NSTOCK_QTY2 = 50,
                        CUNIT2 = "MTR",
                        NSTOCK_QTY1 = 25,
                        CUNIT1 = "KGS",
                        NWH_QTY3 = 50,
                        NWH_QTY2 = 55,
                        NWH_QTY1 = 14,
                        NVAR_QTY3 = 10,
                        NVAR_QTY2 = 20,
                        NVAR_QTY1 = 16
                    }
                );
            }

            var loData = new ICR00600ReportResultDTO
            {
                Title = "Stock Take Activity",
                Label = new ICR00600ReportLabelDTO(),
                Header = new ICR00600ReportHeaderDTO
                {
                    CPROPERTY = poParam.CPROPERTY_ID + " (" + poParam.CPROPERTY_NAME + ")",
                    CDEPARTMENT = (poParam.LDEPT) ? "All" : poParam.CDEPT_CODE + " (" + poParam.CDEPT_NAME + ")",
                    CPERIOD = poParam.IPERIOD_YEAR + " " + poParam.CPERIOD_MONTH,
                    COPTION_PRINT = poParam.COPTION_PRINT,
                    COPTION_PRINT_NAME = poParam.COPTION_PRINT_NAME,
                },
                Data = loCollection
            };

            foreach (var item in loData.Data)
            {
                item.DREF_DATE = DateTime.TryParseExact(item.CREF_DATE, "yyyyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var refDate)
                    ? refDate
                    : (DateTime?)null;
            }

            return loData;
        }

        public static ICR00600ReportWithBaseHeaderDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO
            {
                CCOMPANY_NAME = "PT. Realta Chakradarma",
                CPRINT_CODE = "001",
                CPRINT_NAME = "Stock Take Activity",
                CUSER_ID = "RHC"
            };

            var loData = new ICR00600ReportWithBaseHeaderDTO
            {
                BaseHeaderData = loParam,
                Data = DefaultData()
            };

            return loData;
        }
    }
}