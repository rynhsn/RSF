namespace PMT01500Common.DTO._1._AgreementList
{
    public class PMT01500ChangeStatusParameterDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CPROPERTY_ID { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CACCEPT_DATE { get; set; }
        public string? CNEW_STATUS { get; set; }
        public string CREASON { get; set; } = "";
        public string? CNOTES { get; set; }
        public string? CUSER_ID { get; set; }
    }
}