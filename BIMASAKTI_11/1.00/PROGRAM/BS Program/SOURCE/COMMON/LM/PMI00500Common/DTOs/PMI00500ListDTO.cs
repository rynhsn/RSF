using System.Collections.Generic;
using R_APICommonDTO;

namespace PMI00500Common.DTOs
{
    public class PMI00500ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}