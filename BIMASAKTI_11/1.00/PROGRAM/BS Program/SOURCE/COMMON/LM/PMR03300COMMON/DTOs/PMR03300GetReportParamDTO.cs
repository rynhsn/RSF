using System;
using System.Collections.Generic;
using System.Text;

namespace PMR03300COMMON.DTOs
{
    public class PMR03300GetReportParamDTOz
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CFR_PERIOD { get; set; }
        public string CTO_PERIOD { get; set; }
        public string CCURRENCY_TYPE { get; set; }
        public string CFILTER_BY { get; set; }
        public string CFR_CODE { get; set; }
        public string CTO_CODE { get; set; }
        public bool LSUPPRESS { get; set; }
        public string CLANGUAGE_ID { get; set; }
    }
}
