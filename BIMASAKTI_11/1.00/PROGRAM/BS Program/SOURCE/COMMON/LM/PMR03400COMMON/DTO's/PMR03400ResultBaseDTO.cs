using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR03400COMMON.DTO_s
{
    public class PMR03400ResultBaseDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
