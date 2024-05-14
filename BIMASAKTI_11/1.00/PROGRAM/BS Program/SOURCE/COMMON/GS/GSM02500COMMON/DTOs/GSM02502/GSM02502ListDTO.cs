using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02502
{
    public class GSM02502ListDTO : R_APIResultBaseDTO
    {
        public List<GSM02502DTO> Data { get; set; }
    }
}
