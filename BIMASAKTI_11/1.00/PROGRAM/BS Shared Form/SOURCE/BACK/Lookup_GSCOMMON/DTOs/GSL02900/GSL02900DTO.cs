using System;

namespace Lookup_GSCOMMON.DTOs
{
    public class GSL02900DTO
    {
        public string CSUPPLIER_ID { get; set; }
        public string CSUPPLIER_NAME { get; set; } 
        public bool LONETIME { get; set; }
        public string CADDRESS { get; set; }
        public string CCITY_CODE { get; set; }
        public string CCITY_NAME { get; set; }
        public string CREC_ID { get; set; }
        public string CTAX_REG_ID { get; set; }

        public string CTAX_TYPE { get; set; }
        public string CTAX_NAME { get; set; }
        public string CSTATUS { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CCATEGORY_ID { get; set; }
        public string CCATEGORY_TYPE { get; set; }
        public string CPAY_TERM_CODE { get; set; }
        public decimal NPO_BALANCE { get; set; }
        public decimal NBPO_BALANCE { get; set; }
        public decimal NDP_BALANCE { get; set; }
        public decimal NBDP_BALANCE { get; set; }
        public decimal NBALANCE { get; set; }
        public decimal NBBALANCE { get; set; }
        public decimal NLAST_PURCHASE { get; set; }
        public decimal NBLAST_PURCHASE { get; set; }
        public string CLAST_PURCHASE_DATE { get; set; }
        public DateTime? DLAST_PURCHASE_DATE { get; set; }
        public decimal NLAST_PAYMENT { get; set; }
        public decimal NBLAST_PAYMENT { get; set; }
        public string CLAST_PAYMENT_DATE { get; set; }
        public DateTime? DLAST_PAYMENT_DATE { get; set; }
        public string CTKU { get; set; }
    }

}
