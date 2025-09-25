using System;
using System.Collections.Generic;
using System.Text;

namespace PMI00300COMMON.DTO
{
    public class PMI00300GetLSStatusListParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CPROPERTY_ID { get; set; } = string.Empty;
        public string STATUS_TYPE { get; set; } = string.Empty;
        public string CUSER_ID  { get; set; } = string.Empty;
    }
}
