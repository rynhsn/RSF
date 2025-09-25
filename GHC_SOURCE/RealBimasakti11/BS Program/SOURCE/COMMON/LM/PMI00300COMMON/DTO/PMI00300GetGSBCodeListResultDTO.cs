using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMI00300COMMON.DTO
{
    public class PMI00300GetGSBCodeListResultDTO : R_APIResultBaseDTO
    {
        public List<PMI00300GetGSBCodeListDTO> Data { get; set; }
    }
}
