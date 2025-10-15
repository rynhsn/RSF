using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMR03300COMMON.DTOs
{
    public class PMR03300ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}
