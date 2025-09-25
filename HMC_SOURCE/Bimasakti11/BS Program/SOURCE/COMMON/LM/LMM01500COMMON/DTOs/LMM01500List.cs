using R_APICommonDTO;
using System.Collections.Generic;

namespace LMM01500COMMON
{
    public class LMM01500List<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

}
