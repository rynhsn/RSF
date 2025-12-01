using R_APICommonDTO;

namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Generic Result DTO for FAT0010003 operations
    /// </summary>
    public class FAT0010003ResultDTO<T> : R_APIResultBaseDTO
    {
        public T? Data { get; set; }
    }
}

