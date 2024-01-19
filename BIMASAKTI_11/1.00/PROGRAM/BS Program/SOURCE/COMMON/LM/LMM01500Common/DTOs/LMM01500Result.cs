using R_APICommonDTO;
using System.Collections.Generic;

namespace LMM01500COMMON
{
    public class LMM01500ListResult<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
    public class LMM01500SingleResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
