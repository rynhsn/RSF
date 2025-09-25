using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLT00500COMMON.DTOs
{
    public class GLT00500DetailResultDTO : R_APIResultBaseDTO
    {
        public List<GLT00500DetailDTO> Data { get; set; }
    }
}
