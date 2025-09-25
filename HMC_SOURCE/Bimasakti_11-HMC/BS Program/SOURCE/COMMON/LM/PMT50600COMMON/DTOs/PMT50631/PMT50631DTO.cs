using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON.DTOs.PMT50631
{
    public class PMT50631DTO
    {
        //lOCKING
        public string CPROPERTY_ID { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CSEQ_NO { get; set; } = "";


        public string CREC_ID { get; set; } = "";
        public string CTRX_TYPE { get; set; } = "";
        public string CADD_DEPT_CODE { get; set; } = "";
        public string CADD_DEPT_NAME { get; set; } = "";
        public string CCHARGES_ID { get; set; } = "";
        public string CCHARGES_NAME { get; set; } = "";
        public string CCHARGES_DESC { get; set; } = "";
        public decimal NADDITION_AMOUNT { get; set; } = 0;
        public decimal NLADDITION_AMOUNT { get; set; } = 0;
        public decimal NBADDITION_AMOUNT { get; set; } = 0;
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = "";
        public DateTime? DUPDATE_DATE { get; set; } 
    }
}
