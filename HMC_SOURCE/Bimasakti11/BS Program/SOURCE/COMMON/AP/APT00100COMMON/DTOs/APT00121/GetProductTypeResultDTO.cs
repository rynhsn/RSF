using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00121
{
    public class GetProductTypeResultDTO : R_APIResultBaseDTO
    {
        public List<GetProductTypeDTO> Data { get; set; }
    }
}
