using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace PMM07500COMMON.DTO_s
{
    public class CurrencyDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CCURRENCY_DISPLAY { get; set; }
        public string CCURRENCY_SYMBOL { get; set; }
        public string CCURRENCY_NAME { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CUSER_ID { get; set; }
    }
}
