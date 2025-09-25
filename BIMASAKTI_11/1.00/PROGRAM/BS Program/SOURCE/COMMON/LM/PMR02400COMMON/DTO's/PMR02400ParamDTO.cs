using System;
using System.Collections.Generic;
using System.Text;

namespace PMR02400COMMON.DTO_s
{
    public class PMR02400ParamDTO : PMR02400SpParamDTO
    {
        public string CPROPERTY_NAME { get; set; }
        public string CUSER_ID { get; set; }
        //public string CREPORT_CULTURE { get; set; }
        public string CREPORT_TYPE_DISPLAY { get; set; }
        public string CFROM_CUSTOMER_NAME { get; set; }
        public string CTO_CUSTOMER_NAME { get; set; }
        public string CBASED_ON_DISPLAY { get; set; }
        public string CREPORT_CURRENCY_TYPE_DISPLAY { get; set; }
        public string CCUSTOMER_DISPLAY=> CFR_CUSTOMER != CTO_CUSTOMER
            ? $"{CFROM_CUSTOMER_NAME} ({CFR_CUSTOMER}) - {CTO_CUSTOMER_NAME} ({CTO_CUSTOMER})"
            : $"{CFROM_CUSTOMER_NAME} ({CFR_CUSTOMER})";
        public bool LIS_BASED_ON_CUTOFF { get; set; }
        public string CREPORT_OPTION_TEXT { get; set; }

        //save as
        public bool LIS_PRINT { get; set; } 
        public string CREPORT_FILENAME { get; set; }
        public string CREPORT_FILETYPE { get; set; }
    }
}
