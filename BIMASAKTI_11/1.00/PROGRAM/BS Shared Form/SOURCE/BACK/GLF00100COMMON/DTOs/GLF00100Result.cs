using R_APICommonDTO;
using System.Collections.Generic;

namespace GLF00100COMMON
{
    public class GLF00100ListResult<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
    public class GLF00100SingleResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
