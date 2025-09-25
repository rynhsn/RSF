using System;
using System.Collections.Generic;
using System.Text;

namespace GSM00300COMMON.DTO_s
{
    public class CompanyParamRecordDTO : CompanyParamDTO
    { 
        public string CCOGS_METHOD { get; set; }
        public string CPRIMARY_CO_ID { get; set; }
        public string CPRIMARY_CO_NAME { get; set; }
        public string CBASE_CURRENCY_CODE { get; set; }
        public string CBASE_CURRENCY_NAME { get; set; }
        public string CLOCAL_CURRENCY_CODE { get; set; }
        public string CLOCAL_CURRENCY_NAME { get; set; }
        public string CCENTER_BY { get; set; }
        public bool LCASH_FLOW { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }
}
