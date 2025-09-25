using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON.DTOs.PMT50621
{
    public class GetProductTypeResultDTO : R_APIResultBaseDTO
    {
        public List<GetProductTypeDTO> Data { get; set; }
    }
}
