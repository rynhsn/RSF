using System;
using System.Collections.Generic;
using System.Text;

namespace APR00100COMMON.DTO_s.Print
{
    public class ReportParamDTO : APR00100SpParamDTO
    {
        public string CPROPERTY_NAME { get; set; }
        public string CREPORT_CULTURE { get; set; }
        public string CUSER_ID { get; set; }

        public string CTO_SUPPLIER_NAME { get; set; }
        public string CFROM_SUPPLIER_NAME { get; set; }

        public string CFROM_JRNGRP_NAME { get; set; }
        public string CTO_JRNGRP_NAME { get; set; }

        public string CFROM_DEPT_NAME { get; set; }
        public string CTO_DEPT_NAME { get; set; }

        public string CDATE_BASED_ON_DISPLAY { get; set; }
        public string CREMAINING_BASED_ON_DISPLAY { get; set; }
        public string CREPORT_TYPE_DISPLAY { get; set; }
        public string CSORT_BY_DISPLAY { get; set; }

        public string CJRNGRP_DISPLAY => CFROM_JRNGRP_NAME != CTO_JRNGRP_NAME
            ? $"{CFROM_JRNGRP_NAME} ({CFROM_JRNGRP_CODE}) - {CTO_JRNGRP_NAME} ({CTO_JRNGRP_CODE})"
            : $"{CFROM_JRNGRP_NAME} ({CFROM_JRNGRP_CODE})";

        public string CSUPPLIER_DISPLAY => CFROM_SUPPLIER_ID != CTO_SUPPLIER_ID
            ? $"{CFROM_SUPPLIER_NAME} ({CFROM_SUPPLIER_ID}) - {CTO_SUPPLIER_NAME} ({CTO_SUPPLIER_ID})"
            : $"{CFROM_SUPPLIER_NAME} ({CFROM_SUPPLIER_ID})";

        public string CDEPT_DISPLAY => CFROM_DEPT_CODE != CTO_DEPT_CODE
            ? $"{CFROM_DEPT_NAME} ({CFROM_DEPT_CODE}) - {CTO_DEPT_NAME} ({CTO_DEPT_CODE})"
            : $"{CFROM_DEPT_NAME} ({CFROM_DEPT_CODE})";

        public bool LIS_PRINT { get; set; }
        public bool LTRANSACTION_CURRENCY { get; set; } = true;
        public bool LBASE_CURRENCY { get; set; }
        public bool LLOCAL_CURRENCY { get; set; }
        public bool LTRANSACTION_TYPE { get; set; }
        public bool LSUPPLIER__CATEGORY { get; set; }
        public bool LDEPARTMENT { get; set; }
        public string CREPORT_FILENAME { get; set; }
        public string CREPORT_FILETYPE { get; set; }
        public DateTime DDATE_CUTOFF { get; set; }
    }
}
