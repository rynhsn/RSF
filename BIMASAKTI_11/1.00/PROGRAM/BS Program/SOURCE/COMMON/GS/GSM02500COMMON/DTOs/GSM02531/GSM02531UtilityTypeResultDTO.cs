using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02530
{
    public class GSM02531UtilityTypeResultDTO : R_APIResultBaseDTO
    {
        public List<GSM02531UtilityTypeDTO> Data { get; set; }
    }
}
