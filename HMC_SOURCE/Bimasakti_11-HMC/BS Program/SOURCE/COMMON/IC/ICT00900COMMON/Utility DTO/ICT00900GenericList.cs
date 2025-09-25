using R_APICommonDTO;
using System.Collections.Generic;

namespace ICT00900COMMON.Utility_DTO
{
    public class ICT00900GenericList<T> : R_APIResultBaseDTO
    {
        public List<T>? Data { get; set; }
    }
}
