using System.Collections.Generic;
using R_APICommonDTO;

namespace HDI00100Common.DTOs
{
    public class HDI00100ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}