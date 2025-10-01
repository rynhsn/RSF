using System;
using System.Collections.Generic;
using System.Text;

namespace APB00200COMMON.DTO_s
{
    public class ClosePeriodDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CCURRENT_PERIOD { get; set; }
        public string CPERIOD_YEAR { get; set; }
        public string CPERIOD_MONTH { get; set; }
        public string CSTART_PERIOD { get; set; }
        public string CSOFT_PERIOD { get; set; }
        public bool LSOFT_CLOSING_FLAG { get; set; }
        public string CSOFT_CLOSING_BY { get; set; }
        public DateTime DSOFT_CLOSING_DATE { get; set; }
        public string CLSOFT_END_BY { get; set; }
        public DateTime DLSOFT_END_DATE { get; set; }
        public DateTime DCLOSING_DATE { get; set; }
        public string CCLOSE_END_BY { get; set; }
    }
}
