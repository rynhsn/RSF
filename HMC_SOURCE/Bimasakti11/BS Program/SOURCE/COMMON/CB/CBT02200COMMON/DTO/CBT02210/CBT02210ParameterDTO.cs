using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON.DTO.CBT02210
{
    public class CBT02210ParameterDTO
    {
        public CBT02210DTO Data { get; set; }
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CACTION { get; set; } = "";
        public string CLANGUAGE_ID { get; set; } = "";

        //DIFFERENTIATE
        public bool LPAGE { get; set; } = false;

        //PAGE
        public string CCALLER_TRANS_CODE { get; set; } = "";
        public string CCALLER_REF_NO { get; set; } = "";
        public string CCALLER_ID { get; set; } = "";
        public string CGLACCOUNT_NO { get; set; } = "";
        public string CCENTER_CODE { get; set; } = ""; 
        public string CCASH_FLOW_GROUP_CODE { get; set; } = "";
        public string CCASH_FLOW_CODE { get; set; } = "";
    }
}
