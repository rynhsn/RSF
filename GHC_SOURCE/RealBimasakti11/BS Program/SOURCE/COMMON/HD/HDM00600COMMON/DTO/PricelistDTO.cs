using HDM00600COMMON.DTO.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace HDM00600COMMON.DTO
{
    public class PricelistDTO : GeneralParamDTO
    {
        public string CPRICELIST_ID { get; set; }
        public string CPRICELIST_NAME { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CUNIT { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public int IPRICE { get; set; }
        public string CDESCRIPTION { get; set; }
        public string CVALID_ID { get; set; }
        public string CSTART_DATE { get; set; }
        public DateTime? DSTART_DATE { get; set; }
        public string CACTION { get; set; }
        public string CINVGRP_CODE { get; set; }
        public bool LTAXABLE { get; set; }
        public string CACTIVE_START_DATE { get; set; }
        public string CACTIVE_END_DATE { get; set; }
    }
}
