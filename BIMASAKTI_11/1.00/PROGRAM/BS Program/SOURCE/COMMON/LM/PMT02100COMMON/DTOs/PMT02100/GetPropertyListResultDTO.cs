using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON.DTOs.PMT02100
{
    public class GetPropertyListResultDTO : R_APIResultBaseDTO
    {
        public List<GetPropertyListDTO> Data { get; set; }
    }
}
