using GSM00300COMMON.DTO_s;
using GSM00300COMMON.DTO_s.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace GSM00300COMMON.DTO_s
{
    public class CompanyParamDTO : GeneralParamDTO
    {
        public bool LPRIMARY_ACCOUNT { get; set; }
        public string CBASE_CURRENCY_CODE { get; set; }
        public string CLOCAL_CURRENCY_CODE { get; set; }
        public string CCENTER_BY { get; set; }
        public bool LCASH_FLOW { get; set; }
    }
}
