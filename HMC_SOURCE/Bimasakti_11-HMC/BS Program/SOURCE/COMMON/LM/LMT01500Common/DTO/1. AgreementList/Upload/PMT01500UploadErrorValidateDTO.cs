namespace PMT01500Common.DTO._1._AgreementList.Upload
{
    public class PMT01500UploadErrorValidateDTO
    {
        public int NO { get; set; }
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CREF_DATE { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CDOC_NO { get; set; }
        public string? CDOC_DATE { get; set; }
        public string? CSTART_DATE { get; set; }
        public string? CEND_DATE { get; set; }
        public string? CMONTH { get; set; }
        public string? CYEAR { get; set; }
        public string? CDAY { get; set; }
        public string? CSALESMAN_ID { get; set; }
        public string? CTENANT_ID { get; set; }
        public string? CUNIT_DESCRIPTION { get; set; }
        public string? CNOTES { get; set; }
        public string? CCURRENCY_CODE { get; set; }
        public string? CLEASE_MODE { get; set; }
        public string? CCHARGE_MODE { get; set; }
        public string? CNOTESError { get; set; }
        public string? ErrorFlag { get; set; } = "N";
    }
}
