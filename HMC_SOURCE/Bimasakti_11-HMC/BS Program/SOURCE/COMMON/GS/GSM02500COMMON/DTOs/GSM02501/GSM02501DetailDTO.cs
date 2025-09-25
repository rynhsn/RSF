using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02501
{
    public class GSM02501DetailDTO
    {
        public string CPROPERTY_ID { get; set; } = "";
        public string CPROPERTY_NAME { get; set; } = "";
        public string CADDRESS { get; set; } = "";
        public string CPROVINCE { get; set; } = "";
        public string CCITY { get; set; } = "";
        public string CDISTRICT { get; set; } = "";
        public string CSUBDISTRICT { get; set; } = "";
        public string CSALES_TAX_ID { get; set; } = "";
        public string CSALES_TAX_NAME { get; set; } = "";
        public decimal NTAX_PERCENTAGE { get; set; } = 0;
        public string CCURRENCY { get; set; } = "";
        public string CCURRENCY_NAME { get; set; } = "";
        public string CUOM { get; set; } = "";
        public bool LACTIVE { get; set; } = true;
        public string CUPDATE_BY { get; set; } = "";
        public DateTime? DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
    }
}
