using R_APICommonDTO;

namespace FAT01100Common.DTOs
{
    /// <summary>
    /// Generic Result DTO for FAT01100 operations
    /// </summary>
    public class FAT01100ResultDTO<T> : R_APIResultBaseDTO
    {
        public T? Data { get; set; }
    }
}
