using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PMT01300COMMON.Models
{
    public static class GenerateDataModel
    {
        public static PMT01300ResultPrintDTO DefaultData()
        {
            PMT01300ResultPrintDTO loData = new PMT01300ResultPrintDTO();

            List<PMT01300ResultSPPrintDTO> loSPResult = new List<PMT01300ResultSPPrintDTO>();
            List<PMT01300ResultDetailSPPrintDTO> loDetailSPResult = new List<PMT01300ResultDetailSPPrintDTO>();

            for (int i = 1; i <= 5; i++)
            {
                loSPResult.Add(new PMT01300ResultSPPrintDTO
                {
                    INO = i,
                    CCOMPANY_ID = $"COMP{i}",
                    CPROPERTY_ID = $"PROP{i}",
                    CPROPERTY_NAME = $"Property Name {i}",
                    CDEPT_CODE = $"DPT{i}",
                    CDEPT_NAME = $"Department {i}",
                    CTRANS_CODE = $"TRANS{i}",
                    CREF_NO = $"REF{i}",
                    CREF_DATE = DateTime.Now.ToString("yyyyMMdd"),
                    CREF_PRD = $"Period {i}",
                    CTENANT_ID = $"TENANT{i}",
                    CTENANT_NAME = $"Tenant Name {i}",
                    CCUSTOMER_TYPE = $"CUST_TYPE{i}",
                    CCUSTOMER_TYPE_NAME = $"Customer Type Name {i}",
                    CADDRESS = $"Address {i}",
                    CPHONE1 = $"Phone1 {i}",
                    CPHONE2 = $"Phone2 {i}",
                    CEMAIL = $"email{i}@example.com",
                    CDOC_NO = $"DOC_NO{i}",
                    CDOC_DATE = DateTime.Now.ToString("yyyyMMdd"),
                    CPAY_TERM_CODE = $"PAY_TERM_CODE{i}",
                    CPAY_TERM_NAME = $"Pay Term Name {i}",
                    CDUE_DATE = DateTime.Now.AddDays(30).ToString("yyyyMMdd"),
                    CTRANS_DESC = $"Transaction Description {i}",
                    CCURRENCY_CODE = $"CURR_CODE{i}",
                    CCURRENCY_NAME = $"Currency Name {i}",
                    NLBASE_RATE = 100m + i,
                    NLCURRENCY_RATE = 1.1m + i,
                    NBBASE_RATE = 200m + i,
                    NBCURRENCY_RATE = 2.2m + i,
                    NTAX_BASE_RATE = 10m + i,
                    NTAX_CURRENCY_RATE = 0.5m + i,
                    NAMOUNT = 1000m + i,
                    NLAMOUNT = 1100m + i,
                    NBAMOUNT = 1200m + i,
                    NDISCOUNT = 50m + i,
                    NLDISCOUNT = 60m + i,
                    NBDISCOUNT = 70m + i,
                    NLSUMMARY_DISCOUNT = 80m + i,
                    NSUMMARY_DISCOUNT = 90m + i,
                    NADD_ON = 100m + i,
                    NBSUMMARY_DISCOUNT = 110m + i,
                    NLADD_ON = 120m + i,
                    NBADD_ON = 130m + i,
                    NTAXABLE_AMOUNT = 1400m + i,
                    NLTAXABLE_AMOUNT = 1500m + i,
                    NBTAXABLE_AMOUNT = 1600m + i,
                    NTAX = 170m + i,
                    NLTAX = 180m + i,
                    NBTAX = 190m + i,
                    NOTHER_TAX = 200m + i,
                    NLOTHER_TAX = 210m + i,
                    NBOTHER_TAX = 220m + i,
                    NADDITION = 230m + i,
                    NLADDITION = 240m + i,
                    NBADDITION = 250m + i,
                    NDEDUCTION = 260m + i,
                    NLDEDUCTION = 270m + i,
                    NBDEDUCTION = 280m + i,
                    NTRANS_AMOUNT = 2900m + i,
                    NLTRANS_AMOUNT = 3000m + i,
                    NBTRANS_AMOUNT = 3100m + i,
                    CSOURCE_MODULE = $"SRC_MOD{i}",
                    LTAXABLE = true,
                    CTAX_ID = $"TAX_ID{i}",
                    CTAX_NAME = $"Tax Name {i}",
                    NTAX_PCT = 10m + i,
                    CTRANS_STATUS = $"STATUS{i}",
                    CTRANS_STATUS_NAME = $"Status Name {i}",
                    NTOTAL_AMOUNT = 3200m + i,
                    CTOTAL_AMOUNT_IN_WORDS = $"Three Thousand Two Hundred and {i} Dollars",
                    LGLLINK = false,
                    CGL_REF_NO = $"GL_REF_NO{i}",
                    CLINK_REF_NO = $"LINK_REF_NO{i}",
                    CINVGRP_CODE = $"INVGRP_CODE{i}",
                    CINVGRP_NAME = $"Invoice Group Name {i}",
                    CUNIT_DESCRIPTION = $"Unit Description {i}",
                    NTOTAL_TAX = 330m + i,
                    NTAX_EXEMPTION = 340m + i,
                    CPROD_DEPT_CODE = $"PROD_DEPT_CODE{i}",
                    CPROD_TYPE = $"PROD_TYPE{i}",
                    CPRODUCT_ID = $"PROD_ID{i}",
                    CDETAIL_DESC = $"Detail Description {i}",
                    CPRODUCT_NAME = $"Product Name {i}",
                    NTRANS_QTY = 5m + i,
                    NAPPV_QTY = 6m + i,
                    NBILL_UNIT_QTY = 7m + i,
                    CUNIT = $"Unit{i}",
                    NUNIT_PRICE = 8m + i,
                    NLINE_AMOUNT = 350m + i,
                    NDISC_AMOUNT = 360m + i,
                    NDIST_DISCOUNT = 370m + i,
                    NTOTAL_DISCOUNT = 380m + i,
                    NDIST_ADD_ON = 390m + i,
                    NLINE_TAXABLE_AMOUNT = 400m + i,
                    NTAX_AMOUNT = 410m + i,
                    NOTHER_TAX_AMOUNT = 420m + i,
                    CJRN_ID = $"JRN_ID{i}"
                });
            }
            for (int i = 1; i <= 5; i++)
            {
                loDetailSPResult.Add(new PMT01300ResultDetailSPPrintDTO
                {
                    INO = i,
                    CREC_ID = $"REC{i}",
                    CSEQ_NO = $"SEQ{i}",
                    CGLACCOUNT_NO = $"GLACC{i}",
                    CGLACCOUNT_NAME = $"GL Account Name {i}",
                    CCENTER_CODE = $"CENTER{i}",
                    CCENTER_NAME = $"Center Name {i}",
                    CDBCR = i % 2 == 0 ? "DB" : "CR",
                    CCURRENCY_CODE = $"CURR{i}",
                    NTRANS_AMOUNT = 1000m + i,
                    NLTRANS_AMOUNT = 1100m + i,
                    NBTRANS_AMOUNT = 1200m + i,
                    CDETAIL_DESC = $"Detail Description {i}",
                    CDOCUMENT_NO = $"DOC{i}",
                    CDOCUMENT_DATE = DateTime.Now.ToString("yyyyMMdd"),
                    NDEBIT = 500m + i,
                    NCREDIT = 300m + i,
                    NLDEBIT = 600m + i,
                    NLCREDIT = 400m + i,
                    NBDEBIT = 700m + i,
                    NBCREDIT = 500m + i,
                    CBSIS = $"BSIS{i}",
                    CINPUT_TYPE = $"INPUT_TYPE{i}"
                });
            }

            // Header
            var loHeaderSP = loSPResult.FirstOrDefault();
            //PMT01300HeaderPrintDTO loHeader = R_Utility.R_ConvertObjectToObject<PMT01300ResultSPPrintDTO, PMT01300HeaderPrintDTO>(loHeaderSP);

            //if (DateTime.TryParseExact(loHeader.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate))
            //{
            //    loHeader.DREF_DATE = ldRefDate;
            //}
            //else
            //{
            //    loHeader.DREF_DATE = null;
            //}
            //if (DateTime.TryParseExact(loHeader.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDocDate))
            //{
            //    loHeader.DDOC_DATE = ldDocDate;
            //}
            //else
            //{
            //    loHeader.DDOC_DATE = null;
            //}

            //loData.Header = loHeader;
            //loData.Detail = R_Utility.R_ConvertCollectionToCollection<PMT01300ResultSPPrintDTO, PMT01300DetailPrintDTO>(loSPResult).ToList();
            //loData.Footer = R_Utility.R_ConvertObjectToObject<PMT01300ResultSPPrintDTO, PMT01300FooterPrintDTO>(loHeaderSP);
            //loData.SubDetail = R_Utility.R_ConvertCollectionToCollection<PMT01300ResultDetailSPPrintDTO, PMT01300SubDetailPrintDTO>(loDetailSPResult).ToList();

            return loData;
        }

        public static PMT01300ResultWithBaseHeaderPrintDTO DefaultDataWithHeader()
        {
            var loParam = new BaseHeaderDTO()
            {
                CCOMPANY_NAME = "PT Realta Chackradarma",
                CPRINT_CODE = "001",
                CPRINT_NAME = "PM INVOICE",
                CUSER_ID = "FMC",
            };

            PMT01300ResultWithBaseHeaderPrintDTO loRtn = new PMT01300ResultWithBaseHeaderPrintDTO { Column = new PMT01300ColumnPrintDTO() };
            loRtn.BaseHeaderData = loParam;
            loRtn.Invoice = GenerateDataModel.DefaultData();

            return loRtn;
        }
    }

}
