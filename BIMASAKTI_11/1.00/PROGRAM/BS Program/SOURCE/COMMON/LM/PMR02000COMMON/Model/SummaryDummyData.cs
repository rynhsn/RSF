////using APR00100COMMON.DTO_s.Print;
//using BaseHeaderReportCOMMON;
////using PMR02000COMMON.DTO_s;
//using PMR02000COMMON.DTO_s.Print;
//using PMR02000COMMON.DTO_s.Print.Grouping;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;

//namespace PMR02000COMMON.Model
//{
//    public static class SummaryDummyData
//    {
//        public static ReportPrintSummaryDTO GenerateDummyData()
//        {
//            var loRtn = new ReportPrintSummaryDTO();
//            loRtn.BaseHeaderData = new BaseHeaderDTO()
//            {
//                CCOMPANY_NAME = "PT Realta Chakradarma",
//                CPRINT_CODE = "010",
//                CPRINT_NAME = "Overtime",
//                CUSER_ID = "GHC",
//            };
//            loRtn.ReportData = new ReportSummaryDataDTO()
//            {
//                Header = "Header",
//                Title = "Title",
//                Label = new ReportLabelDTO(),
//                Param = new ReportParamDTO()
//                {
//                    CCOMPANY_ID = "RCD",
//                    CPROPERTY_ID = "ASHMD",
//                    CPROPERTY_NAME = "Metro Park Residence",
//                    CFROM_CUSTOMER_ID = "c099",
//                    CFROM_CUSTOMER_NAME = "c099",
//                    CTO_CUSTOMER_ID = "c099",
//                    CTO_CUSTOMER_NAME = "c099",
//                    CFROM_JRNGRP_CODE = "j001",
//                    CTO_JRNGRP_CODE = "j001",
//                    CDATA_BASED_ON_DISPLAY = "Customer",
//                    CBASED_ON = "C",
//                    CREMAINING_BASED_ON = "C",
//                    CREMAINING_BASED_ON_DISPLAY = "Cut Off Remaining",
//                    CCUT_OFF_DATE = "20250101",
//                    DDATE_CUTOFF = DateTime.ParseExact("20250101", "yyyyMMdd", CultureInfo.InvariantCulture),
//                    CPERIOD = "20250101",
//                    CPERIOD_DISPLAY = "20250101".Substring(0, 4) + "-" + "20250101".Substring(4, 2),
//                    CREPORT_TYPE = "1",
//                    CREPORT_TYPE_DISPLAY = "Detail",
//                    CSORT_BY = "C",
//                    CCURRENCY_TYPE_CODE = "1",
//                    IS_TRANS_CURRENCY = true,
//                    IS_BASE_CURRENCY = true,
//                    IS_LOCAL_CURRENCY = true,
//                    IS_DEPT_FILTER_ENABLED = true,
//                    CFR_DEPT_CODE = "ACC",
//                    CFR_DEPT_NAME = "ACCOUNTING",
//                    CTO_DEPT_CODE = "FIN",
//                    CTO_DEPT_NAME = "FINANCE",
//                    IS_TRANSTYPE_FILTER_ENABLED = true,
//                    CTRANSTYPE_FILTER_DISPLAY = "Transaction A",
//                    IS_CUSTCTG_FILTER_ENABLED = true,
//                    CUSTCTG_FILTER_DISPLAY = "Customer CTG B",

//                },
//                Data = new List<DeptDTO>(),
//                GrandTotal = new List<SubtotalCurrenciesDTO>()
//            };
//            var loDummyDBData = new List<APR00100SummaryBySupp1DTO>();

//            // Dummy master data
//            var departments = new List<(string Code, string Name)>
//{
//    ("D001", "Finance"),
//    ("D002", "Operations")
//};

//            var suppliers = new List<(string Id, string Name)>
//{
//    ("SUP001", "Supplier A"),
//    ("SUP002", "Supplier B")
//};

//            var trxTypes = new List<(string Code, string Name)>
//{
//    ("INV", "Invoice"),
//    ("PAY", "Payment")
//};

//            var currencies = new List<string>
//{
//    "IDR", "USD"
//};

//            // Loop generate dummy
//            foreach (var (deptCode, deptName) in departments)
//            {
//                foreach (var (suppId, suppName) in suppliers)
//                {
//                    foreach (var (trxCode, trxName) in trxTypes)
//                    {
//                        foreach (var currency in currencies)
//                        {
//                            loDummyDBData.Add(new APR00100SummaryBySupp1DTO
//                            {
//                                CSUPPLIER_ID = suppId,
//                                CSUPPLIER_NAME = suppName,
//                                CTRX_TYPE_NAME = trxName,
//                                CREF_NO = $"REF-{trxCode}-{Guid.NewGuid().ToString().Substring(0, 6)}",
//                                CREF_DATE = DateTime.Now.ToString("yyyyMMdd"),
//                                DREF_DATE = DateTime.Now,
//                                CSUPPLIER_TYPE_NAME = "Local",
//                                CLOI_AGRMT_NO = $"AGR-{suppId}",
//                                CCURRENCY_CODE = currency,
//                                NBEGINNING_APPLY_AMOUNT = 1000m,
//                                NREMAINING_AMOUNT = 500m,
//                                NTAX_AMOUNT = 100m,
//                                NGAINLOSS_AMOUNT = 20m,
//                                NCASHBANK_AMOUNT = 300m
//                            });
//                        }
//                    }
//                }
//            }

//            // === GROUPING DATA ===
//            var loReport = new ReportSummaryDataDTO
//            {
//                Title = "Account Payable Summary",
//                Header = "Summary by Department and Supplier",
//                Label = new ReportLabelDTO { LabelName = "Example Report" },
//                Param = new ReportParamDTO { Period = "202511", Company = "BSI" },
//            };

//            // Group by department
//            loReport.Data = departments
//                .Select(dept => new APR00100DataResultDTO
//                {
//                    CDEPT_CODE = dept.Code,
//                    CDEPT_NAME = dept.Name,
//                    Detail1 = loDummyDBData
//                        .GroupBy(x => new
//                        {
//                            x.CSUPPLIER_ID,
//                            x.CSUPPLIER_NAME,
//                            x.CTRX_TYPE_NAME,
//                            x.CREF_NO,
//                            x.CREF_DATE,
//                            x.CSUPPLIER_TYPE_NAME,
//                            x.CLOI_AGRMT_NO,
//                            x.CCURRENCY_CODE
//                        })
//                        .Select(g => new APR00100SummaryBySupp1DTO
//                        {
//                            CSUPPLIER_ID = g.Key.CSUPPLIER_ID,
//                            CSUPPLIER_NAME = g.Key.CSUPPLIER_NAME,
//                            CTRX_TYPE_NAME = g.Key.CTRX_TYPE_NAME,
//                            CREF_NO = g.Key.CREF_NO,
//                            CREF_DATE = g.Key.CREF_DATE,
//                            DREF_DATE = DateTime.ParseExact(g.Key.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture),
//                            CSUPPLIER_TYPE_NAME = g.Key.CSUPPLIER_TYPE_NAME,
//                            CLOI_AGRMT_NO = g.Key.CLOI_AGRMT_NO,
//                            CCURRENCY_CODE = g.Key.CCURRENCY_CODE,
//                            NBEGINNING_APPLY_AMOUNT = g.Sum(x => x.NBEGINNING_APPLY_AMOUNT),
//                            NREMAINING_AMOUNT = g.Sum(x => x.NREMAINING_AMOUNT),
//                            NTAX_AMOUNT = g.Sum(x => x.NTAX_AMOUNT),
//                            NGAINLOSS_AMOUNT = g.Sum(x => x.NGAINLOSS_AMOUNT),
//                            NCASHBANK_AMOUNT = g.Sum(x => x.NCASHBANK_AMOUNT),
//                            SuppSubtotalCurr = g
//                                .GroupBy(x => x.CCURRENCY_CODE)
//                                .Select(sub => new SubtotalCurrenciesDTO
//                                {
//                                    CDEPT_CODE = dept.Code,
//                                    CSUPPLIER_ID = g.Key.CSUPPLIER_ID,
//                                    CCURRENCY_CODE = sub.Key,
//                                    NBEGINNING_APPLY_AMOUNT = sub.Sum(x => x.NBEGINNING_APPLY_AMOUNT),
//                                    NREMAINING_AMOUNT = sub.Sum(x => x.NREMAINING_AMOUNT),
//                                    NTAX_AMOUNT = sub.Sum(x => x.NTAX_AMOUNT),
//                                    NGAINLOSS_AMOUNT = sub.Sum(x => x.NGAINLOSS_AMOUNT),
//                                    NCASHBANK_AMOUNT = sub.Sum(x => x.NCASHBANK_AMOUNT)
//                                }).ToList()
//                        }).ToList(),

//                    // Subtotal per dept by currency
//                    DeptSubtotalCurrencies = loDummyDBData
//                        .GroupBy(x => x.CCURRENCY_CODE)
//                        .Select(g => new SubtotalCurrenciesDTO
//                        {
//                            CDEPT_CODE = dept.Code,
//                            CCURRENCY_CODE = g.Key,
//                            NBEGINNING_APPLY_AMOUNT = g.Sum(x => x.NBEGINNING_APPLY_AMOUNT),
//                            NREMAINING_AMOUNT = g.Sum(x => x.NREMAINING_AMOUNT),
//                            NTAX_AMOUNT = g.Sum(x => x.NTAX_AMOUNT),
//                            NGAINLOSS_AMOUNT = g.Sum(x => x.NGAINLOSS_AMOUNT),
//                            NCASHBANK_AMOUNT = g.Sum(x => x.NCASHBANK_AMOUNT)
//                        }).ToList()
//                }).ToList();

//            // === GRAND TOTAL ===
//            loReport.GrandTotal = loDummyDBData
//                .GroupBy(x => x.CCURRENCY_CODE)
//                .Select(g => new SubtotalCurrenciesDTO
//                {
//                    CCURRENCY_CODE = g.Key,
//                    NBEGINNING_APPLY_AMOUNT = g.Sum(x => x.NBEGINNING_APPLY_AMOUNT),
//                    NREMAINING_AMOUNT = g.Sum(x => x.NREMAINING_AMOUNT),
//                    NTAX_AMOUNT = g.Sum(x => x.NTAX_AMOUNT),
//                    NGAINLOSS_AMOUNT = g.Sum(x => x.NGAINLOSS_AMOUNT),
//                    NCASHBANK_AMOUNT = g.Sum(x => x.NCASHBANK_AMOUNT)
//                }).ToList();

//            return loRtn;
//        }
//    }
//}
