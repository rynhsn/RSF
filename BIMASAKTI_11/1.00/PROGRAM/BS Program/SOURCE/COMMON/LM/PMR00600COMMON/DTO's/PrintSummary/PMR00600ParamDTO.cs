using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PMR00600COMMON.DTO_s.Print
{
    public class PMR00600ParamDTO : PMR00600SPParamDTO
    {
        public string TYPE_PRINT { get; set; }
        public string CUSER_ID { get; set; }
        public string CREPORT_CULTURE { get; set; }
        public string CFROM_BUILDING_NAME { get; set; }
        public string CTO_BUILDING_NAME { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CFROM_SERVICE_NAME { get; set; }
        public string CTO_SERVICE_NAME { get; set; }
        public string CFROM_DEPT_NAME { get; set; }
        public string CTO_DEPT_NAME { get; set; }
        public string CFROM_TENANT_NAME { get; set; }
        public string CTO_TENANT_NAME { get; set; }
        public string CINVOICE_DISPLAY { get; set; }
        public string CSTATUS_DISPLAY { get; set; }
        public bool LTENANT { get; set; }
        public bool LSERVICE { get; set; }
        public bool LSTATUS { get; set; }
        public bool LINVOICE { get; set; }
        public bool LIS_SUMMARY_BY_TENANT { get; set; }
        public bool LIS_SUMMARY_BY_CHARGES { get; set; }
        public string CREPORT_TYPE_DISPLAY { get; set; }
        public string CGROUP_TYPE_DISPLAY { get; set; }
        public string CPERIOD_DISPLAY { get; set; }
        public string CBUILDING_DISPLAY => CFROM_BUILDING_ID != CTO_BUILDING_ID
            ? $"{CFROM_BUILDING_NAME} ({CFROM_BUILDING_ID}) - {CTO_BUILDING_NAME} ({CTO_BUILDING_ID})"
            : $"{CFROM_BUILDING_NAME} ({CFROM_BUILDING_ID})";
        public string CDEPT_REPORT_DISPLAY => CFROM_DEPT_CODE != CTO_DEPT_CODE
            ? $"{CFROM_DEPT_NAME} ({CFROM_DEPT_CODE}) - {CTO_DEPT_NAME} ({CTO_DEPT_CODE})"
            : $"{CFROM_DEPT_NAME} ({CFROM_DEPT_CODE})";
        public string CTENANT_DISPLAY => CFROM_TENANT_ID != CTO_TENANT_ID
            ? $"{CFROM_TENANT_NAME} ({CFROM_TENANT_ID}) - {CTO_TENANT_NAME} ({CTO_TENANT_ID})"
            : $"{CFROM_TENANT_NAME} ({CFROM_TENANT_ID})";
        public string CSERVICE_DISPLAY => CFROM_SERVICE_ID != CTO_SERVICE_ID
            ? $"{CFROM_SERVICE_NAME} ({CFROM_SERVICE_ID}) - {CTO_SERVICE_NAME} ({CTO_SERVICE_ID})"
            : $"{CFROM_SERVICE_NAME} ({CFROM_SERVICE_ID})";

        public string CREPORT_FILENAME { get; set; }
        public string CREPORT_FILEEXT { get; set; }
    }
}
