using System.Collections.Generic;
using R_APICommonDTO;

namespace PMT03500Common.DTOs
{
    public class PMT03500ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}