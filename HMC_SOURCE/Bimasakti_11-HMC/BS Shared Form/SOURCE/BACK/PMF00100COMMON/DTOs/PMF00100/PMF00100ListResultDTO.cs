using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMF00100COMMON.DTOs.PMF00100
{
    public class PMF00100ListResultDTO : R_APIResultBaseDTO
    {
        public List<PMF00100ListDTO> Data { get; set; }
    }
}
