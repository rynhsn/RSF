using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PMM07500COMMON.DTO_s.stamp_code
{
    public class PMM07500GridDTO : PMM07500SaveDTO
    {
        public string CCURRENCY_NAME { get; set; }
        public string CCURRENCY_DISPLAY { get; set; }
        public decimal NMIN_AMT { get; set; }
        public decimal NSTAMP_AMT { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CLANGUAGE_ID { get; set; }
    }
}
