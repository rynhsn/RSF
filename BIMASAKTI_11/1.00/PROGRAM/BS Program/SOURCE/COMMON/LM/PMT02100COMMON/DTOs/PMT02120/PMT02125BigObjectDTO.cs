using PMT02100COMMON.DTOs.PMT02130;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON.DTOs.PMT02120
{
    public class PMT02125BigObjectDTO
    {
        public List<PMT02120HandoverProcessUtilityDTO> UtilityList { get; set; }
        public List<PMT02120HandoverProcessUnitDTO> UnitList { get; set; }
    }
}
