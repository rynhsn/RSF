using PMT50600COMMON.DTOs.PMT50610Print;
using BaseHeaderReportCOMMON.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PMT50600COMMON.Models
{
    public class PMT50610PrintReportModelDummyData
    {
        public static PMT50610PrintReportResultDTO DefaultDataReport()
        {
            PMT50610PrintReportResultDTO loData = new PMT50610PrintReportResultDTO()
            {
                Column = new PMT50610PrintReportColumnDTO()
            };

            int lnHeader = 5;
            int lnDetail;
            List<PMT50610PrintReportDTO> loCollection = new List<PMT50610PrintReportDTO>();
            for (int j = 1; j <= lnHeader; j++)
            {
                loCollection.Add(new PMT50610PrintReportDTO()
                {
                    CREF_NO = "REF001",
                    CREF_DATE = "20230101",
                    CDOC_NO = "DOC001",
                    CDOC_DATE = "20230101",
                    CTENANT_NAME = "SUPP001",
                    CCUSTOMER_TYPE_NAME = $"tenant {j}",
                    CADDRESS = "SUP001 ADDRESS",
                    CCURRENCY_CODE = "IDR",
                    CPAY_TERM_NAME = "TERM001",
                    CPHONE1 = "SUP_PHONE001",
                    //CTENANT_FAX1 = "SUP_FAX001",
                    CEMAIL = "CSUP_EMAIL001",
                    INO = j,
                    CPRODUCT_ID = $"PROD00{j}",
                    CPRODUCT_NAME = $"PRODUCT {j}",
                    CDETAIL_DESC = $"DETAIL {j}",
                    NTRANS_QTY = j*2, 
                    CUNIT = "PCS",
                    NUNIT_PRICE = j*100,
                    NLINE_AMOUNT = j*50,
                    NTOTAL_DISCOUNT = j*10,
                    //NDIST_ADD_ON = j*3,
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

            PMT50610PrintReportHeaderDTO loTempData = loCollection
            .GroupBy(item => new {
                item.CREF_NO,
                item.CREF_DATE,
                item.CDOC_NO,
                item.CDOC_DATE,
                item.CTENANT_NAME,
                item.CCUSTOMER_TYPE_NAME,
                item.CADDRESS,
                item.CCURRENCY_CODE,
                item.CPAY_TERM_NAME,
                item.CPHONE1,
                //item.CTENANT_FAX1,
                item.CEMAIL,
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
            .Select(header => new PMT50610PrintReportHeaderDTO
            {
                CREF_NO = header.Key.CREF_NO,
                CREF_DATE = header.Key.CREF_DATE,
                DREF_DATE = DateTime.ParseExact(header.Key.CREF_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                CDOC_NO = header.Key.CDOC_NO,
                CDOC_DATE = header.Key.CDOC_DATE,
                DDOC_DATE = DateTime.ParseExact(header.Key.CDOC_DATE, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture),
                CTENANT_NAME = header.Key.CTENANT_NAME,
                CCUSTOMER_TYPE_NAME = header.Key.CCUSTOMER_TYPE_NAME,
                CTENANT_ADDRESS = header.Key.CADDRESS,
                CCURRENCY_CODE = header.Key.CCURRENCY_CODE,
                CPAY_TERM_NAME = header.Key.CPAY_TERM_NAME,
                CTENANT_PHONE1 = header.Key.CPHONE1,
                //CTENANT_FAX1 = header.Key.CTENANT_FAX1,
                CTENANT_EMAIL1 = header.Key.CEMAIL,
                CJRN_ID = header.Key.CJRN_ID,
                FooterData = new PMT50610PrintReportFooterDTO()
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
                    .Select(detail => new PMT50610PrintReportDetailDTO
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
                        //NDIST_ADD_ON = detail.NDIST_ADD_ON,
                        NLINE_TAXABLE_AMOUNT = detail.NLINE_TAXABLE_AMOUNT
                    })
                    .ToList()
            })
            .FirstOrDefault();

            List<PMT50610PrintReportSubDetailDTO> loSubDetail = new List<PMT50610PrintReportSubDetailDTO>();
            for (int j = 1; j <= lnHeader; j++)
            {
                loSubDetail.Add(new PMT50610PrintReportSubDetailDTO()
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

        public static PMT50610PrintReportResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            PMT50610PrintReportResultWithBaseHeaderPrintDTO loRtn = new PMT50610PrintReportResultWithBaseHeaderPrintDTO();
            loRtn.BaseHeaderData = GenerateDataModelHeader.DefaultData().BaseHeaderData;
            loRtn.ReportData = PMT50610PrintReportModelDummyData.DefaultDataReport();

            return loRtn;
        }
    }
}
