using System.Collections.Generic;
using R_APICommonDTO;

namespace ICR00600Common.DTOs
{
    public class ICR00600ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T>? Data { get; set; }
    }
}