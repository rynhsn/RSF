using R_APICommonDTO;

namespace PMR00460Common.DTOs
{
    //Single DTO
    public class PMR00460SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}