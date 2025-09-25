using System;
using System.Collections.Generic;
using System.Text;

namespace PMT05000COMMON.DTO_s
{
    public class AgreementChrgDiscDetailDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public bool LSELECTED { get; set; }
        public string CFLOOR_DISPLAY { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CUNIT_ID { get; set; }
        public string CUNIT_NAME { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CFLOOR_NAME { get; set; }
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public decimal NCHARGE_AMOUNT { get; set; }
        public decimal NCHARGE_DISCOUNT { get; set; }
        public decimal NNET_CHARGE { get; set; }
        public string CREF_NO { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CSTART_DATE { get; set; }
        public DateTime? DSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
        public DateTime? DEND_DATE { get; set; }
        public string CEXISTS_DISCOUNT_CODE { get; set; }
        public decimal NEXIST_CHARGE_DISCOUNT { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CLOI_ID { get; set; }
        public int ITOTAL_UNIT { get; set; }
        public string CALREADY_HAVE_DISCOUNT { get; set; }
        public bool LALREADY_HAVE_DISCOUNT { get; set; }
        public string CLINK_TRANS_CODE { get; set; }
        public string CLINK_DEPT_CODE { get; set; }
        public string CLINK_REF_NO { get; set; }
        public decimal NCHARGES_AMOUNT { get; set; }
        public decimal NCHARGES_DISCOUNT { get; set; }
        public decimal NNET_CHARGES { get; set; }
    }
}
