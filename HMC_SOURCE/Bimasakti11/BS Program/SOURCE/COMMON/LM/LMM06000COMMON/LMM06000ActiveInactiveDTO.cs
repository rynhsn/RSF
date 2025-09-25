using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMM06000COMMON
{
    public class LMM06000ActiveInactiveDTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string PROPERTY_ID { get; set; }
        public string CUNIT_TYPE_ID { get; set; }
        public string CBILLING_RULE_CODE { get; set; }
        public bool LACTIVE { get; set; }
        public string CUSER_ID { get; set; }
    }
}
