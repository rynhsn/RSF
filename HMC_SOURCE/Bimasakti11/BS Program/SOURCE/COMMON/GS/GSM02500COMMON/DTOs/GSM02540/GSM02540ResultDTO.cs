using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02540
{
    public class GSM02540ResultDTO : R_APIResultBaseDTO
    {
        public List<GSM02540DTO> Data { get; set; }
    }
}
