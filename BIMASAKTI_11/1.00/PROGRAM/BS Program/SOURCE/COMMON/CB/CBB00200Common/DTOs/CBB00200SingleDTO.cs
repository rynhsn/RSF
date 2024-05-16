using R_APICommonDTO;

namespace CBB00200Common.DTOs
{
    public class CBB00200SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}