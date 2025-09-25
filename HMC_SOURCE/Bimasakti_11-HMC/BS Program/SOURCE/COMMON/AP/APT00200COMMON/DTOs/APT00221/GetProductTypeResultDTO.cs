using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON.DTOs.APT00221
{
    public class GetProductTypeResultDTO : R_APIResultBaseDTO
    {
        public List<GetProductTypeDTO> Data { get; set; }
    }
}
