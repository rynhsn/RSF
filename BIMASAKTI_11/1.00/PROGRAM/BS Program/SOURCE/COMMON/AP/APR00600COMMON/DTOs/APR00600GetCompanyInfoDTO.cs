using System;
using System.Collections.Generic;
using System.Text;

namespace APR00600COMMON.DTOs
{
    public class APR00600GetCompanyInfoDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CCOMPANY_NAME { get; set; }
        public string CCOGS_METHOD { get; set; }
        public bool LENABLE_CENTER_IS { get; set; }
        public bool LENABLE_CENTER_BS { get; set; }
        public bool LPRIMARY_ACCOUNT { get; set; }
        public string CPRIMARY_CO_ID { get; set; }
        public string CBASE_CURRENCY_CODE { get; set; }
        public string CBASE_CURRENCY_NAME { get; set; }
        public string CLOCAL_CURRENCY_CODE { get; set; }
        public string CLOCAL_CURRENCY_NAME { get; set; }
        public bool LCASH_FLOW { get; set; }
        public int ITIMEZONE { get; set; }
        public string CDATETIME_NOW { get; set; }
    }
}
