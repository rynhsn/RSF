using R_APICommonDTO;

namespace FAF00100COMMON.DTOs
{
    public class FAF00100ResultDTO<T> : R_APIResultBaseDTO
    {
        public T? Data { get; set; }
    }
}
