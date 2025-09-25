using GSM02500COMMON.DTOs.GSM02500;
using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02550
{
    public class GSM02550ResultDTO : R_APIResultBaseDTO
    {
        public List<GSM02550DTO> Data { get; set; }
    }
}
