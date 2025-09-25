namespace GLR00100Common.DTOs
{
    public class GLR00100TransCodeDTO
    {
        public string CTRANS_CODE { get; set; }
        public string CTRANSACTION_NAME { get; set; }
        
        public string CTRANS_CODE_DESC => CTRANS_CODE + "-" + CTRANSACTION_NAME;
    }
}