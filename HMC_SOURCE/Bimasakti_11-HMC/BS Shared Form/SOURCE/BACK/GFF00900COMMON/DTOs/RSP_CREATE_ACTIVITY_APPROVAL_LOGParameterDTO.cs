using System;
using System.Collections.Generic;
using System.Text;

namespace GFF00900COMMON.DTOs
{
    public class RSP_CREATE_ACTIVITY_APPROVAL_LOGParameterDTO
    {
        public string P_CCOMPANY_ID { get; set; }
        public string P_CAPPROVAL_CODE { get; set; }
        public string P_CREFERENCE_NO { get; set; }
        public string P_CREFERENCE_INFO { get; set; }
        public DateTime P_DAPPROVAL_DATE { get; set; }
        public string P_CAPPROVAL_USER_ID { get; set; }
        public string P_CACTIVITY_USER_ID { get; set; }
        public string P_CREASON_CODE { get; set; }
        public string P_CAPPROVAL_NOTE { get; set; }
    }
}
