using R_APICommonDTO;
using System;
using System.Collections.Generic;

namespace PMM10000COMMON.UtilityDTO
{
    public class PMM10000GenericList<T> : R_APIResultBaseDTO
    {
        public List<T>? Data { get; set; }
    }
}
