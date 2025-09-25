using System;

namespace PMT01500Common.DTO._1._AgreementList
{
    public class PMT01500AgreementListOriginalDTO
    {
        //Tambahan
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CBUILDING_ID { get; set; }
        //Real DTOs
        public string? CDEPT_NAME { get; set; }
        public string? CREF_NO { get; set; }
        public string? CREF_DATE { get; set; }
        public string? CSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        //Updated 30 Apr 2024 : DateTime for START and END DATE
        public DateTime? DSTART_DATE { get; set; }
        public DateTime? DEND_DATE { get; set; }
        public string? CTENANT_NAME { get; set; }
        public string? CBUILDING_NAME { get; set; }
        public string? CLEASE_MODE_DESCR { get; set; }
        public string? CCHARGE_MODE { get; set; }
        public string? CCHARGE_MODE_DESCR { get; set; }
        public string? CAGREEMENT_STATUS_DESCR { get; set; }
        public string? CUNIT_DESCRIPTION { get; set; }
        public string? CCURRENCY_CODE { get; set; }
        public string? CDOC_NO { get; set; }
        public string? CDOC_DATE { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }

    }
}
