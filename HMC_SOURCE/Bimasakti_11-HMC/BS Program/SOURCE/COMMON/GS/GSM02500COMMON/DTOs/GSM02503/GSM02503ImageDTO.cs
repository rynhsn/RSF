using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02503
{
    public class GSM02503ImageDTO
    {
        public string CIMAGE_ID { get; set; } = "";
        public string CIMAGE_NAME { get; set; } = "";
        public string CSTORAGE_ID { get; set; } = "";
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
    }
}
