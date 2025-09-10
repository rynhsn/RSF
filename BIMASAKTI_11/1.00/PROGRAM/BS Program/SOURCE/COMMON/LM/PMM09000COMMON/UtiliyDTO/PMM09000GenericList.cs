using R_APICommonDTO;
using System.Collections.Generic;

namespace PMM09000COMMON.UtiliyDTO
{
    public class PMM09000GenericList<T> : R_APIResultBaseDTO
    {
        public List<T>? Data { get; set; }

    }
}
