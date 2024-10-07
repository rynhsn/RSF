using System.Collections.Generic;
using R_APICommonDTO;

namespace Lookup_APCOMMON.DTOs
{
    public class APLGenericList<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

    public class APLGenericRecord<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}