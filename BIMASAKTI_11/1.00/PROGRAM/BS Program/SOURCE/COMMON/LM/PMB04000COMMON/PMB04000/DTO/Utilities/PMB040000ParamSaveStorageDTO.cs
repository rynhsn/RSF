using PMB04000COMMONPrintBatch;

namespace PMB04000COMMON.DTO.Utilities
{
    public class PMB040000ParamSaveStorageDTO
    {
        public string? CSTORAGE_ID { get; set; }
     //   public PMB04000DataReportDTO? ReportInformation { get; set; }
     public string? CREF_NO { get; set; }
        public string ? FileExtension { get; set;}
        public byte[]? REPORT { get; set; }
    }
}
