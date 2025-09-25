using System.Collections.Generic;
using R_APICommonDTO;

namespace GLR00100Common.DTOs
{
    public class GLR00100ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}