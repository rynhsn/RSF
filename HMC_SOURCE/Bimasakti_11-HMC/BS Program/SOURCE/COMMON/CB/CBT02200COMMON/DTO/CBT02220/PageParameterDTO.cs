using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON.DTO.CBT02220
{
    public class PageParameterDTO
    {
        public string CCHEQUE_ID { get; set; } = "";
        public string PARAM_CALLER_ID { get; set; } = "";
        public string PARAM_CALLER_TRANS_CODE { get; set; } = "";
        public string PARAM_CALLER_REF_NO { get; set; } = "";
        public string PARAM_CALLER_ACTION { get; set; } = "";
        public string PARAM_DEPT_CODE { get; set; } = "";
        public string PARAM_REF_NO { get; set; } = "";
        public string PARAM_DOC_NO { get; set; } = "";
        public string PARAM_DOC_DATE { get; set; } = "";
        public string PARAM_DESCRIPTION { get; set; } = "";
        public string PARAM_GLACCOUNT_NO { get; set; } = "";
        public string PARAM_CENTER_CODE { get; set; } = "";
        public string PARAM_CASH_FLOW_GROUP_CODE { get; set; } = "";
        public string PARAM_CASH_FLOW_CODE { get; set; } = "";
        public decimal PARAM_AMOUNT { get; set; } = 0;
    }
}
