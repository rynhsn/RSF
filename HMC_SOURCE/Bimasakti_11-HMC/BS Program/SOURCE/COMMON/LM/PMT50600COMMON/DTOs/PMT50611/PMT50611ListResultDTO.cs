using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON.DTOs.PMT50611
{
    public class PMT50611ListResultDTO : R_APIResultBaseDTO
    {
        public List<PMT50611ListDTO> Data { get; set; }
    }
}
