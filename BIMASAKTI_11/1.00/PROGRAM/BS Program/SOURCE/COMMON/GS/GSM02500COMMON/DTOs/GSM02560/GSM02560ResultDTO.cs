using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02560
{
    public class GSM02560ResultDTO : R_APIResultBaseDTO
    {
        public List<GSM02560DTO> Data { get; set; }
    }
}
