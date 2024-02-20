using R_APICommonDTO;

namespace LMT03500Common.DTOs
{
    public class LMT03500SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}