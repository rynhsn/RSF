using System;

namespace PMT06000Common.DTOs
{
    public class PMT06000OvtGridDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CTENANT_DISPLAY => CTENANT_ID + " - " + CTENANT_NAME;

        public string CREC_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CREF_NO { get; set; }
        public string CAGREEMENT_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime? DREF_DATE { get; set; }
        public string CREF_PRD { get; set; }
        public string CINV_PRD { get; set; }
        public string CREQUEST_NAME { get; set; }
        public string CDESCRIPTION { get; set; }
        public string CINVOICE_NO { get; set; }
        public string CINVOICE_DATE { get; set; }
        public DateTime? DINVOICE_DATE { get; set; }
        public string CTRANS_STATUS { get; set; }
        public string CTRANS_STATUS_DESC { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
}