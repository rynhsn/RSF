using R_APICommonDTO;

namespace PMT06000Common.DTOs
{
    public class PMT06000SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}