using R_APICommonDTO;
using System.Collections.Generic;

namespace PMF00200COMMON
{
    public class PMF00200Record<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
