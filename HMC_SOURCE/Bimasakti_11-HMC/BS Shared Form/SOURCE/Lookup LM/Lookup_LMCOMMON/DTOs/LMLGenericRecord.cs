using R_APICommonDTO;

namespace Lookup_PMCOMMON.DTOs
{
    public class LMLGenericRecord<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
