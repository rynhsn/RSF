using System;

namespace PMM00100COMMON.DTO_s
{
    public class SystemParamBillingDTO : SystemParamDTO
    {
        public bool LENABLED_ONLINE_PAYMENT { get; set; }
        public string COL_PAY_START_DATE { get; set; }
        public string COL_PAY_SUBMIT_BY { get; set; }
        public string COL_PAY_CURRENCY { get; set; }
        public string COL_PAY_CURRENCY_DESCRIPTION { get; set; }
        public bool LOL_PAY_INCL_PENALTY { get; set; }
        public string CBILLING_STATEMENT_DATE { get; set; }
        public string CBILLING_STATEMENT_TOP_CODE { get; set; }
        public string CBILLING_STATEMENT_TOP_NAME { get; set; }
        public int IBILLING_STATEMENT_DATE { get; set; }
        public DateTime? DOL_PAY_START_DATE { get; set; }
    }
}
