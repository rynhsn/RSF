using R_APICommonDTO;

namespace ICR00600Common.DTOs
{
    public class ICR00600SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}