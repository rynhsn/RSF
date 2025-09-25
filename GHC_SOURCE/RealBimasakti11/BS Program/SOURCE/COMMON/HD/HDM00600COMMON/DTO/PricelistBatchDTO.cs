using System;
using System.Collections.Generic;
using System.Text;

namespace HDM00600COMMON.DTO
{
    public class PricelistBatchDTO
    {
        public int NO { get; set; }
        public string CPRICELIST_ID { get; set; }
        public string CPRICELIST_NAME { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CUNIT { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public int IPRICE { get; set; }
        public string CDESCRIPTION { get; set; }
        public string CSTART_DATE { get; set; }
    }
}
