using R_APICommonDTO;
using System.Collections.Generic;

namespace CBT00200COMMON
{
    public class CBT00200SingleResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
    public class CBT00200ListResult<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}
