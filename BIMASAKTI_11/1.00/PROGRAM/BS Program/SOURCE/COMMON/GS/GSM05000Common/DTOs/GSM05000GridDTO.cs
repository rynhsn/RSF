namespace GSM05000Common.DTOs
{
    public class GSM05000GridDTO
    {
        public string CTRANSACTION_CODE { get; set; }
        public string CTRANSACTION_NAME { get; set; }
        public string CMODULE_NAME { get; set; }

        private string _CTRANSACTION;
        public string CTRANSACTION { get => _CTRANSACTION; set => _CTRANSACTION = CTRANSACTION_NAME + " (" + CTRANSACTION_CODE + ")"; }
    }
}