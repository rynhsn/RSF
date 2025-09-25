using System.Collections.Generic;
using R_APICommonDTO;

namespace HDR00200Common.DTOs
{
    public class HDR00200ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}   
