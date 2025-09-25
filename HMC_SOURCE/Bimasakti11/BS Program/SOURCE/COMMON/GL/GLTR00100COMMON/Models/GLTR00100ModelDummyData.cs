using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using R_Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GLTR00100COMMON.Models
{
    public static class GenerateDataModelGLTR00100
    {
        public static GLTR00100ResultPrintDTO DefaultData()
        {
            GLTR00100ResultPrintDTO loData = new GLTR00100ResultPrintDTO();

            var loDetailData = new List<GLTR00100PrintDTO>();

            for (int i = 1; i <= 5; i++)
            {
                loDetailData.Add(new GLTR00100PrintDTO
                {
                    CUSER_ID = "User" + i,
                    CLANGUAGE_ID = "Language" + i,
                    CREC_JOURNAL_ID = "Journal" + i,
                    CCOMPANY_ID = "Company" + i,
                    CDEPT_CODE = "DeptCode" + i,
                    CDEPT_NAME = "DeptName" + i,
                    CTRANS_CODE = "TransCode" + i,
                    CREF_NO = "RefNo" + i,
                    CREF_DATE = DateTime.Now.AddDays(-i).ToString("yyyyMMdd"),
                    CREF_PRD = "RefPrd" + i,
                    CDOC_NO = "DocNo" + i,
                    CDOC_DATE = DateTime.Now.AddDays(-i).ToString("yyyyMMdd"),
                    CDOC_SEQ_NO = "DocSeqNo" + i,
                    CREVERSE_DATE = DateTime.Now.AddDays(-i).ToString("yyyyMMdd"),
                    LREVERSE = i % 2 == 0, // Alternate between true and false
                    CTRANS_DESC = "TransDesc" + i,
                    CCURRENCY_CODE = "CurrencyCode" + i,
                    CLOCAL_CURRENCY_CODE = "LocalCurrencyCode" + i,
                    CBASE_CURRENCY_CODE = "BaseCurrencyCode" + i,
                    NLBASE_RATE = i * 1.5m, // Example decimal value
                    NLCURRENCY_RATE = i * 1.25m, // Example decimal value
                    NBBASE_RATE = i * 1.75m, // Example decimal value
                    NBCURRENCY_RATE = i * 1.6m, // Example decimal value
                    NLTRANS_AMOUNT = i * 100.0m, // Example decimal value
                    NBTRANS_AMOUNT = i * 120.0m, // Example decimal value
                    NDEBIT_AMOUNT = i * 50.0m, // Example decimal value
                    NCREDIT_AMOUNT = i * 70.0m, // Example decimal value
                    NLDEBIT_AMOUNT = i * 40.0m, // Example decimal value
                    NLCREDIT_AMOUNT = i * 60.0m, // Example decimal value
                    NBDEBIT_AMOUNT = i * 45.0m, // Example decimal value
                    NBCREDIT_AMOUNT = i * 65.0m, // Example decimal value
                    NPRELIST_AMOUNT = i * 10.0m, // Example decimal value
                    LINTERCO = i % 3 == 0, // Alternate between true and false
                    IINTERCO_TYPE = i % 4, // Example integer value
                    CSOURCE_TRANS_CODE = "SourceTransCode" + i,
                    CSOURCE_REF_NO = "SourceRefNo" + i,
                    LIMPORTED = i % 5 == 0, // Alternate between true and false
                    CSTATUS = "Status" + i,
                    CSTATUS_NAME = "StatusName" + i,
                    CAPPROVE_BY = "ApproveBy" + i,
                    DAPPROVE_DATE = DateTime.Now.AddDays(-i), // Example date value
                    CCOMMIT_BY = "CommitBy" + i,
                    DCOMMIT_DATE = DateTime.Now.AddDays(-i), // Example date value
                    CREC_ID = "RecID" + i,
                    CCREATE_BY = "CreateBy" + i,
                    DCREATE_DATE = DateTime.Now.AddDays(-i), // Example date value
                    CUPDATE_BY = "UpdateBy" + i,
                    DUPDATE_DATE = DateTime.Now.AddDays(-i), // Example date value
                    CTRANSACTION_NAME = "TransactionName" + i,
                    CGLACCOUNT_NO = "GLAccountNo" + i,
                    CGLACCOUNT_NAME = "GLAccountName" + i,
                    CCENTER_CODE = "CenterCode" + i,
                    CCENTER_NAME = "CenterName" + i,
                    CDBCR = i % 2 == 0 ? "C" : "D",
                    NTRANS_AMOUNT = i * 85.0m, // Example decimal value
                    CDETAIL_DESC = "DetailDesc" + i,
                    CDOCUMENT_NO = "DocumentNo" + i,
                    CDOCUMENT_DATE = DateTime.Now.ToString("yyyyMMdd")
                });
            }

            // Set Header Data
            var loHeaderData = R_Utility.R_ConvertObjectToObject<GLTR00100PrintDTO, GLTR00101DTO>(loDetailData.FirstOrDefault());

            //Convert Header Data
            loHeaderData.DDOC_DATE = !string.IsNullOrWhiteSpace(loHeaderData.CDOC_DATE)
                                    ? DateTime.ParseExact(loHeaderData.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture)
                                    : default;
            loHeaderData.DREF_DATE = !string.IsNullOrWhiteSpace(loHeaderData.CREF_DATE)
                                    ? DateTime.ParseExact(loHeaderData.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture)
                                    : default;
            loHeaderData.DREVERSE_DATE = !string.IsNullOrWhiteSpace(loHeaderData.CREVERSE_DATE)
                                    ? DateTime.ParseExact(loHeaderData.CREVERSE_DATE, "yyyyMMdd", CultureInfo.InvariantCulture)
                                    : default;

            // Set Detail Data
            List<GLTR00102DTO> loDetail = new List<GLTR00102DTO>();
            foreach (var item in loDetailData)
            {
                //Convert Detail Data
                var itemDetail = new GLTR00102DTO
                {
                    CGLACCOUNT_NO = item.CGLACCOUNT_NO,
                    CGLACCOUNT_NAME = item.CGLACCOUNT_NAME,
                    CCENTER_CODE = item.CCENTER_CODE,
                    CCENTER_NAME = item.CCENTER_NAME,
                    CDBCR = item.CDBCR,
                    NTRANS_AMOUNT = item.NTRANS_AMOUNT, // Example decimal value
                    CDETAIL_DESC = item.CDETAIL_DESC,
                    CDOCUMENT_NO = item.CDOCUMENT_NO,
                    CDOCUMENT_DATE = item.CDOCUMENT_DATE,
                    DDOCUMENT_DATE = !string.IsNullOrWhiteSpace(item.CDOCUMENT_DATE)
                                    ? DateTime.ParseExact(item.CDOCUMENT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture)
                                    : default
                };

                loDetail.Add(itemDetail);
            }

            // Assign Data
            loData.HeaderData = loHeaderData;
            loData.ListDetail = loDetail;

            return loData;
        }

        public static GLTR00100ResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "010",
                CPRINT_NAME = "JOURNAL TRANSACTION",
                CUSER_ID = "FMC",
            };

            GLTR00100ResultWithBaseHeaderPrintDTO loRtn = new GLTR00100ResultWithBaseHeaderPrintDTO();

            loRtn.BaseHeaderData = loParam;
            loRtn.GLTR = DefaultData();

            return loRtn;
        }
    }

}
