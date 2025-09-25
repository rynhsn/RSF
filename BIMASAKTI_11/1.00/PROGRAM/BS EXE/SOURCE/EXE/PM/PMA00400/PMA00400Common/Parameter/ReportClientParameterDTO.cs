namespace PMA00400Common.Parameter
{
    public class ReportClientParameterDTO
    {
        public string? CTENANT_CUSTOMER_ID { get; set; }
        public string? CCOMPANY_ID { get; set; }
        public string? CREPORT_CULTURE { get; set; }
        public int IDECIMAL_PLACES { get; set; }
        public string? CNUMBER_FORMAT { get; set; }
        public string? CDATE_LONG_FORMAT { get; set; }
        public string? CDATE_SHORT_FORMAT { get; set; }
        public string? CTIME_LONG_FORMAT { get; set; }
        public string? CTIME_SHORT_FORMAT { get; set; }
        public byte[] OCOMPANY_LOGO{ get; set; }
        public string CPRINT_CODE { get; set; }
        public string CCOMPANY_NAME { get; set; }
        public string CPRINT_NAME { get; set; }
    }
}
