using System;
using System.Collections.Generic;
using System.Text;

namespace GST00500Common
{
    public class GST00500ParamToOthersProgramDTO
    {
        public string? CCOMPANY_ID { get; set; } = "";
        public string? CPROPERTY_ID { get; set; } = "";
        public string? CTRANS_CODE { get; set; } = "";
        public string? CDEPT_CODE { get; set; } = "";
        public string? CREF_NO { get; set; } = "";
        public string CPROGRAM_ID { get; set; } = "";
        public string? CALLER_ACTION { get; set; } = "";
        public string? CTENANT_ID { get; set; } = "";
        public string? CPROGRAM_ACCESS_ID { get; set; } = "";
    }
}
