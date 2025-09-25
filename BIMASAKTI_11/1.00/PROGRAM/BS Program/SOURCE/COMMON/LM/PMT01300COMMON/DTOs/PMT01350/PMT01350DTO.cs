using System;
using System.Collections.Generic;

namespace PMT01300COMMON
{
    public class PMT01350DTO
    {
        public string CSEQ_NO { get; set; }
        public string CACTION { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CREC_ID { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGE_MODE { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CUNIT_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CCHARGES_SEQ_NO { get; set; }
        public string CREVENUE_SHARING_ID { get; set; }
        public decimal NSALES_AMOUNT { get; set; }
        public decimal NDISCOUNT { get; set; }
        public string CSTART_DATE { get; set; }
        public DateTime? DSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
        public DateTime? DEND_DATE { get; set; }
        public string CINV_PERIOD { get; set; }
        public string CINV_PERIOD_Display { get; set; }
        public decimal NAMOUNT { get; set; }
        public decimal NTAX_AMOUNT { get; set; }
        public decimal NAFTER_TAX_AMOUNT { get; set; }
        public decimal NBOOKING_AMOUNT { get; set; }
        public decimal NTOTAL_AMOUNT { get; set; }
        public string CSTATUS_DESCR { get; set; }

        public bool LINVOICED { get; set; }
        public string CINV_NO { get; set; }
        public bool LPAYMENT { get; set; }
        public string CPAYMENT_NO { get; set; }
        public decimal NPAYMENT_AMOUNT { get; set; }
        public string CDESCRIPTION { get; set; }

        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }


    }
}
