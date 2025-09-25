using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02541
{
    public class UploadOtherUnitErrorDTO
    {
        public string OtherUnitId { get; set; } = "";
        public string OtherUnitName { get; set; } = "";
        public string OtherUnitType { get; set; } = "";
        public string Building { get; set; } = "";
        public string Floor { get; set; } = "";
        public string Location { get; set; } = "";
        public string OtherUnitView { get; set; } = "";
        public decimal Gross { get; set; } = 0;
        public decimal Net { get; set; } = 0;
        public string LeaseStatus { get; set; } = "";
        public bool Active { get; set; } = false;
        public string NonActiveDate { get; set; } = "";
        public string ErrorMessage { get; set; } = "";
    }
}
