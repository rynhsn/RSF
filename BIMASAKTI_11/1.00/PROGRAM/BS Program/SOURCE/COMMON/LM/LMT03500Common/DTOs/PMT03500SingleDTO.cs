using R_APICommonDTO;

namespace PMT03500Common.DTOs
{
    public class PMT03500SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}