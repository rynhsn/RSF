using R_CommonFrontBackAPI;
using System;

namespace PMT01300COMMON
{
    public class PMT01300UploadAgreementDTO
    {
        public int NO { get; set; } = 0;
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
        public string CBUILDING_ID { get; set; } = "";
        public string CDOC_NO { get; set; } = "";
        public string CDOC_DATE { get; set; } = "";
        public string CSTART_DATE { get; set; } = "";
        public string CEND_DATE { get; set; } = "";
        public string CHO_PLAN_DATE { get; set; } = "";
        public string CYEAR { get; set; } = "";
        public string CMONTH { get; set; } = "";
        public string CDAY { get; set; } = "";
        public string CSALESMAN_ID { get; set; } = "";
        public string CTENANT_ID { get; set; } = "";
        public string CUNIT_DESCRIPTION { get; set; } = "";
        public string CNOTES { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public string CLEASE_MODE { get; set; } = "";
        public string CCHARGE_MODE { get; set; } = "";
        public string CBILLING_RULE_CODE { get; set; } = "";
        public decimal NBOOKING_FEE { get; set; } = 0;
        public string CTC_CODE { get; set; } = "";
        public int ISEQ_NO_ERROR { get; set; } = 0;
        public bool LERROR { get; set; }
        public string CHO_ACTUAL_DATE { get; set; } = "";
    }
}
