using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02503
{
    public class GSM02503ImageListDTO : R_APIResultBaseDTO
    {
        public List<GSM02503ImageDTO> Data { get; set; }
    }
}
