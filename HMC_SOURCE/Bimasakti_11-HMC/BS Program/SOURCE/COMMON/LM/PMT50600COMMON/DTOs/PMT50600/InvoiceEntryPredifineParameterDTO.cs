using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace PMT50600COMMON.DTOs.PMT50600
{
    public class InvoiceEntryPredifineParameterDTO
    {

        public string CCHEQUE_ID { get; set; } = "";
        //public string CPROPERTY_ID { get; set; } = "";
        public string CREC_ID { get; set; } = "";
        public string CCOMPANY_ID { get; set; } = "";
        //public string PARAM_REF_NO { get; set; } = "";
        //public string PARAM_DOC_NO { get; set; } = "";
        //public string PARAM_DOC_DATE { get; set; } = "";
        //public string PARAM_DESCRIPTION { get; set; } = "";
        //public string PARAM_GLACCOUNT_NO { get; set; } = "";
        //public string PARAM_CENTER_CODE { get; set; } = "";
        //public string PARAM_CASH_FLOW_GROUP_CODE { get; set; } = "";
        //public string PARAM_CASH_FLOW_CODE { get; set; } = "";
        //public decimal PARAM_AMOUNT { get; set; } = 0;

        //public string CTRANSACTION_CODE { get; set; } = "";
        //public string CDEPT_CODE { get; set; } = "";
        //public string CREFERENCE_NO { get; set; } = "";
        public bool LOPEN_AS_PAGE { get; set; } = true;


        //CR05
        public string PARAM_CALLER_ID { get; set; } = "";
        public string PARAM_CALLER_TRANS_CODE { get; set; } = "";
        public string PARAM_CALLER_REF_NO { get; set; } = "";
        public string PARAM_CALLER_ACTION { get; set; } = "";
        public string PARAM_PROPERTY_ID { get; set; } = "";
        public string PARAM_DEPT_CODE { get; set; } = "";
        public string PARAM_DEPT_NAME { get; set; } = "";
        public string PARAM_CUSTOMER_ID { get; set; } = "";
        public string PARAM_CUSTOMER_NAME { get; set; } = "";
        public string PARAM_CUSTOMER_TYPE_NAME { get; set; } = "";
        public string PARAM_DOC_NO { get; set; } = "";
        public string? PARAM_DOC_DATE { get; set; } 
        public string PARAM_CURRENCY { get; set; } = "";
        public string PARAM_DESCRIPTION { get; set; } = "";
        public decimal PARAM_LBASE_RATE { get; set; } = 0;
        public decimal PARAM_LCURRENCY_RATE { get; set; } = 0;
        public decimal PARAM_BBASE_RATE { get; set; } = 0;
        public decimal PARAM_BCURRENCY_RATE { get; set; } = 0;
        public bool PARAM_TAXABLE { get; set; } = false;
        public string PARAM_TAX_ID { get; set; } = "";
        public string PARAM_TAX_NAME { get; set; } = "";
        public decimal PARAM_TAX_PCT { get; set; } = 0;
        public decimal PARAM_TAX_BRATE { get; set; } = 0;
        public decimal PARAM_TAX_CURR_RATE { get; set; } = 0;
        public string PARAM_SERVICE_ID { get; set; } = "";
        public string PARAM_SERVICE_NAME { get; set; } = "";
        public string PARAM_ITEM_NOTES { get; set; } = "";
    }
}
