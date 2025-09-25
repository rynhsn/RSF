using PMT02000COMMON.DownloadTemplate;
using System;

namespace PMT02000COMMON.Upload.Agreement
{
    public class AgreementUploadExcelDTO : SaveAgreementToExcelDTO
    {
        public int NO { get; set; } = 0;
        public DateTime? HORefDateDisplay { get; set; }
        public DateTime? HOActualDateDisplay { get; set; }
        public string? Valid { get; set; }
        public string? Notes { get; set; }
        public string? NotesError { get; set; }
    }
}
