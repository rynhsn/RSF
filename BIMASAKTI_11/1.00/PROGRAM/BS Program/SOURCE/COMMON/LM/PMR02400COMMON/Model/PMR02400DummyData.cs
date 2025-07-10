using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using PMR02400COMMON.DTO_s.Print;
using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;

namespace PMR02400COMMON.DTO_s.Model
{
    public static class PMR02400DummyData
    {
        public static PMR02400PrintDisplayDTO PMR02400PrintDislpayWithBaseHeader()
        {
            PMR02400PrintDisplayDTO loRtn = new PMR02400PrintDisplayDTO();

            loRtn.BaseHeaderData = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "Penalty",
                CUSER_ID = "GHC",
            };
            loRtn.ReportDataDTO = new PMR02400ReportDataDTO();
            loRtn.ReportDataDTO.Header = "PM";
            loRtn.ReportDataDTO.Title = "Penalty";
            loRtn.ReportDataDTO.Label = new PMR02400LabelDTO();
            loRtn.ReportDataDTO.Param = new PMR02400ParamDTO()
            {
                CCOMPANY_ID = "RCD",
                CPROPERTY_ID = "ProeprtyID",
                CPROPERTY_NAME = "Property Name",
                CREPORT_TYPE = "S",
                CREPORT_TYPE_DISPLAY = "Summary",
                LIS_BASED_ON_CUTOFF = true,
                CCUT_OFF_DATE = "20240612",
                CFR_CPERIOD = "20240612",
                CTO_CPERIOD = "20240613",
                CFR_CUSTOMER = "BuildingID001",
                CFROM_CUSTOMER_NAME = "BuildingName001",
                CTO_CUSTOMER = "Cust1",
                CTO_CUSTOMER_NAME = "Cust1 Name",
                CLANGUAGE_ID = "en",
                CCURRENCY_TYPE ="T",
                CREPORT_CURRENCY_TYPE_DISPLAY= "Transaction Currency",
                CREPORT_OPTION_TEXT="Cut Off Date",
                CUSER_ID = "ghc",
            };
            loRtn.ReportDataDTO.Data = new List<SummaryDTO>();

            //setting based on display
            if (loRtn.ReportDataDTO.Param.LIS_BASED_ON_CUTOFF)
            {
                loRtn.ReportDataDTO.Param.CBASED_ON_DISPLAY = DateTime.TryParseExact(loRtn.ReportDataDTO.Param.CCUT_OFF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime poCutOffDate)
                    ? $"{poCutOffDate:dd MMM yyyy}"
                    : "";
            }
            else
            {
                var fromPeriod = DateTime.TryParseExact(loRtn.ReportDataDTO.Param.CFR_CPERIOD + "01", "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fromDate);
                var toPeriod = DateTime.TryParseExact(loRtn.ReportDataDTO.Param.CTO_CPERIOD + "01", "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime toDate);
                loRtn.ReportDataDTO.Param.CBASED_ON_DISPLAY = fromPeriod != toPeriod ? $"{fromDate:dd MMM yyyy} - {toDate:dd MMM yyyy}" : $"{fromDate:dd MMM yyyy}";
            }

            //generate dummya data
            var tenants = new[]
            {
                new { CTENANT_ID = "T001", CTENANT_NAME = "Tenant A" },
                new { CTENANT_ID = "T002", CTENANT_NAME = "Tenant B" }
            };
            var agreements = new[]
            {
                new { CAGREEMENT_NO = "AGR001", CUNIT_DESCRIPTION = "Unit 1" },
                new { CAGREEMENT_NO = "AGR002", CUNIT_DESCRIPTION = "Unit 2" },
                //new { CAGREEMENT_NO = "AGR003", CUNIT_DESCRIPTION = "Unit 3" }
            };
            var currencies = new[]
            {
                new { CCURRENCY_CODE = "USD", CCURRENCY_NAME = "US Dollar" },
                new { CCURRENCY_CODE = "EUR", CCURRENCY_NAME = "Euro" },
                //new { CCURRENCY_CODE = "JPY", CCURRENCY_NAME = "Japanese Yen" },
                new { CCURRENCY_CODE = "IDR", CCURRENCY_NAME = "Indonesian Rupiah" }
            };
            var loSPDummyData = new List<PMR02400SPResultDTO>();
            foreach (var tenant in tenants)
            {
                foreach (var agreement in agreements)
                {
                    foreach (var currency in currencies)
                    {
                        loSPDummyData.Add(new PMR02400SPResultDTO
                        {
                            CCOMPANY_ID = "COMP001",
                            CPROPERTY_ID = "PROP001",
                            CTENANT_ID = tenant.CTENANT_ID,
                            CTENANT_NAME = tenant.CTENANT_NAME,
                            CAGREEMENT_NO = agreement.CAGREEMENT_NO,
                            CUNIT_DESCRIPTION = agreement.CUNIT_DESCRIPTION,
                            CBUILDING_ID = "BLDG001",
                            CBUILDING_NAME = "Building A",
                            //CINVOICE_NO = Guid.NewGuid().ToString().Substring(0, 8),
                            //CINVOICE_DESCRIPTION = "Invoice for " + agreement.CAGREEMENT_NO,
                            //CDUE_DATE = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd"),
                            //ILATE_DAYS = 0,
                            CCURRENCY_CODE = currency.CCURRENCY_CODE,
                            CCURRENCY_NAME = currency.CCURRENCY_NAME,
                            NINVOICE_AMOUNT = new Random().Next(20, 21),
                            NREDEEMED_AMOUNT = new Random().Next(10, 11),
                            NPAID_AMOUNT = new Random().Next(5,10),
                            NOUTSTANDING_AMOUNT = new Random().Next(10, 11)
                        });
                    }
                }
            }

            var loGroupedData = loSPDummyData
                .GroupBy(a => new { a.CTENANT_ID, a.CTENANT_NAME })
                .Select(loTenantGroup => new SummaryDTO
                {
                    CTENANT_ID = loTenantGroup.Key.CTENANT_ID,
                    CTENANT_NAME = loTenantGroup.Key.CTENANT_NAME,

                    // Full list of agreements for the tenant
                    Agreements = loTenantGroup.Select(a => new AgreementDTO
                    {
                        CAGREEMENT_NO = a.CAGREEMENT_NO,
                        CUNIT_DESCRIPTION = a.CUNIT_DESCRIPTION,
                        CBUILDING_ID = a.CBUILDING_ID,
                        CBUILDING_NAME = a.CBUILDING_NAME,
                        CCURRENCY_CODE = a.CCURRENCY_CODE,
                        CCURRENCY_NAME = a.CCURRENCY_NAME,
                        NINVOICE_AMOUNT = a.NINVOICE_AMOUNT,
                        NREDEEMED_AMOUNT = a.NREDEEMED_AMOUNT,
                        NPAID_AMOUNT = a.NPAID_AMOUNT,
                        NOUTSTANDING_AMOUNT = a.NOUTSTANDING_AMOUNT
                    }).ToList(),

                    // Subtotal for each currency within a tenant
                    SubtotalCurrencies = loTenantGroup
                        .GroupBy(a => new { a.CCURRENCY_CODE, a.CCURRENCY_NAME })
                        .Select(loCurrGroup => new SubtotalCurrencyDTO
                        {
                            CCURRENCY_CODE = loCurrGroup.Key.CCURRENCY_CODE,
                            CCURRENCY_NAME = loCurrGroup.Key.CCURRENCY_NAME,
                            NINVOICE_AMOUNT = loCurrGroup.Sum(a => a.NINVOICE_AMOUNT),
                            NREDEEMED_AMOUNT = loCurrGroup.Sum(a => a.NREDEEMED_AMOUNT),
                            NPAID_AMOUNT = loCurrGroup.Sum(a => a.NPAID_AMOUNT),
                            NOUTSTANDING_AMOUNT = loCurrGroup.Sum(a => a.NOUTSTANDING_AMOUNT)
                        }).ToList()
                }).ToList();
            loRtn.ReportDataDTO.Data = loGroupedData;

            loRtn.ReportDataDTO.GrandTotal = loSPDummyData
                .GroupBy(a => new { a.CCURRENCY_CODE, a.CCURRENCY_NAME })
                        .Select(loCurrGroup => new SubtotalCurrencyDTO
                        {
                            CCURRENCY_CODE = loCurrGroup.Key.CCURRENCY_CODE,
                            CCURRENCY_NAME = loCurrGroup.Key.CCURRENCY_NAME,
                            NINVOICE_AMOUNT = loCurrGroup.Sum(a => a.NINVOICE_AMOUNT),
                            NREDEEMED_AMOUNT = loCurrGroup.Sum(a => a.NREDEEMED_AMOUNT),
                            NPAID_AMOUNT = loCurrGroup.Sum(a => a.NPAID_AMOUNT),
                            NOUTSTANDING_AMOUNT = loCurrGroup.Sum(a => a.NOUTSTANDING_AMOUNT)
                        }).ToList();
            return loRtn;
        }
    }
}