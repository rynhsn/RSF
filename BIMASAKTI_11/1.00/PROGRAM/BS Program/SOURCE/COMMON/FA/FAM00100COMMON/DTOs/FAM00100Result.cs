using R_APICommonDTO;
using System.Collections.Generic;

namespace FAM00100Common.DTOs
{
    public class FAM00100SingleResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
    public class FAM0100ListResult<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}
