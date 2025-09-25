using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PMF00200ReportCommon
{
    public static class GenerateDataModel
    {
        public static PMF00200ResultPrintDTO DefaultData()
        {
            PMF00200ResultPrintDTO loData = new PMF00200ResultPrintDTO()
            {
                Header = new PMF00200HeaderPrintDTO
                {
                    CREC_ID = "REC001",
                    CCOMPANY_ID = "COMP001",
                    CPROPERTY_ID = "PROP001",
                    CDEPT_CODE = "DEPT001",
                    INO = 1,
                    CDEPT_NAME = "Finance",
                    CTRANS_CODE = "T001",
                    CREF_NO = "REF001",
                    CREF_DATE = "2023-10-10",
                    DREF_DATE_DISPLAY = DateTime.Now,
                    CDOC_NO = "DOC001",
                    CDOC_DATE = "2023-10-10",
                    DDOC_DATE_DISPLAY = DateTime.Now,
                    CCHEQUE_NO = "CHQ001",
                    CCHEQUE_DATE = "2023-10-10",
                    DCHEQUE_DATE_DISPLAY = DateTime.Now,
                    CREF_PRD = "202310",
                    CTRANS_DESC = "Transaction Description",
                    CCURRENCY_CODE = "USD",
                    CAMOUNT_WORDS = "One Hundred Dollars",
                    NTRANS_AMOUNT = 100.00m,
                    NLBASE_RATE = 1.0m,
                    NLTRANS_AMOUNT = 100.00m,
                    NBTRANS_AMOUNT = 90.00m,
                    NBANK_CHARGES = 10.00m,
                    NLBANK_CHARGES = 9.00m,
                    NBBANK_CHARGES = 1.00m,
                    CSTATUS = "Active",
                    CSTATUS_NAME = "Active Status",
                    CCREATE_BY = "Admin",
                    DCREATE_DATE = DateTime.Now,
                    CUPDATE_BY = "User1",
                    DUPDATE_DATE = DateTime.Now,
                    CCB_CODE = "CB001",
                    CCB_NAME = "Central Bank",
                    CCB_ACCOUNT_NO = "AC001",
                    CCB_ACCOUNT_NAME = "Account Name",
                    CUNIT_DESCRIPTION = "Unit Description",
                    CCUST_SUPP_ID = "CUST001",
                    CCUST_SUPP_ID_NAME = "Customer Name",
                    CCUST_SUPP_NAME = "John Doe",
                    CEMAIL = "john.doe@example.com",
                    CLOI_AGRMT_ID = "AGR001",
                    CLOI_AGRMT_NO = "AGRMT001",
                    CLOI_DEPT_CODE = "DEPT002",
                    CLOI_DEPT_NAME = "Sales",
                    CCASH_FLOW_GROUP_CODE = "CFG001",
                    CCASH_FLOW_CODE = "CF001",
                    CCASH_FLOW_NAME = "Cash Flow Name",
                    CCUSTOMER_TYPE = "Individual",
                    CCUSTOMER_TYPE_NAME = "Individual Type",
                    NAR_REMAINING = 50.00m,
                    NLAR_REMAINING = 45.00m,
                    NBAR_REMAINING = 5.00m,
                    NTAX_REMAINING = 15.00m,
                    NLTAX_REMAINING = 14.00m,
                    NBTAX_REMAINING = 1.00m,
                    NTOTAL_REMAINING = 100.00m,
                    NLTOTAL_REMAINING = 90.00m,
                    NBTOTAL_REMAINING = 10.00m,
                    NLCURRENCY_RATE = 1.1m,
                    NTAX_BASE_RATE = 1.2m,
                    NTAX_RATE = 0.1m,
                    NBBASE_RATE = 1.05m,
                    NBCURRENCY_RATE = 1.05m
                },
                AllocList = new List<PMF00201DTO>(),
                JournalList = new List<PMF00202DTO>()
            };
            for (int i = 1; i <= 5; i++)
            {
                loData.AllocList.Add(new PMF00201DTO
                {
                    INO = i,
                    CALLOC_DEPT_CODE = $"ALLOC_DEPT_{i}",
                    DALLOC_DATE = DateTime.Now,
                    CALLOC_NO = $"ALLOC_NO_{i}",
                    CALLOC_DATE = DateTime.Now.ToString("yyyy-MM-dd"),
                    CCURRENCY_CODE = "USD",
                    CINVOICE_NO = $"INV{i:000}",
                    CINVOICE_DESC = $"Invoice Description {i}",
                    NTRANS_AMOUNT = i * 100m
                });
            }

            // Populate JournalList with 5 items
            for (int i = 1; i <= 5; i++)
            {
                loData.JournalList.Add(new PMF00202DTO
                {
                    CREF_NO = $"JREF_{i}",
                    CREF_DATE = DateTime.Now.ToString("yyyy-MM-dd"),
                    DREF_DATE = DateTime.Now,
                    CDBCR = (i % 2 == 0) ? "CR" : "DB",
                    NDEBIT_AMOUNT = (i % 2 == 0) ? 0 : i * 50.00m,
                    NCREDIT_AMOUNT = (i % 2 == 0) ? i * 50.00m : 0,
                    CGLACCOUNT_NO = $"GL{i:000}",
                    CGLACCOUNT_NAME = $"GL Account {i}",
                    CCENTER_CODE = $"Center_{i}"
                });
            }



            return loData;
        }

        public static PMF00200ResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "001",
                CPRINT_NAME = "Utility Charges",
                CUSER_ID = "FMC",
            };

            PMF00200ResultWithBaseHeaderPrintDTO loRtn = new PMF00200ResultWithBaseHeaderPrintDTO { Column = new PMF00200ColumnPrintDTO() };
            loRtn.BaseHeaderData = loParam;
            loRtn.Data = GenerateDataModel.DefaultData();

            return loRtn;
        }
    }

}
