using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using System.Collections.Generic;

namespace PMM01000COMMON.Models
{
    public static class GenerateDataModelRateWG
    {
        public static PMM01020ResultPrintDTO DefaultData()
        {
            PMM01020ResultPrintDTO loData = new PMM01020ResultPrintDTO()
            {
                Title = "Water and Gas Rate",
                Header = "JBMPC - Metro Park Residence",
            };

            // Create the header data (LMM01020DTO)
            PMM01020DTO loHeaderTempData = new PMM01020DTO
            {
                CCOMPANY_ID = "ABC123",
                CPROPERTY_ID = "PROP456",
                CCHARGES_TYPE = "Electricity",
                CCHARGES_ID = "CHARGE001",
                CUSER_ID = "USER001",
                CACTION = "CREATE",
                CCHARGES_NAME = "Electricity Charges",
                CCURRENCY_CODE = "USD",
                NPIPE_SIZE = 2.5m,
                NBUY_STANDING_CHARGE = 10.5m,
                NSTANDING_CHARGE = 8.2m,
                CUSAGE_RATE_MODE = "Fixed",
                NBUY_USAGE_CHARGE_RATE = 0.20m,
                NUSAGE_CHARGE_RATE = 0.18m,
                LUSAGE_MIN_CHARGE = true,
                NUSAGE_MIN_CHARGE_AMT = 15.0m,
                NMAINTENANCE_FEE = 5.0m,
                CADMIN_FEE = "01",
                CADMIN_FEE_DESCR = "Fixed Amount",
                NADMIN_FEE_PCT = 0.02m,
                NADMIN_FEE_AMT = 5.0m,
                LADMIN_FEE_TAX = true,
                LADMIN_FEE_SC = false,
                LADMIN_FEE_USAGE = true,
                LADMIN_FEE_MAINTENANCE = false,
                CRATE_WG_LIST = new List<PMM01021DTO>()
            };

            // Create some detail data (LMM01021DTO) using a loop
            List<PMM01021DTO> loDetailTempData = new List<PMM01021DTO>();

            for (int i = 1; i <= 5; i++)
            {
                // Create a new LMM01021DTO object for each iteration of the loop
                PMM01021DTO detailItem = new PMM01021DTO
                {
                    CCOMPANY_ID = "ABC123",
                    CPROPERTY_ID = "PROP456",
                    CCHARGES_TYPE = "Electricity",
                    CCHARGES_ID = "CHARGE001",
                    CUSER_ID = "USER001",
                    IUP_TO_USAGE = 100 * i,
                    CUSAGE_DESC = string.Format("Usage up to {0} kWh", 0),
                    NBUY_USAGE_CHARGE = 0.10m + (i * 0.02m),
                    NUSAGE_CHARGE = 0.08m + (i * 0.02m)
                };

                // Add the new detail item to the list
                loDetailTempData.Add(detailItem);
            }

            // Add the detail data to the header data
            loHeaderTempData.CRATE_WG_LIST.AddRange(loDetailTempData);

            loData.HeaderData = loHeaderTempData;

            return loData;
        }

        public static PMM01020ResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "003",
                CPRINT_NAME = "Water and Gas Rate",
                CUSER_ID = "FMC",
            };

            PMM01020ResultWithBaseHeaderPrintDTO loRtn = new PMM01020ResultWithBaseHeaderPrintDTO { Column = new PMM01020ColumnPrintDTO() };

            loRtn.BaseHeaderData = loParam;
            loRtn.RateWG = GenerateDataModelRateWG.DefaultData();

            return loRtn;
        }
    }

}
