using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMF00100COMMON.DTOs.PMF00100
{
    public class GetCompanyInfoResultDTO : R_APIResultBaseDTO
    {
        public GetCompanyInfoDTO Data { get; set; }
    }
}
