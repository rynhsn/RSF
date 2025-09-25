using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02501
{
    public class GSM02501ParameterDTO
    {
        public GSM02501DetailDTO Data { get; set; }
        public string CCOMPANY_ID { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
    }
}
