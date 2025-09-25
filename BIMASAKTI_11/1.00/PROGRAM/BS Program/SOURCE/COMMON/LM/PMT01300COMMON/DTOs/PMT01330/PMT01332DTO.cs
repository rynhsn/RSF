using System;
using System.Collections.Generic;

namespace PMT01300COMMON
{
    public class PMT01332DTO
    {
        //Parameter
        public string CACTION { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CCHARGE_SEQ_NO { get; set; }

        //List
        public string CREVENUE_SHARING_ID { get; set; }
        public string CSEQ_NO { get; set; }
        public decimal NMONTHLY_REVENUE_FROM { get; set; }
        public decimal NMONTHLY_REVENUE_TO { get; set; }
        public decimal NSHARE_PCT { get; set; }
    }
}
