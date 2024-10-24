using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using APR00300Common.DTOs.Print;
using BaseHeaderReportCOMMON;

namespace APR00300Common.Model
{
    public class APR00300ModelReportDummyData
    {
        public static APR00300ReportResultDTO DefaultData()
        {
            var loCollection = new List<APR00300DataResultDTO>();
            var totalData = 10;
            var SupplierId = new string[]{"SUP1", "SUP2"};
            var SupplierName = new string[]{"Supplier 1", "Supplier 2"};

            //looping sebanyak isi array days_late
            for (var i = 0; i < totalData; i++)
            {
                if (i < 5)
                {
                    if (i < 3)
                    {
                        loCollection.Add(
                            new APR00300DataResultDTO
                            {
                                CSUPPLIER_ID = SupplierId[0],
                                CSUPPLIER_NAME = SupplierName[0],
                                CREF_DATE = "20241011",
                                CREF_PRD = "202410",
                                CREF_NO = "xxxxxxxxxxxxxxxxxxxxxxxxx",
                                CCURRENCY_CODE = "IDR",
                                CTRANS_DESC = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                                NORIGINAL_AMOUNT = 900000000.00m,
                                NOUTSTANDING_AMOUNT = 990000000.01m,
                                NAGE_NOT_DUE_AMOUNT = 999000000.02m,
                                NAGE_CURRENT_AMOUNT = 999900000.03m,
                                NAGE_MORE_THAN_30_DAYS_AMOUNT = 999990000.04m,
                                NAGE_MORE_THAN_60_DAYS_AMOUNT = 999999000.05m,
                                NAGE_MORE_THAN_90_DAYS_AMOUNT = 999999900.06m,
                                NAGE_MORE_THAN_120_DAYS_AMOUNT = 999999990.07m,
                                NAGE_UNALLOCATED_PAYMENT = 999999999.00m,
                            }
                        );
                    }
                    else
                    {
                        loCollection.Add(
                            new APR00300DataResultDTO
                            {
                                CSUPPLIER_ID = SupplierId[0],
                                CSUPPLIER_NAME = SupplierName[0],
                                CREF_DATE = "20241011",
                                CREF_PRD = "202410",
                                CREF_NO = "xxxxxxxxxxxxxxxxxxxxxxxxx",
                                CCURRENCY_CODE = "SGD",
                                CTRANS_DESC = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                                NORIGINAL_AMOUNT = 900000000.00m,
                                NOUTSTANDING_AMOUNT = 990000000.01m,
                                NAGE_NOT_DUE_AMOUNT = 999000000.02m,
                                NAGE_CURRENT_AMOUNT = 999900000.03m,
                                NAGE_MORE_THAN_30_DAYS_AMOUNT = 999990000.04m,
                                NAGE_MORE_THAN_60_DAYS_AMOUNT = 999999000.05m,
                                NAGE_MORE_THAN_90_DAYS_AMOUNT = 999999900.06m,
                                NAGE_MORE_THAN_120_DAYS_AMOUNT = 999999990.07m,
                                NAGE_UNALLOCATED_PAYMENT = 999999999.00m,
                            }
                        );
                    }
                }
                else
                {
                    if (i < 8)
                    {
                        loCollection.Add(
                            new APR00300DataResultDTO
                            {
                                CSUPPLIER_ID = SupplierId[0],
                                CSUPPLIER_NAME = SupplierName[0],
                                CREF_DATE = "20241011",
                                CREF_PRD = "202411",
                                CREF_NO = "xxxxxxxxxxxxxxxxxxxxxxxxx",
                                CCURRENCY_CODE = "IDR",
                                CTRANS_DESC = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                                NORIGINAL_AMOUNT = 900000000.00m,
                                NOUTSTANDING_AMOUNT = 990000000.01m,
                                NAGE_NOT_DUE_AMOUNT = 999000000.02m,
                                NAGE_CURRENT_AMOUNT = 999900000.03m,
                                NAGE_MORE_THAN_30_DAYS_AMOUNT = 999990000.04m,
                                NAGE_MORE_THAN_60_DAYS_AMOUNT = 999999000.05m,
                                NAGE_MORE_THAN_90_DAYS_AMOUNT = 999999900.06m,
                                NAGE_MORE_THAN_120_DAYS_AMOUNT = 999999990.07m,
                                NAGE_UNALLOCATED_PAYMENT = 999999999.00m,
                            }
                        );
                    }
                    else
                    {
                        loCollection.Add(
                            new APR00300DataResultDTO
                            {
                                CSUPPLIER_ID = SupplierId[0],
                                CSUPPLIER_NAME = SupplierName[0],
                                CREF_DATE = "20241011",
                                CREF_PRD = "202411",
                                CREF_NO = "xxxxxxxxxxxxxxxxxxxxxxxxx",
                                CCURRENCY_CODE = "SGD",
                                CTRANS_DESC = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                                NORIGINAL_AMOUNT = 900000000.00m,
                                NOUTSTANDING_AMOUNT = 990000000.01m,
                                NAGE_NOT_DUE_AMOUNT = 999000000.02m,
                                NAGE_CURRENT_AMOUNT = 999900000.03m,
                                NAGE_MORE_THAN_30_DAYS_AMOUNT = 999990000.04m,
                                NAGE_MORE_THAN_60_DAYS_AMOUNT = 999999000.05m,
                                NAGE_MORE_THAN_90_DAYS_AMOUNT = 999999900.06m,
                                NAGE_MORE_THAN_120_DAYS_AMOUNT = 999999990.07m,
                                NAGE_UNALLOCATED_PAYMENT = 999999999.00m,
                            }
                        );
                    }
                }
            }
            
            for (var i = 0; i < totalData; i++)
            {
                if (i < 5)
                {
                    if (i < 3)
                    {
                        loCollection.Add(
                            new APR00300DataResultDTO
                            {
                                CSUPPLIER_ID = SupplierId[1],
                                CSUPPLIER_NAME = SupplierName[1],
                                CREF_DATE = "20241011",
                                CREF_PRD = "202410",
                                CREF_NO = "xxxxxxxxxxxxxxxxxxxxxxxxx",
                                CCURRENCY_CODE = "IDR",
                                CTRANS_DESC = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                                NORIGINAL_AMOUNT = 900000000.00m,
                                NOUTSTANDING_AMOUNT = 990000000.01m,
                                NAGE_NOT_DUE_AMOUNT = 999000000.02m,
                                NAGE_CURRENT_AMOUNT = 999900000.03m,
                                NAGE_MORE_THAN_30_DAYS_AMOUNT = 999990000.04m,
                                NAGE_MORE_THAN_60_DAYS_AMOUNT = 999999000.05m,
                                NAGE_MORE_THAN_90_DAYS_AMOUNT = 999999900.06m,
                                NAGE_MORE_THAN_120_DAYS_AMOUNT = 999999990.07m,
                                NAGE_UNALLOCATED_PAYMENT = 999999999.00m,
                            }
                        );
                    }
                    else
                    {
                        loCollection.Add(
                            new APR00300DataResultDTO
                            {
                                CSUPPLIER_ID = SupplierId[1],
                                CSUPPLIER_NAME = SupplierName[1],
                                CREF_DATE = "20241011",
                                CREF_PRD = "202410",
                                CREF_NO = "xxxxxxxxxxxxxxxxxxxxxxxxx",
                                CCURRENCY_CODE = "SGD",
                                CTRANS_DESC = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                                NORIGINAL_AMOUNT = 900000000.00m,
                                NOUTSTANDING_AMOUNT = 990000000.01m,
                                NAGE_NOT_DUE_AMOUNT = 999000000.02m,
                                NAGE_CURRENT_AMOUNT = 999900000.03m,
                                NAGE_MORE_THAN_30_DAYS_AMOUNT = 999990000.04m,
                                NAGE_MORE_THAN_60_DAYS_AMOUNT = 999999000.05m,
                                NAGE_MORE_THAN_90_DAYS_AMOUNT = 999999900.06m,
                                NAGE_MORE_THAN_120_DAYS_AMOUNT = 999999990.07m,
                                NAGE_UNALLOCATED_PAYMENT = 999999999.00m,
                            }
                        );
                    }
                }
                else
                {
                    if (i < 8)
                    {
                        loCollection.Add(
                            new APR00300DataResultDTO
                            {
                                CSUPPLIER_ID = SupplierId[1],
                                CSUPPLIER_NAME = SupplierName[1],
                                CREF_DATE = "20241011",
                                CREF_PRD = "202411",
                                CREF_NO = "xxxxxxxxxxxxxxxxxxxxxxxxx",
                                CCURRENCY_CODE = "IDR",
                                CTRANS_DESC = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                                NORIGINAL_AMOUNT = 900000000.00m,
                                NOUTSTANDING_AMOUNT = 990000000.01m,
                                NAGE_NOT_DUE_AMOUNT = 999000000.02m,
                                NAGE_CURRENT_AMOUNT = 999900000.03m,
                                NAGE_MORE_THAN_30_DAYS_AMOUNT = 999990000.04m,
                                NAGE_MORE_THAN_60_DAYS_AMOUNT = 999999000.05m,
                                NAGE_MORE_THAN_90_DAYS_AMOUNT = 999999900.06m,
                                NAGE_MORE_THAN_120_DAYS_AMOUNT = 999999990.07m,
                                NAGE_UNALLOCATED_PAYMENT = 999999999.00m,
                            }
                        );
                    }
                    else
                    {
                        loCollection.Add(
                            new APR00300DataResultDTO
                            {
                                CSUPPLIER_ID = SupplierId[1],
                                CSUPPLIER_NAME = SupplierName[1],
                                CREF_DATE = "20241011",
                                CREF_PRD = "202411",
                                CREF_NO = "xxxxxxxxxxxxxxxxxxxxxxxxx",
                                CCURRENCY_CODE = "SGD",
                                CTRANS_DESC = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                                NORIGINAL_AMOUNT = 900000000.00m,
                                NOUTSTANDING_AMOUNT = 990000000.01m,
                                NAGE_NOT_DUE_AMOUNT = 999000000.02m,
                                NAGE_CURRENT_AMOUNT = 999900000.03m,
                                NAGE_MORE_THAN_30_DAYS_AMOUNT = 999990000.04m,
                                NAGE_MORE_THAN_60_DAYS_AMOUNT = 999999000.05m,
                                NAGE_MORE_THAN_90_DAYS_AMOUNT = 999999900.06m,
                                NAGE_MORE_THAN_120_DAYS_AMOUNT = 999999990.07m,
                                NAGE_UNALLOCATED_PAYMENT = 999999999.00m,
                            }
                        );
                    }
                }
            }
            
            // grouping data berdasarkan ke APR00300ReportDataDTO
            var loResult = new List<APR00300ReportDataDTO>();
            var loGroupSupplier = loCollection.GroupBy(x => x.CSUPPLIER_ID).ToList();
            foreach (var item in loGroupSupplier)
            {
                //untuk list period
                var loGroupPeriod = item.GroupBy(x => x.CREF_PRD).ToList();
                var loGroup = new APR00300ReportDataDTO
                {
                    CSUPPLIER_ID = item.Key,
                    CSUPPLIER_NAME = item.First().CSUPPLIER_NAME,
                    PERIODS = new List<APR00300GroupPeriodDTO>(),
                    NGRAND_TOTAL = item.Sum(x => x.NOUTSTANDING_AMOUNT)
                };
                
                foreach (var item2 in loGroupPeriod)
                {
                    var loGroupCurrencyPeriod = item2.GroupBy(x => x.CCURRENCY_CODE).ToList();
                    var loPeriod = new APR00300GroupPeriodDTO
                    {
                        CREF_PRD = item2.Key,
                        INVOICES = new List<APR00300DataResultDTO>(),
                        CURRENCIES = new List<APR00300GroupCurrencyDTO>(),
                        NSUB_TOTAL = item2.Sum(x => x.NOUTSTANDING_AMOUNT)
                    };
                    foreach (var item3 in loGroupCurrencyPeriod)
                    {
                        var loCurrency = new APR00300GroupCurrencyDTO
                        {
                            CCURRENCY_CODE = item3.Key,
                            NSUB_TOTAL = item3.Sum(x => x.NOUTSTANDING_AMOUNT)
                        };
                        loPeriod.CURRENCIES.Add(loCurrency);
                        loPeriod.INVOICES.AddRange(item3);
                    }
                    loGroup.PERIODS.Add(loPeriod);
                }
                
                // untuk list invoice
                loGroup.INVOICES = item.ToList();
                // untuk list currency
                var loGroupCurrency = item.GroupBy(x => x.CCURRENCY_CODE).ToList();
                loGroup.CURRENCIES = new List<APR00300GroupCurrencyDTO>();
                foreach (var item2 in loGroupCurrency)
                {
                    var loCurrency = new APR00300GroupCurrencyDTO
                    {
                        CCURRENCY_CODE = item2.Key,
                        NSUB_TOTAL = item2.Sum(x => x.NOUTSTANDING_AMOUNT)
                    };
                    loGroup.CURRENCIES.Add(loCurrency);
                }
                
                loResult.Add(loGroup);
            }
            
            var loData = new APR00300ReportResultDTO
            {
                Title = "Supplier Statement",
                Label = new APR00300ReportLabelDTO(),
                Header = new APR00300ReportHeaderDTO
                {
                    CPROPERTY_ID = "ASHMD",
                    CPROPERTY_NAME = "Harco Mas",
                    DSTATEMENT_DATE = DateTime.Today,
                    DCUT_OFF_DATE = DateTime.Today,
                    CFROM_PERIOD = "202410",
                    CTO_PERIOD = "202411",
                    CDATE_BASED_ON = "P", // C/P
                    LINCLUDE_ZERO_BALANCE = true,
                    // LSHOW_AGE_TOTAL = false
                    LSHOW_AGE_TOTAL = true
                },
                Data = loResult
            };
            
            foreach (var item in loData.Data)
            {
                item.PERIODS.ForEach(x => x.INVOICES.ForEach(y =>
                {
                    y.DREF_DATE = DateTime.TryParseExact(y.CREF_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var refDate) ? refDate : (DateTime?)null;
                }));
                
                item.INVOICES.ForEach(x =>
                {
                    x.DREF_DATE = DateTime.TryParseExact(x.CREF_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var refDate) ? refDate : (DateTime?)null;
                });
            }
            
            return loData;
        }
        
        public static APR00300ReportWithBaseHeaderDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO
            {
                CCOMPANY_NAME = "PT. Realta Chakradarma",
                CPRINT_CODE = "001",
                CPRINT_NAME = "Supplier Statement",
                CUSER_ID = "RHC"
            };

            var loData = new APR00300ReportWithBaseHeaderDTO
            {
                BaseHeaderData = loParam,
                Data = DefaultData()
            };

            return loData;
        }
    }
}