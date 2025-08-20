using System;
using System.Collections.Generic;
using R_APICommonDTO;

namespace PMR03000Common.DTOs
{
    public class PMR03000ListDTO<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}