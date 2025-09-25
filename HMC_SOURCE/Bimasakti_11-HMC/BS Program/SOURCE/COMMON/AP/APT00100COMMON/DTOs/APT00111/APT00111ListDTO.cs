using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00111
{
    public class APT00111ListDTO
    {
        public string CREC_ID { get; set; }
        public string CSEQ_NO { get; set; }
        public string CPROD_TYPE_NAME { get; set; }
        public string CSUP_PRODUCT_ID { get; set; }
        public string CSUP_PRODUCT_NAME { get; set; }
        public string CBILL_UNIT { get; set; }
        public decimal NBILL_UNIT_QTY{ get; set; }
        public decimal NUNIT_PRICE { get; set; }
        public decimal NAMOUNT { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; } = DateTime.Now;
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; } = DateTime.Now;
    }
}
