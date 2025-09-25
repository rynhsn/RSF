using System;
using System.Collections.Generic;
using System.Text;

namespace GSM02500COMMON.DTOs.GSM02502
{
    public class UploadUnitTypeCategoryDTO
    {
        public int No { get; set; } = 0;
        public string CompanyId { get; set; } = "";
        public string PropertyId { get; set; } = "";
        public string UnitTypeCategoryCode { get; set; } = "";
        public string UnitTypeCategoryName { get; set; } = "";
        public string Description { get; set; } = "";
        public string PropertyType { get; set; } = "";
        public int InvitationInvoicePeriod { get; set; } = 0;
        public bool Active { get; set; } = false;
        public string NonActiveDate { get; set; } = "";
        public string Valid { get; set; } = "";
        public string Notes { get; set; } = "";
    }
}
