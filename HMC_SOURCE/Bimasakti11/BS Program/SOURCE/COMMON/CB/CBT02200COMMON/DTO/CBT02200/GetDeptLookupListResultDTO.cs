using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON.DTO.CBT02200
{
    public class GetDeptLookupListResultDTO : R_APIResultBaseDTO
    {
        public List<GetDeptLookupListDTO> Data { get; set; }
    }
}
