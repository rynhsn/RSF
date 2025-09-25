using R_APICommonDTO;

namespace ICI00200Common.DTOs
{
    public class ICI00200SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}