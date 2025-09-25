using R_APICommonDTO;
using System.Collections.Generic;

namespace CBR00600COMMON
{
    public class CBR00600Record<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
    public class CBR00600List<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}
