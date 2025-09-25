using R_APICommonDTO;
using System.Collections.Generic;

namespace GLM00400COMMON
{
    public class GLM00400List<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

}
