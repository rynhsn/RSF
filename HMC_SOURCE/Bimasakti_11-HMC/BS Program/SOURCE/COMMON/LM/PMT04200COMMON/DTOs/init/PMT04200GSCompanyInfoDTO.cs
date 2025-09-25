using System;
using System.Collections.Generic;
using System.Text;

namespace PMT04200Common.DTOs
{
    public class PMT04200GSCompanyInfoDTO
    {
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
    }
}
