using System.Collections.Generic;
using R_APICommonDTO;

namespace ICR00100Common.DTOs
{
    public class ICR00100ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T>? Data { get; set; }
    }
}