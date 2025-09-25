﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00150COMMON
{
    public class PMR00150ColumnDetailDTO : PMR00150ColumnSummaryDTO
    {
        public string Col_CUNIT { get; set; } = "Unit";
        public string Col_AREA { get; set; } = "Area";
        public string Col_PRICE { get; set; } = "Price";
        public string Col_CCHARGE_TYPE { get; set; } = "Charge Type";
        public string Col_CHARGE { get; set; } = "Charge";
        public string Col_CSTARTDATE { get; set; } = "Start Date";
        public string Col_CENDDATE { get; set; } = "End Date";
        public string Col_FEE_METHOD{ get; set; } = "Fee Method";
        public string Col_FEE_AMOUNT { get; set; } = "Fee Amount";
        public string Col_CAL_FEE_AMOUNT { get; set; } = "Calculated Fee Amount";
        public string Col_DEPOSIT { get; set; } = "Deposit";
        public string Col_DATE { get; set; } = "Date";
        public string Col_AMOUNT { get; set; } = "Amount";
        public string Col_DESC { get; set; } = "Description";

    }
}
