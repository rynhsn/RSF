using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using System.Collections.Generic;

namespace PMM01000COMMON.Models
{
    public static class GenerateDataModelRateEC
    {
        public static PMM01010ResultPrintDTO DefaultData()
        {
            PMM01010ResultPrintDTO loData = new PMM01010ResultPrintDTO()
            {
                Title = "Electricity and Chiller Rate",
                Header = "JBMPC - Metro Park Residence",
            };

            PMM01010DTO loHeaderTempData = new PMM01010DTO
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
                NBUY_LWBP_CHARGE = 0.16m,
                NLWBP_CHARGE = 0.12m,
                NBUY_WBP_CHARGE = 0.20m,
                NWBP_CHARGE = 0.18m,
                NBUY_TRANSFORMATOR_FEE = 5.0m,
                NTRANSFORMATOR_FEE = 4.0m,
                LUSAGE_MIN_CHARGE = true,
                NUSAGE_MIN_CHARGE = 15.0m,
                NKWH_USED_MAX = 1000.0m,
                NKWH_USED_RATE = 0.25m,
                NBUY_KWH_USED_RATE = 0.30m,
                NBUY_KWA_USED_RATE = 0.35m,
                NKWA_USED_RATE = 0.40m,
                NRETRIBUTION_PCT = 0.05m,
                LRETRIBUTION_EXCLUDED_VAT = false,
                CADMIN_FEE = "00",
                CADMIN_FEE_DESCR = "Fixed Amount",
                NADMIN_FEE_PCT = 0.02m,
                NADMIN_FEE_AMT = 5.0m,
                LADMIN_FEE_TAX = true,
                NOTHER_DISINCENTIVE_FACTOR = 0.1m,
                NBUY_KVA_RANGE = 10.0m,
                NKVA_RANGE = 8.0m,
                CRATE_EC_LIST = new List<PMM01011DTO>()
            };

            List<PMM01011DTO> loDetailTempData = new List<PMM01011DTO>();
            for (int i = 1; i <= 5; i++)
            {
                // Create a new LMM01011DTO object for each iteration of the loop
                PMM01011DTO detailItem = new PMM01011DTO
                {
                    CCOMPANY_ID = "ABC123",
                    CPROPERTY_ID = "PROP456",
                    CCHARGES_TYPE = "Electricity",
                    CCHARGES_ID = "CHARGE001",
                    CUSER_ID = "USER001",
                    IUP_TO_USAGE = 100 * i,
                    CUSAGE_DESC = string.Format("Usage up to {0} kWh", i*100),
                    NBUY_LWBP_CHARGE = 0.10m + (i * 0.02m),
                    NLWBP_CHARGE = 0.08m + (i * 0.02m),
                    NBUY_WBP_CHARGE = 0.15m + (i * 0.02m),
                    NWBP_CHARGE = 0.13m + (i * 0.02m)
                };

                // Add the new detail item to the list
                loDetailTempData.Add(detailItem);
            }

            loHeaderTempData.CRATE_EC_LIST.AddRange(loDetailTempData);

            loData.HeaderData = loHeaderTempData;

            return loData;
        }

        public static PMM01010ResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "002",
                CPRINT_NAME = "Electricity and Chiller Rate",
                CUSER_ID = "FMC",
            };

            PMM01010ResultWithBaseHeaderPrintDTO loRtn = new PMM01010ResultWithBaseHeaderPrintDTO{ Column = new PMM01010ColumnPrintDTO() };

            loRtn.BaseHeaderData = loParam;
            loRtn.RateEC = GenerateDataModelRateEC.DefaultData();

            return loRtn;
        }
    }

}
