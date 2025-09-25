namespace PMM10000COMMON.Upload
{
    public class CallTypeErrorDTO : CallTypeUploadExcelDTO
    {
        public string? ErrorMessage { get; set; }
        public string? ErrorFlag { get; set; } = "Y";
    }
}
