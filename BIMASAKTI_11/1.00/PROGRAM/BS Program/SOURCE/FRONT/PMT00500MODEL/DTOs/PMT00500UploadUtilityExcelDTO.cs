using System;

namespace PMT00500MODEL
{
    public class PMT00500UploadUtilityExcelDTO
    {
        public int SEQ_ERROR { get; set; } = 0;
        public int NO { get; set; } = 0;
        public string DocNo { get; set; } = "";
        public string UnitId { get; set; } = "";
        public string UtilityType { get; set; } = "";
        public string MeterNo { get; set; } = "";
        public string ChargesId { get; set; } = "";
        public string TaxId { get; set; } = "";
        public decimal MeterStart { get; set; } = 0;
        public decimal Block1Start { get; set; } = 0;
        public decimal Block2Start { get; set; } = 0;
        public string Valid { get; set; } = "";
        public string ErrorNotes { get; set; } = "";
    }
    public class PMT00500UploadUtilitySaveExcelDTO
    {
        public string DocNo { get; set; } = "";
        public string UnitId { get; set; } = "";
        public string UtilityType { get; set; } = "";
        public string MeterNo { get; set; } = "";
        public string ChargesId { get; set; } = "";
        public string TaxId { get; set; } = "";
        public decimal MeterStart { get; set; } = 0;
        public decimal Block1Start { get; set; } = 0;
        public decimal Block2Start { get; set; } = 0;
        public string Valid { get; set; } = "";
        public string ErrorNotes { get; set; } = "";
    }
}
