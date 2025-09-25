using R_APICommonDTO;

namespace HDR00200Common.DTOs
{
    public class HDR00200SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}