using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APR00600COMMON.DTOs
{
    public class APR00600ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}
