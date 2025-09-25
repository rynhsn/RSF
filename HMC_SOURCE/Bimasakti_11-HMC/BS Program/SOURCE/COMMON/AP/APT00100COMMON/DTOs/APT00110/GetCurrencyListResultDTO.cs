using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00100COMMON.DTOs.APT00110
{
    public class GetCurrencyListResultDTO : R_APIResultBaseDTO
    {
        public List<GetCurrencyListDTO> Data { get; set; }
    }
}
