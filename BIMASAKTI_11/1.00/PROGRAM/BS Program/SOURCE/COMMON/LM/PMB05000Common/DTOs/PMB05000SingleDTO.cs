using R_APICommonDTO;

namespace PMB05000Common.DTOs
{
    public class PMB05000SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}