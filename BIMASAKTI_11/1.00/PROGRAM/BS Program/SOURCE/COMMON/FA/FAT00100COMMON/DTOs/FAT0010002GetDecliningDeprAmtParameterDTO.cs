namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Parameter DTO for GetDecliningDeprAmt method
    /// </summary>
    public class FAT0010002GetDecliningDeprAmtParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = string.Empty;
        public string CLANG_ID { get; set; } = string.Empty;
        public string CUSER_ID { get; set; } = string.Empty;
        public string CDEPR_METHOD { get; set; } = string.Empty;
        public int IBEG_UL_YR { get; set; }
        public int IBEG_UL_MO { get; set; }
        public int IREM_UL_YR { get; set; }
        public int IREM_UL_MO { get; set; }
        public decimal NBEG_BOOK_VAL { get; set; }
    }
}

