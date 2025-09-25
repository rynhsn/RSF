using R_APICommonDTO;


namespace PMR00800COMMON.DTO_s.Helper
{
    public class GeneralAPIResultDTO<T> : R_APIResultBaseDTO
    {
        public T data { get; set; }
    }
}
