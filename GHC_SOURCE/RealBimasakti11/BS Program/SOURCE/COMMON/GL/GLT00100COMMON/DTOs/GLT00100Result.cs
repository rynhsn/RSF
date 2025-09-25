using R_APICommonDTO;
using System.Collections.Generic;

namespace GLT00100COMMON
{
    public class GLT00100ListResult<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

    public class GLT00100RecordResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
