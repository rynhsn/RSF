using System;
using System.Collections.Generic;
using System.Text;

namespace PMA00300COMMON
{
    public class PMA00300ReportClientParameterDTO
    {
        public string CTENANT_CUSTOMER_ID { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CREPORT_CULTURE { get; set; }
        public int IDECIMAL_PLACES { get; set; }
        public string CNUMBER_FORMAT { get; set; }
        public string CDATE_LONG_FORMAT { get; set; }
        public string CDATE_SHORT_FORMAT { get; set; }
        public string CTIME_LONG_FORMAT { get; set; }
        public string CTIME_SHORT_FORMAT { get; set; }
    }
}
