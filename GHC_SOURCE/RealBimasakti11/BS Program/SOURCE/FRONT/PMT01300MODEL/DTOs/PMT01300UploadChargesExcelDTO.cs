using System;

namespace PMT01300MODEL
{
    public class PMT01300UploadChargesExcelDTO
    {
        public int SEQ_ERROR { get; set; } = 0;
        public int NO { get; set; } = 0;
        public string ChargesId { get; set; } = "";
        public string TaxId { get; set; } = "";
        public string TenureYear { get; set; } = "";
        public string TenureMonth { get; set; } = "";
        public string TenureDays { get; set; } = "";
        public string BaseonOpeningDate { get; set; } 
        public bool BaseonOpeningDateDisplay { get; set; }
        public string StartDate { get; set; } = "";
        public DateTime? StartDateDisplay { get; set; } 
        public string EndDate { get; set; } = "";
        public DateTime? EndDateDisplay { get; set; } 
        public string BillingMode { get; set; } = "";
        public string Currency { get; set; } = "";
        public string FeeMethod { get; set; } = "";
        public decimal FeeAmount { get; set; } = 0;
        public string PeriodMode { get; set; } = "";
        public string Prorate { get; set; } 
        public bool ProrateDisplay { get; set; }
        public string Description { get; set; } = "";
        public string DocumentNo { get; set; } = "";
        public string UnitId { get; set; } = "";
        public string FloorId { get; set; } = "";
        public string Notes { get; set; } = "";
        public string Valid { get; set; } = "";
    }
    public class PMT01300UploadChargesSaveExcelDTO
    {
        public string DocumentNo { get; set; } = "";
        public string UnitId { get; set; } = "";
        public string FloorId { get; set; } = "";
        public string ChargesId { get; set; } = "";
        public string TaxId { get; set; } = "";
        public string TenureYear { get; set; } = "";
        public string TenureMonth { get; set; } = "";
        public string TenureDays { get; set; } = "";
        public string BaseonOpeningDate { get; set; }
        public string StartDate { get; set; } = "";
        public string EndDate { get; set; } = "";
        public string BillingMode { get; set; } = "";
        public string Currency { get; set; } = "";
        public string FeeMethod { get; set; } = "";
        public decimal FeeAmount { get; set; } = 0;
        public string PeriodMode { get; set; } = "";
        public string Prorate { get; set; }
        public string Description { get; set; } = "";
        public string Valid { get; set; } = "";
        public string Notes { get; set; } = "";
    }
}
