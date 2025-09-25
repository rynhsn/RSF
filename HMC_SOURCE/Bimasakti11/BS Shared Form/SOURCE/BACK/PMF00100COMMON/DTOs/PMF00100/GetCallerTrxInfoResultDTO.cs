using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMF00100COMMON.DTOs.PMF00100
{
    public class GetCallerTrxInfoResultDTO : R_APIResultBaseDTO
    {
        public GetCallerTrxInfoDTO Data { get; set; }
    }
}
