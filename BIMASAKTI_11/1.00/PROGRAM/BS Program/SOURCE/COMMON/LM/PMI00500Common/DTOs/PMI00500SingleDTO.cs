using R_APICommonDTO;

namespace PMI00500Common.DTOs
{
    public class PMI00500SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}