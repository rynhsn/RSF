using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02502Utility
{
    public class GSM02502UtilityResultDTO : R_APIResultBaseDTO
    {
        public List<GSM02502UtilityDTO> Data { get; set; }
    }
}
