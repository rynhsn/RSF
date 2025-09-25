using R_APICommonDTO;

namespace LMM02500Common.DTOs
{
    public class LMM02500SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}