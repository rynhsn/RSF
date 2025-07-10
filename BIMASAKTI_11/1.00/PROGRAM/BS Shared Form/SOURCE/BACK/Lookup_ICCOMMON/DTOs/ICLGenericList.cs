using System.Collections.Generic;
using R_APICommonDTO;

namespace Lookup_ICCOMMON.DTOs
{
    public class ICLGenericList<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

    public class ICLGenericRecord<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}