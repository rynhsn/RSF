using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APT00200COMMON.DTOs.APT00210
{
    public class GetPaymentTermListResultDTO : R_APIResultBaseDTO
    {
        public List<GetPaymentTermListDTO> Data { get; set; }
    }
}
