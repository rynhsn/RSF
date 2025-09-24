using PMR00200COMMON.DTO_s;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PMR00200COMMON
{
    public class PMR00200ParamDTO : PMR00200SPParamDTO
    {
        public string CREPORT_CULTURE { get; set; }
        public string CUSER_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CDEPT_REPORT_DISPLAY => CFROM_DEPARTMENT_ID != CTO_DEPARTMENT_ID
        ? $"{CFROM_DEPARTMENT_NAME} ({CFROM_DEPARTMENT_ID}) - {CTO_DEPARTMENT_NAME} ({CTO_DEPARTMENT_ID})"
        : $"{CFROM_DEPARTMENT_NAME} ({CFROM_DEPARTMENT_ID})";
        public string CSALESMAN_REPORT_DISPLAY => CFROM_SALESMAN_ID != CTO_SALESMAN_ID
        ? $"{CFROM_SALESMAN_NAME} ({CFROM_SALESMAN_ID}) - {CTO_SALESMAN_NAME} ({CTO_SALESMAN_ID})"
        : $"{CFROM_SALESMAN_NAME} ({CFROM_SALESMAN_ID})";
        public string CPERIOD_DISPLAY { get; set; }
        public string CREPORT_TYPE_DISPLAY { get; set; }
        public string CREPORT_FILENAME { get; set; } = "";
        public string CREPORT_FILEEXT { get; set; } = "";
    }
}