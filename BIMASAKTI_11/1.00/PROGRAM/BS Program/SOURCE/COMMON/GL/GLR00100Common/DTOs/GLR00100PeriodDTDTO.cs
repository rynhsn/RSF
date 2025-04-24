namespace GLR00100Common.DTOs
{
    public class GLR00100PeriodDTDTO
    {
        private string _cperiodNo;
        public string CPERIOD_NO
        {
            get => _cperiodNo;
            set
            {
                _cperiodNo = value;
                CPERIOD_DISPLAY = value;
            }
        }

        public string CPERIOD_DISPLAY { get; set; }

    }
}