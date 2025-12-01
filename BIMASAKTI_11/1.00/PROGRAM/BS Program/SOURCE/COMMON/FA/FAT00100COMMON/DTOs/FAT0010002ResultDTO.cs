using R_APICommonDTO;

namespace FAT00100Common.DTOs
{
    /// <summary>
    /// Generic Result DTO for FAT0010002 operations
    /// </summary>
    public class FAT0010002ResultDTO<T> : R_APIResultBaseDTO
    {
        public T? Data { get; set; }
    }
}

