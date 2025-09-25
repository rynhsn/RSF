using System.Collections.Generic;
using R_APICommonDTO;

namespace PMT06000Common.DTOs
{
    public class PMT06000ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}