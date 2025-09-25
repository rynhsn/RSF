using R_APICommonDTO;

namespace APR00500Common.DTOs
{
    public class APR00500SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}