using System;
using System.Collections.Generic;
using System.Text;

namespace FAT00800Common.DTOs
{
    public class FAT00800UpdateTransHdStatusParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
        public string CREC_ID { get; set; } = string.Empty;
        public string CNEW_STATUS { get; set; } = string.Empty;
    }
}
