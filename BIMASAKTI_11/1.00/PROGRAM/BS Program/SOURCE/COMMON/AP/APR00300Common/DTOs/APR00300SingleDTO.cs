using R_APICommonDTO;

namespace APR00300Common.DTOs
{
    // DTO ini digunakan untuk menampung data single
    public class APR00300SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}