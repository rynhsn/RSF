﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02530
{
    public class UploadUnitSaveDTO
    {
        public int NO { get; set; } = 0;
        public string FloorId { get; set; } = "";
        public string UnitId { get; set; } = "";
        public string UnitName { get; set; } = "";
        public string UnitType { get; set; } = "";
        public string UnitView { get; set; } = "";
        public decimal GrossSize { get; set; } = 0;
        public decimal NetSize { get; set; } = 0;
        public string UnitCategory { get; set; } = "";
        public string StrataStatus { get; set; } = "";
        public string LeaseStatus { get; set; } = "";
        public bool Active { get; set; } = false;
        public string NonActiveDate { get; set; } = "";
    }
}
