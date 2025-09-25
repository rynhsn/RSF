using System.Collections.Generic;
using R_APICommonDTO;

namespace Lookup_HDCOMMON.DTOs
{
    public class HDLGenericList<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

    public class HDLGenericRecord<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}