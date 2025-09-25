using APT00100COMMON.DTOs.APT00110Print;
using BaseHeaderReportCOMMON.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APT00100COMMON.Models
{
    public class APT00110PrintReportModelDummyData
    {
        public static APT00110PrintReportResultDTO DefaultDataReport()
        {
            APT00110PrintReportResultDTO loData = new APT00110PrintReportResultDTO()
            {
                Column = new APT00110PrintReportColumnDTO()
            };

            int lnHeader = 5;
            int lnDetail;
            List<APT00110PrintReportDTO> loCollection = new List<APT00110PrintReportDTO>();
            for (int j = 1; j <= lnHeader; j++)
            {
                loCollection.Add(new APT00110PrintReportDTO()
                {
                    CREF_NO = "REF001",
                    CREF_DATE = "20230101",
                    CDOC_NO = "DOC001",
                    CDOC_DATE = "20230101",
                    CSUPPLIER_NAME = "SUPP001",
                    CSUPPLIER_ADDRESS = "SUP001 ADDRESS",
                    CCURRENCY_CODE = "IDR",
                    CPAY_TERM_NAME = "TERM001",
                    CSUPPLIER_PHONE1 = "SUP_PHONE001",
                    CSUPPLIER_FAX1 = "SUP_FAX001",
                    CSUPPLIER_EMAIL1 = "CSUP_EMAIL001",
                    INO = j,
                    CPRODUCT_ID = $"PROD00{j}",
                    CPRODUCT_NAME = $"PRODUCT {j}",
                    CDETAIL_DESC = $"DETAIL {j}",
                    NTRANS_QTY = j*2, 
                    CUNIT = "PCS",
                    NUNIT_PRICE = j*100,
                    NLINE_AMOUNT = j*50,
                    NTOTAL_DISCOUNT = j*10,
                    NDIST_ADD_ON = j*3,
                    NLINE_TAXABLE_AMOUNT = j*5,
                    CTOTAL_AMOUNT_IN_WORDS = "TOTAL AMOUNT IN WORDS",
                    CTRANS_DESC = "NOTES001",
                    NTAXABLE_AMOUNT = 100,
                    NTAX = 10, 
                    NOTHER_TAX = 5,
                    NADDITION = 20,
                    NDEDUCTION = 15,
                    NTRANS_AMOUNT = 30
                });
            }

            APT00110PrintReportHeaderDTO loTempData = loCollection
            .GroupBy(item => new {
                item.CREF_NO,
                item.CREF_DATE,
                item.CDOC_NO,
                item.CDOC_DATE,
                item.CSUPPLIER_NAME,
                item.CSUPPLIER_ADDRESS,
                item.CCURRENCY_CODE,
                item.CPAY_TERM_NAME,
                item.CSUPPLIER_PHONE1,
                item.CSUPPLIER_FAX1,
                item.CSUPPLIER_EMAIL1,
                item.CJRN_ID,
                item.CTOTAL_AMOUNT_IN_WORDS,
                item.CTRANS_DESC,
                item.NTAXABLE_AMOUNT,
                item.NTAX,
                item.NOTHER_TAX,
                item.NADDITION,
                item.NDEDUCTION,
                item.NTRANS_AMOUNT
            })
            .Select(header => new APT00110PrintReportHeaderDTO
            {
                CREF_NO = header.Key.CREF_NO,
                CREF_DATE = header.Key.CREF_DATE,
                DREF_DATE = DateTime.ParseExact(header.Key.CREF_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                CDOC_NO = header.Key.CDOC_NO,
                CDOC_DATE = header.Key.CDOC_DATE,
                DDOC_DATE = DateTime.ParseExact(header.Key.CDOC_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                CSUPPLIER_NAME = header.Key.CSUPPLIER_NAME,
                CSUPPLIER_ADDRESS = header.Key.CSUPPLIER_ADDRESS,
                CCURRENCY_CODE = header.Key.CCURRENCY_CODE,
                CPAY_TERM_NAME = header.Key.CPAY_TERM_NAME,
                CSUPPLIER_PHONE1 = header.Key.CSUPPLIER_PHONE1,
                CSUPPLIER_FAX1 = header.Key.CSUPPLIER_FAX1,
                CSUPPLIER_EMAIL1 = header.Key.CSUPPLIER_EMAIL1,
                CJRN_ID = header.Key.CJRN_ID,
                FooterData = new APT00110PrintReportFooterDTO()
                {
                    CTOTAL_AMOUNT_IN_WORDS = header.Key.CTOTAL_AMOUNT_IN_WORDS,
                    CTRANS_DESC = header.Key.CTRANS_DESC,
                    NTAXABLE_AMOUNT = header.Key.NTAXABLE_AMOUNT,
                    NTAX = header.Key.NTAX,
                    NOTHER_TAX = header.Key.NOTHER_TAX,
                    NADDITION = header.Key.NADDITION,
                    NDEDUCTION = header.Key.NDEDUCTION,
                    NTRANS_AMOUNT = header.Key.NTRANS_AMOUNT
                },
                DetailData = header
                    .Select(detail => new APT00110PrintReportDetailDTO
                    {
                        INO = detail.INO,
                        CPRODUCT_ID = detail.CPRODUCT_ID,
                        CPRODUCT_NAME = detail.CPRODUCT_NAME,
                        CDETAIL_DESC = detail.CDETAIL_DESC,
                        NTRANS_QTY = detail.NTRANS_QTY,
                        CUNIT = detail.CUNIT,
                        NUNIT_PRICE = detail.NUNIT_PRICE,
                        NLINE_AMOUNT = detail.NLINE_AMOUNT,
                        NTOTAL_DISCOUNT = detail.NTOTAL_DISCOUNT,
                        NDIST_ADD_ON = detail.NDIST_ADD_ON,
                        NLINE_TAXABLE_AMOUNT = detail.NLINE_TAXABLE_AMOUNT
                    })
                    .ToList()
            })
            .FirstOrDefault();

            List<APT00110PrintReportSubDetailDTO> loSubDetail = new List<APT00110PrintReportSubDetailDTO>();
            for (int j = 1; j <= lnHeader; j++)
            {
                loSubDetail.Add(new APT00110PrintReportSubDetailDTO()
                {
                    CGLACCOUNT_NO = $"ACC00{j}",
                    CGLACCOUNT_NAME = $"ACCOUNT {j}",
                    CCENTER_NAME = $"CENTER00{j}",
                    CCURRENCY_CODE = loTempData.CCURRENCY_CODE,
                    NDEBIT = 1000,
                    NCREDIT = 1000
                });
            }

            loData.Data = loTempData;
            loData.SubDetail = loSubDetail;

            return loData;
        }

        public static APT00110PrintReportResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            APT00110PrintReportResultWithBaseHeaderPrintDTO loRtn = new APT00110PrintReportResultWithBaseHeaderPrintDTO();
            loRtn.BaseHeaderData = GenerateDataModelHeader.DefaultData().BaseHeaderData;
            loRtn.ReportData = APT00110PrintReportModelDummyData.DefaultDataReport();

            return loRtn;
        }
    }
}
