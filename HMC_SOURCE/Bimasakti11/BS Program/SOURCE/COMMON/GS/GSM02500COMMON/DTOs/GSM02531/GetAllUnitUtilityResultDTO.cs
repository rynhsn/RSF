using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02531
{
    public class GetAllUnitUtilityResultDTO : R_APIResultBaseDTO
    {
        public List<GetAllUnitUtilityDTO> Data { get; set; }
    }
}
