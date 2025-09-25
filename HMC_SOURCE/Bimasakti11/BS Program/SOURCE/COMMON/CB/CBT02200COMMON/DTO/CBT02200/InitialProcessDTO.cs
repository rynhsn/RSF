using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02200COMMON.DTO.CBT02200
{
    public class InitialProcessDTO
    {
        public GetCompanyInfoDTO CompanyInfo { get; set; }
        public GetGLSystemParamDTO GLSystemParam { get; set; }
        public GetCBSystemParamDTO CBSystemParam { get; set; }
        public GetSoftPeriodStartDateDTO SoftPeriodStartDate { get; set; }
        public GetTransCodeInfoDTO TransCodeInfo { get; set; }
        public GetPeriodYearRangeDTO PeriodYearRange { get; set; }
    }
}
