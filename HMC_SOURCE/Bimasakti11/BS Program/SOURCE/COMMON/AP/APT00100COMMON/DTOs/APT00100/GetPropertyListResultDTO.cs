using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00100
{
    public class GetPropertyListResultDTO : R_APIResultBaseDTO
    {
        public List<GetPropertyListDTO> Data { get; set; }
    }
}
