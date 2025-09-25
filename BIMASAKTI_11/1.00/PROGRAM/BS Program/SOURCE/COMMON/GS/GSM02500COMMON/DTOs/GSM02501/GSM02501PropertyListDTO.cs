using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02501
{
    public class GSM02501PropertyListDTO : R_APIResultBaseDTO
    {
        public List<GSM02501PropertyDTO> Data { get; set; }
    }
}
