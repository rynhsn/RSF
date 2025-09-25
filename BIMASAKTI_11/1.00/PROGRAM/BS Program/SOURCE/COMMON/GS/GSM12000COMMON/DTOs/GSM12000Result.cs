using R_APICommonDTO;
using System.Collections.Generic;

namespace GSM12000COMMON
{
    public class GSM12000SingleResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
    public class GSM12000ListResult<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}
