using System;

namespace PMT06500Common.DTOs
{
    public class PMT06500InvoiceDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
        public string CLANG_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CPROPERTY_NAME { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CDEPT_NAME { get; set; } = "";
        public string CTRANS_STATUS { get; set; } = "";
        public string CTRANS_STATUS_DESC { get; set; } = "";
        public string CREC_ID { get; set; } = "";
        public string CINV_PRD { get; set; } = "";
        public string CPERIOD { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
        public DateTime? DREF_DATE { get; set; }
        public string CDESCRIPTION { get; set; } = "";
        public string CAGREEMENT_NO { get; set; } = "";
        public string CTENANT_ID { get; set; } = "";
        public string CTENANT_NAME { get; set; } = "";
        public string CBUILDING_ID { get; set; } = "";
        public string CBUILDING_NAME { get; set; } = "";
        public string CUNIT_DESCRIPTION { get; set; } = "";
        public string CUPDATE_BY { get; set; } = "";
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime DCREATE_DATE { get; set; }
    }

    public class PMT06500SummaryDTO
    {
        public string CUNIT_ID { get; set; }
        public string CUNIT_NAME { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public string CSERVICE => CCHARGES_ID + " - " + CCHARGES_NAME;
        public decimal NACTUAL_AREA_SIZE { get; set; }
        public decimal NRATE_HOUR { get; set; }
        public int IHOURS { get; set; }
        public decimal NTOTAL_AMOUNT { get; set; }
    }
}