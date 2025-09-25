using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02530
{
    public class GetStrataLeaseParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CLASS_ID { get; set; } = "";
        public string REC_ID_LIST { get; set; } = "";
        public string CLANGUAGE_ID { get; set; } = "";
    }
}
