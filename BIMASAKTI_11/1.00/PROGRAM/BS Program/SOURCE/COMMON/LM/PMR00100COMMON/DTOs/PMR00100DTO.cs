using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00100Common.DTOs
{
    public class PMR00100DTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTRANS_NAME { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CTENURE { get; set; }
        public string CSALESMAN_ID { get; set; }
        public string CSALESMAN_NAME { get; set; }
        public decimal NREVISION_COUNT { get; set; }
        public string CTC_CODE { get; set; }
        public string CTC_DESCRIPTION { get; set; }
        public string CTC_MESSAGE { get; set; }
        public string CAGREEMENT_STATUS_ID { get; set; }
        public string CAGREEMENT_STATUS_NAME { get; set; }
        public string CTRANS_STATUS_ID { get; set; }
        public string CTRANS_STATUS_NAME { get; set; }
        public decimal NTOTAL_PRICE { get; set; }
        public string CTAX { get; set; }
        public string CUNIT_DETAIL_ID { get; set; }
        public string CUNIT_DETAIL_NAME { get; set; }
        public decimal NUNIT_DETAIL_GROSS_AREA_SIZE { get; set; }
        public decimal NUNIT_DETAIL_NET_AREA_SIZE { get; set; }
        public decimal NUNIT_DETAIL_COMMON_AREA_SIZE { get; set; }
        public decimal NUNIT_DETAIL_PRICE { get; set; }
        public string CCHARGE_DETAIL_TYPE_NAME { get; set; }
        public string CCHARGE_DETAIL_UNIT_NAME { get; set; }
        public string CCHARGE_DETAIL_CHARGE_NAME { get; set; }
        public string CCHARGE_DETAIL_TAX_NAME { get; set; }
        public string CCHARGE_DETAIL_START_DATE { get; set; }
        public string CCHARGE_DETAIL_END_DATE { get; set; }
        public bool LFOR_TOTAL_AREA { get; set; }
        public string CCHARGE_DETAIL_TENURE { get; set; }
        public string CCHARGE_DETAIL_FEE_METHOD { get; set; }
        public decimal NCHARGE_DETAIL_FEE_AMOUNT { get; set; }
        public decimal NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT { get; set; }
        public string CDEPOSIT_DETAIL_ID { get; set; }
        public string CDEPOSIT_DETAIL_DATE { get; set; }
        public decimal NDEPOSIT_DETAIL_AMOUNT { get; set; }
        public string CDEPOSIT_DETAIL_DESCRIPTION { get; set; }
        public byte[] CLOGO { get; set; }
        public string CCOMPANY_NAME { get; set; }
        public string CDATETIME_NOW { get; set; }

    }


    public class PMR00100DataDTO : R_APIResultBaseDTO
    {
        public List<PMR00100DTO> Data { get; set; }
    }
}
