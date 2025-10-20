using System;
using System.Collections.Generic;
using System.Text;

namespace PMR03400COMMON.DTO_s
{
    public class PMR03400SPParamDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CPROPERTY_ID { get; set; } = string.Empty;
        public string CFR_PERIOD { get; set; } = string.Empty;
        public string CTO_PERIOD { get; set; } = string.Empty;
        public string CCURRENCY_TYPE { get; set; } = string.Empty;
        public string CFR_CODE { get; set; } = string.Empty;
        public string CFR_CODE_NAME { get; set; } = string.Empty;
        public string CTO_CODE { get; set; } = string.Empty;
        public string CTO_CODE_NAME { get; set; } = string.Empty;
        public bool LDESC { get; set; }
        public bool LPROFORMA { get; set; }
        public string CLANGUAGE_ID { get; set; } = string.Empty;
    }
}
