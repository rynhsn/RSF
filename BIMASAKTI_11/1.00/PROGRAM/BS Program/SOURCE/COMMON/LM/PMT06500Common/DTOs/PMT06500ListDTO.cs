using System.Collections.Generic;
using R_APICommonDTO;

namespace PMT06500Common.DTOs
{
    public class PMT06500ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}