using System;
using System.Collections.Generic;
using System.Text;

namespace PMI00300COMMON.DTO
{
    public class PMI00300GetGSBCodeListParameterDTO
    {
        public string CAPPLICATION { get; set; } = string.Empty;
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CCLASS_ID { get; set;} = string.Empty;
        public string CLANGUAGE_ID { get; set; } = string.Empty;
    }
}
