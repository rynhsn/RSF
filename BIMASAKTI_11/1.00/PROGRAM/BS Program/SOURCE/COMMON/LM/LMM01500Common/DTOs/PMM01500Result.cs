using R_APICommonDTO;
using System.Collections.Generic;

namespace PMM01500COMMON
{
    public class PMM01500ListResult<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
    public class PMM01500SingleResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
