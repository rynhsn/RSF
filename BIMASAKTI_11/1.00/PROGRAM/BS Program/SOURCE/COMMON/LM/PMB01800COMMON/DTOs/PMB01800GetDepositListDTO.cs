using System;
using System.Collections.Generic;
using System.Text;

namespace PMB01800COMMON.DTOs
{
    public class PMB01800GetDepositListDTO
    {
        public string CDEPT { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_TYPE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CSTART_DATE { get; set; }
        public DateTime DSTART_DATE { get; set; }
        public string CEND_DATE { get; set; }
        public DateTime DEND_DATE { get; set; }
        public string CTENANT { get; set; }
        public string CDEPOSIT { get; set; }
        public string CDEPOSIT_DATE { get; set; }
        public decimal NDEPOSIT_AMOUNT { get; set; }
        public string CINVOICE_NO { get; set; }
        public string CCONTRACTOR_NAME { get; set; }
        public string CBUILDING_NAME { get; set; }
        public string CCATEGORY { get; set; }
        public string CDOC_NO { get; set; }
        public string CDOC_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUNIT_DESCRIPTION { get; set; }
        public string CSEQ_NO { get; set; }
        public bool LSELECTED { get; set; }

    }

}
