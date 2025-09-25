using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBT01200Common.DTOs
{
    public class CBT01200ListResult<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

    public class CBT01200RecordResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
