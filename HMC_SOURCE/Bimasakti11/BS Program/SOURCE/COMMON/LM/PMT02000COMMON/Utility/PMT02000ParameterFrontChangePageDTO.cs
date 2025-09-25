using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02000COMMON.Utility
{
    public class PMT02000ParameterFrontChangePageDTO
    {
        public string? CCOMPANY_ID { get; set; } = "";
        public string? CPROPERTY_ID { get; set; } = "";
        public string? CTRANS_CODE { get; set; } = "";
        public string? CDEPT_CODE { get; set; } = "";
        public string? CREF_NO { get; set; } = "";
        public string CPROGRAM_ID { get; set; } = "";
        public string? CALLER_ACTION { get; set; } = "";
        public string? CSAVEMODE { get; set; } = "";
    }
}
