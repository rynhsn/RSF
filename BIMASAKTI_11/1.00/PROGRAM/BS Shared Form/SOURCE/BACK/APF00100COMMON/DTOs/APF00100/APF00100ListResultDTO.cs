using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APF00100COMMON.DTOs.APF00100
{
    public class APF00100ListResultDTO : R_APIResultBaseDTO
    {
        public List<APF00100ListDTO> Data { get; set; }
    }
}
