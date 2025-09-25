using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PMI00300COMMON.DTO
{
    public class PMI00300DisplayProcessDTO
    {
        public string CPROPERTY_ID { get; set; } = string.Empty;
        public string CUNIT_OPTION { get; set; } = string.Empty;
        public string CBUILDING_ID { get; set; } = string.Empty;
        public string CBUILDING_NAME { get; set; } = string.Empty;
        public bool LALL_BUILDING { get; set; } = false;  
        public string CFROM_FLOOR_ID { get; set; } = string.Empty;
        public string CFROM_FLOOR_NAME { get; set; } = string.Empty;
        public string CTO_FLOOR_ID { get; set; } = string.Empty;
        public string CTO_FLOOR_NAME { get; set; } = string.Empty;
        public string CUNIT_CATEGORY { get; set; } = string.Empty;
        public string CSTATUS_ID { get; set; } = string.Empty;
        public string CSTATUS_NAME { get; set; } = string.Empty;

    }
}
