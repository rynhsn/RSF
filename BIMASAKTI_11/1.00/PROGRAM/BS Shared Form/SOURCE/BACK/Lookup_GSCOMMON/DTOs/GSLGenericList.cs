using R_APICommonDTO;
using System.Collections.Generic;

namespace Lookup_GSCOMMON.DTOs
{
    public class GSLGenericList<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

    public class GSLGenericRecord<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
