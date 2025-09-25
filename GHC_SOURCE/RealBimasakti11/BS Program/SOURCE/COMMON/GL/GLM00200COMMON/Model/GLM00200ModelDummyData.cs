using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using GLM00200COMMON;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GLM00200COMMON.Models
{
    public static class GenerateDataModelGLR00200
    {
        public static GLM00200ResultPrintDTO DefaultData()
        {
            GLM00200ResultPrintDTO loData = new GLM00200ResultPrintDTO() { LabelPrint = new JournalLabelPrint() };

            var loHeaderData = new JournalDTO
            {
                CUSER_ID = "user1",
                CJRN_ID = "jrn1",
                CREC_ID = "rec1",
                CACTION = "action1",
                CCOMPANY_ID = "company1",
                CDEPT_CODE = "dept1",
                CDEPT_NAME = "department1",
                CTRANS_CODE = "trans1",
                CREF_NO = "ref1",
                CREF_DATE = "2024-03-21",
                DREF_DATE = DateTime.Parse("2024-03-21"),
                CDOC_NO = "doc1",
                CDOC_DATE = "2024-03-21",
                DDOC_DATE = DateTime.Parse("2024-03-21"),
                IFREQUENCY = 1,
                IAPPLIED = 2,
                IPERIOD = 3,
                CSTATUS = "status1",
                CSTART_DATE = "2024-03-21",
                DSTART_DATE = DateTime.Parse("2024-03-21"),
                CNEXT_DATE = "2024-03-22",
                DNEXT_DATE = DateTime.Parse("2024-03-22"),
                CLAST_DATE = "2024-03-20",
                DLAST_DATE = DateTime.Parse("2024-03-20"),
                CTRANS_DESC = "description1",
                CCURRENCY_CODE = "currency1",
                LFIX_RATE = true,
                CFIX_RATE = "fix1",
                NLBASE_RATE = 1.5m,
                NLCURRENCY_RATE = 2.5m,
                NBBASE_RATE = 3.5m,
                NBCURRENCY_RATE = 4.5m,
                NPRELIST_AMOUNT = 100.5m,
                NNTRANS_AMOUNT_C = 200.5m,
                NNTRANS_AMOUNT_D = 300.5m,
                CUPDATE_BY = "update1",
                DUPDATE_DATE = DateTime.Now,
                CCREATE_BY = "create1",
                DCREATE_DATE = DateTime.Now,
                CLANGUAGE_ID = "language1",
                LALLOW_APPROVE = true,
                CNEXT_PRD = "next1",
                NTRANS_AMOUNT = 400.5m,
                CSTATUS_NAME = "statusname1"
            };

            var loDetailData = new List<JournalDetailActualGridDTO>
                {
                    new JournalDetailActualGridDTO
                    {
                        CREF_NO = "ref1",
                        CDOC_SEQ_NO = "seq1",
                        CREF_PRD = "prd1",
                        CREF_DATE = "2024-03-21",
                        DREF_DATE = DateTime.Parse("2024-03-21"),
                        //NTRANS_AMOUNT = "100.5",
                        //NLTRANS_AMOUNT = "200.5",
                        //NBTRANS_AMOUNT = "300.5",
                        CCREATE_BY = "create1",
                        DCREATE_DATE = DateTime.Now
                    },
                    new JournalDetailActualGridDTO
                    {
                        CREF_NO = "ref2",
                        CDOC_SEQ_NO = "seq2",
                        CREF_PRD = "prd2",
                        CREF_DATE = "2024-03-22",
                        DREF_DATE = DateTime.Parse("2024-03-22"),
                        //NTRANS_AMOUNT = "200.5",
                        //NLTRANS_AMOUNT = "300.5",
                        //NBTRANS_AMOUNT = "400.5",
                        CCREATE_BY = "create2",
                        DCREATE_DATE = DateTime.Now
                    }
                };

            loData.HeaderData = loHeaderData;
            loData.DetailData = loDetailData;

            return loData;
        }

        public static GLM00200ResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "GL ACCOUNT LEDGER",
                CUSER_ID = "GHC",
            };

            GLM00200ResultWithBaseHeaderPrintDTO loRtn = new GLM00200ResultWithBaseHeaderPrintDTO();

            loRtn.BaseHeaderData = loParam;
            loRtn.Data = DefaultData();

            return loRtn;
        }
    }

}
