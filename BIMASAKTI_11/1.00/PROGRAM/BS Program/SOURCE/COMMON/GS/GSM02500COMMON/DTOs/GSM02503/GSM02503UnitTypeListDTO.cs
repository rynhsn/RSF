using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02503
{
    public class GSM02503UnitTypeListDTO : R_APIResultBaseDTO
    {
        public List<GSM02503UnitTypeDTO> Data { get; set; }
    }
}
