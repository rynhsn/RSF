using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02541
{
    public class OtherUnitTypeResultDTO : R_APIResultBaseDTO
    {
        public List<OtherUnitTypeDTO> Data { get; set; }
    }
}
