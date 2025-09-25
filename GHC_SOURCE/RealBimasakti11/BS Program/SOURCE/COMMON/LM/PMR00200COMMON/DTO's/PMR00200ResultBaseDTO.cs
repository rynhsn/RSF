using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR00200COMMON.DTO_s
{
    public class PMR00200ResultBaseDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
