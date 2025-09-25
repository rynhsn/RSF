    using BaseHeaderReportCOMMON;
    using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using APR00100COMMON.DTO_s;
using APR00100COMMON.DTO_s.Print;

namespace APR00100COMMON.Model
{
    public static class DetailDummyData
    {
        public static ReportPrintSummaryDTO Print()
        {
            var loRtn = new ReportPrintSummaryDTO();
            loRtn.BaseHeaderData = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chakradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "Supplier Activity",
                CUSER_ID = "RYC",
            };
            bool lTransactionType = true;
            bool lCustCategory = false;

     
            loRtn.ReportData = new ReportSummaryDataDTO()
            {
                Header = "Header",
                Title = "Title",
                Label = new ReportLabelDTO(),
                Param = new ReportParamDTO()
                {
                    CCOMPANY_ID="RCD",
                    CPROPERTY_ID="ASHMD",
                    CPROPERTY_NAME="Metro park residence",
                    CDATE_BASED_ON_DISPLAY="Cut Off",
                    CFROM_SUPPLIER_ID="c099",
                    CTO_SUPPLIER_ID="c099",
                    CFROM_JRNGRP_CODE="j001",
                    CTO_JRNGRP_CODE="j001",
                    CREMAINING_BASED_ON="1",
                    CREMAINING_BASED_ON_DISPLAY = "Cut Off Remaining",
                    CCUT_OFF="20250101",
                    CPERIOD="20250101",
                    CREPORT_TYPE="Detail",
                    CSORT_BY="Supplier",
                    CCURRENCY_TYPE_CODE="Transcaction Currency",
                    CFROM_DEPT_CODE="ACC",
                    CTO_DEPT_CODE="ACC",
                    LALLOCATION = true,
                    LTRANSACTION_TYPE = lTransactionType,
                    LSUPPLIER__CATEGORY = lCustCategory,
                    CTRANSACTION_TYPE_CODE = !lTransactionType ? "Transaction Type" : "USD",
                    CSUPPLIER_CATEGORY_CODE= !lCustCategory ? "Transaction Type" : "Main Supplier",
                    CLANG_ID="en",
                    CREPORT_CULTURE="",
                    CUSER_ID="RYC",
                    LTRANSACTION_CURRENCY = true,
                    LBASE_CURRENCY = true,
                    LLOCAL_CURRENCY = true
                },
               
            };
            
            int Data1 = 4; // Number of suppliers
            int Data2 = 2; // Number of departments per supplier
            int Data3 = 3; // Number of currencies per department (3 distinct)

            List<SummaryDataDTO> loCollection = new List<SummaryDataDTO>();

            for (int a = 1; a <= Data1; a++)
            {
                for (int b = 1; b <= Data2; b++)
                {
                    // Loop for exactly 3 currencies (USD, IDR, SGD)
                    string[] currencies = new string[] { "USD", "IDR", "SGD" };
                    foreach (string currency in currencies)
                    {
                        // Use the same amount for all currencies
                        decimal beginningApplyAmount = 100000.00M;
                        decimal localBeginningApplyAmount = 200000.00M;
                        decimal baseBeginningApplyAmount = 300000.00M;

                        decimal totalRemainingAmount = 50000.00M;
                        decimal localTotalRemainingAmount = 70000.00M;
                        decimal baseTotalRemainingAmount = 60000.00M;

                        decimal taxableAmount = 10000.00M;
                        decimal localTaxableAmount = 13000.00M;
                        decimal baseTaxableAmount = 14000.00M;

                        decimal gainLossAmount = 5500.00M;
                        decimal localGainLossAmount = 5600.00M;
                        decimal baseGainLossAmount = 5020.00M;

                        decimal cashBankAmount = 30200.00M;
                        decimal localCashBankAmount = 31000.00M;
                        decimal baseCashBankAmount = 30300.00M;

                        loCollection.Add(new SummaryDataDTO()
                        {
                            CSUPPLIER_ID = $"SUPP-0{a}",
                            CSUPPLIER_NAME = $"Supplier Name-{a}",
                            CDEPT_CODE = $"ACC{b:D2}",
                            CDEPT_NAME = $"ACCNAME{b:D2}",
                            CREF_DATE = $"2024-05-{b:D2}",
                            DREF_DATE = DateTime.ParseExact($"2024-05-{b:D2}", "yyyy-MM-dd", CultureInfo.InvariantCulture),
                            CTRANS_CODE = $"110010",
                            CTRANS_NAME = $"Purchase Invoice",
                            CREF_NO = $"INV-001-00{b}",
                            // Currency codes
                            CCURRENCY_CODE = "IDR",
                            CLOCAL_CURRENCY_CODE = "SGD", // local currency as the same as CCURRENCY_CODE for simplicity
                            CBASE_CURRENCY_CODE = "USD",  // base currency as the same as CCURRENCY_CODE for simplicity

                            // Beginning amounts (same for all currencies)
                            NBEGINNING_APPLY_AMOUNT = beginningApplyAmount,
                            NLBEGINNING_APPLY_AMOUNT = localBeginningApplyAmount,
                            NBBEGINNING_APPLY_AMOUNT = baseBeginningApplyAmount,

                            // Remaining amounts (same for all currencies)
                            NTOTAL_REMAINING = totalRemainingAmount,
                            NLTOTAL_REMAINING = localTotalRemainingAmount,
                            NBTOTAL_REMAINING = baseTotalRemainingAmount,

                            // Tax amounts (same for all currencies)
                            NTAXABLE_AMOUNT = taxableAmount,
                            NLTAXABLE_AMOUNT = localTaxableAmount,
                            NBTAXABLE_AMOUNT = baseTaxableAmount,

                            // Gain/Loss amounts (same for all currencies)
                            NGAIN_LOSS_AMOUNT = gainLossAmount,
                            NLGAIN_LOSS_AMOUNT = localGainLossAmount,
                            NBGAIN_LOSS_AMOUNT = baseGainLossAmount,

                            // Cash/Bank amounts (same for all currencies)
                            NCASH_BANK_AMOUNT = cashBankAmount,
                            NLCASH_BANK_AMOUNT = localCashBankAmount,
                            NBCASH_BANK_AMOUNT = baseCashBankAmount,
                        });
                    }
                }
            }
            

            var loTempData = loCollection
            .GroupBy(data1a => new
            {
                data1a.CSUPPLIER_ID,
                data1a.CSUPPLIER_NAME,
            }).Select(data1b => new APR00100DataResultDTO()
            {
                CSUPPLIER_ID = data1b.Key.CSUPPLIER_ID,
                CSUPPLIER_NAME = data1b.Key.CSUPPLIER_NAME,
                Detail1 = data1b.GroupBy(data2a => new
                {
                   data2a.CREF_DATE,
                   data2a.DREF_DATE,
                   data2a.CDEPT_CODE,
                   data2a.CDEPT_NAME,
                   data2a.CREF_PRD,
                   data2a.CREF_NO,
                   data2a.CTRANS_CODE,
                   data2a.CTRANS_NAME,
                }).Select(data2b => new APR00100SummaryBySupp1DTO()
                {
                    CREF_DATE = data2b.Key.CREF_DATE,
                    DREF_DATE = data2b.Key.DREF_DATE,
                    CDEPT_CODE = data2b.Key.CDEPT_CODE,
                    CDEPT_NAME = data2b.Key.CDEPT_NAME,
                    CREF_PRD = data2b.Key.CREF_PRD,
                    CREF_NO = data2b.Key.CREF_NO,
                    CTRANS_CODE = data2b.Key.CTRANS_CODE,
                    CTRANS_NAME = data2b.Key.CTRANS_NAME,
                    Detail2 = data2b.GroupBy(data2b => new
                    {
                        data2b.CCURRENCY_CODE,
                        data2b.CBASE_CURRENCY_CODE,
                        data2b.CLOCAL_CURRENCY_CODE,
                        data2b.NBEGINNING_APPLY_AMOUNT,
                        data2b.NLBEGINNING_APPLY_AMOUNT,
                        data2b.NBBEGINNING_APPLY_AMOUNT,
                        data2b.NTOTAL_REMAINING,
                        data2b.NLTOTAL_REMAINING,
                        data2b.NBTOTAL_REMAINING,
                        data2b.NTAXABLE_AMOUNT,
                        data2b.NBTAXABLE_AMOUNT,
                        data2b.NLTAXABLE_AMOUNT,
                        data2b.NGAIN_LOSS_AMOUNT,
                        data2b.NLGAIN_LOSS_AMOUNT,
                        data2b.NBGAIN_LOSS_AMOUNT,
                        data2b.NCASH_BANK_AMOUNT,
                        data2b.NLCASH_BANK_AMOUNT,
                        data2b.NBCASH_BANK_AMOUNT,
                        
                    }).Select(data3b => new APR00100SummaryBySupp2DTO()
                    {
                        CCURRENCY_CODE = data3b.Key.CCURRENCY_CODE,
                        CBASE_CURRENCY_CODE = data3b.Key.CBASE_CURRENCY_CODE,
                        CLOCAL_CURRENCY_CODE = data3b.Key.CLOCAL_CURRENCY_CODE,
                        NBEGINNING_APPLY_AMOUNT = data3b.Key.NBEGINNING_APPLY_AMOUNT,
                        NLBEGINNING_APPLY_AMOUNT = data3b.Key.NLBEGINNING_APPLY_AMOUNT,
                        NBBEGINNING_APPLY_AMOUNT = data3b.Key.NBBEGINNING_APPLY_AMOUNT,
                        NTOTAL_REMAINING = data3b.Key.NTOTAL_REMAINING,
                        NLTOTAL_REMAINING = data3b.Key.NLTOTAL_REMAINING,
                        NBTOTAL_REMAINING = data3b.Key.NBTOTAL_REMAINING,
                        NTAXABLE_AMOUNT = data3b.Key.NTAXABLE_AMOUNT,
                        NBTAXABLE_AMOUNT = data3b.Key.NBTAXABLE_AMOUNT,
                        NLTAXABLE_AMOUNT = data3b.Key.NLTAXABLE_AMOUNT,
                        NGAIN_LOSS_AMOUNT = data3b.Key.NGAIN_LOSS_AMOUNT,
                        NLGAIN_LOSS_AMOUNT = data3b.Key.NLGAIN_LOSS_AMOUNT,
                        NBGAIN_LOSS_AMOUNT = data3b.Key.NBGAIN_LOSS_AMOUNT,
                        NCASH_BANK_AMOUNT = data3b.Key.NCASH_BANK_AMOUNT,
                        NLCASH_BANK_AMOUNT = data3b.Key.NLCASH_BANK_AMOUNT,
                        NBCASH_BANK_AMOUNT = data3b.Key.NBCASH_BANK_AMOUNT,
                    }).ToList()
                }).ToList()
            }).ToList();
            
            // Grouping by CCURRENCY_CODE and calculating the subtotal for each group
            /*var loTempDataTotal = loCollection
                .GroupBy(data => new 
                { 
                    data.CCURRENCY_CODE, 
                    data.CBASE_CURRENCY_CODE, 
                    data.CLOCAL_CURRENCY_CODE 
                })
                .Select(group => new ReportTotalDTO()
                {
                    // Grouped by currency code
                    // Grouped by currency code, base currency code, and local currency code
                    CCURRENCY_CODE = group.Key.CCURRENCY_CODE,
                    CBASE_CURRENCY_CODE = group.Key.CBASE_CURRENCY_CODE,
                    CLOCAL_CURRENCY_CODE = group.Key.CLOCAL_CURRENCY_CODE,

                    // Calculate the sum for each group
                    NBEGINNING_APPLY_AMOUNT = group.Sum(data => data.NBEGINNING_APPLY_AMOUNT),
                    NLBEGINNING_APPLY_AMOUNT = group.Sum(data => data.NLBEGINNING_APPLY_AMOUNT),
                    NBBEGINNING_APPLY_AMOUNT = group.Sum(data => data.NBBEGINNING_APPLY_AMOUNT),

                    NTOTAL_REMAINING = group.Sum(data => data.NTOTAL_REMAINING),
                    NLTOTAL_REMAINING = group.Sum(data => data.NLTOTAL_REMAINING),
                    NBTOTAL_REMAINING = group.Sum(data => data.NBTOTAL_REMAINING),

                    NTAXABLE_AMOUNT = group.Sum(data => data.NTAXABLE_AMOUNT),
                    NLTAXABLE_AMOUNT = group.Sum(data => data.NLTAXABLE_AMOUNT),
                    NBTAXABLE_AMOUNT = group.Sum(data => data.NBTAXABLE_AMOUNT),

                    NGAIN_LOSS_AMOUNT = group.Sum(data => data.NGAIN_LOSS_AMOUNT),
                    NLGAIN_LOSS_AMOUNT = group.Sum(data => data.NLGAIN_LOSS_AMOUNT),
                    NBGAIN_LOSS_AMOUNT = group.Sum(data => data.NBGAIN_LOSS_AMOUNT),

                    NCASH_BANK_AMOUNT = group.Sum(data => data.NCASH_BANK_AMOUNT),
                    NLCASH_BANK_AMOUNT = group.Sum(data => data.NLCASH_BANK_AMOUNT),
                    NBCASH_BANK_AMOUNT = group.Sum(data => data.NBCASH_BANK_AMOUNT),

                    // Additional fields to identify this row as a subtotal row
                })
                .ToList();*/
            
            /*
            loRtn.ReportData.Total = loTempDataTotal;
            */
            loRtn.ReportData.Data = loTempData;
            return loRtn;
        }
    }
}
