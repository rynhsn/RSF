using System.Collections.Generic;
using R_APICommonDTO;

namespace TXB00200Common.DTOs
{
    public class TXB00200ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}