using System;
using System.Collections.Generic;
using System.Text;

namespace PMT05000COMMON.DTO_s
{
    public class AgreementChrgDiscDetailBulkProcessDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CUNIT_ID { get; set; }
        public string CTENANT_ID { get; set; }
        public decimal NCHARGES_AMOUNT { get; set; }
        public decimal NCHARGES_DISCOUNT { get; set; }
        public decimal NNET_CHARGES { get; set; }
        public string CLINK_TRANS_CODE { get; set; }
        public string CLINK_DEPT_CODE { get; set; }
        public string CLINK_REF_NO { get; set; }
        public string CSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
    }
}
