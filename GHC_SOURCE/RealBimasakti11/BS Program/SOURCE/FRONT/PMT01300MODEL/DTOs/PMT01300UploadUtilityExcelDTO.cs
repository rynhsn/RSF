using System;

namespace PMT01300MODEL
{
    public class PMT01300UploadUtilityExcelDTO
    {
        public int SEQ_ERROR { get; set; } = 0;
        public int NO { get; set; } = 0;
        public string DocumentNo { get; set; } = "";
        public string UnitId { get; set; } = "";
        public string UtilityType { get; set; } = "";
        public string MeterNo { get; set; } = "";
        public string ChargesId { get; set; } = "";
        public string TaxId { get; set; } = "";
        public string Notes { get; set; } = "";
        public string Valid { get; set; } = "";
    }
    public class PMT01300UploadUtilitySaveExcelDTO
    {
        public string DocumentNo { get; set; } = "";
        public string UnitId { get; set; } = "";
        public string UtilityType { get; set; } = "";
        public string MeterNo { get; set; } = "";
        public string ChargesId { get; set; } = "";
        public string TaxId { get; set; } = "";
        public string Valid { get; set; } = "";
        public string Notes { get; set; } = "";
    }
}
