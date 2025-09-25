using BaseHeaderReportCOMMON;
using PMR02000COMMON.DTO_s;
using PMR02000COMMON.DTO_s.Print;
using PMR02000COMMON.DTO_s.Print.Grouping;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PMR02000COMMON.Model
{
    public static class DetailDummyData
    {
        public static ReportPrintDetailDTO GenerateDummyData()
        {
            var loRtn = new ReportPrintDetailDTO();
            loRtn.BaseHeaderData = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "Overtime",
                CUSER_ID = "GHC",
            };
            loRtn.ReportData = new ReportDetailDataDTO()
            {
                Header = "Header",
                Title = "Title",
                Label = new ReportLabelDTO(),
                Param = new ReportParamDTO()
                {
                    CCOMPANY_ID = "RCD",
                    CPROPERTY_ID = "ASHMD",
                    CPROPERTY_NAME = "Metro Park Residence",
                    CFROM_CUSTOMER_ID = "c099",
                    CFROM_CUSTOMER_NAME = "c099",
                    CTO_CUSTOMER_ID = "c099",
                    CTO_CUSTOMER_NAME = "c099",
                    CFROM_JRNGRP_CODE = "j001",
                    CTO_JRNGRP_CODE = "j001",
                    CDATA_BASED_ON_DISPLAY = "Customer",
                    CBASED_ON = "C",
                    CREMAINING_BASED_ON = "C",
                    CREMAINING_BASED_ON_DISPLAY = "Cut Off Remaining",
                    CCUT_OFF_DATE = "20250101",
                    DDATE_CUTOFF = DateTime.ParseExact("20250101", "yyyyMMdd", CultureInfo.InvariantCulture),
                    CPERIOD = "20250101",
                    CPERIOD_DISPLAY= "20250101".Substring(0, 4) + "-" + "20250101".Substring(4, 2),
                    CREPORT_TYPE = "1",
                    CREPORT_TYPE_DISPLAY = "Detail",
                    CSORT_BY = "C",
                    CCURRENCY_TYPE_CODE = "1",
                    IS_TRANS_CURRENCY = true,
                    IS_BASE_CURRENCY = true,
                    IS_LOCAL_CURRENCY = true,
                    IS_DEPT_FILTER_ENABLED = true,
                    CFR_DEPT_CODE = "ACC",
                    CFR_DEPT_NAME = "ACCOUNTING",
                    CTO_DEPT_CODE = "FIN",
                    CTO_DEPT_NAME = "FINANCE",
                    IS_TRANSTYPE_FILTER_ENABLED = true,
                    CTRANSTYPE_FILTER_DISPLAY = "Transaction A",
                    IS_CUSTCTG_FILTER_ENABLED = true,
                    CUSTCTG_FILTER_DISPLAY = "Customer CTG B",

                },
                Data = new List<DeptDTO>(),
                GrandTotal = new List<SubtotalCurrenciesDTO>()
            };
            var loDummyDBData = new List<PMR02001SpResultDTO>();

            // Generate dummy data
            var departments = new List<(string Code, string Name)>
            {
                ("D001", "Sales"),
                ("D002", "Marketing")
            };

            var customers = new List<(string Id, string Name)>
            {
                ("CU001", "Customer A"),
                ("CU002", "Customer B")
            };

            var transactions = new List<(string Code, string Name)>
            {
                ("T001", "Invoice"),
                ("T002", "Receipt")
            };

            var products = new List<(string Id, string Name)>
            {
                ("PCH001", "Product A"),
                ("PCH002", "Product B")
            };

            var currencies = new List<string>
            {
                ("USD"),
                ("JPN"),
                ("SGD"),
            };

            // Loop through each department
            foreach (var (deptCode, deptName) in departments)
            {
                // Loop through each customer for the current department
                foreach (var (custId, custName) in customers)
                {
                    // Loop through each transaction for the current customer
                    foreach (var (trxCode, trxName) in transactions)
                    {
                        // Loop through each product for the current transaction
                        foreach (var (prodId, prodName) in products)
                        {
                            foreach (var currency in currencies)
                            {
                                // Create and populate a new DTO
                                loDummyDBData.Add(new PMR02001SpResultDTO
                                {
                                    CCOMPANY_ID = "C001", // Dummy company ID
                                    CPROPERTY_ID = "P001", // Dummy property ID
                                    CDEPT_CODE = deptCode,
                                    CDEPT_NAME = deptName,
                                    CDEPT_CODE_NAME = $"{deptCode} - {deptName}",
                                    CCUSTOMER_ID = custId,
                                    CCUSTOMER_NAME = custName,
                                    CTRANS_CODE = trxCode,
                                    CTRX_TYPE_NAME = trxName,
                                    CREF_NO = $"REF-{trxCode}0000000000",
                                    CREF_DATE = DateTime.Now.ToString("yyyyMMdd"),
                                    DREF_DATE = DateTime.Now,
                                    CTENANT_TYPE_ID = "TNT01",
                                    CCUSTOMER_TYPE_NAME = "Regular",
                                    CLOI_AGRMT_NO = "LOI0010000000000",
                                    CCURRENCY_CODE = currency,
                                    NBEGINNING_APPLY_AMOUNT = 1000m,
                                    NREMAINING_AMOUNT = 500m,
                                    NTAX_AMOUNT = 50m,
                                    NGAINLOSS_AMOUNT = 20m,
                                    NCASHBANK_AMOUNT = 200m,
                                    CSEQ_NO = "SEQ001",
                                    CPRODUCT_OR_CHARGE_ID = prodId,
                                    CPRODUCT_OR_CHARGE_NAME = prodName,
                                    CPRODUCT_DEPARTMENT_CODE = deptCode,
                                    CPRODUCT_DEPARTMENT_NAME = deptName,
                                    NPRODUCT_QUANTITY = 10m,
                                    CPRODUCT_MEASUREMENT_NAME = "Pieces",
                                    NPRODUCT_PRICE_AMOUNT = 100m,
                                    NPRODUCT_LINE_AMOUNT = 1000m,
                                    NPRODUCT_DISCOUNT_AMOUNT = 50m,
                                    NOTHER_TAX_AMOUNT = 10m,
                                    NPRODUCT_LINE_TOTAL_AMOUNT = 960m
                                });
                            }
                        }
                    }
                }
            }

            //grouping data 
            loRtn.ReportData.Data = loDummyDBData
                .GroupBy(x => new { x.CDEPT_CODE, x.CDEPT_NAME }) // Group by Department
                .Select(deptGroup => new DeptDTO
                {
                    CDEPT_CODE = deptGroup.Key.CDEPT_CODE,
                    CDEPT_NAME = deptGroup.Key.CDEPT_NAME,
                    Customers = deptGroup
                        .GroupBy(c => new { c.CCUSTOMER_ID, c.CCUSTOMER_NAME }) // Group by Customer
                        .Select(customerGroup => new CustomerDTO
                        {
                            CCUSTOMER_ID = customerGroup.Key.CCUSTOMER_ID,
                            CCUSTOMER_NAME = customerGroup.Key.CCUSTOMER_NAME,

                            Transactions = customerGroup
                                .GroupBy(t => new { t.CTRX_TYPE_NAME, t.CREF_NO, t.CREF_DATE, t.DREF_DATE }) // Group by Transaction
                                .Select(trxGroup => new TrxTypeDTO
                                {
                                    CTRX_TYPE_NAME = trxGroup.Key.CTRX_TYPE_NAME,
                                    CREF_NO = trxGroup.Key.CREF_NO,
                                    CREF_DATE = trxGroup.Key.CREF_DATE,
                                    DREF_DATE = trxGroup.Key.DREF_DATE,
                                    CTENANT_TYPE_ID = trxGroup.First().CTENANT_TYPE_ID,
                                    CCUSTOMER_TYPE_NAME = trxGroup.First().CCUSTOMER_TYPE_NAME,
                                    CLOI_AGRMT_NO = trxGroup.First().CLOI_AGRMT_NO,
                                    CCURRENCY_CODE = trxGroup.First().CCURRENCY_CODE,
                                    NBEGINNING_APPLY_AMOUNT = trxGroup.Sum(t => t.NBEGINNING_APPLY_AMOUNT),
                                    NREMAINING_AMOUNT = trxGroup.Sum(t => t.NREMAINING_AMOUNT),
                                    NTAX_AMOUNT = trxGroup.Sum(t => t.NTAX_AMOUNT),
                                    NGAINLOSS_AMOUNT = trxGroup.Sum(t => t.NGAINLOSS_AMOUNT),
                                    NCASHBANK_AMOUNT = trxGroup.Sum(t => t.NCASHBANK_AMOUNT),

                                }).ToList(),
                            ProductList = customerGroup
                                        .Select(p => new ProductDTO
                                        {
                                            CPRODUCT_OR_CHARGE_ID = p.CPRODUCT_OR_CHARGE_ID,
                                            CPRODUCT_OR_CHARGE_NAME = p.CPRODUCT_OR_CHARGE_NAME,
                                            CPRODUCT_DEPARTMENT_CODE = p.CPRODUCT_DEPARTMENT_CODE,
                                            CPRODUCT_DEPARTMENT_NAME = p.CPRODUCT_DEPARTMENT_NAME,
                                            NPRODUCT_QUANTITY = p.NPRODUCT_QUANTITY,
                                            CPRODUCT_MEASUREMENT_NAME = p.CPRODUCT_MEASUREMENT_NAME,
                                            NPRODUCT_PRICE_AMOUNT = p.NPRODUCT_PRICE_AMOUNT,
                                            NPRODUCT_LINE_AMOUNT = p.NPRODUCT_LINE_AMOUNT,
                                            NPRODUCT_DISCOUNT_AMOUNT = p.NPRODUCT_DISCOUNT_AMOUNT,
                                            NOTHER_TAX_AMOUNT = p.NOTHER_TAX_AMOUNT,
                                            NPRODUCT_LINE_TOTAL_AMOUNT = p.NPRODUCT_LINE_TOTAL_AMOUNT,
                                        }).ToList(),
                            CustSubtotalCurr = customerGroup
                                .GroupBy(c => c.CCURRENCY_CODE) // Group by Currency for Subtotal
                                .Select(currencyGroup => new SubtotalCurrenciesDTO
                                {
                                    CCURRENCY_CODE = currencyGroup.Key,
                                    NBEGINNING_APPLY_AMOUNT = currencyGroup.Sum(c => c.NBEGINNING_APPLY_AMOUNT),
                                    NREMAINING_AMOUNT = currencyGroup.Sum(c => c.NREMAINING_AMOUNT),
                                    NTAX_AMOUNT = currencyGroup.Sum(c => c.NTAX_AMOUNT),
                                    NGAINLOSS_AMOUNT = currencyGroup.Sum(c => c.NGAINLOSS_AMOUNT),
                                    NCASHBANK_AMOUNT = currencyGroup.Sum(c => c.NCASHBANK_AMOUNT)
                                }).ToList()
                        }).ToList(),

                    DeptSubtotalCurrencies = deptGroup
                        .GroupBy(d => d.CCURRENCY_CODE) // Group by Currency for Department Subtotal
                        .Select(currencyGroup => new SubtotalCurrenciesDTO
                        {
                            CCURRENCY_CODE = currencyGroup.Key,
                            NBEGINNING_APPLY_AMOUNT = currencyGroup.Sum(d => d.NBEGINNING_APPLY_AMOUNT),
                            NREMAINING_AMOUNT = currencyGroup.Sum(d => d.NREMAINING_AMOUNT),
                            NTAX_AMOUNT = currencyGroup.Sum(d => d.NTAX_AMOUNT),
                            NGAINLOSS_AMOUNT = currencyGroup.Sum(d => d.NGAINLOSS_AMOUNT),
                            NCASHBANK_AMOUNT = currencyGroup.Sum(d => d.NCASHBANK_AMOUNT)
                        }).ToList()
                }).ToList();

            //grouping currency
            loRtn.ReportData.GrandTotal = loDummyDBData.GroupBy(x => x.CCURRENCY_CODE)
                .Select(currencyGroup => new SubtotalCurrenciesDTO
                {
                    CCURRENCY_CODE = currencyGroup.Key,
                    NBEGINNING_APPLY_AMOUNT = currencyGroup.Sum(c => c.NBEGINNING_APPLY_AMOUNT),
                    NREMAINING_AMOUNT = currencyGroup.Sum(c => c.NREMAINING_AMOUNT),
                    NTAX_AMOUNT = currencyGroup.Sum(c => c.NTAX_AMOUNT),
                    NGAINLOSS_AMOUNT = currencyGroup.Sum(c => c.NGAINLOSS_AMOUNT),
                    NCASHBANK_AMOUNT = currencyGroup.Sum(c => c.NCASHBANK_AMOUNT)
                }).ToList();

            return loRtn;
        }
    }
}
