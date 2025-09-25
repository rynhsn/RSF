using System.Collections.Generic;
using R_APICommonDTO;

namespace PMR00460Common.DTOs
{
    //List DTO
    public class PMR00460ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}