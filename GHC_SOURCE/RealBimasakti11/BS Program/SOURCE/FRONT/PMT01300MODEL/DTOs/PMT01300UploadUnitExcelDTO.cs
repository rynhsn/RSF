using System;

namespace PMT01300MODEL
{
    public class PMT01300UploadUnitExcelDTO
    {
        public int SEQ_ERROR { get; set; } = 0;
        public int NO { get; set; } = 0;
        public string FloorId { get; set; } = "";
        public string BuildingId { get; set; } = "";
        public string UnitId { get; set; } = "";
        public string DocumentNo { get; set; } = "";
        public string Notes { get; set; } = "";
        public string Valid { get; set; } = "";
    }
    public class PMT01300UploadUnitSaveExcelDTO
    {
        public string DocumentNo { get; set; } = "";
        public string UnitId { get; set; } = "";
        public string FloorId { get; set; } = "";
        public string BuildingId { get; set; } = "";
        public string Valid { get; set; } = "";
        public string Notes { get; set; } = "";
    }
}
