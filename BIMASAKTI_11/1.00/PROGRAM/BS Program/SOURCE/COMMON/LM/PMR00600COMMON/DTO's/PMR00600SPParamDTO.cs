using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00600COMMON.DTO_s
{
    public class PMR00600SPParamDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CFROM_PERIOD { get; set; }
        public string CTO_PERIOD { get; set; }
        public string CFROM_BUILDING_ID { get; set; }
        public string CTO_BUILDING_ID { get; set; }
        public string CFROM_DEPT_CODE { get; set; }
        public string CTO_DEPT_CODE { get; set; }
        public string CREPORT_TYPE { get; set; }
        public string CFROM_TENANT_ID { get; set; }
        public string CTO_TENANT_ID { get; set; }
        public string CFROM_SERVICE_ID { get; set; }
        public string CTO_SERVICE_ID { get; set; }
        public string CSTATUS { get; set; }
        public string CINVOICE { get; set; }
        public string CLANG_ID { get; set; }
    }
}
