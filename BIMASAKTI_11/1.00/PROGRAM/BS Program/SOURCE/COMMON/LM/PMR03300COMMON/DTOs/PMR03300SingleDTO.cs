using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR03300COMMON.DTOs
{
    public class PMR03300SingleDTO<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
