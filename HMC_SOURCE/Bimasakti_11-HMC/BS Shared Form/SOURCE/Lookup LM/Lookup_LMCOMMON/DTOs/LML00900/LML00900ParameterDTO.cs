using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs
{
    public class LML00900ParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CTENANT_ID { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CTRANS_NAME { get; set; } = "";
        public string CPERIOD { get; set; } = "";
        public bool LHAS_REMAINING { get; set; }
        public bool LNO_REMAINING { get; set; }
        public string CLANGUAGE_ID { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public string CSEARCH_TEXT { get; set; } = "";
    }
}
