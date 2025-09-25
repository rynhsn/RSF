namespace ICR00600Common.DTOs
{
    public class ICR00600TransCodeDTO
    {
        public string CTRANS_CODE { get; set; }
        public string CTRANSACTION_NAME { get; set; }
        public string CTRANS_SHORT_NAME { get; set; }
        public string CMODULE_ID { get; set; }
        public bool LINCREMENT_FLAG { get; set; }
        public bool LDEPT_MODE { get; set; }
        public string CDEPT_DELIMITER { get; set; }
        public bool LTRANSACTION_MODE { get; set; }
    }
}