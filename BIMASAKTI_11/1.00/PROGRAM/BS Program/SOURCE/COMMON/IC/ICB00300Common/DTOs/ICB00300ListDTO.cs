using System.Collections.Generic;
using R_APICommonDTO;

namespace ICB00300Common.DTOs
{
    public class ICB00300ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}