using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02520
{
    public class GSM02520DetailDTO
    {
        public string CFLOOR_ID { get; set; } = "";
        public string CFLOOR_NAME { get; set; } = "";
        public string CDESCRIPTION { get; set; } = "";
        public decimal NGROSS_AREA_SIZE { get; set; } = 0;
        public string CDEFAULT_UNIT_TYPE_ID { get; set; } = "";
        public string CDEFAULT_UNIT_CATEGORY_ID { get; set; } = "";
        public decimal NTOTAL_UNIT { get; set; } = 0;
        public decimal NTOTAL_ACTIVE_UNIT { get; set; } = 0;
        public decimal NTOTAL_UNIT_SPACE { get; set; } = 0;
        public decimal NTOTAL_EMPTY_SPACE { get; set; } = 0;
        public bool LACTIVE { get; set; } = true;
        public string CUPDATE_BY { get; set; } = "";
        public DateTime? DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
    }
}
