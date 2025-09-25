using System;
using System.Collections.Generic;
using System.Text;

namespace Lookup_PMCOMMON.DTOs.GET_USER_PARAM_DETAIL
{
    public class GET_USER_PARAM_DETAILDTO
    {
        public string? CCOMPANY_ID { get; set; }
        public string? CCODE { get; set; }
        public string CDESCRIPTION { get; set; } = "";
        public bool LACTIVE { get; set; }
        public string CVALUE { get; set; } = "";
    }
}
