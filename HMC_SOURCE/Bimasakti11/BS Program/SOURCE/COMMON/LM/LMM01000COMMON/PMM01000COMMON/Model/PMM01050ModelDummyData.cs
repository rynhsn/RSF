using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using System.Collections.Generic;

namespace PMM01000COMMON.Models
{
    public static class GenerateDataModelRateOT
    {
        public static PMM01050ResultPrintDTO DefaultData()
        {
            PMM01050ResultPrintDTO loData = new PMM01050ResultPrintDTO()
            {
                Title = "Overtime Rate",
                Header = "JBMPC - Metro Park Residence",
            };

            // Create the header data (LMM01020DTO)
            PMM01050DTO loHeaderTempData = new PMM01050DTO
            {
                CCOMPANY_ID = "ABC123",
                CPROPERTY_ID = "PROP456",
                CCHARGES_TYPE = "Electricity",
                CCHARGES_ID = "CHARGE001",
                CUSER_ID = "USER001",
                CACTION = "CREATE",
                CCHARGES_NAME = "Electricity Charges",
                CCURRENCY_CODE = "USD",
                CADMIN_FEE = "00",
                CADMIN_FEE_DESCR = "Admin Fee Description",
                NADMIN_FEE_PCT = 0.02m,
                NADMIN_FEE_AMT = 5.0m,
                NUNIT_AREA_VALID_FROM = 100.0m,
                NUNIT_AREA_VALID_TO = 200.0m,
                LCUT_OFF_WEEKDAY = true,
                LHOLIDAY = false,
                LSATURDAY = true,
                LSUNDAY = false
            };

            var loDetailData1 = new List<PMM01051DTO>();

            for (int i = 1; i <= 3; i++)
            {
                loDetailData1.Add(new PMM01051DTO
                {
                    CCOMPANY_ID = "ABC123",
                    CPROPERTY_ID = "PROP456",
                    CCHARGES_TYPE = "Electricity",
                    CCHARGES_ID = string.Format("CHARGEWD00{0}", i),
                    CDAY_TYPE = "WD",
                    CUSER_ID = "USER001",
                    ILEVEL = i,
                    CLEVEL_DESC = string.Format("Level {0}", i),
                    IHOURS_FROM = i * 8,
                    IHOURS_TO = (i + 1) * 6,
                    NRATE_HOUR = 0.15m + (i * 0.05m)
                });
            }

            // Create detail data for loDetailData2 (List<LMM01051DTO>)
            var loDetailData2 = new List<PMM01051DTO>();

            for (int i = 1; i <= 2; i++)
            {
                loDetailData2.Add(new PMM01051DTO
                {
                    CCOMPANY_ID = "ABC123",
                    CPROPERTY_ID = "PROP456",
                    CCHARGES_TYPE = "Electricity",
                    CCHARGES_ID = string.Format("CHARGEWK00{0}", i),
                    CDAY_TYPE = "WK",
                    CUSER_ID = "USER001",
                    ILEVEL = i + 3,
                    CLEVEL_DESC = string.Format("Level {0}", i),
                    IHOURS_FROM = 9,
                    IHOURS_TO = 15,
                    NRATE_HOUR = 0.18m
                });
            }

            loData.HeaderData = loHeaderTempData;
            loData.DetailWDData = loDetailData1;
            loData.DetailWKData = loDetailData2;

            return loData;
        }

        public static PMM01050ResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "005",
                CPRINT_NAME = "Overtime Rate",
                CUSER_ID = "FMC",
            };

            PMM01050ResultWithBaseHeaderPrintDTO loRtn = new PMM01050ResultWithBaseHeaderPrintDTO { Column = new PMM01050ColumnPrintDTO() };

            loRtn.BaseHeaderData = loParam;
            loRtn.RateOT = GenerateDataModelRateOT.DefaultData();

            return loRtn;
        }
    }

}
