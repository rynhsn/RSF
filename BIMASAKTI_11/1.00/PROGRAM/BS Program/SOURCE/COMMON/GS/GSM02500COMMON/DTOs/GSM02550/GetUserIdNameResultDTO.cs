using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02550
{
    public class GetUserIdNameResultDTO : R_APIResultBaseDTO
    {
        public List<GetUserIdNameDTO> Data { get; set; }
    }
}
