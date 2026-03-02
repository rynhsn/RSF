using R_APICommonDTO;

namespace FAT00700Common.DTOs
{
    public class FAT00700ResultDTO<T> : R_APIResultBaseDTO
    {
        public T? Data { get; set; }
    }
}

