using APF00100COMMON.DTOs.APF00100;
using System;
using System.Collections.Generic;
using System.Text;

namespace APF00100COMMON.DTOs.APF00110
{
    public class APF00110ParameterDTO
    {
        public APF00110DTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
        public string CALLOCATION_ID { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CVAR_TRANS_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CALLOC_DATE { get; set; } = "";
        public string CSUPPLIER_ID { get; set; } = "";
        public string CSUPPLIER_SEQ_NO { get; set; } = "";
        public string CFR_REC_ID { get; set; } = "";
        public string CFR_DEPT_CODE { get; set; } = "";
        public string CFR_TRANS_CODE { get; set; } = "";
        public string CFR_REF_NO { get; set; } = "";
        public string CFR_CURRENCY_CODE { get; set; } = "";
        public decimal NFR_AP_AMOUNT { get; set; } = 0;
        public decimal NFR_TAX_AMOUNT { get; set; } = 0;
        public decimal NFR_DISC_AMOUNT { get; set; } = 0;
        public decimal NFR_LBASE_RATE { get; set; } = 0;
        public decimal NFR_LCURRENCY_RATE { get; set; } = 0;
        public decimal NFR_BBASE_RATE { get; set; } = 0;
        public decimal NFR_BCURRENCY_RATE { get; set; } = 0;
        public decimal NFR_TAX_BASE_RATE { get; set; } = 0;
        public decimal NFR_TAX_CURRENCY_RATE { get; set; } = 0;
        public string CTO_REC_ID { get; set; } = "";
        public string CTO_DEPT_CODE { get; set; } = "";
        public string CTO_TRANS_CODE { get; set; } = "";
        public string CTO_REF_NO { get; set; } = "";
        public string CTO_CURRENCY_CODE { get; set; } = "";
        public decimal NTO_AP_AMOUNT { get; set; } = 0;
        public decimal NTO_TAX_AMOUNT { get; set; } = 0;
        public decimal NTO_DISC_AMOUNT { get; set; } = 0;
        public decimal NTO_LBASE_RATE { get; set; } = 0;
        public decimal NTO_LCURRENCY_RATE { get; set; } = 0;
        public decimal NTO_BBASE_RATE { get; set; } = 0;
        public decimal NTO_BCURRENCY_RATE { get; set; } = 0;
        public decimal NTO_TAX_BASE_RATE { get; set; } = 0;
        public decimal NTO_TAX_CURRENCY_RATE { get; set; } = 0;
        public decimal NLFOREX_GAINLOSS { get; set; } = 0;
        public decimal NBFOREX_GAINLOSS { get; set; } = 0;

        //OTHER
        public string CLANGUAGE_ID { get; set; } = "";
    }
}
