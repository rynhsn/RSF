namespace LMT03500Common.DTOs
{
    public class LMT03500PeriodDTO
    {
        public string CYEAR { get; set; }
        public string CPERIOD_NO { get; set; }
        public string CSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
        private string _CPERIOD { get; set; }

        public string CPERIOD
        {
            get => $"{CYEAR}-{CPERIOD_NO}";
            set => _CPERIOD = value;
        }   
    }

    public class LMT03500YearDTO
    {
        public string CYEAR { get; set; }
    }
}