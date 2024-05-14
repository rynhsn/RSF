using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02502Charge
{
    public class GSM02502ChargeDTO
    {
        public string CSEQUENCE { get; set; } = "";
        public string CCHARGES_TYPE { get; set; } = "";
        public string CCHARGES_ID { get; set; } = "";
        public decimal NFEE { get; set; } = 0;
        public string CDESCRIPTION { get; set; } = "";
        public string CFEE_METHOD { get; set; } = "";
        public string CINVOICE_PERIOD { get; set; } = "";
        public string CUPDATE_BY { get; set; } = "";
        public DateTime? DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
    }
}
