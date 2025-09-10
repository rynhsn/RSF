using System;

namespace ICB00100Common.DTOs
{
    public class ICB00100SystemParamDTO
    {
        public string CPROPERTY_ID { get; set; } = "";
        public string CSOFT_PERIOD { get; set; } = "";

        public string CSOFT_PERIOD_YY { get; set; } = "";

        //parsing to int buat property ISOFT_PERIOD_YY
        public int ISOFT_PERIOD_YY { get; set; }
        public string CSOFT_PERIOD_MM { get; set; } = "";

        public string CCURRENT_PERIOD { get; set; } = "";
        public string CCURRENT_PERIOD_YY { get; set; } = "";
        public string CCURRENT_PERIOD_MM { get; set; } = "";

        public string CLSOFT_END_BY { get; set; } = "";
        public DateTime? DLSOFT_END_DATE { get; set; }

        public bool LSOFT_CLOSING_FLAG { get; set; } = false;
    }
}