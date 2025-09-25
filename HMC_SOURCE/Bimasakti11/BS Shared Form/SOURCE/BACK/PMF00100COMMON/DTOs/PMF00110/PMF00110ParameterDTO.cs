using PMF00100COMMON.DTOs.PMF00100;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMF00100COMMON.DTOs.PMF00110
{
    public class PMF00110ParameterDTO
    {
        public PMF00110DTO Data { get; set; }
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
        public string CTENANT_ID { get; set; } = "";
        //public string CSUPPLIER_SEQ_NO { get; set; } = "";
        public string CFR_REC_ID { get; set; } = "";
        public string CFR_DEPT_CODE { get; set; } = "";
        public string CFR_TRANS_CODE { get; set; } = "";
        public string CFR_REF_NO { get; set; } = "";
        public string CFR_CURRENCY_CODE { get; set; } = "";
        public decimal NFR_AR_AMOUNT { get; set; } = 0;
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
        public decimal NTO_AR_AMOUNT { get; set; } = 0;
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
        public string CCALLER_TRANS_CODE { get; set; } = "";
    }
}
