using R_APICommonDTO;

namespace Lookup_TXCOMMON.DTOs.Utilities
{
    public class TXLGenericRecord<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
