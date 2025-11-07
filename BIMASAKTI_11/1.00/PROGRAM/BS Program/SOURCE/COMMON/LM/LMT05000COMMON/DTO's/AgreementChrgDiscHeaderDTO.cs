using System;
using System.Collections.Generic;
using System.Text;

namespace PMT05000COMMON.DTO_s
{
    public class AgreementChrgDiscHeaderDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CDISCOUNT_CODE { get; set; }
        public string CINV_PERIOD_YEAR { get; set; }
        public string CINV_PERIOD_MONTH { get; set; }
        public bool LALL_BUILDING { get; set; }
        public string CBUILDING_ID { get; set; }
        public string CAGREEMENT_TYPE { get; set; }
        public string CACTION { get; set; }
        public string CUSER_ID { get; set; }
    }
}
