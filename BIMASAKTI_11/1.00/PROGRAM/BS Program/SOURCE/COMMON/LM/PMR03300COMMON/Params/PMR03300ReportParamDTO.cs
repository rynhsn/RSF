using System;
using System.Collections.Generic;
using System.Text;

namespace PMR03300COMMON.Params
{
    public class PMR03300ReportParamDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CPROPERTY_NAME { get; set; } = "";
        public string CLANG_ID { get; set; } = "";
        public string CREPORT_CULTURE { get; set; } = "";


        public string CREPORT_FILETYPE { get; set; } = "";
        public string CREPORT_FILENAME { get; set; } = "";
        public bool LIS_PRINT { get; set; } = true;

        public string CFR_PERIOD { get; set; }
        public string CFR_PERIOD_DISPLAY { get; set; }
        public string CTO_PERIOD { get; set; }
        public string CTO_PERIOD_DISPLAY { get; set; }
        public string CCURRENCY_TYPE { get; set; }
        public string CCURRENCY_TYPE_NAME { get; set; }
        public string CFILTER_BY { get; set; }
        public string CFILTER_BY_NAME { get; set; }
        public string CFR_CODE { get; set; }
        public string CFR_CODE_NAME { get; set; }
        public string CTO_CODE { get; set; }
        public string CTO_CODE_NAME { get; set; }
        public bool LSUPPRESS { get; set; }
        public string CLANGUAGE_ID { get; set; }
        public string CYEAR { get; set; }
        public int IYEAR { get; set; }
        public string CMONTH { get; set; }
        public int IMONTH { get; set; }
        public string CMODE { get; set; }
    }
}
