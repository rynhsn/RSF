using PMR00150COMMON.DTO_Report_Detail.Detail;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00150COMMON
{
    public class PMR00150DataDetailChargeDTO
    {
        public string? CCHARGE_DETAIL_TYPE_NAME { get; set; }
        public List<PMR00150DataDetailChargeTypeDTO>? ChargeTypeDetail { get; set; }
    }
}
