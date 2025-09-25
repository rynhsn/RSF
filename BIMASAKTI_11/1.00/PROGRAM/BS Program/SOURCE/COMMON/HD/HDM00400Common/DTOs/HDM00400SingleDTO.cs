using R_APICommonDTO;

namespace HDM00400Common.DTOs
{
    public class HDM00400SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }

    public class HDM00400ActiveInactiveDTO
    {
        public bool IsSuccess { get; set; } = true;
    }
}