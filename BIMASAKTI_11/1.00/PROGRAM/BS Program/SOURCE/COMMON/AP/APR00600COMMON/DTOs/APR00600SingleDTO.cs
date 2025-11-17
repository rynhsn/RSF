using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APR00600COMMON.DTOs
{
    public class APR00600SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
