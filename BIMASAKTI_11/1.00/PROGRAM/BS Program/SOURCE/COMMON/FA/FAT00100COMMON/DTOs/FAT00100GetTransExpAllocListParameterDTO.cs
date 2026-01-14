namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for FAT00100GetTransExpAllocList method
    /// </summary>
    public class FAT00100GetTransExpAllocListParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CPARENT_ID { get; set; } = string.Empty;
        public string CDEPT_CODE { get; set; } = string.Empty;
        public string CTRANS_CODE { get; set; } = string.Empty;
        public string CREF_NO { get; set; } = string.Empty;
        public string CASSET_CODE { get; set; } = string.Empty;
        public string CASSET_TRANS_SEQ_NO { get; set; } = string.Empty;
        public string CLANGUAGE_ID { get; set; } = string.Empty;
    }
}
