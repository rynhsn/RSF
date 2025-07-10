using R_APICommonDTO;

namespace ICR00100Common.DTOs
{
    public class ICR00100SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}