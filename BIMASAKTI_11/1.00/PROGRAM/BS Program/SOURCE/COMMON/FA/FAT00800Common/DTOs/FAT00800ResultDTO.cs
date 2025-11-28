using R_APICommonDTO;

namespace FAT00800Common.DTOs
{
    /// <summary>
    /// Generic Result DTO for FAT00800 operations
    /// </summary>
    public class FAT00800ResultDTO<T> : R_APIResultBaseDTO
    {
        public T? Data { get; set; }
    }
}

