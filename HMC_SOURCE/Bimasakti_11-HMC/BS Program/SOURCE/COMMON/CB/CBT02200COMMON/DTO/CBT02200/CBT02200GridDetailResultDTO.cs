using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON.DTO.CBT02200
{
    public class CBT02200GridDetailResultDTO : R_APIResultBaseDTO
    {
        public List<CBT02200GridDetailDTO> Data { get; set; }
    }
}
