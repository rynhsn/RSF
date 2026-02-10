using R_APICommonDTO;

namespace FAT00300Common.DTOs
{
    public class FAT00300ResultDTO<T> : R_APIResultBaseDTO
    {
        public T? Data { get; set; }
    }
}







