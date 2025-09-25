using R_APICommonDTO;

namespace PQM00500COMMON.DTO_s.Helper
{
    public class GeneralRecordAPIResultDTO<T> : R_APIResultBaseDTO
    {
        public T data { get; set; }
    }
}
