using System.Collections.Generic;
using R_APICommonDTO;

namespace PMR02600Common.DTOs
{
    public class PMR02600ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}