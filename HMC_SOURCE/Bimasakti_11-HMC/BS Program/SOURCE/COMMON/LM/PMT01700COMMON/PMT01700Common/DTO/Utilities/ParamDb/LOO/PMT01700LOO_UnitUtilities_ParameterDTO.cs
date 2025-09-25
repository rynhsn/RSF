using System;
using System.Collections.Generic;
using System.Text;

namespace PMT01700COMMON.DTO.Utilities.ParamDb.LOO
{
    public class PMT01700LOO_UnitUtilities_ParameterDTO : PMT01700BaseParameterDTO
    {
        public string? COTHER_UNIT_ID { get; set; }
        public string? CFLOOR_ID { get; set; }
        public string? CBUILDING_ID { get; set; }
        public string? CTRANS_CODE { get; set; }
        public string? CDEPT_CODE { get; set; } 
        public string? CREF_NO { get; set; }
        public string? CSEQ_NO { get; set; }
        public string? CCHARGE_MODE { get; set; }

        public string? CUTILITY_TYPE { get; set; }

        //CR add FIlter CFROM_REF_DATE 18/07/2023
        public string? CFROM_REF_DATE { get; set; }
        public string? CTRANS_STATUS_LIST { get; set; }
    }
}
