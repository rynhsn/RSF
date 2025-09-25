using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON.DTOs.APT00200
{
    public class PurchaseReturnEntryPredifineParameterDTO
    {
        public string CREC_ID { get; set; } = "";
        public string CCOMPANY_ID { get; set; } = "";
        public string CTRANSACTION_CODE { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CREFERENCE_NO { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public bool LOPEN_AS_PAGE { get; set; } 

    }
}
