using PMT50600COMMON.DTOs.PMT50600;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON.DTOs.PMT50611
{
    public class PMT50611HeaderDTO
    {
        public string CREC_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CPROPERTY_NAME { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CDEPT_NAME { get; set; } = "";
        public string CTENANT_ID { get; set; } = "";
        public string CTENANT_NAME { get; set; } = "";
        public string CCUSTOMER_TYPE_NAME { get; set; } = "";
        //public string CTENANT_SEQ_NO { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
        public DateTime DREF_DATE { get; set; }
        public string CCURRENCY_CODE { get; set; } = ""; 
        public string CCURRENCY_NAME { get; set; } = "";
        public decimal NLBASE_RATE { get; set; } = 0;
        public decimal NLCURRENCY_RATE { get; set; } = 0;
        public string CLOCAL_CURRENCY_CODE { get; set; } = "";
        public decimal NTAX_BASE_RATE { get; set; } = 0;
        public decimal NTAX_CURRENCY_RATE { get; set; } = 0;
        public bool LTAXABLE { get; set; } = false;
        public string CTAX_ID { get; set; } = "";
        public string CTAX_NAME { get; set; } = "";
        public decimal NTAX_PCT { get; set; } = 0;
        public decimal NADDITION { get; set; } = 0;
        public decimal NDEDUCTION { get; set; } = 0;
        public string CTRANS_STATUS { get; set; } = "";
    }
}
