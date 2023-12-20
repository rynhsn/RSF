using System.Collections.Generic;
using R_APICommonDTO;

namespace GSM04500Common.DTOs
{
    public class GSM04500ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}