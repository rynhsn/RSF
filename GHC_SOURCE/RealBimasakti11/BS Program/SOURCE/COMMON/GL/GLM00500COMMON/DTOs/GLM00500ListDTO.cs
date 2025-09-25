using System.Collections.Generic;
using R_APICommonDTO;

namespace GLM00500Common.DTOs
{
    public class GLM00500ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
        // public IAsyncEnumerable<T> Data { get; set; }
    }
}