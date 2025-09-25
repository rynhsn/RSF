using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02500
{
    public class GetUnitTypeDTO
    {
        public string CUNIT_TYPE_ID { get; set; }
        public string CUNIT_TYPE_NAME { get; set; }
        public decimal NNET_AREA_SIZE { get; set; }
        public decimal NGROSS_AREA_SIZE { get; set; }
    }
}
