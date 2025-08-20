using System.Collections.Generic;
using R_APICommonDTO;

namespace PMB05000Common.DTOs
{
    public class PMB05000ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}