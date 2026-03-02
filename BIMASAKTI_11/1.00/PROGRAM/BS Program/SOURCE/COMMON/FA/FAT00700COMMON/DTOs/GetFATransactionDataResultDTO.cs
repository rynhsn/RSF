namespace FAT00700Common.DTOs
{
    public class GetFATransactionDataResultDTO
    {
        public string CTRANS_DESC { get; set; } = string.Empty;
        public bool LTRANS_APPROVAL { get; set; }
        public bool LINCREMENT_FLAG { get; set; }
    }
}

