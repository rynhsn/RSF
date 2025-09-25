using R_APICommonDTO;

namespace Lookup_GLCOMMON.DTOs
{
    public class GLLGenericRecord<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
