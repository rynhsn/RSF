namespace PMM10000COMMON.Upload
{
    public class CallTypeUploadDTO
    {
        public int No { get; set; } = 0;
        public string CCOMPANY_ID { get; set; } = "";
        public string? CPROPERTY_ID { get; set; }
        public string? CCALL_TYPE_ID { get; set; }
        public string? CCALL_TYPE_NAME { get; set; }
        public string? CCATEGORY_ID { get; set; }
        public int IDAYS { get; set; }
        public int IHOURS { get; set; }
        public int IMINUTES { get; set; }
        public bool LPRIOIRTY { get; set; }
    }
}
