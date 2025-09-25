using System.Collections.Generic;
using R_APICommonDTO;

namespace LMM02500Common.DTOs
{
    public class LMM02500ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}