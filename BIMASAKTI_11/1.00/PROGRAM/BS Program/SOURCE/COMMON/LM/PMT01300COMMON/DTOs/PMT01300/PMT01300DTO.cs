using R_CommonFrontBackAPI;
using System;

namespace PMT01300COMMON
{
    public class PMT01300DTO
    {
        public string CACTION { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CREC_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTRANS_NAME { get; set; }
        public string CREF_NO { get; set; }
        public string CLINK_REF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CDOC_NO { get; set; }
        public string CDOC_DATE { get; set; }
        public DateTime? DDOC_DATE { get; set; }
        public string CSTART_DATE { get; set; }
        public DateTime? DSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
        public DateTime? DEND_DATE { get; set; }
        public string CMONTH { get; set; }
        public string CYEAR { get; set; }
        public string CDAY { get; set; }
        public string CSALESMAN_ID { get; set; }
        public string CSALESMAN_NAME { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CUNIT_DESCRIPTION { get; set; }
        public string CNOTES { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CCURRENCY_NAME { get; set; }
        public string CLEASE_MODE { get; set; }
        public string CLEASE_MODE_DESCR { get; set; }
        public string CCHARGE_MODE { get; set; }
        public string CCHARGE_MODE_DESCR { get; set; }
        public string CTRANS_STATUS { get; set; }
        public string CTRANS_STATUS_DESCR { get; set; }
        public string CAGREEMENT_STATUS { get; set; }
        public string CAGREEMENT_STATUS_DESCR { get; set; }
        public string CDOC_TYPE { get; set; }
        public string CORIGINAL_REF_NO { get; set; }
        public string CFOLLOW_UP_DATE { get; set; }
        public DateTime? DFOLLOW_UP_DATE { get; set; }
        public string CHO_ACTUAL_DATE { get; set; }
        public DateTime? DHO_ACTUAL_DATE { get; set; }
        public string COPEN_DATE { get; set; }
        public DateTime? DOPEN_DATE { get; set; }

        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }

        //Detail
        public string CUNIT_NAME_LIST { get; set; }
        public string CUNIT_ID { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CUNIT_NAME { get; set; }
        public string CUNIT_TYPE_CATEGORY_ID { get; set; }
        public string CUNIT_TYPE_CATEGORY_NAME { get; set; }
        public decimal NTOTAL_GROSS_AREA { get; set; }
        public decimal NTOTAL_NET_AREA { get; set; }
        public decimal NTOTAL_ACTUAL_AREA { get; set; }
        public decimal NTOTAL_COMMON_AREA { get; set; }
        public int ITOTAL_UNIT { get; set; }
        public string CTENANT_TYPE_ID { get; set; }
        public string CADDRESS { get; set; }
        public string CEMAIL { get; set; }
        public string CPHONE1 { get; set; }
        public string CPHONE2 { get; set; }
        public string CTENANT_CATEGORY_ID { get; set; }
        public string CTAX_TYPE { get; set; }
        public string CTAX_ID { get; set; }
        public string CTAX_NAME { get; set; }
        public string CID_TYPE { get; set; }
        public string CID_NO { get; set; }
        public string CID_EXPIRED_DATE { get; set; }
        public string CTAX_ADDRESS { get; set; }
        public string CATTENTION1_NAME { get; set; }
        public string CATTENTION1_EMAIL { get; set; }
        public string CATTENTION1_MOBILE_PHONE1 { get; set; }
        public string CEXPIRED_DATE { get; set; }
        public string CHO_DATE { get; set; }
        public string CBILLING_RULE_CODE { get; set; }
        public decimal NBOOKING_FEE { get; set; }
        public decimal NACTUAL_PRICE { get; set; }
        public string CTC_ID { get; set; }
        public string CTC_CODE { get; set; }
        public bool LPAID { get; set; }
        public int IREVISE_SEQ_NO { get; set; }
        public bool LWITH_FO { get; set; }
        public int IDAYS { get; set; }
        public int IMONTHS { get; set; }
        public int IYEARS { get; set; }
        public string CLINK_TRANS_CODE { get; set; }
        public string CLINK_DEPT_CODE { get; set; }
        public string CHO_PLAN_DATE { get; set; }
        public DateTime? DHO_PLAN_DATE { get; set; }
        public string CACTUAL_START_DATE { get; set; }
        public DateTime? DACTUAL_START_DATE { get; set; }
        public string CACTUAL_END_DATE { get; set; }
        public DateTime? DACTUAL_END_DATE { get; set; }
        public bool LIS_ADD_DATA_LOI { get; set; }
        public bool LPAY_OL_INCL_PENALTY { get; set; }
    }
}
