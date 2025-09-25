using BaseHeaderReportCOMMON;
using PMR03100COMMON.DTO_s.DB;
using PMR03100COMMON.DTO_s.Print;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR03100COMMON.Model
{
    public static class GenerateDataModel
    {
        public static PrintDTO GenerateDummyData()
        {
            var loRtn = new PrintDTO();

            loRtn.BaseHeaderData = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "PMR03100",
                CPRINT_NAME = "AR Collection",
                CUSER_ID = "GHC",
            };

            loRtn.ReportData = new ReportDataDTO()
            {
                Header = "Header",
                Title = "Title",
                Label = new LabelDTO(),
                Param = new ParamDTO()
                {
                    CCOMPANY_ID = "RCD",
                    CPROPERTY_ID = "PROP_ID",
                    CPROPERTY_NAME = "PT Property Name",
                    CCUT_OFF_YEAR = "2024",
                    CREPORT_FILEEXT = "",
                    CREPORT_FILENAME = "",
                    CUSER_ID = "GHC"
                },
                Data = new List<SPResultDTO>()
            };


            for (int i = 1; i <= 20; i++)
            {
                var month = (i % 12) + 1;  // Ensure month is between 1-12
                var year = 2024 + (i / 12); // Adjust the year after every 12 rows

                loRtn.ReportData.Data.Add(new SPResultDTO
                {
                    NINVOICE_AMT = 1000m * i,
                    NDPP = 800m * i,
                    NBUKTI_POTONG_AMT = 200m * i,
                    CREF_NO = $"REF-{i:D17}",  // "REF-000000000000001", etc.
                    NPPH_AMOUNT = 50m * i,
                    CMONTH_YEAR = new DateTime(year, month, 1).ToString("MMMM") // Example: "September"
                });
            }

            return loRtn;
        }
    }
}
