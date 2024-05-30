namespace GLR00100Common.DTOs
{
    public class GLR00100ResultActivityReportDTO
    {
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public string CREF_DATE_DISPLAY { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CDOC_NO { get; set; }
        public string CDOC_DATE { get; set; }
        public string CDOC_DATE_DISPLAY { get; set; }
        public string CTRANS_DESC { get; set; }
        public string CSTATUS { get; set; }
        public string CSTATUS_NAME { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CCENTER_CODE { get; set; }
        public string CDBCR { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public decimal NDEBIT_AMOUNT { get; set; }
        public decimal NCREDIT_AMOUNT { get; set; }
        public string CDETAIL_DESC { get; set; }
        public string CDOCUMENT_NO { get; set; }
        public string CDOCUMENT_DATE { get; set; }
        public string CDOCUMENT_DATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public string CUPDATE_BY { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTRANSACTION_NAME { get; set; }
        public string CMODULE_ID { get; set; }
        public string CCUST_SUPP_ID { get; set; }
        public string CCUST_SUPP_NAME { get; set; }
        public string CFROM_DEPT_CODE { get; set; }
        public string CTO_DEPT_CODE { get; set; }
        public string CFROM_PERIOD { get; set; }
        public string CTO_PERIOD { get; set; }
        public string CCURRENCY_TYPE_NAME { get; set; }
    }

    public class GLR00100ResultActivitySubReportDTO
    {
        public string CCURRENCY_TYPE_NAME { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CCENTER_CODE { get; set; }
        public decimal NTOTAL_DEBIT { get; set; }
        public decimal NTOTAL_CREDIT { get; set; }
    }
}