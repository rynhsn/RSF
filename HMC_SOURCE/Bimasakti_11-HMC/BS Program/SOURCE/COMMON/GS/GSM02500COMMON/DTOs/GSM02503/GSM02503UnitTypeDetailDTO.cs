using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02503
{
    public class GSM02503UnitTypeDetailDTO
    {
        public string CUNIT_TYPE_ID { get; set; } = "";
        public string CUNIT_TYPE_NAME { get; set; } = "";
        public string CDESCRIPTION { get; set; } = "";
        public decimal NGROSS_AREA_SIZE { get; set; } = 0;
        public decimal NNET_AREA_SIZE { get; set; } = 0;
        public decimal NCOMMON_AREA_SIZE { get; set; } = 0;
        public bool LACTIVE { get; set; } = true;
        public string CUPDATE_BY { get; set; } = "";
        public DateTime? DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
    }
}
