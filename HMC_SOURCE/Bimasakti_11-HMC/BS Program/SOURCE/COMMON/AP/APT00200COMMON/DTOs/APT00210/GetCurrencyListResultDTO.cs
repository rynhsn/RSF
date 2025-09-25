using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON.DTOs.APT00210
{
    public class GetCurrencyListResultDTO : R_APIResultBaseDTO
    {
        public List<GetCurrencyListDTO> Data { get; set; }
    }
}
