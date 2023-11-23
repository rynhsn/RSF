namespace GSM05000Common.DTOs
{
    public class GSM05000GridDTO
    {
        public string CTRANS_CODE { get; set; }

        public string CTRANSACTION_NAME { get; set; }
        public string CMODULE_NAME { get; set; }
        
        // private string _CTRANSACTION;
        public string CTRANSACTION { get => CTRANSACTION_NAME + " (" + CTRANS_CODE + ")"; }
        public string MODULE { get => CMODULE_NAME; }
    }
}