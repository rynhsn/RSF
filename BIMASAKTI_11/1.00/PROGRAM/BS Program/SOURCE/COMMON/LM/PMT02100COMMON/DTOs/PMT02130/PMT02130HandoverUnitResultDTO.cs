using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON.DTOs.PMT02130
{
    public class PMT02130HandoverUnitResultDTO : R_APIResultBaseDTO
    {
        public List<PMT02130HandoverUnitDTO> Data { get; set; }
    }
}
