using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02500
{
    public class GetUnitTypeListDTO : R_APIResultBaseDTO
    {
        public List<GetUnitTypeDTO> Data { get; set; }
    }
}
