using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00600COMMON.DTO_s
{
    public class PMR00600Result<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
