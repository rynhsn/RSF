using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR02400COMMON.DTO_s
{
    public class PMR02400Result<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
