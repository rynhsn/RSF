using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APF00100COMMON.DTOs.APF00100
{
    public class GetCallerTrxInfoResultDTO : R_APIResultBaseDTO
    {
        public GetCallerTrxInfoDTO Data { get; set; }
    }
}
