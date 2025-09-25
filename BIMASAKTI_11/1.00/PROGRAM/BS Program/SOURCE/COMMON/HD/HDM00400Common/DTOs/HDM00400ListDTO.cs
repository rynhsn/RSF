using System.Collections.Generic;
using R_APICommonDTO;

namespace HDM00400Common.DTOs
{
    public class HDM00400ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}