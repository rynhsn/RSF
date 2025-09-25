using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON.DTO.CBT02200
{
    public class CBT02200GridResultDTO : R_APIResultBaseDTO
    {
        public List<CBT02200GridDTO> Data { get; set; }
    }
}
