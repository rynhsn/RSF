using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON.DTOs.APT00200
{
    public class GetPropertyListResultDTO : R_APIResultBaseDTO
    {
        public List<GetPropertyListDTO> Data { get; set; }
    }
}
