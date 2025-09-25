using System.Collections.Generic;
using R_APICommonDTO;

namespace GLR00300Common
{
    public class GenericList<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}