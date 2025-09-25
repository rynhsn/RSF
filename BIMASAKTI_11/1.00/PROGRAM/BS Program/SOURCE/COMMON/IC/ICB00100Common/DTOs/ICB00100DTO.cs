using System;

namespace ICB00100Common.DTOs
{
    public class ICB00100DTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CCURRENT_PERIOD { get; set; }
        public string CPERIOD_YEAR { get; set; }
        public string CPERIOD_MONTH { get; set; }
        public bool LSOFT_CLOSING { get; set; }
        public string CSOFT_CLOSE_PRD_BY { get; set; }
        public DateTime DSOFT_CLOSE_DATE { get; set; }
    }
}