using R_APICommonDTO;
using System.Collections.Generic;

namespace PMM01000COMMON
{
    public class PMM01000List<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
    public class PMM01000Record<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
