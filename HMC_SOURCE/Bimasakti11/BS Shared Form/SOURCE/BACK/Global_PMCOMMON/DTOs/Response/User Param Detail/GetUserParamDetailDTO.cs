using System;
using System.Collections.Generic;
using System.Text;

namespace Global_PMCOMMON.DTOs.User_Param_Detail
{
    public class GetUserParamDetailDTO 
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CCODE { get; set; }
        public string CDESCRIPTION { get; set; } = "";
        public bool LACTIVE { get; set; }
        public string CVALUE { get; set; } = "";
    }
}
