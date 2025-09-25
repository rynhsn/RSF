using R_APICommonDTO;

namespace ICB00100Common.DTOs
{
    public class ICB00100SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}