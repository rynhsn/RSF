using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMI00300COMMON.DTO
{
    public class PMI00300DetailLeftResultDTO : R_APIResultBaseDTO
    {
        public List<PMI00300DetailLeftDTO> Data { get; set; }
    }
}
