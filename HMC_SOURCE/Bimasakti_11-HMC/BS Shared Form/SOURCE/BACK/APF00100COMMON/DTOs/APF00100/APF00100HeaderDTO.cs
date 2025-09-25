using System;
using System.Collections.Generic;
using System.Text;

namespace APF00100COMMON.DTOs.APF00100
{
    public class APF00100HeaderDTO
    {
        public string CTRANSACTION_NAME { get; set; } = "";
        public string CPROPERTY_NAME { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CDEPT_NAME { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public DateTime? DREF_DATE { get; set; }
        public string CREF_DATE { get; set; } = "";
        public string CSUPPLIER_ID { get; set; } = "";
        public string CSUPPLIER_NAME { get; set; } = "";
        public string CSUPPLIER_SEQ_NO { get; set; } = "";
        public string CDOC_NO { get; set; } = "";
        public string CDOC_DATE { get; set; } = "";
        public DateTime? DDOC_DATE { get; set; }
    }
}
