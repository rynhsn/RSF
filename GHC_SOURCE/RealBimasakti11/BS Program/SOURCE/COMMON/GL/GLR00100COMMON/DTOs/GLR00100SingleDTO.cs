using R_APICommonDTO;

namespace GLR00100Common.DTOs
{
    public class GLR00100SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}