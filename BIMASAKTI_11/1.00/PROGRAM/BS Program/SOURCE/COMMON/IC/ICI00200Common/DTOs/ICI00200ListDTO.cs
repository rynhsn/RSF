using System.Collections.Generic;
using R_APICommonDTO;

namespace ICI00200Common.DTOs
{
    public class ICI00200ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}