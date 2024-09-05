using R_APICommonDTO;

namespace TXB00200Common.DTOs
{
    public class TXB00200SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}