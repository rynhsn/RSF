using System;
using System.Collections.Generic;
using System.Text;

namespace PMR02000COMMON.DTO_s.Print
{
    public class ReportParamDTO : PMR02000SpParamDTO
    {
        public string CPROPERTY_NAME { get; set; }
        public string CREPORT_CULTURE { get; set; }
        public string CPERIOD_DISPLAY { get; set; }

        public string CTO_CUSTOMER_NAME { get; set; }
        public string CFROM_CUSTOMER_NAME { get; set; }

        public string CFROM_JRNGRP_NAME { get; set; }
        public string CTO_JRNGRP_NAME { get; set; }

        public string CFR_DEPT_NAME { get; set; }
        public string CTO_DEPT_NAME { get; set; }
        public string CDATA_BASED_ON_DISPLAY { get; set; }
        public string CREMAINING_BASED_ON_DISPLAY { get; set; }
        public string CREMAINING_BASED_ON_TEXT { get; set; }
        public DateTime DDATE_CUTOFF { get; set; }
        public string CREPORT_TYPE_DISPLAY { get; set; }
        public string CJRNGRP_DISPLAY { get; set; }
        public string CCUSTOMER_DISPLAY { get; set; }
        public string CDEPT_DISPLAY { get; set; }
        public bool LIS_PRINT { get; set; }
        public string CREPORT_FILENAME { get; set; }
        public string CREPORT_FILETYPE { get; set; }

        public bool IS_TRANS_CURRENCY { get; set; }
        public bool IS_LOCAL_CURRENCY { get; set; }
        public bool IS_BASE_CURRENCY { get; set; }
        public bool IS_DEPT_FILTER_ENABLED { get; set; }
        public bool IS_TRANSTYPE_FILTER_ENABLED { get; set; }
        public bool IS_CUSTCTG_FILTER_ENABLED { get; set; }

        public string CUSTCTG_FILTER_DISPLAY { get; set; }
        public string CTRANSTYPE_FILTER_DISPLAY { get; set; }
    }
}
