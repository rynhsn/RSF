using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON.DTOs.APT00211
{
    public class APT00211ListResultDTO : R_APIResultBaseDTO
    {
        public List<APT00211ListDTO> Data { get; set; }
    }
}
