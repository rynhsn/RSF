using PMT02000COMMON.DownloadTemplate;

namespace PMT02000COMMON.Upload.Unit
{
    public class UnitUploadExcelDTO : SaveUnitToExcelDTO
    {
        public int SEQ_ERROR { get; set; } = 0;
        public int NO { get; set; } = 0;
        public string? Valid { get; set; }
        public string? Notes { get; set; }
    }
}
