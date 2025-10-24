using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APR00700COMMON.DTO_s
{
    public class APR00700SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
