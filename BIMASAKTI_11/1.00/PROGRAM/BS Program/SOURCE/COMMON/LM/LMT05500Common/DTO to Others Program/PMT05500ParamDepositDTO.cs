using System;
using System.Collections.Generic;
using System.Text;

namespace PMT05500COMMON
{
    public class PMT05500ParamDepositDTO
    {
        public string CCHEQUE_ID { get; set; } = "";
        public string PARAM_CALLER_ID { get; set; } = "";
        public string PARAM_CALLER_TRANS_CODE { get; set; } = "";
        public string PARAM_CALLER_REF_NO { get; set; } = ""; //send from deposit field
        public string PARAM_CALLER_ACTION { get; set; } = "";
        public string PARAM_DEPT_CODE { get; set; } = ""; //OKE
        public string PARAM_REF_NO { get; set; } = ""; //ini didapatkan saat data sudah dibuat alias id punya data yg sudah dibuat
        public string PARAM_DOC_NO { get; set; } = "";
        public string? PARAM_DOC_DATE { get; set; } = "";
        public string PARAM_DESCRIPTION { get; set; } = "";
        public string PARAM_GLACCOUNT_NO { get; set; } = "";
        public string PARAM_CENTER_CODE { get; set; } = "";
        public string PARAM_CASH_FLOW_GROUP_CODE { get; set; } = "";
        public string PARAM_CASH_FLOW_CODE { get; set; } = "";
        public decimal PARAM_AMOUNT { get; set; } = 0;

        //To GLT00100
        public string PARAM_CURRENCY_CODE { get; set; } = "";

        public decimal PARAM_LC_BASE_RATE { get; set; } //belum ada fieldnya
        public decimal PARAM_LC_RATE { get; set; } //belum ada fieldnya
        public decimal PARAM_BC_BASE_RATE { get; set; } //belum ada fieldnya
        public decimal PARAM_BC_RATE { get; set; } //belum ada fieldnya

        public string PARAM_BSIS { get; set; } = "";
        public string PARAM_DBCR { get; set; } = "";

        //Param name
        //CR 27/08/2024 for cal
        public string PARAM_GLACCOUNT_NAME { get; set; } = "";
        public string PARAM_CENTER_NAME { get; set; } = "";
        public string PARAM_DEPT_NAME { get; set; } = "";

        //CR 29/08/2024 for Call 50600 (btn Credit Note) //KHUSUS DAH GW BUATIN HANYA UNTUK DEV ERC 
        public string CPROPERTY_ID { get; set; } = "";
        public string PARAM_PROPERTY_ID { get; set; } = "";
        public string PARAM_CUSTOMER_ID { get; set; } = "";
        public string PARAM_CUSTOMER_NAME { get; set; } = "";
        public string PARAM_CUSTOMER_TYPE_NAME { get; set; } = "";
        public string PARAM_CURRENCY { get; set; } = "";

        public decimal NLBASE_RATE { get; set; }
        public decimal NLCURRENCY_RATE { get; set; }
        public decimal NBBASE_RATE { get; set; }
        public decimal NBCURRENCY_RATE { get; set; }

        public decimal PARAM_LBASE_RATE { get; set; }
        public decimal PARAM_LCURRENCY_RATE { get; set; }
        public decimal PARAM_BBASE_RATE { get; set; }
        public decimal PARAM_BCURRENCY_RATE { get; set; }
        public bool PARAM_TAXABLE { get; set; }
        public string PARAM_TAX_ID { get; set; } = "";
        public string PARAM_TAX_NAME { get; set; } = "";
        public decimal PARAM_TAX_PCT { get; set; }
        public decimal PARAM_TAX_BRATE { get; set; }
        public decimal PARAM_TAX_CURR_RATE { get; set; }
        public decimal PARAM_NDEPOSIT_AMOUNT { get; set; }

        public string CLINK_FLOW_ID { get; set; } = "";
        public string PARAM_SERVICE_ID { get; set; } = "";
        public string PARAM_SERVICE_NAME { get; set; } = "";
        public string PARAM_ITEM_NOTES { get; set; } = "";
        public string PARAM_TERM { get; set; } = "";
        public string PARAM_REC_ID { get; set; } = "";
        public string PARAM_CHARGE_TYPE_ID { get; set; } = "";
        public string PARAM_SEQ_NO { get; set; } = "";
    }
}
