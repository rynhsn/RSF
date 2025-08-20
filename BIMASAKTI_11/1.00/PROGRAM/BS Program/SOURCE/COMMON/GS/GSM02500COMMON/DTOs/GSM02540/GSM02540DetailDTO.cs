using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02540
{
    public class GSM02540DetailDTO
    {
        public string COTHER_UNIT_TYPE_ID { get; set; } = "";
        public string COTHER_UNIT_TYPE_NAME { get; set; } = "";
        public string CDESCRIPTION { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CDEPT_NAME { get; set; } = "";
        public string CPROPERTY_TYPE { get; set; } = "";
        public decimal NGROSS_AREA_SIZE { get; set; } = 0;
        public decimal NNET_AREA_SIZE { get; set; } = 0;
        public bool LACTIVE { get; set; } = true;
        public bool LEVENT { get; set; } = false;
        public string CUPDATE_BY { get; set; } = "";
        public DateTime? DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }

        public void Clear()
        {
            COTHER_UNIT_TYPE_ID = "";
            COTHER_UNIT_TYPE_NAME = "";
            CDESCRIPTION = "";
            CDEPT_CODE = "";
            CDEPT_NAME = "";
            NGROSS_AREA_SIZE = 0;
            NNET_AREA_SIZE = 0;
            LACTIVE = true;
            LEVENT = false;
            CUPDATE_BY = "";
            DUPDATE_DATE = null;
            CCREATE_BY = "";
            DCREATE_DATE = null;
        }
    }
}
