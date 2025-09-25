﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00150COMMON.DTO_Report_Detail.Detail
{
    public class PMR00150DataDetailChargeTypeUnitDTO
    {
        public string? CCHARGE_DETAIL_CHARGE_NAME { get; set; }
        public string? CCHARGE_DETAIL_TAX_NAME { get; set; }
        public string? CCHARGE_DETAIL_START_DATE { get; set; }
        public string? CCHARGE_DETAIL_END_DATE { get; set; }
        public DateTime? DCHARGE_DETAIL_START_DATE { get; set; }
        public DateTime? DCHARGE_DETAIL_END_DATE { get; set; }
        public string? CCHARGE_DETAIL_TENURE { get; set; }
        public string? CCHARGE_DETAIL_FEE_METHOD { get; set; }
        public decimal NCHARGE_DETAIL_FEE_AMOUNT { get; set; }
        public decimal NCHARGE_DETAIL_CALCULATED_FEE_AMOUNT { get; set; }
    }
}

