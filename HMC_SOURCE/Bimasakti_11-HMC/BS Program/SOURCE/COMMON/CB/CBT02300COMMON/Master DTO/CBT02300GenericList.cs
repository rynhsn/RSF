using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace CBT02300COMMON
{
    public class CBT02300GenericList<T> : R_APIResultBaseDTO
    {
        public List<T>? Data { get; set; }
    }
}
