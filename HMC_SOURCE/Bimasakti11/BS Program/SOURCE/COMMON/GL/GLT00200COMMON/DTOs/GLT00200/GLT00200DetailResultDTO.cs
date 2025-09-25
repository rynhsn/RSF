using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00200COMMON.DTOs.GLT00200
{
    public class GLT00200DetailResultDTO : R_APIResultBaseDTO
    {
        public List<GLT00200DetailDTO> Data { get; set; }
    }
}
