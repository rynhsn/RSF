namespace GSM05000Common.DTOs
{
    public class GSM05000TrxCodeParamsDTO
    {
        public string CTRANS_CODE { get; set; }
        public GSM05000eTabName ETAB_NAME { get; set; }
    }
    
    public class GSM05000TrxDeptParamsDTO
    {
        public string CTRANSACTION_CODE { get; set; }
        public string CDEPT_CODE { get; set; }
    }
    
    public class GSM05000DeptCodeParamsDTO
    {
        public string CDEPT_CODE { get; set; }
    }
    
    public class GSM05000CopyToParamsDTO
    {
        public string CTRANSACTION_CODE { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_CODE_TO { get; set; }
    }
    
    public class GSM05000CopyFromParamsDTO
    {
        public string CTRANSACTION_CODE { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_CODE_FROM { get; set; }
    }
}