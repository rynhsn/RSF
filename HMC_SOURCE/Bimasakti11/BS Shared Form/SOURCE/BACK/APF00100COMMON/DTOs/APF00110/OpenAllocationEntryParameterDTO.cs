using System;
using System.Collections.Generic;
using System.Text;

namespace APF00100COMMON.DTOs.APF00110
{
    public class OpenAllocationEntryParameterDTO
    {
        public string CALLOCATION_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CREC_ID { get; set; } = "";
        public bool LDISPLAY_ONLY { get; set; } 

    }
}
