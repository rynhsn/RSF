using R_APICommonDTO;
using System.Collections.Generic;

namespace PMB04000COMMON.DTO.Utilities
{
    public class PMB04000GenericList<T> : R_APIResultBaseDTO
    {
        public List<T>? Data { get; set; }

    }
}
