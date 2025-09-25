using R_APICommonDTO;
using System.Collections.Generic;

namespace CBT01100COMMON
{
    public class CBT01100ListResult<T> : R_APIResultBaseDTO
    {
        public IAsyncEnumerable<T> Data { get; set; }
    }

    public class CBT01100RecordResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
