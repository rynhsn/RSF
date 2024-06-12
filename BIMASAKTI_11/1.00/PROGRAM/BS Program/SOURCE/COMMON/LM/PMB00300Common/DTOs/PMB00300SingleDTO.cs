using R_APICommonDTO;

namespace PMB00300Common.DTOs
{
    public class PMB00300SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}