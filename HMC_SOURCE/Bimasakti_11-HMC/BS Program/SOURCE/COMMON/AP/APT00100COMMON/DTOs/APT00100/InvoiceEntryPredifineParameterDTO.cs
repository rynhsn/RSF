using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00100
{
    public class InvoiceEntryPredifineParameterDTO
    {
        public string CREC_ID { get; set; } = "";
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CTRANSACTION_CODE { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CREFERENCE_NO { get; set; } = "";
        public bool LOPEN_AS_PAGE { get; set; } 

    }
}
