using System;

namespace PMT05500COMMON.DTO
{
    public class LMT05500DepositListDTO : LMT05500Header
    {

        public string? CSEQ_NO { get; set; }
        public string? CDEPT_CODE { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CREF_NO { get; set; }
        public string? CCONTRACTOR_NAME { get; set; }
        public string CDEPOSIT_ID { get; set; } = "";
        public string CDEPOSIT_NAME { get; set; } = "";
        public string? CDEPOSIT_DATE { get; set; }

        public decimal NDEPOSIT_AMOUNT { get; set; }
        public decimal NREMAINING_AMOUNT { get; set; }
        public string? CINVOICE_NO { get; set; }
        public bool LPAYMENT { get; set; }
        public string? CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string? CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public DateTime? DDEPOSIT_DATE { get; set; }


        //PARAM TO ANOTHER PROGRAM Penambahan
        //others param on the top
        public string CCHARGE_MODE { get; set; } = "";
        public string CBUILDING_ID { get; set; } = "";
        public string CFLOOR_ID { get; set; } = "";
        public string CUNIT_ID { get; set; } = "";
        public string CDOC_NO { get; set; } = "";
        public string CDOC_DATE { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public string CGLACCOUNT_NO { get; set; } = "";
        public string CGLACCOUNT_NAME { get; set; } = "";
        public string CCENTER_CODE { get; set; } = "";
        public string CCENTER_NAME { get; set; } = "";
        public string CCASH_FLOW_GROUP_CODE { get; set; } = "";
        public string CCASH_FLOW_CODE { get; set; } = "";
        public string CBSIS { get; set; } = "";
        public string CDBCR { get; set; } = "";
        public string CDESCRIPTION { get; set; } = "";

        public decimal NBBASE_RATE_AMOUNT { get; set; }
        public decimal NBCURRENCY_RATE_AMOUNT { get; set; }
        public decimal NLBASE_RATE_AMOUNT { get; set; }
        public decimal NLCURRENCY_RATE_AMOUNT { get; set; }

        //CR Param for PMT50600
        public string CDEPT_NAME { get; set; } = "";
        public string CTENANT_ID { get; set; } = "";
        public string CTENANT_NAME { get; set; } = "";
        public string CCUSTOMER_TYPE_NAME { get; set; } = "";
        public bool LTAXABLE { get; set; }
        public string CTAX_ID { get; set; } = "";
        public string CTAX_NAME { get; set; } = "";
        public decimal NTAX_PCT { get; set; }
        public decimal NTAX_BASE_RATE { get; set; }
        public decimal NTAX_CURRENCY_RATE { get; set; }
        //CR 11/10/2024
        public string CFLOW_ID { get; set; } = "";
        public string CSERVICE_ID { get; set; } = "";
        public string CSERVICE_NAME { get; set; } = "";
        public string CITEM_NOTES { get; set; } = "";
        public string CPAYMENT_TERM_CODE { get; set; } = "";
        public string PARAM_REC_ID { get; set; } = "";
        public string CCHARGES_TYPE { get; set; } = "";


    }
}