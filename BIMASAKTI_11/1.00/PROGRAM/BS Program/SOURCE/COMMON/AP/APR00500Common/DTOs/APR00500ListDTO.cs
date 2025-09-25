using System.Collections.Generic;
using R_APICommonDTO;

namespace APR00500Common.DTOs
{
    public class APR00500ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}