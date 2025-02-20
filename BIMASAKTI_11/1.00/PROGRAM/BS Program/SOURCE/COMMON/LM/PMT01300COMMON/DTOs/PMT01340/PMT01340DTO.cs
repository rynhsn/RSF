using System;
using System.Collections.Generic;

namespace PMT01300COMMON
{
    public class PMT01340DTO
    {
        //Parameter
        public string CACTION { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CUNIT_ID { get; set; }
        public string CFLOOR_ID { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CCHARGE_MODE { get; set; }

        //List
        public string CDEPT_NAME { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CTRANSACTION_NAME { get; set; }
        public string CSEQ_NO { get; set; }
        public bool LCONTRACTOR { get; set; }
        public bool LTAXABLE { get; set; }
        public string CCONTRACTOR_ID { get; set; }
        public string CCONTRACTOR_NAME { get; set; }
        public string CDEPOSIT_ID { get; set; }
        public string CDEPOSIT_NAME { get; set; }
        public string CTAX_ID { get; set; }
        public string CTAX_NAME { get; set; }
        public string CDEPOSIT_DATE { get; set; }
        public DateTime? DDEPOSIT_DATE { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public decimal NDEPOSIT_AMT { get; set; }
        public decimal NPAYMENT_AMT { get; set; }
        public decimal NREMAINING_AMT { get; set; }
        public string CDESCRIPTION { get; set; }
        
        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }

        //Detail
        public string CCURRENCY_NAME { get; set; }

    }
}
