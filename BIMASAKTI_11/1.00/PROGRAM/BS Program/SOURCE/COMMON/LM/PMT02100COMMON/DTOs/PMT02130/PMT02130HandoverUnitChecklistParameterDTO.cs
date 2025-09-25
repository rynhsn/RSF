using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON.DTOs.PMT02130
{
    public class PMT02130HandoverUnitChecklistParameterDTO
    {
        public string CPROPERTY_ID { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CREF_NO { get; set; }
        public string CUNIT_ID { get; set; }
        
        //CR04
        public string CFLOOR_ID { get; set; }
        public string CBUILDING_ID { get; set; }
    }
}
