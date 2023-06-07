namespace GSM05000Common.DTOs
{
    public class GSM05000ApprovalDepartmentDTO
    {
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        private string _CDEPT;
        public string CDEPT { get => _CDEPT; set => _CDEPT = CDEPT_NAME + " (" + CDEPT_CODE + ")"; }
    }
}