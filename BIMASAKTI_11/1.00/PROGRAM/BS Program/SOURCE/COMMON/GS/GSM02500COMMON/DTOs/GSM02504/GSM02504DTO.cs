using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02504
{
    public class GSM02504DTO
    {
        public string CUNIT_VIEW_ID { get; set; } = "";      
        public string CUNIT_VIEW_NAME { get; set; } = "";
        public string CUPDATE_BY { get; set; } = "";
        public DateTime? DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
    }
}
