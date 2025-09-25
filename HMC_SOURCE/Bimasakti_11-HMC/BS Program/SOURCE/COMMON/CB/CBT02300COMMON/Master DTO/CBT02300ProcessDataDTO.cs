using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02300COMMON
{
    public class CBT02300ProcessDataDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CUSER_ID { get; set; }
        public string? CACTION { get; set; }
        public string? CPROCESS_DATE { get; set; }
        public string CREASON { get; set; } = "";
        public List<string>? CREC_ID_LIST { get; set; }
    }
}