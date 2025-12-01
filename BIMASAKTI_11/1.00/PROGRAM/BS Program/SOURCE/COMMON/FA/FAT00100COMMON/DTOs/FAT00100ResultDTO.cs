using R_APICommonDTO;

namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Generic Result DTO for FAT00100 operations
    /// </summary>
    public class FAT00100ResultDTO<T> : R_APIResultBaseDTO
    {
        public T? Data { get; set; }
    }
}

