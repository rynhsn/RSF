using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON.DTO.CBT02210
{
    public class GetCenterListResultDTO : R_APIResultBaseDTO
    {
        public List<GetCenterListDTO> Data { get; set; }
    }
}
