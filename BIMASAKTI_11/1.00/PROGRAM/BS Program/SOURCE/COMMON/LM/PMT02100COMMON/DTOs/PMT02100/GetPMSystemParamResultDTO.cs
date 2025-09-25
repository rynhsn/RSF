using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT02100COMMON.DTOs.PMT02100
{
    public class GetPMSystemParamResultDTO : R_APIResultBaseDTO
    {
        public GetPMSystemParamDTO Data { get; set; }
    }
}
