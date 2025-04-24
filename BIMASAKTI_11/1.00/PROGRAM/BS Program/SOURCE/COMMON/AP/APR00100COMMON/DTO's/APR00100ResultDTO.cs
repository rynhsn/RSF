using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APR00100COMMON.DTO_s
{
    public class APR00100ResultDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
