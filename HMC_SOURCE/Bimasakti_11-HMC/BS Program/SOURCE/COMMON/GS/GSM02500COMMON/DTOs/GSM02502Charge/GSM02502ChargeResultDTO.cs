using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02502Charge
{
    public class GSM02502ChargeResultDTO : R_APIResultBaseDTO
    {
        public List<GSM02502ChargeDTO> Data { get; set; }
    }
}
