using System.Collections.Generic;
using R_APICommonDTO;

namespace LMT03500Common.DTOs
{
    public class LMT03500ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
    
    public class LMT03500ExcelDTO : R_APIResultBaseDTO
    {
        public byte[] FileBytes { get; set; }
    }
}