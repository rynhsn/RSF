using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.DTO._2._LOO._2._LOO___Offer
{
    public class PMT01700LOO_Offer_TenantDetailDTO : R_APIResultBaseDTO
    {
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CDEPT_NAME { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CTRANS_NAME { get; set; }
        public string? CREF_NO { get; set; }
        public string? CREF_DATE { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public string? CDOC_NO { get; set; }
        public string? CDOC_DATE { get; set; }
        public string? CSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        public int IMONTHS { get; set; }
        public int IYEARS { get; set; }
        public int IDAYS { get; set; }
        public int IHOURS { get; set; }
        public string? CSALESMAN_ID { get; set; }
        public string? CSALESMAN_NAME { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CUNIT_DESCRIPTION { get; set; }
        public string? CNOTES { get; set; }
        public string? CCURRENCY_CODE { get; set; }
        public string? CCURRENCY_NAME { get; set; }
        public string? CLEASE_MODE { get; set; }
        public string? CLEASE_MODE_DESCR { get; set; }
        public string? CCHARGE_MODE { get; set; }
        public string? CCHARGE_MODE_DESCR { get; set; }
        public string? CTRANS_STATUS { get; set; }
        public string? CTRANS_STATUS_DESCR { get; set; }
        public string? CAGREEMENT_STATUS { get; set; }
        public string? CAGREEMENT_STATUS_DESCR { get; set; }
        public string? CUNIT_NAME_LIST { get; set; }
        public string? CUNIT_ID { get; set; }
        public string? CUNIT_NAME { get; set; }
        public string? CUNIT_TYPE_CATEGORY_ID { get; set; }
        public string? CUNIT_TYPE_CATEGORY_NAME { get; set; }
        public decimal NTOTAL_GROSS_AREA { get; set; }
        public decimal NTOTAL_NET_AREA { get; set; }
        public decimal NTOTAL_ACTUAL_AREA { get; set; }
        public decimal NTOTAL_COMMON_AREA { get; set; }
        public int ITOTAL_UNIT { get; set; }
        public string? CTENANT_TYPE_ID { get; set; }
        public string? CADDRESS { get; set; }
        public string? CEMAIL { get; set; }
        public string? CPHONE1 { get; set; }
        public string? CPHONE2 { get; set; }
        public string? CTENANT_CATEGORY_ID { get; set; }
        public string? CTAX_TYPE { get; set; }
        public string? CTAX_ID { get; set; }
        public string? CTAX_NAME { get; set; }
        public string? CID_TYPE { get; set; }
        public string? CID_NO { get; set; }
        public string? CID_EXPIRED_DATE { get; set; }
        public string? CTAX_ADDRESS { get; set; }
        public string? CATTENTION1_NAME { get; set; }
        public string? CATTENTION1_EMAIL { get; set; }
        public string? CATTENTION1_MOBILE_PHONE1 { get; set; }
        public string? CORIGINAL_REF_NO { get; set; }
        public string? CFOLLOW_UP_DATE { get; set; }
        public string? CEXPIRED_DATE { get; set; }
        public string? CBILLING_RULE_CODE { get; set; }
        public decimal NBOOKING_FEE { get; set; }
        public string? CTC_CODE { get; set; }
        public int IREVISE_SEQ_NO { get; set; }
        public bool LWITH_FO { get; set; }
        public string? CHO_PLAN_DATE { get; set; }
        public string? CLINK_DEPT_CODE { get; set; }
        public string? CLINK_TRANS_CODE { get; set; }
        public string? CLINK_REF_NO { get; set; }
    }
}
