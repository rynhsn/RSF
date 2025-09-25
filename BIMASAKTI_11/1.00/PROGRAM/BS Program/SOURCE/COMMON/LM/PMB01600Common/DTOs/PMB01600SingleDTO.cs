using R_APICommonDTO;

namespace PMB01600Common.DTOs
{
    public class PMB01600SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
