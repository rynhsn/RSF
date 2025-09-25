using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02530
{
    public class GetStrataLeaseResultDTO : R_APIResultBaseDTO
    {
        public List<GetStrataLeaseDTO> Data { get; set; }
    }
}
