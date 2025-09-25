using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02550
{
    public class GSM02550DTO
    {
        public string CUSER_ID { get; set; } = "";
        public string CUSER_NAME { get; set; } = "";
        public int IUSER_LEVEL { get; set; } = 0;
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
    }
}
