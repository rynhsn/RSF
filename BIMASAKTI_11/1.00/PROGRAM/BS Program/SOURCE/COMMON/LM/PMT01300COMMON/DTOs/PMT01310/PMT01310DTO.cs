using System;

namespace PMT01300COMMON
{
    public class PMT01310DTO
    {
        public DateTime? DSTART_DATE { get; set; }

        public string CACTION { get; set; }
        public string CTRANS_STATUS_LOI { get; set; }
        public string CLEASE_MODE { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CCHARGE_MODE { get; set; }
        public string CCHARGE_MODE_DESCR { get; set; }
        public string CUNIT_ID { get; set; }
        public string CUNIT_NAME { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CFLOOR_NAME { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CBUILDING_NAME { get; set; }
        public decimal NACTUAL_AREA_SIZE { get; set; }
        public decimal NCOMMON_AREA_SIZE { get; set; }
        public decimal NGROSS_AREA_SIZE { get; set; }
        public decimal NNET_AREA_SIZE { get; set; }
        public string CUNIT_TYPE_ID { get; set; }
        public string CUNIT_TYPE_NAME { get; set; }

        public decimal NTOTAL_NET_AREA { get; set; }
        public decimal NTOTAL_GROSS_AREA { get; set; }
        public bool LSINGLE_UNIT { get; set; }

        public string CSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
        public string CCURRENCY_CODE { get; set; }

        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
