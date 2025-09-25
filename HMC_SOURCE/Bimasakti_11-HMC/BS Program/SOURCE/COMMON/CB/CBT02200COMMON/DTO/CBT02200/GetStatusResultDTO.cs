using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON.DTO.CBT02200
{
    public class GetStatusResultDTO : R_APIResultBaseDTO
    {
        public List<GetStatusDTO> Data { get; set; }
    }
}
