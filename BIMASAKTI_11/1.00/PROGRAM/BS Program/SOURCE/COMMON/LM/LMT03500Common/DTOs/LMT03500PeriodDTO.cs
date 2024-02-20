namespace LMT03500Common.DTOs
{
    public class LMT03500PeriodDTO
    {
        public string CCYEAR { get; set; }
        public string CPERIOD_NO { get; set; }
        public string CSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
        private string _CPERIOD { get; set; }

        public string CPERIOD
        {
            get => $"{CCYEAR}-{CPERIOD_NO}";
            set => _CPERIOD = value;
        }   
    }
}