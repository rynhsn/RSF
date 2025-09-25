using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00110
{
    public class GetPaymentTermListResultDTO : R_APIResultBaseDTO
    {
        public List<GetPaymentTermListDTO> Data { get; set; }
    }
}
