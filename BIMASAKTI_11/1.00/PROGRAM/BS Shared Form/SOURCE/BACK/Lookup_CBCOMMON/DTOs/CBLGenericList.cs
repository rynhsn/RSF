using System.Collections.Generic;
using R_APICommonDTO;

namespace Lookup_CBCOMMON.DTOs
{
    public class CBLGenericList<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

    public class CBLGenericRecord<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}