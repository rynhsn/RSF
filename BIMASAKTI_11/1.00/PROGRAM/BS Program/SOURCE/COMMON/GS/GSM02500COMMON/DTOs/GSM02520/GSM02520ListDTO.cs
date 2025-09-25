using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02520
{
    public class GSM02520ListDTO : R_APIResultBaseDTO
    {
        public List<GSM02520DTO> Data { get; set; }
    }
}
