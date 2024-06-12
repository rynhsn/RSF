using System.Collections.Generic;
using R_APICommonDTO;

namespace PMB00300Common.DTOs
{
    public class PMB00300ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}