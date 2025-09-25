using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APF00100COMMON.DTOs.APF00110
{
    public class GetTransactionTypeResultDTO : R_APIResultBaseDTO
    {
        public List<GetTransactionTypeDTO> Data { get; set; }
    }
}
