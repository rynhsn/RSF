using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT50600COMMON.DTOs.PMT50610
{
    public class GetPaymentTermListResultDTO : R_APIResultBaseDTO
    {
        public List<GetPaymentTermListDTO> Data { get; set; }
    }
}
