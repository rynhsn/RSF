using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;

namespace PMM01000COMMON.Models
{
    public static class GenerateDataModelRatePG
    {
        public static PMM01030ResultPrintDTO DefaultData()
        {
            PMM01030ResultPrintDTO loData = new PMM01030ResultPrintDTO()
            {
                Title = "Parkir and General Fixed Rate",
                Header = "JBMPC - Metro Park Residence",
            };

            // Create the header data (LMM01020DTO)
            PMM01030DTO loHeaderTempData = new PMM01030DTO
            {
                CCOMPANY_ID = "ABC123",
                CPROPERTY_ID = "PROP456",
                CCHARGES_TYPE = "Electricity",
                CCHARGES_ID = "CHARGE001",
                CUSER_ID = "USER001",
                CACTION = "CREATE",
                CCHARGES_NAME = "Electricity Charges",
                CCURRENCY_CODE = "USD",
                NBUY_STANDING_CHARGE = 10.5m,
                NSTANDING_CHARGE = 8.2m,
                NBUY_USAGE_CHARGE_RATE = 0.20m,
                NUSAGE_CHARGE_RATE = 0.18m,
                NMAINTENANCE_FEE = 5.0m,
                CADMIN_FEE = "00",
                CADMIN_FEE_DESCR = "Admin Fee Description",
                NADMIN_FEE_PCT = 0.02m,
                NADMIN_FEE_AMT = 5.0m,
                LADMIN_FEE_TAX = true,
                LADMIN_FEE_SC = false,
                LADMIN_FEE_USAGE = true,
                LADMIN_FEE_MAINTENANCE = false
            };

            loData.HeaderData = loHeaderTempData;

            return loData;
        }

        public static PMM01030ResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "004",
                CPRINT_NAME = "Parkir and General Fixed Rate",
                CUSER_ID = "FMC",
            };

            PMM01030ResultWithBaseHeaderPrintDTO loRtn = new PMM01030ResultWithBaseHeaderPrintDTO { Column = new PMM01030ColumnPrintDTO() };

            loRtn.BaseHeaderData = loParam;
            loRtn.RatePG = GenerateDataModelRatePG.DefaultData();

            return loRtn;
        }
    }

}
