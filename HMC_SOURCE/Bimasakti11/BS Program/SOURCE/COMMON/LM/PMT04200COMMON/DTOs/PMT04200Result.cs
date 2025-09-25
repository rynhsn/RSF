using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMT04200Common.DTOs
{
    public class PMT04200ListResult<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

    public class PMT04200RecordResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
