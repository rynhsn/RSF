using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02510
{
    public class GSM02510ListDTO : R_APIResultBaseDTO
    {
        public List<GSM02510DTO> Data { get; set; }
    }
}
