using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON.DTOs.PMT02100
{
    public class PMT02100HandoverBuildingResultDTO : R_APIResultBaseDTO
    {
        public List<PMT02100HandoverBuildingDTO> Data { get; set; }
    }
}
