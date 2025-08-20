using System;
using System.Collections.Generic;
using System.Globalization;
using APR00500Common.DTOs.Print;
using BaseHeaderReportCOMMON;

namespace APR00500Common.Model
{
    public class APR00500ModelReportDummyData
    {
        public static APR00500ReportResultDTO DefaultData()
        {
            var loCollection = new List<APR00500DataResultDTO>();
            // ESTENENGINEERING-ESTATE
            var department = "ESTENENGINEERING-ESTATE";
            var departmentName = "Engineering Estate";
            var days_late = new[] { 22, 0, 12, 1, 4, 13, 0, 0, 1, 4, 13, 0, 0 };

            //looping sebanyak isi array days_late
            for (var i = 0; i < days_late.Length; i++)
            {
                loCollection.Add(
                    new APR00500DataResultDTO
                    {
                        CDEPARTMENT_CODE = department,
                        CDEPARTMENT_NAME = departmentName,
                        CREFERENCE_NO = $"REFNO/123JJKK00",
                        CREFERENCE_DATE = "20220111",
                        CSUPPLIER_ID = "XXX",
                        CSUPPLIER_NAME = "XXXXXXXXXX",
                        CINVOICE_PERIOD = "202201",
                        CDUE_DATE = "20220111",
                        CCURRENCY = "IDR",
                        NTOTAL_AMOUNT = 1000000,
                        NDISCOUNT = 1000000,
                        NADD_ON = 1000000,
                        NTAX = 1000000,
                        NADDITION = 1000000,
                        NDEDUCTION = 1000000,
                        NINVOICE_AMOUNT = 1000000,
                        NREMAINING = 1000000,
                        IDAYS_LATE = days_late[i],
                        CUNIT = "ESTMN"
                    }
                );
            }

            // ESTENENGINEERING-ESTATE2
            var department2 = "ESTENENGINEERING-ESTATE2";
            var departmentName2 = "Engineering Estate 2";
            var days_late2 = new[] { 22, 0, 12, 1, 1, 4, 13, 0, 0 };

            //looping sebanyak isi array days_late2
            for (var i = 0; i < days_late2.Length; i++)
            {
                loCollection.Add(
                    new APR00500DataResultDTO
                    {
                        CDEPARTMENT_CODE = department2,
                        CDEPARTMENT_NAME = departmentName2,
                        CREFERENCE_NO = $"REFNO/123JJKK00",
                        CREFERENCE_DATE = "20220111",
                        CSUPPLIER_ID = "XXX",
                        CSUPPLIER_NAME = "XXXXXXXXXX",
                        CINVOICE_PERIOD = "202201",
                        CDUE_DATE = "20220111",
                        CCURRENCY = "IDR",
                        NTOTAL_AMOUNT = 1000000,
                        NDISCOUNT = 1000000,
                        NADD_ON = 1000000,
                        NTAX = 1000000,
                        NADDITION = 1000000,
                        NDEDUCTION = 1000000,
                        NINVOICE_AMOUNT = 1000000,
                        NREMAINING = 1000000,
                        IDAYS_LATE = days_late2[i],
                        CUNIT = "ESTMN"
                    }
                );
            }

            ;

            var loData = new APR00500ReportResultDTO
            {
                Title = "AP Invoice List",
                Label = new APR00500ReportLabelDTO(),
                Header = new APR00500ReportHeaderDTO
                {
                    CPROPERTY_ID = "ASHMD",
                    CPROPERTY_NAME = "Harco Mas",
                    // DCUT_OFF_DATE = DateTime.Now,
                    DCUT_OFF_DATE = DateTime.TryParseExact("20240926", "yyyyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var cutOfDate)
                    ? cutOfDate
                    : (DateTime?)null
                    // CCUT_OFF_DATE_DISPLAY = DateTime.TryParseExact("20240926", "yyyyMMdd",
                    // CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var cutOfDate)
                    // ? cutOfDate.ToString("dd MMM yyyy")
                    // : null
                },
                Data = loCollection
                // Data = new List<APR00500DataResultDTO>()
            };
            
            // loData.Header.CCUT_OFF_DATE_DISPLAY = loData.Header.DCUT_OFF_DATE.ToString("dd-MMM-yyyy");

            //looping semua data agar ref date nya sesuai menjadi dd MMM yyyy
            foreach (var item in loData.Data)
            {
                // item.CREFERENCE_DATE_DISPLAY = DateTime.TryParseExact(item.CREFERENCE_DATE, "yyyyMMdd",
                //     CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var refDate)
                //     ? refDate.ToString("dd MMM yyyy")
                //     : null;
                
                // item.CDUE_DATE_DISPLAY = DateTime.TryParseExact(item.CDUE_DATE, "yyyyMMdd",
                //     CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var dueDate)
                //     ? dueDate.ToString("dd MMM yyyy")
                //     : null;
                item.DREFERENCE_DATE = DateTime.TryParseExact(item.CREFERENCE_DATE, "yyyyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var refDate)
                    ? refDate
                    : (DateTime?)null;
                item.DDUE_DATE = DateTime.TryParseExact(item.CDUE_DATE, "yyyyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var dueDate)
                    ? dueDate
                    : (DateTime?)null;
            }

            return loData; 
        }

        public static APR00500ReportWithBaseHeaderDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO
            {
                CCOMPANY_NAME = "PT. Realta Chakradarma",
                CPRINT_CODE = "001",
                CPRINT_NAME = "AP Invoice List",
                CUSER_ID = "RHC"
            };

            var loData = new APR00500ReportWithBaseHeaderDTO
            {
                BaseHeaderData = loParam,
                Data = DefaultData()
            };

            return loData;
        }
    }
}