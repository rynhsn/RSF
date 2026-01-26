using System;
using System.Collections.Generic;
using System.Text;

namespace PMT06000Common.DTOs
{
    public class PMT06000GetSystemParamResultDTO
    {
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CCUR_RATETYPE_CODE { get; set; }
        public string CCUR_RATETYPE_DESCRIPTION { get; set; }
        public string CTAX_RATETYPE_CODE { get; set; }
        public string CTAX_RATETYPE_DESCRIPTION { get; set; }
        public bool LBACKDATE { get; set; }
        public bool LGLLINK { get; set; }
        public string CSOFT_PERIOD { get; set; }
        public DateTime DSOFT_PERIOD_DATE { get; set; } // addtional to display date
        public string CSOFT_PERIOD_YY { get; set; }
        public string CSOFT_PERIOD_MM { get; set; }
        public string CLSOFT_END_BY { get; set; }
        public DateTime DLSOFT_END_DATE { get; set; }
        public string CCURRENT_PERIOD { get; set; }
        public string CCURRENT_PERIOD_YY { get; set; }
        public string CCURRENT_PERIOD_MM { get; set; }
        public bool LPRD_END_FLAG { get; set; }
        public string CPCPRD_END_BY { get; set; }
        public string CLPRD_END_BY { get; set; }
        public DateTime DLPRD_END_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public bool LALLOW_EDIT_GLLINK { get; set; }
        public string CWHT_MODE { get; set; }
        public Decimal NRECEIPT_CR_ADJ_AMT { get; set; }
        public Decimal NRECEIPT_DB_ADJ_AMT { get; set; }
        public string CRECEIPT_CR_ADJ_CHARGES_ID { get; set; }
        public string CRECEIPT_DB_ADJ_CHARGES_ID { get; set; }
        public bool LINV_PROCESS_FLAG { get; set; }
        public string CELECTRIC_PERIOD { get; set; }
        public string CELECTRIC_DATE { get; set; }
        public bool LELECTRIC_END_MONTH { get; set; }
        public string CWATER_PERIOD { get; set; }
        public string CWATER_DATE { get; set; }
        public bool LWATER_END_MONTH { get; set; }
        public string CGAS_PERIOD { get; set; }
        public string CGAS_DATE { get; set; }
        public bool LGAS_END_MONTH { get; set; }
        public string CCURRENCY { get; set; }
        public string CPAY_ID { get; set; }
        public int IMAX_DAYS { get; set; }
        public int IMAX_ATTEMPTS { get; set; }
        public string CCALL_TYPE_ID { get; set; }
        public bool LALL_BUILDING { get; set; }
        public bool LPRIORITY { get; set; }
        public string COL_PAY_START_DATE { get; set; }
        public string COL_PAY_CURRENCY { get; set; }
        public string COL_PAY_SUBMIT_BY { get; set; }
        public bool LOL_PAY_INCL_PENALTY { get; set; }
        public string CGENERATE_INV_MODE { get; set; }
    }
}
