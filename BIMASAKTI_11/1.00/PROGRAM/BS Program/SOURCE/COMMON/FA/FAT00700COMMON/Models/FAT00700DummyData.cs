using BaseHeaderReportCOMMON;
using FAT00700Common.Print;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FAT00700Common.Models
{
    public static class FAT00700DummyData
    {
        public static FAT00700ResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "GL ACCOUNT LEDGER",
                CUSER_ID = "FMC",
            };

            FAT00700ResultWithBaseHeaderPrintDTO loRtn = new FAT00700ResultWithBaseHeaderPrintDTO();
            loRtn.Column = new FAT00700ColumnPrintDTO();
            loRtn.Label = new FAT00700LabelDTO();
            loRtn.BaseHeaderData = loParam;
            loRtn.Data = DefaultData();

            return loRtn;
        }

        public static FAT00700ResultPrintDTO DefaultData()
        {
            FAT00700ResultPrintDTO loData = new FAT00700ResultPrintDTO();
            var loTempResult = new List<FAT00700PrintDataDTO>();

            loTempResult.Add(new FAT00700PrintDataDTO()
            {
                CDEPT_CODE = "FIN",
                CDEPT_NAME = "Finance Department",
                CREFERENCE_NO = "REF-001",
                CTRANS_STATUS_NAME = "Approved",
                CCOMMIT_BY = "JDOE",
                DCOMMIT_DATE = new DateTime(2024, 1, 15),
                CASSET_CODE = "AST-1001",
                CASSET_NAME = "Office Equipment",
                CASSET_LOCATION = "HQ-01",
                CCATEGORY_DESC = "Furniture",
                CSERIAL_NUMBER = "SN-123456",
                NTRANSACTION_AMOUNT = 1250.75m,
                CBASE_CURRENCY_CODE = "USD",
                CALLOC_EXPENSE_CODE = "EXP-450",
                CTRANSACTION_DESCR = "Replacement chair purchase",
                CTRANSACTION_DATE = "2024-01-10",
                CSTART_DATE = "2024-01-10"
            });

            var loHeader = loTempResult.First();

            loData.Title = "Other Charges";
            loData.Header = string.Format("{0} - {1}", loHeader.CDEPT_CODE, loHeader.CDEPT_NAME);
            loData.Data = loTempResult;

            return loData;
        }

    }
}
