using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02541
{
    public class GSM02541DetailDTO
    {
        public string CUNIT_PROMOTION_TYPE_ID { get; set; } = "";
        public string CUNIT_PROMOTION_TYPE_NAME { get; set; } = "";
        public string CBUILDING_ID { get; set; } = "";
        public string CBUILDING_NAME { get; set; } = "";
        public string CFLOOR_ID { get; set; } = "";
        public string CFLOOR_NAME { get; set; } = "";
        public string CUNIT_PROMOTION_ID { get; set; } = "";
        public string CUNIT_PROMOTION_NAME { get; set; } = "";
        public string CDESCRIPTION { get; set; } = "";
        public string CLOCATION { get; set; } = "";
        public bool LACTIVE { get; set; } = true;
        public string CUPDATE_BY { get; set; } = "";
        public DateTime? DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
    }
}
