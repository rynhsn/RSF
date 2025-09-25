using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMF00100COMMON.DTOs.PMF00110
{
    public class GetTransactionTypeResultDTO : R_APIResultBaseDTO
    {
        public List<GetTransactionTypeDTO> Data { get; set; }
    }
}
