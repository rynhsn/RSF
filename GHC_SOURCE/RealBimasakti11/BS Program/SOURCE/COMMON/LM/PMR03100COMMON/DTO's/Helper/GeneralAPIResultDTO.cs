using R_APICommonDTO;

namespace PMR03100COMMON.DTO_s.Helper
{
    public class GeneralAPIResultDTO<T> : R_APIResultBaseDTO
    {
        public T data { get; set; }
    }
}
