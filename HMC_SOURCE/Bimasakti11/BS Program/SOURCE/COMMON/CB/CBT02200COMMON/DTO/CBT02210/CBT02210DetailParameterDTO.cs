using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON.DTO.CBT02210
{
    public class CBT02210DetailParameterDTO
    {
        public CBT02210DetailDTO Data { get; set; }
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CREF_DATE { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public string CACTION { get; set; } = "";
        public string CCHEQUE_ID { get; set; } = "";
        public string CLANGUAGE_ID { get; set; } = "";
    }
}
