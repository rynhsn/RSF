namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Result DTO for GetTransTypeDesc method
    /// </summary>
    public class FAT00800GetTransTypeDescResultDTO
    {
        public string CTRANS_DESC { get; set; } = string.Empty;
        public bool LTRANS_APPROVAL { get; set; }
        public bool LINCREMENT_FLAG { get; set; }
    }
}

