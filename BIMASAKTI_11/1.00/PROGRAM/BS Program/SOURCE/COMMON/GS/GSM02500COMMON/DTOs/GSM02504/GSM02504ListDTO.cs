using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02504
{
    public class GSM02504ListDTO : R_APIResultBaseDTO
    {
        public List<GSM02504DTO> Data { get; set; }
    }
}
