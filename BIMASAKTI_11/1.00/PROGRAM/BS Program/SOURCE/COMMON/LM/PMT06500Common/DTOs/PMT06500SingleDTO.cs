using R_APICommonDTO;

namespace PMT06500Common.DTOs
{
    public class PMT06500SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}