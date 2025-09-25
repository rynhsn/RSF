using R_APICommonDTO;
using System.Collections.Generic;

namespace GLTR00100COMMON
{
    public class GLTR00100List<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

    public class GLTR00100Record<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
