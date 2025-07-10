using System.Collections.Generic;
using R_APICommonDTO;

namespace ICB00100Common.DTOs
{
    public class ICB00100ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}