using R_APICommonDTO;
using System.Collections.Generic;

namespace FAM00200Common.DTOs
{
    public class FAM00200SingleResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }

    public class FAM00200ListResult<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}
