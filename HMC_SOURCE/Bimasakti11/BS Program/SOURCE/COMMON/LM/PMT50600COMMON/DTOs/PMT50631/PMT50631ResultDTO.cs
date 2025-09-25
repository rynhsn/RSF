using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON.DTOs.PMT50631
{
    public class PMT50631ResultDTO : R_APIResultBaseDTO
    {
        public List<PMT50631DTO> Data { get; set; }
    }
}
