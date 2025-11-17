using R_APICommonDTO;
using System.Collections.Generic;

namespace ICT00900COMMON.Utility_DTO
{
    public class ICT00900GenericList<T> : R_APIResultBaseDTO
    {
        public List<T>? Data { get; set; }
    }
    public class ICT00900GenericRecord<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
