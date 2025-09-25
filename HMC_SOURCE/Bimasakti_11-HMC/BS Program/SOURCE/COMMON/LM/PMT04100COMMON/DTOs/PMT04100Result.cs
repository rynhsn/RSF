using R_APICommonDTO;
using System.Collections.Generic;

namespace PMT04100COMMON
{
    public class PMT04100SingleResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
    public class PMT04100ListResult<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}
