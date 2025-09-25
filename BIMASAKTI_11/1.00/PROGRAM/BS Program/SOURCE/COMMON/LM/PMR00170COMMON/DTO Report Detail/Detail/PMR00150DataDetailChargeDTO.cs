using PMR00170COMMON.DTO_Report_Detail.Detail;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00170COMMON
{
    public class PMR00170DataDetailChargeDTO
    {
        public string? CCHARGE_DETAIL_TYPE_NAME { get; set; }
        public List<PMR00170DataDetailChargeTypeDTO>? ChargeTypeDetail { get; set; }
    }
}
