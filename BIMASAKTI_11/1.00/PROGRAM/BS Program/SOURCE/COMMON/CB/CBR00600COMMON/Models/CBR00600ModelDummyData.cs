using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CBR00600COMMON.Models
{
    public static class GenerateDataModelCBR00600
    {
        public static CBR00600PrintDataDTO DefaultData()
        {
            CBR00600PrintDataDTO loData = new CBR00600PrintDataDTO() 
            {
                OfficialReceipt = new List<CBR00600OfficialReceiptHeaderDTO>(),
                Allocation = new List<CBR00600AllocationHeaderDTO>(),
                Journal = new List<CBR00600JournalHeaderDTO>(), 
            };

            var listSP = new List<CBR00600SPResultDTO>();
            for (int h = 1; h <= 2; h++) // 2 headers
            {
                for (int d = 1; d <= 3; d++) // 3 details per header
                {
                    listSP.Add(new CBR00600SPResultDTO
                    {
                        // RECEIPT (Header-like)
                        CREF_NO = $"REF-{h:000}",
                        CREF_DATE = DateTime.Now.AddDays(-h).ToString("yyyyMMdd"),
                        DREF_DATE = DateTime.Now.AddDays(-h),
                        CCUSTOMER_ID = $"CUST-{h:000}",
                        CUSTOMER_NAME = $"Customer {h}",
                        CCUSTOMER_ID_NAME = $"CUST-{h:000} - Customer {h}",
                        CCURRENCY_CODE = "USD",
                        NTRANS_AMOUNT = 1000m * h,
                        CTRANS_AMOUNT = (1000m * h).ToString("N2"),
                        CTRANS_DESC = $"Transaction {h}",
                        CMESSAGE_DESC = $"Message {h}",
                        CMESSAGE_DESC_RTF = $"{{\\rtf1\\ansi Message {h} RTF}}",
                        CADDITIONAL_INFO = $"Additional info {h}",
                        CADDITIONAL_INFO_RTF = $"{{\\rtf1\\ansi Additional info {h} RTF}}",

                        // ALLOCATION
                        INO = d,
                        CALLOC_DEPT_CODE = $"DEPT-{h}",
                        CALLOC_NO = $"ALLOC-{h}{d:00}",
                        CALLOC_DATE = DateTime.Now.AddDays(-(h + d)).ToString("yyyyMMdd"),
                        DALLOC_DATE = DateTime.Now.AddDays(-(h + d)),
                        CINVOICE_NO = $"INV-{h}{d:00}",
                        CINVOICE_DESC = $"Invoice description {h}-{d}",
                        CREPORT_TITLE = $"Report Title {h}",
                        CCB_REF_NO = $"CBREF-{h:000}",
                        CCB_REF_DATE = DateTime.Now.AddDays(-h).ToString("yyyyMMdd"),
                        DCB_REF_DATE = DateTime.Now.AddDays(-h),
                        NCB_AMOUNT = 5000m * h,
                        CCUST_SUPP_ID = $"SUPP-{h:000}",
                        CCUST_SUPP_ID_NAME = $"Supplier {h}",
                        CCB_CODE = $"CODE-{h}",
                        CCB_NAME = $"Cashbook {h}",
                        CCB_ACCOUNT_NO = $"ACC-{h}001",
                        CCB_DEPT_CODE = $"CBDEPT-{h}",
                        CCB_DEPT_NAME = $"Cashbook Dept {h}",
                        CCB_DOC_NO = $"DOC-{h}001",
                        CCB_DOC_DATE = DateTime.Now.AddDays(-h * 2).ToString("yyyyMMdd"),
                        DCB_DOC_DATE = DateTime.Now.AddDays(-h * 2),
                        CCB_CHEQUE_NO = $"CHQ-{h}001",
                        CCB_CHEQUE_DATE = DateTime.Now.AddDays(h).ToString("yyyyMMdd"),
                        DCB_CHEQUE_DATE = DateTime.Now.AddDays(h),
                        CCB_CURRENCY_CODE = "USD",
                        CCB_AMOUNT_WORDS = $"{5000m * h} Dollars",
                        NCBLBASE_RATE = 1.0m,
                        NCBLCURRENCY_RATE = 15000.0m,
                        NCBBBASE_RATE = 1.0m,
                        NCBBCURRENCY_RATE = 15000.0m,
                        CCB_PAYMENT_TYPE = "Bank Transfer",
                        CCB_TRANS_DESC = $"Cashbook Transaction {h}",
                        CCOMPANY_BASE_CURRENCY_CODE = "IDR",
                        CCOMPANY_LOCAL_CURRENCY_CODE = "SGD", 

                        // JOURNAL
                        NDEBIT_AMOUNT = 200m * d,
                        NCREDIT_AMOUNT = 200m * d,
                        CGLACCOUNT_NO = $"GL-{h}{d:00}",
                        CGLACCOUNT_NAME = $"GL Account {h}-{d}",
                        CCENTER_CODE = $"CENTER-{h}"
                    });
                }
            }

            for (int i = 1; i <= 2; i++)
            {
                loData.OfficialReceipt.Add(new CBR00600OfficialReceiptHeaderDTO
                {
                    CREF_NO = $"REF-{i:000}",
                    CREF_DATE = DateTime.Now.AddDays(-i).ToString("yyyyMMdd"),
                    CCUSTOMER_ID = $"CUST-{i:000}",
                    CUSTOMER_NAME = $"Customer {i}",
                    CCUSTOMER_ID_NAME = $"CUST-{i:000} - Customer {i}",
                    CCURRENCY_CODE = "USD",
                    NTRANS_AMOUNT = 100m * i,
                    CTRANS_AMOUNT = (100m * i).ToString("N2"),
                    CTRANS_DESC = $"Transaction {i} description",
                    CMESSAGE_DESC = $"Message {i}",
                    CMESSAGE_DESC_RTF = $"{{\\rtf1\\ansi Message {i} RTF}}",
                    CADDITIONAL_INFO = $"Additional info {i}",
                    CADDITIONAL_INFO_RTF = $"{{\\rtf1\\ansi Additional info {i} RTF}}"
                });
            }

            // LINQ grouping
            var loGroupingAllocation = listSP
                .GroupBy(x => x.CCB_REF_NO) // Group by header key
                .Select(g => new CBR00600AllocationHeaderDTO
                {
                    // HEADER FIELDS
                    CREPORT_TITLE = g.First().CREPORT_TITLE,
                    CCB_REF_NO = g.Key,
                    CCB_REF_DATE = g.First().CCB_REF_DATE,
                    NCB_AMOUNT = g.First().NCB_AMOUNT,
                    CCUST_SUPP_ID = g.First().CCUST_SUPP_ID,
                    CCUST_SUPP_ID_NAME = g.First().CCUST_SUPP_ID_NAME,
                    CCB_CODE = g.First().CCB_CODE,
                    CCB_NAME = g.First().CCB_NAME,
                    CCB_ACCOUNT_NO = g.First().CCB_ACCOUNT_NO,
                    CCB_DEPT_CODE = g.First().CCB_DEPT_CODE,
                    CCB_DEPT_NAME = g.First().CCB_DEPT_NAME,
                    CCB_DOC_NO = g.First().CCB_DOC_NO,
                    CCB_DOC_DATE = g.First().CCB_DOC_DATE,
                    CCB_CHEQUE_NO = g.First().CCB_CHEQUE_NO,
                    CCB_CHEQUE_DATE = g.First().CCB_CHEQUE_DATE,
                    CCB_CURRENCY_CODE = g.First().CCB_CURRENCY_CODE,
                    CCB_AMOUNT_WORDS = g.First().CCB_AMOUNT_WORDS,
                    NCBLBASE_RATE = g.First().NCBLBASE_RATE,
                    NCBLCURRENCY_RATE = g.First().NCBLCURRENCY_RATE,
                    NCBBBASE_RATE = g.First().NCBBBASE_RATE,
                    NCBBCURRENCY_RATE = g.First().NCBBCURRENCY_RATE,
                    CCB_PAYMENT_TYPE = g.First().CCB_PAYMENT_TYPE,
                    CCB_TRANS_DESC = g.First().CCB_TRANS_DESC,
                    DCB_REF_DATE = DateTime.TryParseExact(g.First().CCB_REF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldCbRefDate) ? (DateTime?)ldCbRefDate : null,
                    DCB_CHEQUE_DATE = DateTime.TryParseExact(g.First().CCB_CHEQUE_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldCbChequeDate) ? (DateTime?)ldCbChequeDate : null,
                    DCB_DOC_DATE = DateTime.TryParseExact(g.First().CCB_DOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldCbDocDate) ? (DateTime?)ldCbDocDate : null,
                    CCOMPANY_BASE_CURRENCY_CODE = g.First().CCOMPANY_BASE_CURRENCY_CODE,
                    CCOMPANY_LOCAL_CURRENCY_CODE = g.First().CCOMPANY_LOCAL_CURRENCY_CODE,

                    // DETAILS
                    DetailAllocation = g.Select(d => new CBR00600AllocationDetailDTO
                    {
                        CCB_REF_NO = d.CCB_REF_NO,
                        INO = d.INO,
                        CALLOC_NO = d.CALLOC_NO,
                        CALLOC_DATE = d.CALLOC_DATE,
                        CCURRENCY_CODE = d.CCURRENCY_CODE,
                        CINVOICE_NO = d.CINVOICE_NO,
                        CINVOICE_DESC = d.CINVOICE_DESC,
                        NTRANS_AMOUNT = d.NTRANS_AMOUNT,
                        DALLOC_DATE = DateTime.TryParseExact(d.CALLOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldAllocDate) ? (DateTime?)ldAllocDate : null,
                    }).ToList()
                })
                .ToList();
            loData.Allocation = loGroupingAllocation;

            // LINQ grouping
            var loGroupingJournal = listSP
                .GroupBy(x => x.CCB_REF_NO) // Group by header key
                .Select(g => new CBR00600JournalHeaderDTO
                {
                    // HEADER FIELDS
                    CCB_REF_NO = g.Key,
                    CCB_REF_DATE = g.First().CCB_REF_DATE,
                    NCB_AMOUNT = g.First().NCB_AMOUNT,
                    CCUST_SUPP_ID = g.First().CCUST_SUPP_ID,
                    CCB_CODE = g.First().CCB_CODE,
                    CCB_NAME = g.First().CCB_NAME,
                    CCB_ACCOUNT_NO = g.First().CCB_ACCOUNT_NO,
                    CCB_DEPT_CODE = g.First().CCB_DEPT_CODE,
                    CCB_DEPT_NAME = g.First().CCB_DEPT_NAME,
                    CCB_DOC_NO = g.First().CCB_DOC_NO,
                    CCB_DOC_DATE = g.First().CCB_DOC_DATE,
                    CCB_CHEQUE_NO = g.First().CCB_CHEQUE_NO,
                    CCB_CHEQUE_DATE = g.First().CCB_CHEQUE_DATE,
                    CCB_CURRENCY_CODE = g.First().CCB_CURRENCY_CODE,
                    CCB_AMOUNT_WORDS = g.First().CCB_AMOUNT_WORDS,
                    NCBLBASE_RATE = g.First().NCBLBASE_RATE,
                    NCBLCURRENCY_RATE = g.First().NCBLCURRENCY_RATE,
                    NCBBBASE_RATE = g.First().NCBBBASE_RATE,
                    NCBBCURRENCY_RATE = g.First().NCBBCURRENCY_RATE,
                    CCB_PAYMENT_TYPE = g.First().CCB_PAYMENT_TYPE,
                    CCB_TRANS_DESC = g.First().CCB_TRANS_DESC,
                    CCOMPANY_BASE_CURRENCY_CODE = g.First().CCOMPANY_BASE_CURRENCY_CODE,
                    CCOMPANY_LOCAL_CURRENCY_CODE = g.First().CCOMPANY_LOCAL_CURRENCY_CODE,
                    DCB_REF_DATE = DateTime.TryParseExact(g.First().CCB_REF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldCbRefDate) ? (DateTime?)ldCbRefDate : null,
                    DCB_CHEQUE_DATE = DateTime.TryParseExact(g.First().CCB_CHEQUE_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldCbChequeDate) ? (DateTime?)ldCbChequeDate : null,
                    DCB_DOC_DATE = DateTime.TryParseExact(g.First().CCB_DOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldCbDocDate) ? (DateTime?)ldCbDocDate : null,

                    // DETAILS
                    DetailJournal = g.Select(d => new CBR00600JournalDetailDTO
                    {
                        CCB_REF_NO = d.CCB_REF_NO,
                        INO = d.INO,
                        CCENTER_CODE = d.CCENTER_CODE,
                        CGLACCOUNT_NAME = d.CGLACCOUNT_NAME,
                        CGLACCOUNT_NO = d.CGLACCOUNT_NO,
                        CREF_DATE = d.CCB_REF_DATE,
                        CREF_NO = d.CREF_NO,
                        NCREDIT_AMOUNT = d.NCREDIT_AMOUNT,
                        NDEBIT_AMOUNT = d.NCREDIT_AMOUNT,
                        DREF_DATE = DateTime.TryParseExact(d.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldARefDate) ? (DateTime?)ldARefDate : null,
                    }).ToList()
                })
                .ToList();
            loData.Journal = loGroupingJournal;

            return loData;
        }

        public static CBR00600ResultPrintDTO DefaultDataWithHeader()
        {
            CBR00600ResultPrintDTO loRtn = new CBR00600ResultPrintDTO();
            CBR00600ColoumnDTO loColoumRtn = new CBR00600ColoumnDTO() 
            { 
                AllocationColoumn = new CBR00600AllocationColumnDTO(),
                BaseHeaderColoumn = new CBR00600BaseHeaderColumnDTO(),
                JournalColoumn = new CBR00600JournalColumnDTO(),
                OfficialReceiptColoumn = new CBR00600OfficialReceiptColumnDTO(),
            };

            loRtn.Column = loColoumRtn;
            //loRtn.Data = DefaultData();
            loRtn.Data = new CBR00600PrintDataDTO();

            return loRtn;
        }
    }

}
