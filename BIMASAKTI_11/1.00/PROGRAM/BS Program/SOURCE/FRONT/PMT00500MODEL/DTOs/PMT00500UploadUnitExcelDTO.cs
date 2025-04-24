using System;

namespace PMT00500MODEL
{
    public class PMT00500UploadUnitExcelDTO
    {
        public int SEQ_ERROR { get; set; } = 0;
        public int NO { get; set; } = 0;
        public string FloorId { get; set; } = "";
        public string BuildingId { get; set; } = "";
        public string UnitId { get; set; } = "";
        public string DocNo { get; set; } = "";
        public string Valid { get; set; } = "";
        public string ErrorNotes { get; set; } = "";
    }
    public class PMT00500UploadUnitSaveExcelDTO
    {
        public string DocNo { get; set; } = "";
        public string UnitId { get; set; } = "";
        public string FloorId { get; set; } = "";
        public string BuildingId { get; set; } = "";
        public string Valid { get; set; } = "";
        public string ErrorNotes { get; set; } = "";
    }
}
