namespace PMT06000Common.DTOs
{
    public class PMT06000ParameterDTO
    {
        public string CDEPT_CODE { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CREC_ID { get; set; } = "";
        public bool isCaller { get; set; } = true;
    }
}