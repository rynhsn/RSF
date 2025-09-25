using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00111
{
    public class APT00111ListResultDTO : R_APIResultBaseDTO
    {
        public List<APT00111ListDTO> Data { get; set; }
    }
}
